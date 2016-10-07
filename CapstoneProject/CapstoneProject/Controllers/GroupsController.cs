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
        
        // GET: Groups
        public ActionResult Index()
        {
            
            return View(gbs.GetGroups());
        }
        public ActionResult Details(int id)
        {
            GroupStudent gs = new GroupStudent();
            gs.Students = GetStudents(id);
            return View(gs);
        }

        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(Group g, string button)
        {
            return View(g);
        }

        public ActionResult JoinGroup()
        {
            return RedirectToAction("Details");
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
                return gbs.GetStudentsByGroupId(id);
            }
        }

    }
}