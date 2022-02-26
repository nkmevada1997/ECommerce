using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("Customers", Schema = "dbo")]
    public class Customer
    {
        [Key]
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(20)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(20)]
        [DataType(DataType.Text)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [MaxLength(15)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }

        [Required]
        [MaxLength((6))]
        [DataType(DataType.Text)]
        public string Gender { get; set; } = string.Empty;

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
