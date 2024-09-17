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
    public partial class frmReporteHonorario : System.Web.UI.Page
    {
        ReportViewer rv = new ReportViewer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string TIPO_CONSULTOR = (Request.QueryString["TIPO_CONSULTOR"].ToString());
                string ANIO = Request.QueryString["ANIO"].ToString();
                string TIPO_DESCARGA = (Request.QueryString["TIPO_DESCARGA"].ToString());
                string DOCUMENTO = (Request.QueryString["DOCUMENTO"].ToString());
                if (TIPO_DESCARGA=="PDF")
                {
                    MostrarFormato_PDF(TIPO_CONSULTOR, ANIO, DOCUMENTO);

                }
                else
                {
                    MostrarFormato_EXCEL(TIPO_CONSULTOR, ANIO, DOCUMENTO);

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
        private void MostrarFormato_PDF(string TIPO_CONSULTOR, string ANIO, string DOCUMENTO)
        {
            string strReporte = "Rpt_Honorario_Contrato";
           
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
            parameters[0] = new ReportParameter("P_DOCUMENTO", DOCUMENTO.ToString());
            parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
            parameters[2] = new ReportParameter("P_ANIO", ANIO.ToString());
                
           
            this.rv.ServerReport.SetParameters(parameters);
            rv.ServerReport.Refresh();
            renderedBytes = rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            MemoryStream ms = new MemoryStream(renderedBytes);
            Response.ContentType = "Application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=" + strReporte + ".pdf" );
            Response.BinaryWrite(ms.ToArray());
            Response.End();




            //rv.ProcessingMode = ProcessingMode.Local;
            //    string format = "pdf";
            //    string deviceInfo = null;
            //    string mimeType = string.Empty;
            //    string encoding = string.Empty;
            //    string fileNameExtension = string.Empty;
            //    string[] streams = null;
            //    Warning[] warnings = null;
            //    byte[] renderedBytes = null;
            //    ConfigurarReporte();
            //    string rutatarget = ConfigurationManager.AppSettings["RutaReportes"].ToString();
            //    rv.ServerReport.ReportPath = string.Format("{0}/{1}", rutatarget, strReporte);
            //    ReportParameter[] parameters;
            //    if (TIPO == "R_S_T")
            //    {
            //        parameters = new ReportParameter[4];
            //        parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
            //        parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
            //        parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
            //        parameters[3] = new ReportParameter("P_ESTADO", ESTADO_SOLICITUD.ToString());
            //        this.rv.ServerReport.SetParameters(parameters);
            //    }
            //    else
            //    {
            //        parameters = new ReportParameter[3];
            //        parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
            //        parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
            //        parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
            //        this.rv.ServerReport.SetParameters(parameters);

            //    }

            //    this.rv.ServerReport.SetParameters(parameters);
            //    rv.ServerReport.Refresh();
            //    renderedBytes = rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //    MemoryStream ms = new MemoryStream(renderedBytes);
            //    Response.ContentType = "Application/pdf";
            //    Response.BinaryWrite(ms.ToArray());
            //    Response.End();
            
       


        }
        private void MostrarFormato_EXCEL(string TIPO_CONSULTOR, string ANIO, string DOCUMENTO)
        {
            string strReporte = "";
          
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
                parameters[0] = new ReportParameter("P_DOCUMENTO", DOCUMENTO.ToString());
                parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                parameters[2] = new ReportParameter("P_ANIO", ANIO.ToString());



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