using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbAppli.Models
{
    public class ReservationModelView : IValidatableObject
    {
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Champ obligatoire.")]
        [Display(Name = "Date de début * :")]
        public DateTime DateDebut { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Champ obligatoire.")]
        [Display(Name = "Date de fin * :")]
        [DataType(DataType.Date)]
        public DateTime DateFin { get; set; }

        public int IdLogement { get; set; }

        //public int IdProprietaire { get; set; }

        public bool Validated
        {
            get; set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTime.Compare(DateDebut, DateFin) >= 0)
            {
                yield return new ValidationResult(
                                    "La date de fin doit être supérieure à la date de début.",
                                    new[] { "DateFin" }
                               );
            }
            Validated = true;
        }
    }
}
