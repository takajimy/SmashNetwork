using System.Web.Mvc;

namespace SmashNetworkPolymer.Areas.Blogs
{
    public class BlogsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Blogs";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Blogs_default",
                "Blogs/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
