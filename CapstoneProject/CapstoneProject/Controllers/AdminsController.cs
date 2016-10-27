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

        public ActionResult Projects(String state)
        {
            List<Project> Projects = new List<Project>();
            if (state == null)
            {
               ViewBag.Project = "All Projects";
               Projects = _pm.GetProjects();
            }
            else 
            {
                if (state.Equals("Pending"))
                {
                    ViewBag.Project = state + " Projects";
                }
                else if (state.Equals("Approved")) {
                    ViewBag.Project = state + " Projects";
                }
                Projects = _pm.GetProjectsByState(state);
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