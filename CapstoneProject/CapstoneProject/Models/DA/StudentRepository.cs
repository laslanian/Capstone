using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapstoneProject.Models.Interfaces;

namespace CapstoneProject.Models.DA
{
    public class StudentRepository : IStudentRepository, IDisposable
    {
        private CapstoneDBModel ctx;
        private bool disposed = false;

        public StudentRepository()
        {
            this.ctx = new CapstoneDBModel();
        }

        public StudentRepository(CapstoneDBModel context)
        {
            this.ctx = context;
        }

        public IEnumerable<Student> GetStudents()
        {
            return ctx.Users.OfType<Student>().ToList();
        }

        public IEnumerable<Student> GetStudentsByProgram(int ProgramId)
        {
            return ctx.Users.OfType<Student>().ToList().Where(student => student.Program.ProgramId == ProgramId);
        }

        public bool isExistingStudentNumber(int number)
        {
            return ctx.Users.OfType<Student>().Any(student => student.StudentNumber == number);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}