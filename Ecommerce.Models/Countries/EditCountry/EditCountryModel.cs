using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Countries.EditCountry
{
    public class EditCountryModel
    {
        public Guid CountryId { get; set; }

        [Required]
        [Display(Name = "Country Name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only Letters")]
        public string CountryName { get; set; } = string.Empty;
    }
}
