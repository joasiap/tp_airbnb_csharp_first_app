using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbAppli.Models
{
    public class Disponibilite
    {
        public int Id { get; set; }

        public virtual Logement Logement { get; set; }

        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }

        public bool Disponible { get; set; }
    }
}
