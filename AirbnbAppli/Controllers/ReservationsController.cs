using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AirbnbAppli.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AirbnbAppli.Controllers
{
    public class ReservationsController : Controller
    {
        private Context _db;

        public ReservationsController(Context db)
        {
            _db = db;
        }

        /**
         * Redirige vers la page de Détail de logement
         * où on peut créer une réservation
         */
        // GET: ReservationsController/Create
        public ActionResult Create(int id)
        {
            Logement logement = getLogement(id);

            var reservationVM = new ReservationModelView
            {
                IdLogement = logement.Id,
                DateDebut = DateTime.Now,
                DateFin = DateTime.Now.AddDays(7)
            };
          
            ViewData["logement"] = logement;
            return View(reservationVM);
        }

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationModelView reservationVM)
        {
            // récupère l'utilisateur authentifié
            Utilisateur utilisateur = null;
            if (HttpContext.Request.Cookies.ContainsKey("userId"))
            {
                int idUtilisateur = Convert.ToInt32(HttpContext.Request.Cookies["userId"]);
                utilisateur = _db.Utilisateurs.Single(utilisateur => utilisateur.Id == idUtilisateur);
            }

            if (utilisateur == null)
            {
                TempData["messageErreur"] = "Connectez-vous pour effectuer une réservation";
                return RedirectToAction("Index", "Home");
            }

            Logement logement = getLogement(reservationVM.IdLogement);
            ViewData["logement"] = logement;

            if (!ModelState.IsValid)
            {
                return View(reservationVM);
            }

            if (!reservationVM.Validated)
            {
                var validationResults = reservationVM.Validate(new ValidationContext(reservationVM, null, null));
                foreach (var error in validationResults)
                    foreach (var memberName in error.MemberNames)
                        ModelState.AddModelError(memberName, error.ErrorMessage);


                return View(reservationVM);
            }


            try
            {
                Reservation reservation = new Reservation()
                {
                    Logement = logement,
                    DateDebut = reservationVM.DateDebut,
                    DateFin = reservationVM.DateFin,
                    Locateur = utilisateur
                };

                _db.Reservations.Add(reservation);
                _db.SaveChanges();

                TempData["messageSucces"] = "Réservation effectuée avec succès";
                return RedirectToAction("Details", new { id = reservation.Id });
            }
            catch
            {
                return View(reservationVM);
            }
 
        }

        /**
         * Redirige vers la page de détail d'une réservation
         */
        // GET: ReservationsController/Details/5
        public ActionResult Details(int id)
        {
            Reservation reservation = _db.Reservations
                .Where(reservation => reservation.Id == id)
                .Include(reservation => reservation.Logement)
                .ThenInclude(logement => logement.Adresse)
                .ThenInclude(adresse => adresse.Departement)
                .FirstOrDefault();

            ViewData["reservation"] = reservation;
            return View();
        }


        /**
         * Permet de récupérer un logement de la BDD 
         */
        public Logement getLogement(int id)
        {
            return _db.Logements
             .Where(logement => logement.Id == id)
             .Include(logement => logement.Proprietaire)
             .Include(logement => logement.Adresse)          // récupère aussi des infos sur Adresse (aggrégation)
             .ThenInclude(adresse => adresse.Departement)    // et ensuite relation Adresse - Département;
             .FirstOrDefault();
        }

        /*
        // GET: ReservationsController
        public ActionResult Index()
        {
      
            return View();
           
        }

        // GET: ReservationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReservationsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
