using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;

namespace CapstoneProject.Models.Services
{
    public class ProjectManager
    {
        IProjectRepository _projects;
        IUserInterface _users;

        public ProjectManager()
        {
            CapstoneDBModel ctx = new CapstoneDBModel();
            this._projects = new ProjectRepository(ctx);
            this._users = new UserRepository(ctx);
        }

        public List<Project> GetProjectsByClient(int id)
        {
            Client c = (Client) _users.GetUserById(id);
            return _projects.GetProjectByClient(c).ToList(); ;
        }
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