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
            return null; // ctx.Users.OfType<Student>().ToList().Where(student => student.ProgramId == ProgramId);
        }

        public bool isExistingStudentNumber(int number)
        {
            return ctx.Users.OfType<Student>().Any(student => student.StudentNumber == number);
        }

        public Skillset GetSkillSetById(int id)
        {
            return ctx.Skillsets.Find(id);
        }

        public Skillset GetSkillByUserId(int id)
        {
            var skill = ctx.Skillsets.SingleOrDefault(s => s.Student.UserId == id);
            return skill;
        }

        public Coop GetCoopById(int id)
        {
            var coop = ctx.Coops.SingleOrDefault(c => c.CoopId == id);
            return coop;
        }

        public int UpdateCoop(Coop c)
        {
            ctx.Entry(c).State = System.Data.Entity.EntityState.Modified;
            return ctx.SaveChanges();
        }

        public int DeleteCoop(int id)
        {
            var coop = ctx.Coops.SingleOrDefault(c => c.CoopId == id);
            ctx.Coops.Remove(coop);
            return ctx.SaveChanges();
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