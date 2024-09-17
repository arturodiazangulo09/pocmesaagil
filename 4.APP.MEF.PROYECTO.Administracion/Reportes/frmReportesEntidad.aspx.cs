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
                string FECHA_INICIO = Request.QueryString["FECHA_INICIO"].ToString();
                string TIPO = Request.QueryString["TIPO"].ToString();
                if (TIPO_DESCARGA=="PDF")
                {
                    MostrarFormato_PDF(ID_ENTIDAD, TIPO_CONSULTOR, FECHA, TIPO_DESCARGA, ESTADO_SOLICITUD, TIPO, FECHA_INICIO);

                }
                else
                {
                    MostrarFormato_EXCEL(ID_ENTIDAD, TIPO_CONSULTOR, FECHA, TIPO_DESCARGA, ESTADO_SOLICITUD, TIPO, FECHA_INICIO);

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
        private void MostrarFormato_PDF(int ID_ENTIDAD, string TIPO_CONSULTOR, string FECHA, string TIPO_DESCARGA, string ESTADO_SOLICITUD, string TIPO, string FECHA_INICIO/* String strReporte, int id*/)
        {
            string strReporte = "";
            switch (TIPO)
            {
                case "R_E":
                    
                    if (ID_ENTIDAD == 0)
                    {
                        strReporte = "Rpt_Entidad_Resumen Ejecutivo";
                    }
                    else
                    {
                        strReporte = "Rpt_Detalle_Resumen Ejecutivo";
                    }
                    break;
                case "R_D_C_A":
                    if (ID_ENTIDAD == 0)
                    {
                        strReporte = "Rpt_Contrato_Vigente";
                    }
                    else
                    {
                        strReporte = "Rpt_Detalle_Consultores_Activo";
                    }
                    break;
                case "R_S_T":
                    
                    if (ID_ENTIDAD == 0)
                    {
                        strReporte = "Rpt_Solicitudes_Tramite";
                    }
                    else
                    {
                        strReporte = "Rpt_Detalle_Solicitudes_Tramite";
                    }
                    break;
                case "R_B_C":
                    if (ID_ENTIDAD == 0)
                    {
                        strReporte = "Rpt_Renuncias_Resumen";
                    }
                    else {
                        strReporte = "Rpt_Detalle_Consultores_Baja";
                    }
                    break;
                case "S_A_FP":
                    strReporte = "Rpt_Solicitud_Adenda";
                    break;
                case "R_E_M":
                    strReporte = "Rpt_Resumen_Ejec_Mensual";
                    break;
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
            ReportParameter[] parameters;
            if (TIPO == "R_S_T" && ID_ENTIDAD > 0)
            {
                parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                parameters[3] = new ReportParameter("P_ESTADO", ESTADO_SOLICITUD.ToString());
                this.rv.ServerReport.SetParameters(parameters);
            }
            else if (TIPO == "R_E_M") {
                parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                this.rv.ServerReport.SetParameters(parameters);
            }else if (TIPO == "R_E" && ID_ENTIDAD==0)
            {
                parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                parameters[1] = new ReportParameter("P_FECHA", FECHA.ToString());
                this.rv.ServerReport.SetParameters(parameters);
            }
            else if (TIPO == "R_E" && ID_ENTIDAD > 0)
            {
                parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                this.rv.ServerReport.SetParameters(parameters);
            }
            else if (TIPO == "R_D_C_A" && ID_ENTIDAD == 0)
            {
                parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                parameters[1] = new ReportParameter("P_FECHA", FECHA.ToString());
                this.rv.ServerReport.SetParameters(parameters);
            }
            else if (TIPO == "S_A_FP")
            {
                parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("P_ID_CONTRATO_DET", TIPO_CONSULTOR.ToString());
                parameters[1] = new ReportParameter("P_TIPO_PROCESO", TIPO_CONSULTOR.ToString());
                this.rv.ServerReport.SetParameters(parameters);
            }
            else if (TIPO == "R_S_T" && ID_ENTIDAD == 0)
            {
                parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                parameters[3] = new ReportParameter("P_ESTADO", ESTADO_SOLICITUD.ToString());
                this.rv.ServerReport.SetParameters(parameters);
            }
            else if (TIPO == "R_B_C" && ID_ENTIDAD == 0)
            {
                parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                parameters[2] = new ReportParameter("P_FECHA_INI", FECHA_INICIO.ToString());
                parameters[3] = new ReportParameter("P_FECHA_FIN", FECHA .ToString());
                this.rv.ServerReport.SetParameters(parameters);
            }
            else if (TIPO == "R_B_C" && ID_ENTIDAD > 0)
            {
                parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
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
        private void MostrarFormato_EXCEL(int ID_ENTIDAD, string TIPO_CONSULTOR, string FECHA, string TIPO_DESCARGA, string ESTADO_SOLICITUD, string TIPO, string FECHA_INICIO/* String strReporte, int id*/)
        {
            string strReporte = "";
            switch (TIPO)
            {
                case "R_E":

                    if (ID_ENTIDAD == 0)
                    {
                        strReporte = "Rpt_Entidad_Resumen Ejecutivo";
                    }
                    else
                    {
                        strReporte = "Rpt_Detalle_Resumen Ejecutivo";
                    }
                    break;
                case "R_D_C_A":
                    if (ID_ENTIDAD == 0)
                    {
                        strReporte = "Rpt_Contrato_Vigente";
                    }
                    else
                    {
                        strReporte = "Rpt_Detalle_Consultores_Activo";
                    }
                    break;
                case "R_S_T":

                    if (ID_ENTIDAD == 0)
                    {
                        strReporte = "Rpt_Solicitudes_Tramite";
                    }
                    else
                    {
                        strReporte = "Rpt_Detalle_Solicitudes_Tramite";
                    }
                    break;
                case "R_B_C":
                    if (ID_ENTIDAD == 0)
                    {
                        strReporte = "Rpt_Renuncias_Resumen";
                    }
                    else
                    {
                        strReporte = "Rpt_Detalle_Consultores_Baja";
                    }
                    break;
                case "S_A_FP":
                    strReporte = "Rpt_Solicitud_Adenda";
                    break;
                case "R_E_M":
                    strReporte = "Rpt_Resumen_Ejec_Mensual";
                    break;
            }

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
                if (TIPO == "R_S_T" && ID_ENTIDAD > 0)
                {
                    parameters = new ReportParameter[4];
                    parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                    parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                    parameters[3] = new ReportParameter("P_ESTADO", ESTADO_SOLICITUD.ToString());
                    this.rv.ServerReport.SetParameters(parameters);
                }
                else if (TIPO == "R_E_M")
                {
                    parameters = new ReportParameter[3];
                    parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                    parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                    this.rv.ServerReport.SetParameters(parameters);
                }
                else if (TIPO == "R_E" && ID_ENTIDAD == 0)
                {
                    parameters = new ReportParameter[2];
                    parameters[0] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[1] = new ReportParameter("P_FECHA", FECHA.ToString());
                    this.rv.ServerReport.SetParameters(parameters);
                }
                else if (TIPO == "R_E" && ID_ENTIDAD > 0)
                {
                    parameters = new ReportParameter[3];
                    parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                    parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                    this.rv.ServerReport.SetParameters(parameters);
                }
                else if (TIPO == "R_D_C_A" && ID_ENTIDAD == 0)
                {
                    parameters = new ReportParameter[2];
                    parameters[0] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[1] = new ReportParameter("P_FECHA", FECHA.ToString());
                    this.rv.ServerReport.SetParameters(parameters);
                }
                else if (TIPO == "S_A_FP")
                {
                    parameters = new ReportParameter[2];
                    parameters[0] = new ReportParameter("P_ID_CONTRATO_DET", TIPO_CONSULTOR.ToString());
                    parameters[1] = new ReportParameter("P_TIPO_PROCESO", TIPO_CONSULTOR.ToString());
                    this.rv.ServerReport.SetParameters(parameters);
                }
                else if (TIPO == "R_S_T" && ID_ENTIDAD == 0)
                {
                    parameters = new ReportParameter[4];
                    parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                    parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
                    parameters[3] = new ReportParameter("P_ESTADO", ESTADO_SOLICITUD.ToString());
                    this.rv.ServerReport.SetParameters(parameters);
                }
                else if (TIPO == "R_B_C" && ID_ENTIDAD == 0)
                {
                    parameters = new ReportParameter[4];
                    parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                    parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[2] = new ReportParameter("P_FECHA_INI", FECHA_INICIO.ToString());
                    parameters[3] = new ReportParameter("P_FECHA_FIN", FECHA.ToString());
                    this.rv.ServerReport.SetParameters(parameters);
                }
                else if (TIPO == "R_B_C" && ID_ENTIDAD > 0)
                {
                    parameters = new ReportParameter[3];
                    parameters[0] = new ReportParameter("P_ENTIDAD", ID_ENTIDAD.ToString());
                    parameters[1] = new ReportParameter("P_TIPO_CONSULTOR", TIPO_CONSULTOR.ToString());
                    parameters[2] = new ReportParameter("P_FECHA", FECHA.ToString());
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
                Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename=" + strReporte + ".xlsx");
            Response.BinaryWrite(ms.ToArray());
                Response.End();
            


        }

    }
}