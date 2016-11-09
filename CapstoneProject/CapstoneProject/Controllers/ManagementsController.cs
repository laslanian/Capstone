using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProject.Controllers
{
    public class ManagementsController : Controller
    {
        // GET: Managements
        public ActionResult Reports()
        {
            return View();
        }
    }
}