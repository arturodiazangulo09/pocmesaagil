using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using MEF.PROYECTO.Entity.Administracion;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.AltasBajas.Repositorio;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Reportes
{
    public partial class DescargaPlanilla : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string PERIODO_PLANILLA = Request.QueryString["PERIODO_PLANILLA"].ToString();
                string ANIO_PLANILLA = Request.QueryString["ANIO_PLANILLA"].ToString();
                string CODIGO_PLANILLA = Request.QueryString["CODIGO_PLANILLA"].ToString();
                Cls_Ent_Planilla_PDT entidad = new Cls_Ent_Planilla_PDT();
                entidad.PERIODO_PLANILLA = PERIODO_PLANILLA;
                entidad.ANIO_PLANILLA = ANIO_PLANILLA; 
                entidad.CODIGO_PLANILLA = CODIGO_PLANILLA;
                Descargar_Planila(entidad);
            }
        }
        private void Descargar_Planila(Cls_Ent_Planilla_PDT entidad)
        {
            try
            {
                int M = int.Parse(entidad.PERIODO_PLANILLA);
                string Mes = M.ToString("D2");
                string NOMBRE_ARCHIVO = "PLL000046" + entidad.ANIO_PLANILLA + Mes + "01"+"04"+ entidad.CODIGO_PLANILLA + ".txt";
                string PlanillaText = Generar_Planilla(entidad);
                byte[] bytes = null;
                using (var ms = new MemoryStream())
                {
                    using (TextWriter tw = new StreamWriter(ms))
                    {
                        tw.Write(PlanillaText);
                        tw.Flush();
                        ms.Position = 0;
                        bytes = ms.ToArray();
                    }
                }
                Response.Clear();
                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", NOMBRE_ARCHIVO.Replace(",", "")));
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytes);
                Response.End();

            }
            catch (Exception ex)
            {
                //Recursos.Clases.Css_Log.Guardar(auditoria.ERROR_LOG);
            }
        }
        private string Generar_Planilla(Cls_Ent_Planilla_PDT entidad)
        {
            StringBuilder Plantilla = new StringBuilder();
            List<Cls_Ent_Planilla_PDT> Lista = null;
            Lista = new PdtRepositorio().ListaPlanillaGeneral(entidad);
            string Anio = entidad.ANIO_PLANILLA;
            int M= int.Parse(entidad.PERIODO_PLANILLA);
            string Mes = M.ToString("D2");
            int TotalRegisto = Lista.Select(x => x.ID_CONFORMIDAD).Distinct().Count();
            decimal TotalRemu = Lista.Where(e => e.FIJO3 == "1").Sum(item => item.MONTO);
            decimal TotalRetencion = Lista.Where(e => e.FIJO3 == "2").Sum(item => item.MONTO);
            Plantilla.Append("000046;" + Anio + ";" + Mes + ";01;04;"+ entidad.CODIGO_PLANILLA + ";" + TotalRegisto + ";" + TotalRemu + ";" + TotalRetencion + ";0.00;0.00;0.00;0.00");
            Plantilla.Append("");
            Plantilla.Append("\n");
            foreach (Cls_Ent_Planilla_PDT item in Lista)
            {
                var Monto = item.MONTO.ToString("00.00");
                Plantilla.Append("" + item.FIJO1 + ";" + item.NUM_DOCUMENTO + ";" + item.FIJO2 + ";"
                    + item.FIJO3 + ";" + item.FIJO4 + ";" + item.FIJO5 + ";" + Monto + "\n"); ;
            }
            return Plantilla.ToString();
        }
    }
}