using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapstoneProject.Models;
using CapstoneProject.Models.Services;
using CapstoneProject.Models.ViewModels;
using CapstoneProject.Utility;

namespace CapstoneProject.Controllers
{
    public class GroupsController : Controller
    {
        private GroupBuilderService gbs = new GroupBuilderService();
        private UserAccountService uas = new UserAccountService();

        [NoDirectAccess]
        public ActionResult Index()
        {
            Student s = (Student)uas.GetUser(Convert.ToInt32(Session["Id"]));
            if (s.Skillset == null)
            {
                return RedirectToAction("CreateSkillset");
            }
            else
            {
                StudentGroup sg = gbs.GetStudentGroup(Convert.ToInt32(Session["Id"]));
                return View(sg);
            }
        }

        [NoDirectAccess]
        public ActionResult Details()
        {
            int UserId = Convert.ToInt32(Session["Id"]);
            GroupProject gp = new GroupProject();
            gp = gbs.GetGroupDetails(UserId);
            if (gp.Group.Owner != null && UserId == gp.Group.Owner.Value)
            {
               ViewBag.Owner = true;
            }
            return View(gp);
        }

        [NoDirectAccess]
        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(Group g)
        {
            if (ModelState.IsValid)
            {
                gbs.AddGroup(g, Convert.ToInt32(Session["Id"]));
                return RedirectToAction("Details", g.GroupId);
            }
            else {
                return View(g);
            }
        }

        [NoDirectAccess]
        public ActionResult CreateSkillset() // BEFORE VIEWING GROUPS MAKE USER ENTER SKILLLSET
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSkillset(Skillset s, string button)
        {
            if (ModelState.IsValid)
            {
                if (button == "Submit")
                {
                    int code = uas.AddStudentSkill(s, Convert.ToInt32(Session["Id"]));
                    if (code == 1) return RedirectToAction("Index", "Students");
                }
            }
            ViewBag.SkillError = "An error has occured.";
            return View();
        }

        [NoDirectAccess]
        [HttpGet]
        public ActionResult JoinGroup(int id)
        {
            Group g = gbs.GetGroupById(id);
            g.Pin = "";
            return View(g);
        }

        [HttpPost]
        public ActionResult JoinGroup(Group g)
        {
            Group gr = gbs.GetGroupById(g.GroupId);
            if (g.Pin == null || g.Pin.Equals(String.Empty))
            {
                ViewBag.JoinError = "Pin is required";
                return View(gr);
            }
            else {
                int code = gbs.AddStudent(g.GroupId, Convert.ToInt32(Session["Id"]), g.Pin);
                if (code == 99)
                {
                    return RedirectToAction("Details", new { id = Convert.ToInt32(Session["Id"]) });
                }
                else
                {
                    ViewBag.JoinError = "Incorrect pin";
                    return View(gr);
                }
            }
        }

        [NoDirectAccess]
        public ActionResult Edit(int id)
        {
            GroupStudent gs = gbs.GetGroupStudentVM(id);

            return View(gs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupStudent gs)
        {
            if (ModelState.IsValid)
            {
                gs = gbs.EditGroupStudentVM(gs);
            }
            return View(gs);
        }

        [NoDirectAccess]
        public ActionResult LeaveGroup(int id)
        {
            Group g = gbs.GetGroupById(id);

            int code = 0;

            if (g.Owner.Equals(uas.GetUser(Convert.ToInt32(Session["Id"])).UserId))
            {
                if (g.Students.Count < 2)
                {
                    code = gbs.RemoveStudent(id, Convert.ToInt32(Session["Id"]));
                }
                else
                {
                    code = 0;
                }
            }
            else
            {
                code = gbs.RemoveStudent(id, Convert.ToInt32(Session["Id"]));
            }


            if (code == 99)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Details", new { id = Convert.ToInt32(Session["Id"]) });
            }
        }

        [NoDirectAccess]
        public List<Student> GetStudents(int id)
        {
            using (GroupBuilderService gbs = new GroupBuilderService())
            {
                Group g = gbs.GetGroupById(id);
                return g.Students.ToList();
            }
        }

        [NoDirectAccess]
        [HttpGet]
        public ActionResult AssignProjects(int id)
        {
            GroupProject gp = new GroupProject();
            gp.Group = gbs.GetGroupById(id);
            gp.Projects = gbs.GetProjectsByState(ProjectState.Approved);
            gp.ProjectTypes = gbs.GetProjectTypes();
            return View(gp);
        }

        [HttpPost]
        public ActionResult AssignProjects(GroupProject gp, FormCollection collection)
        {

            Group g = gbs.GetGroupById(gp.Group.GroupId);
            gp.Group = g;
            gp.Projects = gbs.GetProjectsByState(ProjectState.Approved);
            gp.ProjectTypes = gbs.GetProjectTypes();
            g.ProjectRankings.Clear();
            foreach (Project p in gp.Projects)
            {

                var value = collection["project" + p.ProjectId];
                if (!value.Equals("0"))
                {
                    ProjectRanking pr = new ProjectRanking();
                    pr.Group = g;
                    pr.GroupGroupId = g.GroupId;
                    pr.ProjectId = p.ProjectId.ToString();
                    pr.Rank = value.ToString();
                    g.ProjectRankings.Add(pr);
                }
            }
            if (g.ProjectRankings.Count == 5)
            {
                gbs.AddProjectPreference(g);
                return RedirectToAction("Details", new { id = Convert.ToInt32(Session["Id"]) });
            }
            else
            {
                ViewBag.CountError = "You must select 5 projects";
                gp.Projects = gbs.GetProjectsByState(ProjectState.Approved);
                return View(gp);
            }

        }
    }
}
