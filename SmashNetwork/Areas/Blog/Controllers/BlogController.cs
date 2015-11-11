using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmashNetwork.Areas.Blog.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/Blog/

        public ActionResult Index()
        {
            return View();
        }

    }
}
