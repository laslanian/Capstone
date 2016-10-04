using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;

namespace CapstoneProject.Models.Services
{
    public class GroupApplicationService
    {
        IGroupRepository _groups;
        IProjectRepository _projects;

        public List<Project> GetProjects() { return null; }
        public void AssignProject(Project p) { }
        public void AddProjectPreference(List<Project> projects) { }
    }
}