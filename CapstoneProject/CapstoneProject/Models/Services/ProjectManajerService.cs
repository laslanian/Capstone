using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;

namespace CapstoneProject.Models.Services
{
    public class ProjectManajerService
    {
        IProjectRepository _projects;

        public List<Project> GetProjects() { return null; }
        public List<Project> GetProjects(String state) { return null; }
        public List<Project> GetArchivedProjects(DateTime date) { return null; }
        public List<Project> GetTopProjects() { return null; }
        public Project GetProjectDetails(int id) { return null; }
        public Project AddProject(Project p) { return null; }
        public Project UpdateProject(Project p) { return null; }
        public void DeleteProject(int id) { }
        public void ArchiveProjects(List<Project> projects) { }
    }
}