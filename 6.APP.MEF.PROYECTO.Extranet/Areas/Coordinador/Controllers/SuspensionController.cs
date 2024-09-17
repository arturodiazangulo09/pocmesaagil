using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Entity.Coordinador;
using APP.MEF.EXTRANET.FAG.PAG.Core;
using APP.MEF.EXTRANET.FAG.PAG.Response;
using MEF.PROYECTO.Utilitario;
using System.IO;
using System.Text;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Models;
using MEF.PROYECTO.Entity.Administracion;
using System.Configuration;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Controllers
{
    public class SuspensionController : BaseController
    {
        //
        // GET: /Coordinador/Suspension/
        StdModel Datos = new StdModel();
        WCF_STD22.hrDto Resultado = new WCF_STD22.hrDto();
        Cls_Ent_Coordinador UsuarioSistemaSesion = new Cls_Ent_Coordinador();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListaSolicitud_Suspension(Cls_Ent_Descanso entidad)
        {
            List<Cls_Ent_Descanso> lista;
            lista = new SolicitudDesancasoRepositorio().ListaSolicitud_Suspension(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult UpdateReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Reevaluacion PreguntaRspta = null;
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


                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                entidad.NOMBRE_COMPLETO = UsuarioSistemaSesion.APELLIDO_PATERNO + ' ' + UsuarioSistemaSesion.APELLIDO_MATERNO + ' ' + UsuarioSistemaSesion.NOMBRES; ;
                PreguntaRspta = new SolicitudDesancasoRepositorio().InsReevaluacionSuspension(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {

                    Cls_Ent_Descanso ent_susp = new Cls_Ent_Descanso();
                    ent_susp.ID_SUSPENSION = entidad.ID_SUSPENSION;
                    ent_susp = new SolicitudDesancasoRepositorio().ListaSolicitud_Suspension(ent_susp).First();
                    if (EnviarEmailCoordinadorObservadoSuspension(ent_susp, entidad))
                    {
                        itemRespuesta.message = "Se realizo la notificación al coordinador.";
                    }
                    else
                    {
                        itemRespuesta.message = "Error al realizar la notificación al coordinador, vuelva a intentarlo en unos minutos.";
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
        private bool EnviarEmailCoordinadorObservadoSuspension(Cls_Ent_Descanso entidad, Cls_Ent_Reevaluacion detalle)
        {

            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarObservacionSuspension.html"))
            {
                mensaje = sr.ReadToEnd();
            }
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
            mensaje = mensaje.Replace("{0}", entidad.CONSULTOR);
            mensaje = mensaje.Replace("{1}", "Del periodo del contrato desde " + entidad.FECHA_PERIODO_INICIO.ToShortDateString() + " hasta " + entidad.FECHA_PERIODO_FIN.ToShortDateString());
            mensaje = mensaje.Replace("{2}", "El periodo de suspensión desde " + entidad.FECHA_INICIO.ToShortDateString() + " hasta " + entidad.FECHA_FIN.ToShortDateString());
            mensaje = mensaje.Replace("{3}", detalle.DES_REEVALUACION);

            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.CORREO_CONSULTOR;
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
        public ActionResult ObservacionProfesional(int ID_SUSPENSION)
        {

            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_SUSPENSION = ID_SUSPENSION;
            return View(modelo);
        }
        public ActionResult ListaReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista;
            lista = new SolicitudDesancasoRepositorio().ListaReevaluacionSuspension(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult VerObservacionSuspension(int ID_SUSPENSION,string TIPO)
        {

            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_SUSPENSION = ID_SUSPENSION;
            modelo.TIPO = TIPO;
            return View(modelo);
        }
        public ActionResult VerSuspensionArchivo(int ID_SUSPENSION)
        {
            Cls_Ent_Descanso Modelo = new Cls_Ent_Descanso();
            Modelo.ID_SUSPENSION = ID_SUSPENSION;
            Modelo = new SolicitudDesancasoRepositorio().ListaSolicitud_Suspension(Modelo).First();
            return View(Modelo);
        }
        public ActionResult UpdArchivoSuspension(Cls_Ent_Descanso entidad)
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
            Cls_Ent_Descanso PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                var httpRequest = Request.Files;
                if (httpRequest.Count > 0)
                {
                    long IdLaserfiche = 0;
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    var filesArchivo = Request.Files[0] as HttpPostedFileBase;
                    using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(filesArchivo)))
                    {
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesArchivo.FileName.ToString());
                        filesArchivo.SaveAs(NombreArchivo);
                        string SubcarpetaLF = entidad.NUM_DOCUMENTO + "\\" + "SUSPENSION_CONTRATO_" + entidad.ID_CONTRATO;
                        nomArchivoSave = "DOCUMENTO_COORDINADOR_" + entidad.ID_CONTRATO + "_" + DateTime.Now.Month + "_" + RandomString(5, true) + ".pdf";
                        IdLaserfiche = new APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio.LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "CONTRATOS", SubcarpetaLF, nomArchivoSave, entidad.NUM_DOCUMENTO, entidad.IP_INGRESO);
                        if (IdLaserfiche == 0)
                        {
                            throw new Exception("Error al registrar la información del archivo.");
                        }
                        else
                        {
                            entidad.ID_ARCHIVO_C = int.Parse(IdLaserfiche.ToString());
                        }
                    }
                }

                PreguntaRspta = new SolicitudDesancasoRepositorio().UpdArchivoSuspension(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {
                    if (entidad.ACCION == "I")
                    {
                        itemRespuesta.message = "El archivo se registro correctamente.";
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
        public ActionResult UpdEnvioSuspension(Cls_Ent_Descanso entidad)
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
            Cls_Ent_Descanso PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                // PROCESO TRAMITE
                Cls_Ent_Descanso ent_susp = new Cls_Ent_Descanso();
                ent_susp.ID_SUSPENSION = entidad.ID_SUSPENSION;
                ent_susp = new SolicitudDesancasoRepositorio().ListaSolicitud_Suspension(ent_susp).First();
                Cls_Ent_Descanso ent_susp_temp = new Cls_Ent_Descanso();
                ent_susp_temp.ID_SUSPENSION = entidad.ID_SUSPENSION;
                List<Cls_Ent_Descanso> List = new SolicitudDesancasoRepositorio().ListaSolicitud_Suspension(ent_susp_temp);
                if (ent_susp.ID_TRAMITE == 0)
                {
                    var permite = ConfigurationManager.AppSettings["PermiteTramite"].ToString();
                    if (permite == "1")
                    {
                        if (entidad.FLG_CON_HR=="2")
                        {
                            entidad.ID_TRAMITE = 1;
                        }
                        else
                        {
                            Cls_Ent_Entidades entidad_desc = new Cls_Ent_Entidades();
                            entidad_desc.ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD;
                            entidad_desc = new CoordinadorRepositorio().ListaEntidades(entidad_desc).First();
                            Datos.NUM_REGISTRO = entidad.ID_SUSPENSION.ToString();
                            Datos.NUM_OFICIO = entidad.NR_OFICIO;//"1021-2022/GR-AREQUIPA";
                            Datos.NUM_FOLIOS = entidad.NR_FOLIOS;
                            Datos.ASUNTO = entidad.ASUNTO;//"SOLICITA LISTA DE EXPEDIENTES";
                            Datos.CLASIFICACION = "SOLICITUD DE SUSPENSIÓN";
                            Datos.RAZONSOCIAL = UsuarioSistemaSesion.DESC_ENTIDAD_PRINCIPAL;
                            Datos.RUC = entidad_desc.RUC;
                            Datos.SEC_EJECT = "";
                            Datos.DIRECCION = entidad_desc.DIRECCION;
                            Datos.DEPARTAMENTO = entidad_desc.DES_DEPARTAMENTO;
                            Datos.PROVINCIA = entidad_desc.DES_PROVINCIA;
                            Datos.DISTRITO = entidad_desc.DES_DISTRITO;
                            Datos.CORREO = UsuarioSistemaSesion.CORREO_NOTIFICADOR;
                            Datos.OBSERVACION = "SIN OBSERVACIONES";
                            try
                            {
                                using (StdRepositorio xx = new StdRepositorio())
                                {
                                    // byte[] archivo = UtilLaserfiche.ExportarDocumentoPDF(1653, ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", "", "");
                                    var files = Request.Files[0] as HttpPostedFileBase;
                                    var tempFile = new byte[files.ContentLength];
                                    files.InputStream.Read(tempFile, 0, files.ContentLength);

                                    var files2 = Request.Files[1] as HttpPostedFileBase;
                                    var tempFile2 = new byte[files2.ContentLength];
                                    files2.InputStream.Read(tempFile2, 0, files2.ContentLength);

                                    Datos.OFICIO = xx.Archivo_Adjunto(tempFile, "OFICIO.PDF", tempFile.Length);
                                    Datos.RESUMEN = xx.Archivo_Adjunto(tempFile2, "RESUMEN.PDF", tempFile2.Length);
                                    WCF_STD22.anexoDto anexoDto = null;
                                    List<WCF_STD22.anexoDto> intermediate_list = new List<WCF_STD22.anexoDto>();
                                    foreach (var item in List)
                                    {
                                        anexoDto = new WCF_STD22.anexoDto();
                                        string nom = "";
                                        anexoDto.archivo = UtilLaserfiche.ExportarDocumentoPDF(int.Parse(item.ID_ARCHIVO_C.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nom, "");
                                        anexoDto.name = "DOCUMENTO_SUSTENTO.pdf";
                                        anexoDto.length = anexoDto.archivo.Length;
                                        intermediate_list.Add(anexoDto);
                                    }
                                    Datos.ANEXOS = intermediate_list.ToArray();
                                    Datos.REMOTEADDRESS = entidad.IP_PC;
                                    Resultado = xx.Crear_Expediente_Std(Datos);
                                }

                                if (Resultado.iddoc > 0)
                                {
                                    entidad.ID_TRAMITE = int.Parse(Resultado.iddoc.ToString());
                                    entidad.NR_TRAMITE = Resultado.numeroSid + '-' + Resultado.numeroAnio;
                                    itemRespuesta.extra2 = "1";
                                    itemRespuesta.message2 = "Se genero el tramite " + entidad.NR_TRAMITE;
                                }
                                else
                                {
                                    throw new Exception("No se pudo generar la Hoja de Ruta, intentar en unos minutos.");
                                }
                            }
                            catch (Exception ex)
                            {

                                throw new Exception(ex.ToString());
                            }
                        }
                    }
                    else
                    {
                        entidad.ID_TRAMITE = 0;
                        entidad.NR_TRAMITE = "";
                    }
                }
                else
                {
                    entidad.ID_TRAMITE = ent_susp.ID_TRAMITE;
                    entidad.NR_TRAMITE = ent_susp.NR_TRAMITE;

                }
                // FIN PROCESO TRAMITE 
                PreguntaRspta = new SolicitudDesancasoRepositorio().UpdEnvioSuspension(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {
                
                  
                    if (EnviarEmailAccionUtp(ent_susp))
                        
                        {

                            itemRespuesta.message = "Se realizó la notificación a la oficina de UTP del Ministerio Economía y Finanzas.";
                        }
                        else
                        {
                            itemRespuesta.message = "Error al realizar la notificación.";
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
        private bool EnviarEmailAccionUtp(Cls_Ent_Descanso periodo)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarUTP_Suspension.html"))
            {
                mensaje = sr.ReadToEnd();
            }
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
            mensaje = mensaje.Replace("{0}", UsuarioSistemaSesion.APELLIDO_PATERNO + " " + UsuarioSistemaSesion.APELLIDO_MATERNO + " " + UsuarioSistemaSesion.NOMBRES);
            mensaje = mensaje.Replace("{1}", UsuarioSistemaSesion.DESC_ENTIDAD);
           mensaje = mensaje.Replace("{2}", periodo.CODIGO_CONTRATO);
          mensaje = mensaje.Replace("{3}", "El periodo de suspensión desde " + periodo.FECHA_INICIO.ToShortDateString() + " hasta " + periodo.FECHA_FIN.ToShortDateString());
            mensaje = mensaje.Replace("{4}", periodo.CONSULTOR);
            mensaje = mensaje.Replace("{5}", periodo.DENOMINACION_PUESTO);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = ConfigurationManager.AppSettings["CorreoCopia"];
            bool strRet = EnviarMail.SendMailMessage(ref strMsgError, destinatarios, "", "", titulo, mensaje, "", ConfigurationManager.AppSettings["Usermail"]);

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
        public ActionResult Ver_Integrar_STD(int ID_SUSPENSION)
        {
            Cls_Ent_Descanso modelo = new Cls_Ent_Descanso();
            modelo.ID_SUSPENSION = ID_SUSPENSION;
            return View(modelo);
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
    }
}
