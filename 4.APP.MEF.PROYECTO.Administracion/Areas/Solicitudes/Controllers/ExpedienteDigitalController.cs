using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Administracion;
using APP.ADMINISTRAR.FAG.PAG.Response;
using APP.ADMINISTRAR.FAG.PAG.Core;
using System.Threading.Tasks;
using System.IO;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Configuration;
using MEF.PROYECTO.Utilitario;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes.Repositorio;
using MEF.PROYECTO.BusinessLayer.Administracion;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes.Controllers
{
    public class ExpedienteDigitalController : Controller
    {
        //
        // GET: /Solicitudes/ExpedienteDigital/

        public ActionResult Index()
        {
            List<Cls_Ent_Entidades> lista = null;
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            lista = Cls_Rule_Entidades.ListaEntidades(xx).FindAll(A => A.CANT_DEPENDENCIA.Equals(0));
            lista = lista.OrderBy(A => A.DESC_UNIDAD).ToList();
            ViewBag.DcboEntidades = lista;
            return View();
        }
        public ActionResult ListaDocumentosContrato(Cls_Ent_Documentos_Contrato entidad)
        {
            List<Cls_Ent_Documentos_Contrato> lista;
            lista = new ExpedienteDigitalRepositorio().ListaDocumentosContrato(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}
