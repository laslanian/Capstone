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
            return View(c);
        }
    }
}