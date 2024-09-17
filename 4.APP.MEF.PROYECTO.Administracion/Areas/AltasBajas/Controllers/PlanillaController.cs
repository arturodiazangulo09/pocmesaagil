using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Administracion;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.AltasBajas.Repositorio;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.Entity.Coordinador;
using APP.ADMINISTRAR.FAG.PAG.Response;
using APP.ADMINISTRAR.FAG.PAG.Core;
using MEF.PROYECTO.Utilitario;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.AltasBajas.Controllers
{
    public class PlanillaController : BaseController
    {
        //
        // GET: /AltasBajas/Planilla/

        public ActionResult Index()
        {
            List<Cls_Ent_Entidades> lista = null;
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            lista = Cls_Rule_Entidades.ListaEntidades(xx).FindAll(A => A.CANT_DEPENDENCIA.Equals(0));
            lista = lista.OrderBy(A => A.DESC_UNIDAD).ToList();
            lista.Insert(0, new Cls_Ent_Entidades() { ID_ENTIDAD = 0, DESC_UNIDAD = "---------- TODAS LAS ENTIDADES --------" });
            ViewBag.DcboEntidades = lista;
            return View();
        }
        public ActionResult ListaPeriodoPlanilla(Cls_Ent_Solicitud_Pago entidad)
        {
            List<Cls_Ent_Solicitud_Pago> lista;
            lista = new PlanillaRepositorio().ListaPeriodoPlanilla(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult UpdAsignarPlanilla(Cls_Ent_Solicitud_Pago entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud_Pago PreguntaRspta = null;
            PreguntaRspta = new PlanillaRepositorio().UpdAsignarPlanilla(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra =  Log.MensajeLogText(); //entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.success = true;
                { itemRespuesta.message = "La planilla se genero correctamente."; }
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult GenerarPlanilla()
        {
            return View();
        }
    }
}
