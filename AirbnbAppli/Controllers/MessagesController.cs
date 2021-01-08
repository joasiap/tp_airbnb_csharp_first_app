using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AirbnbAppli.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace AirbnbAppli.Controllers
{
    public class MessagesController : Controller
    {
        private Context _db;

        public MessagesController(Context db)
        {
            _db = db;
        }
        public IActionResult Index(int idProprietaire = 0)
        {
            if (idProprietaire < 1)
            {
                TempData["messageErreur"] = "Echec lors du chargement de la messagerie.";
                return RedirectToAction("Index", "Home");
            }

            // récupère l'utilisateur authentifié
            Utilisateur utilisateur = null;
            if (HttpContext.Session.GetInt32("userId") != null && HttpContext.Session.GetInt32("userId") > 0)
            {
                int idUtilisateur = (int)HttpContext.Session.GetInt32("userId");
                utilisateur = _db.Utilisateurs.Single(utilisateur => utilisateur.Id == idUtilisateur);
            }

            if (utilisateur == null)
            {
                TempData["messageErreur"] = "Connectez-vous pour envoyer un message au propriétaire de l'appartement";
                return RedirectToAction("Index", "Home");
            }

            ViewData["idProprietaire"] = idProprietaire;
            return View("Index");
        }



        /**
         * Récupère et retourne la liste de messages
         * en format JSON -> appel AJAX
         */
        public JsonResult getMessages(int idProprietaire, int idUtilisateur)
        {
            List<Message> messages = _db.Messages
                           .Where(message =>
                                   (message.Destinateur.Id == idProprietaire && message.Emetteur.Id == idUtilisateur) ||
                                   (message.Destinateur.Id == idUtilisateur && message.Emetteur.Id == idProprietaire)
                                  )
                           .Include(message => message.Destinateur)
                           .Include(message => message.Emetteur)
                           .ToList();

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(messages));
        }




        /**
         * Création d'un message
         * -> appel AJAX
         */
        // POST: ReservationsController/Create
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            try
            {
                string content = (new StreamReader(Request.Body, Encoding.UTF8)).ReadToEndAsync().Result;
                JObject jsonContent = JObject.Parse(content);
                int idProprietaire = Convert.ToInt32(jsonContent["idProprietaire"]);
                int idUtilisateur = Convert.ToInt32(jsonContent["idUtilisateur"]);
                string contenu = jsonContent["contenu"].ToString();

                Utilisateur proprietaire = _db.Utilisateurs.Single(utilisateur => utilisateur.Id == idProprietaire);
                Utilisateur utilisateurAuthentifie = _db.Utilisateurs.Single(utilisateur => utilisateur.Id == idUtilisateur);

                Message message = new Message
                {
                    Date = DateTime.Now,
                    Contenu = contenu,
                    Destinateur = proprietaire,
                    Emetteur = utilisateurAuthentifie
                };

                _db.Messages.Add(message);
                _db.SaveChanges();

                //  Send "success"
                 return Json(new { success = true });
            }
            catch
            {
                //  Send "erreur"
                return Json(new { success = false });
            }

        }
    }
}