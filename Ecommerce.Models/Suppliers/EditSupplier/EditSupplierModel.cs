using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Suppliers.EditSupplier
{
    public class EditSupplierModel
    {
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "Supplier Name Is Required.")]
        [Display(Name = "Supplier Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only Letters")]
        [DataType(DataType.Text)]
        [MaxLength((20))]
        public string Name { get; set; } = string.Empty;

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
