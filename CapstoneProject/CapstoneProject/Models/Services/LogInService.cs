using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.Interfaces;
using CapstoneProject.Models.DA;

namespace CapstoneProject.Models.Services
{

    public class LogInService
    {
        IUserInterface _users;

        public LogInService()
        {
            this._users = new UserRepository();
        }

        public User Login(String username, String password)
        {
            AesEncrpyt en = new AesEncrpyt();

            User u = _users.GetUserByUNPW(en.Encrypt(username), en.Encrypt(password));

            if (u != null)
            {
                return u;
            }

            return null;
        }

    }
}