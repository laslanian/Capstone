using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;
namespace CapstoneProject.Models.Services
{

    public class UserAccountService : IDisposable
    {
        IUserInterface _users;

        public UserAccountService()
        {
            this._users = new UserRepository();
        }

        public User Register(User u)
        {
            AesEncrpyt en = new AesEncrpyt();
            u.Username = en.Encrypt(u.Username);
            u.Password = en.Encrypt(u.Password);

            if (_users.GetUserByUNPW(u.Username, u.Password) == null)
            {
                _users.InsertUser(u);
                _users.Save();
                return u;
            }

            return null;
        }
        public int RegisterStudent(StudentUser stUser)
        {
            Student s = new Student();
            using (ProgramManagerService pms = new ProgramManagerService())
            {
                s.Program = pms.GetProgram(stUser.ProgramId);
            }
            using (UserAccountService uas = new UserAccountService())
            {
                s.FirstName = stUser.FirstName;
                s.LastName = stUser.LastName;
                s.PhoneNumber = stUser.PhoneNumber;
                s.Email = stUser.Email;
                s.Username = stUser.Username;
                s.Password = stUser.Password;
                s.StudentNumber = Convert.ToInt32(stUser.StudentNumber);
                s.Title = "Student";

                AesEncrpyt en = new AesEncrpyt();
                s.Username = en.Encrypt(stUser.Username);
                s.Password = en.Encrypt(stUser.Password);

                //1 - success
                //0 - failure 
                if (!_users.isExistingUsername(s.Username))
                {
                    _users.InsertUser(s);
                    _users.Save();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int RegisterClient(ClientUser client)
        {
            Client s = new Client();

            using (UserAccountService uas = new UserAccountService())
            {
                s.FirstName = client.FirstName;
                s.LastName = client.LastName;
                s.PhoneNumber = client.PhoneNumber;
                s.Email = client.Email;
                s.Username = client.Username;
                s.Password = client.Password;
                s.CompanyName = client.CompanyName;
                s.CompanyAddress = client.CompanyAddress;
                s.CompanyDescription = client.CompanyDesc;

                s.Title = "Client";

                AesEncrpyt en = new AesEncrpyt();
                s.Username = en.Encrypt(client.Username);
                s.Password = en.Encrypt(client.Password);

                if (!_users.isExistingUsername(s.Username))
                {
                    _users.InsertUser(s);
                    _users.Save();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int EditUser(User u)
        {
            if (_users.GetUserById(u.UserId) != null)
            {
                _users.UpdateUser(u);
                _users.Save();
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public User GetUser(int id)
        {
            return _users.GetUserById(id) != null ? _users.GetUserById(id) : null;
        }
        public void DeleteUser(int id)
        {
            if (_users.GetUserById(id) != null)
            {
                _users.DeleteUser(id);
            }
            else
            {

            }

        }

        public void Dispose()
        {
            _users.Dispose();
        }
    }
}