using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;

namespace CapstoneProject.Models.Services
{
    public class GroupBuilderService
    {
        IGroupRepository _groups;
        IStudentRepository _students;
        IProjectRepository _projects;

        public Group AddGroup() { return null; }
        public Group GetGroup() { return null; }
        public Group EditGroup() { return null; }
        public String GeneratePin() { return null; }
        public void AddStudent(int pin, int id) { }
        public void RemoveStudent(int id) { }
    }
}