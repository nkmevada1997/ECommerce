using Ecommerce.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Customers.AddCustomer
{
    public class AddCustomerModel
    {
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

        [Required(ErrorMessage = "Email Address Is Required.")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile Number Is Required.")]
        [MaxLength(20)]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^([0-9]{10})+$", ErrorMessage = "Please Enter Only Numbers")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Is Required.")]
        [MinLength(8, ErrorMessage = "Password Length Must Be 8 to 15 Characters.")]
        [MaxLength(15, ErrorMessage = "Password Length Must Be 8 to 15 Characters.")]
        [RegularExpression(@"(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*#?&^_-]{8,15}$", ErrorMessage = "Invalid Password Pattern")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password Is Required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password Must Be Same.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "DOB Is Required.")]
        public string DOB { get; set; } = string.Empty;

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
