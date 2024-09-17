using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Repositorio;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Administracion;
using APP.ADMINISTRAR.FAG.PAG.Response;
using APP.ADMINISTRAR.FAG.PAG.Core;
using MEF.PROYECTO.Utilitario;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.Script.Serialization;
using APP.MEF.ADMINISTRAR.FAG.PAG.Response;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Controllers
{
    public class CoordinadoresController : BaseController
    {
        //
        // GET: /Administracion/Coordinadores/
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario();
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario_result = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();

        public ActionResult Index()
        {
            List<Cls_Ent_Entidades> lista = null;
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            lista = new EntidadRepositorio().ListaEntidades(xx);
            lista = lista.FindAll(A => A.FLG_ESTADO == 1);
            lista.Insert(0, new Cls_Ent_Entidades() { ID_ENTIDAD = 0, DESC_ENTIDAD = "--Seleccione--" });
            ViewBag.DcboEntidades = lista;
            return View();
        }
        public ActionResult ListaCoordinadores(Cls_Ent_Coordinador entidad)
        {
            IList<Cls_Ent_Coordinador> lista;
            lista = new CoordinadorRepositorio().ListaCoordinadores(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult Ver_Solicitud(int ID, string FLG_SOLICITUD)
        {
            Cls_Ent_Coordinador modelo = new Cls_Ent_Coordinador();
            modelo.ID_COORDINADOR = ID;
            modelo.FLG_SOLICITUD = FLG_SOLICITUD;
            return View(modelo);
        }
        public ActionResult Ver_Observado()
        {
            return View();
        }
        public ActionResult MantenimientoAccionesCoordinador(Cls_Ent_Coordinador entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Coordinador PreguntaRspta = null;
            var user = "";
            if (Session["Personal"] != null)
            {
                usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
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
            PreguntaRspta = new CoordinadorRepositorio().MantenimientoAccionesCoordinador(entidad);
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
                if (entidad.ACCION == "A")
                { itemRespuesta.message = "Se aprobó la solicitud de acceso al coordinador, se realizara la notificación de las credenciales de ingreso.";
                    EnviarEmailAccionSolicitud(entidad,"A"); 
                }
                if (entidad.ACCION == "D")
                { itemRespuesta.message = "Se observó la solicitud de acceso al coordinador, se realizara la notificación de la observación realizada.";
                    EnviarEmailAccionSolicitud(entidad, "D");
                }
                if (entidad.ACCION == "DESAC")
                {
                    itemRespuesta.message = "Se realizó la desactivación del usuario.";
                }

            }

            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailAccionSolicitud(Cls_Ent_Coordinador entidad,string TIPO)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            IList<Cls_Ent_Coordinador> lista;
            lista = new CoordinadorRepositorio().ListaCoordinadores(entidad).FindAll(A => A.ID_COORDINADOR == entidad.ID_COORDINADOR);
            entidad.CORREO_NOTIFICADOR = lista[0].CORREO_NOTIFICADOR;
            switch (TIPO)
            {
                case "A":
                    using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "AprobarSolicitudAcceso.html"))
                    {
                        mensaje = sr.ReadToEnd();
                    }         
                    mensaje = mensaje.Replace("{0}", lista[0].APELLIDO_PATERNO + " " + lista[0].APELLIDO_MATERNO + " " +lista[0].NOMBRES);
                    mensaje = mensaje.Replace("{1}", lista[0].NUM_DOCUMENTO);
                    mensaje = mensaje.Replace("{2}", lista[0].DESC_ENTIDAD);
                    mensaje = mensaje.Replace("{3}", lista[0].NUM_DOCUMENTO);
                    mensaje = mensaje.Replace("{4}", Encriptar.Desencriptar_Pass(lista[0].CONTRASENA));
                    break;
                case "D":
                    using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "ObservarSolicitudAcceso.html"))
                    {
                        mensaje = sr.ReadToEnd();
                    }
                    mensaje = mensaje.Replace("{0}", entidad.ID_COORDINADOR.ToString());
                    mensaje = mensaje.Replace("{1}", entidad.OBSERVACION_SOLICITUD);
                    break;

            }
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
    }
}
