using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP.ADMINISTRAR.FAG.PAG.Response;
using APP.ADMINISTRAR.FAG.PAG.Core;
using System.Configuration;
using MEF.PROYECTO.Utilitario;
namespace APP.ADMINISTRAR.FAG.PAG.Controllers
{
    public class SeguridadController : BaseController
    {
        //
        // GET: /Seguridad/
        public ActionResult Index()
        {
            Session["Personal"] = null;
            LIMPIAR_COOKIS("MEF-TOKEN-MIGUEL");
            return View();
        }
        public ActionResult Validar_usuario(string USUARIO, string PASSWORD)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            try
            {
                APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.SWLoginClient proxy = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.SWLoginClient();
                APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuariologin = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();
                APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();
                usuariologin = proxy.Usuario_Login(USUARIO, PASSWORD);
                if (usuariologin.Usuario_Valido)
                {
                    int ID_SISTEMA = int.Parse(ConfigurationManager.AppSettings["IdAplicacion_Seguridad"]);
                    APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario miusuario = usuariologin.Usuario;
                    usuario = proxy.Usuario_Sistema(miusuario.TOKEN, ID_SISTEMA);
                    if (usuario.Usuario_Valido)
                    {
                        itemRespuesta.success = true;
                        itemRespuesta.TOKEN = miusuario.TOKEN;
                        Session["Personal"] = usuario.Usuario;
                    }
                    else
                    {
                        itemRespuesta.success = false;
                        itemRespuesta.codigo = ("El usuario no tiene permiso a la aplicación SIFP");
                    }
             
                }
                else
                {
                    itemRespuesta.success = false;
                    itemRespuesta.codigo = ("El usuario y/o contraseña no validos");
                }
            }
            catch (Exception ex )
            {
                itemRespuesta.success = false;
                itemRespuesta.codigo = (ex.ToString());
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoGout()
        {
            Session["Personal"] = "";
            LIMPIAR_COOKIS("MEF-TOKEN-MIGUEL");
            return RedirectToAction("Index", "Seguridad");
        }
        public ActionResult AccesoDenegado()
        {

            return View();
        }
        public void LIMPIAR_COOKIS(string co)
        {

            HttpCookie myCookie = new HttpCookie(co);
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
        }
        public ActionResult ValidarCaptcha_Login(string Captcha)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            var valor = Session["ClaveIntraCapchaFagPagLogin"].ToString();
            if (Captcha == valor)
            {
                itemRespuesta.success = true;
            }
            else
            {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }

    }
}
