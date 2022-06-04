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
        [Display(Name = "Country")]
        public virtual Guid CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        [Required]
        [Display(Name = "State")]
        public virtual Guid StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [Required]
        [Display(Name = "City")]
        public virtual Guid CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }
    }
}
