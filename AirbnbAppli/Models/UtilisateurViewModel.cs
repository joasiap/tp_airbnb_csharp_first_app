using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbAppli.Models
{
    public class UtilisateurViewModel
    {
        [Required(ErrorMessage = "Ce champ ne peut pas être vide.", AllowEmptyStrings = false)]
        [StringLength(30, ErrorMessage = "Ce champ n'accepte pas plus de 30 caractères")]
        [Display(Name = "Prénom * :")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut pas être vide.", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Ce champ n'accepte pas plus de 30 caractères")]
        [Display(Name = "Nom * :")]
        public string Nom { get; set; }


        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Ce champ accepte uniquement des chiffres, sans espaces. ")]
        [StringLength(15, MinimumLength = 10,
            ErrorMessage = "Ce champ accepte au minimum 10 et au maximum 15 caractères.")]
        [Display(Name = "Téléphone :")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut pas être vide.", AllowEmptyStrings = false)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Email n'est pas valide.")]
        [Display(Name = "E-mail * :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut pas être vide.", AllowEmptyStrings = false)]
        [
            RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,}$",
            ErrorMessage = "Le mot de passe doit comporter au moins 8 caractères dont une lettre majuscule, une lettre minuscule et un chiffre.")
        ]
        [Display(Name = "Mot de passe * :")]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut pas être vide.", AllowEmptyStrings = false)]
        [Compare("MotDePasse", ErrorMessage = "Les deux mots de passe doivent être identiques.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation du mot de passe * :")]
        public string ConfirmationMotDePasse { get; set; }
    }
}
