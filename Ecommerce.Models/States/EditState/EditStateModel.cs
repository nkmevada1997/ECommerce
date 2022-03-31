using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.States.EditState
{
    public class EditStateModel
    {
        public Guid StateId { get; set; }

        [Required]
        [Display(Name = "State Name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only Letters")]
        public string StateName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Country Name")]
        public Guid CountryId { get; set; }
    }
}
