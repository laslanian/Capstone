using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject.Models.Interfaces;
using CapstoneProject;

namespace CapstoneProject.Models.DA
{
    public class GroupRepository : IGroupRepository
    {
        private CapstoneDBModel ctx;
        private bool disposed = false;

        public GroupRepository()
        {
            this.ctx = new CapstoneDBModel();
        }

        public GroupRepository(CapstoneDBModel context)
        {
            this.ctx = context;
        }

        public Group GetGroupyId(int id)
        {
            return ctx.Groups.Find(id);
        }


        public IEnumerable<Group> GetGroups()
        {
            return ctx.Groups.ToList();
        }

        public void InsertGroup(Group g)
        {
            ctx.Groups.Add(g);
        }

        public void UpdateGroup(Group g)
        {
            ctx.Entry(g).State = System.Data.Entity.EntityState.Modified;
        }

        public void DeleteGroup(int id)
        {
            Group g = ctx.Groups.Find(id);
            ctx.Groups.Remove(g);
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
