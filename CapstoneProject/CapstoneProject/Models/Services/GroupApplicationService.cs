using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;

namespace CapstoneProject.Models.Services
{
    public class GroupApplicationService : IDisposable
    {
        IGroupRepository _groups;
        IProjectRepository _projects;

        public GroupApplicationService()
        {
            CapstoneDBModel ctx = new CapstoneDBModel();
            this._groups = new GroupRepository(ctx);
            this._projects = new ProjectRepository(ctx);
        }
        public List<Project> GetProjects()
        {
            return _projects.GetProjects().ToList();
        }
        public Project GetProjectById(int id)
        {
            return _projects.GetProjectById(id);
        }
        public int AssignProject(Group g, Project p)
        {
            if (_projects.GetProjectById(p.ProjectId) != null)
            {
                if (_groups.GetGroupyId(g.GroupId) != null && !g.Projects.Contains(p))
                {
                    g.Projects.Add(p);
                    _groups.UpdateGroup(g);
                    _groups.Save();
                    return 1;
                }
            }
            return 0;
        }
        public int AddProjectPreference(Group g)
        {
            if (g.Projects.Count() <= 5)
            {
                _groups.UpdateGroup(g);
                _groups.Save();
                return 1;
            }
            return 0;          
            
        }

        public void Dispose()
        {
            _groups.Dispose();
            _projects.Dispose();
        }
    }
}