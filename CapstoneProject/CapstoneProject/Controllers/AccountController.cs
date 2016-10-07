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
                using (LogInService ls = new LogInService())
                {
                    User user = ls.Login(u.Username, u.Password);
                    if (user != null)
                    {
                        String userType = ls.GetUserType(user.UserId);
                        int id = user.UserId;
                        Session["Id"] = id;
                        Session["Username"] = user.Username;
                        Session["UserType"] = userType;
                        switch(userType)
                        {
                            case "Student":
                                {
                                   return RedirectToAction("Index", "Students", new { id = id });
                                }
                            case "Client":
                                {
                                    return RedirectToAction("Index", "Clients", new { id = id });
                                }
                            default:
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                        }
                    }
                }
            }
            ViewBag["AuthError"] = "Invalid username or password";
            return View(u);
        }

        public ActionResult Logout()
        {
            Session["Id"] = null;
            Session["Username"] = null;
            Session["UserType"] = null;
            return RedirectToAction("Index", "Home");
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
            StudentUser su = new StudentUser();
            su.ProgList = GetPrograms();
            return View(su);
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
                    using (UserAccountService uas = new UserAccountService())
                    {
                        int code = uas.RegisterStudent(student);
                        switch (code)
                        {
                            case 1:
                                {
                                    ViewBag.AuthError = "Username already exists.";
                                    break;
                                }
                            case 2:
                                {
                                    ViewBag.AuthError = "Student number already in use.";
                                    break;
                                }
                            case 99:
                                {
                                    return View("~/Views/Account/RegistrationSuccessful.cshtml");
                                }
                        }
                    }
                }
                student.ProgList = GetPrograms();
                return View(student);
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

        public List<Program> GetPrograms()
        {
            using (ProgramManagerService pms = new ProgramManagerService())
            {
                return pms.GetPrograms();
            }
        }
    }
}