using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Models.Interfaces
{
    interface IUserInterface : IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserByUNPW(string username, string password);
        bool isExistingUsername(string username);
        User GetUserById(int id);
        void InsertUser(User u);
        int UpdateUser(User u);
        int UpdateStudent(Student s);
        int UpdateClient(Client c);
        void DeleteUser(int id);
        void Save();
    }
}
