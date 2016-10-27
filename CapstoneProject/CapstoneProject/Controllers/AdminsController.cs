using CapstoneProject.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Controllers
{
    public class AdminsController : Controller
    {
        ProjectManager _pm = new ProjectManager();

        public ActionResult Projects(String type)
        {
            List<Project> Projects = new List<Project>();
            if (type == null)
            {
               ViewBag.Project = "All Projects";
               Projects = _pm.GetProjects();
            }
            else if(type.Equals("Pending"))
            {
                ViewBag.Project = "Pending Projects";
                Projects = _pm.GetPendingProjects();
            }
            return View(Projects);
        }

        public ActionResult GetPendingProjects()
        {
            List<Project> PendingProjects = _pm.GetPendingProjects();
            return View(PendingProjects);
        }
    }
}