using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.BusinessLayer.Administracion;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Models;
using System.Web.Services;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Utilitario;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Globalization;
using System.Drawing.Imaging;
using MEF.PROYECTO.BusinessLayer.Contratos;
using MEF.PROYECTO.Entity.Contratos;
using System.Data;
using MEF.PROYECTO.BusinessLayer.CargaMasiva;
using APP.MEF.ADMINISTRAR.FAG.PAG.Response;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Carga.Repositorio;
using System.Diagnostics.Contracts;
using static MEF.PROYECTO.Utilitario.Constants.TipoDocumento;
using static OfficeOpenXml.ExcelErrorValue;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Carga.Controllers
{
    public class ContratoController : Controller
    {

        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario();
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario_result = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult OpenMantenimientoFag(int idRegistro, int idCarga)
        {
            ViewBag.ID_REGISTRO = idRegistro;
            Cls_Ent_Contratos modelo = new ContratoRepositorio().ObtenerContrato(idRegistro,idCarga).Data;
            using (UbigeoRepositorio repositorio = new UbigeoRepositorio())
            {
                modelo.ListaDpto = repositorio.ListaDepartamento().Select(x => new SelectListItem()
                {
                    Text = x.CNOMDEPARTAMENTO.Trim(),
                    Value = x.CCODDEPARTAMENTO.ToString()
                }).ToList();
                modelo.ListaDpto.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }

            return View("MantenimientoContratoFAG", modelo);
        }
        public ActionResult OpenMantenimientoPac(int idRegistro)
        {
            ViewBag.ID_REGISTRO = idRegistro;
            return View("MantenimientoContratoPAC");
        }

        public ActionResult OpenMantenimientoAdendas(int idCargaAdenda)
        {
            ViewBag.ID_CARGA_ADENDA = idCargaAdenda;
            Cls_Ent_Adendas modelo = new ContratoRepositorio().ObtenerAdendas(idCargaAdenda);
            return View("MantenimientoAdendas", modelo);
        }
        public ActionResult OpenAdendas(string CodContrato)
        {
            ViewBag.COD_CONTRATO = CodContrato;
            return View("Adendas");
        }
        public ActionResult MantenimientoAdendas(int idCargaAdenda)
        {
            ViewBag.ID_CARGA_ADENDA = idCargaAdenda;
            return View("MantenimientoAdendas");
        }
        public ActionResult ListaContratos(string numDocumento, string cargo, decimal idEntidad, string campos, string valores, int pagina, int nregistros)
        {
            var jsonResult = new JsonResult();
            campos += "FLG_ESTADO*";
            valores += "1,*";
            jsonResult.Data = new ContratoRepositorio().ListarContratos(numDocumento, cargo, idEntidad, campos, valores, pagina, nregistros);
            return jsonResult;
        }
        public ActionResult ListaAdendas(string codContrato, string campos, string valores, int pagina, int nregistros)
        {
            var jsonResult = new JsonResult();
            jsonResult.Data = new ContratoRepositorio().ListaAdendas(codContrato, campos, valores, pagina, nregistros);
            return jsonResult;
        }
        public ActionResult EditarCargaDetalle(int idContrato, Cls_Ent_Contratos contrato)
        {
            var jsonResult = new JsonResult();
            jsonResult.Data = new ContratoRepositorio().EditarContrato(idContrato, contrato);
            return jsonResult;
        }
        public ActionResult EliminarContrato(int idRegistro)
        {
            var jsonResult = new JsonResult();
            jsonResult.Data = new ContratoRepositorio().EliminarContrato(idRegistro); 
            return jsonResult;
        }

        public ActionResult EditarAdenda(int idAdenda, Cls_Ent_Adendas adenda)
        {
            var jsonResult = new JsonResult();
            jsonResult.Data = new ContratoRepositorio().EditarAdenda(idAdenda, adenda);
            return jsonResult;
        }
        public ActionResult EliminarAdendas(int idAdenda)
        {
            var jsonResult = new JsonResult();
            jsonResult.Data = new ContratoRepositorio().EliminarAdenda(idAdenda);
            return jsonResult;
        }
        public ActionResult ListarFiltroContrato()
        {
            ContratosUtil contratos = new ContratosUtil();
            DataTable listado = contratos.ListaContrato();

            var resultado = from fila in listado.AsEnumerable()
                            select new
                            {
                                ID = fila.Field<int>("ID"),
                                DESCRIPCION = fila.Field<string>("DESCRIPCION")
                            };

            string jsonResult = JsonConvert.SerializeObject(resultado, Formatting.Indented);
            return Content(jsonResult, "application/json");
        }
        public ActionResult ListaFiltroAdenda()
        {
            ContratosUtil contratos = new ContratosUtil();
            DataTable listado = contratos.ListaAdendas();
            var resultado = from fila in listado.AsEnumerable()
                            select new
                            {
                                ID = fila.Field<string>("ID"),
                                DESCRIPCION = fila.Field<string>("DESCRIPCION")
                            };
            string jsonResult = JsonConvert.SerializeObject(resultado, Formatting.Indented);
            return Content(jsonResult, "application/json");
        }

        public ActionResult ListaMaestra()
        {
            CargaInicial cargaInicial = new CargaInicial
            {
                Estado = new List<Catalogo>
                {
                    new Catalogo{ Id = Constants.Estados.Activo.Id, Descripcion = Constants.Estados.Activo.Descripcion },
                    new Catalogo{ Id = Constants.Estados.Inactivo.Id, Descripcion = string.Concat(Constants.Estados.Inactivo.Descripcion, "/", Constants.Estados.Baja.Descripcion) },
                    new Catalogo{ Id = Constants.Estados.Devolucion.Id, Descripcion = Constants.Estados.Devolucion.Descripcion }
                },
                SiNo = new List<Catalogo>
                {
                    new Catalogo{ Id = Constants.SiNo.Si, Descripcion = Constants.SiNo.Si },
                    new Catalogo{ Id = Constants.SiNo.No, Descripcion = Constants.SiNo.No }
                },
                Sexo = new List<Catalogo>
                {
                    new Catalogo { Id = Constants.Sexo.Masculino, Descripcion = Constants.Sexo.Masculino },
                    new Catalogo { Id = Constants.Sexo.Femenino, Descripcion = Constants.Sexo.Femenino }
                }
            };



            string jsonResult = JsonConvert.SerializeObject(cargaInicial, Formatting.Indented);
            return Content(jsonResult, "application/json");
        }

        public ActionResult Ubigeo(string region, string provincias)
        {
            //CargaInicial datos = new CargaInicial();
            List<Cls_Ent_Ubigeo> listaUbigeo = new UbigeoRepositorio().ListarUbigeo();

            //datos.Region = listaUbigeo.Select(x => new Catalogo { Id = x.CCODDEPARTAMENTO, Descripcion = x.CNOMDEPARTAMENTO }).ToList();
            //datos.Provincia = listaUbigeo.Select(x => new Catalogo { Id = x.CCODPROVINCIA, Descripcion = x.CNOMPROVINCIA }).ToList();
            //datos.Distrito = listaUbigeo.Select(x => new Catalogo { Id = x.CCODDISTRITO, Descripcion = x.CNOMDISTRITO }).ToList();

            //datos.Region = regionRepetida
            //                .GroupBy(x => x.Id)
            //                .Select(gpr => gpr.First())
            //                .ToList();
            //datos.Provincia = provinciaRepetida
            //                .GroupBy(x => x.Id)
            //                .Select(gpr => gpr.First())
            //                .ToList();
            //datos.Distrito = distritoRepetida
            //                .GroupBy(x => x.Id)
            //                .Select(gpr => gpr.First())
            //                .ToList();


            string jsonResult = JsonConvert.SerializeObject(listaUbigeo, Formatting.Indented);
            return Content(jsonResult, "application/json");
        }
        public JsonResult ListarProvinciaCbo(Cls_Ent_Ubigeo entidad)
        {
            IList<Cls_Ent_Ubigeo> cboProv = new UbigeoRepositorio().listaProvincias(entidad);
            cboProv.Insert(0, new Cls_Ent_Ubigeo() { CCODPROVINCIA = "", CNOMPROVINCIA = "--Seleccione--" });
            ViewBag.Prov = cboProv;
            return Json(cboProv, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarDistritoCbo(Cls_Ent_Ubigeo entidad)
        {
            IList<Cls_Ent_Ubigeo> cboDist = new UbigeoRepositorio().listaDistritos(entidad);
            cboDist.Insert(0, new Cls_Ent_Ubigeo() { CCODDISTRITO = "", CNOMDISTRITO = "--Seleccione--" });
            ViewBag.Dist = cboDist;
            return Json(cboDist, JsonRequestBehavior.AllowGet);
        }
        #region Pagos
        public ActionResult OpenPagos(string CodContrato)
        {
            ViewBag.COD_CONTRATO = CodContrato;
            return View("Pagos");
        }
        public ActionResult MantenimientoPagos()
        {
            return View("MantenimientoPagos");
        }
        public ActionResult ListaPagos(string codContrato, string periodo, string numDocumento, string rucCas, decimal idEntidad, string campos, string valores, int pagina, int nregistros)
        {
            var jsonResult = new JsonResult();
            campos += "FLG_ESTADO*";
            valores += "1,*";

            //var periodo = Request.QueryString["periodo"];
            //var codTrabajador = Request.QueryString["codTrabajador"];
            //var numDocumento = Request.QueryString["numDocumento"];
            //var rucCas = Request.QueryString["rucCas"];
            //decimal idEntidad = 0;
            //if (Request.QueryString["idEntidad"] != null)
            //{
            //    idEntidad = Convert.ToDecimal(Request.QueryString["idEntidad"].ToString());
            //}
         
            jsonResult.Data = new ContratoRepositorio().listarPagos(codContrato,periodo, numDocumento, rucCas, idEntidad, campos, valores, pagina, nregistros);
            return jsonResult;
        }
        public ActionResult EditarPago(Cls_Ent_Pagos pago)
        {
            var jsonResult = new JsonResult();
            jsonResult.Data = new ContratoRepositorio().EditarPago(pago);
            return jsonResult;
        }
        public ActionResult EliminarPago(int idPago)
        {
            var jsonResult = new JsonResult();
            jsonResult.Data = new ContratoRepositorio().EliminarPago(idPago);
            return jsonResult;
        }
        public ActionResult ObtenerPago(string idPago)
        {
            var json = new JsonResult();
            Cls_Ent_Pagos detalle = new ContratoRepositorio().ObtenerPagoxId(idPago).Data;

            json.Data = detalle;
            return json;
        }
        //Lista las Entidades para Combo
        [HttpPost]
        public ActionResult GetEntidades()
        {
            string user = "";
            if (Session["Personal"] != null)
            {
                usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
                user = usuario.COD_USUARIO;
            }
            else
            {
                var cook_Token = HttpContext.Request.Cookies["MEF-TOKEN-MIGUEL"];
                if (cook_Token != null)
                {
                    int ID_SISTEMA = int.Parse(ConfigurationManager.AppSettings["IdAplicacion_Seguridad"]);
                    using (UsuarioReconectar XX = new UsuarioReconectar())
                    {
                        usuario_result = XX.ConsultaPUsuario(cook_Token.Value, ID_SISTEMA);
                    }
                    Session["Personal"] = usuario_result.Usuario;
                    user = usuario_result.Usuario.COD_USUARIO;
                }
                else
                {
                    Session["Personal"] = null;
                    Response.Redirect("../Seguridad/AccesoDenegado");
                }
            }
            var lista = new ContratoRepositorio().listarEntidades().ToList().OrderBy(a => a.ACRONIMO);
            List<Catalogo> lstCarga = lista.Select(x => new Catalogo() { Id = x.ID_ENTIDAD.ToString(), Descripcion = x.DESC_ENTIDAD }).ToList();
            var json = new JsonResult
            {
                Data = JsonConvert.SerializeObject(lstCarga)
            };
            return json;
        }
        #endregion
    }
}
