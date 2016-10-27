using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;
using CapstoneProject.Models.ViewModels;

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

        public int CreateProject(ProjectForm pf, int id)
        {
            Client client = (Client)_users.GetUserById(id);
            Project p = new Project();
            p.Name = pf.project.Name;
            p.Description = pf.project.Description;
            p.Type = "Unknown";
            p.State = "Pending Approval";
            p.Client = client;

            Criteria crt = new Criteria();
            crt.Goal = pf.criteria.Goal;
            crt.Storage = pf.criteria.Storage;
            crt.Application = pf.criteria.Application;
            crt.Website = pf.criteria.Website;
            crt.Mobile = pf.criteria.Mobile;

            p.Criteria = crt;
            try
            {
                _projects.InsertProject(p);
                _projects.Save();
                return 99;
            }
            catch
            {
                return 0;
            }
        }
        public List<Project> GetProjects(String state) { return null; }
        public List<Project> GetArchivedProjects(DateTime date) { return null; }
        public List<Project> GetTopProjects() { return null; }
        public Project GetProjectDetails(int id) {
            return _projects.GetProjectById(id);
        }
        public Project AddProject(Project p) { return null; }
        public Project UpdateProject(Project p) { return null; }
        public void DeleteProject(int id) { }
        public void ArchiveProjects(List<Project> projects) { }
    }
}