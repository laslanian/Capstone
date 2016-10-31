using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Models.Interfaces
{
    interface IProgramRepository : IDisposable
    {
        IEnumerable<Program> GetPrograms();
        Program GetProgram(int id);
        void InsertProgram(Program p);
        void UpdateProgram(Program p);
        void DeleteProgram(int id);
        void Save();
    }
}
