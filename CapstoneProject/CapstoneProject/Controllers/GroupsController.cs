using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapstoneProject.Models;
using CapstoneProject.Models.Services;

namespace CapstoneProject.Controllers
{
    public class GroupsController : Controller
    {
        private GroupBuilderService gbs = new GroupBuilderService();
        private UserAccountService uas = new UserAccountService();
        
        // GET: Groups
        public ActionResult Index()
        {
            StudentGroup sg = gbs.GetStudentGroup(Convert.ToInt32(Session["Id"]));
            return View(sg);
        }

        public ActionResult Details(int id)
        {
            Student s = (Student) uas.GetUser(id);
            Group g = new Group();
            if(s.Group != null)
            {
                 g = gbs.GetGroupById(s.Group.GroupId);
            }
            return View(g);
        }

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
                return RedirectToAction("Index");
            }
            else {
                return View(g);
            }
        }

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
            int code = gbs.AddStudent(g.GroupId, Convert.ToInt32(Session["Id"]), g.Pin);
            if(code == 99)
            {
                return RedirectToAction("Details", new { id = Convert.ToInt32(Session["Id"]) });
            }
            else if (code == 1)
            {
                ViewBag.JoinError = "Already have a group";
                return View();
            }
            else if (code == 2)
            {
                ViewBag.JoinError = "Incorrect pin";
                return View();
            }
            else
            {
                return View();
            }
        }

        
        public ActionResult Edit(int id)
        {
            GroupStudent gs = gbs.GetGroupStudentVM(id);

            return View(gs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GroupStudent gs)
        {
            if (ModelState.IsValid)
            {
                gs = gbs.EditGroupStudentVM(gs);
            }
            return View(gs);
        }

        public List<Student> GetStudents(int id)
        {
            using (GroupBuilderService gbs = new GroupBuilderService())
            {
                Group g = gbs.GetGroupById(id);
                return g.Students.ToList();
            }
        }

    }
}