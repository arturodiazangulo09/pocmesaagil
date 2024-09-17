using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP.ADMINISTRAR.FAG.PAG.Response;
using APP.ADMINISTRAR.FAG.PAG.Core;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes.Repositorio;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes.Models;
using System.IO;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Configuration;
using MEF.PROYECTO.Utilitario;
//using MEF.PROYECTO.BusinessLayer.Personal;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Repositorio;
using APP.MEF.ADMINISTRAR.FAG.PAG.Response;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Net;
using APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad;
using static MEF.PROYECTO.Utilitario.Constants;
using System.Web.UI.WebControls;
using APP.MEF.ADMINISTRAR.FAG.PAG.WCF_AIRSH;
using System.Security.Policy;
using Antlr.Runtime.Misc;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Messaging;
using System.Web.Services.Description;
using static iTextSharp.text.pdf.events.IndexEvents;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes.Controllers
{
    public class SolicitudesController : BaseController
    {
        //
        // GET: /Solicitudes/Solicitudes/
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario();
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario_result = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();

        public ActionResult Index()
        {
            List<Cls_Ent_Entidades> lista = null;
            var user = "";
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
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            xx.EVALUADOR = user.ToUpper();
            lista = Cls_Rule_Entidades.ListaEntidadesEvaluador(xx);
            lista = lista.OrderBy(A => A.DESC_UNIDAD).ToList();
            lista.Insert(0, new Cls_Ent_Entidades() { ID_ENTIDAD = 1000, DESC_UNIDAD = "--Seleccione--" });
            ViewBag.DcboEntidades = lista;
            return View();
        }
        public ActionResult ListaSolicitudesCoordinador(Cls_Ent_Solicitud_Personal entidad)
        {
            List<Cls_Ent_Solicitud_Personal> lista;
            lista = new SolicitudesCoordinadorRepositorio().ListaSolicitudesCoordinador(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult Ver_Anexos_Solicitud(int ID_SOLICITUD, string TIPO)
        {
            Cls_Ent_Solicitud_Personal modelo = new Cls_Ent_Solicitud_Personal();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.IDEDOCODIGO = "";
            modelo.TIPO_PROCESO = "";
            modelo = new SolicitudesCoordinadorRepositorio().ListaSolicitudesCoordinador(modelo).FirstOrDefault();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.ACCION = TIPO;
            modelo.ListaCalificacion = new List<SelectListItem>();
            modelo.ListaCalificacion.Add(new SelectListItem { Value = "", Text = "----------",Selected=true });
            modelo.ListaCalificacion.Add(new SelectListItem { Value = "0", Text = "OBSERVADO" });
            modelo.ListaCalificacion.Add(new SelectListItem { Value = "1", Text = "APROBADO" });
           
            modelo.ListaCumpleRequisitos = new List<SelectListItem>();
            modelo.ListaCumpleRequisitos.Add(new SelectListItem { Value = "", Text = "-------", Selected = true });
            modelo.ListaCumpleRequisitos.Add(new SelectListItem { Value = "0", Text = "NO CUMPLE" });
            modelo.ListaCumpleRequisitos.Add(new SelectListItem { Value = "1", Text = "CUMPLE" });
            return View(modelo);
        }
        //INICIO CV
        public ActionResult ListaEstudios(Cls_Ent_Estudios entidad)
        {
            List<Cls_Ent_Estudios> lista;
            lista = new SolicitudesCoordinadorRepositorio().ListaEstudios(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult MentenimientoEstudios_Verificar(Cls_Ent_Estudios entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Estudios PreguntaRspta = null;
            var user = "";
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
            entidad.USU_INGRESO = user;
            entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new SolicitudesCoordinadorRepositorio().MentenimientoEstudios_Verificar(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.success = true;
                itemRespuesta.message = "Se realizo Correctamente.";

            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ListaEspecializacion(Cls_Ent_Especializacion entidad)
        {
            List<Cls_Ent_Especializacion> lista;
            lista = new SolicitudesCoordinadorRepositorio().ListaEspecializacion(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult MentenimientoEspecializacion_Verificar(Cls_Ent_Especializacion entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Especializacion PreguntaRspta = null;
            var user = "";
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
            entidad.USU_INGRESO = user;
            entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new SolicitudesCoordinadorRepositorio().MentenimientoEstudios_Especializacion(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.success = true;
                itemRespuesta.message = "Se realizo Correctamente.";

            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ListaCapacitacion(Cls_Ent_Capacitacion entidad)
        {
            List<Cls_Ent_Capacitacion> lista;
            lista = new SolicitudesCoordinadorRepositorio().ListaCapacitacion(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult MentenimientoCapacitacion_Verificar(Cls_Ent_Capacitacion entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Capacitacion PreguntaRspta = null;
            var user = "";
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
            entidad.USU_INGRESO = user;
            entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new SolicitudesCoordinadorRepositorio().MentenimientoEstudios_Capacitacion(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.success = true;
                itemRespuesta.message = "Se realizo Correctamente.";

            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ListaExperiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            List<Cls_Ent_Experiencia_Laboral> lista;
            lista = new SolicitudesCoordinadorRepositorio().ListaExperiencia(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult MentenimientoExperiencia_Verificar(Cls_Ent_Experiencia_Laboral entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Experiencia_Laboral PreguntaRspta = null;
            var user = "";
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
            entidad.USU_INGRESO = user;
            entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new SolicitudesCoordinadorRepositorio().MentenimientoEstudios_Experiencia(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.success = true;
                itemRespuesta.message = "Se realizo Correctamente.";

            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult VerFormatoArchivosPersonal(int ID_PERSONAL, int ID_SOLICITUD, int ID, string TIPO)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            modelo.ID_PERSONAL = ID_PERSONAL;
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.ID = ID;
            modelo.ACCION = TIPO;
            return View(modelo);

        }
        //FIN CV
        public ActionResult UPD_CALIFICAR_DOCUMENTOS(Cls_Ent_Solicitud_Personal entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud_Personal PreguntaRspta = null;
            var user = "";
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
            entidad.USU_INGRESO = user;
            entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new SolicitudesCoordinadorRepositorio().UPD_CALIFICAR_DOCUMENTOS(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                itemRespuesta.success = true;
                    itemRespuesta.message = "La calificación se realizó correctamente.";

            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ObservacionUTP(int ID_SOLICITUD, String TIPO)
        {
            Cls_Ent_Solicitud_Personal modelo = new Cls_Ent_Solicitud_Personal();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.ACCION = TIPO;
            return View(modelo);
        }
        public ActionResult InsReevaluacion(Cls_Ent_Reevaluacion entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Reevaluacion PreguntaRspta = null;
            try
            {
                var user = "";
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
                entidad.USU_INGRESO = user;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                PreguntaRspta = new SolicitudesCoordinadorRepositorio().InsReevaluacion(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {

                    Cls_Ent_Solicitud_Personal ent_s = new Cls_Ent_Solicitud_Personal();
                    ent_s.ID_SOLICITUD = entidad.ID_SOLICITUD;
                    ent_s.IDEDOCODIGO = "";
                    ent_s.TIPO_PROCESO = "";
                    ent_s = new SolicitudesCoordinadorRepositorio().ListaSolicitudesCoordinador(ent_s).FirstOrDefault();
                    ent_s.DESCRIPCION = entidad.DES_REEVALUACION;
                    Cls_Ent_Coordinador corr = new Cls_Ent_Coordinador();
                    corr = new SolicitudesCoordinadorRepositorio().ListaCoordinadores(corr).FirstOrDefault(A => A.USUARIO==ent_s.USER_COORDINADOR && A.FLG_ESTADO==1);
                    if (entidad.TIPO=="U")
                    {
                        if (EnviarEmailCoordinadorObservado(ent_s, corr))
                        {
                            itemRespuesta.message = "Se realizo la notificación al coordinador.";
                        }
                        else
                        {
                            itemRespuesta.message = "Error al realizar la notificación al coordinador, vuelva a intentarlo en unos minutos.";
                        }
                    }
                    else
                    {
                        if (EnviarEmailCoordinadorRechazado(ent_s, corr))
                        {
                            itemRespuesta.message = "Se realizo la notificación al coordinador.";
                        }
                        else
                        {
                            itemRespuesta.message = "Error al realizar la notificación al coordinador, vuelva a intentarlo en unos minutos.";
                        }
                    }
                    itemRespuesta.extra = entidad.ID_SOLICITUD.ToString();
                    itemRespuesta.success = true;
                }

            }
            catch (Exception ex)
            {
                itemRespuesta.success = false;
                itemRespuesta.extra = ex.ToString();
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        private bool EnviarEmailCoordinadorObservado(Cls_Ent_Solicitud_Personal entidad, Cls_Ent_Coordinador coordinador)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarobservacionCoordinador.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", coordinador.APELLIDO_PATERNO + " " + coordinador.APELLIDO_MATERNO + " " + coordinador.NOMBRES);
            mensaje = mensaje.Replace("{1}", coordinador.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", entidad.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{3}", entidad.DESCRIPCION);
            //mensaje = mensaje.Replace("{4}", Encriptar.Desencriptar_Pass(entidad.CONTRASENA));
            mensaje = mensaje.Replace("{5}", ConfigurationManager.AppSettings["UrlAplicacion"]);
            mensaje = mensaje.Replace("{6}", entidad.ENTIDAD);
            mensaje = mensaje.Replace("{7}", entidad.NUM_PROCESO);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = coordinador.CORREO_NOTIFICADOR;
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
        private bool EnviarEmailCoordinadorRechazado(Cls_Ent_Solicitud_Personal entidad, Cls_Ent_Coordinador coordinador)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarRechazoCoordinador.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", coordinador.APELLIDO_PATERNO + " " + coordinador.APELLIDO_MATERNO + " " + coordinador.NOMBRES);
            mensaje = mensaje.Replace("{1}", coordinador.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", entidad.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{3}", entidad.DESCRIPCION);
            //mensaje = mensaje.Replace("{4}", Encriptar.Desencriptar_Pass(entidad.CONTRASENA));
            mensaje = mensaje.Replace("{5}", ConfigurationManager.AppSettings["UrlAplicacion"]);
            mensaje = mensaje.Replace("{6}", entidad.ENTIDAD);
            mensaje = mensaje.Replace("{7}", entidad.NUM_PROCESO);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = coordinador.CORREO_NOTIFICADOR;
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
     
        public ActionResult VerObservacionUTP(int ID_SOLICITUD, String TIPO)
        {
            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.ACCION = TIPO;
            return View(modelo);
        }
        public ActionResult ListaObservacion(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista;

            lista = new SolicitudesCoordinadorRepositorio().ListaReevaluacion(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult VerRegistroContrato(int ID_SOLICITUD, String TIPO,string ID_CONTRATO)
        {
            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.ACCION = TIPO;
            modelo.ID = ID_CONTRATO;
            modelo.COD_CONTRATO = new SolicitudesCoordinadorRepositorio().ProximoCodContrato().FirstOrDefault().COD_CONTRATO;      
            //NUM_DOCUMENTO = "41852899";
            return View(modelo);
        }

        public empleadoForm HabilitadoAIRSH(string TipoDocumento, string NumeroDocumento)
        {
            Boolean flag =true;
            String usuarioAIRSH = ConfigurationManager.AppSettings["UsuarioAIRSH"];
            String passwordAIRSH = ConfigurationManager.AppSettings["PasswordAIRSH"];
            WCF_AIRSH.WsmefairhspWsClient cliente = new WCF_AIRSH.WsmefairhspWsClient();
            genericOutputEmpleado respuesta = new genericOutputEmpleado();
            empleadoForm dataConsultor = new empleadoForm();
            using (new OperationContextScope(cliente.InnerChannel))
            {
                HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                requestMessage.Headers["usuario"] = usuarioAIRSH;
                requestMessage.Headers["clave"] = passwordAIRSH;

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
                respuesta = cliente.consultaIndividual(TipoDocumento, NumeroDocumento);

                if (respuesta.codMensaje == "0001")
                {
                    foreach (var item in respuesta.data)
                    {
                        if (item.estadoEmpleado == "Ocupado")
                        {
                            flag = false;
                            dataConsultor = item;
                        }
                    }
                }
            }
            return dataConsultor;
        }

        public ActionResult InsContratoSolicitud(Cls_Ent_Solicitud_Personal entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud_Personal PreguntaRspta = null;
            Cls_Ent_Solicitud_Personal ent_s = new Cls_Ent_Solicitud_Personal();
            empleadoForm datosAIRSH =new empleadoForm();
            string TIPO_DOCUMENTO = "2";
            bool flag_AIRSH = false;
            string NUM_DOCUMENTO = new SolicitudesCoordinadorRepositorio().GetDatosSolicitud(entidad.ID_SOLICITUD).FirstOrDefault().NUM_DOCUMENTO;
            datosAIRSH = HabilitadoAIRSH(TIPO_DOCUMENTO, NUM_DOCUMENTO);
            ent_s.ID_SOLICITUD = entidad.ID_SOLICITUD;
            ent_s.IDEDOCODIGO = "";
            ent_s.TIPO_PROCESO = "";
            ent_s = new SolicitudesCoordinadorRepositorio().ListaSolicitudesCoordinador(ent_s).FirstOrDefault();
            String SubcarpetaLF = "";
            try
            {

                if (datosAIRSH.codigoAirhsp != null) {
                    flag_AIRSH = true;
                }

                if (flag_AIRSH && entidad.ACCION == "F") {
                    itemRespuesta.message = " BLOQUEO DE AIRSH - No se puede culminar la contratación: Persona se encuentra activa en " + datosAIRSH.descripcionPliego;
                    itemRespuesta.success = false;
                    itemRespuesta.extra = "ALERT";
                    return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
                }
              

                var user = "";
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
                entidad.USU_INGRESO = user;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                if (entidad.ACCION == "P")
                {
                    SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                    var httpRequest = Request.Files;
                    int IdLaserfiche = 0;
                    string nomArchivoSave = "";
                    if (httpRequest.Count > 0)
                    {
                        var files = Request.Files[0] as HttpPostedFileBase;
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                        {
                            string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                            files.SaveAs(NombreArchivo);
                            //string SubcarpetaLF ="PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                            nomArchivoSave = "CONTRATO_FIMADO_" + RandomString(5, true) + ".pdf";
                            IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, ent_s.ENTIDAD_PADRE, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                            entidad.ID_ARCHIVO = IdLaserfiche;
                        }

                        //Archivo RNSDD
                        var fileRNSDD = Request.Files[1] as HttpPostedFileBase;
                        if (fileRNSDD != null)
                        {
                            var carpetaAIRSHP = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileRNSDD)))
                            {
                                string NombreArchivo = Path.Combine(carpetaAIRSHP, fileRNSDD.FileName.ToString());
                                fileRNSDD.SaveAs(NombreArchivo);
                                nomArchivoSave = "RNSDD_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, ent_s.ENTIDAD_PADRE, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                                entidad.ID_ARCHIVO_RNSDD = IdLaserfiche;
                            }
                        }
                        //Archivo REDAM
                        var fileREDAM = Request.Files[2] as HttpPostedFileBase;
                        if (fileREDAM != null)
                        {
                            var carpetaREDAM = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileREDAM)))
                            {
                                string NombreArchivo = Path.Combine(carpetaREDAM, fileREDAM.FileName.ToString());
                                fileREDAM.SaveAs(NombreArchivo);
                                nomArchivoSave = "REDAM_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, ent_s.ENTIDAD_PADRE, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);

                                entidad.ID_ARCHIVO_REDAM = IdLaserfiche;
                            }
                        }

                        //Archivo REDERECI
                        var fileREDERECI = Request.Files[3] as HttpPostedFileBase;
                        if (fileREDERECI != null)
                        {
                            var carpetaREDERECI = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileREDERECI)))
                            {
                                string NombreArchivo = Path.Combine(carpetaREDERECI, fileREDERECI.FileName.ToString());
                                fileREDERECI.SaveAs(NombreArchivo);
                                nomArchivoSave = "REDERECI_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, ent_s.ENTIDAD_PADRE, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);

                                entidad.ID_ARCHIVO_REDERECI = IdLaserfiche;
                            }
                        }

                        //Archivo AIRSHP
                        var fileAIRSHP = Request.Files[4] as HttpPostedFileBase;
                        if (fileAIRSHP != null)
                        {
                            var carpetaREDERECI = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileAIRSHP)))
                            {
                                string NombreArchivo = Path.Combine(carpetaREDERECI, fileAIRSHP.FileName.ToString());
                                fileAIRSHP.SaveAs(NombreArchivo);
                                nomArchivoSave = "AIRSHP_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, ent_s.ENTIDAD_PADRE, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);

                                entidad.ID_ARCHIVO_AIRSHP = IdLaserfiche;
                            }
                        }
                    }
                } else {
                    SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                    var httpRequest = Request.Files;
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    int IdLaserfiche = 0;
                    string nomArchivoSave = "";
                    if (httpRequest.Count > 0)
                    {
                        //Archivo RNSDD
                        var fileRNSDD = Request.Files[0] as HttpPostedFileBase;
                        if (fileRNSDD != null)
                        {
                            var carpetaRNSDD = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileRNSDD)))
                            {
                                string NombreArchivo = Path.Combine(carpetaRNSDD, fileRNSDD.FileName.ToString());
                                fileRNSDD.SaveAs(NombreArchivo);
                                nomArchivoSave = "RNSDD_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, ent_s.ENTIDAD_PADRE, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                                entidad.ID_ARCHIVO_RNSDD = IdLaserfiche;
                            }
                        }
                        //Archivo REDAM
                        var fileREDAM = Request.Files[1] as HttpPostedFileBase;
                        if (fileREDAM != null)
                        {
                            var carpetaREDAM = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileREDAM)))
                            {
                                string NombreArchivo = Path.Combine(carpetaREDAM, fileREDAM.FileName.ToString());
                                fileREDAM.SaveAs(NombreArchivo);
                                nomArchivoSave = "REDAM_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, ent_s.ENTIDAD_PADRE, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);

                                entidad.ID_ARCHIVO_REDAM = IdLaserfiche;
                            }
                        }

                        //Archivo REDERECI
                        var fileREDERECI = Request.Files[2] as HttpPostedFileBase;
                        if (fileREDERECI != null)
                        {
                            var carpetaREDERECI = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileREDERECI)))
                            {
                                string NombreArchivo = Path.Combine(carpetaREDERECI, fileREDERECI.FileName.ToString());
                                fileREDERECI.SaveAs(NombreArchivo);
                                nomArchivoSave = "REDERECI_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, ent_s.ENTIDAD_PADRE, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);

                                entidad.ID_ARCHIVO_REDERECI = IdLaserfiche;
                            }
                        }

                        //Archivo AIRSHP
                        var fileAIRSHP = Request.Files[3] as HttpPostedFileBase;
                        if (fileAIRSHP != null)
                        {
                            var carpetaAIRSHP = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileAIRSHP)))
                            {
                                string NombreArchivo = Path.Combine(carpetaAIRSHP, fileAIRSHP.FileName.ToString());
                                fileAIRSHP.SaveAs(NombreArchivo);
                                nomArchivoSave = "AIRSHP_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, ent_s.ENTIDAD_PADRE, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);

                                entidad.ID_ARCHIVO_AIRSHP = IdLaserfiche;
                            }
                        }
                    }
                }
                PreguntaRspta = new SolicitudesCoordinadorRepositorio().InsContratoSolicitud(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
                }
                else
                {
                    Cls_Ent_Coordinador corr = new Cls_Ent_Coordinador();
                    corr = new SolicitudesCoordinadorRepositorio().ListaCoordinadores(corr).FirstOrDefault(A => A.USUARIO == ent_s.USER_COORDINADOR && A.FLG_ESTADO == 1);
                        if (EnviarEmailCoordinadorAprueba(ent_s, corr))
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
                itemRespuesta.extra = Log.MensajeInterno() +"<br/>" +  ex.ToString();
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        private bool EnviarEmailCoordinadorAprueba(Cls_Ent_Solicitud_Personal entidad, Cls_Ent_Coordinador coordinador)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarApruebaSolicitudCoordinador.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", coordinador.APELLIDO_PATERNO + " " + coordinador.APELLIDO_MATERNO + " " + coordinador.NOMBRES);
            mensaje = mensaje.Replace("{1}", coordinador.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", entidad.DENOMINACION_PUESTO);
            //mensaje = mensaje.Replace("{3}", entidad.DESCRIPCION);
            //mensaje = mensaje.Replace("{5}", ConfigurationManager.AppSettings["UrlAplicacion"]);
            mensaje = mensaje.Replace("{6}", entidad.ENTIDAD);
            mensaje = mensaje.Replace("{7}", entidad.NUM_PROCESO);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = coordinador.CORREO_NOTIFICADOR;
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
        /****** INICIO PROCESO SOLICITUD DE PAGO ****/
        public ActionResult ListaSolicitudPagoEntidadUTP(Cls_Ent_Solicitud_Pago entidad)
        {
            List<Cls_Ent_Solicitud_Pago> lista;
            lista = new SolicitudesCoordinadorRepositorio().ListaSolicitudPagoEntidadUTP(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ObservacionPeriodoPagoUTP(int ID_SOLICITUD, int ID_CONFORMIDAD, int NR_MES, String TIPO)
        {
            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.ID_CONFORMIDAD = ID_CONFORMIDAD;
            modelo.NR_MES = NR_MES;
            modelo.ACCION = TIPO;
            return View(modelo);
        }
        public ActionResult UpdateReevaluacionPago(Cls_Ent_Solicitud_Pago entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud_Pago PreguntaRspta = null;
            try
            {
                var user = "";
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

                entidad.USU_INGRESO = user;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
          
                PreguntaRspta = new SolicitudesCoordinadorRepositorio().UpdateReevaluacionPago(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
                }
                else
                {

                    Cls_Ent_Solicitud_Personal ent_s = new Cls_Ent_Solicitud_Personal();
                    ent_s.ID_SOLICITUD = entidad.ID_SOLICITUD;
                    ent_s.IDEDOCODIGO = "";
                    ent_s.TIPO_PROCESO = "";
                    ent_s = new SolicitudesCoordinadorRepositorio().ListaSolicitudesCoordinador(ent_s).FirstOrDefault();
                    ent_s.DESCRIPCION =entidad.DESCRIPCION;
                     Cls_Ent_Coordinador corr = new Cls_Ent_Coordinador();
                    corr = new SolicitudesCoordinadorRepositorio().ListaCoordinadores(corr).FirstOrDefault(A => A.USUARIO == ent_s.USER_COORDINADOR && A.FLG_ESTADO == 1);
                    corr.DESCRIPCION=FuncionUtil.Mi_Mes(entidad.NUM_MES);
                    if (EnviarEmailCoordinadorObservadoPeriodo(ent_s, corr))
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
                itemRespuesta.extra = Log.MensajeInterno() + "<br/>" + ex.ToString();
            }
           
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        private bool EnviarEmailCoordinadorObservadoPeriodo(Cls_Ent_Solicitud_Personal entidad, Cls_Ent_Coordinador coordinador)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarobservacionCoordinadorPeriodo.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", coordinador.APELLIDO_PATERNO + " " + coordinador.APELLIDO_MATERNO + " " + coordinador.NOMBRES);
            mensaje = mensaje.Replace("{1}", coordinador.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", entidad.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{3}", entidad.DESCRIPCION);
            mensaje = mensaje.Replace("{6}", entidad.ENTIDAD);
            mensaje = mensaje.Replace("{9}", entidad.APELLIDO_PATERNO + " " + entidad.APELLIDO_MATERNO + " " + entidad.NOMBRES);
            mensaje = mensaje.Replace("{8}", coordinador.DESCRIPCION);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = coordinador.CORREO_NOTIFICADOR;
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
      
        public ActionResult Update_Conformidad_Pago(Cls_Ent_Solicitud_Pago entidad)
        {
      
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud_Pago PreguntaRspta = null;
            try
            {
                var user = "";
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
                entidad.USU_INGRESO = user;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                PreguntaRspta = new SolicitudesCoordinadorRepositorio().Update_Conformidad_Pago(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText() ;
                }
                else
                {
                    Cls_Ent_Solicitud_Personal ent_s = new Cls_Ent_Solicitud_Personal();
                    ent_s.ID_SOLICITUD = entidad.ID_SOLICITUD;
                    ent_s.IDEDOCODIGO = "";
                        ent_s.TIPO_PROCESO = "";
                    ent_s = new SolicitudesCoordinadorRepositorio().ListaSolicitudesCoordinador(ent_s).FirstOrDefault();
                    ent_s.DESCRIPCION = entidad.DESCRIPCION;
                    Cls_Ent_Coordinador corr = new Cls_Ent_Coordinador();
                    corr = new SolicitudesCoordinadorRepositorio().ListaCoordinadores(corr).FirstOrDefault(A => A.USUARIO == ent_s.USER_COORDINADOR && A.FLG_ESTADO == 1);
                    corr.DESCRIPCION = FuncionUtil.Mi_Mes(entidad.NUM_MES);
                    if (EnviarEmailCoordinadorApruebaPeriodo(ent_s, corr))
                        {

                            itemRespuesta.message = "Se realizó la notificación al coordinador";
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
                itemRespuesta.extra = Log.MensajeInterno() + "<br/>" + ex.ToString();
                ;
            }

            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailCoordinadorApruebaPeriodo(Cls_Ent_Solicitud_Personal entidad, Cls_Ent_Coordinador coordinador)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarApruebaCoordinadorPeriodo.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", coordinador.APELLIDO_PATERNO + " " + coordinador.APELLIDO_MATERNO + " " + coordinador.NOMBRES);
            mensaje = mensaje.Replace("{1}", coordinador.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", entidad.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{3}", entidad.DESCRIPCION);
            mensaje = mensaje.Replace("{6}", entidad.ENTIDAD);
            mensaje = mensaje.Replace("{9}", entidad.APELLIDO_PATERNO + " " + entidad.APELLIDO_MATERNO + " " + entidad.NOMBRES);
            mensaje = mensaje.Replace("{8}", coordinador.DESCRIPCION);

            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = coordinador.CORREO_NOTIFICADOR;
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
        /****** FIN PROCESO SOLICITUD DE PAGO  ****/
        /****** INICIO PROCESO SOLICITUD DE ADENDA ****/
        public ActionResult ListaDetalleContratos(Cls_Ent_Adenda entidad)
        {
            List<Cls_Ent_Adenda> lista;
            lista = Cls_Rule_Adenda.ListaDetalleContratos(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ObservacionADENDA(int ID_CONTRATO_DET, String TIPO)
        {
            Cls_Ent_Adenda modelo = new Cls_Ent_Adenda();
            modelo.ID_CONTRATO_DET = ID_CONTRATO_DET;
            modelo.ACCION = TIPO;
            return View(modelo);
        }
        public ActionResult Ver_Adenda(int ID)
        {
            BusquedaModelView modelo = new BusquedaModelView();

            modelo.ID = ID;
            modelo.ACCION = "ANEXO 1";

            return View(modelo);
        }
        public ActionResult InsReevaluacionAdenda(Cls_Ent_Reevaluacion entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Reevaluacion PreguntaRspta = null;
            try
            {
                var user = "";
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
                entidad.USU_INGRESO = user;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                PreguntaRspta = new SolicitudesCoordinadorRepositorio().InsReevaluacionAdenda(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {

                    Cls_Ent_Adenda ent_s = new Cls_Ent_Adenda();
                    ent_s.ID_CONTRATO_DET = entidad.ID_CONTRATO_DET;
                    ent_s.TIPO_PROCESO = "";
                    ent_s.IDEDOCODIGO = "";
                    ent_s = Cls_Rule_Adenda.ListaDetalleContratos(ent_s).FirstOrDefault();
                    ent_s.DESCRIPCION = entidad.DES_REEVALUACION;
                    if (entidad.TIPO == "O")
                    {
                        if (EnviarEmailCoordinadorObservadoAdenda(ent_s))
                        {
                            itemRespuesta.message = "Se realizo la notificación al coordinador.";
                        }
                        else
                        {
                            itemRespuesta.message = "Error al realizar la notificación al coordinador, vuelva a intentarlo en unos minutos.";
                        }
                    }
                    else
                    {
                        if (EnviarEmailCoordinadorRechazadoAdenda(ent_s))
                        {
                            itemRespuesta.message = "Se realizo la notificación al coordinador.";
                        }
                        else
                        {
                            itemRespuesta.message = "Error al realizar la notificación al coordinador, vuelva a intentarlo en unos minutos.";
                        }
                    }
                    itemRespuesta.extra = entidad.ID_SOLICITUD.ToString();
                    itemRespuesta.success = true;
                }

            }
            catch (Exception ex)
            {
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() +"<br/>" + ex.ToString();
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        private bool EnviarEmailCoordinadorObservadoAdenda(Cls_Ent_Adenda entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarobservacionCoordinadorAdenda.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", entidad.COORDINADOR);
            mensaje = mensaje.Replace("{1}", entidad.DESC_ENTIDAD);
            mensaje = mensaje.Replace("{2}", entidad.CODIGO_ADENDA);
            mensaje = mensaje.Replace("{3}", entidad.NOMBRE_PUESTO);
            mensaje = mensaje.Replace("{4}", entidad.DESCRIPCION);

            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.EMAIL_COORDINADOR;
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
        private bool EnviarEmailCoordinadorRechazadoAdenda(Cls_Ent_Adenda entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarRechazoCoordinadorAdenda.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", entidad.COORDINADOR);
            mensaje = mensaje.Replace("{1}", entidad.DESC_ENTIDAD);
            mensaje = mensaje.Replace("{2}", entidad.CODIGO_ADENDA);
            mensaje = mensaje.Replace("{3}", entidad.NOMBRE_PUESTO);
            mensaje = mensaje.Replace("{4}", entidad.DESCRIPCION);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.EMAIL_COORDINADOR;
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
        public ActionResult ListaReevaluacionAdenda(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista;
            lista = new SolicitudesCoordinadorRepositorio().ListaReevaluacionAdenda(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult VerObservacionADENDA(int ID_CONTRATO_DET)
        {
            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_CONTRATO_DET = ID_CONTRATO_DET;
            return View(modelo);
        }
        public ActionResult VerRegistroAdenda(int ID_CONTRATO_DET, String TIPO)
        {
            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_CONTRATO_DET = ID_CONTRATO_DET;
            modelo.ACCION = TIPO;
            Cls_Ent_Adenda ent_s = new Cls_Ent_Adenda();
            ent_s.ID_CONTRATO_DET = ID_CONTRATO_DET;
            ent_s.TIPO_PROCESO = ""; ent_s.IDEDOCODIGO = ""; ent_s.DOCUMENTO_CONSULTOR = "";
            ent_s = Cls_Rule_Adenda.ListaDetalleContratos(ent_s).FirstOrDefault();
            modelo.ID = ent_s.ID_ARCHIVO_CONTRATO.ToString();
            modelo.ID_SOLICITUD = ent_s.ID_SOLICITUD;
            modelo.TIPO = ent_s.TIPO_PROCESO;
            modelo.DESCRIPCION = ent_s.DESC_ENTIDAD;
            string TIPO_DOCUMENTO = "2";
            string NUM_DOCUMENTO = new SolicitudesCoordinadorRepositorio().GetDatosAdenda(ID_CONTRATO_DET).FirstOrDefault().NUM_DOCUMENTO;
            //NUM_DOCUMENTO = "41852899";
            //modelo.AIRSH = this.HabilitadoAIRSH(TIPO_DOCUMENTO, NUM_DOCUMENTO);
            return View(modelo);
        }
        public ActionResult UpdEstadoAdenda(Cls_Ent_Adenda entidad)
        {
       
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Adenda PreguntaRspta = null;
            empleadoForm datosAIRSH = new empleadoForm();
            string TIPO_DOCUMENTO = "2";
            bool flag_AIRSH = false;
            string NUM_DOCUMENTO = new SolicitudesCoordinadorRepositorio().GetDatosSolicitud(entidad.ID_SOLICITUD).FirstOrDefault().NUM_DOCUMENTO;
            //NUM_DOCUMENTO = "41852899";
            datosAIRSH = HabilitadoAIRSH(TIPO_DOCUMENTO, NUM_DOCUMENTO);
            try
            {
                if (datosAIRSH.codigoAirhsp != null)
                {
                    flag_AIRSH = true;
                }

                if (flag_AIRSH && entidad.ACCION == "F")
                {
                    itemRespuesta.message = "BLOQUEO DE AIRSH - No se puede culminar la contratación: Persona se encuentra activa en " + datosAIRSH.descripcionPliego;
                    itemRespuesta.success = false;
                    itemRespuesta.extra = "BLOQUEO DE AIRSH - No se puede culminar la contratación: Persona se encuentra activa en  " + datosAIRSH.descripcionPliego;
                    return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
                }
                var user = "";
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
                entidad.USU_INGRESO = user;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                string SubcarpetaLF = "";
                if (entidad.TIPO == "F")
                {
                    SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                }
                else
                {
                    SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                }
                var carpeta = Server.MapPath("~/ArchivoTemp");
                if (entidad.ACCION == "P")
                {
                    var httpRequest = Request.Files;
                    //Cls_Ent_Archivo xx = new Cls_Ent_Archivo();
                    //Cls_Ent_Archivo ArchivoRspta = null;
                  

                    if (httpRequest.Count > 0)
                    {
                       
                        string nomArchivoSave = "";
                        var files = Request.Files[0] as HttpPostedFileBase;
                        long IdLaserfiche = 0;
                        using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                        {
                            string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                            files.SaveAs(NombreArchivo);
                          

                            nomArchivoSave = "DOCUMENTO_FIRMADO_ADENDA_" + RandomString(5, true) + ".pdf";
                            IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, entidad.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, user, entidad.IP_PC);
                            // entidad.ID_ARCHIVO = int.Parse(IdLaserfiche.ToString());
                        }
                        if (IdLaserfiche == 0)
                        {
                            throw new Exception("Error al registrar la información del archivo.");
                        }
                        else
                        {
                            entidad.ID_ARCHIVO = int.Parse(IdLaserfiche.ToString());
                        }
                        //Archivo RNSDD
                        var fileRNSDD = Request.Files[1] as HttpPostedFileBase;
                        if (fileRNSDD != null)
                        {
                            var carpetaAIRSHP = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileRNSDD)))
                            {
                                string NombreArchivo = Path.Combine(carpetaAIRSHP, fileRNSDD.FileName.ToString());
                                fileRNSDD.SaveAs(NombreArchivo);
                                nomArchivoSave = "RNSDD_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, entidad.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                                if (IdLaserfiche == 0)
                                {
                                    throw new Exception("Error al registrar la información del archivo.");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_RNSDD = int.Parse(IdLaserfiche.ToString());
                                }
                            }
                        }
                        //Archivo REDAM
                        var fileREDAM = Request.Files[2] as HttpPostedFileBase;
                        if (fileREDAM != null)
                        {
                            var carpetaREDAM = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileREDAM)))
                            {
                                string NombreArchivo = Path.Combine(carpetaREDAM, fileREDAM.FileName.ToString());
                                fileREDAM.SaveAs(NombreArchivo);
                                nomArchivoSave = "REDAM_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, entidad.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                                if (IdLaserfiche == 0)
                                {
                                    throw new Exception("Error al registrar la información del archivo.");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_REDAM = int.Parse(IdLaserfiche.ToString());
                                }
                            }
                        }

                        //Archivo REDERECI
                        var fileREDERECI = Request.Files[3] as HttpPostedFileBase;
                        if (fileREDERECI != null)
                        {
                            var carpetaREDERECI = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileREDERECI)))
                            {
                                string NombreArchivo = Path.Combine(carpetaREDERECI, fileREDERECI.FileName.ToString());
                                fileREDERECI.SaveAs(NombreArchivo);
                                nomArchivoSave = "REDERECI_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, entidad.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                                if (IdLaserfiche == 0)
                                {
                                    throw new Exception("Error al registrar la información del archivo.");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_REDERECI = int.Parse(IdLaserfiche.ToString());
                                }
                            }
                        }
                        //Archivo AIRSHP
                        var fileAIRSHP = Request.Files[4] as HttpPostedFileBase;
                        if (fileAIRSHP != null)
                        {
                            var carpetaREDERECI = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileAIRSHP)))
                            {
                                string NombreArchivo = Path.Combine(carpetaREDERECI, fileAIRSHP.FileName.ToString());
                                fileAIRSHP.SaveAs(NombreArchivo);
                                nomArchivoSave = "AIRSHP_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, entidad.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                                if (IdLaserfiche == 0)
                                {
                                    throw new Exception("Error al registrar la información del archivo.");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_AIRSHP = int.Parse(IdLaserfiche.ToString());
                                }
                            }
                        }


                        //Archivo otros
                        var fileOTROS = Request.Files[5] as HttpPostedFileBase;
                        if (fileOTROS != null)
                        {
                            var carpetaOTROS = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileOTROS)))
                            {
                                string NombreArchivo = Path.Combine(carpetaOTROS, fileOTROS.FileName.ToString());
                                fileOTROS.SaveAs(NombreArchivo);
                                nomArchivoSave = "OTROS_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, entidad.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                                if (IdLaserfiche == 0)
                                {
                                    throw new Exception("Error al registrar la información del archivo.");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_OTROS = int.Parse(IdLaserfiche.ToString());
                                }
                            }
                        }
                    }
                }
                else {
                    var httpRequest = Request.Files;
                    //Cls_Ent_Archivo xx = new Cls_Ent_Archivo();
                    //Cls_Ent_Archivo ArchivoRspta = null;


                    if (httpRequest.Count > 0)
                    {

                        string nomArchivoSave = "";
                        long IdLaserfiche = 0;
                       
                        //Archivo RNSDD
                        var fileRNSDD = Request.Files[0] as HttpPostedFileBase;
                        if (fileRNSDD != null)
                        {
                            var carpetaAIRSHP = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileRNSDD)))
                            {
                                string NombreArchivo = Path.Combine(carpetaAIRSHP, fileRNSDD.FileName.ToString());
                                fileRNSDD.SaveAs(NombreArchivo);
                                nomArchivoSave = "RNSDD_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, entidad.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                                if (IdLaserfiche == 0)
                                {
                                    throw new Exception("Error al registrar la información del archivo.");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_RNSDD = int.Parse(IdLaserfiche.ToString());
                                }
                            }
                        }
                        //Archivo REDAM
                        var fileREDAM = Request.Files[1] as HttpPostedFileBase;
                        if (fileREDAM != null)
                        {
                            var carpetaREDAM = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileREDAM)))
                            {
                                string NombreArchivo = Path.Combine(carpetaREDAM, fileREDAM.FileName.ToString());
                                fileREDAM.SaveAs(NombreArchivo);
                                nomArchivoSave = "REDAM_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, entidad.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                                if (IdLaserfiche == 0)
                                {
                                    throw new Exception("Error al registrar la información del archivo.");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_REDAM = int.Parse(IdLaserfiche.ToString());
                                }
                            }
                        }

                        //Archivo REDERECI
                        var fileREDERECI = Request.Files[2] as HttpPostedFileBase;
                        if (fileREDERECI != null)
                        {
                            var carpetaREDERECI = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileREDERECI)))
                            {
                                string NombreArchivo = Path.Combine(carpetaREDERECI, fileREDERECI.FileName.ToString());
                                fileREDERECI.SaveAs(NombreArchivo);
                                nomArchivoSave = "REDERECI_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, entidad.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                                if (IdLaserfiche == 0)
                                {
                                    throw new Exception("Error al registrar la información del archivo.");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_REDERECI = int.Parse(IdLaserfiche.ToString());
                                }
                            }
                        }
                        //Archivo otros
                        var fileOTROS = Request.Files[3] as HttpPostedFileBase;
                        if (fileOTROS != null)
                        {
                            var carpetaREDERECI = Server.MapPath("~/ArchivoTemp");
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileOTROS)))
                            {
                                string NombreArchivo = Path.Combine(carpetaREDERECI, fileOTROS.FileName.ToString());
                                fileOTROS.SaveAs(NombreArchivo);
                                nomArchivoSave = "OTROS_" + RandomString(5, true) + ".pdf";
                                IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, entidad.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                                if (IdLaserfiche == 0)
                                {
                                    throw new Exception("Error al registrar la información del archivo.");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_OTROS = int.Parse(IdLaserfiche.ToString());
                                }
                            }
                        }
                    }

                }
                PreguntaRspta = new SolicitudesCoordinadorRepositorio().UpdEstadoAdenda(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                  
                    Cls_Ent_Adenda ent_s = new Cls_Ent_Adenda();
                    ent_s.ID_CONTRATO_DET = entidad.ID_CONTRATO_DET;
                    ent_s.TIPO_PROCESO = "";
                    ent_s.IDEDOCODIGO = "";
                    ent_s.DOCUMENTO_CONSULTOR = "";
                   ent_s = Cls_Rule_Adenda.ListaDetalleContratos(ent_s).FirstOrDefault();
                    if (EnviarEmailAprobacionAdenda(ent_s))
                    {
                        itemRespuesta.message = "Se realizo la notificación al coordinador de la entidad.";
                    }
                    else
                    {
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
                itemRespuesta.extra = Log.MensajeInterno() +"<br/>" + ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailAprobacionAdenda(Cls_Ent_Adenda entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarApruebaAdendaCoordinador.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", entidad.COORDINADOR);
            mensaje = mensaje.Replace("{1}", entidad.DESC_ENTIDAD);
            mensaje = mensaje.Replace("{2}", entidad.CODIGO_ADENDA);
            mensaje = mensaje.Replace("{3}", entidad.NOMBRE_PUESTO);
            mensaje = mensaje.Replace("{4}", entidad.DESCRIPCION);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.EMAIL_COORDINADOR;
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

        /****** FIN PROCESO SOLICITUD DE ADENDA  ****/
        /****** INICIO PROCESO SOLICITUD DE SUSPENSION ****/
        public ActionResult ListaSolicitud_Suspension(Cls_Ent_Descanso entidad)
        {
            List<Cls_Ent_Descanso> lista;
            SolicitudesCoordinadorRepositorio repositorio =new SolicitudesCoordinadorRepositorio();
            lista = repositorio.ListaSolicitud_Suspension(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ObservacionSUSPENSION(int ID_SUSPENSION)
        {
            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_SUSPENSION = ID_SUSPENSION;
            modelo.TIPO = "U";
            return View(modelo);
        }
        public ActionResult UpdateReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Reevaluacion PreguntaRspta = null;
            try
            {

                var user = "";
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
                entidad.USU_INGRESO = user;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                PreguntaRspta = new SolicitudesCoordinadorRepositorio().InsReevaluacionSuspension(entidad);
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
                    ent_susp.TIPO_PROCESO = "";
                         ent_susp.IDEDOCODIGO = "";
                    ent_susp = new SolicitudesCoordinadorRepositorio().ListaSolicitud_Suspension(ent_susp).FirstOrDefault();
                    ent_susp.DESCRIPCION = entidad.DES_REEVALUACION;
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
                itemRespuesta.extra = Log.MensajeInterno() + "<br/>"+ ex.ToString();
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        private bool EnviarEmailCoordinadorObservadoSuspension(Cls_Ent_Descanso entidad, Cls_Ent_Reevaluacion obser)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarobservacionCoordinadorSuspension.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", entidad.COORDINADOR);
          //  mensaje = mensaje.Replace("{1}", entidad.DESC_ENTIDAD);
            mensaje = mensaje.Replace("{2}", entidad.CODIGO_CONTRATO);
            mensaje = mensaje.Replace("{3}", entidad.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{4}", entidad.DESCRIPCION);

            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.CORREO_COORDINADOR;
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
        public ActionResult VerObservacionSUSPENSION(int ID_SUSPENSION)
        {
            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_SUSPENSION = ID_SUSPENSION;
            modelo.TIPO ="U";
            return View(modelo);
        }
        public ActionResult ListaReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista;
            SolicitudesCoordinadorRepositorio repositorio = new SolicitudesCoordinadorRepositorio();
            lista = repositorio.ListaReevaluacionSuspension(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult UpdEnvioSuspensionAprueba(Cls_Ent_Descanso entidad)
        {

            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Descanso PreguntaRspta = null;
            try
            {
                var user = "";
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
                entidad.USU_INGRESO = user;
                PreguntaRspta = new SolicitudesCoordinadorRepositorio().UpdEnvioSuspensionAprueba(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = entidad.DES_ERROR;
                }
                else
                {

                    Cls_Ent_Descanso ent_susp = new Cls_Ent_Descanso();
                    ent_susp.ID_SUSPENSION = entidad.ID_SUSPENSION;
                    ent_susp.TIPO_PROCESO = "";
                    ent_susp.IDEDOCODIGO = "";
                    ent_susp = new SolicitudesCoordinadorRepositorio().ListaSolicitud_Suspension(ent_susp).FirstOrDefault();
                    if (EnviarEmailAprobacionSuspension(ent_susp))
                    {
                        itemRespuesta.message = "Se realizo la notificación al coordinador de la entidad.";
                    }
                    else
                    {
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
                itemRespuesta.extra = Log.MensajeInterno() + "</br>"+ ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailAprobacionSuspension(Cls_Ent_Descanso entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarApruebaAdendaSuspension.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", entidad.COORDINADOR);
            //mensaje = mensaje.Replace("{1}", coordinador.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", entidad.CODIGO_CONTRATO);
            mensaje = mensaje.Replace("{3}", entidad.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{4}", "Del periodo del contrato desde " + entidad.FECHA_PERIODO_INICIO.ToShortDateString() + " hasta " + entidad.FECHA_PERIODO_FIN.ToShortDateString());
            mensaje = mensaje.Replace("{5}", "El periodo de suspensión desde " + entidad.FECHA_INICIO.ToShortDateString() + " hasta " + entidad.FECHA_FIN.ToShortDateString());

            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.CORREO_COORDINADOR;
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

        /****** FIN PROCESO SOLICITUD DE SUSPENSION  ****/
        /****** INICIO PROCESO SOLICITUD DE COVID ****/
        public ActionResult ListaSolicitud_Covid(Cls_Ent_Covid entidad)
        {
            List<Cls_Ent_Covid> lista;
            SolicitudesCoordinadorRepositorio repositorio = new SolicitudesCoordinadorRepositorio();
            lista = repositorio.ListaSolicitud_Covid(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ObservacionCOVID(int ID_COVID)
        {
            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_COVID = ID_COVID;
            modelo.TIPO = "U";
            return View(modelo);
        }
        public ActionResult UpdateReevaluacionCovid(Cls_Ent_Reevaluacion entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Reevaluacion PreguntaRspta = null;
            try
            {

                var user = "";
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
                entidad.USU_INGRESO = user;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                PreguntaRspta = new SolicitudesCoordinadorRepositorio().InsReevaluacionCovid(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {

                    Cls_Ent_Covid ent_susp = new Cls_Ent_Covid();
                    ent_susp.ID_COVID = entidad.ID_COVID;
                    ent_susp.TIPO_PROCESO = "";
                    ent_susp.IDEDOCODIGO = "";
                    ent_susp = new SolicitudesCoordinadorRepositorio().ListaSolicitud_Covid(ent_susp).FirstOrDefault();
                    ent_susp.DESCRIPCION = entidad.DES_REEVALUACION;
                    if (EnviarEmailCoordinadorObservadoCovid(ent_susp))
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
                itemRespuesta.extra =Log.MensajeInterno()+"</br>"+ ex.ToString();
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        private bool EnviarEmailCoordinadorObservadoCovid(Cls_Ent_Covid entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarobservacionCoordinadorCovid.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", entidad.COORDINADOR);
            //  mensaje = mensaje.Replace("{1}", entidad.DESC_ENTIDAD);
            mensaje = mensaje.Replace("{2}", entidad.CODIGO_CONTRATO);
            mensaje = mensaje.Replace("{3}", entidad.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{4}", entidad.DESCRIPCION);

            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.CORREO_COORDINADOR;
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
        public ActionResult VerObservacionCOVID(int ID_COVID)
        {
            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_COVID = ID_COVID;
            modelo.TIPO = "U";
            return View(modelo);
        }
        public ActionResult ListaReevaluacionCovid(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista;
            SolicitudesCoordinadorRepositorio repositorio = new SolicitudesCoordinadorRepositorio();
            lista = repositorio.ListaReevaluacionCovid(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult UpdEnvioCovidAprueba(Cls_Ent_Covid entidad)
        {

            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Covid PreguntaRspta = null;
            try
            {
                var user = "";
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
                entidad.USU_INGRESO = user;
                PreguntaRspta = new SolicitudesCoordinadorRepositorio().UpdEnvioCovidAprueba(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {

                    Cls_Ent_Covid ent_susp = new Cls_Ent_Covid();
                    ent_susp.ID_COVID = entidad.ID_COVID;
                    ent_susp.TIPO_PROCESO = "";
                    ent_susp.IDEDOCODIGO = "";
                    // ent_susp = new SolicitudesCoordinadorRepositorio().ListaSolicitud_Suspension(ent_susp).First();
                    ent_susp = new SolicitudesCoordinadorRepositorio().ListaSolicitud_Covid(ent_susp).FirstOrDefault();
                   /// ent_susp.DESCRIPCION = entidad.DES_REEVALUACION;
                    if (EnviarEmailAprobacionCovid(ent_susp))
                    {
                        itemRespuesta.message = "Se realizo la notificación al coordinador de la entidad.";
                    }
                    else
                    {
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
                itemRespuesta.extra = Log.MensajeInterno() +"<br/>"+ ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailAprobacionCovid(Cls_Ent_Covid entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarApruebaCovid.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", entidad.COORDINADOR);
            //mensaje = mensaje.Replace("{1}", coordinador.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", entidad.CODIGO_CONTRATO);
            mensaje = mensaje.Replace("{3}", entidad.DENOMINACION_PUESTO);
            //mensaje = mensaje.Replace("{4}", "Del periodo del contrato desde " + entidad.FECHA_PERIODO_INICIO.ToShortDateString() + " hasta " + entidad.FECHA_PERIODO_FIN.ToShortDateString());
            mensaje = mensaje.Replace("{5}", "Desde " + entidad.FECHA_INICIO.ToShortDateString() + " hasta " + entidad.FECHA_FIN.ToShortDateString());

            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.CORREO_COORDINADOR;
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
        /****** FIN PROCESO SOLICITUD DE COVID  ****/
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
