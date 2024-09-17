using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.IO;
using APP.ADMINISTRAR.FAG.PAG;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Reportes
{
    public partial class frmReportePlanilla : System.Web.UI.Page
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
                if (TIPO_DESCARGA == "PDF")
                {
                    MostrarFormato_PDF(ID_ENTIDAD, TIPO_CONSULTOR, FECHA);

                }
                else {
                    MostrarFormato_EXCEL(ID_ENTIDAD, TIPO_CONSULTOR, FECHA);
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
        private void MostrarFormato_PDF(int ID_ENTIDAD, string TIPO_CONSULTOR, string FECHA)
        {
            string strReporte = "Rpt_Detalle_Planilla";
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

            parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
            parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
            parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
            this.rv.ServerReport.SetParameters(parameters);
          

            rv.ServerReport.Refresh();
            renderedBytes = rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            MemoryStream ms = new MemoryStream(renderedBytes);
            Response.ContentType = "Application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=" + strReporte + ".pdf" );
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }
        private void MostrarFormato_EXCEL(int ID_ENTIDAD, string TIPO_CONSULTOR, string FECHA)
        {
            string strReporte = "Rpt_Detalle_Planilla";
      

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

                parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
               
                this.rv.ServerReport.SetParameters(parameters);
                rv.ServerReport.Refresh();
                renderedBytes = rv.ServerReport.Render("EXCELOPENXML", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);//rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                MemoryStream ms = new MemoryStream(renderedBytes);
                Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename=" + strReporte + ".xlsx");
            Response.BinaryWrite(ms.ToArray());
                Response.End();
            


        }

    }
}