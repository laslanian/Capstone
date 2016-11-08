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
                g.Status = "Unassigned";
                Student s = (Student)_users.GetUserById(id);
                g.Students.Add(s);
                g.Skillset = new Skillset();
                g.Owner = id;
                g = AddSkills(g, s.Skillset);
                _groups.InsertGroup(g);
                _groups.Save();


                // testing send email
                EmailService emailService = new EmailService();

                emailService.SendGroupPin(s.Email, pin);

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
                    g = AddSkills(g, s.Skillset);
                    g.Students.Add(s);
                    g = GetAverageSkills(g);
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
                g = SubtractSkill(g, s.Skillset);
                g.Students.Remove(s);
                g = GetAverageSkills(g);

                if (g.Students.Count == 0)
                {
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

        public Group AddSkills(Group g, Skillset s)
        {
            int count = g.Students.Count;

            g.Skillset.CSharp *= count;
            g.Skillset.Java *= count;
            g.Skillset.Database *= count;
            g.Skillset.WebDev *= count;
            g.Skillset.MobileDev *= count;
            g.Skillset.ApplDev *= count;
            g.Skillset.UIDesign *= count;

            g.Skillset.CSharp += s.CSharp;
            g.Skillset.Java += s.Java;
            g.Skillset.Database += s.Database;
            g.Skillset.WebDev += s.WebDev;
            g.Skillset.MobileDev += s.MobileDev;
            g.Skillset.ApplDev += s.ApplDev;
            g.Skillset.UIDesign += s.UIDesign;

            return g;
        }

        public Group GetAverageSkills(Group g)
        {
            int count = g.Students.Count ;

            g.Skillset.CSharp = g.Skillset.CSharp / count;
            g.Skillset.Java = g.Skillset.Java / count;
            g.Skillset.Database = g.Skillset.Database / count;
            g.Skillset.WebDev = g.Skillset.WebDev / count;
            g.Skillset.MobileDev = g.Skillset.MobileDev / count;
            g.Skillset.ApplDev = g.Skillset.ApplDev / count;
            g.Skillset.UIDesign = g.Skillset.UIDesign / count;

            return g;
        }

        public Group SubtractSkill(Group g, Skillset s)
        {
            int count = g.Students.Count;

            g.Skillset.CSharp *= count;
            g.Skillset.Java *= count;
            g.Skillset.Database *= count;
            g.Skillset.WebDev *= count;
            g.Skillset.MobileDev *= count;
            g.Skillset.ApplDev *= count;
            g.Skillset.UIDesign *= count;

            g.Skillset.CSharp -= s.CSharp;
            g.Skillset.Java -= s.Java;
            g.Skillset.Database -= s.Database;
            g.Skillset.WebDev -= s.WebDev;
            g.Skillset.MobileDev -= s.MobileDev;
            g.Skillset.ApplDev -= s.ApplDev;
            g.Skillset.UIDesign -= s.UIDesign;

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