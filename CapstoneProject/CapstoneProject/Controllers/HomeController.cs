using CapstoneProject.Models;
using CapstoneProject.Models.DA;
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
    public class HomeController : Controller
    {
        private UserAccountService _uas = new UserAccountService();
        private ProjectManager _pm = new ProjectManager();
        private GroupBuilderService _gbs = new GroupBuilderService();

        public ActionResult Index()
        {
            UserAccountService uas = new UserAccountService();
            return View(uas.GetFeedbacks().Take(3));
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

        public ActionResult Projects(String state)
        {
            ProjectWithList pl = new ProjectWithList();
            pl.Projects = _pm.GetProjects();


            if (state == null || state.Equals(ProjectState.All))
            {
                state = ProjectState.All;
            }
            else
            {
                pl.Projects = pl.Projects.FindAll(s => s.State == state);
            }
            pl.SelectedItem = state;
            ViewBag.Project = state + " Projects";
            return View(pl);
        }
    }
}