using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapstoneProject.Utility;

namespace CapstoneProject.Models.ViewModels
{
    public class GroupList
    {
        public GroupList()
        {
            Groups = new List<Group>();
            GroupStatus = new List<SelectListItem>();
            GroupStatus.Add(new SelectListItem { Text = GroupState.Assigned, Value = GroupState.Assigned });
            GroupStatus.Add(new SelectListItem { Text = GroupState.Unassigned, Value = GroupState.Unassigned });
            GroupStatus.Add(new SelectListItem { Text = GroupState.All, Value = GroupState.All });
        }
        public string SelectedStatus { get; set; }
        public List<Group> Groups { get; set; }
        public List<SelectListItem> GroupStatus { get; set; }
    }
}