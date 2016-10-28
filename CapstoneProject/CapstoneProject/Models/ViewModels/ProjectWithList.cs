using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Models.ViewModels
{
    public class ProjectWithList
    {
        public ProjectWithList()
        {
            Projects = new List<Project>();
            States = new List<SelectListItem>();
            States.Add(new SelectListItem { Text = "All", Value = "All" });
            States.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            States.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
        }
        public string SelectedItem { get; set; }
        public List<Project> Projects { get; set; }
        public List<SelectListItem> States { get; set; }
    }
}