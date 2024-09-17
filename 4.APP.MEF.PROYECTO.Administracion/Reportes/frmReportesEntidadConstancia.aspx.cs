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
    public partial class frmReportesEntidadConstancia : System.Web.UI.Page
    {
        ReportViewer rv = new ReportViewer();
        protected void Page_Load(object sender, EventArgs e)
        {
            int ID_ENTIDAD = int.Parse(Request.QueryString["ID_ENTIDAD"].ToString());
            string TIPO_CONSULTOR = (Request.QueryString["TIPO_CONSULTOR"].ToString());
            string DOCUMENTO = Request.QueryString["DOCUMENTO"].ToString();
            string ANIO = (Request.QueryString["ANIO"].ToString());
            string TIPO_REPORTE = Request.QueryString["TIPO_REPORTE"].ToString();
            if (TIPO_REPORTE == "C_R_C")
            {
                MostrarFormato_Retencion_PDF(ID_ENTIDAD, TIPO_CONSULTOR, DOCUMENTO, ANIO);
    

            }
            else
            {
                if (TIPO_REPORTE == "C_R_C_D")
                {
                    MostrarFormato_Retencion_Detalle_PDF(ID_ENTIDAD, TIPO_CONSULTOR, DOCUMENTO, ANIO);


                }
                else
                {
                    MostrarFormato_Constancia_PDF(ID_ENTIDAD, TIPO_CONSULTOR, DOCUMENTO, ANIO);
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
        private void MostrarFormato_Retencion_PDF(int ID_ENTIDAD, string TIPO_CONSULTOR, string DOCUMENTO, string ANIO)
        {
            string strReporte = "Rpt_Cuarta_Categoria";
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
                parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("P_ID_ENTIDAD", ID_ENTIDAD.ToString());
                parameters[1] = new ReportParameter("P_TIPO_PROCESO", TIPO_CONSULTOR.ToString());
                parameters[2] = new ReportParameter("P_ANIO", ANIO.ToString());
                parameters[3] = new ReportParameter("P_NUM_DOCUMENTO", DOCUMENTO.ToString());
            this.rv.ServerReport.SetParameters(parameters);
            rv.ServerReport.Refresh();
            renderedBytes = rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            MemoryStream ms = new MemoryStream(renderedBytes);
            Response.ContentType = "Application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=" + "CERTIFICADO_DE_RETENCIONES_" + DOCUMENTO + ".pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.End();
            MostrarFormato_Retencion_Detalle_PDF(ID_ENTIDAD, TIPO_CONSULTOR, DOCUMENTO, ANIO);

        }
        private void MostrarFormato_Retencion_Detalle_PDF(int ID_ENTIDAD, string TIPO_CONSULTOR, string DOCUMENTO, string ANIO)
        {
            string strReporte = "Rpt_Cuarta_Categoria_Detalle";
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
            parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("P_ID_ENTIDAD", ID_ENTIDAD.ToString());
            parameters[1] = new ReportParameter("P_TIPO_PROCESO", TIPO_CONSULTOR.ToString());
            parameters[2] = new ReportParameter("P_ANIO", ANIO.ToString());
            parameters[3] = new ReportParameter("P_NUM_DOCUMENTO", DOCUMENTO.ToString());
            this.rv.ServerReport.SetParameters(parameters);
            rv.ServerReport.Refresh();
            renderedBytes = rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            MemoryStream ms = new MemoryStream(renderedBytes);
            Response.ContentType = "Application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=" + "CERTIFICADO_DE_RETENCIONES_DETALLE" + DOCUMENTO + ".pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.End();

        }

        private void MostrarFormato_Constancia_PDF(int ID_ENTIDAD, string TIPO_CONSULTOR, string DOCUMENTO, string ANIO)
        {
            string strReporte = "Rpt_Constancia_Servicio";
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
            parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("P_ID_ENTIDAD", ID_ENTIDAD.ToString());
            parameters[1] = new ReportParameter("P_TIPO_PROCESO", TIPO_CONSULTOR.ToString());
            parameters[2] = new ReportParameter("P_ANIO", ANIO.ToString());
            parameters[3] = new ReportParameter("P_NUM_CONTRATO", DOCUMENTO.ToString());
            this.rv.ServerReport.SetParameters(parameters);
            rv.ServerReport.Refresh();
            renderedBytes = rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            MemoryStream ms = new MemoryStream(renderedBytes);
            Response.ContentType = "Application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=" + strReporte + ".pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }

    }
}