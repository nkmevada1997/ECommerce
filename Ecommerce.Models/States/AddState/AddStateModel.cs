﻿using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.States.AddState
{
    public class AddStateModel
    {
        [Required]
        [Display(Name = "State Name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only Letters")]
        public string StateName { get; set; } = string.Empty;

        [Required]
        public Guid CountryId { get; set; }
    }
}
