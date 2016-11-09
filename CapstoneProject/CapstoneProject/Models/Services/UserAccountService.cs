using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;
using CapstoneProject.Utility;
using CapstoneProject.Models.ViewModels;

namespace CapstoneProject.Models.Services
{

    public class UserAccountService : IDisposable
    {
        IUserInterface _users;
        IProgramRepository _programs;
        IStudentRepository _students;

        public UserAccountService()
        {
            CapstoneDBModel ctx = new CapstoneDBModel();
            this._users = new UserRepository(ctx);
            this._programs = new ProgamRepository(ctx);
            this._students = new StudentRepository(ctx);
        }

        public List<User> GetUsers()
        {
            return _users.GetUsers().OrderBy(u => u.LastName).Where(u => u.Lock == false).ToList();
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
            s.FirstName = stUser.FirstName;
            s.LastName = stUser.LastName;
            s.PhoneNumber = stUser.PhoneNumber;
            s.Email = stUser.Email;
            s.Username = stUser.Username;
            s.Password = stUser.Password;
            s.StudentNumber = Convert.ToInt32(stUser.StudentNumber);
            s.ProgramId = stUser.ProgramId;
            s.Type = AccountType.Student;
            s.Lock = false;


            AesEncrpyt en = new AesEncrpyt();
            s.Username = en.Encrypt(stUser.Username);
            s.Password = en.Encrypt(stUser.Password);

            //Admin u = new Admin();
            //u.FirstName = "Super";
            //u.LastName = "Admin";
            //u.PhoneNumber = "9998887777";
            //u.Email = "massivcapstone@outlook.com";
            //u.Username = "superadmin";
            //u.Password = "superadmin";
            //u.Username = en.Encrypt(u.Username);
            //u.Password = en.Encrypt(u.Password);
            //u.Type = AccountType.Admin;
            //u.Lock = false;
            

            //1 - username already exist
            //2 = studentnumber already exist
            //3 = existing email
            //99 - success
            if (!_users.isExistingEmail(s.Email))
            {
                if (!_users.isExistingUsername(s.Username))
                {
                    using (StudentRepository sr = new StudentRepository())
                    {
                        if (!sr.isExistingStudentNumber(s.StudentNumber))
                        {
                            _users.InsertUser(s);
                            //_users.InsertUser(u);
                            _users.Save();
                            return 99;

                        }
                        else
                        {
                            return 2;
                        }
                    }
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 3;
            }
        }
        public int ChangePassword(ChangePassword cp, User u)
        {
            AesEncrpyt en = new AesEncrpyt();

            User user = _users.GetUserByUNPW(u.Username, en.Encrypt(cp.OldPassword));
            if (user!=null)
            {
                user.Password = en.Encrypt(cp.NewPassword);
                _users.UpdateUser(user);
                _users.Save();
                return 1;
            }
            return 0;
        }

        public int AddStudentSkill(Skillset ss, int id)
        {
            if(ss != null)
            {
                Student s = (Student)_users.GetUserById(id);
                s.Skillset = ss;
                _users.UpdateUser(s);
                _users.Save();
                return 1;
            }
            return 0;
        }
        public int RegisterClient(ClientUser client)
        {
            Client c = new Client();

            c.FirstName = client.FirstName;
            c.LastName = client.LastName;
            c.PhoneNumber = client.PhoneNumber;
            c.Email = client.Email;
            c.Username = client.Username;
            c.Password = client.Password;
            c.CompanyName = client.CompanyName;
            c.CompanyAddress = client.CompanyAddress;
            c.CompanyDescription = client.CompanyDesc;
            c.Type = AccountType.Client;



            AesEncrpyt en = new AesEncrpyt();
            c.Username = en.Encrypt(client.Username);
            c.Password = en.Encrypt(client.Password);

            if (!_users.isExistingUsername(c.Username))
            {
                if (!_users.isExistingEmail(c.Email))
                {
                    _users.InsertUser(c);
                    _users.Save();
                    return 99;
                }
                return 2;
            }
            else
            {
                return 1;
            }
        }
        public int UpdateUserPW(User u)
        {
            if (_users.GetUserById(u.UserId) != null)
            {
                AesEncrpyt en = new AesEncrpyt();
                u.Password = en.Encrypt(u.Password);
                return _users.UpdateUser(u);
            }
            else
            {
                return 0;
            }
        }
        public int EditUser(User u)
        {
            if (_users.GetUserById(u.UserId) != null)
            {
                AesEncrpyt en = new AesEncrpyt();
                u.Username = en.Encrypt(u.Username);
                u.Password = en.Encrypt(u.Password);
                return _users.UpdateUser(u);
            }
            else
            {
                return 0;
            }
        }

        public int UpdateStudent(Student s)
        {
            if (_users.GetUserById(s.UserId) != null)
            {
                return _users.UpdateStudent(s);
            }
            else
            {
                return 0;
            }
        }

        public int UpdateClient(Client c)
        {
            if (_users.GetUserById(c.UserId) != null)
            {
                return _users.UpdateClient(c);
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
        public User GetUserByUname(string uname)
        {
            return _users.GetUserByUname(uname);
        }
        public User GetUserByEmail(string email)
        {
            return _users.GetUserByEmail(email);
        }
        public User GetUserByUnPW(string uname, string pw)
        {
            return _users.GetUserByUNPW(uname,pw);
        }
        public Skillset GetSkillsetByUserId(int id)
        {
            return _students.GetSkillByUserId(id);
        }

        public Program GetProgramById(int id)
        {
            return _programs.GetProgram(id);
        }


        public int CreateUser(CreateAccount ac)
        {

            AesEncrpyt en = new AesEncrpyt();
            ac.Username = en.Encrypt(ac.Username);
            ac.Password = en.Encrypt(ac.Password);
            if(_users.isExistingUsername(ac.Username))
            {
                return 4;
            }

            if(_users.isExistingEmail(ac.Email))
            {
                return 5;
            }

            if (ac.SelectedAccount.Equals(AccountType.Coop_Advisor))
            {
                Coop_Advisor ca = new Coop_Advisor();
                ca.FirstName = ac.FirstName;
                ca.LastName = ac.LastName;
                ca.Username = ac.Username;
                ca.Password = ac.Password;
                ca.PhoneNumber = ac.PhoneNumber;
                ca.Email = ac.Email;
                ca.Type = AccountType.Coop_Advisor;
                ca.Lock = false;
                _users.InsertUser(ca);
                _users.Save();
                return 1;
              
            }
            else if(ac.SelectedAccount.Equals(AccountType.Management))
            {
                Management ma = new Management();
                ma.FirstName = ac.FirstName;
                ma.LastName = ac.LastName;
                ma.Username = ac.Username;
                ma.Password = ac.Password;
                ma.PhoneNumber = ac.PhoneNumber;
                ma.Email = ac.Email;
                ma.Type = AccountType.Management;
                ma.Lock = false;
                _users.InsertUser(ma);
                _users.Save();
                return 2;
            }
            else if(ac.SelectedAccount.Equals(AccountType.Admin)) { 
                Admin ad = new Admin();
                ad.FirstName = ac.FirstName;
                ad.LastName = ac.LastName;
                ad.Username = ac.Username;
                ad.Password = ac.Password;
                ad.PhoneNumber = ac.PhoneNumber;
                ad.Email = ac.Email;
                ad.Type = AccountType.Admin;
                ad.Lock = false;
                _users.InsertUser(ad);
                _users.Save();
                return 3;
            }
            else {
                return 0;
            }
        }

        public void Dispose()
        {
            _users.Dispose();
        }
    }
}