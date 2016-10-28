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
    public class ProjectsController : Controller
    {
        private UserAccountService _uas = new UserAccountService();
        private ProjectManager _pm = new ProjectManager();
        // GET: Projects
        public ActionResult ProjectsByClient()
        {
            return View(_pm.GetProjectsByClient(Convert.ToInt32(Session["Id"])));
        }

        public ActionResult Projects(String state)
        {
            String UserType = Session["UserType"].ToString();
            ProjectWithList pl = new ProjectWithList();
            if (UserType.Equals("Client"))
            {
                pl.Projects = _pm.GetProjectsByClient(Convert.ToInt32(Session["Id"]));
            }
            else if (UserType.Equals("Admin"))
            {
                pl.Projects = _pm.GetProjects();
            }

            if (state == null)
            {
               ViewBag.Project = "All Projects";
               pl.SelectedItem = ProjectState.All;
            }
            else 
            {
                if (state.Equals(ProjectState.Pending))
                {
                    pl.SelectedItem = ProjectState.Pending;
                    ViewBag.Project = ProjectState.Pending + " Projects";
                }
                else if (state.Equals(ProjectState.Approved)) {
                    pl.SelectedItem = ProjectState.Approved;
                    ViewBag.Project = ProjectState.Approved + " Projects";
                }
                else if (state.Equals(ProjectState.Rejected))
                {
                    pl.SelectedItem = ProjectState.Rejected;
                    ViewBag.Project = ProjectState.Rejected + " Projects";
                }
                pl.Projects = pl.Projects.FindAll(s => s.State == state);
            }
            return View(pl);
        }

        public ActionResult Create()
        {
            ProjectForm pf = new ProjectForm();
            return View(pf);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectForm p)
        {
            if(ModelState.IsValid)
            {
                int code = _pm.CreateProject(p, Convert.ToInt32(Session["Id"]));
                if (code == 99)
                {
                    return RedirectToAction("Projects", "Projects", null);
                }
                else
                {
                    return View(p);
                }
            }
            else
            {
                return View(p);
            }
        }

        public ActionResult Details(int id)
        {
            Project p = _pm.GetProjectDetails(id);
            return View(p);
        }
    }
}