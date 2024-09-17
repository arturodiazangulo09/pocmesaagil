using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Configuration;
using APP.MEF.ADMINISTRAR.FAG.PAG.Response;
using MEF.PROYECTO.Utilitario;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Controllers
{
    public class LaserficheController : Controller
    {
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario();
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario_result = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();

        // GET: Laserfiche
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> ExportarDocRepositorio(int ID_LASERFICHE)
        {
            if (Session["Personal"] != null)
            {
                usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
            }
            else
            {
                var cook_Token = HttpContext.Request.Cookies["MEF-TOKEN-MIGUEL"];
                if (cook_Token != null)
                {
                    int ID_SISTEMA = int.Parse(ConfigurationManager.AppSettings["IdAplicacion_Seguridad"]);
                    using (UsuarioReconectar XX = new UsuarioReconectar())
                    {
                        usuario_result = XX.ConsultaPUsuario(cook_Token.Value, ID_SISTEMA);
                    }
                    Session["Personal"] = usuario_result.Usuario;
                }
                else
                {
                    Session["Personal"] = null;
                    Response.Redirect("../Seguridad/AccesoDenegado");
                }
            }
            string nombre_archivo = "";
            Byte[] bytes = null;
            try
            {
                bytes = UtilLaserfiche.ExportarDocumentoPDF(ID_LASERFICHE, ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nombre_archivo, "");
                using (var stream = new System.IO.MemoryStream(bytes))
                {
                    byte[] buffer = new byte[stream.Length];
                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = nombre_archivo,
                        Inline = false,
                    };
                    Response.AppendHeader("Content-Disposition", cd.ToString());
                    return await Task.FromResult(File(stream.ToArray(), MediaTypeNames.Application.Pdf));
                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), "LaserficheController.ExportarDocRepositorio");
            }
            return await FormatoError();
        }
        public async Task<ActionResult> FormatoError()
        {
            byte[] adjunto = null;
            string rutaBse = Request.PhysicalApplicationPath + "Reportes\\" + "FORMATOS\\" + "ERROR.pdf";
            adjunto = System.IO.File.ReadAllBytes(rutaBse);
            using (var stream = new System.IO.MemoryStream(adjunto))
            {
                byte[] buffer = new byte[stream.Length];
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = "ErrorExportarArchivo.pdf",
                    Inline = false,
                };
                Response.AppendHeader("Content-Disposition", cd.ToString());
                return await Task.FromResult(File(stream.ToArray(), MediaTypeNames.Image.Jpeg));
            }

        }
    }
}