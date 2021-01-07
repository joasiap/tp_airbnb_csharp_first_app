using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbAppli.Models
{
    public class SearchFormViewModel
    {

        [Display(Name = "Nombre de personnes :")]
        public int NbPersonnes { get; set; }

        [Display(Name = "Ville :")]
        public string Ville { get; set; }

        [Display(Name = "Département :")]
        public string Departement { get; set; }
    }
}
