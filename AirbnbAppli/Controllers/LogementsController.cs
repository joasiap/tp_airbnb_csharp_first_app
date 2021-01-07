using System;
using System.Collections.Generic;
using System.Linq;
using AirbnbAppli.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbAppli.Controllers
{
    public class LogementsController : Controller
    {

        private Context _db;

        public LogementsController(Context db)
        {
            _db = db;
        }

        // GET: LogementsController/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["departements"] = GetDepartements();
            return View();
        }

        // POST: LogementsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LogementViewModel logementVM)
        {
            if (!ModelState.IsValid)
            {
                ViewData["departements"] = GetDepartements();
                return View();
            }

            try
            {
                // récupération de l'utilisateur connecté
                Utilisateur utilisateur = null;
                if (HttpContext.Request.Cookies.ContainsKey("userId"))
                {
                    int idUtilisateur = Convert.ToInt32(HttpContext.Request.Cookies["userId"]);
                    utilisateur = _db.Utilisateurs.Single(utilisateur => utilisateur.Id == idUtilisateur);
                }

                if(utilisateur == null)
                {
                    TempData["messageErreur"] = "Connectez-vous pour pouvoir créer une annonce";
                    return RedirectToAction("Index", "Home");
                }

                Departement departement = _db.Departements.Single(departement => departement.Id == Convert.ToInt32(logementVM.Departement));
                Adresse adresse = new Adresse()
                {
                    Rue = logementVM.Rue,
                    CodePostal = logementVM.CodePostal,
                    Ville = logementVM.Ville,
                    Departement = departement
                };
                Logement logement = new Logement()
                {
                    Titre = logementVM.Titre,
                    Description = logementVM.Description,
                    NbPersonnes = logementVM.NbPersonnes,
                    Proprietaire = utilisateur,
                    Adresse = adresse
                };
                _db.Adresses.Add(adresse);
                _db.Logements.Add(logement);
                _db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ViewData["departements"] = GetDepartements();
                return View();
            }
        }


        /**
         * Récupère la liste de départements de la BDD
         */
        public List<Departement> GetDepartements()
        {
            return _db.Departements.ToList();
        }




        /*
        // GET: LogementsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        public ActionResult CreateReservation(Reservation reservation)
        {
            return View("Details");
        }*/

        /*
        // GET: LogementsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LogementsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LogementsController/Edit/5
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

        // GET: LogementsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LogementsController/Delete/5
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
        }*/
    }
}
