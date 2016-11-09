using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapstoneProject.Models.Services;
using CapstoneProject.Models.ViewModels;
using System.Web.Helpers;
using CapstoneProject.Models.DA;

namespace CapstoneProject.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        public ActionResult Index()
        {
           
            if (TempData["ChangePassConfirm"]!=null)
            {
                ViewBag.ChangePassConfirmMsg = TempData["ChangePassConfirm"].ToString();
            }
            UserAccountService uas = new UserAccountService();

            Student s = (Student) uas.GetUser(Convert.ToInt32(Session["Id"]));
            Skillset sk = uas.GetSkillsetByUserId(s.UserId);
            s.Skillset = sk;

            Program p = uas.GetProgramById(s.ProgramId);
            StudentProfile sp = new StudentProfile();

            sp.Student = s;
            sp.Program = p;

            return View(sp);
        }

        public ActionResult Edit(int id)
        {
            UserAccountService uas = new UserAccountService();
            Student s = (Student)uas.GetUser(id);
            return View(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student s)
        {
            UserAccountService uas = new UserAccountService();
            int code = uas.UpdateStudent(s);
            if(code > 0)
            {
                return RedirectToAction("Index");
            }
            return View(s);
        }

        public ActionResult CreateChart(int id)
        {
            using (StudentRepository sr = new StudentRepository())
            {
                Skillset s = sr.GetSkillSetById(id);

                return View(s);
            }
        }
    }
}