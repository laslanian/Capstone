using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject;
using CapstoneProject.Models.Interfaces;

namespace CapstoneProject.Models.DA
{
    public class UserRepository : IUserInterface, IDisposable
    {
        private CapstoneDBModel ctx;
        private bool disposed = false;

        public UserRepository()
        {
            this.ctx = new CapstoneDBModel();
        }

        public UserRepository(CapstoneDBModel context)
        {
            this.ctx = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return ctx.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return ctx.Users.Find(id);
        }

        public User GetUserByUNPW(string username, string password)
        {
            var user = (from u in ctx.Users where u.Username == username && u.Password == password select u).FirstOrDefault();
            return user;
        }

        public bool isExistingUsername(String username)
        {
            return ctx.Users.Any(user => user.Username.Equals(username));
        }

        public void InsertUser(User u)
        {
            AesEncrpyt en = new AesEncrpyt();
            u.Username = en.Encrypt(u.Username);
            u.Password = en.Encrypt(u.Password);
            ctx.Users.Add(u);
        }

        public void UpdateUser(User u)
        {
            ctx.Entry(u).State = System.Data.Entity.EntityState.Modified;
        }

        public void DeleteUser(int id)
        {
            User u = ctx.Users.Find(id);
            ctx.Users.Remove(u);
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
