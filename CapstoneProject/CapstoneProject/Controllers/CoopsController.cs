using CapstoneProject.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Controllers
{
    public class CoopsController : Controller
    {
        private StudentCoopService scs = new StudentCoopService();
        private UserAccountService uas = new UserAccountService();
        // GET: Coops

        [NoDirectAccess]
        public ActionResult Students()
        {
            return View(scs.GetStudents());
        }

        [NoDirectAccess]
        public ActionResult ViewCoop(int id)
        {
            Student s = (Student) uas.GetUser(id);
            ViewBag.Id = s.UserId;
            return View(s);
        }

        [NoDirectAccess]
        public ActionResult Details(int id)
        {
            Coop c = scs.GetCoopById(id);
            return View(c);
        }


        [NoDirectAccess]
        [HttpGet]
        public ActionResult AddComment(int id)
        {
            Coop c = scs.GetCoopById(id);
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(Coop c)
        {
            int code = scs.UpdateCoop(c);
            if (code > 0)
            {
                return RedirectToAction("Details", new { id = c.CoopId });
            }
            return View(c);
        }

        public ActionResult AddCoop()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCoop(Coop c)
        {
            if (ModelState.IsValid)
            {
                int result = DateTime.Compare(c.StartDate, c.EndDate);
                if (result >= 0)
                {
                    ViewBag.DateError = "The start date must be earlier than end date.";
                    return View(c);
                }
                using (StudentCoopService scs = new StudentCoopService())
                {
                    scs.AddCoop(Convert.ToInt32(Session["Id"]), c);
                    return RedirectToAction("Index", "Students");
                }
            }
            return View(c);
        }

        [NoDirectAccess]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Coop c = scs.GetCoopById(id);
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Coop c)
        {
            int result = DateTime.Compare(c.StartDate, c.EndDate);
            if (result >= 0)
            {
                ViewBag.DateError = "The start date must be earlier than end date.";
                return View(c);
            }
            int code = scs.UpdateCoop(c);
            if(code > 0)
            {
                return RedirectToAction("Index", "Students");
            }
            return View(c);
        }

        [NoDirectAccess]
        public ActionResult DeleteCoop(int id)
        {
            int code = scs.DeleteCoop(id);
            if(code > 0)
            {
                return RedirectToAction("Index", "Students");
            }
            return View();
        }
    }
}