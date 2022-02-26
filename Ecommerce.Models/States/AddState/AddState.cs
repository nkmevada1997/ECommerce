using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.States.AddState
{
    public class AddState
    {
        [Required]
        [Display(Name = "State Name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only Letters")]
        public string Name { get; set; } = string.Empty;

        public Guid CountryId { get; set; }
    }
}
