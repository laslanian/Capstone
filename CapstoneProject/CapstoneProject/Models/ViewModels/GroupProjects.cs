using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.ViewModels
{
    public class GroupProjects
    {
        public GroupProjects()
        {
            RankedProjects = new List<Project>();
        }

        public Group Group { get; set; }
        public List<Project> RankedProjects { get; set; }
    }
}