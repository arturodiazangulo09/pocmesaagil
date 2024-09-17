using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.IO;
using System.Net;
namespace APP.MEF.EXTRANET.FAG.PAG.Reportes
{
  
    public partial class frmReportSolicitud : System.Web.UI.Page
    {
        ReportViewer rv = new ReportViewer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID = int.Parse(Request.QueryString["ID"].ToString());
                string TIPO = Request.QueryString["TIPO"].ToString();
                MostrarFormato(ID, TIPO);
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
        private void MostrarFormato(int ID, string TIPO/* String strReporte, int id*/)
        {
            String strReporte="";
            if (TIPO == "S_T_F")
            {
                 strReporte = "Rpt_Solicitud_Tdr_Fag";
            }
            else
            {
                if (TIPO == "S_C_F")
                {
                     strReporte = "Rpt_Solicitud_Serti_Fag";
                }
                else
                {
                    if (TIPO == "S_T_P")
                    {
                        strReporte = "Rpt_Solicitud_Tdr_Pac";
                    }
                    else
                    {
                        if (TIPO == "S_C_P")
                        {
                            strReporte = "Rpt_Solicitud_Serti_Pac";
                        }

                    }
                }
            }
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
            parameters[0] = new ReportParameter("P_ID_SOLICITUD", ID.ToString());
            this.rv.ServerReport.SetParameters(parameters);
            rv.ServerReport.Refresh();
            renderedBytes = rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            MemoryStream ms = new MemoryStream(renderedBytes);
            Response.ContentType = "Application/pdf";
            Response.BinaryWrite(ms.ToArray());
            Response.End();

        }
    }

}