using System.Web.Mvc;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes
{
    public class SolicitudesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Solicitudes";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Solicitudes_default",
                "Solicitudes/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
