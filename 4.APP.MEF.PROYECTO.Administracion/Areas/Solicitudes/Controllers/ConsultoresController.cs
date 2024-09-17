using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes.Repositorio;
using MEF.PROYECTO.Entity.Personal;
using APP.ADMINISTRAR.FAG.PAG.Response;
using APP.ADMINISTRAR.FAG.PAG.Core;
using System.Threading.Tasks;
using System.IO;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Configuration;
using MEF.PROYECTO.Utilitario;
using APP.MEF.ADMINISTRAR.FAG.PAG.Response;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes.Controllers
{
    public class ConsultoresController : BaseController
    {
        //
        // GET: /Solicitudes/Consultores/
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario();
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario_result = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListaPersonalAdministrador(Cls_Ent_Personal entidad)
        {
            List<Cls_Ent_Personal> lista;
            lista = new ConsultoresRepositorio().ListaPersonalAdministrador(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult PersonalComentarios(int ID_PERSONAL)
        {
            Cls_Ent_Personal modelo = new Cls_Ent_Personal();
            modelo.ID_PERSONAL = ID_PERSONAL;
            return View(modelo);
        }
        public ActionResult EditarPersonalInformacion(int ID_INFORMACION)
        {
            Cls_Ent_Personal modelo = new Cls_Ent_Personal();
            modelo.ACCION = "I";
            modelo.ID_INFORMACION = ID_INFORMACION;
            if (ID_INFORMACION>0)
            {
                modelo = new ConsultoresRepositorio().ListaInformacionPersonal(modelo).FirstOrDefault();
                modelo.ACCION = "U";
            }
            
            return View(modelo);
        }

        public ActionResult MantenimientoInformacion(Cls_Ent_Personal xx)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Personal PreguntaRspta = null;
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
            xx.USU_INGRESO = user;
            xx.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new ConsultoresRepositorio().MentenimientoInformacion_Personal(xx);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();//xx.DES_ERROR;
            }
            else
            {
                
                itemRespuesta.success = true;
                if (xx.ACCION == "I")
                {
                    itemRespuesta.message = "Se registro la información";
              
                }
                if (xx.ACCION == "U")
                {
                    itemRespuesta.message = "Se actualizo la información";

                }
                if (xx.ACCION == "D")
                {
                    itemRespuesta.message = "Se elimino la información";
            
                }
  

            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ListaInformacionPersonal(Cls_Ent_Personal entidad)
        {
            List<Cls_Ent_Personal> lista;
            lista = new ConsultoresRepositorio().ListaInformacionPersonal(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}
