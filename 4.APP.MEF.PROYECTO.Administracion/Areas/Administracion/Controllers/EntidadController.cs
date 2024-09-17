using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.BusinessLayer.Administracion;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Repositorio;
using APP.ADMINISTRAR.FAG.PAG.Response;
using APP.ADMINISTRAR.FAG.PAG.Core;
using System.IO;
using APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Models;
using System.Web.UI.WebControls;
using System.Text;
using MEF.PROYECTO.Utilitario;
using APP.MEF.ADMINISTRAR.FAG.PAG.Response;
using System.Configuration;
using System.Net;
using MEF.PROYECTO.Entity.Personal;
using System.Web.Script.Serialization;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Controllers
{
    public class EntidadController : BaseController
    {
        //
        // GET: /Administracion/Entidad/
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario();
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario_result = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DatosMef()
        {
            Cls_Datos_Mef Modelo = new Cls_Datos_Mef();
            Modelo = new EntidadRepositorio().ListaDatosMef().FirstOrDefault();
            return View(Modelo);
        }
        public ActionResult EvaluadorEntidad()
        {
            Cls_Ent_Entidades Modelo = new Cls_Ent_Entidades();
            Modelo.DESCRIPCION = "";
            Modelo = new EntidadRepositorio().ListaEntidades(Modelo).FirstOrDefault();
            return View(Modelo);
        }

        /**PROCESO EVALUADOR**/
        public ActionResult ListaEvaluador(Cls_Ent_Evaluador entidad)
        {
            List<Cls_Ent_Evaluador> lista;
            lista = new EntidadRepositorio().ListaEvaluador(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult NuevoEditEvaluador(int ID_EVALUADOR)
        {
            Cls_Ent_Evaluador modelo = new Cls_Ent_Evaluador();
            modelo.ID_EVALUADOR = ID_EVALUADOR;
            if (ID_EVALUADOR == 0)
            {
                modelo.ACCION = "I";
            }
            else
            {
                modelo = new EntidadRepositorio().ListaEvaluador(modelo).FirstOrDefault(A => A.ID_EVALUADOR == ID_EVALUADOR);
                modelo.ACCION = "U";
            }
            return View(modelo);
        }
        public ActionResult MantenimientoEvaluador(Cls_Ent_Evaluador entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Evaluador PreguntaRspta = null;
            var user = "";
            if (Session["Personal"] != null)
            {
                usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
                user = usuario.COD_USUARIO;
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
                    user = usuario_result.Usuario.COD_USUARIO;
                }
                else
                {
                    Session["Personal"] = null;
                    Response.Redirect("../Seguridad/AccesoDenegado");
                }
            }
            entidad.USU_INGRESO = user;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new EntidadRepositorio().MantenimientoEvaluador(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();
            }
            else
            {
                itemRespuesta.extra = entidad.ID_EVALUADOR.ToString();
                itemRespuesta.success = true;
                if (entidad.ACCION == "I")
                { itemRespuesta.message = "El evaluador se registro correctamente."; }
                if (entidad.ACCION == "U")
                { itemRespuesta.message = "El evaluador se actualizo correctamente."; }
                if (entidad.ACCION == "D")
                { itemRespuesta.message = "El evaluador se elimino correctamente."; }
                if (entidad.ACCION == "H")
                { itemRespuesta.message = "El evaluador se habilito correctamente."; }
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ValidarDuplicidadEvaluador(Cls_Ent_Evaluador entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            List<Cls_Ent_Evaluador> lista = null;
            entidad.DESC_EVALUADOR = entidad.DESC_EVALUADOR.ToUpper();
            lista = new EntidadRepositorio().ListaEvaluador(entidad);
            if (lista.Count > 0)
            {
                itemRespuesta.success = true;
            }
            else
            {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }
        public ActionResult NuevoEntidadAsignar(int ID_EVALUADOR)
        {
            Cls_Ent_Evaluador modelo = new Cls_Ent_Evaluador();
            modelo = new EntidadRepositorio().ListaEvaluador(modelo).FirstOrDefault(A => A.ID_EVALUADOR == ID_EVALUADOR);
            return View(modelo);
        }
        public JsonResult ListarEntidadesAsignadas(Cls_Ent_Entidades entidad)
        {
            IList<Cls_Ent_Entidades> lista = null;
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            lista = Cls_Rule_Entidades.ListaEntidades(xx).FindAll(A => A.ID_PADRE_ENTIDAD==0 && A.EVALUADOR!=entidad.EVALUADOR);
            lista = lista.OrderBy(A => A.DESC_UNIDAD).ToList();
            lista.Insert(0, new Cls_Ent_Entidades() { ID_ENTIDAD = 0, DESC_UNIDAD = "--Seleccione--" });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListaEntidadEvaluador(Cls_Ent_Entidades entidad)
        {
            List<Cls_Ent_Entidades> lista = null;
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            lista = Cls_Rule_Entidades.ListaEntidades(xx).FindAll(A =>  A.EVALUADOR == entidad.EVALUADOR);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult UDP_EvaluadorEntidad(Cls_Ent_Entidades entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Entidades PreguntaRspta = null;
            var user = "";
            if (Session["Personal"] != null)
            {
                usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
                user = usuario.COD_USUARIO;
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
                    user = usuario_result.Usuario.COD_USUARIO;
                }
                else
                {
                    Session["Personal"] = null;
                    Response.Redirect("../Seguridad/AccesoDenegado");
                }
            }
            entidad.USU_INGRESO = user;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new EntidadRepositorio().UDP_EvaluadorEntidad(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();
            }
            else
            {
                itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                itemRespuesta.success = true;
                if (entidad.ACCION == "I")
                { itemRespuesta.message = "La entidad se asigno correctamente al evaluador."; }
                if (entidad.ACCION == "D")
                { itemRespuesta.message = "La entidad se retiro del evaluador."; }
            }

            return Respuesta(itemRespuesta);
        }
        /**FIN PROCESO EVALUADOR**/
        public ActionResult ListaEntidades(Cls_Ent_Entidades entidad)
        {
            List<Cls_Ent_Entidades> lista;

            lista = new EntidadRepositorio().ListaEntidades(entidad);
            lista = lista.FindAll(A => A.ID_PADRE_ENTIDAD == 0);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

     
        public ActionResult NuevoEditaEntidad(int ID_ENTIDAD)
        {
            Cls_Ent_Entidades modelo = new Cls_Ent_Entidades();
            List<Cls_Ent_Entidades> lista = null;
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            lista =new EntidadRepositorio().ListaEntidades(modelo);
           // lista=lista.FindAll(A => A.ID_ENTIDAD.Equals(ID_ENTIDAD));
            if (lista.Count > 0)
            {
                modelo.DESC_ENTIDAD = lista[0].DESC_ENTIDAD;
                modelo.DESC_UNIDAD = lista[0].DESC_UNIDAD;
                modelo.ACRONIMO = lista[0].ACRONIMO;
            }
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            return View(modelo);
        }

        public ActionResult ListaGenerales()
        {
            List<Cls_Datos_Mef> lista;

            lista = new EntidadRepositorio().ListaGenerales();
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult NuevoEditaGeneral(int ID_GENERAL)
        {
            Cls_Datos_Mef modelo = new Cls_Datos_Mef();
            List<Cls_Datos_Mef> lista = null;
            lista = new EntidadRepositorio().ListaGenerales();
            modelo.ID_GENERAL = ID_GENERAL;

            if (ID_GENERAL > 0)
            {
                lista = lista.FindAll(A => A.ID_GENERAL == ID_GENERAL);
                if (lista.Count > 0)
                {
                    modelo.ID_GENERAL = lista[0].ID_GENERAL;
                    modelo.ANIO = lista[0].ANIO;
                    modelo.PIA = lista[0].PIA;
                    modelo.PIM = lista[0].PIM;
                }
            }
            return View(modelo);
        }

        public ActionResult MantenimientoGenerales(Cls_Datos_Mef entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Datos_Mef PreguntaRspta = null;
            var user = "";
            if (Session["Personal"] != null)
            {
                usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
                user = usuario.COD_USUARIO;
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
                    user = usuario_result.Usuario.COD_USUARIO;
                }
                else
                {
                    Session["Personal"] = null;
                    Response.Redirect("../Seguridad/AccesoDenegado");
                }
            }
            entidad.USU_INGRESO = user;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new EntidadRepositorio().MantenimientoGenerales(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.extra = entidad.ID_GENERAL.ToString();
                itemRespuesta.success = true;
                if (entidad.ACCION == "I")
                { itemRespuesta.message = "Se registro correctamente."; }
                if (entidad.ACCION == "U")
                { itemRespuesta.message = "Se actualizó correctamente."; }
                if (entidad.ACCION == "D")
                { itemRespuesta.message = "Sse eliminó correctamente."; }

            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult MantenimientoEntidades(Cls_Ent_Entidades entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Entidades PreguntaRspta = null;
            var user = "";
            if (Session["Personal"] != null)
            {
                usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
                user = usuario.COD_USUARIO;
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
                    user = usuario_result.Usuario.COD_USUARIO;
                }
                else
                {
                    Session["Personal"] = null;
                    Response.Redirect("../Seguridad/AccesoDenegado");
                }
            }
            entidad.USU_INGRESO = user;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new EntidadRepositorio().MantenimientoEntidades(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                itemRespuesta.success = true;
                if (entidad.ACCION =="I")
                { itemRespuesta.message = "La entidad se registro correctamente."; }
                if (entidad.ACCION == "U")
                { itemRespuesta.message = "La entidad se actualizo correctamente."; }
                if (entidad.ACCION == "D")
                { itemRespuesta.message = "La entidad se elimino correctamente."; }
                if (entidad.ACCION == "H")
                { itemRespuesta.message = "La entidad se activo correctamente."; }

                if (entidad.ACCION == "AF")
                { itemRespuesta.message = "Se activo el proceso FAG en la entidad."; }
                if (entidad.ACCION == "DF")
                { itemRespuesta.message = "Se desactivo el proceso FAG en la entidad."; }
                if (entidad.ACCION == "AP")
                { itemRespuesta.message = "Se activo el proceso PAC en la entidad."; }
                if (entidad.ACCION == "DP")
                { itemRespuesta.message = "Se desactivo el proceso PAC en la entidad."; }
                if (entidad.ACCION == "IO")
                { itemRespuesta.message = "El órgano se asigno correctamente a la entidad."; }

            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ValidarDuplicidadEntidad(Cls_Ent_Entidades entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            List<Cls_Ent_Entidades> lista = null;
            lista = new EntidadRepositorio().ListaEntidades(entidad);
            lista = lista.FindAll(A => A.DESC_ENTIDAD==entidad.DESC_ENTIDAD && A.DESC_UNIDAD== entidad.DESC_UNIDAD);
            if (lista.Count > 0)
            {
                itemRespuesta.success = true;
            }
            else {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }

        public ActionResult ValidarDuplicidadGeneral(string anio)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            List<Cls_Datos_Mef> lista = null;
            lista = new EntidadRepositorio().ListaGenerales();
            lista = lista.FindAll(A => A.ANIO == anio);
            if (lista.Count > 0)
            {
                itemRespuesta.success = true;
            }
            else
            {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }
        public ActionResult ProcesoFag(int ID_ENTIDAD, string FLG_PROCESO)
        {
            Cls_Ent_Entidades modelo = new Cls_Ent_Entidades();
            List<Cls_Ent_Entidades> lista = null;
            lista = new EntidadRepositorio().ListaEntidades(modelo);
            lista = lista.FindAll(A => A.ID_ENTIDAD.Equals(ID_ENTIDAD));
            if (lista.Count > 0)
            {
                modelo.DESC_ENTIDAD = lista[0].DESC_ENTIDAD;
            }
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            modelo.DESCRIPCION = FLG_PROCESO == "F" ? "FAG" : "PAC";
            return View(modelo);
        }
        public ActionResult ProcesoPac(int ID_ENTIDAD)
        {
            Cls_Ent_Entidades modelo = new Cls_Ent_Entidades();
            List<Cls_Ent_Entidades> lista = null;
            lista = new EntidadRepositorio().ListaEntidades(modelo);
            lista = lista.FindAll(A => A.ID_ENTIDAD.Equals(ID_ENTIDAD));
            if (lista.Count > 0)
            {
                modelo.DESC_ENTIDAD = lista[0].DESC_ENTIDAD;
            }
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            return View(modelo);
        }
        [HttpPost]
        public ActionResult MantenimientoPeriodoEntidad(Cls_Ent_Entidades entidad)
        {
            
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Entidades PreguntaRspta = null;
            var httpRequest = Request.Files;
            string nomArchivoSave = "";
            int IdLaserfiche = 0;
            if (httpRequest.Count > 0)
            {
             
                var files = Request.Files[0] as HttpPostedFileBase;
                var carpeta = Server.MapPath("~/ArchivoTemp");
                using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                {
                    string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                    files.SaveAs(NombreArchivo);
                    string SubcarpetaLF = entidad.ANIO_PERIODO+ "\\ENTIDAD_" + entidad.ID_ENTIDAD;
                    nomArchivoSave = entidad.ID_ENTIDAD +"_"+ entidad.ANIO_PERIODO + "_"+ RandomString(5, true) + ".pdf";
                    IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "PERIODOS", SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                }
            }
            if (IdLaserfiche > 0)
            {
                var user = "";
                if (Session["Personal"] != null)
                {
                    usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
                    user = usuario.COD_USUARIO;
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
                        user = usuario_result.Usuario.COD_USUARIO;
                    }
                    else
                    {
                        Session["Personal"] = null;
                        Response.Redirect("../Seguridad/AccesoDenegado");
                    }
                }
                entidad.USU_INGRESO = user;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                entidad.FEC_PERIODO_INI = Convert.ToDateTime(entidad.FEC_PERIODO_INI);
                entidad.FEC_PERIODO_FIN = Convert.ToDateTime(entidad.FEC_PERIODO_FIN);
                entidad.ACCION = "I";
                entidad.ARCHIVO = IdLaserfiche;
                PreguntaRspta = new EntidadRepositorio().MantenimientoPeriodoEntidad(entidad);

            }
            else
            {
                throw new Exception("Error al registrar la información del archivo.");

                //PreguntaRspta.FLG_OK = false;
                //entidad.DES_ERROR = "No se pudo registrar el documento en el laserfiche";
            }


            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();
            }
            else
            {
                itemRespuesta.success = true;
                if (entidad.ACCION == "I")
                { itemRespuesta.message = "El periodo se registro correctamente."; }
                if (entidad.ACCION == "U")
                { itemRespuesta.message = "El periodo se actualizo correctamente."; }
            }

            return Respuesta(itemRespuesta);
        }
        [HttpPost]
        public ActionResult UpdatePeriodoEntidad(Cls_Ent_Entidades entidad)
        {

            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Entidades PreguntaRspta = null;
            var httpRequest = Request.Files;
            string nomArchivoSave = "";
            int IdLaserfiche = 0;
            if (httpRequest.Count > 0)
            {

                var files = Request.Files[0] as HttpPostedFileBase;
                var carpeta = Server.MapPath("~/ArchivoTemp");
                using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                {
                    string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                    files.SaveAs(NombreArchivo);
                    string SubcarpetaLF = entidad.ANIO_PERIODO + "\\ENTIDAD_" + entidad.ID_ENTIDAD;
                    nomArchivoSave = entidad.ID_ENTIDAD + "_" + entidad.ANIO_PERIODO + "_" + RandomString(5, true) + ".pdf";
                    IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "PERIODOS", SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                }
            }
            if (IdLaserfiche > 0)
            {
                var user = "";
                if (Session["Personal"] != null)
                {
                    usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
                    user = usuario.COD_USUARIO;
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
                        user = usuario_result.Usuario.COD_USUARIO;
                    }
                    else
                    {
                        Session["Personal"] = null;
                        Response.Redirect("../Seguridad/AccesoDenegado");
                    }
                }
                entidad.USU_INGRESO = user;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                entidad.FEC_PERIODO_INI = Convert.ToDateTime(entidad.FEC_PERIODO_INI);
                entidad.FEC_PERIODO_FIN = Convert.ToDateTime(entidad.FEC_PERIODO_FIN);
                entidad.ACCION = "U";
                entidad.ARCHIVO = IdLaserfiche;
                PreguntaRspta = new EntidadRepositorio().MantenimientoPeriodoEntidad(entidad);

            }
            else
            {
                throw new Exception("Error al registrar la información del archivo.");

                //PreguntaRspta.FLG_OK = false;
                //entidad.DES_ERROR = "No se pudo registrar el documento en el laserfiche";
            }


            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();
            }
            else
            {
                itemRespuesta.success = true;
                if (entidad.ACCION == "I")
                { itemRespuesta.message = "El periodo se registro correctamente."; }
                if (entidad.ACCION == "U")
                { itemRespuesta.message = "El periodo se actualizo correctamente."; }
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ListaPeriodoEntidades(string anio, int id_entidad,string tipo_proceso)
        {
            List<Cls_Ent_Entidades> lista;
            lista = new EntidadRepositorio().ListaPeriodoEntidades().FindAll(A => A.TIPO_PROCESO == tipo_proceso && A.ANIO_PERIODO == anio && A.ID_ENTIDAD == id_entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ListaPeriodoDetalleEntidades(Cls_Ent_Entidades entidad)
        {
            List<Cls_Ent_Entidades> lista;
            lista = new EntidadRepositorio().ListaPeriodoDetalleEntidades();
            lista = lista.FindAll(A => A.ID_PERIODO == entidad.ID_PERIODO );
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult PuestosPac(int ID_ENTIDAD)
        {
            Cls_Ent_Entidades modelo = new Cls_Ent_Entidades();
            List<Cls_Ent_Entidades> lista = null;
            lista = new EntidadRepositorio().ListaEntidades(modelo);
            lista = lista.FindAll(A => A.ID_ENTIDAD.Equals(ID_ENTIDAD));
            if (lista.Count > 0)
            {
                modelo.DESC_ENTIDAD = lista[0].DESC_ENTIDAD;
            }
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            return View(modelo);
        }
        public ActionResult NuevoEditaPuesto(int ID_PUESTO,int ID_ENTIDAD)
        {
            Cls_Ent_Puesto modelo = new Cls_Ent_Puesto();
            modelo.ID_PUESTO = ID_PUESTO;
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            if (ID_PUESTO > 0)
            {
                Cls_Ent_Puesto lista = null;
                lista = new PuestoRepositorio().ListaPuestos(modelo).FirstOrDefault();
                modelo.DES_PUESTO = lista.DES_PUESTO;
            }
            return View(modelo);
        }
        public ActionResult ListaPuestos(Cls_Ent_Puesto entidad)
        {
            List<Cls_Ent_Puesto> lista;
            lista = new PuestoRepositorio().ListaPuestos(entidad);
            lista = lista.FindAll(A => A.ID_ENTIDAD == entidad.ID_ENTIDAD);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult MantenimientoPuesto(Cls_Ent_Puesto entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Puesto PreguntaRspta = null;
            var user = "";
            if (Session["Personal"] != null)
            {
                usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
                user = usuario.COD_USUARIO;
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
                    user = usuario_result.Usuario.COD_USUARIO;
                }
                else
                {
                    Session["Personal"] = null;
                    Response.Redirect("../Seguridad/AccesoDenegado");
                }
            }
            entidad.USU_INGRESO = user;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new PuestoRepositorio().MantenimientoPuestos(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();
            }
            else
            {
                itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                itemRespuesta.success = true;
                if (entidad.ACCION == "I")
                { itemRespuesta.message = "El puesto se registro correctamente."; }
                if (entidad.ACCION == "U")
                { itemRespuesta.message = "El puesto se actualizo correctamente."; }
                if (entidad.ACCION == "D")
                { itemRespuesta.message = "El puesto se elimino correctamente."; }
                if (entidad.ACCION == "H")
                { itemRespuesta.message = "El puesto se activo correctamente."; }
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ValidarDuplicidadPuesto(Cls_Ent_Puesto entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            List<Cls_Ent_Puesto> lista = null;
            lista = new PuestoRepositorio().ListaPuestos(entidad);
            lista = lista.FindAll(A => A.DES_PUESTO.Equals(entidad.DES_PUESTO));
            if (lista.Count > 0)
            {
                itemRespuesta.success = true;
            }
            else
            {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }
        public ActionResult NuevoEditaMontoMensual (int ID_PERIODO, int ID_ENTIDAD, int MES,string TIPO)
        {
            Cls_Ent_Entidades modelo = new Cls_Ent_Entidades();
            modelo.ID_PERIODO = ID_PERIODO;
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            modelo.NUM_MES = MES;
            List<Cls_Ent_Entidades> lista = null;
            lista = new EntidadRepositorio().ListaPeriodoDetalleEntidades();
            lista = lista.FindAll(A => A.ID_PERIODO == ID_PERIODO && A.ID_ENTIDAD == ID_ENTIDAD && A.NUM_MES== MES);
            modelo.MONTO_MENSUAL = lista[0].MONTO_MENSUAL;
            modelo.DES_MES = lista[0].DES_MES;
            modelo.ACCION = TIPO;
            modelo.NOMBRE_ARCHIVO=lista[0].NOMBRE_ARCHIVO;
            modelo.ARCHIVO = lista[0].ARCHIVO;
            return View(modelo);
        }
        public ActionResult UpdateMensualPeriodoEntidad(Cls_Ent_Entidades entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Entidades PreguntaRspta = null;
            var user = "";
            if (Session["Personal"] != null)
            {
                usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
                user = usuario.COD_USUARIO;
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
                    user = usuario_result.Usuario.COD_USUARIO;
                }
                else
                {
                    Session["Personal"] = null;
                    Response.Redirect("../Seguridad/AccesoDenegado");
                }
            }
            entidad.USU_INGRESO = user;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            var httpRequest = Request.Files;
            string nomArchivoSave = "";
            int IdLaserfiche = 0;
            if (httpRequest.Count > 0)
            {
     
                var files = Request.Files[0] as HttpPostedFileBase;
                var carpeta = Server.MapPath("~/ArchivoTemp");
                using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                {
                    string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                    files.SaveAs(NombreArchivo);
                    string SubcarpetaLF = entidad.ANIO_PERIODO + "\\ENTIDAD_" + entidad.ID_ENTIDAD + "_MES_" + entidad.NUM_MES;
                    nomArchivoSave = entidad.ID_ENTIDAD + "_" + entidad.ANIO_PERIODO + "_" + entidad.NUM_MES + "_" + RandomString(5, true) + ".pdf";
                    IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "PERIODOS", SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_INGRESO);
                }
            }
            else {
                Cls_Ent_Entidades ent = null;
                ent = new EntidadRepositorio().ListaPeriodoDetalleEntidades().FirstOrDefault(A => A.ID_PERIODO == entidad.ID_PERIODO && A.ID_ENTIDAD == entidad.ID_ENTIDAD && A.NUM_MES == entidad.NUM_MES);
                entidad.ARCHIVO = ent.ARCHIVO;
                entidad.NOMBRE_ARCHIVO = ent.NOMBRE_ARCHIVO;
            }

            if (IdLaserfiche > 0)
            {
                entidad.ARCHIVO = IdLaserfiche;
                PreguntaRspta = new EntidadRepositorio().UpdateMensualPeriodoEntidad(entidad);

            }
            else
            {
                if (entidad.ARCHIVO>0)
                {
                    PreguntaRspta = new EntidadRepositorio().UpdateMensualPeriodoEntidad(entidad);
                }
                else
                {
                    PreguntaRspta.FLG_OK = false;
                    entidad.DES_ERROR = "No se pudo registrar el documento en el laserfiche";
                }
     
            }

     
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();
                //itemRespuesta.extra = entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.success = true;
                itemRespuesta.message = "La actualización se realizó correctamente."; 
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult VerArchivo(int id, string proceso)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            modelo.ID = id;
            modelo.ACCION = proceso;
            return View(modelo);

        }
        public ActionResult ListaEntidades_Organo(int id)
        {
            List<Cls_Ent_Entidades> lista;
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            lista = new EntidadRepositorio().ListaEntidades(xx);
            lista = lista.FindAll(A => A.ID_PADRE_ENTIDAD == id);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ValidarDuplicidadEntidadOgano(Cls_Ent_Entidades entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            List<Cls_Ent_Entidades> lista = null;
            Cls_Ent_Entidades xxx = new Cls_Ent_Entidades();
             lista = new EntidadRepositorio().ListaEntidades(xxx);
            lista = lista.FindAll(A => A.DESC_ENTIDAD == entidad.DESC_ENTIDAD && A.ID_PADRE_ENTIDAD == entidad.ID_ENTIDAD);
            if (lista.Count > 0)
            {
                itemRespuesta.success = true;
            }
            else
            {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }
        public ActionResult NuevoEditaOrgano(int ID_ENTIDAD)
        {
            Cls_Ent_Entidades modelo = new Cls_Ent_Entidades();
            List<Cls_Ent_Entidades> lista = null;
            lista = new EntidadRepositorio().ListaEntidades(modelo);
            lista = lista.FindAll(A => A.ID_ENTIDAD.Equals(ID_ENTIDAD));
            if (lista.Count > 0)
            {
                modelo.DESC_ENTIDAD = lista[0].DESC_ENTIDAD;
                modelo.DESC_UNIDAD = lista[0].DESC_UNIDAD;
            }
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            return View(modelo);
        }
        public ActionResult GenerarPeriodo(int ID_ENTIDAD, int ID_PERIODO)
        {
            Cls_Ent_Entidades modelo = new Cls_Ent_Entidades();
            List<Cls_Ent_Entidades> list = new List<Cls_Ent_Entidades>();
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            modelo.ID_PERIODO = ID_PERIODO;
            if (ID_PERIODO!=0)
            {
                list = new EntidadRepositorio().ListaPeriodoEntidades().FindAll(A => A.ID_PERIODO == modelo.ID_PERIODO && A.ID_ENTIDAD == modelo.ID_ENTIDAD);
                foreach (var item in list)
                {
                    modelo.ANIO_PERIODO = item.ANIO_PERIODO;
                    modelo.FEC_PERIODO_INI_STRING =  (item.FEC_PERIODO_INI.ToString("dd/MM/yyyy"));
                    modelo.FEC_PERIODO_FIN_STRING = item.FEC_PERIODO_FIN.ToString("dd/MM/yyyy");
                    modelo.MONTO_MENSUAL = item.MONTO_MENSUAL;
                    modelo.ARCHIVO = item.ARCHIVO;
                }
            }

            return View(modelo);
        }
        public ActionResult ValidarDetallePeriodo(Cls_Ent_Entidades entidad)
        {
            int validador = 0;
            int Mes_I = Convert.ToInt32(entidad.FEC_PERIODO_INI.Month);
            int Mes_F = Convert.ToInt32(entidad.FEC_PERIODO_FIN.Month);
            ResponseEntity itemRespuesta = new ResponseEntity();
            List<Cls_Ent_Entidades> lista_I = null;
            lista_I = new EntidadRepositorio().ListaPeriodoDetalleEntidades();
            lista_I = lista_I.FindAll(A => A.ID_ENTIDAD == entidad.ID_ENTIDAD && A.NUM_MES == Mes_I && A.ANIO_PERIODO == entidad.ANIO_PERIODO && A.TIPO_PROCESO == entidad.TIPO_PROCESO && A.ID_PERIODO != entidad.ID_PERIODO);
            if (lista_I.Count>0)
            {
                 validador = 1;
            }
            List<Cls_Ent_Entidades> lista_F = null;
            lista_F = new EntidadRepositorio().ListaPeriodoDetalleEntidades();
            lista_F = lista_F.FindAll(A => A.ID_ENTIDAD == entidad.ID_ENTIDAD && A.NUM_MES == Mes_F && A.ANIO_PERIODO == entidad.ANIO_PERIODO && A.TIPO_PROCESO == entidad.TIPO_PROCESO && A.ID_PERIODO != entidad.ID_PERIODO);
            if (lista_F.Count > 0)
            {
                validador = 1;
            }
            if (validador == 1)
            {
                itemRespuesta.success = true;
            }
            else
            {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }
        public ActionResult GenerarPeriodoEntidadMaestra(int ID_ENTIDAD, string FLG_PROCESO)
        {
            Cls_Ent_Entidades modelo = new Cls_Ent_Entidades();
            List<Cls_Ent_Entidades> lista = null;
            List<Cls_Ent_Entidades> cbo_entidad_Organo = new EntidadRepositorio().ListaEntidades(modelo).FindAll(A => A.ID_PADRE_ENTIDAD.Equals(ID_ENTIDAD));
            cbo_entidad_Organo.Insert(0, new Cls_Ent_Entidades() { ID_ENTIDAD = 0, DESC_ENTIDAD = "--Seleccione--" });

            ViewBag.CboOrgano = cbo_entidad_Organo;

            lista = new EntidadRepositorio().ListaEntidades(modelo).FindAll(A => A.ID_ENTIDAD.Equals(ID_ENTIDAD));
            if (lista.Count > 0)
            {
                modelo.DESC_ENTIDAD = lista[0].DESC_ENTIDAD;
            }
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            modelo.DESCRIPCION = FLG_PROCESO == "F" ? "FAG": "PAC";
            return View(modelo);
        }
        public JsonResult ListarPeriodoCbo(Cls_Ent_Entidades entidad)
        {
            IList<Cls_Ent_Entidades> lista = new EntidadRepositorio().ListaPeriodoEntidades().FindAll(A => A.ANIO_PERIODO == entidad.ANIO_PERIODO && A.ID_ENTIDAD == entidad.ID_ENTIDAD && A.TIPO_PROCESO== entidad.TIPO_PROCESO);
            Cls_Ent_Entidades data = null;
            List<Cls_Ent_Entidades> cboPeriodocBo = new List<Cls_Ent_Entidades>();
            foreach (var item in lista)
            {
                data = new Cls_Ent_Entidades();
                data.ID_PERIODO = item.ID_PERIODO;
                data.DESCRIPCION = item.ANIO_PERIODO;
                data.DESCRIPCION += "-" +  item.FEC_PERIODO_INI.ToShortDateString();
                data.DESCRIPCION +=  "-" + item.FEC_PERIODO_FIN.ToShortDateString();
                cboPeriodocBo.Add(data);
            }
            cboPeriodocBo.Insert(0, new Cls_Ent_Entidades() { ID_PERIODO = 0, DESCRIPCION = "--Seleccione--" });
            ViewBag.cboPeriodocBo = cboPeriodocBo;
            return Json(cboPeriodocBo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GenerarPeriodoOrgano(int ID_ENTIDAD, int ID_PERIODO)
        {
            Cls_Ent_Entidades modelo = new Cls_Ent_Entidades();
            decimal monto_consumido = 0;
            IList<Cls_Ent_Entidades> lista_monto = new EntidadRepositorio().ListaPeriodoEntidades().FindAll(A => A.ID_PERIODO_PADRE == ID_PERIODO);
            foreach (var item in lista_monto)
            {
                monto_consumido += item.MONTO_MENSUAL;
            }
            List<Cls_Ent_Entidades> lista;
            lista = new EntidadRepositorio().ListaPeriodoEntidades().FindAll(A => A.ID_PERIODO == ID_PERIODO);
            ViewBag.FECHA_INICIO= lista[0].FEC_PERIODO_INI.ToShortDateString();
            ViewBag.FECHA_FIN = lista[0].FEC_PERIODO_FIN.ToShortDateString();
            ViewBag.ANIO = lista[0].ANIO_PERIODO;
            modelo.MONTO_MENSUAL= lista[0].MONTO_MENSUAL;
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            modelo.ID_PERIODO = ID_PERIODO;
            modelo.ID_PADRE_ENTIDAD= lista[0].ID_ENTIDAD;
            modelo.MONTO_MENSUAL = lista[0].MONTO_MENSUAL;
            modelo.MONTO_TOTAL = (modelo.MONTO_MENSUAL- monto_consumido);
        
            
            return View(modelo);
        }
        public ActionResult UpdateDatosMef(Cls_Datos_Mef entidad)
        {

            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Datos_Mef PreguntaRspta = null;
            var user = "";
            if (Session["Personal"] != null)
            {
                usuario = (APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.Cls_Ent_Usuario)Session["Personal"];
                user = usuario.COD_USUARIO;
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
                    user = usuario_result.Usuario.COD_USUARIO;
                }
                else
                {
                    Session["Personal"] = null;
                    Response.Redirect("../Seguridad/AccesoDenegado");
                }
            }
            entidad.USU_INGRESO = user;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new EntidadRepositorio().UpdateDatosMef(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText(); //itemRespuesta.extra = entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.success = true;
                itemRespuesta.message = "Los datos se actualizaron correctamente."; 
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ListaDetallePersona(string dni)
        {
            Cls_Datos_Mef datos = new Cls_Datos_Mef();
            Cls_Ent_Encriptacion resultadoEncriptarDNI = new Cls_Ent_Encriptacion();
            Cls_Ent_Encriptacion resultadoReniecDNI = new Cls_Ent_Encriptacion();
            try
            {
                string llave = "";
                llave = GetLlave();

                if (llave != "")
                {
                    llave = System.Web.HttpUtility.UrlEncode(llave);
                    resultadoEncriptarDNI = EncriptarLlave(llave, dni);

                    if (resultadoEncriptarDNI.Code == 1)
                    { //Se encripto correctamente el dni
                        string dniEncriptado = resultadoEncriptarDNI.MensajeSalida;

                        resultadoReniecDNI = ObtenerDatosDNI_RENIEC(llave, dniEncriptado);

                        if (resultadoReniecDNI.Code == 1)
                        {
                            datos.APE_PATERNO = resultadoReniecDNI.Objeto.APE_PATERNO;
                            datos.APE_MATERNO = resultadoReniecDNI.Objeto.APE_MATERNO;
                            datos.NOMBRES = resultadoReniecDNI.Objeto.NOMBRES;
                            datos.ACCION = "1";
                        }
                    }
                }
                /* WCF_DatosPersonales.tabla[] resultado = null;
                 using (WCF_DatosPersonales.ReniecSoapClient proxy = new WCF_DatosPersonales.ReniecSoapClient())
                 {
                     resultado = proxy.TDni(dni);
                     datos.APE_PATERNO = resultado[0].t02.Trim();
                     datos.APE_MATERNO = resultado[0].t03.Trim();
                     datos.NOMBRES = resultado[0].t04.Trim();
                     datos.ACCION = "1";
                 }*/
            }
            catch (Exception ex)
            {
                datos.ACCION = "0";
                datos.DES_ERROR = ex.ToString();

            }

            var jsonResult = Json(datos, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        public String GetLlave()
        {
            string llave = "";
            string metodo = "/sercivios-api/encriptar/traer-llave/";
            var url = ConfigurationManager.AppSettings["Ruta_API_PIDEMEF"].ToString() + metodo;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                            llave = jsSerializer.Deserialize<string>(objReader.ReadToEnd());
                        }
                    }
                }
                return llave;
            }
            catch (WebException ex)
            {
                // Handle error
                return "";
            }
        }
        public Cls_Ent_Encriptacion EncriptarLlave(string llave, string dni)
        {
            string metodo = "/sercivios-api/encriptar/encriptar-valor";
            var baseUrl = ConfigurationManager.AppSettings["Ruta_API_PIDEMEF"].ToString() + metodo;
            string responderParameters = "LLAVE=" + llave + "&TEXT=" + dni;
            var cookieJar = new CookieContainer();
            Cls_Ent_Encriptacion resultado = new Cls_Ent_Encriptacion();
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
                request.CookieContainer = cookieJar;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(responderParameters);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string ResponseAPI = reader.ReadToEnd();
                response.Close();
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                resultado = jsSerializer.Deserialize<Cls_Ent_Encriptacion>(ResponseAPI);

            }
            catch (WebException ex)
            {
                throw ex;
            }

            return resultado;

        }
        public Cls_Ent_Encriptacion ObtenerDatosDNI_RENIEC(string llave, string dni_encriptado)
        {
            dni_encriptado = System.Web.HttpUtility.UrlEncode(dni_encriptado);
            Cls_Ent_Encriptacion resultado = new Cls_Ent_Encriptacion();
            string metodo = "/servicios-pide/reniec/consulpersona";
            var baseUrl = ConfigurationManager.AppSettings["Ruta_API_PIDEMEF"].ToString() + metodo;
            string responderParameters = "LLAVE=" + llave + "&NUM_DOC=" + dni_encriptado;
            var cookieJar = new CookieContainer();

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
                request.CookieContainer = cookieJar;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(responderParameters);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string ResponseAPI = reader.ReadToEnd();
                response.Close();
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                resultado = jsSerializer.Deserialize<Cls_Ent_Encriptacion>(ResponseAPI);
            }
            catch (WebException ex)
            {
                resultado.Code = 0;
                throw ex;

            }
            return resultado;
        }


        protected Stream GetArchivoStream(HttpPostedFileBase archivo)
        {
            if (((archivo != null) && (archivo.ContentLength > 0)))
            {
                return archivo.InputStream;
            }
            throw new ArgumentNullException("El archivo esta vacio");
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
