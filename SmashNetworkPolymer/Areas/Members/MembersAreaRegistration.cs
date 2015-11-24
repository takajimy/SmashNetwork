using System.Web.Mvc;

namespace SmashNetworkPolymer.Areas.Members
{
    public class MembersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Members";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Members_default",
                "{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new { controller = "(Members)" }
            );
        }
    }
}
