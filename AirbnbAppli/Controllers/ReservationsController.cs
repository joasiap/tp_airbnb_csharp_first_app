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

            List<Reservation> reservations = getAllReservationsByLogement(logement.Id);

            ViewData["reservations"] = Newtonsoft.Json.JsonConvert.SerializeObject(reservations);
            ViewData["logement"] = logement;
            return View(reservationVM);
        }

        /**
         * Récupère la liste de réservations pour un logement
         */ 
        public List<Reservation> getAllReservationsByLogement(int idLogement)
        {
            return _db.Reservations
                .Where(reservation => reservation.Logement.Id == idLogement)
                .ToList();
        }

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationModelView reservationVM)
        {
            // récupère l'utilisateur authentifié
            Utilisateur utilisateur = null;
            if (HttpContext.Session.GetInt32("userId") != null && HttpContext.Session.GetInt32("userId") > 0)
            {
                int idUtilisateur = (int) HttpContext.Session.GetInt32("userId"); //Convert.ToInt32(HttpContext.Request.Cookies["userId"]);
                utilisateur = _db.Utilisateurs.Single(utilisateur => utilisateur.Id == idUtilisateur);
            }

            if (utilisateur == null)
            {
                TempData["messageErreur"] = "Connectez-vous pour effectuer une réservation";
                return RedirectToAction("Index", "Home");
            }

            Logement logement = getLogement(reservationVM.IdLogement);
            List<Reservation> reservations = getAllReservationsByLogement(logement.Id);

            ViewData["reservations"] = Newtonsoft.Json.JsonConvert.SerializeObject(reservations);
            ViewData["logement"] = logement;

            foreach (Reservation reservation in reservations)
            {
                bool overlap = isPeriodeDisponible(reservation.DateDebut, reservation.DateFin, reservationVM.DateDebut, reservationVM.DateFin);
                if (overlap)
                {
                    ModelState.AddModelError("", "La réservation n'est pas disponible pour la période choisie!");
                    return View(reservationVM);
                }
            }

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

                TempData["messageSucces"] = "Réservation effectuée avec succès !";
                return RedirectToAction("Details", new { id = reservation.Id });
            }
            catch
            {
                return View(reservationVM);
            }
 
        }

        /**
         *  Permet de vérifier si le logement est disponible 
         *  dans la période choisie lors de la réservation
         */ 
        public bool isPeriodeDisponible(DateTime dateDebut1, DateTime dateFin1, DateTime dateDebut2, DateTime dateFin2)
        {
            /*
            return (
                (dateDebut2 >= dateDebut1 && dateDebut2 < dateFin1) || (dateFin2 <= dateFin1 && TE > dateDebut1) || (dateDebut2 <= dateDebut1 && dateFin2 >= dateFin1)
            );
            */

            return (
                // 1. Case:
                //
                //       dateDebut2-------dateFin2
                //    dateDebut1------dateFin1 
                //
                // dateDebut2 is after dateDebut1 but before dateFin1
                (dateDebut2 >= dateDebut1 && dateDebut2 < dateFin1)
                || // or

                // 2. Case
                //
                //    dateDebut2-------dateFin2
                //        dateDebut1---------dateFin1
                //
                // dateFin2 is before dateFin1 but after dateDebut1
                (dateFin2 <= dateFin1 && dateFin2 > dateDebut1)
                || // or

                // 3. Case
                //
                //  dateDebut2----------dateFin2
                //     dateDebut1----dateFin1
                //
                // dateDebut2 is before dateDebut1 and dateFin2 is after dateFin1
                (dateDebut2 <= dateDebut1 && dateFin2 >= dateFin1)
            );
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
        private Logement getLogement(int id)
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
