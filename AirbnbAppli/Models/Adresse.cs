using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbAppli.Models
{
    public class Adresse
    {
        public int Id { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public virtual Departement Departement{ get; set; }
    }
}
