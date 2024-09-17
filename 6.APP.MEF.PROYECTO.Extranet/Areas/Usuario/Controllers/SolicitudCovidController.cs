using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP.MEF.EXTRANET.FAG.PAG.Core;
using APP.MEF.EXTRANET.FAG.PAG.Response;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Utilitario;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio;
//using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using MEF.PROYECTO.Entity.Coordinador;
using System.Configuration;
using System.IO;
using System.Text;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Controllers
{
    public class SolicitudCovidController : BaseController
    {
        //
        // GET: /Usuario/SolicitudCovid/
        Cls_Ent_Personal PersonalSistemaSesion = new Cls_Ent_Personal();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GenerarSolicitudCovid(int ID_CONTRATO)
        {
            Cls_Ent_Descanso Modelo = new Cls_Ent_Descanso();
            Cls_Ent_Descanso xx = new Cls_Ent_Descanso();
            xx.ID_CONTRATO = ID_CONTRATO;
            using (SolicitudDesancasoRepositorio repositorio = new SolicitudDesancasoRepositorio())
            {
                xx = repositorio.ListaDetalle_contrato(xx).First();
            }

            Modelo.ID_CONTRATO = ID_CONTRATO;
            Modelo.CODIGO_CONTRATO = xx.CODIGO_CONTRATO;
            Modelo.FECHA_INICIO_CONTRATO = xx.FECHA_INICIO_CONTRATO;
            Modelo.FECHA_FIN_CONTRATO = xx.FECHA_FIN_CONTRATO;
            return View(Modelo);
        }
        public ActionResult CrearSolicitudCovid(int ID_COVID)
        {
            Cls_Ent_Covid Modelo = new Cls_Ent_Covid();
            Modelo.ACCION = "I";
            if (ID_COVID > 0)
            {

                Modelo.ID_COVID = ID_COVID;
                Modelo = new SolicitudCovidRepositorio().ListaSolicitud_Covid(Modelo).First();
                Modelo.ACCION = "U";
            }
            Modelo.ID_COVID = ID_COVID;
            return View(Modelo);
        }
        public ActionResult MentenimientoSolicitud_Covid(Cls_Ent_Covid entidad)
        {
            if (Session["Usuario"] != null)
            {
                PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_personal = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio.ReconectaUsuario().ReconectaUsuario_Personal(id_personal))
                {
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                }
            }
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Covid PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = PersonalSistemaSesion.USUARIO;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
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
                        string SubcarpetaLF = PersonalSistemaSesion.USUARIO + "\\" + "DESCANSO_COVID_CONTRATO_" + entidad.ID_CONTRATO;
                        nomArchivoSave = "DOCUMENTO_COVID_" + entidad.ID_CONTRATO + "_" + DateTime.Now.Month + "_" + RandomString(5, true) + ".pdf";
                        IdLaserfiche = new APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio.LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "CONTRATOS", SubcarpetaLF, nomArchivoSave, PersonalSistemaSesion.USUARIO, entidad.IP_PC);
                        if (IdLaserfiche == 0)
                        {
                            throw new Exception("Error al registrar la información del archivo.");
                        }
                        else
                        {
                            entidad.ID_ARCHIVO_U = int.Parse(IdLaserfiche.ToString());
                        }
                    }
                    if (httpRequest.Count > 1)
                    {
                        var filesArchivoCertificado = Request.Files[1] as HttpPostedFileBase;
                        using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(filesArchivoCertificado)))
                        {
                            string nomArchivoSaveCertificado = "";
                            string NombreArchivoCertificado = Path.Combine(carpeta, filesArchivoCertificado.FileName.ToString());
                            filesArchivoCertificado.SaveAs(NombreArchivoCertificado);
                            string SubcarpetaLF = PersonalSistemaSesion.USUARIO + "\\" + "DESCANSO_COVID_CONTRATO_" + entidad.ID_CONTRATO;
                            nomArchivoSaveCertificado = "DOCUMENTO_COVID_" + entidad.ID_CONTRATO + "_" + DateTime.Now.Month + "_" + RandomString(5, true) + ".pdf";
                            IdLaserfiche = new APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio.LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivoCertificado, "CONTRATOS", SubcarpetaLF, nomArchivoSaveCertificado, PersonalSistemaSesion.USUARIO, entidad.IP_PC);
                            if (IdLaserfiche == 0)
                            {
                                throw new Exception("Error al registrar la información del archivo.");
                            }
                            else
                            {
                                entidad.ID_ARCHIVO_CM = int.Parse(IdLaserfiche.ToString());
                            }
                        }
                    }
                


                }

                PreguntaRspta = new SolicitudCovidRepositorio().MentenimientoSolicitud_Covid(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {
                    switch (entidad.ACCION)
                    {
                        case "I":
                            itemRespuesta.message = "El registro se realizo correctamente.";
                            break;
                        case "U":
                            itemRespuesta.message = "El registro se actualizo realizo correctamente.";
                            break;
                        case "D":
                            itemRespuesta.message = "El registro se elimino correctamente.";
                            break;
                    }
                    itemRespuesta.extra = entidad.ID_COVID.ToString();
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
        public ActionResult ListaSolicitud_Covid(Cls_Ent_Covid entidad)
        {
            List<Cls_Ent_Covid> lista;
            lista = new SolicitudCovidRepositorio().ListaSolicitud_Covid(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult UpdEstado_Covid(Cls_Ent_Covid entidad)
        {
            if (Session["Usuario"] != null)
            {
                PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_personal = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio.ReconectaUsuario().ReconectaUsuario_Personal(id_personal))
                {
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                }
            }
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Covid PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = PersonalSistemaSesion.USUARIO;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();

                PreguntaRspta = new SolicitudCovidRepositorio().UpdEstado_Covid(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {
                    Cls_Ent_Contrato x = new Cls_Ent_Contrato();
                    Cls_Ent_Covid xx = new Cls_Ent_Covid();
                    xx.ID_COVID = entidad.ID_COVID;
                    xx = new SolicitudCovidRepositorio().ListaSolicitud_Covid(xx).First();
                    x.ID_CONTRATO = entidad.ID_CONTRATO;
                    x = new SolicitudDesancasoRepositorio().ListaContratos(x).First();
                    if (EnviarEmailSolicitudCovid(xx, x))
                    {
                        itemRespuesta.message = "La solicitud  se envio correctamente.";
                    }
                    else
                    {
                        itemRespuesta.message = "No se pudo realizar la notificación.";
                    }
                    itemRespuesta.extra = entidad.ID_COVID.ToString();
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
        private bool EnviarEmailSolicitudCovid(Cls_Ent_Covid entidad, Cls_Ent_Contrato con)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarSolicitudCovid.html"))
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
                if (new APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio.ReconectaUsuario().ReconectaUsuario_Personal(id_personal))
                {
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                }
            }
            mensaje = mensaje.Replace("{0}", PersonalSistemaSesion.APELLIDO_PATERNO + " " + PersonalSistemaSesion.APELLIDO_MATERNO + " " + PersonalSistemaSesion.NOMBRES);
            mensaje = mensaje.Replace("{1}", PersonalSistemaSesion.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", entidad.CODIGO_CONTRATO);
            //mensaje = mensaje.Replace("{3}", "Del periodo del contrato desde " + entidad.FECHA_PERIODO_INICIO.ToShortDateString() + " hasta " + entidad.FECHA_PERIODO_FIN.ToShortDateString());
            mensaje = mensaje.Replace("{4}", "Desde " + entidad.FECHA_INICIO.ToShortDateString() + " hasta " + entidad.FECHA_FIN.ToShortDateString());
            mensaje = mensaje.Replace("{5}", con.DENOMINACION_PUESTO);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = con.EMAIL_COORDINADOR;
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
        public ActionResult ListaReevaluacionCovid(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista;
            lista = new SolicitudCovidRepositorio().ListaReevaluacionCovid(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult VerObservacionCovid(int ID_COVID)
        {

            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_COVID = ID_COVID;
            modelo.TIPO = "C";
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
