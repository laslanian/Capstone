using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Controllers
{
    public class AdminsController : Controller
    {
        // GET: Admins
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult CreateUser(User u)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("CreateUser");
            }
            return View(u);
        }
    }
}