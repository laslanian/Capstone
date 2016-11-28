using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.ViewModels
{
    public class ProjectMatchGroup
    {
        public List<Group> Groups { get; set; }
        public List<GroupProject> GroupProjects { get; set; }
        public List<Project> Projects { get; set; }

        public ProjectMatchGroup()
        {
            GroupProjects = new List<GroupProject>();
            Groups = new List<Group>();
            Projects = new List<Project>();
        }
    }
}