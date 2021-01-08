using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AirbnbAppli.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public JsonResult getMessages(int idProprietaire, int idUtilisateur) { 

            int idProprietaire1 = Convert.ToInt32(idProprietaire);
            int idUtilisateur1 = Convert.ToInt32(idUtilisateur);
       
            List<Message> messages = _db.Messages
                           .Where(message =>
                                   (message.Destinateur.Id == idProprietaire && message.Emetteur.Id == idUtilisateur) ||
                                   (message.Destinateur.Id == idUtilisateur && message.Emetteur.Id == idProprietaire)
                                  )
                           .Include(message => message.Destinateur)
                           .ToList();

            //ViewData["messages"] = Newtonsoft.Json.JsonConvert.SerializeObject(messages);
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(messages));
        }
    }
}