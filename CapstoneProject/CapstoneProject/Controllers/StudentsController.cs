using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapstoneProject.Models.Services;

namespace CapstoneProject.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        public ActionResult Index(int id)
        {
            UserAccountService uas = new UserAccountService();
            Student s = (Student) uas.GetUser(id);
            return View(s);
        }

        public ActionResult AddCoop()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            UserAccountService uas = new UserAccountService();
            Student s = (Student) uas.GetUser(id);
            return View(s);
        }
    }
}