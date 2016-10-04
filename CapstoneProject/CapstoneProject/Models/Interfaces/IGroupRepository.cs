using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Models.Interfaces
{
    interface IGroupRepository
    {
        IEnumerable<Group> GetGroups();
        Group GetGroupyId(int id);
        void InsertGroup(Group g);
        void UpdateGroup(Group g);
        void DeleteGroup(int id);
        void Save();
    }
}
