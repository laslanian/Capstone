using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using CapstoneProject.Models.Interfaces;
using CapstoneProject.Models.DA;
using System.Data.Entity.Core.Objects;
using CapstoneProject.Utility;

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

            Type type =ObjectContext.GetObjectType(u.GetType());

            if (type.Equals(typeof(Student)))
            {
                return AccountType.Student;
            }
            else if (type.Equals(typeof(Client)))
            {
                return AccountType.Client;
            }
            else if (type.Equals(typeof(Coop_Advisor)))
            {
                return AccountType.Coop_Advisor;
            }
            else if (type.Equals(typeof(Management)))
            {
                return AccountType.Management;
            }
            else if (type.Equals(typeof(Admin)))
            {
                return  AccountType.Admin;
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
                u.Username = username;//en.Decrypt(u.Username);
                return u;
            }

            return null;
        }

    }
}