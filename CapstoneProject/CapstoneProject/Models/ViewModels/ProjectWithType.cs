using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.ViewModels
{
    public class ProjectWithType
    {
        public ProjectWithType()
        {
            ProjTypes = new List<ProjectTypes>();
        }

        public Project Project { get; set; }
        [Required]
        [Display(Name = "Type")]

        public List<ProjectTypes> ProjTypes { get; set; }
    }
}