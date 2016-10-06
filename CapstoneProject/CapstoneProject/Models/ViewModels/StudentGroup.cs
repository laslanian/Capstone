using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models
{
    public class StudentGroup
    {
        public StudentGroup()
        {
            StudentList = new List<Student>();
        }
      
        public string GroupId { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Group Name must be 2 to 25 characters")]
        public string GroupName { get; set; }

        [Required]
        [Display(Name = "Group Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Pin must be atleast 4 to 8 characters.")]
        [Display(Name = "Pin")]
        public string Pin { get; set; }

        [Required]
        [Display(Name = "Owner")]
        public string Owner { get; set; }

        [Display(Name = "Student List")]
        public List<Student> StudentList { get; set; }

     
    }
}