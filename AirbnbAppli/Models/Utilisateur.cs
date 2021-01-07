using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirbnbAppli.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }

        public string Prenom { get; set; }

        public string Nom { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string MotDePasse { get; set; }

        public bool Authentifie { get; set; }
    }
}
