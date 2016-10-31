using CapstoneProject.Models.Services;
using CapstoneProject.Models.ViewModels;
using CapstoneProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Controllers
{
    public class ProjectsController : Controller
    {
        private UserAccountService _uas = new UserAccountService();
        private ProjectManager _pm = new ProjectManager();
        private GroupBuilderService _gbs = new GroupBuilderService();
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
        [HttpGet]
        public ActionResult ProjectMatch()
        {
            ProjectMatchGroup pmg = new ProjectMatchGroup();
            pmg.Projects=_pm.GetProjects();
            pmg.Groups = _gbs.GetGroups();
            
            return View(pmg);
        }
        [HttpPost]
        public ActionResult ProjectMatch(int groupId, int projectId, ProjectMatchGroup pmg)
        {
            
            if (groupId != 0 && projectId != 0)
            {
                Group g = _gbs.GetGroupById(groupId);
                Project p = _gbs.GetProjectById(projectId);
                p.State = "Assigned";
                g.Projects.Clear();
                g.Projects.Add(p);

                _gbs.EditGroup(g);
            }
            else
            {
                ViewBag.SubmitError = "Group and Project must be selected.";
            }
            pmg.Groups = _gbs.GetGroups();
            pmg.Projects = _gbs.GetProjects();
            return View(pmg);
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