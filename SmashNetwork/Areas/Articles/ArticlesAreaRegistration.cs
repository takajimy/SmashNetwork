using System.Web.Mvc;

namespace SmashNetwork.Areas.Articles
{
    public class ArticlesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Articles";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Articles_default",
                "{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new { controller = "(Articles)" }
            );
        }
    }
}
