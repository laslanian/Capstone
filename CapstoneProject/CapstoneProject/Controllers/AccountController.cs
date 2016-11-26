﻿using CapstoneProject.Models;
using CapstoneProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapstoneProject.Models.Services;
using reCAPTCHA.MVC;
using System.IO;
using CapstoneProject.Utility;
using System.Web.Helpers;

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
        public ActionResult SendResetPassword(string email)
        {
            int code = 0;
            using (UserAccountService _uas = new UserAccountService())
            {
                AesEncrpyt en = new AesEncrpyt();
                User user = _uas.GetUserByUname(email);
                if (user != null)
                {
                    user.Password = GenerateTempPass();
                    _uas.UpdateUserPW(user);

                    EmailService es = new EmailService();
                    var url = Url.Action("ResetPassword", "Account", routeValues: null, protocol: Request.Url.Scheme);
                    code = es.SendResetPassword(user, en.Decrypt(user.Password), url);
                    if (code != 99)
                    {
                        //error
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            ResetPassword rp = new ResetPassword();
            return View(rp);
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPassword rp)
        {
            if (ModelState.IsValid)
            {
                AesEncrpyt en = new AesEncrpyt();
                using (UserAccountService uas = new UserAccountService())
                {
                    User user = uas.GetUserByEmail(rp.Email);
                    user = uas.GetUserByUnPW(user.Username, en.Encrypt(rp.TempPassword));
                    if (user != null)
                    {
                        user.Password = rp.NewPassword;
                        uas.UpdateUserPW(user);
                    }

                }
                return RedirectToAction("Login", "Account");
            }
            return View();

        }

        public String GenerateTempPass()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 10).Trim();
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            ChangePassword cp = new ChangePassword();

            return View(cp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword cp)
        {

            if (ModelState.IsValid)
            {

                using (UserAccountService _uas = new UserAccountService())
                {
                    int code = 0;
                    User user = _uas.GetUser(Convert.ToInt32(Session["Id"]));
                    AesEncrpyt en = new AesEncrpyt();
                    if (user.Password.Equals(en.Encrypt(cp.OldPassword)))
                    {
                        code = _uas.ChangePassword(cp, user);
                        if (code == 1)
                        {
                            ViewBag.ChangePasswordMessage = "Successfuly changed password!";
                        }
                        else
                        {
                            ViewBag.ChangePasswordMessage = "Password change unsuccessful.";
                        }
                    }
                    else
                    {
                        ViewBag.ChangePasswordMessage = "Password change unsuccessful.";
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidator]
        public ActionResult Login(LoginUser u, bool captchaValid)
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
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.LoginError = "Invalid username or password";
                        return View(u);
                    }
                }
            }
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
                Session["accountType"] = "Student";
            }
            else
            {
                Session["accountType"] = "Client";
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
            if (button.Equals("Cancel"))
            {
                return RedirectToAction("Register");
            }
            else
            {
                if (Session["accountType"].Equals("Student"))
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
                                    ViewBag.SNError = "Student number already in use.";
                                    break;
                                }
                            case 3:
                                {
                                    ViewBag.EmailError = "Email already exist.";
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
                    using (UserAccountService uas = new UserAccountService())
                    {
                        int code = uas.RegisterClient(client);
                        switch (code)
                        {
                            case 1:
                                {
                                    ViewBag.AuthError = "Username already exists.";
                                    break;
                                }
                            case 2:
                                {
                                    ViewBag.EmailError = "Email already exists.";
                                    break;
                                }
                            case 99:
                                {
                                    return View("~/Views/Account/RegistrationSuccessful.cshtml");
                                }
                        }
                    }
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

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            string usertype = Session["UserType"].ToString();
            using (UserAccountService _uas = new UserAccountService())
            {
                User user = _uas.GetUser(Convert.ToInt32(Session["Id"]));
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(file.FileName);
                    if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".gif")
                    {
                        if (file.ContentLength < 2097153)
                        {
                            string path = Path.Combine(Server.MapPath(@"~/Content/Images/Profiles"), user.Username + ".png");
                            file.SaveAs(path);
                        }
                        else
                        {
                            ViewBag.FileError = "Image must be 2MB or less";
                        }
                    }
                    else {
                        ViewBag.FileError = "Invalid file extension";
                    }
                }
                else {
                    ViewBag.FileError = "Please browse for an image to upload";
                }
            }
            if (usertype.Equals(AccountType.Student))
            {
                return RedirectToAction("Index", "Students");
            }
            else
            {
                return RedirectToAction("Index", "Clients");
            }
        }

        public ActionResult GetImage(string filename)
        {
            var root = Server.MapPath(@"~/Content/Images/Profiles");
            var path = Path.Combine(root, filename + ".png");
            path = Path.GetFullPath(path);
            if (path.StartsWith(root) && System.IO.File.Exists(path))
            {
                return base.File(path, "image/png");
            }
            else
            {
                return base.File(Server.MapPath(@"~/Content/Images/man-unknown-4.png"), "image/png");
            }
        }
    }
}