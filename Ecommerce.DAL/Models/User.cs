using Ecommerce.Common.Enum;
using Ecommerce.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("Users", Schema = "dbo")]
    public class User : BaseModel
    {
        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name ="User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [Display(Name ="User Type")]
        public UserType UserType { get; set; }

        [Required]
        public bool CanLogin { get; set; } = true;

        [Display(Name = "Customer")]
        public virtual Guid? CustomerId { get; set; } = null;

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; } = null;

        [Display(Name = "Supplier")]
        public virtual Guid? SupplierId { get; set; } = null;

        [ForeignKey("SupplierId")]
        public virtual Supplier? Supplier { get; set; } = null;
    }
}
