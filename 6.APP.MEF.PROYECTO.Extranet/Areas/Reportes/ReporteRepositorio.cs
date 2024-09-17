using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using MEF.PROYECTO.Utilitario;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Reportes
{
    public class ReporteRepositorio : IDisposable
    {
        ReportViewer rvReporte = new ReportViewer();
        private void ConfigurarReporte()
        {
            string Usuario = ConfigurationManager.AppSettings["UsuarioReporte"].ToString();
            string Password = ConfigurationManager.AppSettings["Contrasenia"].ToString();
            string UriReporte = ConfigurationManager.AppSettings["UriReporte"].ToString();
            string DominioReporte = ConfigurationManager.AppSettings["DominioReporte"].ToString();

            rvReporte.ShowCredentialPrompts = true;
            rvReporte.ServerReport.ReportServerCredentials = new ReportCredentials(Usuario, Password, DominioReporte);
            rvReporte.ProcessingMode = ProcessingMode.Remote;
            rvReporte.ServerReport.ReportServerUrl = new Uri(UriReporte);
        }
        #region SOLICITUDES FAG - PAC
        public byte[] GenerarSolicitudPDF(long ID_SOLICITUD, string FLG_TIPO)
        {
            byte[] archivo = null;
            String strReporte = "";
            try
            {
                switch (FLG_TIPO)
                {
                    case "S_T_F":
                        strReporte = "Rpt_Solicitud_Tdr_Fag";
                        break;
                    case "S_T_F2":
                        strReporte = "Rpt_Solicitud_Tdr_Fag_v2";
                        break;
                    case "S_T_F_CEU":
                        strReporte = "Rpt_Solicitud_Tdr_Fag_Ceu";
                        break;
                    case "S_C_F":
                        strReporte = "Rpt_Solicitud_Serti_Fag";
                        break;
                    case "S_T_P":
                        strReporte = "Rpt_Solicitud_Tdr_Pac";
                        break;
                    case "S_C_P":
                        strReporte = "Rpt_Solicitud_Serti_Pac";
                        break;
                }
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;
                string rutatarget = ConfigurationManager.AppSettings["RutaReportes"].ToString();
                ConfigurarReporte();
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("P_ID_SOLICITUD", ID_SOLICITUD.ToString()));
                rvReporte.ServerReport.ReportPath = string.Format("{0}/{1}", rutatarget, strReporte);
                rvReporte.ServerReport.SetParameters(parameters);
                archivo = rvReporte.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                this.rvReporte.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                archivo = null;
                Log.MensajeLog(ex.ToString(), "Reporte.ReporteRepositorio.GenerarSolicitudPDF");
            }
            return archivo;
        }
        #endregion
        #region CONTRATO FAG - PAC
        public byte[] GenerarContratoWORD(long ID_SOLICITUD, int ID_PERSONAL, string FLG_TIPO)
        {
            byte[] archivo = null;
            String strReporte = "";
            try
            {
                if (FLG_TIPO == "CONTRATO_FAG")
                {
                    strReporte = "Rpt_Contrato_Fag";
                }
                else
                {
                    strReporte = "Rpt_Contrato_Pac";
                }
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;
                string rutatarget = ConfigurationManager.AppSettings["RutaReportes"].ToString();
                ConfigurarReporte();
                List<ReportParameter> parameters = new List<ReportParameter>();
                rvReporte.ServerReport.ReportPath = string.Format("{0}/{1}", rutatarget, strReporte);
                parameters.Add(new ReportParameter("P_ID_PERSONAL", ID_PERSONAL.ToString()));
                parameters.Add(new ReportParameter("P_ID_SOLICITUD", ID_SOLICITUD.ToString()));
                rvReporte.ServerReport.SetParameters(parameters);
                archivo = rvReporte.ServerReport.Render("WORDOPENXML", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                this.rvReporte.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                archivo = null;
                Log.MensajeLog(ex.ToString(), "Reporte.ReporteRepositorio.GenerarContratoWORD");
            }
            return archivo;
        }
        #endregion
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}