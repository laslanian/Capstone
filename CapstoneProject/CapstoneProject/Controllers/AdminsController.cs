using CapstoneProject.Models.Services;
using CapstoneProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Controllers
{
    public class AdminsController : Controller
    {
        //ProjectManager _pm = new ProjectManager();

        //public ActionResult Projects(String state)
        //{
        //    ProjectWithList pl = new ProjectWithList();
        //    if (state == null)
        //    {
        //       ViewBag.Project = "All Projects";
        //        pl.SelectedItem = "All";
        //       pl.Projects = _pm.GetProjects();
        //    }
        //    else 
        //    {
        //        if (state.Equals("Pending"))
        //        {
        //            pl.SelectedItem = "Pending";
        //            ViewBag.Project = state + " Projects";
        //        }
        //        else if (state.Equals("Approved")) {
        //            pl.SelectedItem = "Approved";
        //            ViewBag.Project = state + " Projects";
        //        }
        //        pl.Projects = _pm.GetProjectsByState(state);
        //    }
        //    return View(pl);
        //}

        //public ActionResult GetPendingProjects()
        //{
        //    List<Project> PendingProjects = _pm.GetPendingProjects();
        //    return View(PendingProjects);
        //}
    }
}