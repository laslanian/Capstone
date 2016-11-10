﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapstoneProject.Models;
using CapstoneProject.Models.Services;
using CapstoneProject.Models.ViewModels;

namespace CapstoneProject.Controllers
{
    public class GroupsController : Controller
    {
        private GroupBuilderService gbs = new GroupBuilderService();
        private UserAccountService uas = new UserAccountService();
        
        
        // GET: Groups
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

        public ActionResult Details()
        {
            Student s = (Student) uas.GetUser(Convert.ToInt32(Session["Id"]));
            Group g = new Group();
            if(s.Group != null)
            {
                if (s.UserId == s.Group.Owner.Value)
                {
                    ViewBag.Owner = true;
                }
                g = gbs.GetGroupById(s.Group.GroupId);
                 Skillset sk = gbs.GetSkillsetByGroupId(s.Group.GroupId);
                if (sk != null) { g.Skillset = sk; }
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
                return RedirectToAction("Details", g.GroupId);
            }
            else {
                return View(g);
            }
        }

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
                    if(code == 1) return RedirectToAction("Index", "Students");
                }
            }
            ViewBag.SkillError = "An error has occured.";
            return View();
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
            else
            {
                ViewBag.JoinError = "Incorrect pin";
                return View(g);
            }
        }

        
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
       
        public List<Student> GetStudents(int id)
        {
            using (GroupBuilderService gbs = new GroupBuilderService())
            {
                Group g = gbs.GetGroupById(id);
                return g.Students.ToList();
            }
        }
        [HttpGet]
        public ActionResult AssignProjects(int id)
        {
            GroupProject gp = new GroupProject();
            gp.Group = gbs.GetGroupById(id);
            gp.Projects = gbs.GetProjects();
            gp.hasAssignedProject = false;

            return View(gp);
        }
        [HttpPost]
        public ActionResult AssignProjects(GroupProject gp, FormCollection collection)
        {
            var selected = collection.GetValues("chkSelected");

            Group g = gbs.GetGroupById(gp.Group.GroupId);

            if (selected != null)
            {
                if (selected.Count()!=5)
                {
                    gp.Group = g;
                    gp.Projects = gbs.GetProjects();
                    ViewBag.CountError = "You must select 5 projects";
                    return View(gp);
                }
                else
                {
                    for (int i = 0, len = selected.Length; i < len; i++)
                    {
                        g.Projects.Add(gbs.GetProjectById(Convert.ToInt32(selected[i])));
                    }
                    gp.Group = g;

                    int code = gbs.AddProjectPreference(g);
                    if (code == 1)
                    {
                        return RedirectToAction("Details", new { id = Convert.ToInt32(Session["Id"]) });
                    }
                
                }
            }
            else
            {
                g.Projects.Clear();
                gbs.EditGroup(g);
                return RedirectToAction("Details", new { id = Convert.ToInt32(Session["Id"]) });
            }
            return RedirectToAction("Details", new { id = Convert.ToInt32(Session["Id"]) });
        }



    }
}
