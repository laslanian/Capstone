using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models
{
    public class ClientUser
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "Username must be atleast 7 to 25 characters.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "Password must be atleast 7 to 25 characters.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "Password must be atleast 7 to 25 characters.")]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "First Name must be atleast 2 to 25 characters.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Username must be atleast 7 to 25 characters.")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

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
        [Display(Name = "Company Name")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Company name must be atleast 2 to 25 characters.")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Company Address")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Please enter a valid address.")]
        public string CompanyAddress { get; set; }

        [Required]
        [Display(Name = "Company Description")]
        public string CompanyDesc { get; set; }

    }
}