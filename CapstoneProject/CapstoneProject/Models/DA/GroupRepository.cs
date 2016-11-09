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

        public Group GetGroupById(int id)
        {
            return ctx.Groups.Find(id);
        }

        public Skillset GetSkillByGroupId(int id)
        {
            var skill = ctx.Skillsets.SingleOrDefault(s => s.Group.GroupId == id);
            return skill;
        }

        public bool isExistingGroup(string group_name)
        {
            return ctx.Groups.Any(group => group.GroupName == group_name);
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
