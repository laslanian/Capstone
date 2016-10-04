using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.Interfaces
{
    public class ProgamRepository : IProgramRepository, IDisposable
    {
        private CapstoneDBModel ctx;
        private bool disposed = false;

        public ProgamRepository()
        {
            this.ctx = new CapstoneDBModel();
        }

        public ProgamRepository(CapstoneDBModel context)
        {
            this.ctx = context;
        }

        public IEnumerable<Program> GetPrograms()
        {
            return ctx.Programs.ToList();
        }

        public Program GetProgram(int id)
        {
            return ctx.Programs.Find(id);
        }

        public void InsertProgram(Program p)
        {
            ctx.Programs.Add(p);
        }

        public void UpdateProgram(Program p)
        {
            ctx.Entry(p).State = System.Data.Entity.EntityState.Modified;
        }

        public void DeleteProgram(int id)
        {
            Program p = ctx.Programs.Find(id);
            ctx.Programs.Remove(p);
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