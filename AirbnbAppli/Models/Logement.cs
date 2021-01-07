using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbAppli.Models
{
    public class Logement
    {
        public int Id { get; set; }

        public string Titre { get; set; }

        public string Description { get; set; }

        public int NbPersonnes { get; set; }

        public virtual Adresse Adresse { get; set; }

        public virtual Utilisateur Proprietaire { get; set; }
    }
}
 
        