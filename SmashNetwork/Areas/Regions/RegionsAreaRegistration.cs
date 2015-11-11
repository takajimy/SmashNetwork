using System.Web.Mvc;

namespace SmashNetwork.Areas.Regions
{
    public class RegionsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Regions";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Regions_default",
                "Regions/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
