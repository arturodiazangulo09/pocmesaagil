using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.Coordinador;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Repositorio;
using MEF.PROYECTO.Utilitario;
using System.Configuration;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Reportes
{
    public partial class frmDescarga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID = int.Parse(Request.QueryString["ID"].ToString());
                string TIPO = Request.QueryString["TIPO"].ToString();            
                Descargar(ID, TIPO);
            }
        }
        private void Descargar(int ID, string TIPO)
        {
            Byte[] xx = null;
            string nombre = "";
            switch (TIPO)
            {
                //case "PU":
                //    Cls_Ent_Puesto lista_puesto = null;
                //    using (PuestoRepositorio repositorio = new PuestoRepositorio())
                //    {
                //        lista_puesto.ID_PUESTO = ID;
                //        lista_puesto = repositorio.ListaPuestos(lista_puesto).First();
                //        xx = lista_puesto.ARCHIVO_PUESTO;
                //        nombre = lista_puesto.NOMBRE_ARCHIVO_PUESTO;
                //    };
                //    break;
                case "AR_CONTRA":
                    xx = UtilLaserfiche.ExportarDocumentoPDF(ID, ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref TIPO, "");//arch.ARCHIVO;
                    nombre =  "Documento_" + ID + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + ".pdf";
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