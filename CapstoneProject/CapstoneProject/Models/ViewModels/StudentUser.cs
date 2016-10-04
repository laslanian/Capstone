using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class StudentUser
    {

        public StudentUser()
        {
            ProgList = new List<Program>();
        }

        [Required]
        [Display(Name = "Student Number")]
        [RegularExpression(@"^(\d{9})$", ErrorMessage = "Enter a valid student number")]
        public string StudentNumber { get; set; }

        [Required]
        [Display(Name = "Username")]
        //  [StringLength(15, MinimumLength = 7, ErrorMessage = "Username must be atleast 7 to 15 characters.")]
        [RegularExpression(@"^([a-zA-Z0-9]+)$", ErrorMessage = "Alphanumeric only")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //  [StringLength(25, MinimumLength = 7, ErrorMessage = "Password must be atleast 7 to 25 characters.")]
        [RegularExpression(@"^([a-zA-Z0-9]+)$", ErrorMessage = "Alphanumeric only")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //   [StringLength(25, MinimumLength = 7, ErrorMessage = "Password must be atleast 7 to 25 characters.")]
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
        [Display(Name = "Program")]
        public int ProgramId { get; set; }

        public List<Program> ProgList { get; set; }

        [Required]
        [Display(Name = "Campus")]
        public Utility.Campuses Campus { get; set; }
    }
}