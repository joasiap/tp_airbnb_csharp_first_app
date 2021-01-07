using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirbnbAppli.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public virtual Logement Logement { get; set; }

        public virtual Utilisateur Locateur { get; set; }

       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateDebut { get; set; }

       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateFin { get; set; }

        public string Commentaire { get; set; }
    }
}
