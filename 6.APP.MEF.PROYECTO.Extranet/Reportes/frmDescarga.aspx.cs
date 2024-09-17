using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Personal;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using MEF.PROYECTO.Utilitario;
using System.Configuration;
namespace APP.MEF.EXTRANET.FAG.PAG.Reportes
{
    public partial class frmDescarga : System.Web.UI.Page
    {
        ReportViewer rv = new ReportViewer();
        Cls_Ent_Personal PersonalSistemaSesion = new Cls_Ent_Personal();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID = int.Parse(Request.QueryString["ID"].ToString());
                string TIPO = Request.QueryString["TIPO"].ToString();
                Descargar(ID, TIPO);
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
        private void Descargar(int ID, string TIPO)
        {
            Byte[] xx = null;
            string nombre = "";
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            string rutatarget = ConfigurationManager.AppSettings["RutaReportes"].ToString();
            ConfigurarReporte();
            switch (TIPO)
            {
                case "RFICHACV":
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Cv", rutatarget);
                    List<ReportParameter> parameters = new List<ReportParameter>();
                    parameters.Add(new ReportParameter("P_ID_PERSONAL", ID.ToString()));
                    rv.ServerReport.SetParameters(parameters);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RFICHACVPAC":
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Cv_Pac", rutatarget);
                    List<ReportParameter> parametersPac = new List<ReportParameter>();
                    parametersPac.Add(new ReportParameter("P_ID_PERSONAL", ID.ToString()));
                    rv.ServerReport.SetParameters(parametersPac);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RFICHACVFAG":
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Cv_Fag", rutatarget);
                    List<ReportParameter> parametersFag = new List<ReportParameter>();
                    parametersFag.Add(new ReportParameter("P_ID_PERSONAL", ID.ToString()));
                    rv.ServerReport.SetParameters(parametersFag);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RFICHACVPAC_PER":
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Cv_Pac_Personal", rutatarget);
                    List<ReportParameter> parametersPacPer = new List<ReportParameter>();
                    parametersPacPer.Add(new ReportParameter("P_ID_PERSONAL", PersonalSistemaSesion.ID_PERSONAL.ToString()));
                    parametersPacPer.Add(new ReportParameter("P_ID_SOLICITUD", ID.ToString()));
                    rv.ServerReport.SetParameters(parametersPacPer);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RFICHACVFAG_PER":
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Cv_Fag_Personal", rutatarget);
                    List<ReportParameter> parametersFagPer = new List<ReportParameter>();
                    parametersFagPer.Add(new ReportParameter("P_ID_PERSONAL", PersonalSistemaSesion.ID_PERSONAL.ToString()));
                    parametersFagPer.Add(new ReportParameter("P_ID_SOLICITUD", ID.ToString()));
                    rv.ServerReport.SetParameters(parametersFagPer);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RANEXO02":
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Solicitud_Anexo02_Fag", rutatarget);
                    List<ReportParameter> parameters_ = new List<ReportParameter>();
                    parameters_.Add(new ReportParameter("P_ID_PERSONAL", PersonalSistemaSesion.ID_PERSONAL.ToString()));
                    parameters_.Add(new ReportParameter("P_ID_SOLICITUD", ID.ToString()));
                    rv.ServerReport.SetParameters(parameters_);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID.ToString() + PersonalSistemaSesion.ID_PERSONAL.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RANEXO03":
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Solicitud_Anexo03_Fag", rutatarget);
                    List<ReportParameter> parameters__ = new List<ReportParameter>();
                    parameters__.Add(new ReportParameter("P_ID_PERSONAL", PersonalSistemaSesion.ID_PERSONAL.ToString()));
                    parameters__.Add(new ReportParameter("P_ID_SOLICITUD", ID.ToString()));
                    rv.ServerReport.SetParameters(parameters__);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID.ToString() + PersonalSistemaSesion.ID_PERSONAL.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RFORMATOB":
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Solicitud_FORMATOB_Pac", rutatarget);
                    List<ReportParameter> parameters_pac = new List<ReportParameter>();
                    parameters_pac.Add(new ReportParameter("P_ID_PERSONAL", PersonalSistemaSesion.ID_PERSONAL.ToString()));
                    parameters_pac.Add(new ReportParameter("P_ID_SOLICITUD", ID.ToString()));
                    rv.ServerReport.SetParameters(parameters_pac);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID.ToString() + PersonalSistemaSesion.ID_PERSONAL.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RFORMATOC":
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Solicitud_FORMATOC_Pac", rutatarget);
                    List<ReportParameter> parameters__pac = new List<ReportParameter>();
                    parameters__pac.Add(new ReportParameter("P_ID_PERSONAL", PersonalSistemaSesion.ID_PERSONAL.ToString()));
                    parameters__pac.Add(new ReportParameter("P_ID_SOLICITUD", ID.ToString()));
                    rv.ServerReport.SetParameters(parameters__pac);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID.ToString() + PersonalSistemaSesion.ID_PERSONAL.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "AR_CONTRA":
                    string nom = "";
                    xx = UtilLaserfiche.ExportarDocumentoPDF(ID, ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref  nom, "");//arch.ARCHIVO;
                    nombre = "Documento_" + ID + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + ".pdf";
                    break;
            }
            Byte[] bytes = xx;
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", nombre));
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytes);
            Response.End();


        }
    }
}