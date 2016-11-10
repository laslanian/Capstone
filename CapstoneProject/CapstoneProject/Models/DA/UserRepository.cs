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
            return ctx.Users.OrderBy(u => u.Type).ToList();
        }

        public IEnumerable<Feedback> GetFeedbacks()
        {
            return ctx.Feedbacks.OrderBy(f => Guid.NewGuid()).Take(5).ToList();
        }

        public User GetUserById(int id)
        {
            return ctx.Users.Find(id);
        }
        public User GetUserByUname(string uname)
        {
            var user = (from u in ctx.Users where u.Username == uname select u).FirstOrDefault();
            return user;
        }
        public User GetUserByEmail(string email)
        {
            var user = (from u in ctx.Users where u.Email == email select u).FirstOrDefault();
            return user;
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

        public bool isExistingEmail(String email)
        {
            return ctx.Users.Any(user => user.Email.Equals(email));
        }

        public void InsertUser(User u)
        {
            ctx.Users.Add(u);
        }

        public int UpdateUser(User u)
        {
            var user = ctx.Users.SingleOrDefault(us => us.UserId == u.UserId);
            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.PhoneNumber = u.PhoneNumber;
            user.Email = u.Email;
            user.Username = u.Username;
            user.Password = u.Password;
            user.Type = u.Type;
            return ctx.SaveChanges();
        }

        public int UpdateStudent(Student s)
        {
            var student = ctx.Users.OfType<Student>().SingleOrDefault(u => u.UserId == s.UserId);
            student.FirstName = s.FirstName;
            student.LastName = s.LastName;
            student.PhoneNumber = s.PhoneNumber;
            student.Email = s.Email;
            student.StudentNumber = s.StudentNumber;
            return ctx.SaveChanges();
        }

        public int UpdateClient(Client  c)
        {
            var client= ctx.Users.OfType<Client>().SingleOrDefault(u => u.UserId == c.UserId);
            client.FirstName = c.FirstName;
            client.LastName = c.LastName;
            client.PhoneNumber = c.PhoneNumber;
            client.Email = c.Email;
            client.CompanyName = c.CompanyName;
            client.CompanyDescription = c.CompanyDescription;
            client.CompanyAddress = c.CompanyAddress;
            return ctx.SaveChanges();
        }


        public void DeleteUser(int id)
        {
            var user = ctx.Users.SingleOrDefault(u => u.UserId == id);
            user.Lock = true;
            ctx.SaveChanges();
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
