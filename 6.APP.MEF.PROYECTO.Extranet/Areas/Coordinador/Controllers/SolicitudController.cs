using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.Personal;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using MEF.PROYECTO.Utilitario;
using APP.MEF.EXTRANET.FAG.PAG.Core;
using APP.MEF.EXTRANET.FAG.PAG.Response;
using Newtonsoft.Json;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Models;
using System.IO;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Net.Mime;
using System.Globalization;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Reportes;
using System.Net;

namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Controllers
{
    public class SolicitudController : BaseController
    {
        //
        // GET: /Coordinador/Solicitud/
        Cls_Ent_Coordinador UsuarioSistemaSesion = new Cls_Ent_Coordinador();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NuevoEditaSolicitud(int ID_SOLICITUD, string PROCESO, int ID_ENTIDAD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
            List<Cls_Ent_Entidades> Lista_Entidad = null;
            Cls_Ent_Entidades xxx = new Cls_Ent_Entidades();
            Lista_Entidad = new SolicitudRepositorio().ListaEntidades(xxx).FindAll(A => A.ID_ENTIDAD == ID_ENTIDAD);
            ViewBag.DcboEntidades = Lista_Entidad;
            modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString();
            IList<Cls_Ent_Puesto> lista_Puesto;
            Cls_Ent_Puesto xx = new Cls_Ent_Puesto();
            lista_Puesto = new CoordinadorRepositorio().ListaPuestos(xx).FindAll(A => A.ID_ENTIDAD == ID_ENTIDAD && A.FLG_ESTADO == 1);
            lista_Puesto.Insert(0, new Cls_Ent_Puesto() { ID_PUESTO = 0, DES_PUESTO = "--Seleccione--" });
            ViewBag.DcboPuestos = lista_Puesto;
            modelo.TIPO_PROCESO = "P";
            ViewBag.ListaServicio = JsonConvert.SerializeObject(new List<Cls_Ent_Funciones_Solicitud>());
            ViewBag.ListaAcademica = JsonConvert.SerializeObject(new List<Cls_Ent_Academico>());
            ViewBag.ListaCursos = JsonConvert.SerializeObject(new List<Cls_Ent_Curso>());
            ViewBag.ListaConocimientos = JsonConvert.SerializeObject(new List<Cls_Ent_Conocimientos>());
            ViewBag.ListaActividad = JsonConvert.SerializeObject(new List<Cls_Ent_Actividad>());
            ViewBag.ListaActividadOtro = JsonConvert.SerializeObject(new List<Cls_Ent_Actividad_Otro>());
            ViewBag.ListaExperiencia = JsonConvert.SerializeObject(new List<Cls_Ent_Experiencia>());
            ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(new List<Cls_Ent_Renumeracion>());
            if (ID_SOLICITUD == 0)
            {
                modelo.FLG_VERSION = "2";
                modelo.FLG_PROCESO = "I";
            }
            else {
                var lista = new SolicitudRepositorio().ListaSolicitudes(new Cls_Ent_Solicitud { ID_SOLICITUD = ID_SOLICITUD }).First();
                modelo = lista;
                modelo.FLG_PROCESO = "U";
                modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString();
                List<Cls_Ent_Funciones_Solicitud> X;
                Cls_Ent_Funciones_Solicitud EX = new Cls_Ent_Funciones_Solicitud();
                EX.ID_SOLICITUD = ID_SOLICITUD;
                X = new SolicitudRepositorio().ListaServicio(EX);
                if (X.Count > 0)
                {
                    ViewBag.ListaServicio = JsonConvert.SerializeObject(X);
                }
                List<Cls_Ent_Experiencia> XX;
                Cls_Ent_Experiencia EXX = new Cls_Ent_Experiencia();
                EXX.ID_SOLICITUD = ID_SOLICITUD;
                XX = new SolicitudRepositorio().ListaExperiencia(EXX);
                if (XX.Count > 0)
                {
                    ViewBag.ListaExperiencia = JsonConvert.SerializeObject(XX);
                }
                List<Cls_Ent_Academico> XXX;
                Cls_Ent_Academico EXXX = new Cls_Ent_Academico();
                EXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXX = new SolicitudRepositorio().ListaAcademica(EXXX);
                if (XXX.Count > 0)
                {
                    ViewBag.ListaAcademica = JsonConvert.SerializeObject(XXX);
                }
                List<Cls_Ent_Curso> XXXX;
                Cls_Ent_Curso EXXXX = new Cls_Ent_Curso();
                EXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXX = new SolicitudRepositorio().ListaCursos(EXXXX);
                if (XXXX.Count > 0)
                {
                    ViewBag.ListaCursos = JsonConvert.SerializeObject(XXXX);
                }
                List<Cls_Ent_Conocimientos> XXXXX;
                Cls_Ent_Conocimientos EXXXXX = new Cls_Ent_Conocimientos();
                EXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXX = new SolicitudRepositorio().ListaConocimientos(EXXXXX);
                if (XXXXX.Count > 0)
                {
                    ViewBag.ListaConocimientos = JsonConvert.SerializeObject(XXXXX);
                }
                List<Cls_Ent_Actividad> XXXXXX;
                Cls_Ent_Actividad EXXXXXX = new Cls_Ent_Actividad();
                EXXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXXX = new SolicitudRepositorio().ListaActividad(EXXXXXX);
                if (XXXXXX.Count > 0)
                {
                    ViewBag.ListaActividad = JsonConvert.SerializeObject(XXXXXX);

                }
                List<Cls_Ent_Actividad_Otro> XXXXXXX;
                Cls_Ent_Actividad_Otro EXXXXXXX = new Cls_Ent_Actividad_Otro();
                EXXXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXXXX = new SolicitudRepositorio().ListaActividadOtro(EXXXXXXX);
                if (XXXXXXX.Count > 0)
                {
                    ViewBag.ListaActividadOtro = JsonConvert.SerializeObject(XXXXXXX);
                }
                List<Cls_Ent_Renumeracion> XXXXXXXX;
                Cls_Ent_Renumeracion EXXXXXXXX = new Cls_Ent_Renumeracion();
                EXXXXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXXXXX = new SolicitudRepositorio().ListaPeriodoPagoSolicitud(EXXXXXXXX);
                if (XXXXXXXX.Count > 0)
                {
                    ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(XXXXXXXX);
                }
            }
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            return View(modelo);
        }
        public ActionResult ListaPuestoDetalle(int id)
        {
            Cls_Ent_Puesto lista = new Cls_Ent_Puesto();
            lista.ID_PUESTO = id;
            lista = new CoordinadorRepositorio().ListaPuestos(lista).First();
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        public ActionResult MantenimientoSolicitudPac(Cls_Ent_Solicitud entidad)
        {
            int ID = entidad.ID_SOLICITUD;
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                var httpRequest = Request.Files;
                long IdLaserfiche = 0;
                if (httpRequest.Count > 0)
                {
                    if (entidad.ID_SOLICITUD != 0)
                    {
                        var files = Request.Files[0] as HttpPostedFileBase;
                        if (files.ContentLength > 0)
                        {
                            var carpeta = Server.MapPath("~/ArchivoTemp");
                            string nomArchivoSave = "";
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                            {
                                string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                                files.SaveAs(NombreArchivo);
                                string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                                nomArchivoSave = "SOLICITUD_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                                entidad.ARCHIVO_PUESTO_SUS_SOLICITUD = IdLaserfiche;
                            }
                        }
                    }
                }
                else
                {
                   
                    var ent = new SolicitudRepositorio().ListaSolicitudes(new Cls_Ent_Solicitud { TIPO_PROCESO = "P", ID_SOLICITUD= entidad.ID_SOLICITUD }).First();
                    if (ent != null)
                    {
                        entidad.ARCHIVO_PUESTO_SUS_SOLICITUD = ent.ARCHIVO_PUESTO_SUS_SOLICITUD;
                        entidad.NOMBRE_ARCHIVO_SUS_SOLICITUD = ent.NOMBRE_ARCHIVO_SUS_SOLICITUD;
                    }
                }
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                if (entidad.FLG_PROCESO != "D")
                {
                    var ex = jsSerializer.Deserialize<List<Cls_Ent_Experiencia>>(Request.Form["listaExperiencia"]);
                    entidad.listaExperiencia = ex;
                    var cur = jsSerializer.Deserialize<List<Cls_Ent_Curso>>(Request.Form["listaCurso"]);
                    entidad.listaCurso = cur;
                    var Acad = jsSerializer.Deserialize<List<Cls_Ent_Academico>>(Request.Form["listaAcademico"]);
                    entidad.listaAcademico = Acad;
                    var Cono = jsSerializer.Deserialize<List<Cls_Ent_Conocimientos>>(Request.Form["listaConocimiento"]);
                    entidad.listaConocimiento = Cono;
                    var act = jsSerializer.Deserialize<List<Cls_Ent_Actividad>>(Request.Form["listaActividad"]);
                    entidad.listaActividad = act;
                    var act_o = jsSerializer.Deserialize<List<Cls_Ent_Actividad_Otro>>(Request.Form["listaAcividadOtro"]);
                    entidad.listaAcividadOtro = act_o;
                    var fun = jsSerializer.Deserialize<List<Cls_Ent_Funciones_Solicitud>>(Request.Form["listaFunciones"]);
                    entidad.listaFunciones = fun;
                    var pg = jsSerializer.Deserialize<List<Cls_Ent_Renumeracion>>(Request.Form["listaRenumeracion"]);
                    entidad.listaRenumeracion = pg;
                }

                PreguntaRspta = new SolicitudRepositorio().MantenimientoSolicitudPac(entidad);
                Cls_Ent_Documento ent_doc = new Cls_Ent_Documento();
                if (entidad.FLG_PROCESO != "D" && entidad.FLG_VERSION == "2")
                {
                    if (PreguntaRspta.FLG_OK)
                    {
                        var delete = new SolicitudRepositorio().MantenimientoDOcumento(new Cls_Ent_Documento { ID_PROCESO = PreguntaRspta.ID_SOLICITUD, FLG_TIPO = "S", ACCION = "T" });
                        if (delete.FLG_OK)
                        {
                            var doc = jsSerializer.Deserialize<List<Cls_Ent_Documento>>(Request.Form["listaDocumentos"]);
                            long ID_LF = 0;
                            foreach (var item in doc)
                            {
                                if (item.ID_LF == 0)
                                {
                                    var carpeta = Server.MapPath("~//ArchivoTemp//Registro_Multiple//");
                                    string NombreArchivo = carpeta + item.NOM_DOCUMENTO + ".pdf";
                                    string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + PreguntaRspta.ID_SOLICITUD;
                                    ID_LF = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, item.NOM_DOCUMENTO + ".pdf", UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                                }

                                var entity_ = new Cls_Ent_Documento
                                {
                                    ID_DOCUMENTO = item.ID_DOCUMENTO,
                                    ID_PROCESO = PreguntaRspta.ID_SOLICITUD,
                                    FLG_TIPO = item.FLG_TIPO,
                                    DES_DOCUMENTO = item.DES_DOCUMENTO,
                                    ID_LF = ID_LF == 0 ? item.ID_LF : ID_LF,
                                    RUTA_ARCHIVO = item.RUTA_ARCHIVO,
                                    NOM_DOCUMENTO = item.NOM_DOCUMENTO,
                                };
                                ent_doc = new SolicitudRepositorio().MantenimientoDOcumento(entity_);
                            }
                        }

                    }
                }
                else
                {
                    ent_doc.FLG_OK = true;
                }
                //if (ID == 0)
                //{
                //    entidad.ID_SOLICITUD = PreguntaRspta.ID_SOLICITUD;
                //    entidad.FLG_PROCESO = "U";

                //    var files = Request.Files[0] as HttpPostedFileBase;
                //    if (files.ContentLength > 0)
                //    {
                //        var carpeta = Server.MapPath("~/ArchivoTemp");
                //        string nomArchivoSave = "";
                //        using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                //        {
                //            string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                //            files.SaveAs(NombreArchivo);
                //            string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + PreguntaRspta.ID_SOLICITUD;
                //            nomArchivoSave = "SOLICITUD_" + RandomString(5, true) + ".pdf";
                //            IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                //            entidad.ARCHIVO_PUESTO_SUS_SOLICITUD = IdLaserfiche;
                //        }
                //    }
                //    PreguntaRspta = new SolicitudRepositorio().MantenimientoSolicitudFag(entidad);
                //    entidad.FLG_PROCESO = "I";
                //}


                if (ent_doc.FLG_OK)
                {
                    if (!PreguntaRspta.FLG_OK)
                    {
                        itemRespuesta.message = "Ocurrio un error.";
                        itemRespuesta.success = false;
                        itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
                    }
                    else
                    {
                        if (entidad.FLG_PROCESO == "I")
                        {
                            itemRespuesta.message = "Se registro correctamente la solicitud.";
                        }
                        else
                        {
                            if (entidad.FLG_PROCESO == "U")
                            {
                                itemRespuesta.message = "Se actualizo correctamente la solicitud.";
                            }
                            else
                            {
                                itemRespuesta.message = "Se elimino correctamente la solicitud.";
                            }
                        }


                        itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                        itemRespuesta.success = true;
                    }
                }
                else
                {
                    itemRespuesta.message = "Ocurrio un error en registrar los archivos de la solicitud.";
                    itemRespuesta.success = false;
                    itemRespuesta.message2 = "0";
                    itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "<br/>"+ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ListaDetallePersona(string dni)
        {
            Cls_Ent_Solicitud datos = new Cls_Ent_Solicitud();
            Cls_Ent_Encriptacion resultadoEncriptarDNI = new Cls_Ent_Encriptacion();
            Cls_Ent_Encriptacion resultadoReniecDNI = new Cls_Ent_Encriptacion();
            try
            {
                string llave = "";
                llave = GetLlave();

                if (llave != "")
                {
                    llave = System.Web.HttpUtility.UrlEncode(llave);
                    resultadoEncriptarDNI = EncriptarLlave(llave, dni);

                    if (resultadoEncriptarDNI.Code == 1)
                    { //Se encripto correctamente el dni
                        string dniEncriptado = resultadoEncriptarDNI.MensajeSalida;

                        resultadoReniecDNI = ObtenerDatosDNI_RENIEC(llave, dniEncriptado);

                        if (resultadoReniecDNI.Code == 1)
                        {
                            datos.APELLIDO_PAT_CONSULTOR = resultadoReniecDNI.Objeto.APE_PATERNO;
                            datos.APELLIDO_MAT_CONSULTOR = resultadoReniecDNI.Objeto.APE_MATERNO;
                            datos.NOMBRES_CONSULTOR = resultadoReniecDNI.Objeto.NOMBRES;
                            datos.ACCION = "1";
                        }
                    }
                }
                /*WCF_DatosPersonales.tabla[] resultado = null;
                using (WCF_DatosPersonales.ReniecSoapClient proxy = new WCF_DatosPersonales.ReniecSoapClient())
                {
                    resultado = proxy.TDni(dni);
                    datos.APELLIDO_PAT_CONSULTOR = resultado[0].t02.Trim();
                    datos.APELLIDO_MAT_CONSULTOR = resultado[0].t03.Trim();
                    datos.NOMBRES_CONSULTOR = resultado[0].t04.Trim();
                    datos.ACCION = "1";
                }*/
            }
            catch (Exception ex)
            {
                datos.ACCION = "0";
                datos.DES_ERROR = ex.ToString();

            }

            var jsonResult = Json(datos, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        public String GetLlave()
        {
            string llave = "";
            string metodo = "/sercivios-api/encriptar/traer-llave/";
            var url = ConfigurationManager.AppSettings["Ruta_API_PIDEMEF"].ToString() + metodo;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                            llave = jsSerializer.Deserialize<string>(objReader.ReadToEnd());
                        }
                    }
                }
                return llave;
            }
            catch (WebException ex)
            {
                // Handle error
                return "";
            }
        }
        public Cls_Ent_Encriptacion EncriptarLlave(string llave, string dni)
        {
            string metodo = "/sercivios-api/encriptar/encriptar-valor";
            var baseUrl = ConfigurationManager.AppSettings["Ruta_API_PIDEMEF"].ToString() + metodo;
            string responderParameters = "LLAVE=" + llave + "&TEXT=" + dni;
            var cookieJar = new CookieContainer();
            Cls_Ent_Encriptacion resultado = new Cls_Ent_Encriptacion();
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
                request.CookieContainer = cookieJar;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(responderParameters);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string ResponseAPI = reader.ReadToEnd();
                response.Close();
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                resultado = jsSerializer.Deserialize<Cls_Ent_Encriptacion>(ResponseAPI);

            }
            catch (WebException ex)
            {
                throw ex;
            }

            return resultado;

        }
        public Cls_Ent_Encriptacion ObtenerDatosDNI_RENIEC(string llave, string dni_encriptado)
        {
            dni_encriptado = System.Web.HttpUtility.UrlEncode(dni_encriptado);
            Cls_Ent_Encriptacion resultado = new Cls_Ent_Encriptacion();
            string metodo = "/servicios-pide/reniec/consulpersona";
            var baseUrl = ConfigurationManager.AppSettings["Ruta_API_PIDEMEF"].ToString() + metodo;
            string responderParameters = "LLAVE=" + llave + "&NUM_DOC=" + dni_encriptado;
            var cookieJar = new CookieContainer();

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
                request.CookieContainer = cookieJar;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(responderParameters);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string ResponseAPI = reader.ReadToEnd();
                response.Close();
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                resultado = jsSerializer.Deserialize<Cls_Ent_Encriptacion>(ResponseAPI);
            }
            catch (WebException ex)
            {
                resultado.Code = 0;
                throw ex;

            }
            return resultado;
        }





        public ActionResult ListaSolicitudes(Cls_Ent_Solicitud entidad)
        {
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            entidad.ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD;
            List<Cls_Ent_Solicitud> lista;
            lista = new SolicitudRepositorio().ListaSolicitudes(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public async Task<ActionResult> ExportarExcelSolicitudes(Cls_Ent_Solicitud entidad)
        {
            try
            {
                if (Session["Usuario"] != null)
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
                else
                {
                    var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                    var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                    if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                    {
                        UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                    }
                }

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                int row = 0;
                using (ExcelPackage package = new ExcelPackage())
                {
                    var ws1 = package.Workbook.Worksheets.Add("Registros_Consulta");
                    row += 1;
                    int fila = 1;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "FECHA REGISTRO", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "H.R. ASIGNADA", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "N° SOLICITUD", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "ESTADO", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 40; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "DNI / CE ", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "APELLIDOS Y NOMBRES", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 50; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "N° DE CONTRATO", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    if (entidad.TIPO_PROCESO=="P")
                    {
                        ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "FICHA", row, fila, 0, 0, "L", true);
                        ws1.Column(fila).Width = 30; fila++;
                    }
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "CARGO", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 40; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "HONORARIOS S/", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "FECHA INICIO", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "FECHA FIN", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ws1.Row(row).Height = 30;
                    entidad.ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD;
                    var data = new  SolicitudRepositorio().ListaSolicitudes(entidad);
                    if (data.Count > 0)
                    {
                        foreach (var item in data)
                        {
                            row++;
                            int columna = 1;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.FECHA_REGISTRO, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NR_TRAMITE, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NUM_PROCESO, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.DESC_SOLICITUD, row, columna, 0, 0, "L"); columna++;

                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NUM_DOCUMENTO_CONSULTOR, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.APELLIDO_PAT_CONSULTOR +" "+ item.APELLIDO_MAT_CONSULTOR + " "+ item.NOMBRES_CONSULTOR, row, columna, 0, 0, "L"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NUM_CONTRATO, row, columna, 0, 0, "C"); columna++;
                            if (entidad.TIPO_PROCESO == "P")
                            {
                                ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.DESC_TIPO_FICHA, row, columna, 0, 0, "L"); columna++;
                            }
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.DENOMINACION_PUESTO, row, columna, 0, 0, "L"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.MONTO_MENSUAL, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.FECHA_INICIO.ToShortDateString(), row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.FECHA_FIN.ToShortDateString(), row, columna, 0, 0, "C"); columna++;
                        }
                    }
                    using (var stream = new System.IO.MemoryStream(package.GetAsByteArray()))
                    {
                        byte[] buffer = new byte[stream.Length];
                        var cd = new System.Net.Mime.ContentDisposition
                        {
                            FileName = Guid.NewGuid().ToString() + ".xlsx",
                            Inline = false,
                        };
                        Response.AppendHeader("Content-Disposition", cd.ToString());
                        return await Task.FromResult(File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), "Coordinador.SolicitudController.ExportarExcelSolicitudes");
            }
            return await FormatoError();
        }
        public ActionResult NuevoEditaSolicitudFag(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
            List<Cls_Ent_Entidades> Lista_Entidad = null;
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            Lista_Entidad = new SolicitudRepositorio().ListaEntidades(new Cls_Ent_Entidades { ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD });
            ViewBag.DcboEntidades = Lista_Entidad;
            modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString();
            modelo.TIPO_PROCESO = "F";
            ViewBag.ListaServicio = JsonConvert.SerializeObject(new List<Cls_Ent_Funciones_Solicitud>());
            ViewBag.ListaAcademica = JsonConvert.SerializeObject(new List<Cls_Ent_Academico>());
            ViewBag.ListaCursos = JsonConvert.SerializeObject(new List<Cls_Ent_Curso>());
            ViewBag.ListaConocimientos = JsonConvert.SerializeObject(new List<Cls_Ent_Conocimientos>());
            ViewBag.ListaActividad = JsonConvert.SerializeObject(new List<Cls_Ent_Actividad>());
            ViewBag.ListaActividadOtro = JsonConvert.SerializeObject(new List<Cls_Ent_Actividad_Otro>());
            ViewBag.ListaExperiencia = JsonConvert.SerializeObject(new List<Cls_Ent_Experiencia>());
            ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(new List<Cls_Ent_Renumeracion>());
            if (ID_SOLICITUD == 0)
            {
                modelo.FLG_PROCESO = "I";
                modelo.FLG_VERSION = "1";
            }
            else
            {
                var lista = new SolicitudRepositorio().ListaSolicitudes(new Cls_Ent_Solicitud { ID_SOLICITUD = ID_SOLICITUD }).First();
                modelo = lista;
                modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString();
                modelo.FLG_PROCESO = "U";
                List<Cls_Ent_Experiencia> XX;
                Cls_Ent_Experiencia EXX = new Cls_Ent_Experiencia();
                EXX.ID_SOLICITUD = ID_SOLICITUD;
                XX = new SolicitudRepositorio().ListaExperiencia(EXX);
                if (XX.Count > 0)
                {
                    ViewBag.ListaExperiencia = JsonConvert.SerializeObject(XX);
                }
                List<Cls_Ent_Academico> XXX;
                Cls_Ent_Academico EXXX = new Cls_Ent_Academico();
                EXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXX = new SolicitudRepositorio().ListaAcademica(EXXX);
                if (XXX.Count > 0)
                {
                    ViewBag.ListaAcademica = JsonConvert.SerializeObject(XXX);
                }
                List<Cls_Ent_Curso> XXXX;
                Cls_Ent_Curso EXXXX = new Cls_Ent_Curso();
                EXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXX = new SolicitudRepositorio().ListaCursos(EXXXX);
                if (XXXX.Count > 0)
                {
                    ViewBag.ListaCursos = JsonConvert.SerializeObject(XXXX);
                }
                List<Cls_Ent_Conocimientos> XXXXX;
                Cls_Ent_Conocimientos EXXXXX = new Cls_Ent_Conocimientos();
                EXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXX = new SolicitudRepositorio().ListaConocimientos(EXXXXX);
                if (XXXXX.Count > 0)
                {
                    ViewBag.ListaConocimientos = JsonConvert.SerializeObject(XXXXX);
                }
                List<Cls_Ent_Actividad> XXXXXX;
                Cls_Ent_Actividad EXXXXXX = new Cls_Ent_Actividad();
                EXXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXXX = new SolicitudRepositorio().ListaActividad(EXXXXXX);
                if (XXXXXX.Count > 0)
                {
                    ViewBag.ListaActividad = JsonConvert.SerializeObject(XXXXXX);

                }
                List<Cls_Ent_Actividad_Otro> XXXXXXX;
                Cls_Ent_Actividad_Otro EXXXXXXX = new Cls_Ent_Actividad_Otro();
                EXXXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXXXX = new SolicitudRepositorio().ListaActividadOtro(EXXXXXXX);
                if (XXXXXXX.Count > 0)
                {
                    ViewBag.ListaActividadOtro = JsonConvert.SerializeObject(XXXXXXX);
                }
                List<Cls_Ent_Renumeracion> XXXXXXXX;
                Cls_Ent_Renumeracion EXXXXXXXX = new Cls_Ent_Renumeracion();
                EXXXXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXXXXX = new SolicitudRepositorio().ListaPeriodoPagoSolicitud(EXXXXXXXX);
                if (XXXXXXXX.Count > 0)
                {
                    ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(XXXXXXXX);
                }
            }
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            return View(modelo);
        }
        public ActionResult MantenimientoSolicitudFag(Cls_Ent_Solicitud entidad)
        {
           // int ID = entidad.ID_SOLICITUD;
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                var httpRequest = Request.Files;
                long IdLaserfiche = 0;
                if (httpRequest.Count > 0)
                {
                    if (entidad.ID_SOLICITUD != 0)
                    {
                        var files = Request.Files[0] as HttpPostedFileBase;
                        if (files.ContentLength > 0)
                        {
                            var carpeta = Server.MapPath("~/ArchivoTemp");
                            string nomArchivoSave = "";
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                            {
                                string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                                files.SaveAs(NombreArchivo);
                                string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                                nomArchivoSave = "SOLICITUD_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                                entidad.ARCHIVO_PUESTO_SUS_SOLICITUD = IdLaserfiche;
                            }
                        }
                    }

                }
                else
                {
                    var ent = new SolicitudRepositorio().ListaSolicitudes(new Cls_Ent_Solicitud { TIPO_PROCESO = "F", ID_SOLICITUD = entidad.ID_SOLICITUD }).First();
                    if (ent != null)
                    {
                        entidad.ARCHIVO_PUESTO_SUS_SOLICITUD = ent.ARCHIVO_PUESTO_SUS_SOLICITUD;
                        entidad.NOMBRE_ARCHIVO_SUS_SOLICITUD = ent.NOMBRE_ARCHIVO_SUS_SOLICITUD;
                    }
                }
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                if (entidad.FLG_PROCESO != "D")
                {
                    entidad.listaExperiencia = jsSerializer.Deserialize<List<Cls_Ent_Experiencia>>(Request.Form["listaExperiencia"]);
                    entidad.listaCurso = jsSerializer.Deserialize<List<Cls_Ent_Curso>>(Request.Form["listaCurso"]);
                    entidad.listaAcademico = jsSerializer.Deserialize<List<Cls_Ent_Academico>>(Request.Form["listaAcademico"]);
                    entidad.listaConocimiento = jsSerializer.Deserialize<List<Cls_Ent_Conocimientos>>(Request.Form["listaConocimiento"]);
                    entidad.listaActividad = jsSerializer.Deserialize<List<Cls_Ent_Actividad>>(Request.Form["listaActividad"]);
                    entidad.listaAcividadOtro = jsSerializer.Deserialize<List<Cls_Ent_Actividad_Otro>>(Request.Form["listaAcividadOtro"]);
                    entidad.listaRenumeracion = jsSerializer.Deserialize<List<Cls_Ent_Renumeracion>>(Request.Form["listaRenumeracion"]);
                
                }
                PreguntaRspta = new SolicitudRepositorio().MantenimientoSolicitudFag(entidad);
                Cls_Ent_Documento ent_doc= new Cls_Ent_Documento();
                if (entidad.FLG_PROCESO != "D")
                {
                    if (PreguntaRspta.FLG_OK)
                    {
                        var delete = new SolicitudRepositorio().MantenimientoDOcumento(new Cls_Ent_Documento {ID_PROCESO = PreguntaRspta.ID_SOLICITUD,FLG_TIPO = "S", ACCION="T" });
                        if (delete.FLG_OK)
                        {
                            var doc = jsSerializer.Deserialize<List<Cls_Ent_Documento>>(Request.Form["listaDocumentos"]);
                            long ID_LF = 0;
                            foreach (var item in doc)
                            {
                                if (item.ID_LF==0)
                                {
                                    var carpeta = Server.MapPath("~//ArchivoTemp//Registro_Multiple//");
                                    string NombreArchivo = carpeta + item.NOM_DOCUMENTO + ".pdf";
                                    string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + PreguntaRspta.ID_SOLICITUD;
                                    ID_LF = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, item.NOM_DOCUMENTO + ".pdf", UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                                }

                                var entity_ = new Cls_Ent_Documento
                                {
                                    ID_DOCUMENTO = item.ID_DOCUMENTO,
                                    ID_PROCESO = PreguntaRspta.ID_SOLICITUD,
                                    FLG_TIPO = item.FLG_TIPO,
                                    DES_DOCUMENTO = item.DES_DOCUMENTO,
                                    ID_LF = ID_LF==0? item.ID_LF: ID_LF,
                                    RUTA_ARCHIVO = item.RUTA_ARCHIVO,
                                    NOM_DOCUMENTO = item.NOM_DOCUMENTO,
                                };
                                ent_doc = new SolicitudRepositorio().MantenimientoDOcumento(entity_);
                            }
                        }
                     
                    }
                }
                else
                {
                    ent_doc.FLG_OK = true;
                }
          

                if (ent_doc.FLG_OK)
                {
                    if (!PreguntaRspta.FLG_OK)
                    {
                        itemRespuesta.message = "Ocurrio un error.";
                        itemRespuesta.success = false;
                        itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
                    }
                    else
                    {
                        if (entidad.FLG_PROCESO == "I")
                        {
                            itemRespuesta.message = "Se registro correctamente la solicitud.";
                        }
                        else
                        {
                            if (entidad.FLG_PROCESO == "U")
                            {
                                itemRespuesta.message = "Se actualizo correctamente la solicitud.";
                            }
                            else
                            {
                                itemRespuesta.message = "Se elimino correctamente la solicitud.";
                            }
                        }


                        itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                        itemRespuesta.success = true;
                    }
                }
                else
                {
                    itemRespuesta.message = "Ocurrio un error en registrar los archivos de la solicitud.";
                    itemRespuesta.success = false;
                    itemRespuesta.message2 = "0";
                    itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
                }
            
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno();
            }

            return Respuesta(itemRespuesta);
        }
        public async Task<ActionResult> Ver_Formatos(int ID, string FLG_TIPO)
        {
            try
            {
                byte[] bytes = new ReporteRepositorio().GenerarSolicitudPDF(ID, FLG_TIPO);
                using (var stream = new System.IO.MemoryStream(bytes))
                {
                    byte[] buffer = new byte[stream.Length];
                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = Guid.NewGuid() + ".pdf",
                        Inline = false,
                    };
                    Response.AppendHeader("Content-Disposition", cd.ToString());
                    return await Task.FromResult(File(stream.ToArray(), MediaTypeNames.Application.Pdf));
                }
            }
            catch (Exception ex)
            {

                Log.MensajeLog(ex.ToString(), "Coordinador.SolicitudController.Ver_Formatos");
            }
            return await FormatoError();
        }
        public ActionResult Enviar_Solicitud_Participante(Cls_Ent_Solicitud entidad)
        {
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                entidad.CONTRASENA = Encriptar.Encriptar_Pass(DateTime.Now.Year.ToString() + "," + DateTime.Now.Second.ToString() + "!" + entidad.USU_INGRESO.Left(2));
                List<Cls_Ent_Solicitud> Lista_Solicitud = null;
                Cls_Ent_Solicitud ent_s = new Cls_Ent_Solicitud();
                Lista_Solicitud = new SolicitudRepositorio().ListaSolicitudes(ent_s).FindAll(A => A.ID_SOLICITUD == entidad.ID_SOLICITUD);
                entidad.DENOMINACION_PUESTO = Lista_Solicitud[0].DENOMINACION_PUESTO;
                entidad.DESC_ENTIDAD = Lista_Solicitud[0].DESC_ENTIDAD;
                List<Cls_Ent_Personal> Lista_Personal = null;
                Cls_Ent_Personal ent = new Cls_Ent_Personal();
                Lista_Personal = new SolicitudRepositorio().ListaPersonal(ent).FindAll(A => A.NUM_DOCUMENTO == Lista_Solicitud[0].NUM_DOCUMENTO_CONSULTOR);
                entidad.ID_PERSONAL = Lista_Personal[0].ID_PERSONAL;
                PreguntaRspta = new SolicitudRepositorio().Enviar_Solicitud_Participante(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                    Cls_Ent_Personal ent_ = new Cls_Ent_Personal();
                    ent_ = new SolicitudRepositorio().ListaPersonal(ent_).First(A => A.NUM_DOCUMENTO == Lista_Solicitud[0].NUM_DOCUMENTO_CONSULTOR);
                    ent_.DESCRIPCION = Lista_Solicitud[0].DENOMINACION_PUESTO;
                    entidad.NUM_PROCESO = Lista_Solicitud[0].NUM_PROCESO;
                    if (EnviarEmailPersonal(ent_, entidad))
                    {
                        itemRespuesta.message = "Se realizo la notificación al participante.";
                    }
                    else {
                        itemRespuesta.message = "Error al realizar la notificación al participante, vuelva a intentarlo en unos minutos.";
                    }

                    itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                    itemRespuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "</br>" + ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailPersonal(Cls_Ent_Personal entidad, Cls_Ent_Solicitud soli)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarAccesoPersonal.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", entidad.APELLIDO_PATERNO + " " + entidad.APELLIDO_MATERNO + " " + entidad.NOMBRES);
            mensaje = mensaje.Replace("{1}", entidad.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", entidad.DESCRIPCION);
            mensaje = mensaje.Replace("{3}", entidad.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{4}", Encriptar.Desencriptar_Pass(entidad.CONTRASENA));
            mensaje = mensaje.Replace("{5}", ConfigurationManager.AppSettings["UrlAplicacion"]);
            mensaje = mensaje.Replace("{6}", soli.DESC_ENTIDAD);
            mensaje = mensaje.Replace("{7}", soli.NUM_PROCESO);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.EMAIL;
            bool strRet = EnviarMail.SendMailMessage(ref strMsgError, destinatarios, ConfigurationManager.AppSettings["CorreoCopia"], "", titulo, mensaje, "", ConfigurationManager.AppSettings["Usermail"]);

            if (strRet)
            {
                return true;
            }
            else
            {
                ModelState.AddModelError("Error", strMsgError);
                return false;
            }
        }
        public ActionResult ValidarRemuneracion(Cls_Ent_Renumeracion entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            try
            {
                itemRespuesta.success = true;
                string error = "";
                string Mensaje = "";
                itemRespuesta.codigo = "1";
                foreach (Cls_Ent_Renumeracion EntidadDet in entidad.Lista)
                {
                    new SolicitudRepositorio().ValidarRemuneracion(EntidadDet);
                    if (!EntidadDet.FLG_OK)
                    {
                        var tex = "No tiene presupuesto el mes de " + EntidadDet.DES_MES + "</br>";
                        Mensaje += tex + "\n\r ";
                        itemRespuesta.codigo = "0";
                    }
                    else
                    {
                        if (EntidadDet.PO_VALIDO == "0")
                        {
                            Mensaje += EntidadDet.PO_MENSAJE + "</br>";
                            Mensaje += "\n\r ";
                            itemRespuesta.codigo = "0";
                        }

                    }
                }
                itemRespuesta.success = true;
                if (itemRespuesta.codigo == "0")
                {
                    itemRespuesta.message = Mensaje;
                }
                else {
                    itemRespuesta.message = "La validación se realizó correctamente.";
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "</br>" + ex.ToString();
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateArchivoTdr(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            return View(modelo);
        }
        public ActionResult UpdateArchivoTdrSN (int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            return View(modelo);
        }
        public ActionResult AccionUpdateArchivoTdr(Cls_Ent_Solicitud entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
                var httpRequest = Request.Files;
                int IdLaserfiche = 0;
                if (httpRequest.Count > 0)
                {
                    var files = Request.Files[0] as HttpPostedFileBase;
                    if (files.ContentLength > 0)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                        {
                            string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                            files.SaveAs(NombreArchivo);
                            string SubcarpetaLF = entidad.TIPO_PROCESO+"_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                            nomArchivoSave = "TDR_FIRMADO_PROFESSIONAL_" + RandomString(5, true) + ".pdf";
                            IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                            entidad.ARCHIVO_TDR = IdLaserfiche;
                        }
                    }
                }
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                PreguntaRspta = new SolicitudRepositorio().UpdateArchivoTdr(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                    itemRespuesta.message = "El archivo se guardo correctamente.";
                    itemRespuesta.extra = entidad.ID_SOLICITUD.ToString();
                    itemRespuesta.success = true;
                }

            }
            catch (Exception ex)
            {
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "</br>" + ex.ToString();
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        public ActionResult VerFormatoSolicitud(int ID_PERSONAL, int ID_SOLICITUD, string TIPO)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            modelo.ID_PERSONAL = ID_PERSONAL;
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.ACCION = TIPO;
            return View(modelo);

        }
        public ActionResult VerFormatoArchivosPersonal(int ID_PERSONAL, int ID_SOLICITUD,int ID, string TIPO)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            modelo.ID_PERSONAL = ID_PERSONAL;
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.ID = ID;
            modelo.ACCION = TIPO;
            return View(modelo);

        }
        //INICIO CV
        public ActionResult ListaEstudios(Cls_Ent_Estudios entidad)
        {
            List<Cls_Ent_Estudios> lista;
            lista = new PersonalCvRepositorio().ListaEstudios(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ListaEspecializacion(Cls_Ent_Especializacion entidad)
        {
            List<Cls_Ent_Especializacion> lista;
            lista = new PersonalCvRepositorio().ListaEspecializacion(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ListaCapacitacion(Cls_Ent_Capacitacion entidad)
        {
            List<Cls_Ent_Capacitacion> lista;
            lista = new PersonalCvRepositorio().ListaCapacitacion(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ListaExperiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            List<Cls_Ent_Experiencia_Laboral> lista;
            lista = new PersonalCvRepositorio().ListaExperiencia(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //FIN CV
        public ActionResult ObservacionProfesional(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            return View(modelo);
        }
        public ActionResult InsReevaluacion(Cls_Ent_Reevaluacion entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            Cls_Ent_Reevaluacion PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                entidad.NOMBRE_COMPLETO= UsuarioSistemaSesion.NOMBRE_COMPLETO;
                PreguntaRspta = new SolicitudRepositorio().InsReevaluacion(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {
                  
                    Cls_Ent_Solicitud ent_s = new Cls_Ent_Solicitud();
                    ent_s = new SolicitudRepositorio().ListaSolicitudes(ent_s).First(A => A.ID_SOLICITUD == entidad.ID_SOLICITUD);
                    Cls_Ent_Personal ent_ = new Cls_Ent_Personal();
                    ent_ = new SolicitudRepositorio().ListaPersonal(ent_).First(A => A.NUM_DOCUMENTO == ent_s.NUM_DOCUMENTO_CONSULTOR);
                    ent_s.DESCRIPCION = entidad.DES_REEVALUACION;
                    if (EnviarEmailPersonalobservado(ent_, ent_s))
                    {
                        itemRespuesta.message = "Se realizo la notificación al profesional.";
                    }
                    else
                    {
                        itemRespuesta.message = "Error al realizar la notificación al participante, vuelva a intentarlo en unos minutos.";
                    }
                    itemRespuesta.extra = entidad.ID_SOLICITUD.ToString();
                    itemRespuesta.success = true;
                }

            }
            catch (Exception ex)
            {
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "</br>" + ex.ToString();
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        private bool EnviarEmailPersonalobservado(Cls_Ent_Personal entidad, Cls_Ent_Solicitud soli)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarobservacionPersonal.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", entidad.APELLIDO_PATERNO + " " + entidad.APELLIDO_MATERNO + " " + entidad.NOMBRES);
            mensaje = mensaje.Replace("{1}", entidad.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", soli.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{3}", soli.DESCRIPCION);
            //mensaje = mensaje.Replace("{4}", Encriptar.Desencriptar_Pass(entidad.CONTRASENA));
            mensaje = mensaje.Replace("{5}", ConfigurationManager.AppSettings["UrlAplicacion"]);
            mensaje = mensaje.Replace("{6}", soli.DESC_ENTIDAD);
            mensaje = mensaje.Replace("{7}", soli.NUM_PROCESO);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.EMAIL;
            bool strRet = EnviarMail.SendMailMessage(ref strMsgError, destinatarios, ConfigurationManager.AppSettings["CorreoCopia"], "", titulo, mensaje, "", ConfigurationManager.AppSettings["Usermail"]);

            if (strRet)
            {
                return true;
            }
            else
            {
                ModelState.AddModelError("Error", strMsgError);
                return false;
            }
        }
        public ActionResult ListaObservacion(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista;
     
            lista = new SolicitudRepositorio().ListaReevaluacion(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult VerObservacionProfesional(int ID_SOLICITUD, string TIPO)
        {
            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.ACCION = TIPO;
            return View(modelo);
        }
        public ActionResult EnvioUtpDocumentos(int ID_SOLICITUD , string TIPO)
        {
            Cls_Ent_Solicitud_Personal modelo = new Cls_Ent_Solicitud_Personal();
            modelo = new PersonalCvRepositorio().ListaSolicitudesCoordinador(new Cls_Ent_Solicitud_Personal { ID_SOLICITUD= ID_SOLICITUD }).FirstOrDefault();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.ACCION = TIPO;
            modelo.ID_ANEXO1 = modelo.ARCHIVO_TDR;
            modelo.ID_FORMATOA = modelo.ID_FORMATOA;
            return View(modelo);
        }
        public ActionResult UpdArchivosFAG(Cls_Ent_Solicitud entidad)
        {
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                Cls_Ent_Solicitud x = new Cls_Ent_Solicitud();
                x.ID_SOLICITUD = entidad.ID_SOLICITUD;
                x = new SolicitudRepositorio().ListaSolicitudes(x).First();
                entidad.ID_ANEXO2 = x.ID_ANEXO2;
                entidad.ID_ANEXO3 = x.ID_ANEXO3;
                entidad.ID_ANEXO7 = x.ID_ANEXO7;
                entidad.ID_BANCO = x.ID_BANCO;
                var httpRequest = Request.Files;
                if (httpRequest.Count > 0)
                {
                    var filesAnexo1 = Request.Files["FileArchivoAnexo1"] as HttpPostedFileBase;
                    int IdLaserficheAnexo01 = 0;
                    if (filesAnexo1 != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesAnexo1.FileName.ToString());
                        filesAnexo1.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "ANEXO01_FIRMADO_" + RandomString(5, true) + ".pdf";
                        IdLaserficheAnexo01 = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_ANEXO1 = IdLaserficheAnexo01;
                    }
                    //else
                    //{
                    //    entidad.ID_ANEXO1 = x.ID_ANEXO1;
                    //}
                    var filesAnexo4 = Request.Files["FileArchivoAnexo4"] as HttpPostedFileBase;
                    int IdLaserficheAnexo04 = 0;
                    if (filesAnexo4 != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesAnexo4.FileName.ToString());
                        filesAnexo4.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "ANEXO04_FIRMADO_" + RandomString(5, true) + ".pdf";
                        IdLaserficheAnexo04 = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_ANEXO4 = IdLaserficheAnexo04;
                    }
                    else
                    {
                        entidad.ID_ANEXO4 = x.ID_ANEXO4;
                    }
                    var filesAnexo5 = Request.Files["FileArchivoAnexo5"] as HttpPostedFileBase;
                    int IdLaserficheAnexo05 = 0;
                    if (filesAnexo5 != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesAnexo5.FileName.ToString());
                        filesAnexo5.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "ANEXO05_FIRMADO_" + RandomString(5, true) + ".pdf";
                        IdLaserficheAnexo05 = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_ANEXO5 = IdLaserficheAnexo05;
                    }
                    else
                    {
                        entidad.ID_ANEXO5 = x.ID_ANEXO5;
                    }
                    var filesAnexoHabilitacion = Request.Files["FileArchivoHabilitacion"] as HttpPostedFileBase;
                    int IdLaserficheAnexoHabilitacion = 0;
                    if (filesAnexoHabilitacion != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesAnexoHabilitacion.FileName.ToString());
                        filesAnexoHabilitacion.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "HABILITACION_FIRMADO_" + RandomString(5, true) + ".pdf";
                        IdLaserficheAnexoHabilitacion = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_H_PROFESIONAL = IdLaserficheAnexoHabilitacion;
                    }
                    else
                    {
                        entidad.ID_H_PROFESIONAL = x.ID_H_PROFESIONAL;
                    }
                    var filesOtros = Request.Files["FileArchivoOtros"] as HttpPostedFileBase;
                    int IdLaserficheOtros = 0;
                    if (filesOtros != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesOtros.FileName.ToString());
                        filesOtros.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "OTROS_ARCHIVOS_FIRMADO_" + RandomString(5, true) + ".pdf";
                        IdLaserficheOtros = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_OTROS = IdLaserficheOtros;
                    }
                    else
                    {
                        entidad.ID_OTROS = x.ID_OTROS;
                    }
                }
                else {
                    //entidad.ID_ANEXO1 = x.ID_ANEXO1;
                    entidad.ID_ANEXO2 = x.ID_ANEXO2;
                    entidad.ID_ANEXO3 = x.ID_ANEXO3;
                    entidad.ID_ANEXO4 = x.ID_ANEXO4;
                    entidad.ID_ANEXO5 = x.ID_ANEXO5;
                    entidad.ID_ANEXO7 = x.ID_ANEXO7;
                    entidad.ID_BANCO = x.ID_BANCO;
                    entidad.ID_H_PROFESIONAL = x.ID_H_PROFESIONAL;
                    entidad.ID_OTROS = x.ID_OTROS;
                }
                PreguntaRspta = new SolicitudRepositorio().UpdArchivosFAG(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                 itemRespuesta.message = "Los archivos se registrarón correctamente.";
                    itemRespuesta.extra = entidad.ID_SOLICITUD.ToString();
                    itemRespuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "</br>" + ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult UpdArchivosPAC(Cls_Ent_Solicitud entidad)
        {
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                Cls_Ent_Solicitud x = new Cls_Ent_Solicitud();
                x.ID_SOLICITUD = entidad.ID_SOLICITUD;
                x = new SolicitudRepositorio().ListaSolicitudes(x).First();
                entidad.ID_FORMATOB = x.ID_FORMATOB;
                entidad.ID_FORMATOC = x.ID_FORMATOC;
                entidad.ID_PAC_ANEXO2 = x.ID_PAC_ANEXO2; 
                var httpRequest = Request.Files;
                if (httpRequest.Count > 0)
                {
                    var filesInforme = Request.Files["FileArchivoIF"] as HttpPostedFileBase;
                    int IdLaserficheInforme = 0;
                    if (filesInforme != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesInforme.FileName.ToString());
                        filesInforme.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "INFORME_FAVORABLE_FIRMADO_" + RandomString(5, true) + ".pdf";
                        IdLaserficheInforme = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_INFORME_F = IdLaserficheInforme;
                    }
                    else
                    {
                        entidad.ID_INFORME_F = x.ID_INFORME_F;
                    }

                    var filesDS = Request.Files["FileArchivoDS"] as HttpPostedFileBase;
                    int IdLaserficheDS = 0;
                    if (filesDS != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesDS.FileName.ToString());
                        filesDS.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "DATOS_SECTOR_FIRMADO_" + RandomString(5, true) + ".pdf";
                        IdLaserficheDS = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_DATOS_SECTOR = IdLaserficheDS;
                    }
                    else
                    {
                        entidad.ID_DATOS_SECTOR = x.ID_DATOS_SECTOR;
                    }

                    var filesFormatoA = Request.Files["FileArchivoFORMATOA"] as HttpPostedFileBase;
                    int IdLaserficheFormatoA = 0;
                    if (filesFormatoA != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesFormatoA.FileName.ToString());
                        filesFormatoA.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "FORMATO_A_" + RandomString(5, true) + ".pdf";
                        IdLaserficheFormatoA = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_FORMATOA = IdLaserficheFormatoA;
                    }
                    else
                    {
                        entidad.ID_FORMATOA = x.ID_FORMATOA;
                    }

                    var filesFormatoD = Request.Files["FileArchivoFORMATOD"] as HttpPostedFileBase;
                    int IdLaserficheFormatoD = 0;
                    if (filesFormatoD != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesFormatoD.FileName.ToString());
                        filesFormatoD.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "FORMATO_D_" + RandomString(5, true) + ".pdf";
                        IdLaserficheFormatoD = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_FORMATOD = IdLaserficheFormatoD;
                    }
                    else
                    {
                        entidad.ID_FORMATOD = x.ID_FORMATOD;
                    }

                    var filesFormatoE = Request.Files["FileArchivoFORMATOE"] as HttpPostedFileBase;
                    int IdLaserficheFormatoE = 0;
                    if (filesFormatoE != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesFormatoE.FileName.ToString());
                        filesFormatoE.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "FORMATO_E_" + RandomString(5, true) + ".pdf";
                        IdLaserficheFormatoE = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_FORMATOE = IdLaserficheFormatoE;
                    }
                    else
                    {
                        entidad.ID_FORMATOE = x.ID_FORMATOE;
                    }

                    var filesFormatoHI = Request.Files["FileArchivoFORMATOHI"] as HttpPostedFileBase;
                    int IdLaserficheFormatoHI = 0;
                    if (filesFormatoHI != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesFormatoHI.FileName.ToString());
                        filesFormatoHI.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "FORMATO_HI_" + RandomString(5, true) + ".pdf";
                        IdLaserficheFormatoHI = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_FORMATOH = IdLaserficheFormatoHI;
                    }
                    else
                    {
                        entidad.ID_FORMATOH = x.ID_FORMATOH;
                    }

                    var filesHabilitacion = Request.Files["FileArchivoFORMATORH"] as HttpPostedFileBase;
                    int IdLaserficheFormatoHP = 0;
                    if (filesHabilitacion != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesHabilitacion.FileName.ToString());
                        filesHabilitacion.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "HABILITACION_FIRMADO_" + RandomString(5, true) + ".pdf";
                        IdLaserficheFormatoHP = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_PAC_H_PROFESIONAL = IdLaserficheFormatoHP;
                    }
                    else
                    {
                        entidad.ID_PAC_H_PROFESIONAL = x.ID_PAC_H_PROFESIONAL;
                    }

                    var filesOtros = Request.Files["FileArchivoOtros"] as HttpPostedFileBase;
                    int IdLaserficheOtros = 0;
                    if (filesOtros != null)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesOtros.FileName.ToString());
                        filesOtros.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        nomArchivoSave = "OTROS_" + RandomString(5, true) + ".pdf";
                        IdLaserficheOtros = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                        entidad.ID_OTROS = IdLaserficheOtros;
                    }
                    else
                    {
                        entidad.ID_OTROS = x.ID_OTROS;
                    }
                }
                else
                {
                    entidad.ID_OTROS = x.ID_OTROS;
                    entidad.ID_INFORME_F = x.ID_INFORME_F;
                    entidad.ID_DATOS_SECTOR = x.ID_DATOS_SECTOR;
                    entidad.ID_FORMATOA = x.ID_FORMATOA;
                    entidad.ID_FORMATOB = x.ID_FORMATOB;
                    entidad.ID_FORMATOC = x.ID_FORMATOC;
                    entidad.ID_FORMATOD = x.ID_FORMATOD;
                    entidad.ID_FORMATOE = x.ID_FORMATOE;
                    entidad.ID_FORMATOH = x.ID_FORMATOH;
                    entidad.ID_PAC_ANEXO2 = x.ID_PAC_ANEXO2;
                    entidad.ID_PAC_H_PROFESIONAL = x.ID_PAC_H_PROFESIONAL;
                }
                PreguntaRspta = new SolicitudRepositorio().UpdArchivosPAC(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                    itemRespuesta.message = "Los archivos se registrarón correctamente.";
                    itemRespuesta.extra = entidad.ID_SOLICITUD.ToString();
                    itemRespuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "</br>" + ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        protected Stream GetArchivoStream(HttpPostedFileBase archivo)
        {
            if (((archivo != null) && (archivo.ContentLength > 0)))
            {
                return archivo.InputStream;
            }
            throw new ArgumentNullException("El archivo esta vacio");
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        //INICIO DATOS PERSONALES
        public ActionResult DatosPersonales()
        {
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            Cls_Ent_Entidades Modelo = new Cls_Ent_Entidades();
            Modelo = new CoordinadorRepositorio().ListaEntidades(Modelo).First(A => A.ID_ENTIDAD == UsuarioSistemaSesion.ID_ENTIDAD);
            using (APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio.UbigeoRepositorio repositorio = new APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio.UbigeoRepositorio())
            {
                Modelo.ListaDpto = repositorio.ListaDepartamento().Select(x => new SelectListItem()
                {
                    Text = x.CNOMDEPARTAMENTO.Trim(),
                    Value = x.CCODDEPARTAMENTO.ToString()
                }).ToList();
                Modelo.ListaDpto.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }
            return View(Modelo);
            //return View();
        }
        //FIN DATOS PERSONALES
        public async Task<ActionResult> FormatoError()
        {
            byte[] adjunto = null;
            string rutaBse = Request.PhysicalApplicationPath + "Reportes\\" + "FORMATOS\\" + "ERROR.pdf";
            adjunto = System.IO.File.ReadAllBytes(rutaBse);
            using (var stream = new System.IO.MemoryStream(adjunto))
            {
                byte[] buffer = new byte[stream.Length];
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = "ErrorExportarArchivo.pdf",
                    Inline = false,
                };
                Response.AppendHeader("Content-Disposition", cd.ToString());
                return await Task.FromResult(File(stream.ToArray(), MediaTypeNames.Image.Jpeg));
            }

        }
        public async Task<ActionResult> DescargarExpediente(int ID)
        {
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            try
            {
                Cls_Ent_Solicitud_Personal modelo = new Cls_Ent_Solicitud_Personal();

                modelo.ID_SOLICITUD = ID;
                modelo = new SolicitudRepositorio().ListaSolicitudesCoordinador(modelo).FirstOrDefault();

                string nombreArchivo = "archivo";

                // Documento Sustentatorio
                Byte[] DocumentoSustentatorio = UtilLaserfiche.ExportarDocumentoPDF(Int32.Parse(modelo.ARCHIVO_PUESTO_SUS_SOLICITUD.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nombreArchivo, "");

                // Contrato
                Byte[] Contrato = UtilLaserfiche.ExportarDocumentoPDF(Int32.Parse(modelo.ID_ARCHIVO_CONTRATO.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nombreArchivo, "");

                // TDR
                Byte[] Tdr = UtilLaserfiche.ExportarDocumentoPDF(Int32.Parse(modelo.ID_ANEXO1.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nombreArchivo, "");
                
                // Declaración Jurada
                Byte[] DeclaracionJurada = UtilLaserfiche.ExportarDocumentoPDF(Int32.Parse(modelo.ID_ANEXO2.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nombreArchivo, "");

                // Verificar Impedimentos
                int idVerificarImpedimentos = 0;
                Byte[] VerificarImpedimentos = UtilLaserfiche.ExportarDocumentoPDF(idVerificarImpedimentos, ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nombreArchivo, "");
                
                // Datos Consultor
                Byte[] DatosConsultor = UtilLaserfiche.ExportarDocumentoPDF(Int32.Parse(modelo.ID_ANEXO3.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nombreArchivo, "");
                
                // Dni
                int idDni = 0;
                Byte[] Dni = UtilLaserfiche.ExportarDocumentoPDF(idDni, ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nombreArchivo, "");
                
                // Reporte Bancario
                Byte[] ReporteBancario = UtilLaserfiche.ExportarDocumentoPDF(Int32.Parse(modelo.ID_BANCO.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nombreArchivo, "");

                // Curriculum Vitae
                Byte[] CurriculumVitae = UtilLaserfiche.ExportarDocumentoPDF(Int32.Parse(modelo.ID_ANEXO7.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nombreArchivo, "");

                // Sustento Anexo 7
                List<int> sustentos = new List<int>();
                
                Cls_Ent_Estudios modelEstudios = new Cls_Ent_Estudios();
                modelEstudios.ID_SOLICITUD = ID;
                modelEstudios.ID_PERSONAL = 0;
                
                (new SolicitudRepositorio().ListaEstudios(modelEstudios).Select(x => x.ARCHIVO).ToList()).ForEach(x =>
                {
                    sustentos.Add(x);
                });

                Cls_Ent_Especializacion modelEspecializacion = new Cls_Ent_Especializacion();
                modelEspecializacion.ID_SOLICITUD = ID;
                modelEspecializacion.ID_PERSONAL = 0;
                (new SolicitudRepositorio().ListaEspecializacion(modelEspecializacion).Select(x => x.ARCHIVO).ToList()).ForEach(x =>
                {
                    sustentos.Add(x);
                });

                Cls_Ent_Experiencia_Laboral modelExperiencia = new Cls_Ent_Experiencia_Laboral();
                modelExperiencia.ID_SOLICITUD = ID;
                modelExperiencia.ID_PERSONAL = 0;
                (new SolicitudRepositorio().ListaExperienciaLaboral(modelExperiencia).Select(x => x.ARCHIVO).ToList()).ForEach(x =>
                {
                    sustentos.Add(x);
                });

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(DocumentoSustentatorio, 0, DocumentoSustentatorio.Length);
                    ms.Write(Contrato, 0, Contrato.Length);
                    ms.Write(Tdr, 0, Tdr.Length);
                    ms.Write(DeclaracionJurada, 0, DeclaracionJurada.Length);
                    //ms.Write(VerificarImpedimentos, 0, VerificarImpedimentos.Length);
                    ms.Write(DatosConsultor, 0, DatosConsultor.Length);
                    //ms.Write(Dni, 0, Dni.Length);
                    ms.Write(ReporteBancario, 0, ReporteBancario.Length);
                    ms.Write(CurriculumVitae, 0, CurriculumVitae.Length);

                    sustentos.ForEach(x =>
                    {
                        var archivo = UtilLaserfiche.ExportarDocumentoPDF(int.Parse(x.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nombreArchivo, "");
                        ms.Write(archivo, 0, archivo.Length);
                    });

                    byte[] doc = new byte[ms.Length];
                    var cd = new ContentDisposition
                    {
                        FileName = "Expediente Digital",
                        Inline = false
                    };
                    Response.AppendHeader("Content-Disposition", cd.ToString());
                    return await Task.FromResult(File(ms.ToArray(), MediaTypeNames.Application.Pdf));
                }
            }
            catch(Exception ex)
            {
                Log.MensajeLog(ex.ToString(), "SolicitudController.DescargarExpediente");
            }
            return await FormatoError();
        }
        #region REGISTRO DE CARGA DE ARCHIVOS MULTIPLE
        public ActionResult GenerarHTML_Multiple(Cls_Ent_Documento entidad)
        {
            var respuesta = new ResponseEntity();
            try
            {
                var ex = new JavaScriptSerializer().Deserialize<List<Cls_Ent_Documento>>(Request.Form["Items"]);
                entidad.Items = ex;
                var httpRequest = Request.Files;
                if (httpRequest.Count > 0)
                {
                    var files = Request.Files[0] as HttpPostedFileBase;
                    if (files.ContentLength > 0)
                    {
                        var carpeta = Server.MapPath("~//ArchivoTemp//Registro_Multiple//");
                        string extension = System.IO.Path.GetExtension(files.FileName).ToString();
                        string codigoArchivo = Guid.NewGuid().ToString();
                        entidad.RUTA_ARCHIVO = carpeta + codigoArchivo + extension;
                        entidad.NOM_DOCUMENTO = codigoArchivo;
                        using (Stream fileStream = new FileStream(entidad.RUTA_ARCHIVO, FileMode.Create))
                        {
                            files.InputStream.CopyTo(fileStream);
                        }
                    }
                }
                else
                {
                    respuesta.success = false;
                    respuesta.message = "Error al generar registrar el archivo";
                    return this.Respuesta(respuesta);
                }
                respuesta.extra = FilaTabla_Multiple(entidad);
                if (respuesta.extra == "error")
                {
                    respuesta.success = false;
                    respuesta.message = "Error al generar registrar el archivo";
                }
                else
                {
                    respuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.message = "Error al generar registrar el archivo";
                Log.MensajeLog(ex.ToString(), "Coordinador.SolicitudController.GenerarHTML_Multiple");
            }
       
            return this.Respuesta(respuesta);

        }
        public ActionResult GenerarHTML_Archivo(Cls_Ent_Documento entidad)
        {
            var respuesta = new ResponseEntity();
            try
            {
                respuesta.extra = FilaTabla_Multiple(entidad);
                if (respuesta.extra == "error")
                {
                    respuesta.success = false;
                    respuesta.message = "Error al generar registrar el archivo";
                }
                else
                {
                    respuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.message = "Error al generar registrar el archivo";
                Log.MensajeLog(ex.ToString(), "Coordinador.SolicitudController.GenerarHTML_Multiple");
            }

            return this.Respuesta(respuesta);

        }
        private string FilaTabla_Multiple(Cls_Ent_Documento modelo)
        {
            string html = "";
            try
            {
                var lista = new List<Cls_Ent_Documento>();
                var det = new SolicitudRepositorio().ListaDocumentos(modelo);
                if (modelo.Items != null)
                {
                    lista = modelo.Items.Count == 0 ? (det.Count == 0 ? new List<Cls_Ent_Documento>() : det) : modelo.Items;
                }
                else
                {
                    lista = det.Count == 0 ? new List<Cls_Ent_Documento>() : det;
                }
                if (modelo.ID_DOCUMENTO == 0)
                {
                    var entity_ = new Cls_Ent_Documento
                    {
                        ID_DOCUMENTO = modelo.ID_DOCUMENTO,
                        ID_PROCESO = modelo.ID_PROCESO,
                        FLG_TIPO = modelo.FLG_TIPO,
                        DES_DOCUMENTO = modelo.DES_DOCUMENTO,
                        ID_LF = 0,
                        RUTA_ARCHIVO = modelo.RUTA_ARCHIVO,
                        NOM_DOCUMENTO=modelo.NOM_DOCUMENTO
                    };
                    lista.Add(entity_);
                }
                int num = 0;
                if (lista.Count() > 0)
                {

                    foreach (var item in lista)
                    {
                        num += 1;
                        html += GrupoRegistro_Multiple(item.ID_DOCUMENTO, item.ID_PROCESO, item.FLG_TIPO, item.DES_DOCUMENTO, item.ID_LF, item.RUTA_ARCHIVO,item.NOM_DOCUMENTO, modelo.ID_DOCUMENTO, num);
                    }
                }
            }
            catch (Exception ex)
            {
                html = "error";
                Log.MensajeLog(ex.ToString(), "Coordinador.SolicitudController.FilaTabla_Multiple");

            }

            return html;

        }
        private string GrupoRegistro_Multiple(long ID_DOCUMENTO, long ID_PROCESO, string FLG_TIPO, string DES_DOCUMENTO, long ID_LF, string RUTA_ARCHIVO,string NOM_DOCUMENTO,long TIPO, int num)
        {
            
            string html = "";
            string remove = deleteFila(NOM_DOCUMENTO, ID_LF);
            string descarga = descargaArchivo(NOM_DOCUMENTO, ID_LF);
            html += "<tr>";
            html += "<td class='tdID_DOCUMENTO' style='display:none;'>" + ID_DOCUMENTO + "</td>";
            html += "<td class='tdID_PROCESO' style='display:none;'>" + ID_PROCESO + "</td>";
            html += "<td class='tdFLG_TIPO' style='display:none;'>" + FLG_TIPO + "</td>";
            html += "<td class='tdID_LF' style='display:none;'>" + ID_LF + "</td>";
            html += "<td class='tdNOM_DOCUMENTO' style='display:none;'>" + NOM_DOCUMENTO + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + num + "</td>";

            if (TIPO == 1 || TIPO == 0)
            {
                html += "<td style='text-align:center;font-size:24px'>" + remove + "</td>";
            }

            html += "<td style='text-align:center;font-size:24px'>" + descarga + "</td>";
            html += "<td>";
            if (TIPO == 1)
            {
                html += "<input type='text' style='font-weight:bold' class='form-control tdDES_DOCUMENTO' value='" + DES_DOCUMENTO + "'" + ">";
            }
            else
            {
                // html += "<label style='font-weight:bold' class='form-control'>" + DES_DOCUMENTO + "</label>";
                html += "<input type='text' style='font-weight:bold' readonly class='form-control tdDES_DOCUMENTO' value='" + DES_DOCUMENTO + "'" + ">";
            }

            html += "</td>";
            html += "</tr>";
            return html;
        }
        private string deleteFila(string NOM_DOCUMENTO, long ID_LF)
        {
            string html = "";
            if (ID_LF == 0)
            {
                html = "<a  href ='javascript:void(0);' style='color:#EB5767' title='Eliminar Archivo'><i class='far fa-trash-alt delete_archivo' idNomDoc=" + NOM_DOCUMENTO + "></i></a>";
            }
            else
            {
                html = "<a href ='javascript:void(0);' style='color:#EB5767' title='Eliminar Archivo'><i class='far fa-trash-alt delete_lf' idDoc=" + ID_LF + " ></i></a>";
            }
            return html;

        }
        private string descargaArchivo(string NOM_DOCUMENTO, long ID_LF)
        {
            string html = "";
            if (ID_LF == 0)
            {
                html = "<a  href ='javascript:void(0);' style='color:#84C157' title='Descargar Archivo'><i class='fas fa-file-pdf descarga_archivo' idNomDoc=" + NOM_DOCUMENTO + "></i></a>";
            }
            else
            {
                html = "<a  href ='javascript:void(0);' style='color:#84C157' title='Descargar Archivo'><i class='fas fa-file-pdf descarga_lf' idDoc=" + ID_LF + "></i></a>";
            }
            return html;

        }
        public async Task<ActionResult> ExportarDoc(string DOC)
        {
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            Byte[] bytes = null;
            try
            {
                var carpeta = Server.MapPath("~//ArchivoTemp//Registro_Multiple//");
                string ruta = carpeta+ DOC+".pdf";
                bytes = System.IO.File.ReadAllBytes(ruta);
                using (var stream = new System.IO.MemoryStream(bytes))
                {
                    byte[] buffer = new byte[stream.Length];
                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = Guid.NewGuid().ToString() + ".pdf",
                    Inline = false,
                    };
                    Response.AppendHeader("Content-Disposition", cd.ToString());
                    return await Task.FromResult(File(stream.ToArray(), MediaTypeNames.Application.Pdf));
                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), "SolicitudController.ExportarDoc");
            }
            return await FormatoError();
        }
        public ActionResult ElimiarDoc(Cls_Ent_Documento modelo)
        {
            var respuesta = new ResponseEntity();
            try
            {
               var carpeta = Server.MapPath("~//ArchivoTemp//Registro_Multiple//");
                string ruta = carpeta + modelo.NOM_DOCUMENTO + ".pdf";
                System.IO.File.Delete(ruta);
                    respuesta.success = true;
                respuesta.message = "El archivo se eliminó correctamente.";

            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.message = "Error al eliminar el archivo.";
                Log.MensajeLog(ex.ToString(), "Coordinador.SolicitudController.ElimiarDoc");
            }
            return this.Respuesta(respuesta);

        }
        public ActionResult ListaDocumentos(Cls_Ent_Documento entidad)
        {
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            List<Cls_Ent_Documento> lista;
            lista = new SolicitudRepositorio().ListaDocumentos(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult DocumentosSolicitud(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            return View(modelo);
        }
        #endregion
  
    }
}
