using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Coordinador;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using MEF.PROYECTO.Utilitario;
using APP.MEF.EXTRANET.FAG.PAG.Core;
using APP.MEF.EXTRANET.FAG.PAG.Response;
using System.IO;
using System.Text;
using System.Configuration;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Controllers
{
    public class AltasBajasController : Controller
    {
        //
        // GET: /Coordinador/AltasBajas/
        Cls_Ent_Coordinador UsuarioSistemaSesion = new Cls_Ent_Coordinador();
        public ActionResult Index()
        {
        
            return View();
        }
        public ActionResult ListaContraAdendaPersona(Cls_Ent_Lista_Contr_Aden_Consultor entidad)
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
            List<Cls_Ent_Lista_Contr_Aden_Consultor> lista;
            lista = new SolicitudRepositorio().ListaContraAdendaPersona(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult UpdateBajaAnulacion(Cls_Ent_Lista_Contr_Aden_Consultor entidad)
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
            Cls_Ent_Lista_Contr_Aden_Consultor PreguntaRspta = null;
            try
            {
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                var httpRequest = Request.Files;
                if (httpRequest.Count >0)
                {
                    long IdLaserfiche = 0;
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    var filesArchivo = Request.Files[0] as HttpPostedFileBase;
                    using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(filesArchivo)))
                    {
                        string nomArchivoSave = "";
                        string NombreArchivo = Path.Combine(carpeta, filesArchivo.FileName.ToString());
                        filesArchivo.SaveAs(NombreArchivo);
                        string SubcarpetaLF = entidad.NUM_DOCUMENTO + "\\" + "ALTAS_BAJAS_CONTRATO_" + entidad.ID_CONTRATO;
                        nomArchivoSave = "COORDINADOR_ALTAS_BAJAS_" + entidad.ID_CONTRATO + "_" + DateTime.Now.Month + "_" + RandomString(5, true) + ".pdf";
                        IdLaserfiche = new APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio.LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "CONTRATOS", SubcarpetaLF, nomArchivoSave, UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                        if (IdLaserfiche == 0)
                        {
                            throw new Exception("Error al registrar la información del archivo.");
                        }
                        else
                        {
                            entidad.ID_ARCHIVO_A_B = int.Parse(IdLaserfiche.ToString());
                        }
                    }

                }
                PreguntaRspta = new SolicitudRepositorio().UpdateBajaAnulacion(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                    if (EnviarEmailBajaAnulacion(entidad))
                    {

                        itemRespuesta.message = "Se realizó la notificación a la oficina de UTP del Ministerio Economía y Finanzas.";
                    }
                    else
                    {
                        itemRespuesta.message = "Error al realizar la notificación.";
                    }
                    itemRespuesta.extra = entidad.ID_CONTRATO.ToString();
                    itemRespuesta.success = true;
                }

            }
            catch (Exception ex)
            {
                itemRespuesta.success = false;
                itemRespuesta.extra =Log.MensajeInterno() + "<br/>"+ ex.ToString();
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObservacionAltaBaja(int ID_CONTRATO, string TIPO, string NUM_DOCUMENTO)
        {
            Cls_Ent_Lista_Contr_Aden_Consultor modelo = new Cls_Ent_Lista_Contr_Aden_Consultor();
            modelo.ID_CONTRATO = ID_CONTRATO;
            modelo.TIPO = TIPO;
            modelo.NUM_DOCUMENTO = NUM_DOCUMENTO;
            return View(modelo);
        }
        private bool EnviarEmailBajaAnulacion(Cls_Ent_Lista_Contr_Aden_Consultor entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarAnularBajaContrato.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            if (entidad.TIPO == "C_B" || entidad.TIPO == "C_A")
            {
                Cls_Ent_Contrato ent = new Cls_Ent_Contrato();
                ent.ID_CONTRATO = entidad.ID_CONTRATO;
                ent = new AdendaRepositorio().ListaContratos(ent).First();
                mensaje = mensaje.Replace("{0}", ent.COORDINADOR);
                mensaje = mensaje.Replace("{1}", ent.DESC_ENTIDAD);
                if (entidad.TIPO == "C_B")
                {
                    mensaje = mensaje.Replace("{2}", "dar de baja al consultor de acuerdo a lo siguiente");
                }
                else
                {
                    mensaje = mensaje.Replace("{2}", "anulación de contrato de acuerdo a lo siguiente");
                }
                mensaje = mensaje.Replace("{3}", ent.DENOMINACION_PUESTO);
                mensaje = mensaje.Replace("{4}", ent.CODIGO_CONTRATO);
                mensaje = mensaje.Replace("{5}", "-");
                mensaje = mensaje.Replace("{6}", ent.CONSULTOR);
                mensaje = mensaje.Replace("{7}", entidad.FECHA_BAJA.ToString("dd/MM/yyyy"));
                mensaje = mensaje.Replace("{8}", entidad.DESCRIPCION);
            }
            else
            {
                Cls_Ent_Adenda ent = new Cls_Ent_Adenda();
                ent.ID_CONTRATO_DET = entidad.ID_CONTRATO;
                ent = new AdendaRepositorio().ListaDetalleContratos(ent).First();
                mensaje = mensaje.Replace("{0}", ent.COORDINADOR);
                mensaje = mensaje.Replace("{1}", ent.DESC_ENTIDAD);
                if (entidad.TIPO == "A_B")
                {
                    mensaje = mensaje.Replace("{2}", "dar de baja al consultor de acuerdo a lo siguiente");
                }
                else
                {
                    mensaje = mensaje.Replace("{2}", "anulación de contrato de acuerdo a lo siguiente");
                }
                mensaje = mensaje.Replace("{3}", ent.NOMBRE_PUESTO);
                mensaje = mensaje.Replace("{4}", ent.CODIGO_CONTRATO);
                mensaje = mensaje.Replace("{5}", ent.CODIGO_ADENDA);
                mensaje = mensaje.Replace("{6}", ent.CONSULTOR);
                mensaje = mensaje.Replace("{7}", entidad.FECHA_BAJA.ToString("dd/MM/yyyy"));
                mensaje = mensaje.Replace("{8}", entidad.DESCRIPCION);
            }


        
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
        public ActionResult ListaPeriodoPagoEntidad(Cls_Periodo_Pago_Entidad entidad)
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
            List<Cls_Periodo_Pago_Entidad> lista;
            lista = new PeriodoPagoEntidadRepositorio().ListaPeriodoPagoEntidad(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
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
