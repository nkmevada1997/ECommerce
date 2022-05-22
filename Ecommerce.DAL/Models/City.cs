using Ecommerce.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("Cities", Schema = "dbo")]
    public class City : BaseModel
    {
        [Required]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "City Name")]
        public string CityName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "State")]
        public virtual Guid StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State? State { get; set; }
    }
}
