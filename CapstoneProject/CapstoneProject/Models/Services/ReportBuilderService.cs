using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;

namespace CapstoneProject.Models.Services
{
    public class ReportBuilderService
    {
        IStudentRepository _students;
        IGroupRepository _groups;
        IProjectRepository _projects;

        public void GenerateReport(String type) { }
        public void GenerateOperational() { }
        public void GenerateExecutive() { }
    }
}