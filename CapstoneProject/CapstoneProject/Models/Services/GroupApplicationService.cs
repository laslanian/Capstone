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

        public GroupApplicationService()
        {
            this._groups = new GroupRepository();
            this._projects = new ProjectRepository();
        }
        public List<Project> GetProjects()
        {
            return _projects.GetProjects().ToList();
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
            int count = g.Projects.Count();
            if (count <= 5)
            {
                _groups.UpdateGroup(g);
                _groups.Save();
                return 1;
            }
            return 0; 
             
        }
        
    }
}