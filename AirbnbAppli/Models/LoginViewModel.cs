using System.ComponentModel.DataAnnotations;

namespace AirbnbAppli.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ce champ ne peut pas être vide.", AllowEmptyStrings = false)]
        [Display(Name = "E-mail * :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut pas être vide.", AllowEmptyStrings = false)]
        [Display(Name = "Mot de passe * :")]
        [DataType(DataType.Password)]
        public string MotDePasse  { get; set; }

    }
}
