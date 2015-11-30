using System.Web.Mvc;

namespace SmashNetwork.Areas.Develop
{
    public class DevelopAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Develop";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Develop_default",
                "{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new { controller = "(Develop)" }
            );
        }
    }
}
