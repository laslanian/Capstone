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
            if (UserType.Equals(AccountType.Client))
            {
                pl.Projects = _pm.GetProjectsByClient(Convert.ToInt32(Session["Id"]));
            }
            else if (UserType.Equals(AccountType.Admin))
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
        
        [HttpPost]
        public ActionResult ApproveProject(int id, string state, string type)
        {
            Project p = _pm.GetProjectDetails(id);

            p.State = state;
            p.Type = type;
            int code = _pm.UpdateProject(p);
            if (code == 1)
            {
                return RedirectToAction("Details", "Projects", new { id = id });
            }
            else
            {
                ProjectWithType pt = new ProjectWithType();
                pt.Project = _pm.GetProjectDetails(id);
                pt.ProjTypes = _pm.GetProjectTypes();
                return View(pt);
            }  
        }

        [HttpPost]
        public ActionResult UpdateProjectState(int id, string state)
        {
            Project p = _pm.GetProjectDetails(id);

            p.State = state;

            int code = _pm.UpdateProject(p);
            if (code == 1)
            {
                return RedirectToAction("Details", "Projects", new { id = id });
            }
            else
            {
                return View(p);
            }
        }

        [HttpPost]
        public ActionResult UnassignProject(int projId,int groupId)
        {
            Project p = _gbs.GetProjectById(projId);
            Group g = _gbs.GetGroupById(groupId);

            g.Status=GroupState.Unassigned;
            p.State=ProjectState.Approved;
            g.Project = null;

            _gbs.EditGroup(g);
            _pm.UpdateProject(p);

            return RedirectToAction("ProjectMatch");
           // return View();
        }
        [HttpGet]
        public ActionResult ProjectMatch()
        {
            ProjectMatchGroup pmg = new ProjectMatchGroup();
            pmg.Projects = _pm.GetProjects().Where(item => item.State.Equals(ProjectState.Approved)).ToList();
            pmg.Groups = _gbs.GetGroups().Where(item => item.ProjectRankings.Count ==5 && !item.Status.Equals(GroupState.Assigned)).ToList();

            return View(pmg);
        }

        [HttpPost]
        public ActionResult ProjectMatch(int[] groupId, int[] projectId)
            {
            if (groupId == null)
            {
                ViewBag.SubmitMessage = "Group and Project must be selected.";
            }
            else
            {
                for (int i = 0; i < groupId.Count(); i++)
                {
                    if (groupId[i] != 0 && projectId[i] != 0)
                    {
                        Group g = _gbs.GetGroupById(groupId[i]);
                        Project p = _gbs.GetProjectById(projectId[i]);
                        p.State = ProjectState.Assinged;
                        g.Status = GroupState.Assigned;
                        g.Project = p;

                        _gbs.EditGroup(g);
                    }
                }
                ViewBag.SubmitMessage = "Success!";
            }
            ProjectMatchGroup pmg = new ProjectMatchGroup();
            pmg.Projects = _pm.GetProjects().Where(item => item.State.Equals(ProjectState.Approved)).ToList();
            pmg.Groups = _gbs.GetGroups().Where(item => item.ProjectRankings.Count == 5 && !item.Status.Equals(GroupState.Assigned)).ToList();
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

        [HttpGet] 
        public ActionResult EditProject(int id)
        {
            Project p = _pm.GetProjectDetails(id);
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProject(Project p)
        {
            int code = _pm.UpdateProject(p);
            if(code > 0)
            {
                return RedirectToAction("Details", new { id = p.ProjectId });
            }
            return View(p);
        }

        public ActionResult Details(int id)
        {
            ProjectWithType pt = new ProjectWithType();
            pt.Project = _pm.GetProjectDetails(id);
            pt.ProjTypes = _pm.GetProjectTypes();
            return View(pt);
        }
        
    }
}