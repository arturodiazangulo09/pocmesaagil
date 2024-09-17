using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP.ADMINISTRAR.FAG.PAG.Response;
using APP.ADMINISTRAR.FAG.PAG.Core;
using System.Threading.Tasks;
using System.IO;
using MEF.PROYECTO.Entity.Administracion;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.AltasBajas.Repositorio;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.AltasBajas.Controllers
{
    public class PdtController : BaseController
    {
        //
        // GET: /AltasBajas/Pdt/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListaPlanillaPDT(Cls_Ent_Planilla_PDT entidad)
        {
            List<Cls_Ent_Planilla_PDT> lista;
            lista = new PdtRepositorio().ListaPlanillaPDT(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}
