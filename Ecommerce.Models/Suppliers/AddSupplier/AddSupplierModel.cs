using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Suppliers.AddSupplier
{
    public class AddSupplierModel
    {
        [Required(ErrorMessage = "Supplier Name Is Required.")]
        [Display(Name = "Supplier Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only Letters")]
        [DataType(DataType.Text)]
        [MaxLength((20))]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address Is Required.")]
        [RegularExpression(@"^([a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4})+$", ErrorMessage = "Please enter correct email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength((50))]
        public string Email { get; set; } = string.Empty;

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
