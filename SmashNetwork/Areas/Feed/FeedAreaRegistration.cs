using System.Web.Mvc;

namespace SmashNetwork.Areas.Feed
{
    public class FeedAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Feed";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Feed_default",
                "Feed/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
