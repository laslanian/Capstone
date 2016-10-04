using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class LoginUser
    {
        [Required]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "Username must be atleast 7 to 25 characters.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "Password must be atleast 7 to 25 characters.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}