using Ecommerce.Common.Enum;
using Ecommerce.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.Models
{
    [Table("Customers", Schema = "dbo")]
    public class Customer : BaseModel
    {
        [Required]
        [MaxLength(20)]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(20)]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "DOB")]
        public DateTime DOB { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Required]
        [MaxLength((50))]
        [DataType(DataType.Text)]
        [Display(Name = "Country")]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength((50))]
        [DataType(DataType.Text)]
        [Display(Name = "State")]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength((50))]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;
    }
}
