using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.Interfaces;
using CapstoneProject.Models.DA;

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
            if (u.GetType() == typeof(Student))
            {
                return "Student";
            }
            else if (u.GetType() == typeof(Client))
            {
                return "Client";
            }
            else if (u.GetType() == typeof(Coop_Advisor))
            {
                return "Coop";
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
                u.Username = en.Decrypt(u.Username);
                return u;
            }

            return null;
        }

    }
}