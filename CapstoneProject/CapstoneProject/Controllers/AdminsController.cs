﻿using CapstoneProject.Models.Services;
using CapstoneProject.Models.ViewModels;
using CapstoneProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Controllers
{
    public class AdminsController : Controller
    {
        UserAccountService _uas = new UserAccountService();
        GroupBuilderService _gbs = new GroupBuilderService();

        [NoDirectAccess]
        public ActionResult Users(String type)
        {
            UserList ul = new UserList();
            ul.Users = _uas.GetUsers();
            if (type == null || type.Equals(AccountType.All))
            {
                type = AccountType.All;
            }
            else
            {
                ul.Users = ul.Users.FindAll(s => s.Type == type);
            }
            ul.SelectedType = type;
            ViewBag.Type = type + " Users";
            return View(ul);
        }

        [NoDirectAccess]
        public ActionResult Groups(String status)
        {
            GroupList gl = new GroupList();
            gl.Groups = _gbs.GetGroups();
            if (status == null || status.Equals(GroupState.All))
            {
                status = GroupState.All;
            }
            else
            {
                gl.Groups = gl.Groups.FindAll(s => s.Status == status);
            }
            gl.SelectedStatus = status;
            ViewBag.Status = status + " Groups";
            return View(gl);
        }

        [NoDirectAccess]
        public ActionResult CreateUser()
        {
            CreateAccount ca = new CreateAccount();
            return View(ca);
        }

        [HttpPost]
        public ActionResult DeleteGroup(int groupId)
        {
            if (_gbs.DeleteGroup(groupId) != 1)
            {
                ViewBag.DeleteGroupError = "Unable to delete group.";  
            }
            return RedirectToAction("Groups");
        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult CreateUser(CreateAccount ca)
        {
            if(ModelState.IsValid)
            {
                int code = _uas.CreateUser(ca);
                switch (code)
                {
                    case 1:
                        {
                            ViewBag.CreateUser = "Successfully Added Coop Account.";
                            break;
                        }
                    case 2:
                        {
                            ViewBag.CreateUser = "Successfully Added Management Account.";
                            break;
                        }
                    case 3:
                        {
                            ViewBag.CreateUser = "Successfully Added Admin Account.";
                            break;
                        }
                    case 4:
                        {
                            ViewBag.AuthError = "Username already exists.";
                            return View(ca);
                        }
                    case 5:
                        {
                            ViewBag.EmailError = "Email already exists.";
                            return View(ca);
                        }
                }
                return RedirectToAction("Users");
            }
            return View(ca);
        }

        [NoDirectAccess]
        public ActionResult Details(int id)
        {
            User u = _uas.GetUser(id);
            AesEncrpyt de = new AesEncrpyt();
            u.Username = de.Decrypt(u.Username);
            u.Password = de.Decrypt(u.Password);
            return View(u);
        }

        [NoDirectAccess]
        public ActionResult Edit(int id)
        {
            User u = _uas.GetUser(id);
            AesEncrpyt de = new AesEncrpyt();
            u.Username = de.Decrypt(u.Username);
            u.Password = de.Decrypt(u.Password);
            return View(u);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User u)
        {
            if(ModelState.IsValid)
            {
                
                int code =_uas.EditUser(u);
                if(code > 0)
                {
                    return RedirectToAction("Details", new { id = u.UserId } );
                }
                else
                {
                    return View(u);
                }
            }
            return View(u);
        }

        [NoDirectAccess]
        public ActionResult Delete(int id)
        {
            _uas.DeleteUser(id);
            return RedirectToAction("Users");
        }

        [NoDirectAccess]
        [HttpGet]
        public ActionResult GroupDetail(int id)
        {
            GroupProject gp = new GroupProject();
            gp = _gbs.GetGroupProject(id);

            return View(gp);
        }
        
    }
}