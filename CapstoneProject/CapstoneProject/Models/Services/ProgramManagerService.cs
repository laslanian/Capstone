using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapstoneProject.Models.DA;
using CapstoneProject.Models.Interfaces;
using CapstoneProject;

namespace CapstoneProject.Models.Services
{
    public class ProgramManagerService : IDisposable
    {
        IProgramRepository _programs;

        public ProgramManagerService()
        {
            this._programs = new ProgamRepository();
        }

        public List<Program> GetPrograms()
        {
            return _programs.GetPrograms().ToList();
        }
        public Program AddProgram(Program p) { return null; }
        public Program GetProgram(int id)
        {
            return _programs.GetProgram(id);
        }
        public Program EditProgram(int id) { return null; }
        public void DeleteProgram(int id) { }

        public void Save()
        {
            _programs.Save();
        }
        public void Dispose()
        {
            _programs.Dispose();
        }
    }
}