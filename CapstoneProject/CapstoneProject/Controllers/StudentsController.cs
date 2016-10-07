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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCoop(Coop c)
        {
            using (StudentCoopService scs = new StudentCoopService())
            {
                scs.AddCoop(Convert.ToInt32(Session["Id"]), c);
            }
            return View(c);
        }

        public ActionResult Edit(int id)
        {
            UserAccountService uas = new UserAccountService();
            Student s = (Student) uas.GetUser(id);
            return View(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student s)
        {
            return View(s);
        }
    }
}