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
    public partial class frmReportAdendaWord : System.Web.UI.Page
    {
          
        ReportViewer rv = new ReportViewer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID = int.Parse(Request.QueryString["ID"].ToString());
                string TIPO = Request.QueryString["TIPO"].ToString();
                MostrarFormatoWord(ID, TIPO);
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
        private void MostrarFormatoWord(int ID, string TIPO)
        {
            String strReporte = "";
            if (TIPO == "P")
            {
                strReporte = "Rpt_Adenda_Pac";
            }
            else
            {
                strReporte = "Rpt_Adenda_Fag";
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
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("P_ID_CONTRATO_DET", ID.ToString());
            this.rv.ServerReport.SetParameters(parameters);
            rv.ServerReport.Refresh();
            renderedBytes = rv.ServerReport.Render("WORDOPENXML", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);//rv.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            MemoryStream ms = new MemoryStream(renderedBytes);
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "Informe_" + DateTime.Now.Year + ".docx"));
            Response.ContentType = "application/msword";
            Response.BinaryWrite(ms.ToArray());
            Response.End();

        }
    }
}