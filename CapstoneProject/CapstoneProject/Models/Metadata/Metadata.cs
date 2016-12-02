using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.Metadata
{
    public class SkillsetMetadata
    {
        [Range(0, 10)]
        [DisplayFormat(DataFormatString = "{0:n1}")]
        [Display(Name = "C# Programming")]
        public double CSharp { get; set; }

        [Range(0, 10)]
        [DisplayFormat(DataFormatString = "{0:n1}")]
        [Display(Name = "Java Programming")]
        public double Java { get; set; }

        [Range(0, 10)]
        [DisplayFormat(DataFormatString = "{0:n1}")]
        [Display(Name = "Database")]
        public double Database { get; set; }

        [Range(0, 10)]
        [DisplayFormat(DataFormatString = "{0:n1}")]
        [Display(Name = "Web Development")]
        public double WebDev { get; set; }
        [Range(0, 10)]
        [DisplayFormat(DataFormatString = "{0:n1}")]
        [Display(Name = "Mobile Development")]
        public double MobileDev { get; set; }
        [Range(0, 10)]
        [DisplayFormat(DataFormatString = "{0:n1}")]
        [Display(Name = "Application Development")]
        public double ApplDev { get; set; }
        [Range(0, 10)]
        [DisplayFormat(DataFormatString = "{0:n1}")]
        [Display(Name = "UI Designing")]
        public double UIDesign { get; set; }
    }

    public class CoopMetadata
    {
        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Company Name must be atleast 2 to 25 characters.")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Position")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Position must be atleast 2 to 25 characters.")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(255, ErrorMessage = "Job Description cannot be more than 255 characters.")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public System.DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public System.DateTime EndDate { get; set; }

        [Display(Name = "Comments")]
        [DataType(DataType.MultilineText)]
        [StringLength(255, ErrorMessage = "Comments cannot be more than 255 characters.")]
        public string Comments { get; set; }
    }

    public class ClientMetadata
    {
        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "First Name must be atleast 2 to 25 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Last Name must be atleast 2 to 25 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(10, ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Company Name must be atleast 2 to 25 characters.")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Company Address cannot be more than 50 characters.")]
        [Display(Name = "Company Address")]
        public string CompanyAddress { get; set; }
        [Display(Name = "Company Description")]
        [StringLength(255, ErrorMessage = "Company Description cannot be more than 255 characters.")]
        [DataType(DataType.MultilineText)]
        public string CompanyDescription { get; set; }
    }

    public class CriteriaMetadata
    {
        [Display(Name = "Goal")]
        [DataType(DataType.MultilineText)]
        public string Goal { get; set; }
        [Display(Name = "Does the application stores information")]
        public Nullable<bool> Storage { get; set; }
        [Display(Name = "Is it an desktop application?")]
        public Nullable<bool> Application { get; set; }
        [Display(Name = "Is it a website?")]
        public Nullable<bool> Website { get; set; }
        [Display(Name = "Is it a mobile application?")]
        public Nullable<bool> Mobile { get; set; }
    }

    public class GroupMetadata
    {
        [Required]
        [Display(Name = "Group Name")]
        [StringLength(25, ErrorMessage = "Group name cannot be more than 25 characters.")]
        public string GroupName { get; set; }
     
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(255, ErrorMessage = "Group description cannot be more than 255 characters.")]
        public string Description { get; set; }
    }

    public class UserMetadata
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(10, ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }

    public class ProjectMetadata
    {

        [Required]
        [Display(Name = "Project Title")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Title must be atleast 5 to 25 characters.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(255,  ErrorMessage = "Description cannot be longer than 255 characters.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }

    public class StudentMetadata
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "First Name must be atleast 2 to 25 characters.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "First Name must be atleast 2 to 25 characters.")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Student Number")]
        [RegularExpression(@"^(\d{9})$", ErrorMessage = "Enter a valid student number")]
        public int StudentNumber { get; set; }
    }

    public class ProgramMetadata
    {
        [Display(Name = "Program")]
        public string ProgramName { get; set; }
    }

    public class FeedbackMetadata
    {
        [Required]
        [Display(Name = "Feedback")]
        [StringLength(255, ErrorMessage = "Feedback cannot be longer than 255 characters.")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Range(0, 5)]
        [DisplayFormat(DataFormatString = "{0:n1}")]
        [Display(Name = "Rating")]
        public double Rating { get; set; }
    }

}