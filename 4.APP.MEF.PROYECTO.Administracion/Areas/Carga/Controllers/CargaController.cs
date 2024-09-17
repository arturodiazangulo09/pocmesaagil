using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Administracion;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Models;
using MEF.PROYECTO.Utilitario;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using APP.ADMINISTRAR.FAG.PAG.Core;
using MEF.PROYECTO.BusinessLayer.CargaMasiva;
using MEF.PROYECTO.Entity.CargaMasiva;
using System.Data;
using OfficeOpenXml;
using APP.MEF.ADMINISTRAR.FAG.PAG.Response;
using System.Diagnostics.Contracts;
using MEF.PROYECTO.BusinessLayer.Contratos;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Carga.Repositorio;
using System.Web.Script.Serialization;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Repositorio;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Carga.Controllers
{
    public class CargaController : BaseController
    {
        //
        // GET: /Carga/
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario();
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario_result = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();

        public ActionResult Index()
        {
            Cls_Ent_Carga xx = new Cls_Ent_Carga();
            var usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
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
            IList<Cls_Ent_Carga> lista = new CargaRepositorio().ListaCarga(user);

            if (lista != null) {
                lista = lista.OrderBy(A => A.FEC_REGISTRO).ToList();
                ViewBag.DcboEntidades = lista;
                lista.Insert(0, new Cls_Ent_Carga() { ID_CARGA = 0, DES_CARGA = "---------- SELECCIONE  --------" });
            }
           ViewBag.DcboEntidades_ = lista;
            return View();
        }

        //Listar Tipos de Formato para Combo
        [HttpPost]
        public ActionResult GetTipoFormato()
        {
            var jsonResult = new JsonResult();
            List<Catalogo> tipoFormato = new List<Catalogo>
            {
                new Catalogo { Id = Constants.TipoDocumento.FAG.Id, Descripcion = Constants.TipoDocumento.FAG.Descripcion },
                new Catalogo { Id = Constants.TipoDocumento.PAC.Id, Descripcion = Constants.TipoDocumento.PAC.Descripcion },
                new Catalogo { Id = Constants.TipoDocumento.Adendas.Id, Descripcion = Constants.TipoDocumento.Adendas.Descripcion },
                new Catalogo { Id = Constants.TipoDocumento.SAL.Id, Descripcion = Constants.TipoDocumento.SAL.Descripcion }
            };

            jsonResult.Data = JsonConvert.SerializeObject(tipoFormato);
            return jsonResult;
        }

        //Lista las Cargas para Combo
        [HttpPost]
        public ActionResult GetCargas()
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
            Cls_Rule_CargaMasiva servicio = new Cls_Rule_CargaMasiva();
            var lista = servicio.ListaCarga(user).ToList().OrderBy(a => a.FEC_REGISTRO);
            List<Catalogo> lstCarga = lista.Select(x => new Catalogo() { Id = x.ID_CARGA.ToString(), Descripcion = x.DES_CARGA }).ToList();
            var json = new JsonResult
            {
                Data = JsonConvert.SerializeObject(lstCarga)
            };
            return json;
        }

        
        public ActionResult VerReporte_Entidad(int ID_ENTIDAD, string TIPO_CONSULTOR, string FECHA, string TIPO_DESCARGA, string ESTADO_SOLICITUD, string TIPO)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            modelo.ID_ENTIDAD= ID_ENTIDAD;
            modelo.TIPO_CONSULTOR = TIPO_CONSULTOR;
            modelo.FECHA = FECHA;
            modelo.TIPO_DESCARGA = TIPO_DESCARGA;
            modelo.ESTADO_SOLICITUD = ESTADO_SOLICITUD;
            modelo.TIPO = TIPO;
            return View(modelo);

        }

        //Descarga Formato 
        public ActionResult DescargaFormato(string tipoFormato)
        {
            string nombreArchivo= string.Empty;

            switch (tipoFormato)
            {
                case Constants.TipoDocumento.FAG.Id:
                    nombreArchivo = "FORMATO_FAG.xlsx";
                    break;
                case Constants.TipoDocumento.PAC.Id:
                    nombreArchivo = "FORMATO_PAC.xlsx";
                    break;
                case Constants.TipoDocumento.Adendas.Id:
                    nombreArchivo = "FORMATO_DET.xlsx";
                    break;
                case Constants.TipoDocumento.SAL.Id:
                    nombreArchivo = "FORMATO_" + Constants.TipoDocumento.SAL.Id + ".xlsx";
                    break;
            }

            string rutaArchivo = Server.MapPath("~/Recursos/" + nombreArchivo);

            byte[] fileBytes = System.IO.File.ReadAllBytes(rutaArchivo);
            MemoryStream ms = new MemoryStream(fileBytes, 0, 0, true, true);
            Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo);
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();
            return new FileStreamResult(Response.OutputStream, "application/xlxs");

        }

        //Carga el listado de campos segun el Formato
        [HttpPost]
        public ActionResult LoadCamposFormatos(string tipoDocumento)
        {
            var jsonResult = new JsonResult();
            switch (tipoDocumento)
            {
                case Constants.TipoDocumento.FAG.Id:
                    jsonResult.Data = JsonConvert.SerializeObject(new ExcelCargaUtil().ListaFag());
                    break;
                case Constants.TipoDocumento.PAC.Id:
                    jsonResult.Data = JsonConvert.SerializeObject(new ExcelCargaUtil().ListaPac());
                    break;
                case Constants.TipoDocumento.Adendas.Id:
                    jsonResult.Data = JsonConvert.SerializeObject(new ExcelCargaUtil().ListaAdendas());
                    break;
                case Constants.TipoDocumento.SAL.Id:
                    jsonResult.Data = JsonConvert.SerializeObject(new ExcelCargaUtil().ListaPagos());
                    break;
            }
            
            return jsonResult;
        }

        //Carga las Adendas por IdRegistro
        [HttpPost]
        public ActionResult LoadAdendas(int IdRegistro)
        {
            Cls_Rule_CargaMasiva servicio = new Cls_Rule_CargaMasiva();
            var jsonResult = new JsonResult();
            jsonResult.Data = JsonConvert.SerializeObject(new CargaRepositorio().ListaAdendasxContrato(IdRegistro));
            return jsonResult;
        }


        [HttpPost]
        public ActionResult LoadValoresdeContratos(string tipoDocumento)
        {
            var jsonResult = new JsonResult();
            switch (tipoDocumento)
            {
                case Constants.TipoDocumento.FAG.Id:
                    jsonResult.Data = JsonConvert.SerializeObject(new ExcelCargaUtil().ListaFag());
                    break;
                case Constants.TipoDocumento.PAC.Id:
                    jsonResult.Data = JsonConvert.SerializeObject(new ExcelCargaUtil().ListaPac());
                    break;
                case Constants.TipoDocumento.Adendas.Id:
                    jsonResult.Data = JsonConvert.SerializeObject(new ExcelCargaUtil().ListaAdendas());
                    break;
            }

            return jsonResult;
        }

        // Cargar Archivo para validar, y enviar a tablas Temporales
        public ActionResult CargarArchivo(HttpPostedFileBase file, string tipoDocumento)
        {
            var jsonResult = new JsonResult();
            List<Respuesta> listaErroresP = null;
            RespuestaOperacion respuestaOpe = new RespuestaOperacion();
            Cls_Rule_CargaMasiva oCarga = new Cls_Rule_CargaMasiva();

            if (file != null && file.ContentLength > 0)
            {
                Stream archivo = file.InputStream;
                ExcelCargaUtil excel = new ExcelCargaUtil();
                Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();

                List<Cls_Ent_Entidades> listaEntidades = new CargaRepositorio().ListarEntidades();

                List<Cls_Ent_Ubigeo> listaUbigeo = new UbigeoRepositorio().ListarUbigeo();
               switch (tipoDocumento)
                {
                    case Constants.TipoDocumento.FAG.Id:
                        listaErroresP = excel.ValidacionCargaMasivaDataFag(archivo, listaEntidades, listaUbigeo);
                        break;
                    case Constants.TipoDocumento.PAC.Id:
                        listaErroresP = excel.ValidacionCargaMasivaDataPAC(archivo, listaEntidades, listaUbigeo);
                        break;
                    case Constants.TipoDocumento.Adendas.Id:
                        listaErroresP = excel.ValidacionAdendas(archivo);
                        break;
                    case Constants.TipoDocumento.SAL.Id:
                        listaErroresP = excel.ValidacionCargaMasivaDataPag(archivo, listaEntidades);
                        break;
                }

                if (listaErroresP.Count == 0)
                {
                    DataTable lista = null;
                    string tipo_formato = "";

                    switch (tipoDocumento)
                    {
                        case Constants.TipoDocumento.FAG.Id:
                            lista = excel.ReadExcelDataFag(archivo);
                            tipo_formato = Constants.TipoFormato.Cabecera;
                            break;
                        case Constants.TipoDocumento.PAC.Id:
                            lista = excel.ReadExcelDataPac(archivo);
                            tipo_formato = Constants.TipoFormato.Cabecera;
                            break;
                        case Constants.TipoDocumento.Adendas.Id:
                            lista = excel.ReadExcelDataAdendas(archivo);
                            tipo_formato = Constants.TipoFormato.Detalle;
                            break;
                        case Constants.TipoDocumento.SAL.Id:
                            lista = excel.ReadExcelDataPag(archivo);
                            tipo_formato = Constants.TipoFormato.Detalle;
                            break;
                    }
                    var usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
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

                    Cls_Ent_Carga carga = new Cls_Ent_Carga
                    {
                        ID_CARGA = 0,
                        NRO_REGISTROS = lista.Rows.Count,
                        FLG_ESTADO = "1",
                        FLG_PROCESADO="0",
                        FEC_REGISTRO = DateTime.Now,
                        TIPO_DOC = tipoDocumento,
                        TIPO_FORMATO = tipo_formato,
                        USU_INGRESO = user
                    };

                    service.MantenimientoCarga(carga);

                    if(tipoDocumento == Constants.TipoDocumento.Adendas.Id)
                    {
                        if (carga.Result)
                        {
                            DateTime ahora = DateTime.Now;
                            Random random = new Random();

                            for (var i = 0; i < lista.Rows.Count; i++)
                            {
                                string val = ahora.Year.ToString() + ahora.Month.ToString() + ahora.Day.ToString() + ahora.Hour.ToString() + ahora.Minute.ToString() + "_" + random.Next(1, 1000).ToString() + "_" + i ;
                                lista.Rows[i]["ID_CARGA_DETALLE"] = val;
                                lista.Rows[i]["ID_CARGA"] = carga.ID_CARGA;
                            }
                            Respuesta rpta = new CargaRepositorio().MantenimientoCargaDetalle(lista);
                            listaErroresP.Add(rpta);
                            respuestaOpe.Status = !rpta.IsError;
                            respuestaOpe.ListaErrores = listaErroresP;
                        }
                        else respuestaOpe.Status = carga.Result;
                    }
                    else if (tipoDocumento == Constants.TipoDocumento.FAG.Id || tipoDocumento == Constants.TipoDocumento.PAC.Id)
                    {
                        if (carga.Result)
                        {
                            for (var i = 0; i < lista.Rows.Count; i++)
                            {
                                lista.Rows[i]["ID_REGISTRO"] = Int32.Parse(carga.ID_CARGA.ToString() + i);
                                lista.Rows[i]["ID_CARGA"] = carga.ID_CARGA;
                            }

                            Respuesta rpta = service.MantenimientoCargaCabecera(lista);
                            listaErroresP.Add(rpta);
                            respuestaOpe.Status = !rpta.IsError;
                            respuestaOpe.ListaErrores = listaErroresP;
                        }
                        else
                            respuestaOpe.Status = carga.Result;
                    }
                    else if (tipoDocumento == Constants.TipoDocumento.SAL.Id)
                    {
                        if (carga.Result)
                        {
                            for (var i = 0; i < lista.Rows.Count; i++)
                            {
                                var periodo = Request.QueryString["periodo"];
                                lista.Rows[i]["ID_REGISTRO"] = Int32.Parse(carga.ID_CARGA.ToString() + i);
                                lista.Rows[i]["ID_CARGA"] = carga.ID_CARGA;
                                lista.Rows[i]["PERIODO"] = periodo;
                                lista.Rows[i]["FLG_ESTADO"] = 1;
                            }

                            Respuesta rpta = service.MantenimientoPago(lista);
                            listaErroresP.Add(rpta);
                            respuestaOpe.Status = !rpta.IsError;
                            respuestaOpe.ListaErrores = listaErroresP;
                        }
                        else
                            respuestaOpe.Status = carga.Result;
                    }
                }
                else
                {
                    respuestaOpe.Status = false;
                    respuestaOpe.ListaErrores = listaErroresP;
                }
            }

            ////Se crear una referencia a JavaScriptSerializer
            //var serializer = new JavaScriptSerializer();
            ////Se cambia el Length directo a nuestra referencia
            //serializer.MaxJsonLength = 500000000;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;

            jsonResult.Data = JsonConvert.SerializeObject(respuestaOpe, Formatting.Indented);
            return jsonResult;
        }

        public async Task<ActionResult> ExportarExcelErrores(String errores)
        {
            List<Respuesta> data;
            data = JsonConvert.DeserializeObject<List<Respuesta>>(errores);
            try
            {
                int row = 0;
                using (ExcelPackage package = new ExcelPackage())
                {
                    var ws1 = package.Workbook.Worksheets.Add("Errores");
                    row += 1;
                    int fila = 1;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "FILA", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "COLUMNA", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 50; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "MENSAJE ERROR", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 100; fila++;
                    ws1.Row(row).Height = 30;
                    if (data.Count > 0)
                    {
                        foreach (var item in data)
                        {
                            row++;
                            int columna = 1;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NroItem, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.Campo, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.Mensaje, row, columna, 0, 0, "C"); columna++;
                        }
                    }
                    using (var stream = new System.IO.MemoryStream(package.GetAsByteArray()))
                    {
                        byte[] buffer = new byte[stream.Length];
                        var cd = new System.Net.Mime.ContentDisposition
                        {
                            FileName = "ListaErrores" + ".xlsx",
                            Inline = false,
                        };

                        /* Response.AddHeader("content-disposition", "attachment;filename=" + cd.FileName);
                         Response.Buffer = true;
                         Response.Clear();
                         //Response.OutputStream.Write(stream.GetBuffer(), 0, stream.GetBuffer().Length);
                         Response.OutputStream.Flush();
                         Response.End();
                         return new FileStreamResult(Response.OutputStream, "application/xlxs");*/
                        Response.AppendHeader("Content-Disposition", cd.ToString());
                        return await Task.FromResult(File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
                       
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public ActionResult ProcesoCarga()
        {
            Cls_Ent_Entidades modelo = new Cls_Ent_Entidades();
            return View(modelo);
        }

        public ActionResult MantenimientoCarga() {
            return View();
        }

        public ActionResult ActualizarCargaCabecera(Cls_Ent_Carga_Cabecera contrato)
        {
            Respuesta respuesta = new CargaRepositorio().ActualizarCargaCabecera(contrato);
            var jsonResult = Json(respuesta, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        public ActionResult ListaContratosCabDet(int ID_CARGA,string TIPO_DOC)
        {  
            Cls_Ent_Carga entidad=new Cls_Ent_Carga();
            entidad.ID_CARGA = ID_CARGA;
            entidad.TIPO_DOC = TIPO_DOC;

            if (TIPO_DOC == "DET")
            {
                return ListaContratosDet(entidad);
                
            }
            else if (TIPO_DOC == Constants.TipoDocumento.SAL.Id)
            {
                return ListaContratosPago(entidad);
            }
            else {
                return ListaContratosCab(entidad);
            }

            return null;
        }

        public ActionResult ListaCargaHistorica()
        {
            List<Cls_Ent_Carga> lista = new List<Cls_Ent_Carga> ();
            var usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
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
            Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
            lista = service.ListaCargaHistorica(user);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
           
        }

        public ActionResult ListaContratosCab(Cls_Ent_Carga entidad) {

            IList<Cls_Ent_Carga_Cabecera> lista = new CargaRepositorio().ListaContratosCab(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ListaContratosDet(Cls_Ent_Carga entidad)
        {
            IList<Cls_Ent_Carga_Detalle> listaAdendas = new CargaRepositorio().ListaContratosDet(entidad);
            var jsonResult = Json(listaAdendas, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ListaContratosPago(Cls_Ent_Carga entidad)
        {

            IList<Cls_Ent_Carga_Pago> lista = new CargaRepositorio().ListaContratosPago(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        #region Cargas Pendientes

        /* Procesar Carga */
        public ActionResult ProcesarCarga(int idCarga)
        {
            var usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
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
            bool rpta = new CargaRepositorio().ProcesarCarga(idCarga, usuario.COD_USUARIO);
            JsonResult result = new JsonResult();
            result.Data = rpta;

            return result;
        }
        /* Descargar Carga */
        public async Task<ActionResult> DescargarCarga(int idCarga)
        {
            ExcelCargaUtil excelCargaUtil = new ExcelCargaUtil();
            IList<Cls_Ent_Carga> carga = new CargaRepositorio().ListaCargaCompleta(idCarga);

            MemoryStream excel = excelCargaUtil.ExportarCarga(carga.First());

            byte[] buffer = new byte[excel.Length];
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "CARGA_" + idCarga.ToString() + ".xlsx",
                Inline = false
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());
            return await Task.FromResult(File(excel.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
        }

        /* Eliminar Carga */
        [HttpPost]
        public ActionResult EliminarCarga(int idCarga)
        {
            var usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
            bool rpta = new CargaRepositorio().EliminarCarga(idCarga, usuario.COD_USUARIO);
            JsonResult json = new JsonResult();
            json.Data = rpta;
            return json;
        }

        #endregion Cargas Pendientes


        
        public ActionResult VerAdenda(int IdRegistro)
        {
            ViewBag.ID_REGISTRO = IdRegistro;
            return View();
        }

        #region MantenimientoContratos

        /* Eliminar Carga */
        [HttpPost]
        public ActionResult EliminarContratos(int idRegistro)
        {
            bool rpta = new CargaRepositorio().EliminarContrato(idRegistro);
            JsonResult json = new JsonResult();
            json.Data = rpta;
            return json;
        }

        public ActionResult MantenimientoFAG(int IdRegistro)
        {
            ViewBag.ID_REGISTRO = IdRegistro;
            Cls_Ent_Carga_Cabecera modelo = new Cls_Ent_Carga_Cabecera();
            Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
            modelo = service.ObtenerCabeceraContratos(IdRegistro);

            Cls_Rule_Contratos service2 = new Cls_Rule_Contratos();
           
            using (UbigeoRepositorio repositorio = new UbigeoRepositorio())
            {
                modelo.ListaDpto = repositorio.ListaDepartamento().Select(x => new SelectListItem()
                {
                    Text = x.CNOMDEPARTAMENTO.Trim(),
                    Value = x.CNOMDEPARTAMENTO.Trim()
                }).ToList();
                modelo.ListaDpto.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }
            return View(modelo);
        }

        public ActionResult MantenimientoPAC(int IdRegistro)
        {
            ViewBag.ID_REGISTRO = IdRegistro;
            Cls_Ent_Carga_Cabecera modelo = new Cls_Ent_Carga_Cabecera();
            
            Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
            modelo = service.ObtenerCabeceraContratos(IdRegistro);
            return View(modelo);
        }
        public JsonResult ListarProvinciaCbo(Cls_Ent_Ubigeo entidad)
        {
            IList<Cls_Ent_Ubigeo> cboProv = new UbigeoRepositorio().Carga_listaProvincias(entidad);
            cboProv.Insert(0, new Cls_Ent_Ubigeo() { CCODPROVINCIA = "", CNOMPROVINCIA = "--Seleccione--" });
            ViewBag.Prov = cboProv;
            return Json(cboProv, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarDistritoCbo(Cls_Ent_Ubigeo entidad)
         {
            IList<Cls_Ent_Ubigeo> cboDist = new UbigeoRepositorio().Carga_listaDistritos(entidad);
            cboDist.Insert(0, new Cls_Ent_Ubigeo() { CCODDISTRITO = "", CNOMDISTRITO = "--Seleccione--" });
            ViewBag.Dist = cboDist;
            return Json(cboDist, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult MantenimientoSAL(int IdRegistro)
        //{
        //    ViewBag.ID_REGISTRO = IdRegistro;
        //    Cls_Ent_Carga_Cabecera modelo = new Cls_Ent_Carga_Cabecera();
        //    Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
        //    modelo = service.ObtenerCabeceraContratos(IdRegistro);
        //    return View(modelo);
        //}
        #endregion

        #region Mantenimiento CargasDetalle

        public ActionResult MantenimientoDetalle()
        {
            return View("MantenimientoDetalle");
        }
        public ActionResult EliminarCargaDetalle(string idCargaDetalle)
        {
            var service = new Cls_Rule_CargaMasiva();
            var json = new JsonResult();

            bool rpta = service.EliminarCargaDetalle(idCargaDetalle);
            
            json.Data = rpta;

            return json;
        }
        public ActionResult ObtenerCargaDetalle(string idCargaDetalle)
        {
            var service = new Cls_Rule_CargaMasiva();
            var json = new JsonResult();

            Cls_Ent_Carga_Detalle detalle = service.ObtenerCargaDetallexId(idCargaDetalle);

            json.Data = detalle;
            return json;
        }
        
        [HttpPost]
        public ActionResult EditarCargaDetalle(Cls_Ent_Carga_Detalle detalle)
        {
            var service = new Cls_Rule_CargaMasiva();
            var json = new JsonResult();

            bool rpta = service.EditarCargaDetalle(detalle);

            json.Data = rpta;
            return json;
        }
        #endregion

        #region Pagos
        public ActionResult ProcesoPago()
        {
            Cls_Ent_Entidades modelo = new Cls_Ent_Entidades();
            return View(modelo);
        }
        public ActionResult MantenimientoSAL()
        {
            return View("MantenimientoSAL");
        }
        public ActionResult EliminarCargaSalario(string idCargaSalario)
        {
            var service = new Cls_Rule_CargaMasiva();
            var json = new JsonResult();

            bool rpta = service.EliminarCargaSalario(idCargaSalario);

            json.Data = rpta;

            return json;
        }
        public ActionResult ObtenerCargaSalario(string idCargaSalario)
        {
            var service = new Cls_Rule_CargaMasiva();
            var json = new JsonResult();

            Cls_Ent_Carga_Pago detalle = service.ObtenerCargaSalarioxId(idCargaSalario);

            json.Data = detalle;
            return json;
        }

        [HttpPost]
        public ActionResult EditarCargaSalario(Cls_Ent_Carga_Pago detalle)
        {
            var service = new Cls_Rule_CargaMasiva();
            var json = new JsonResult();

            bool rpta = service.EditarCargaSalario(detalle);

            json.Data = rpta;
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

            Cls_Rule_CargaMasiva servicio = new Cls_Rule_CargaMasiva();
            var lista = servicio.ListarEntidades().ToList().OrderBy(a => a.ACRONIMO);
            List<Catalogo> lstCarga = lista.Select(x => new Catalogo() { Id = x.ACRONIMO.ToString().Trim(), Descripcion = x.DESC_ENTIDAD }).ToList();
            var json = new JsonResult
            {
                Data = JsonConvert.SerializeObject(lstCarga)
            };
            return json;
        }
        #endregion

    }
}
