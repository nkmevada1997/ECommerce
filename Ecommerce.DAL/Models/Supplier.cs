using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("Suppliers", Schema = "dbo")]
    public class Supplier
    {
        [Key]
        [Required]
        public Guid SupplierId { get; set; }

        [Required]
        [MaxLength((20))]
        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength((50))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [MaxLength(15)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength((50))]
        [DataType(DataType.Text)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength((50))]
        [DataType(DataType.Text)]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength((50))]
        [DataType(DataType.Text)]
        public string City { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

    }
}
