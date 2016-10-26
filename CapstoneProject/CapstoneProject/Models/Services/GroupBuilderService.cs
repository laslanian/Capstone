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
            return _groups.GetGroupyId(id) != null ? _groups.GetGroupyId(id) : null;
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
            if (_groups.GetGroupyId(g.GroupId) != null)
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
            Group group = GetGroupById(gs.Group.GroupId);
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
        public int AddStudent(int GroupId, int UserId, string pin)
        {
            Group g = _groups.GetGroupyId(GroupId);
            if (g != null)
            {
                Student s = (Student)_users.GetUserById(UserId);
                if (g.Pin == pin)
                {
                    g.Students.Add(s);
                    g = AddSkills(g, s.Skillset);
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
            Group g = _groups.GetGroupyId(GroupId);
            if (g != null)
            {
                Student s = (Student)_users.GetUserById(UserId);
                g = SubtractSkill(g, s.Skillset);
                g.Students.Remove(s);
                _groups.UpdateGroup(g);
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
            g.Skillset.Programming += s.Programming;
            g.Skillset.WebDev += s.WebDev;
            g.Skillset.MobileDev += s.MobileDev;
            g.Skillset.ApplDev += s.ApplDev;
            g.Skillset.UIDesign += s.UIDesign;
            g = GetAverageSkills(g);
            return g;
        }

        public Group GetAverageSkills(Group g)
        {
            int count = g.Students.Count;

            g.Skillset.Programming = g.Skillset.Programming / count;
            g.Skillset.WebDev = g.Skillset.WebDev / count;
            g.Skillset.MobileDev = g.Skillset.MobileDev / count;
            g.Skillset.ApplDev = g.Skillset.ApplDev / count;
            g.Skillset.UIDesign = g.Skillset.UIDesign / count;

            return g;
        }

        public Group SubtractSkill(Group g, Skillset s)
        {
            g.Skillset.Programming -= s.Programming;
            g.Skillset.WebDev -= s.WebDev;
            g.Skillset.MobileDev -= s.MobileDev;
            g.Skillset.ApplDev -= s.ApplDev;
            g.Skillset.UIDesign -= s.UIDesign;

            g = GetAverageSkills(g);
            return g;
        }
    }
}