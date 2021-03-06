using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Categories.EditCategory
{
    public  class EditCategoryModel
    {
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name Is Required.")]
        [Display(Name = "Category Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only Letters")]
        [DataType(DataType.Text)]
        [MaxLength((20))]
        public string CategoryName { get; set; } = string.Empty;
    }
}
