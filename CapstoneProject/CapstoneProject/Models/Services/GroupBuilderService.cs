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

        public GroupBuilderService()
        {
            this._groups = new GroupRepository();
            this._students = new StudentRepository();
        }

        public bool isExistingGroupName(Group group)
        {
            List<Group> groups = _groups.GetGroups().ToList();
            foreach (Group g in groups)
            {
                if (g.GroupName.Equals(group.GroupName))
                {
                    return true;
                }
            }
            return false;
        }
        public Group AddGroup(Group g, Student s)
        {
            if (!isExistingGroupName(g))
            {
                try
                {
                    _groups.InsertGroup(g);
                    _groups.Save();
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

        public string GenerateCryptoPin(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}