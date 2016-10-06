using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.Interfaces;
using CapstoneProject.Models.DA;
using System.Data.Entity.Core.Objects;

namespace CapstoneProject.Models.Services
{

    public class LogInService : IDisposable
    {
        IUserInterface _users;

        public LogInService()
        {
            this._users = new UserRepository();
        }

        public string GetUserType(int id)
        {
            User u = _users.GetUserById(id);
            Type type = ObjectContext.GetObjectType(u.GetType());
            if (type.Equals(typeof(Student)))
            {
                return "Student";
            }
            else if (type.Equals(typeof(Client)))
            {
                return "Client";
            }
            else if (type.Equals(typeof(Coop_Advisor)))
            {
                return "Coop";
            }
            else if (type.Equals(typeof(Management)))
            {
                return "Management";
            }
            else if (type.Equals(typeof(Admin)))
            {
                return "Admin";
            }
            return null;
        }
        
        public void Dispose()
        {
            _users.Dispose();
        }

        public User Login(String username, String password)
        {
            AesEncrpyt en = new AesEncrpyt();

            User u = _users.GetUserByUNPW(en.Encrypt(username), en.Encrypt(password));

            if (u != null)
            {
                u.Username = username;
                return u;
            }
            return null;
        }

    }
}