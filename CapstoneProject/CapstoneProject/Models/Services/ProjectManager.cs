using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;
using CapstoneProject.Models.ViewModels;
using CapstoneProject.Utility;

namespace CapstoneProject.Models.Services
{
    public class ProjectManager : IDisposable
    {
        IProjectRepository _projects;
        IUserInterface _users;

        public ProjectManager()
        {
            CapstoneDBModel ctx = new CapstoneDBModel();
            this._projects = new ProjectRepository(ctx);
            this._users = new UserRepository(ctx);
        }

        public List<Project> GetProjects()
        {
            return _projects.GetProjects().ToList();
        }

        public List<Project> GetPendingProjects()
        {
            return _projects.GetProjecsByState("Pending").ToList();
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
            p.State = ProjectState.Pending;
            p.Client = client;

            Criteria crt = new Criteria();
            crt.Goal = pf.criteria.Goal;
            crt.Storage = pf.criteria.Storage;
            crt.StorageComment = pf.criteria.StorageComment;
            crt.Application = pf.criteria.Application;
            crt.ApplicationComment = pf.criteria.ApplicationComment;
            crt.Website = pf.criteria.Website;
            crt.WebsiteComment = pf.criteria.WebsiteComment;
            crt.Mobile = pf.criteria.Mobile;
            crt.MobileComment = pf.criteria.MobileComment;


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
        public List<Project> GetProjectsByState(string state) {
            return _projects.GetProjecsByState(state).ToList();
        }

        public List<Project> GetArchivedProjects(DateTime date) { return null; }
        public List<Project> GetTopProjects() { return null; }
        public Project GetProjectDetails(int id) {
            return _projects.GetProjectById(id);
        }
        public Project AddProject(Project p) { return null; }
        public int UpdateProject(Project p)
        {
            try
            {
                _projects.UpdateProject(p);
                _projects.Save();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<ProjectTypes> GetProjectTypes()
        {
            return _projects.GetProjectTypes().ToList();
        }

        public void DeleteProject(int id) { }
        public void ArchiveProjects(List<Project> projects) { }

        public void Dispose()
        {
            _projects.Dispose();
            _users.Dispose();
        }
    }
}