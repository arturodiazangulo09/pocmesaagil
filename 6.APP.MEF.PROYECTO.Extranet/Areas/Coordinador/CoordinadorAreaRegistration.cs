using System.Web.Mvc;

namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador
{
    public class CoordinadorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Coordinador";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Coordinador_default",
                "Coordinador/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
