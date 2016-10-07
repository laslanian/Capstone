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
    public class GroupBuilderService :IDisposable
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
            return _groups.GetGroups().ToList() ;
        }
        public List<Student> GetStudentsByGroupId(int id)
        {
            Group g = _groups.GetGroupyId(id);
            return g.Students.ToList();
        }
        
        public Group AddGroup(Group g, int id)
        {
            if (!_groups.isExistingGroup(g.GroupName))
            {
                try
                {
                    AesEncrpyt ae = new AesEncrpyt();
                    String pin = GeneratePin();
                    g.Pin = pin;
                    g.Status = "Unassigned";
                    Student s = (Student)_users.GetUserById(id);
                     g.Students.Add(s);
                    _groups.InsertGroup(g);
                    _groups.Save();
                    // testing send email
                    //EmailService emailService = new EmailService();

                    //emailService.SendGroupPin(s.Email, pin);

                    return g;
                }
                catch (Exception e)
                {
                    return null;
                }
                
            }
            return null;
        }

        public Group GetGroup(int id)
        {
            return _groups.GetGroupyId(id) != null ? _groups.GetGroupyId(id) : null;
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
        public GroupStudent GetGroupStudentVM(int id)
        {
            Group g = GetGroup(id);
            GroupStudent gs = new GroupStudent();
            List<Student> students = GetStudentsByGroupId(g.GroupId);
            foreach (Student s in students)
            {
                Student st = new Student();
                st.FirstName = s.FirstName;
                st.LastName = s.LastName;
                //st.Username = s.Username;
                //st.PhoneNumber = s.PhoneNumber;
                //st.Program = s.Program;
                //st.StudentNumber = s.StudentNumber;
                //st.Title = s.Title;
                //st.Interests = s.Interests;
                //st.Email = s.Email;
                //st.Coops = s.Coops;

                gs.Students.Add(s);
            }
            return gs;
        }
        public GroupStudent EditGroupStudentVM(GroupStudent gs)
        {
            Group group = GetGroup(gs.Group.GroupId);
            group.Students.Clear();

            foreach (Student s in gs.Students)
            {
                Student st = s; 
                group.Students.Add(s);
            }
            _groups.UpdateGroup(group);
            _groups.Save();

            return gs;
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
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 4);
        }

        public void Dispose()
        {
            _groups.Dispose();
            _students.Dispose();
            _projects.Dispose();
            _users.Dispose();
        }
    }
}