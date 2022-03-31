using Ecommerce.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Customers.AddCustomer
{
    public class AddCustomerModel
    {
        [Required(ErrorMessage ="Profile Picture Is Required")]
        public string AvatarImage { get; set; } = string.Empty;

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

        [Required]
        [DataType(DataType.PhoneNumber)]
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

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Gender Is Required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Country Is Required.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Only Letters")]
        [MaxLength((50))]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "State Is Required.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Only Letters")]
        [MaxLength((50))]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "City Is Required.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Only Letters")]
        [MaxLength((50))]
        public string City { get; set; } = string.Empty;
    }
}
