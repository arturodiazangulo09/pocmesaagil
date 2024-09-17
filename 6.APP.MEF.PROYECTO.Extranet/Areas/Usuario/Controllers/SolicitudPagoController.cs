using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Coordinador;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio;
using MEF.PROYECTO.Entity.Personal;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using MEF.PROYECTO.Utilitario;
using APP.MEF.EXTRANET.FAG.PAG.Core;
using APP.MEF.EXTRANET.FAG.PAG.Response;
using System.Configuration;
using System.IO;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Controllers
{
    public class SolicitudPagoController : BaseController
    {
        //
        // GET: /Usuario/SolicitudPago/
        Cls_Ent_Personal PersonalSistemaSesion = new Cls_Ent_Personal();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GenerarSolicitudPago_Fag(int ID_SOLICITUD)
        {
            Cls_Ent_Renumeracion Modelo = new Cls_Ent_Renumeracion();
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            return View(Modelo);
        }
        public ActionResult GenerarSolicitudPago_Pac(int ID_SOLICITUD)
        {
            Cls_Ent_Renumeracion Modelo = new Cls_Ent_Renumeracion();
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            return View(Modelo);
        }
        public ActionResult RegistroPago_Fag(int ID_SOLICITUD, int ID_MES)
        {
            Cls_Ent_Renumeracion Modelo = new Cls_Ent_Renumeracion();
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            Modelo = new SolicitudPagoRepositorio().ListaPeriodoPagoSolicitud(Modelo).First(A => A.NUM_MES == ID_MES);
            Cls_Ent_Solicitud_Personal Ent = new Cls_Ent_Solicitud_Personal();
            Ent.ID_SOLICITUD = ID_SOLICITUD;
            Ent = new PersonalReposiorio().ListasolicitudPersonal(Ent).First();
            ViewBag.ENTIDAD = Ent.ENTIDAD;
            ViewBag.CONSULTOR = Ent.APELLIDO_PATERNO + " " + Ent.APELLIDO_MATERNO + " " + Ent.NOMBRES;
            ViewBag.NR_CONTRATO = Ent.NUM_CONTRATO_TEXT;//NUM_CONTRATO + "-" + Ent.ANIO_CONTRATO;
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            Modelo.NUM_MES = ID_MES;
            Modelo.DES_MES = Modelo.DES_MES + "-" + Ent.ANIO_CONTRATO;
            return View(Modelo);
        }
        public ActionResult RegistroPago_Pac(int ID_SOLICITUD, int ID_MES)
        {
            Cls_Ent_Renumeracion Modelo = new Cls_Ent_Renumeracion();
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            Modelo = new SolicitudPagoRepositorio().ListaPeriodoPagoSolicitud(Modelo).First(A=>A.NUM_MES==ID_MES);
            Cls_Ent_Solicitud_Personal Ent = new Cls_Ent_Solicitud_Personal();
            Ent.ID_SOLICITUD = ID_SOLICITUD;
            Ent = new PersonalReposiorio().ListasolicitudPersonal(Ent).First();
            ViewBag.ENTIDAD =Ent.ENTIDAD;
            ViewBag.CONSULTOR =Ent.APELLIDO_PATERNO +" "+ Ent.APELLIDO_MATERNO +" "+ Ent.NOMBRES;
            ViewBag.NR_CONTRATO = Ent.NUM_CONTRATO_TEXT;//NUM_CONTRATO +"-"+ Ent.ANIO_CONTRATO;
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            Modelo.NUM_MES = ID_MES;
            Modelo.DES_MES = Modelo.DES_MES + "-" + Ent.ANIO_CONTRATO;
            return View(Modelo);
        }
        public ActionResult ListaPeriodoPagoSolicitud(Cls_Ent_Renumeracion entidad)
        {
            List<Cls_Ent_Renumeracion> lista;
            lista = new SolicitudPagoRepositorio().ListaPeriodoPagoSolicitud(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult UpdateReciboHonorario(Cls_Ent_Renumeracion entidad)
        {
            if (Session["Usuario"] != null)
            {
                PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_personal = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_Personal(id_personal))
                {
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                }
            }
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Renumeracion PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = PersonalSistemaSesion.USUARIO;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                Cls_Ent_Renumeracion x = new Cls_Ent_Renumeracion();
                x.ID_SOLICITUD = entidad.ID_SOLICITUD;
                x.NUM_MES = entidad.NUM_MES;
                x = new SolicitudPagoRepositorio().ListaPeriodoPagoSolicitud(x).First(A=> A.NUM_MES==x.NUM_MES);
                var httpRequest = Request.Files;
                if (httpRequest.Count > 0)
                {
                    long IdLaserfiche_RXH = 0;
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSave = "";
                    var filesArchivoRXH = Request.Files["FileArchivoRXH"] as HttpPostedFileBase;
                    if (filesArchivoRXH != null)
                    {
                        using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(filesArchivoRXH)))
                        {
                            string NombreArchivo = Path.Combine(carpeta, filesArchivoRXH.FileName.ToString());
                            filesArchivoRXH.SaveAs(NombreArchivo);
                            string SubcarpetaLF = "SOLICITUD_" + entidad.ID_SOLICITUD + "\\" + "MES_" + entidad.NUM_MES;
                            nomArchivoSave = "RECIBO_HONORARIO_" + entidad.NUM_MES + "_" + RandomString(5, true) + ".pdf";
                            IdLaserfiche_RXH = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "SOLICITUD_PAGO_" + DateTime.Now.Year.ToString(), SubcarpetaLF, nomArchivoSave, PersonalSistemaSesion.USUARIO, entidad.IP_PC);
                            if (IdLaserfiche_RXH == 0)
                            {
                                throw new Exception("Error al registrar la información del archivo.");
                            }
                            else
                            {
                                entidad.ID_ARCHIVO_RECIBO = IdLaserfiche_RXH;
                            }
                        }
                    }
                    else
                    {
                        entidad.ID_ARCHIVO_RECIBO = x.ID_ARCHIVO_RECIBO;
                    }

                    var filesCP = Request.Files["FileArchivoCP"] as HttpPostedFileBase;
                    long IdLaserfiche_CP = 0;
                    if (filesCP != null)
                    {
                        using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(filesCP)))
                        {
                            string NombreArchivo = Path.Combine(carpeta, filesCP.FileName.ToString());
                            filesCP.SaveAs(NombreArchivo);
                            string SubcarpetaLF = "SOLICITUD_" + entidad.ID_SOLICITUD + "\\" + "MES_" + entidad.NUM_MES;
                            nomArchivoSave = "COMPROBANTE_PAGO_" + entidad.NUM_MES + "_" + RandomString(5, true) + ".pdf";
                            IdLaserfiche_CP = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "SOLICITUD_PAGO_" + DateTime.Now.Year.ToString(), SubcarpetaLF, nomArchivoSave, PersonalSistemaSesion.USUARIO, entidad.IP_PC);
                            if (IdLaserfiche_CP == 0)
                            {
                                throw new Exception("Error al registrar la información del archivo.");
                            }
                            else
                            {
                                entidad.ID_ARCHIVO_CPE = IdLaserfiche_CP;
                            }
                        }
                    }
                    else
                    {
                        entidad.ID_ARCHIVO_CPE = x.ID_ARCHIVO_CPE;
                    }

                    var filesINFORME = Request.Files["FileArchivoINFORME"] as HttpPostedFileBase;
                    long IdLaserfiche_INFORME = 0;
                    if (filesINFORME != null)
                    {
                        using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(filesINFORME)))
                        {
                            string NombreArchivo = Path.Combine(carpeta, filesINFORME.FileName.ToString());
                            filesINFORME.SaveAs(NombreArchivo);
                            string SubcarpetaLF = "SOLICITUD_" + entidad.ID_SOLICITUD + "\\" + "MES_" + entidad.NUM_MES;
                            nomArchivoSave = "INFORME_DE_PAGO_" + entidad.NUM_MES + "_" + RandomString(5, true) + ".pdf";
                            IdLaserfiche_INFORME = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "SOLICITUD_PAGO_" + DateTime.Now.Year.ToString(), SubcarpetaLF, nomArchivoSave, PersonalSistemaSesion.USUARIO, entidad.IP_PC);
                            if (IdLaserfiche_INFORME == 0)
                            {
                                throw new Exception("Error al registrar la información del archivo.");
                            }
                            else
                            {
                                entidad.ID_ARCHIVO_INFORME = IdLaserfiche_INFORME;
                            }
                        }
                    }
                    else
                    {
                        entidad.ID_ARCHIVO_INFORME = x.ID_ARCHIVO_INFORME;
                    }
                }
                else
                {
                    entidad.ID_ARCHIVO_RECIBO = x.ID_ARCHIVO_RECIBO;
                    entidad.ID_ARCHIVO_CPE = x.ID_ARCHIVO_CPE;
                    entidad.ID_ARCHIVO_INFORME = x.ID_ARCHIVO_INFORME;
                    
                }
                PreguntaRspta = new SolicitudPagoRepositorio().UpdateReciboHonorario(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {
                    itemRespuesta.message = "El registro se realizo correctamente.";
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
        public ActionResult UpdateReciboEstado(Cls_Ent_Renumeracion entidad)
        {
            if (Session["Usuario"] != null)
            {
                PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_personal = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_Personal(id_personal))
                {
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                }
            }
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Renumeracion PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = PersonalSistemaSesion.USUARIO;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                PreguntaRspta = new SolicitudPagoRepositorio().UpdateReciboEstado(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {
                    if (entidad.IDEDOCODIGO== "011")
                    {
                        Cls_Ent_Coordinador xx = new Cls_Ent_Coordinador();
                        Cls_Ent_Solicitud_Personal x = new Cls_Ent_Solicitud_Personal();
                        x.ID_SOLICITUD = entidad.ID_SOLICITUD;
                        x = new PersonalReposiorio().ListasolicitudPersonal(x).First();
                        xx = new CoordinadorRepositorio().ListaCoordinadores(xx).First(A => A.NUM_DOCUMENTO == x.USER_COORDINADOR);
                        if (EnviarEmailSolicitudPago(entidad.DES_MES,xx, x))
                        {
                            itemRespuesta.message = "La solicitud de pago se envio correctamente.";
                        }
                        else
                        {
                            itemRespuesta.message = "No se pudo realizar la notificación.";
                        }
                    }


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
        private bool EnviarEmailSolicitudPago(String mes, Cls_Ent_Coordinador entidad, Cls_Ent_Solicitud_Personal solicitud)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarSolicitudPago.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            if (Session["Usuario"] != null)
            {
                PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_personal = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_Personal(id_personal))
                {
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                }
            }
            mensaje = mensaje.Replace("{0}", PersonalSistemaSesion.APELLIDO_PATERNO + " " + PersonalSistemaSesion.APELLIDO_MATERNO + " " + PersonalSistemaSesion.NOMBRES);
            mensaje = mensaje.Replace("{1}", PersonalSistemaSesion.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", mes);
            mensaje = mensaje.Replace("{3}", solicitud.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{4}", solicitud.ENTIDAD);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.CORREO_NOTIFICADOR;
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
