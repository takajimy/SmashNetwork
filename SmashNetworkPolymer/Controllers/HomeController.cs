using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SmashNetworkPolymer.Models;
using SmashNetworkPolymer.Areas.Users.Models;

namespace SmashNetworkPolymer.Controllers
{
    public class HomeController : Controller
    {
        SmashNetworkPolymer.DAL.SmashNetworkDBContext db = new SmashNetworkPolymer.DAL.SmashNetworkDBContext();

        public ActionResult Index()
        {
            ViewBag.ToolbarTitle = "Smash Network";
            ViewBag.IsLoggedIn = false;
            ViewBag.Username = "takaji";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                User userMatch = db.Users.FirstOrDefault(u => u.Username == user.Username);
                if (userMatch != null)
                {
                    if (user.IsPasswordMatch(userMatch))
                    {
                        FormsAuthentication.SetAuthCookie(user.Username, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect login.");
                    }
                }
            }
            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
