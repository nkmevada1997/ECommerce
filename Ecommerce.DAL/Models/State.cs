using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("States", Schema = "dbo")]
    public class State
    {
        [Key]
        [Required]
        public Guid StateId { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        [Display(Name="Country")]
        public virtual Guid CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Countries { get; set; } = new Country();
    }
}
