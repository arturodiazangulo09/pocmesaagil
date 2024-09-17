using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.IO;


namespace APP.MEF.EXTRANET.FAG.PAG.Reportes
{
    public partial class frmReportesEntidad : System.Web.UI.Page
    {
        ReportViewer rv = new ReportViewer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID_ENTIDAD = int.Parse(Request.QueryString["ID_ENTIDAD"].ToString());
                string TIPO_CONSULTOR = (Request.QueryString["TIPO_CONSULTOR"].ToString());
                string FECHA = Request.QueryString["FECHA"].ToString();
                string TIPO_DESCARGA = (Request.QueryString["TIPO_DESCARGA"].ToString());
                string ESTADO_SOLICITUD = (Request.QueryString["ESTADO_SOLICITUD"].ToString());
                string TIPO = Request.QueryString["TIPO"].ToString();
                MostrarFormato(ID_ENTIDAD, TIPO_CONSULTOR, FECHA, TIPO_DESCARGA, ESTADO_SOLICITUD, TIPO);
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
        private void MostrarFormato(int ID_ENTIDAD,string TIPO_CONSULTOR, string FECHA, string TIPO_DESCARGA, string ESTADO_SOLICITUD, string TIPO/* String strReporte, int id*/)
        {
            string strReporte = "";
            switch (TIPO)
            {
                case "R_E":
                    strReporte = "Rpt_Detalle_Resumen Ejecutivo";
                    break;
                case "R_D_C_A":
                    strReporte = "Rpt_Detalle_Consultores_Activo";
                    break;
                case "R_S_T":
                    strReporte = "Rpt_Detalle_Solicitudes_Tramite";
                    break;
                case "R_B_C":
                    strReporte = "Rpt_Detalle_Consultores_Baja";
                    break;
            }
            if (TIPO_DESCARGA=="PDF")
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
                ReportParameter[] parameters;
                if (TIPO== "R_S_T")
                {
                    parameters = new ReportParameter[4];
                    parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                    parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                    parameters[3] = new ReportParameter("P_ESTADO", ESTADO_SOLICITUD.ToString());
                    this.rv.ServerReport.SetParameters(parameters);
                }
                else
                {
                    parameters = new ReportParameter[3];
                    parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                    parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                    this.rv.ServerReport.SetParameters(parameters);

                }
   
            
                rv.ServerReport.Refresh();
                renderedBytes = rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                MemoryStream ms = new MemoryStream(renderedBytes);
                Response.ContentType = "Application/pdf";
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
            else
            {
                rv.ProcessingMode = ProcessingMode.Local;
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;
                byte[] renderedBytes = null;
                ConfigurarReporte();
                string rutatarget = ConfigurationManager.AppSettings["RutaReportes"].ToString();
                rv.ServerReport.ReportPath = string.Format("{0}/{1}", rutatarget, strReporte);
                ReportParameter[] parameters;
                if (TIPO == "R_S_T")
                {
                    parameters = new ReportParameter[4];
                    parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                    parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                    parameters[3] = new ReportParameter("P_ESTADO", ESTADO_SOLICITUD.ToString());
                    this.rv.ServerReport.SetParameters(parameters);
                }
                else
                {
                    parameters = new ReportParameter[3];
                    parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                    parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                    this.rv.ServerReport.SetParameters(parameters);

                }
                this.rv.ServerReport.SetParameters(parameters);
                rv.ServerReport.Refresh();
                renderedBytes = rv.ServerReport.Render("EXCELOPENXML", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);//rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                MemoryStream ms = new MemoryStream(renderedBytes);
                Response.ContentType = "application/msword";
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
          

        }

    }
}