using Ecommerce.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("States", Schema = "dbo")]
    public class State : ModelBase
    {
        [Required]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "State")]
        public string StateName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Country")]
        public virtual Guid CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; } 
    }
}
