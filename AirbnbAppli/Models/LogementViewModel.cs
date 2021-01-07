using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbAppli.Models
{
    public class LogementViewModel
    {
        [Display(Name = "Titre * :")]
        [Required(ErrorMessage = "Ce champ ne peut pas être vide.", AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "Ce champ n'accepte pas plus de 100 caractères")]
        public string Titre { get; set; }

        [Display(Name = "Description :")]
        [StringLength(500, ErrorMessage = "Ce champ n'accepte pas plus de 500 caractères")]
        public string Description { get; set; }

        [Display(Name = "Nombre de personnes * :")]
        [Required(ErrorMessage = "Ce champ ne peut pas être vide.")]
        [Range(1, int.MaxValue, ErrorMessage = "Nombre invalide.")]
        public int NbPersonnes { get; set; }

        [Display(Name = "Rue * :")]
        [Required(ErrorMessage = "Ce champ ne peut pas être vide.", AllowEmptyStrings = false)]
        [StringLength(150, ErrorMessage = "Ce champ n'accepte pas plus de {0} caractères")]
        public string Rue { get; set; }

        [Display(Name = "Code postal * :")]
        [Required(ErrorMessage = "Ce champ ne peut pas être vide.", AllowEmptyStrings = false)]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Le format de code postal accepté est: 44000. ")]
        public string CodePostal { get; set; }

        [Display(Name = "Ville * :")]
        [Required(ErrorMessage = "Ce champ ne peut pas être vide.", AllowEmptyStrings = false)]
        [StringLength(60, ErrorMessage = "Ce champ n'accepte pas plus de {1} caractères")]
        public string Ville { get; set; }

        [Display(Name = "Département * :")]
        [Required(ErrorMessage = "Ce champ ne peut pas être vide.")]
        public string Departement { get; set; }
    }
}
