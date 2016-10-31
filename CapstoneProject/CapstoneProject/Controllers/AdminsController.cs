using CapstoneProject.Models.Services;
using CapstoneProject.Models.ViewModels;
using CapstoneProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Controllers
{
    public class AdminsController : Controller
    {
        UserAccountService _uas = new UserAccountService();

        public ActionResult Users(String type)
        {
            UserList ul = new UserList();
            ul.Users = _uas.GetUsers();
            if (type == null || type.Equals(AccountType.All))
            {
                type = AccountType.All;
            }
            else
            {
                ul.Users = ul.Users.FindAll(s => s.Type == type);
            }
            ul.SelectedType = type;
            ViewBag.Type = type + " Users";
            return View(ul);
        }

        // GET: Admins
        public ActionResult CreateUser()
        {
            CreateAccount ca = new CreateAccount();
            return View(ca);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult CreateUser(CreateAccount ca)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("CreateUser");
            }
            return View(ca);
        }

        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}