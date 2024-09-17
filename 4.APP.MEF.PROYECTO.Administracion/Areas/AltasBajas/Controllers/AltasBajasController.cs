using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.BusinessLayer.Administracion;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Models;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.AltasBajas.Controllers
{
    public class AltasBajasController : Controller
    {
        //
        // GET: /AltasBajas/AltasBajas/
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario();
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario_result = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();

        public ActionResult Index()
        {
            List<Cls_Ent_Entidades> lista = null;
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            lista = Cls_Rule_Entidades.ListaEntidades(xx).FindAll(A => A.CANT_DEPENDENCIA.Equals(0));
            lista = lista.OrderBy(A => A.DESC_UNIDAD).ToList();
            ViewBag.DcboEntidades = lista;
            List<Cls_Ent_Entidades> lista_ = lista;
            lista_.Insert(0, new Cls_Ent_Entidades() { ID_ENTIDAD = 0, DESC_UNIDAD = "---------- TODAS LAS ENTIDADES --------" });
            ViewBag.DcboEntidades_ = lista_;
            return View();
        }
        public ActionResult VerReporte_Entidad(int ID_ENTIDAD, string TIPO_CONSULTOR, string FECHA, string TIPO_DESCARGA, string ESTADO_SOLICITUD, string TIPO, string FECHA_INICIO)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            modelo.ID_ENTIDAD= ID_ENTIDAD;
            modelo.TIPO_CONSULTOR = TIPO_CONSULTOR;
            modelo.FECHA = FECHA;
            modelo.TIPO_DESCARGA = TIPO_DESCARGA;
            modelo.ESTADO_SOLICITUD = ESTADO_SOLICITUD;
            modelo.TIPO = TIPO;
            modelo.FECHA_INICIO = FECHA_INICIO;
            return View(modelo);

        }
        public ActionResult VerReporte_Contancia_Entidad(int ID_ENTIDAD, string TIPO_CONSULTOR,string DOCUMENTO, string ANIO, string TIPO_REPORTE)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            modelo.TIPO_CONSULTOR = TIPO_CONSULTOR;
            modelo.DOCUMENTO = DOCUMENTO;
            modelo.ANIO = ANIO;
            modelo.TIPO_REPORTE = TIPO_REPORTE;
            return View(modelo);

        }
        public ActionResult VerReporte_Planilla(int ID_ENTIDAD, string TIPO_CONSULTOR, string FECHA,string TIPO_REPORTE,string TIPO_DESCARGA)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            modelo.TIPO_CONSULTOR = TIPO_CONSULTOR;
            modelo.FECHA = FECHA;
            modelo.TIPO_REPORTE = TIPO_REPORTE;
            modelo.TIPO_DESCARGA = TIPO_DESCARGA;
            return View(modelo);

        }
        public ActionResult VerReporte_Honorario(string DOCUMENTO, string TIPO_CONSULTOR,  string ANIO, string TIPO_REPORTE, string TIPO_DESCARGA)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            modelo.TIPO_CONSULTOR = TIPO_CONSULTOR;
            modelo.DOCUMENTO = DOCUMENTO;
            modelo.ANIO = ANIO;
            modelo.TIPO_REPORTE = TIPO_REPORTE;
            modelo.TIPO_DESCARGA = TIPO_DESCARGA;
            return View(modelo);
        }
    }
}
