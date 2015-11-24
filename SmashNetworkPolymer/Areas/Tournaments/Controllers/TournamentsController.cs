using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmashNetworkPolymer.Areas.Tournaments.Controllers
{
    public class TournamentsController : Controller
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
