using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AirbnbAppli.Models;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Http;
using System;

namespace AirbnbAppli.Controllers
{
    public class UtilisateursController : Controller
    {

        private Context _db;

        public UtilisateursController(Context db)
        {
            _db = db;
        }

        /**
         * Redirige vers la page de création d'un nouveau compte utilisateur
         */
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        /**
         * Création d'un nouveau compte utilisateur
         */
        // POST: Utilisateurs/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UtilisateurViewModel utilisateurVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            // vérifie si l'e-mail existe déjà dans la BDD
            if (_db.Utilisateurs.Any(utilisateur => utilisateur.Email == utilisateurVM.Email))
            {
                ModelState.AddModelError("Email", "Cet e-mail est déjà utilisé.");
                return View();
            }   

            try
                {
                    var utilisateur = new Utilisateur
                    {
                        Prenom = utilisateurVM.Prenom,
                        Nom = utilisateurVM.Nom,
                        Email = utilisateurVM.Email,
                        Telephone = utilisateurVM.Telephone,
                        // hashache du mot de passe
                        MotDePasse = BC.HashPassword(utilisateurVM.MotDePasse)
                    };

                    _db.Utilisateurs.Add(utilisateur);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View();
                }
        }


        /**
         * Redirection vers la page de login
         */
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Utilisateur utilisateur = getUtilisateurAuthentifie(model);
        
            if (utilisateur != null)
            {
                // utilisateur marqué comme authentifié dans la BDD
                utilisateur.Authentifie = true;
                _db.Utilisateurs.Update(utilisateur);
                _db.SaveChanges();

                // enregistrement des valeurs dans la session
                HttpContext.Session.SetInt32("userId", utilisateur.Id);
                HttpContext.Session.SetInt32("userAuthentifie", Convert.ToInt32(utilisateur.Authentifie));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "E-mail ou mot de passe incorrect.");
                return View();
            }
        }

        /*
        // création de cookies
        CookieOptions cookieOptions = new CookieOptions();
        cookieOptions.Expires = DateTime.Now.AddYears(1);
        HttpContext.Response.Cookies.Append("userId", utilisateur.Id.ToString(), cookieOptions);
        HttpContext.Response.Cookies.Append("userAuthentifie", utilisateur.Authentifie.ToString(), cookieOptions);
        */


        /**
         * Permet de savoir si l'utilisateur s'inscrit avec l'email et le mot de passe correct
         */
        public Utilisateur getUtilisateurAuthentifie(LoginViewModel model)
        {
            // récupérer l'utilisateur de la BDD
            Utilisateur utilisateur = _db.Utilisateurs.Where(utilisateur => utilisateur.Email == model.Email).SingleOrDefault();

            // vérifie si l'utilisateur existe et que le mot de passe est correct
            if (utilisateur == null || !BC.Verify(model.MotDePasse, utilisateur.MotDePasse))
                return null;
            else
                return utilisateur;       
        }


        public ActionResult Logout()
        {
            if (HttpContext.Session.GetInt32("userId") != null && HttpContext.Session.GetInt32("userId") > 0) {

                // récupérer l'utilisateur de la BDD
                Utilisateur utilisateur = _db.Utilisateurs.Where(utilisateur => utilisateur.Id == HttpContext.Session.GetInt32("userId")).SingleOrDefault();

                utilisateur.Authentifie = false;
                _db.Utilisateurs.Add(utilisateur);
                _db.Utilisateurs.Update(utilisateur);
                _db.SaveChanges();

                HttpContext.Session.Remove("userId");
                HttpContext.Session.Remove("userAuthentifie");
                HttpContext.Session.Clear();

                /*
                HttpContext.Response.Cookies.Delete("userId");
                HttpContext.Response.Cookies.Delete("userAuthentifie");
                */
            }
            
            return RedirectToAction("Index", "Home");
        }


        /*

        // GET: Utilisateurs
        public ActionResult Index()
        {
            return View();
        }

        // GET: Utilisateurs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

   

        // POST: Utilisateurs/Create
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

        // GET: Utilisateurs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Utilisateurs/Edit/5
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

        // GET: Utilisateurs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Utilisateurs/Delete/5
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
