using System;
using System.IO;
using System.Web;
using System.Web.UI;

namespace APP.ADMINISTRAR.FAG.PAG
{
    public partial class RevisaLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {

                    String Directorio = HttpContext.Current.Request.PhysicalApplicationPath + "Log\\";
                    if (Directory.Exists(Directorio))
                    {
                        string[] Archivos = Directory.GetFiles(Directorio);
                        if (Archivos.Length > 0)
                        {

                        }
                    }

                    //file = HttpContext.Current.Request.PhysicalApplicationPath + "Log\\" + "ConvCASExt" + DateTime.Today.Date.ToString("yyyyMMdd") + ".log";
                    //StreamWriter sw = new StreamWriter(file, true);
                    //sw.WriteLine(DateTime.Now + ": [" + ubicacion + "] - " + mensaje);
                    //sw.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}