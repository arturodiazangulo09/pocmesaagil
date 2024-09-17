using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using System.IO;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Configuration;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio
{
    public class ReconectaUsuario: IDisposable
    {
        public Boolean ReconectaUsuario_(int Codigo)
        {
            Boolean OK = true;
            try
            {
                Cls_Ent_Coordinador datos = new Cls_Ent_Coordinador();
                datos = new CoordinadorRepositorio().ListaCoordinadores(datos).First(A => A.ID_COORDINADOR == Codigo);
                HttpContext.Current.Session["Usuario"] = datos;
            }
            catch (Exception)
            {
                string url = ConfigurationManager.AppSettings["UrlAplicacion"].ToString();
                HttpContext.Current.Response.Redirect(url);
                throw;
            }
            return OK;
        }
        public Boolean ReconectaUsuario_Personal(int Codigo)
        {
            Boolean OK = true;
            try
            {
                Cls_Ent_Personal datos = new Cls_Ent_Personal();
                datos.ID_PERSONAL = Codigo;
                datos = new PersonalReposiorio().ListaPersonal(datos).First(A => A.ID_PERSONAL == Codigo);
                HttpContext.Current.Session["Usuario"] = datos;
            }
            catch (Exception)
            {
                string url = ConfigurationManager.AppSettings["UrlAplicacion"].ToString();
                HttpContext.Current.Response.Redirect(url);
                throw;
            }
            return OK;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}