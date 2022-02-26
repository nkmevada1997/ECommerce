using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("Users", Schema = "dbo")]
    public class User
    {
        [Key]
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string UserType { get; set; } = string.Empty;

        [Required]
        public bool CanLogin { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
    }
}
