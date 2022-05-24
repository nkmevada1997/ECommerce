using Ecommerce.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("Suppliers", Schema = "dbo")]
    public class Supplier : ModelBase
    {
        [Required]
        [MaxLength((20))]
        [DataType(DataType.Text)]
        [Display(Name ="Supplier Name")]
        public string SupplierName { get; set; } = string.Empty;

        [Required]
        [MaxLength((50))]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        [Display(Name ="Password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength((50))]
        [DataType(DataType.Text)]
        [Display(Name ="Country")]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength((50))]
        [DataType(DataType.Text)]
        [Display(Name ="State")]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength((50))]
        [DataType(DataType.Text)]
        [Display (Name ="City")]
        public string City { get; set; } = string.Empty;
    }
}
