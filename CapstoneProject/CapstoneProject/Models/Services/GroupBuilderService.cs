using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace CapstoneProject.Models.Services
{
    public class GroupBuilderService
    {
        IGroupRepository _groups;
        IStudentRepository _students;
        IProjectRepository _projects;
        IUserInterface _users;

        public GroupBuilderService()
        {
            this._groups = new GroupRepository();
            this._students = new StudentRepository();
            this._projects = new ProjectRepository();
            this._users = new UserRepository();
        }

        public Group AddGroup(Group g, int studentNumber)
        {
            if (!_groups.isExistingGroup(g.GroupName))
            {
                try
                {
                    AesEncrpyt ae = new AesEncrpyt();
                    g.Pin = ae.Encrypt(GeneratePin());
                    _groups.InsertGroup(g);
                    _groups.Save();
                    //send email
                    EmailService emailService = new EmailService();
                    User user = _users.GetUserById(studentNumber);
                    emailService.SendGroupPin(user.Email, g.Pin);

                    return g;
                }
                catch (Exception e)
                {
                    return null;
                }
                
            }
            return null;
        }
        public Group GetGroup(Group g)
        {
            return _groups.GetGroupyId(g.GroupId) != null ? _groups.GetGroupyId(g.GroupId) : null;
        }
        public Group EditGroup(Group g)
        {
            if (_groups.GetGroupyId(g.GroupId)!=null)
            {
                _groups.UpdateGroup(g);
                _groups.Save();
                return g;
            }
            return null;
        }
        public int AddStudent(Group g,Student s, int pin)
        {
            if (_groups.GetGroupyId(g.GroupId)!=null)
            {
                if (_students.isExistingStudentNumber(s.StudentNumber))
                {
                    try
                    {
                        g.Students.Add(s);
                        _groups.UpdateGroup(g);
                        _groups.Save();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        return 0;
                    }                   
                }
            }
            return 0;
        }
        public int RemoveStudent(Group g, Student s)
        {
            if (_groups.GetGroupyId(g.GroupId) != null)
            {
                if (_students.isExistingStudentNumber(s.StudentNumber))
                {
                    try
                    {
                        g.Students.Remove(s);
                        _groups.UpdateGroup(g);
                        _groups.Save();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        return 0;
                    }
                }
            }
            return 0;
        }
        public String GeneratePin()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
        }
    }
}