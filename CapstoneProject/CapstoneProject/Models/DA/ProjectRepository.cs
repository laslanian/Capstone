using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapstoneProject.Models.Interfaces;

namespace CapstoneProject.Models.DA
{
    public class ProjectRepository : IProjectRepository, IDisposable
    {
        private CapstoneDBModel ctx;
        private bool disposed = false;

        public ProjectRepository()
        {
            this.ctx = new CapstoneDBModel();
        }

        public ProjectRepository(CapstoneDBModel context)
        {
            this.ctx = context;
        }

        public IEnumerable<Project> GetProjects()
        {
            return ctx.Projects.ToList();
        }

        public IEnumerable<Project> GetProjecsByState(string state)
        {
            return ctx.Projects.ToList().Where(project => project.State == state);
        }

        public IEnumerable<Project> GetProjectByYear(DateTime year)
        {
            return ctx.Projects.ToList().Where(project => project.DateCompleted == year);
        }

        public IEnumerable<Project> GetProjectByClient(Client c)
        {
            return ctx.Projects.ToList().Where(project => project.Client == c);
        }

        public IEnumerable<Project> GetTopProjects()
        {
            return ctx.Projects.OrderBy(project => project.Grade).ToList().Take(5).ToList();
        }

        public Project GetProjectById(int id)
        {
            return ctx.Projects.Find(id);
        }

        public void InsertProject(Project p)
        {
            ctx.Projects.Add(p);
        }

        public void UpdateProject(Project p)
        {
            ctx.Entry(p).State = System.Data.Entity.EntityState.Modified;
        }


        public void DeleteProject(int id)
        {
            Project p = ctx.Projects.Find(id);
            ctx.Projects.Remove(p);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
    }
}