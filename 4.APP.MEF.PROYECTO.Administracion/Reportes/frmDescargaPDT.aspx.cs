using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Compression;
using System.Text;
using System.IO;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.AltasBajas.Repositorio;
using MEF.PROYECTO.Entity.Administracion;



namespace APP.MEF.ADMINISTRAR.FAG.PAG.Reportes
{
    public partial class frmDescargaPDT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cls_Ent_Planilla_PDT entidad= new Cls_Ent_Planilla_PDT();
                entidad.TIPO_PROCESO = Request.QueryString["TIPO_PROCESO"].ToString();
                entidad.ANIO = Request.QueryString["ANIO"].ToString();
                entidad.MES = Request.QueryString["MES"].ToString();
                Descargar_PDT(entidad);
            }

        }
        private void Descargar_PDT(Cls_Ent_Planilla_PDT entidad)
        {
            try
            {
                string NOMBRE_ARCHIVO = "0601" + entidad.ANIO + Convert.ToInt32(entidad.MES).ToString("D2") + "20131370645" + ".zip";
                List<string> PDTText = Generar_PDT(entidad);
                byte[] ByteZip = null;
                if (PDTText.Count > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                        {

                            var nombre_1 = "0601" + entidad.ANIO + Convert.ToInt32(entidad.MES).ToString("D2") + "20131370645";
                            var File1 = archive.CreateEntry(nombre_1+".4ta");
                            using (var entryStream = File1.Open())
                            using (var streamWriter = new StreamWriter(entryStream))
                            {
                                streamWriter.Write(PDTText[0]);
                            }
                            var nombre_2 = "0601" + entidad.ANIO + Convert.ToInt32(entidad.MES).ToString("D2") + "20131370645";
                            var File2 = archive.CreateEntry(nombre_2+".ps4");
                            using (var entryStream2 = File2.Open())
                            using (var streamWriter2 = new StreamWriter(entryStream2))
                            {
                                streamWriter2.Write(PDTText[1]);
                            }
                        }
                        ms.Seek(0, SeekOrigin.Begin);
                        ByteZip = ms.ToArray();
                    }
                    Response.Clear();
                    Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", NOMBRE_ARCHIVO.Replace(",", "")));
                    Response.ContentType = "application/octet-stream";
                    Response.BinaryWrite(ByteZip);
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                //Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
        }
        private List<string> Generar_PDT(Cls_Ent_Planilla_PDT entidad)
        {
            List<string> ListaTextPDT = new List<string>();
            StringBuilder Plantilla = new StringBuilder();
            List<Cls_Ent_Planilla_PDT> lista = null;
            lista=new  PdtRepositorio().ListaPlanillaPDT(entidad);
            if (lista.Count > 0)
            {
                // pdt 1 
                foreach (Cls_Ent_Planilla_PDT item in lista)
                {
                    Plantilla.Append("" + item.FIJO1 + "|" + item.RUC + "|" + item.FIJO2 + "|"
                        + item.SERIE_COMPROBANTE + "|"+ item.NR_COMPROBANTE + "|" + item.IMPORTE_COMPROBANTE.ToString("00.00") + "|" + item.FECHA_EMISION + "|" + item.FECHA_PAGO + "|1||||\n");
                }
                ListaTextPDT.Add(Plantilla.ToString());
                // pdt 2
                Plantilla = new StringBuilder();
                foreach (Cls_Ent_Planilla_PDT item in lista)
                {
                    Plantilla.Append("" + item.FIJO1 + "|"+ item.RUC+ "|" + item.APELLIDO_PATERNO + "|" + item.APELLIDO_MATERNO + "|"
                         + item.NOMBRES + "|" + item.FIJO3 + "|" + item.FIJO4 + "|\n");
                }
                ListaTextPDT.Add(Plantilla.ToString());
            }
            return ListaTextPDT;
        }
    }
}