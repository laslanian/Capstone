using CapstoneProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Models.ViewModels
{
    public class UserList
    {
        public UserList()
        {
            Users = new List<User>();
            UserTypes = new List<SelectListItem>();
            UserTypes.Add(new SelectListItem { Text = AccountType.All, Value = AccountType.All });
            UserTypes.Add(new SelectListItem { Text = AccountType.Student, Value = AccountType.Student });
            UserTypes.Add(new SelectListItem { Text = AccountType.Client, Value = AccountType.Client});
            UserTypes.Add(new SelectListItem { Text = AccountType.Coop_Advisor, Value = AccountType.Coop_Advisor });
            UserTypes.Add(new SelectListItem { Text = AccountType.Management, Value = AccountType.Management});
            UserTypes.Add(new SelectListItem { Text = AccountType.Admin, Value = AccountType.Admin });
        }
        public string SelectedType { get; set; }
        public List<User> Users { get; set; }
        public List<SelectListItem> UserTypes { get; set; }
    }
}