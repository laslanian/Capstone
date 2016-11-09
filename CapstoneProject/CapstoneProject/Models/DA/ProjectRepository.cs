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

        public int UpdateProject(Project p)
        {
            var project = ctx.Projects.SingleOrDefault(pr => pr.ProjectId == p.ProjectId);
            project.Name = p.Name;
            project.Description = p.Description;
            project.Criteria.Goal = p.Criteria.Goal;
            project.Criteria.Storage = p.Criteria.Storage;
            project.Criteria.Application = p.Criteria.Application;
            project.Criteria.Website = p.Criteria.Website;
            project.Criteria.Mobile = p.Criteria.Mobile;
            project.Criteria.StorageComment = p.Criteria.StorageComment;
            project.Criteria.ApplicationComment = p.Criteria.ApplicationComment;
            project.Criteria.WebsiteComment = p.Criteria.WebsiteComment;
            project.Criteria.MobileComment = p.Criteria.MobileComment;
            return ctx.SaveChanges();
        }


        public void DeleteProject(int id)
        {
            Project p = ctx.Projects.Find(id);
            ctx.Projects.Remove(p);
        }

        public void Save()
        {
            try
            {
                ctx.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
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