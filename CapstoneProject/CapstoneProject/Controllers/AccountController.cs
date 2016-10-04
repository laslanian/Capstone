using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapstoneProject.Models.Services;

namespace CapstoneProject.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUser u)
        {
            if (ModelState.IsValid)
            {

            }
            return View(u);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return View("Index");
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Message = "Registration page.";

            return View();
        }

        [HttpPost]
        public ActionResult Register(String accounttype)
        {
            if (accounttype.Equals("Student"))
            {
                Session["UserType"] = "Student";
            }
            else
            {
                Session["UserType"] = "Client";
            }
            return RedirectToAction("TermsAndCondition");
        }

        [HttpGet]
        public ActionResult TermsAndCondition()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TermsAndCondition(String button)
        {
            if (button.Equals("No"))
            {
                return RedirectToAction("Register");
            }
            else
            {
                if (Session["UserType"].Equals("Student"))
                {
                    return RedirectToAction("RegisterStudent");
                }
                else {
                    return RedirectToAction("RegisterClient");
                }
            }
        }

        // GET: Students/Register
        public ActionResult RegisterStudent()
        {
            using (ProgramManagerService pms = new ProgramManagerService())
            {
                StudentUser su = new StudentUser();
                su.ProgList = pms.GetPrograms();
                return View(su);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterStudent(CapstoneProject.Models.StudentUser student, string button)
        {
            if (button.Equals("Back"))
            {
                return RedirectToAction("TermsAndCondition");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Student s = new Student();
                    using (ProgramManagerService pms = new ProgramManagerService())
                    {
                        s.Program = pms.GetProgram(student.ProgramId);
                    }
                    using (UserAccountService uas = new UserAccountService())
                    {

                        s.FirstName = student.FirstName;
                        s.LastName = student.LastName;
                        s.PhoneNumber = student.PhoneNumber;
                        s.Email = student.Email;
                        s.Username = student.Username;
                        s.Password = student.Password;
                        s.StudentNumber = Convert.ToInt32(student.StudentNumber);
                        s.Title = "Student";
                        uas.Register(s);
                    }
                    return View("~/View/Account/RegistrationSuccessful.cshtml");
                }
                else
                {
                    using(ProgramManagerService pms = new ProgramManagerService())
                    {
                        student.ProgList = pms.GetPrograms();
                    }
                    return View(student);
                }
            }
        }

        // GET: Clients/Create
        public ActionResult RegisterClient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterClient(CapstoneProject.Models.ClientUser client, string button)
        {
            if (button.Equals("Back"))
            {
                return RedirectToAction("TermsAndCondition");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // use useraccountservice here
                }
            }
            return View(client);
        }

        public ActionResult AfterLogin()
        {
            return View();
        }

        public ActionResult FailedLogin()
        {
            return View();
        }

        private String GetUserType()
        {
            return Session["UserType"].ToString();
        }
    }
}