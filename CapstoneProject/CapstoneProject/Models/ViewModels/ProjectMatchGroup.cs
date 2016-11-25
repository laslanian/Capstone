using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.ViewModels
{
    public class ProjectMatchGroup
    {
        public List<Group> Groups { get; set; }
        public List<ProjectRanking> Projects { get; set; }

        public ProjectMatchGroup()
        {
            Groups = new List<Group>();
            Projects = new List<ProjectRanking>();
        }
    }
}