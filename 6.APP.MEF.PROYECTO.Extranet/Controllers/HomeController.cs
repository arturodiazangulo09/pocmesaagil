
using MEF.PROYECTO.Entity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using MEF.PROYECTO.Utilitario;
using System.Text.RegularExpressions;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Personal;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Models;
using Newtonsoft.Json;
using System.Linq;
namespace APP.MEF.EXTRANET.FAG.PAG.Controllers
{
    public class HomeController : Controller
    {
        Cls_Ent_Coordinador UsuarioSistemaSesion = new Cls_Ent_Coordinador();
        Cls_Ent_Personal PersonalSistemaSesion = new Cls_Ent_Personal();
        public ActionResult Error()
        {
            return View("Error");
        }

        public ActionResult Index()
        {
            try
            {
                var tipo = HttpContext.Request.Cookies["MEF-TIPO-U-FAGPAC"];
                
                int ID = Convert.ToInt32(tipo.Value);
                @ViewBag.Tipo = ID;
                if (ID==1)
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                    @ViewBag.Usuario_Encriptado = UsuarioSistemaSesion.USUARIO;
                    @ViewBag.NombreCompletoUsuario = UsuarioSistemaSesion.NOMBRE_COMPLETO;
                    @ViewBag.UsuarioSesion = UsuarioSistemaSesion.USUARIO;
                    @ViewBag.Usuario_Codigo = UsuarioSistemaSesion.ID_COORDINADOR;
                    @ViewBag.Tipo_Usuario = UsuarioSistemaSesion.TIPO_USUSARIO;
                    @ViewBag.Flg_Flag = UsuarioSistemaSesion.FLG_FAG;
                    @ViewBag.Flg_Pac = UsuarioSistemaSesion.FLG_PAC;
                    @ViewBag.Id_Entidad = UsuarioSistemaSesion.ID_ENTIDAD;
                    @ViewBag.Desc_NombreEntidad = UsuarioSistemaSesion.DESC_ENTIDAD;
                    @ViewBag.FlgDatosPersonales = "0";
                }
                else
                {
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                    @ViewBag.Usuario_Encriptado = PersonalSistemaSesion.ID_PERSONAL;
                    @ViewBag.NombreCompletoUsuario = PersonalSistemaSesion.APELLIDO_PATERNO  + " " + PersonalSistemaSesion.APELLIDO_MATERNO + " " + PersonalSistemaSesion.NOMBRES ;
                    @ViewBag.UsuarioSesion = PersonalSistemaSesion.USUARIO;
                    @ViewBag.Usuario_Codigo = PersonalSistemaSesion.ID_PERSONAL;
                    @ViewBag.Tipo_Usuario = "0";
                    @ViewBag.Flg_Flag = "0";
                    @ViewBag.Flg_Pac = "0";
                    @ViewBag.Id_Entidad = "0";
                    @ViewBag.Desc_NombreEntidad = "0";
                    @ViewBag.FlgDatosPersonales = PersonalSistemaSesion.FLG_DATOS_PERSONALES;
                }
            }
            catch (Exception)
            {
                Session["Usuario"] = "";
                LIMPIAR_COOKIS("MEF-ID-U-FAGPAC");
                LIMPIAR_COOKIS("MEF-TIPO-U-FAGPAC");
                return RedirectToAction("Index", "Seguridad");
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
            UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            int IdEntidad= UsuarioSistemaSesion.ID_ENTIDAD;
            IList<Cls_Ent_Contrato_Ren> listaContratos = new AdendaRepositorio().ListaContratosRenovar(IdEntidad);
            int CantidadContratos = listaContratos.Count;
            ViewBag.CantidadRenovar = CantidadContratos;
            var jsonResult = Json(listaContratos, JsonRequestBehavior.AllowGet);
            ViewBag.ListaAdendas = jsonResult;
            return View(listaContratos);
        }

        
 public ActionResult VerContrato(string jsondata)
        {
            ViewBag.ListaAdendas = jsondata;
            return View();
        }

    }
}