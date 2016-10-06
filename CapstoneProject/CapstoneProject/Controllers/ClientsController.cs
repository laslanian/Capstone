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

        // GET: Clients
        public ActionResult Index(int id)
        {
            UserAccountService uas = new UserAccountService();
            Client s = (Client) uas.GetUser(id);
            return View(s);
        }

        // GET: Clients/Create
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CapstoneProject.Models.ClientUser client, string button)
        {
            if (button.Equals("Back"))
            {
                return RedirectToAction("TermsAndCondition", "Home");
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
    }
}
