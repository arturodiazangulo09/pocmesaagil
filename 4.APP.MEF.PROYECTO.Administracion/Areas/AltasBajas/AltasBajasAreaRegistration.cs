using System.Web.Mvc;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.AltasBajas
{
    public class AltasBajasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AltasBajas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AltasBajas_default",
                "AltasBajas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
