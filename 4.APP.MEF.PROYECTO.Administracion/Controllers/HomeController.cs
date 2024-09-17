
using MEF.PROYECTO.Entity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using MEF.PROYECTO.Utilitario;
using APP.ADMINISTRAR.FAG.PAG.Response;
using APP.ADMINISTRAR.FAG.PAG.Core;
using System.Text.RegularExpressions;
using System.Configuration;
using APP.MEF.ADMINISTRAR.FAG.PAG.Response;
namespace APP.ADMINISTRAR.FAG.PAG.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Error()
        {
            return View("Error");
        }

        public ActionResult Index()
        {
            APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.SWLoginClient proxy = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.SWLoginClient();
            APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuariologin = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();
            APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();
            var cook_Token = HttpContext.Request.Cookies["MEF-TOKEN-MIGUEL"];
            if (cook_Token != null)
            {
                int ID_SISTEMA = int.Parse(ConfigurationManager.AppSettings["IdAplicacion_Seguridad"]);
                usuario = proxy.Usuario_Sistema(cook_Token.Value, ID_SISTEMA);
                @ViewBag.NombreCompletoUsuario = usuario.Usuario.Persona.NOMBRE_PERSONA;
                Session["Personal"] = usuario.Usuario;
            }
            else
            {
                Session["Personal"] = null;
                LIMPIAR_COOKIS("MEF-TOKEN-MIGUEL");
                Response.Redirect("../Seguridad/AccesoDenegado");

            }
            ViewBag.version = typeof(HomeController).Assembly.GetName().Version.ToString().Substring(0,3);
            return View();

        }
        public void LIMPIAR_COOKIS(string co)
        {
            HttpCookie myCookie = new HttpCookie(co);
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
        }
        public ActionResult Presentacion()
        {
            return View();
        }
        public ActionResult Modulos_Listar(string CODIGO, string ID_PER)
        {
            APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();
            APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.SWLoginClient proxy = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.SWLoginClient();
            var cook_Token = HttpContext.Request.Cookies["MEF-TOKEN-MIGUEL"];
            ResponseEntity itemRespuesta = new ResponseEntity();
            if (cook_Token != null)
            {
                int ID_SISTEMA = int.Parse(ConfigurationManager.AppSettings["IdAplicacion_Seguridad"]);
                usuario = proxy.Usuario_Sistema(cook_Token.Value, ID_SISTEMA);
                APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadM modulos = proxy.Usuario_ObtenerModulos(usuario.Usuario.TOKEN, ID_SISTEMA, usuario.Usuario.ID_PERFIL.ToString());
                APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Sistemas_Modulos[] Lista_Modulos;
                Lista_Modulos = modulos.Modulos;
                string html = "";
                if (Lista_Modulos.Length == 0)
                {
                    itemRespuesta.extra = "0";
                }
                else
                {
                    Generar_Vista(Lista_Modulos, ref html, 1);
                    itemRespuesta.extra = html;
                }
                itemRespuesta.extra = html;
                return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["Personal"] = null;
                LIMPIAR_COOKIS("MEF-TOKEN-MIGUEL");
                Response.Redirect("../Seguridad/AccesoDenegado");

            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        public void Generar_Vista(APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Sistemas_Modulos[] Menu_Lista, ref string menu, int nivel)
        {
            string cssUL = string.Empty;
            string cssLI = string.Empty;
            string vlInvocar = "InvocaReporte()";
            if (nivel == 1)
            {
                cssUL = "navbar-nav";

            }
            else
            {
                cssUL = "dropdown-menu border-0 shadow";
            }

            if (nivel == 1)
            {
                menu = menu + "<ul  class='" + cssUL + "'>";
                menu += "<li   id=\"liInicio\" class=\"nav-item\">"
                        + "	<a href=\"javascript:void(0);\" class=\"nav-link active\"   id=\"aInicio\" >"
                        + "INICIO"
                        + "	</a>"
                        + " </li>";
            }
            else
            {
                menu = menu + "<ul aria-labelledby=\"dropdownTablasM\"  class='" + cssUL + "' style=\"left: 0px; right: inherit;\" >";//+ Convert.ToChar(13);
            }
            int cuenta = 0;
            foreach (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Sistemas_Modulos _Menu_Lista in Menu_Lista)
            {
                cuenta++;
                bool existe = true;
                string ls_style_li = "";
                if ((existe == true))
                {
                    if (_Menu_Lista.ID_TIPO_MODULO == 5)
                    {
                        ls_style_li = "nav-item dropdown";
                        menu += "<li class='" + ls_style_li + "'  id='" + _Menu_Lista.ID_LI + "' > ";
                        menu += "<a id=\"dropdownTablasM\" href='#' data-toggle=\"dropdown\" aria-haspopup=\"true\"  aria-expanded=\"false\" class=\"nav-link dropdown-toggle\">";
                        menu += "" + _Menu_Lista.DESC_MODULO + "";
                        menu += "</a>";
                    }
                    else
                    {
                        ls_style_li = "nav-item";
                        menu += "<li  class='" + ls_style_li + "' id='" + _Menu_Lista.ID_LI + "' > ";
                        menu += "<a  href='javascript:(0);'id=" + _Menu_Lista.ID_A + " class=\"dropdown-item\" onclick=\"ValidarIngreso(this.id)\" >";
                        menu += "" + _Menu_Lista.DESC_MODULO + "</a>";
                    }

                    if (_Menu_Lista.ID_TIPO_MODULO == 5)
                    {
                        if (_Menu_Lista.Modulos_Hijos != null)
                            if (_Menu_Lista.Modulos_Hijos.Length> 0)
                            {
                                Generar_Vista(_Menu_Lista.Modulos_Hijos, ref menu, 2);
                            }
                    }
                    menu += "</li>" + Convert.ToChar(13);
                }
            }
            menu += "</ul>" + Convert.ToChar(13);
        }


    }
}