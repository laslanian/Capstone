﻿using System;
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
        [Display(Name = "Programming")]
        public double Programming { get; set; }
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
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Position")]
        public string JobTitle { get; set; }
        [Required]
        [Display(Name = "Job Description")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public System.DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public System.DateTime EndDate { get; set; }
        [Display(Name = "Comments")]
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }

    public class ClientMetadata
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Company Address")]
        public string CompanyAddress { get; set; }
        [Display(Name = "Company Description")]
        [DataType(DataType.MultilineText)]
        public string CompanyDescription { get; set; }
    }

    public class CriteriaMetadata
    {
        [Display(Name = "Goal")]
        [DataType(DataType.MultilineText)]
        public string Goal { get; set; }
        [Display(Name = "Do you need to store infomration?")]
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
        public string GroupName { get; set; }
        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
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
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Title must be atleast 5 to 50 characters.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }

    public class StudentMetadata
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Student Number")]
        public int StudentNumber { get; set; }
    }

    public class ProgramMetadata
    {
        [Display(Name = "Program")]
        public string ProgramName { get; set; }
    }
}