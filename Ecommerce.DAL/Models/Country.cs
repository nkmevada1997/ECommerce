using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("Countries", Schema = "dbo")]
    public class Country
    {
        [Key]
        [Required]
        public Guid CountryId { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

    }
}
