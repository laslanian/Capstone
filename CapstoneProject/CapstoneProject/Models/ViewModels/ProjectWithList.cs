using CapstoneProject.Utility;
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
            States.Add(new SelectListItem { Text = ProjectState.All, Value = ProjectState.All });
            States.Add(new SelectListItem { Text = ProjectState.Pending, Value = ProjectState.Pending });
            States.Add(new SelectListItem { Text = ProjectState.Approved, Value = ProjectState.Approved });
            States.Add(new SelectListItem { Text = ProjectState.Rejected, Value = ProjectState.Rejected });
        }
        public string SelectedItem { get; set; }
        public List<Project> Projects { get; set; }
        public List<SelectListItem> States { get; set; }
    }
}