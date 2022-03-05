using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Cities.AddCity
{
    public class AddCityModel
    {
        [Required]
        [Display(Name = "City Name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only Letters")]
        public string CityName { get; set; } = string.Empty;

        [Required]
        public Guid StateId { get; set; }
    }
}
