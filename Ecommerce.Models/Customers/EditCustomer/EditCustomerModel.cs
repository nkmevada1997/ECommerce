using Ecommerce.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Customers.EditCustomer
{
    public class EditCustomerModel
    {
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "First Name Is Required.")]
        [MaxLength(20)]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Only Letters")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Only Letters")]
        [DataType(DataType.Text)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Gender Is Required.")]
        public Gender Gender { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country Is Required.")]
        public string CountryId { get; set; } = string.Empty;

        [Display(Name = "State")]
        [Required(ErrorMessage = "State Is Required.")]
        public string StateId { get; set; } = string.Empty;

        [Display(Name = "City")]
        [Required(ErrorMessage = "City Is Required.")]
        public string CityId { get; set; } = string.Empty;
    }
}
