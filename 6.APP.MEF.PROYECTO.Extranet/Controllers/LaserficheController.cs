using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Utilitario;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Configuration;
using MEF.PROYECTO.Entity.Coordinador;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using MEF.PROYECTO.Entity.Personal;
namespace APP.MEF.EXTRANET.FAG.PAG.Controllers
{
    public class LaserficheController : Controller
    {
        Cls_Ent_Coordinador UsuarioSistemaSesion = new Cls_Ent_Coordinador();
        Cls_Ent_Personal PersonalSistemaSesion = new Cls_Ent_Personal();
        // GET: Laserfiche
        public ActionResult Index() 
        {
            return View();
        }
        public async Task<ActionResult> ExportarDocRepositorio(int ID_LASERFICHE)
        {
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
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
        public async Task<ActionResult> ExportarDocPersonalRepositorio(int ID_LASERFICHE)
        {
            if (Session["Usuario"] != null)
            {
                PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_personal = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_Personal(id_personal))
                {
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
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