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
        public ActionResult Index(int id)
        {
            UserAccountService uas = new UserAccountService();
            //StudentProfile sp = new StudentProfile();
            Student s = (Student)uas.GetUser(id);
            // Program p = (Program)uas.GetProgramById(Convert.ToInt32(s.ProgramId));
            //  sp.student = s;
            // sp.program = p;
            return View(s);
        }

        public ActionResult AddCoop()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCoop(Coop c)
        {
            using (StudentCoopService scs = new StudentCoopService())
            {
                scs.AddCoop(Convert.ToInt32(Session["Id"]), c);
                return RedirectToAction("Index", new { id = Convert.ToInt32(Session["Id"]) });
            }
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
            return View(s);
        }

        public ActionResult CoopDetails(int id)
        {
            return View();
        }

        public ActionResult DeleteCoop()
        {
            return View();
        }

        public ActionResult CreateChart(int id)
        {
            using (StudentRepository sr = new StudentRepository())
            {
                Skillset s = sr.GetSkillSetById(id);

                var skillChart = new Chart(width: 500, height: 300)
                    .SetYAxis(min: 0, max: 10)
                    .AddSeries(chartType: "bar",
                        xValue: new[] { "Programming", "Web Development", "Mobile Development", "Application Development", "UI Design" },
                        yValues: new[] { s.Programming,
                             s.WebDev,
                             s.MobileDev,
                             s.ApplDev,
                             s.UIDesign }).Write();
                skillChart.Save("~/Content/Images/" + id, "jpeg");

                return base.File("~/Content/Images/" + id, "jpeg");
            }
        }
    }
}