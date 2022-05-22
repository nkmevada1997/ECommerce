using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Common.Models
{
    public class BaseModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
    }
}
