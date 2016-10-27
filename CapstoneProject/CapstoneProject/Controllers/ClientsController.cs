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
        private ProjectManager pm = new ProjectManager();
        public ActionResult Index()       {

            UserAccountService uas = new UserAccountService();
            Client s = (Client) uas.GetUser(Convert.ToInt32(Session["Id"]));
            return View(s);
        }

        public ActionResult Projects()
        {
            return View(pm.GetProjectsByClient(Convert.ToInt32(Session["Id"])));
        }
    }
}
