﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;
using System.Security.Cryptography;
using System.Text;
using CapstoneProject.Utility;

namespace CapstoneProject.Models.Services
{
    public class GroupBuilderService : IDisposable
    {
        IGroupRepository _groups;
        IStudentRepository _students;
        IProjectRepository _projects;
        IUserInterface _users;

        public GroupBuilderService()
        {
            CapstoneDBModel ctx = new CapstoneDBModel();
            this._groups = new GroupRepository(ctx);
            this._students = new StudentRepository(ctx);
            this._projects = new ProjectRepository(ctx);
            this._users = new UserRepository(ctx);
        }
        public List<Group> GetGroups()
        {
            return _groups.GetGroups().ToList();
        }
        public Group GetGroupById(int id)
        {
            return _groups.GetGroupById(id) != null ? _groups.GetGroupById(id) : null;
        }

        public Group AddGroup(Group g, int id)
        {
            if (!_groups.isExistingGroup(g.GroupName))
            {
                String pin = GeneratePin();
                g.Pin = pin;
                g.Status = GroupState.Unassigned;
                Student s = (Student)_users.GetUserById(id);
                g.Students.Add(s);
                g.Skillset = new Skillset();
                g.Owner = id;
                g = UpdateSkill(g);
                _groups.InsertGroup(g);
                _groups.Save();

                EmailService emailService = new EmailService();
                emailService.SendGroupPin(s.Email, g);

                return g;
            }
            return null;
        }

        public Group EditGroup(Group g)
        {
            if (_groups.GetGroupById(g.GroupId) != null)
            {
                _groups.UpdateGroup(g);
                _groups.Save();
                return g;
            }
            return null;
        }

        public GroupStudent GetGroupStudentVM(int id)
        {
            Group g = GetGroupById(id);
            GroupStudent gs = new GroupStudent();
            List<Student> students = g.Students.ToList();
            foreach (Student s in students)
            {
                gs.Students.Add(s);
            }
            return gs;
        }

        public GroupStudent EditGroupStudentVM(GroupStudent gs)
        {
            Group g = GetGroupById(gs.Group.GroupId);
            g.GroupName = gs.Group.GroupName;
            g.Description = gs.Group.Description;

            _groups.UpdateGroup(GetGroupById(gs.Group.GroupId));
            _groups.Save();

            return gs;
        }

        public int AddStudent(int GroupId, int UserId, string pin)
        {
            Group g = _groups.GetGroupById(GroupId);
            if (g != null)
            {
                Student s = (Student)_users.GetUserById(UserId);
                if (g.Pin == pin)
                {
                    g.Students.Add(s);
                    g = UpdateSkill(g);
                    _groups.UpdateGroup(g);
                    _groups.Save();
                    return 99;
                }
                else
                {
                    return 1; // wrong pin
                }


            }
            return 0;
        }

        public int RemoveStudent(int GroupId, int UserId)
        {
            Group g = _groups.GetGroupById(GroupId);

            if (g != null)
            {
                Student s = (Student)_users.GetUserById(UserId);
                g.Students.Remove(s);
                g = UpdateSkill(g);
                if (g.Students.Count == 0)
                {
                    _groups.DeleteGroupSkillset(g.Skillset.Id);
                    _groups.DeleteGroup(g.GroupId);
                }
                else
                {
                    _groups.UpdateGroup(g);
                }
                _groups.Save();
                return 99;
            }
            return 0;
        }

        public String GeneratePin()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5).Trim();
        }

        public void Dispose()
        {
            _groups.Dispose();
            _students.Dispose();
            _projects.Dispose();
            _users.Dispose();
        }

        public StudentGroup GetStudentGroup(int id)
        {
            StudentGroup sg = new StudentGroup();
            Student s = (Student)_users.GetUserById(id);
            List<Group> groups = _groups.GetGroups().ToList();
            if (s.Group == null)
            {
                sg.hasGroup = false;
            }
            else
            {
                foreach (Group g in groups)
                {
                    if (g.Owner == id)
                    {
                        sg.isOwner = true;
                    }
                }
                sg.hasGroup = true;
            }
            sg.Student = s;
            sg.Groups = groups;
            return sg;
        }

        public Skillset GetSkillsetByGroupId(int id)
        {
            return _groups.GetSkillByGroupId(id);
        }

        public Group UpdateSkill(Group g)
        {
            g.Skillset.CSharp = 0;
            g.Skillset.Java = 0;
            g.Skillset.Database = 0;
            g.Skillset.WebDev = 0;
            g.Skillset.MobileDev = 0;
            g.Skillset.ApplDev = 0;
            g.Skillset.UIDesign = 0;

            foreach (Student s in g.Students)
            {
                g.Skillset.CSharp += s.Skillset.CSharp;
                g.Skillset.Java += s.Skillset.Java;
                g.Skillset.Database += s.Skillset.Database;
                g.Skillset.WebDev += s.Skillset.WebDev;
                g.Skillset.MobileDev += s.Skillset.MobileDev;
                g.Skillset.ApplDev += s.Skillset.ApplDev;
                g.Skillset.UIDesign += s.Skillset.UIDesign;
            }

            int count = g.Students.Count;

            g.Skillset.CSharp = g.Skillset.CSharp / count;
            g.Skillset.Java = g.Skillset.Java / count;
            g.Skillset.Database = g.Skillset.Database / count;
            g.Skillset.WebDev = g.Skillset.WebDev / count;
            g.Skillset.MobileDev = g.Skillset.MobileDev / count;
            g.Skillset.ApplDev = g.Skillset.ApplDev / count;
            g.Skillset.UIDesign = g.Skillset.UIDesign / count;

            return g;
        }

        public int AddProjectPreference(Group g)
        {
            int count = g.Projects.Count();
            if (count > 5)
            {
                return 99;
            }
            else if (count < 3)
            {
                return 88;
            }
            else
            {
                _groups.UpdateGroup(g);
                _groups.Save();
                return 1;
            }
         

        }
        public List<Project> GetProjects()
        {
            return _projects.GetProjects().ToList();
        }
        public Project GetProjectById(int id)
        {
            return _projects.GetProjectById(id);
        }

    }
}