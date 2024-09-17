using System;
using System.Web.Configuration;

namespace APP.ADMINISTRAR.FAG.PAG
{
    public partial class SesionFinalizada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rutaLogin = WebConfigurationManager.AppSettings["UrlLoginVirtualCAS"].ToString();
            string mensaje = "Su sesión ha caducado. Presione <a href = '" + rutaLogin + "'> Aquí </a> para ingresar al sistema.";
            ltrmensaje.Text = mensaje;
            ltrurl.Text = "<div id='divUrl' style='display: none'>" + rutaLogin + "</div>";
        }
    }
}