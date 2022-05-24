using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Common.Models
{
    public class ModelBase
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
    }
}
