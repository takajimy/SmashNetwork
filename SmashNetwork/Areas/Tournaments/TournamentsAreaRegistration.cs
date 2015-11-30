using System.Web.Mvc;

namespace SmashNetwork.Areas.Tournaments
{
    public class TournamentsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Tournaments";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Tournaments_default",
                "{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new { controller = "(Tournaments)" }
            );
        }
    }
}
