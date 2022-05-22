using Ecommerce.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("Categories", Schema = "dbo")]
    public class Category : BaseModel
    {
        [Required]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
