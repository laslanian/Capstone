using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapstoneProject;
using CapstoneProject.Models;
using CapstoneProject.Models.DA;
using CapstoneProject.Models.Services;

namespace CapstoneProject.Controllers
{
    public class ClientsController : Controller
    {
        public ActionResult Index()       {

            UserAccountService uas = new UserAccountService();
            Client s = (Client) uas.GetUser(Convert.ToInt32(Session["Id"]));
            return View(s);
        }

        public ActionResult Edit(int id)
        {
            UserAccountService uas = new UserAccountService();
            Client c = (Client) uas.GetUser(id);
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client c)
        {
            UserAccountService uas = new UserAccountService();
            int code = uas.UpdateClient(c);
            if (code > 0)
            {
                return RedirectToAction("Index");
            }
            return View(c);
        }
    }
}
