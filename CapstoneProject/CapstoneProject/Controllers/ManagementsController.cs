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
    public class ManagementsController : Controller
    {
        private GroupBuilderService _gbs = new GroupBuilderService();
        private ProjectManager _pm = new ProjectManager();
        private UserAccountService _uas = new UserAccountService();
        // GET: Managements

        [NoDirectAccess]
        public ActionResult ProjectsReports()
        {
            List<Project> Projects = _pm.GetProjects();
            ProjectRatio pr = new ProjectRatio();
            pr.Approved = Projects.FindAll(s => s.State == ProjectState.Approved).Count();
            pr.Rejected = Projects.FindAll(s => s.State == ProjectState.Rejected).Count();
            pr.Completed = Projects.FindAll(s => s.State == ProjectState.Complete).Count();
            pr.Pending = Projects.FindAll(s => s.State == ProjectState.Pending).Count();
            pr.Total = pr.GetTotal();
            return View(pr);
        }

        public ActionResult ProjectChart(int approved, int rejected, int completed, int pending)
        {
            ProjectRatio pr = new ProjectRatio();
            pr.Approved = approved;
            pr.Rejected = rejected;
            pr.Completed = completed;
            pr.Pending = pending;
            return View(pr);
        }

        public ActionResult GroupChart(int assigned, int notassigned)
        {
            GroupRatio gr = new GroupRatio();
            gr.Assigned = assigned;
            gr.NotAssigned = notassigned;
            return View(gr);
        }

        [NoDirectAccess]
        public ActionResult GroupsReports()
        {
            List<Group> Groups = _gbs.GetGroups();
            GroupRatio gr = new GroupRatio();
            gr.Assigned = Groups.FindAll(g => g.Status == GroupState.Assigned).Count();
            gr.NotAssigned = Groups.FindAll(g => g.Status == GroupState.Unassigned).Count();
            gr.Total = gr.GetTotal();
            gr.AssignedPct = gr.GetAssignedPercent();
            gr.NotAssignedPct = gr.GetNotAssignedPercent();
            return View(gr);
        }
    }
}