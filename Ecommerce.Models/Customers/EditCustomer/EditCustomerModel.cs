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
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Only Letters")]
        [MaxLength((6))]
        public string Gender { get; set; } = string.Empty;

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
