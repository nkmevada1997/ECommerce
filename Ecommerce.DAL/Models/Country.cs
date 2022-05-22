using Ecommerce.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("Countries", Schema = "dbo")]
    public class Country : BaseModel
    {
        [Required]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; } = string.Empty;
    }
}
