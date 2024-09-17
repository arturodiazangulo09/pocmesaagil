﻿using System;
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
    public partial class frmReportConformidadWord : System.Web.UI.Page
    {
        ReportViewer rv = new ReportViewer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID_SOLICITUD = int.Parse(Request.QueryString["ID_SOLICITUD"].ToString());
                int NR_MES = int.Parse(Request.QueryString["NR_MES"].ToString());
                string TIPO = Request.QueryString["TIPO"].ToString();
                MostrarFormato(ID_SOLICITUD, NR_MES, TIPO);
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
        private void MostrarFormato(int ID_SOLICITUD, int NR_MES, string TIPO/* String strReporte, int id*/)
        {
            String strReporte = "";
            if (TIPO == "F")
            {
                strReporte = "Rpt_Conformidad_Fag";
            }
            else
            {
                if (TIPO == "FORMATO")
                {
                    strReporte = "Rpt_Informe_Conformidad";
                }
                else
                {
                    strReporte = "Rpt_Conformidad_Pac";
                }
                
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
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("P_NUM_MES", NR_MES.ToString());
            parameters[1] = new ReportParameter("P_ID_CONTRATO", ID_SOLICITUD.ToString());
            this.rv.ServerReport.SetParameters(parameters);
            rv.ServerReport.Refresh();
            renderedBytes = rv.ServerReport.Render("WORDOPENXML", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);//rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            MemoryStream ms = new MemoryStream(renderedBytes);
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "Informe_" + DateTime.Now.Year +"_" + NR_MES.ToString()+".docx"));
            Response.ContentType = "application/msword";
            Response.BinaryWrite(ms.ToArray());
            Response.End();

        }
    }
}