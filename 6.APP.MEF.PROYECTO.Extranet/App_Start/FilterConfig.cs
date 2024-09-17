using System.Web.Mvc;

namespace APP.MEF.EXTRANET.FAG.PAG
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}