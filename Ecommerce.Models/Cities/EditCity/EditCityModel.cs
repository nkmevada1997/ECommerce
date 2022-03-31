using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Cities.EditCity
{
    public class EditCityModel
    {
        public Guid CityId { get; set; }
        [Required]
        [Display(Name = "City Name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only Letters")]
        public string CityName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "State Name")]
        public Guid StateId { get; set; }
    }
}
