using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Counties.AddCountry
{
    public class AddCountryModel
    {
        [Required]
        [Display(Name = "Country Name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only Letters")]
        public string Name { get; set; } = string.Empty;
    }
}
