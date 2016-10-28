using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Models.Interfaces
{
    public interface IStudentRepository : IDisposable
    {
        IEnumerable<Student> GetStudents(); //List of students
        IEnumerable<Student> GetStudentsByProgram(int ProgramId); //Get Students by Program
        bool isExistingStudentNumber(int number);

        Skillset GetSkillSetById(int id);
        Skillset GetSkillByUserId(int id);
    }
}
