using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Models.Interfaces
{
    interface IProjectRepository : IDisposable
    {
        IEnumerable<Project> GetProjects();
        IEnumerable<Project> GetProjecsByState(string state);
        IEnumerable<Project> GetProjectByYear(DateTime year);
        IEnumerable<Project> GetProjectByClient(Client c);
        IEnumerable<Project> GetTopProjects();
        Project GetProjectById(int id);
        void InsertProject(Project p);
        void UpdateProject(Project p);
        void DeleteProject(int id);
        void Save();
    }
}
