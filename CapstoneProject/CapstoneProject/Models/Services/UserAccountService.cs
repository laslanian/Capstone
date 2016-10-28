﻿using System;
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
        IProgramRepository _programs;
        IStudentRepository _students;

        public UserAccountService()
        {
            CapstoneDBModel ctx = new CapstoneDBModel();
            this._users = new UserRepository(ctx);
            this._programs = new ProgamRepository(ctx);
            this._students = new StudentRepository(ctx);
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
            System.Diagnostics.Debug.WriteLine("Student Numer: " + s.StudentNumber + " - - - - - - - -- - ");
            s.Type = "Student";


            AesEncrpyt en = new AesEncrpyt();
            s.Username = en.Encrypt(stUser.Username);
            s.Password = en.Encrypt(stUser.Password);

            //Admin admin = new Admin();
            //admin.FirstName = "Super Admin";
            //admin.LastName = "Super Admin";
            //admin.PhoneNumber = "9991119999";
            //admin.Email = "massivcapstone@outlook.com";
            //admin.Username = "superadmin";
            //admin.Username = en.Encrypt(admin.Username);
            //admin.Password = "superadmin";
            //admin.Password = en.Encrypt(admin.Password);
            //admin.Type = "Admin";

            //1 - username already exist
            //2 = studentnuber already exist
            //99 - success
            if (!_users.isExistingUsername(s.Username))
            {
                using(StudentRepository sr = new StudentRepository()) {
                    if(!sr.isExistingStudentNumber(s.StudentNumber))
                    {
                        _users.InsertUser(s);
                        //_users.InsertUser(admin);
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
            Client s = new Client();

            s.FirstName = client.FirstName;
            s.LastName = client.LastName;
            s.PhoneNumber = client.PhoneNumber;
            s.Email = client.Email;
            s.Username = client.Username;
            s.Password = client.Password;
            s.CompanyName = client.CompanyName;
            s.CompanyAddress = client.CompanyAddress;
            s.CompanyDescription = client.CompanyDesc;
            s.Type = "Client";


            AesEncrpyt en = new AesEncrpyt();
            s.Username = en.Encrypt(client.Username);
            s.Password = en.Encrypt(client.Password);

            if (!_users.isExistingUsername(s.Username))
            {
                _users.InsertUser(s);
                _users.Save();
                return 99;
            }
            else
            {
                return 1;
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

        public Skillset GetSkillsetByUserId(int id)
        {
            return _students.GetSkillByUserId(id);
        }

        public Program GetProgramById(int id)
        {
            return _programs.GetProgram(id);
        }

        public void Dispose()
        {
            _users.Dispose();
        }

        public void CreateAdmin()
        {

        }
    }
}