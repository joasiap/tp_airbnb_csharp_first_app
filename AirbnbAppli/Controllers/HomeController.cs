using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AirbnbAppli.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System;

namespace AirbnbAppli.Controllers
{
    public class HomeController : Controller
    {

        private Context _db;

        public HomeController(Context db)
        {
            _db = db;
        }


        public async Task<IActionResult> IndexAsync(SearchFormViewModel searchFormVM = null)
        {
            // récupère l'utilisateur authentifié
            Utilisateur utilisateur = null;
            if (HttpContext.Request.Cookies.ContainsKey("userId"))
            {
                int idUtilisateur = Convert.ToInt32(HttpContext.Request.Cookies["userId"]);
                utilisateur = _db.Utilisateurs.Single(utilisateur => utilisateur.Id == idUtilisateur);
            }

          
            // récupération des informations renseignées dans le formulaire de recherche
            int nbPersonnes = searchFormVM.NbPersonnes;
            String ville = searchFormVM.Ville;
            int idDepartement = Convert.ToInt32(searchFormVM.Departement);


            // récupération de liste de logements à réserver (avec Adresse et Département d'un logement)
            var logements = _db.Logements.AsQueryable();

            // car je veux pas pouvoir louer mes propres logements
            if (utilisateur != null)
            {
                logements = logements.Where(logement => logement.Proprietaire.Id != utilisateur.Id);
            }

            if (nbPersonnes > 0)
            {
                logements = logements.Where(logement => logement.NbPersonnes == searchFormVM.NbPersonnes);
            }
            if (!String.IsNullOrEmpty(ville))
            {
                logements = logements.Where(logement => logement.Adresse.Ville == searchFormVM.Ville);
            }
            if (idDepartement > 0)
            {
                logements = logements.Where(logement => logement.Adresse.Departement.Id == Convert.ToInt32(searchFormVM.Departement));
            }

            logements = logements
                .Include(logement => logement.Adresse) // récupère aussi des infos sur Adresse (aggrégation)
                .ThenInclude(adresse => adresse.Departement); // et ensuite relation Adresse - Département


            // récupération des départements si la liste n'a pas encore été initialisée
            bool isDepartementInDatabase = _db.Departements.Any();

            if (!isDepartementInDatabase)
            {
                await GetDepartementsAsync();
            }

            List<Logement> mesLogements = null;
            List<Reservation> mesReservations = null;

            if (utilisateur != null)
            {
                mesLogements = getMesLogements(utilisateur.Id);
                mesReservations = getMesReservations(utilisateur.Id);
            }

            ViewData["logements"] = new List<Logement>(logements);
            ViewData["mesLogements"] = mesLogements;
            ViewData["mesReservations"] = mesReservations;
            ViewData["departements"] = GetDepartements();
            return View();
        }


        /**
         * Retourne la liste de logements que j'ai mis en location
         */
        public List<Logement> getMesLogements(int idUtilisateur)
        {
            return _db.Logements
                .Where(logement => logement.Proprietaire.Id == idUtilisateur)
                .Include(logement => logement.Adresse) 
                .ThenInclude(adresse => adresse.Departement)
                .ToList(); 
        }

        /**
        * Retourne la liste de mes réservations
        */
        public List<Reservation> getMesReservations(int idUtilisateur)
        {
            return _db.Reservations
               .Where(reservation => reservation.Locateur.Id == idUtilisateur)
               .Include(reservation => reservation.Logement)
               .ThenInclude(reservation => reservation.Adresse)
               .ThenInclude(adresse => adresse.Departement)
               .ToList();
        }

        public List<Departement> GetDepartements()
        {
            return _db.Departements.ToList();
        }

        /**
         * Permet de récupérer la liste des départements de l'API GeoGouv
         */
        public async Task GetDepartementsAsync()
        {

            var client = new HttpClient();
            
            // header -> json
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("https://geo.api.gouv.fr/departements");

            if (response.IsSuccessStatusCode)
            {
                List<Departement> departements = JsonConvert.DeserializeObject<List<Departement>>(response.Content.ReadAsStringAsync().Result);
                foreach (Departement departement in departements)
                {
                    _db.Departements.Add(departement);
                    _db.SaveChanges();
                }
            }

            client.Dispose();    
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
