using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SmashNetwork.Models;
using SmashNetwork.Areas.Users.Models;

namespace SmashNetwork.Controllers
{
    public class HomeController : Controller
    {
        SmashNetwork.DAL.SmashNetworkDBContext db = new SmashNetwork.DAL.SmashNetworkDBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Denied()
        {
            ViewBag.Message = "Access denied.";
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
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
                        return RedirectToAction("Index", "Articles");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect login.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User does not exist.");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                User userMatch = db.Users.FirstOrDefault(u => u.Username == user.Username);
                if (userMatch == null)
                {
                    PasswordManager pm = new PasswordManager(user.Username, user.Hash);
                    user.Salt = pm.salt.getSaltString();
                    db.Users.Add(user);
                    db.SaveChanges();
                    TempData["message"] = "Successfully added User.";
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "That Username is already taken!");
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
