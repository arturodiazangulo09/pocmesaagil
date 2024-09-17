using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.IO;
using MEF.PROYECTO.Entity.Personal;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
namespace APP.MEF.EXTRANET.FAG.PAG.Reportes
{
    public partial class frmDescargaArchivoPersonal : System.Web.UI.Page
    {
        ReportViewer rv = new ReportViewer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID_PERSONAL = int.Parse(Request.QueryString["ID_PERSONAL"].ToString());
                int ID_SOLICITUD = int.Parse(Request.QueryString["ID_SOLICITUD"].ToString());
                string TIPO = Request.QueryString["TIPO"].ToString();
                int ID = int.Parse(Request.QueryString["ID"].ToString());
                Descargar(ID_PERSONAL, ID_SOLICITUD, TIPO,ID);
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
        private void Descargar(int ID_PERSONAL, int ID_SOLICITUD, string TIPO, int ID)
        {
            Byte[] xx = null;
            string nombre = "";
            //Warning[] warnings;
            //string[] streamids;
            //string mimeType;
            //string encoding;
            //string filenameExtension;
            string rutatarget = ConfigurationManager.AppSettings["RutaReportes"].ToString();
            switch (TIPO)
            {
                //case "CV_F":
                //    Cls_Ent_Estudios ent = new Cls_Ent_Estudios();
                //        ent.ID_PERSONAL = ID_PERSONAL;
                //        ent.ID_SOLICITUD = ID_SOLICITUD;
                //        ent = new PersonalCvRepositorio().ListaEstudios(ent).First(A => A.ID_FORMAC_ACADEMICA== ID);
                //        xx = ent.ARCHIVO;
                //        nombre = ent.NOMBRE_ARCHIVO;
                //    break;
                //case "CV_E":
                //    Cls_Ent_Especializacion ent_ = new Cls_Ent_Especializacion();
                //    ent_.ID_PERSONAL = ID_PERSONAL;
                //    ent_.ID_SOLICITUD = ID_SOLICITUD;
                //    ent_ = new PersonalCvRepositorio().ListaEspecializacion(ent_).First(A=> A.ID_ESPECIALIZACION==ID);
                //        xx = ent_.ARCHIVO;
                //        nombre = ent_.NOMBRE_ARCHIVO;
                //    break;
                //case "CV_C":
                //    Cls_Ent_Capacitacion ent__ = new Cls_Ent_Capacitacion();
                //    ent__.ID_PERSONAL = ID_PERSONAL;
                //    ent__.ID_SOLICITUD = ID_SOLICITUD;
                //    ent__ = new PersonalCvRepositorio().ListaCapacitacion(ent__).First(A=> A.ID_CAPACITACION==ID);
                //        xx = ent__.ARCHIVO;
                //        nombre = ent__.NOMBRE_ARCHIVO;
                //    break;
                //case "CV_EX":
                //    Cls_Ent_Experiencia_Laboral ent_x = new Cls_Ent_Experiencia_Laboral();
                //    ent_x.ID_PERSONAL = ID_PERSONAL;
                //    ent_x.ID_SOLICITUD = ID_SOLICITUD;
                //    ent_x = new PersonalCvRepositorio().ListaExperiencia(ent_x).First(A=> A.ID_EXPERIENCIA==ID);
                //        xx = ent_x.ARCHIVO;
                //        nombre = ent_x.NOMBRE_ARCHIVO;
                //    break;
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