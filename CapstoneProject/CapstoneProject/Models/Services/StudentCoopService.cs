﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;

namespace CapstoneProject.Models.Services
{
    public class StudentCoopService : IDisposable
    {
        IStudentRepository _students;
        IUserInterface _users;

        public StudentCoopService()
        {
            CapstoneDBModel ctx = new CapstoneDBModel();
            _users = new UserRepository(ctx);
            _students = new StudentRepository(ctx);
        }

        public List<Student> GetStudents()
        {
            return _students.GetStudents().OrderBy(s => s.LastName).ToList();
        }

        public void AddCoop(int id, Coop c) {
            Student s = (Student)_users.GetUserById(id);
            s.Coops.Add(c);
            _users.UpdateUser(s);
            _users.Save();
        }

        public Coop GetCoopById(int id) {
            Coop c = _students.GetCoopById(id);
            return c;
        }
        public int UpdateCoop(Coop c) {
            return _students.UpdateCoop(c);
        }
        public int DeleteCoop(int id) {
            return _students.DeleteCoop(id);
        }

        public void Dispose()
        {
            _users.Dispose();
            _students.Dispose();
        }
    }
}