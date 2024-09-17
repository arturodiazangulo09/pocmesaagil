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

namespace APP.MEF.EXTRANET.FAG.PAG.Reportes
{
    public partial class frmDescargaCoordinador : System.Web.UI.Page
    {
        ReportViewer rv = new ReportViewer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID_PERSONAL = int.Parse(Request.QueryString["ID_PERSONAL"].ToString());
                int ID_SOLICITUD = int.Parse(Request.QueryString["ID_SOLICITUD"].ToString());
                string TIPO = Request.QueryString["TIPO"].ToString();
                Descargar(ID_PERSONAL, ID_SOLICITUD,TIPO);
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
        private void Descargar(int ID_PERSONAL,int ID_SOLICITUD, string TIPO)
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
                //case "P":
                //    List<Cls_Ent_Puesto> lista = null;
                //    Cls_Ent_Puesto a = new Cls_Ent_Puesto();
                //    using (CoordinadorRepositorio repositorio = new CoordinadorRepositorio())
                //    {
                //        lista = repositorio.ListaPuestos().FindAll(A => A.ID_PUESTO == ID);
                //        xx = lista[0].ARCHIVO_PUESTO;
                //        nombre = lista[0].NOMBRE_ARCHIVO_PUESTO;
                //    };
                //    break;
                //case "AS":
                //    Cls_Ent_Solicitud a_doc = new Cls_Ent_Solicitud();
                //    using (SolicitudRepositorio repositorio = new SolicitudRepositorio())
                //    {
                //        a_doc = repositorio.ListaSolicitudes(a_doc).First(A => A.ID_SOLICITUD == ID);
                //        xx = a_doc.ARCHIVO_PUESTO_SUS_SOLICITUD;
                //        nombre = a_doc.NOMBRE_ARCHIVO_SUS_SOLICITUD;
                //    };
                //    break;
                //case "ATDR":
                //    Cls_Ent_Solicitud a_TDR = new Cls_Ent_Solicitud();
                //    using (SolicitudRepositorio repositorio = new SolicitudRepositorio())
                //    {
                //        a_doc = repositorio.ListaSolicitudes(a_TDR).First(A => A.ID_SOLICITUD == ID);
                //        xx = a_doc.ARCHIVO_TDR;
                //        nombre = a_doc.NOMBRE_ARCHIVO_TDR;
                //    };
                //    break;
                //case "CV_F":
                //    Cls_Ent_Estudios ent = new Cls_Ent_Estudios();
                //    using (PersonalReposiorio repositorio = new PersonalReposiorio())
                //    {
                //        ent.ID_PERSONAL = 0;
                //        ent.ID_FORMAC_ACADEMICA = ID;
                //        ent = new PersonalReposiorio().ListaEstudios(ent).First();
                //        xx = ent.ARCHIVO;
                //        nombre = ent.NOMBRE_ARCHIVO;
                //    };
                //    break;
                //case "CV_E":
                //    Cls_Ent_Especializacion ent_ = new Cls_Ent_Especializacion();
                //    using (PersonalReposiorio repositorio = new PersonalReposiorio())
                //    {
                //        ent_.ID_PERSONAL = 0;
                //        ent_.ID_ESPECIALIZACION = ID;
                //        ent_ = new PersonalReposiorio().ListaEspecializacion(ent_).First();
                //        xx = ent_.ARCHIVO;
                //        nombre = ent_.NOMBRE_ARCHIVO;
                //    };
                //    break;
                //case "CV_C":
                //    Cls_Ent_Capacitacion ent__ = new Cls_Ent_Capacitacion();
                //    using (PersonalReposiorio repositorio = new PersonalReposiorio())
                //    {
                //        ent__.ID_PERSONAL = 0;
                //        ent__.ID_CAPACITACION = ID;
                //        ent__ = new PersonalReposiorio().ListaCapacitacion(ent__).First();
                //        xx = ent__.ARCHIVO;
                //        nombre = ent__.NOMBRE_ARCHIVO;
                //    };
                //    break;
                //case "CV_EX":
                //    Cls_Ent_Experiencia_Laboral ent_x = new Cls_Ent_Experiencia_Laboral();
                //    using (PersonalReposiorio repositorio = new PersonalReposiorio())
                //    {
                //        ent_x.ID_PERSONAL = 0;
                //        ent_x.ID_EXPERIENCIA = ID;
                //        ent_x = new PersonalReposiorio().ListaExperiencia(ent_x).First();
                //        xx = ent_x.ARCHIVO;
                //        nombre = ent_x.NOMBRE_ARCHIVO;
                //    };
                //    break;
                case "CONTRATO_FAG":
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Contrato_Fag", rutatarget);
                    List<ReportParameter> parameters = new List<ReportParameter>();
                    parameters.Add(new ReportParameter("P_ID_PERSONAL", ID_PERSONAL.ToString()));
                    parameters.Add(new ReportParameter("P_ID_SOLICITUD", ID_SOLICITUD.ToString()));
                    rv.ServerReport.SetParameters(parameters);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID_SOLICITUD.ToString() + ID_PERSONAL.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RANEXO02":
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Solicitud_Anexo02_Fag", rutatarget);
                    List<ReportParameter> parameters_ = new List<ReportParameter>();
                    parameters_.Add(new ReportParameter("P_ID_PERSONAL", ID_PERSONAL.ToString()));
                    parameters_.Add(new ReportParameter("P_ID_SOLICITUD", ID_SOLICITUD.ToString()));
                    rv.ServerReport.SetParameters(parameters_);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID_SOLICITUD.ToString() + ID_PERSONAL.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RANEXO03":
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Solicitud_Anexo03_Fag", rutatarget);
                    List<ReportParameter> parameters__ = new List<ReportParameter>();
                    parameters__.Add(new ReportParameter("P_ID_PERSONAL", ID_PERSONAL.ToString()));
                    parameters__.Add(new ReportParameter("P_ID_SOLICITUD", ID_SOLICITUD.ToString()));
                    rv.ServerReport.SetParameters(parameters__);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID_SOLICITUD.ToString() + ID_PERSONAL.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RFORMATOB":
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Solicitud_FORMATOB_Pac", rutatarget);
                    List<ReportParameter> parameters_pac = new List<ReportParameter>();
                    parameters_pac.Add(new ReportParameter("P_ID_PERSONAL", ID_PERSONAL.ToString()));
                    parameters_pac.Add(new ReportParameter("P_ID_SOLICITUD", ID_SOLICITUD.ToString()));
                    rv.ServerReport.SetParameters(parameters_pac);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID_SOLICITUD.ToString() + ID_PERSONAL.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RFORMATOC":
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Solicitud_FORMATOC_Pac", rutatarget);
                    List<ReportParameter> parameters__pac = new List<ReportParameter>();
                    parameters__pac.Add(new ReportParameter("P_ID_PERSONAL", ID_PERSONAL.ToString()));
                    parameters__pac.Add(new ReportParameter("P_ID_SOLICITUD", ID_SOLICITUD.ToString()));
                    rv.ServerReport.SetParameters(parameters__pac);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID_SOLICITUD.ToString() + ID_PERSONAL.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RFICHACVPAC_PER":
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Cv_Pac_Personal", rutatarget);
                    List<ReportParameter> parametersPacPer = new List<ReportParameter>();
                    parametersPacPer.Add(new ReportParameter("P_ID_PERSONAL", ID_PERSONAL.ToString()));
                    parametersPacPer.Add(new ReportParameter("P_ID_SOLICITUD", ID_SOLICITUD.ToString()));
                    rv.ServerReport.SetParameters(parametersPacPer);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID_SOLICITUD.ToString() + ID_PERSONAL.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
                    break;
                case "RFICHACVFAG_PER":
                    rv.ServerReport.ReportPath = string.Format("{0}/Rpt_Cv_Fag_Personal", rutatarget);
                    List<ReportParameter> parametersFagPer = new List<ReportParameter>();
                    parametersFagPer.Add(new ReportParameter("P_ID_PERSONAL", ID_PERSONAL.ToString()));
                    parametersFagPer.Add(new ReportParameter("P_ID_SOLICITUD", ID_SOLICITUD.ToString()));
                    rv.ServerReport.SetParameters(parametersFagPer);
                    xx = rv.ServerReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    nombre = ID_SOLICITUD.ToString() + ID_PERSONAL.ToString() + "_" + DateTime.Now.Year + DateTime.Now.Second + ".pdf";
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