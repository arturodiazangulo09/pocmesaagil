using System.Web.Mvc;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Carga
{
    public class CargaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Carga";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Carga_default",
                "Carga/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}