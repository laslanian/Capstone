using CapstoneProject.Models;
using CapstoneProject.Models.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Capstone Portal designed for convenience.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Capstone Contact.";

            return View();
        }

        public ActionResult Feedback()
        {
            ViewBag.Message = "Client Feedback.";

            return View();
        }
    }
}