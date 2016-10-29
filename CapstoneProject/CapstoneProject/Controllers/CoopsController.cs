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
        // GET: Coops
        public ActionResult Details(int id)
        {
            Coop c = scs.GetCoopById(id);
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
            using (StudentCoopService scs = new StudentCoopService())
            {
                scs.AddCoop(Convert.ToInt32(Session["Id"]), c);
                return RedirectToAction("Index", "Students");
            }
        }

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
            int code = scs.UpdateCoop(c);
            if(code > 0)
            {
                return RedirectToAction("Index", "Students");
            }
            return View(c);
        }

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