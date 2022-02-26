using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("Cities", Schema = "dbo")]
    public class City
    {
        [Key]
        [Required]
        public Guid CityId { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        [Display(Name = "State")]
        public virtual Guid StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State States { get; set; } = new State();
    }
}
