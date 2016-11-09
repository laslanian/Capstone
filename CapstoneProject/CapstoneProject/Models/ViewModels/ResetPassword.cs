using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class ResetPassword
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "Username must be atleast 7 to 15 characters.")]
        [RegularExpression(@"^([a-zA-Z0-9]+)$", ErrorMessage = "Alphanumeric only")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Confirm Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Please enter a valid email address.")]
        [Compare("Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "Password must be atleast 7 to 25 characters.")]
        [RegularExpression(@"^([a-zA-Z0-9]+)$", ErrorMessage = "Alphanumeric only")]
        [Display(Name = "Temporary Password")]
        public string TempPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "Password must be atleast 7 to 25 characters.")]
        [RegularExpression(@"^([a-zA-Z0-9]+)$", ErrorMessage = "Alphanumeric only")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "Password must be atleast 7 to 25 characters.")]
        [Compare("NewPassword")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}