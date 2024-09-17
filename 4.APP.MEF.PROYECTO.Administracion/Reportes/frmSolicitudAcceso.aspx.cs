using APP.ADMINISTRAR.FAG.PAG;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEF.PROYECTO.Utilitario;
using System.Net;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Reportes
{
    public partial class frmSolicitudAcceso : System.Web.UI.Page
    {
        ReportViewer rv = new ReportViewer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    MostrarFormato("Rpt_Solicitud_Coordinador", Convert.ToInt32(Request.QueryString["ID"].ToString()));

                }

            }
        }
        private void ConfigurarReporte()
        {
            string User = ConfigurationManager.AppSettings["UsuarioReporte"].ToString();
            string Password = ConfigurationManager.AppSettings["Contrasenia"].ToString();
            string UriReporte = ConfigurationManager.AppSettings["UriReporte"].ToString();
            string DominioReporte = ConfigurationManager.AppSettings["DominioReporte"].ToString();
            rv.ShowCredentialPrompts = true;
            rv.ServerReport.ReportServerCredentials = new ReportCredentials(User, Password, DominioReporte);
            rv.ProcessingMode = ProcessingMode.Remote;
            rv.ServerReport.ReportServerUrl = new Uri(UriReporte);
        }
        private void MostrarFormato(String strReporte,int id)
        {
            try
            {
                rv.ProcessingMode = ProcessingMode.Local;
                string format = "pdf";
                string deviceInfo = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string fileNameExtension = string.Empty;
                string[] streams = null;
                Warning[] warnings = null;
                byte[] renderedBytes = null;
                ConfigurarReporte();
                string rutatarget = ConfigurationManager.AppSettings["RutaReportes"].ToString();
                rv.ServerReport.ReportPath = string.Format("{0}/{1}", rutatarget, strReporte);
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("P_ID_COORDINADOR", id.ToString());
                this.rv.ServerReport.SetParameters(parameters);
                rv.ServerReport.Refresh();
                renderedBytes = rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                MemoryStream ms = new MemoryStream(renderedBytes);
                Response.ContentType = "Application/pdf";
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), User.Identity.Name);
                throw;
            }
        

        }
        public class CustomSSRSCredentials : IReportServerCredentials
        {
            private string _SSRSUserName;
            private string _SSRSPassWord;
            private string _DomainName;

            public CustomSSRSCredentials(string UserName, string PassWord, string DomainName)
            {
                _SSRSUserName = UserName;
                _SSRSPassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_SSRSUserName, _SSRSPassWord, _DomainName); }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user,
             out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }
        }
    }
}