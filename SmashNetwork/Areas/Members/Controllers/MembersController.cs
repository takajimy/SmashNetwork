using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmashNetwork.Areas.Members.Controllers
{
    public class MembersController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ToolbarTitle = "Smash Network";
            ViewBag.IsLoggedIn = true;
            ViewBag.Username = "Takaji";
            return View();
        }
    }
}
