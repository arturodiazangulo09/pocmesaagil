using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Administracion;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using APP.MEF.EXTRANET.FAG.PAG.Core;
using APP.MEF.EXTRANET.FAG.PAG.Response;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Utilitario;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Models;
using System.IO;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Controllers
{
    public class ProcesoPacController : BaseController
    {
        //
        // GET: /Coordinador/ProcesoPac/
        Cls_Ent_Coordinador UsuarioSistemaSesion = new Cls_Ent_Coordinador();
        StdModel Datos = new StdModel();
        WCF_STD22.hrDto Resultado = new WCF_STD22.hrDto();
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult NuevoEditaPuesto(int ID_PUESTO, int ID_ENTIDAD)
        {
            Cls_Ent_Puesto modelo = new Cls_Ent_Puesto();
            if (ID_PUESTO > 0)
            {
                modelo.ID_PUESTO = ID_PUESTO;
                modelo = new CoordinadorRepositorio().ListaPuestos(modelo).First();
            }
            modelo.ID_PUESTO = ID_PUESTO;
            modelo.ID_ENTIDAD = ID_ENTIDAD;
            modelo.ListaFicha = new List<SelectListItem>();
            modelo.ListaFicha.Add(new SelectListItem { Value = "", Text = "----Seleccione----" });
            modelo.ListaFicha.Add(new SelectListItem { Value = "1", Text = "ASIGNACIÓN DE PUNTAJE PARA EL PUESTO DE DIRECTIVO" });
            modelo.ListaFicha.Add(new SelectListItem { Value = "2", Text = "ASIGNACIÓN DE PUNTAJE PARA EL PUESTO DE ASESOR" });
            modelo.ListaP_H_1_1 = new List<SelectListItem>();
            modelo.ListaP_H_1_1.Add(new SelectListItem { Value = "0", Text = "----" });
            modelo.ListaP_H_1_1.Add(new SelectListItem { Value = "30", Text = "30" });
            modelo.ListaP_H_1_1.Add(new SelectListItem { Value = "20", Text = "20" });
            modelo.ListaP_H_1_2 = new List<SelectListItem>();
            modelo.ListaP_H_1_2.Add(new SelectListItem { Value = "0", Text = "----" });
            modelo.ListaP_H_1_2.Add(new SelectListItem { Value = "30", Text = "30" });
            modelo.ListaP_H_1_2.Add(new SelectListItem { Value = "20", Text = "20" });
            modelo.ListaP_H_1_3 = new List<SelectListItem>();
            modelo.ListaP_H_1_3.Add(new SelectListItem { Value = "0", Text = "----" });
            modelo.ListaP_H_1_3.Add(new SelectListItem { Value = "30", Text = "30" });
            modelo.ListaP_H_1_3.Add(new SelectListItem { Value = "20", Text = "20" });
            modelo.ListaP_H_2_1 = new List<SelectListItem>();
            modelo.ListaP_H_2_1.Add(new SelectListItem { Value = "0", Text = "----" });
            modelo.ListaP_H_2_1.Add(new SelectListItem { Value = "15", Text = "15" });
            modelo.ListaP_H_2_1.Add(new SelectListItem { Value = "12", Text = "12" });
            modelo.ListaP_H_2_2 = new List<SelectListItem>();
            modelo.ListaP_H_2_2.Add(new SelectListItem { Value = "0", Text = "----" });
            modelo.ListaP_H_2_2.Add(new SelectListItem { Value = "15", Text = "15" });
            modelo.ListaP_H_2_2.Add(new SelectListItem { Value = "12", Text = "12" });
            modelo.ListaP_H_3_1 = new List<SelectListItem>();
            modelo.ListaP_H_3_1.Add(new SelectListItem { Value = "0", Text = "----" });
            modelo.ListaP_H_3_1.Add(new SelectListItem { Value = "20", Text = "20" });
            modelo.ListaP_H_3_1.Add(new SelectListItem { Value = "18", Text = "18" });
            modelo.ListaP_H_3_2 = new List<SelectListItem>();
            modelo.ListaP_H_3_2.Add(new SelectListItem { Value = "0", Text = "----" });
            modelo.ListaP_H_3_2.Add(new SelectListItem { Value = "20", Text = "20" });
            modelo.ListaP_H_3_2.Add(new SelectListItem { Value = "18", Text = "18" });
            modelo.ListaP_I_1_1 = new List<SelectListItem>();
            modelo.ListaP_I_1_1.Add(new SelectListItem { Value = "0", Text = "----" });
            modelo.ListaP_I_1_1.Add(new SelectListItem { Value = "25", Text = "25" });
            modelo.ListaP_I_1_1.Add(new SelectListItem { Value = "20", Text = "20" });
            modelo.ListaP_I_2_1 = new List<SelectListItem>();
            modelo.ListaP_I_2_1.Add(new SelectListItem { Value = "0", Text = "----" });
            modelo.ListaP_I_2_1.Add(new SelectListItem { Value = "25", Text = "25" });
            modelo.ListaP_I_2_1.Add(new SelectListItem { Value = "20", Text = "20" });
            modelo.ListaP_I_3_1 = new List<SelectListItem>();
            modelo.ListaP_I_3_1.Add(new SelectListItem { Value = "0", Text = "----" });
            modelo.ListaP_I_3_1.Add(new SelectListItem { Value = "25", Text = "25" });
            modelo.ListaP_I_3_1.Add(new SelectListItem { Value = "20", Text = "20" });
            modelo.ListaP_I_4_1 = new List<SelectListItem>();
            modelo.ListaP_I_4_1.Add(new SelectListItem { Value = "0", Text = "----" });
            modelo.ListaP_I_4_1.Add(new SelectListItem { Value = "25", Text = "25" });
            modelo.ListaP_I_4_1.Add(new SelectListItem { Value = "20", Text = "20" });
            return View(modelo);
        }
        public ActionResult ListaPuestos(Cls_Ent_Puesto entidad)
        {
            List<Cls_Ent_Puesto> lista;
            lista = new CoordinadorRepositorio().ListaPuestos(entidad).FindAll(A => A.ID_ENTIDAD == entidad.ID_ENTIDAD);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult MantenimientoPuesto(Cls_Ent_Puesto entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Puesto PreguntaRspta = null;
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
            var httpRequest = Request.Files;
            long IdLaserfiche = 0;
            if (httpRequest.Count > 0)
            {

                var files = Request.Files[0] as HttpPostedFileBase;
                var tempFile = new byte[files.ContentLength];
                var carpeta = Server.MapPath("~/ArchivoTemp");
                string nomArchivoSave = "";
                using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                {
                    string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                    files.SaveAs(NombreArchivo);
                    string SubcarpetaLF = "PUESTOS_PAC"  + "\\" + DateTime.Now.Year.ToString();
                    nomArchivoSave = entidad.DES_PUESTO+ "_" + RandomString(5, true) + ".pdf";
                    IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                    entidad.ARCHIVO_PUESTO = IdLaserfiche;
                }
            }
            else {
                Cls_Ent_Puesto ent = new Cls_Ent_Puesto();
                ent.ID_PUESTO = entidad.ID_PUESTO;
                ent = new CoordinadorRepositorio().ListaPuestos(ent).First();
                entidad.ARCHIVO_PUESTO = ent.ARCHIVO_PUESTO;
                entidad.NOMBRE_ARCHIVO_PUESTO = ent.NOMBRE_ARCHIVO_PUESTO;
            }

            entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new CoordinadorRepositorio().MantenimientoPuestos(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
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
            lista = new CoordinadorRepositorio().ListaPuestos(entidad);
            lista = lista.FindAll(A => A.DES_PUESTO == entidad.DES_PUESTO && A.ID_ENTIDAD == entidad.ID_ENTIDAD && A.FLG_ESTADO == 1);
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
        public ActionResult VerArchivo(int id, string proceso)
        {
            BusquedaModelView modelo = new BusquedaModelView();

            modelo.ID = id;
            modelo.ACCION = proceso;
            return View(modelo);

        }
        public ActionResult NuevoCalificarPro(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
            modelo.TIPO_PROCESO = "P";
            modelo = new SolicitudRepositorio().ListaSolicitudes(modelo).First(A => A.ID_SOLICITUD == ID_SOLICITUD);
            modelo.listaRequisitos = new SolicitudRepositorio().ListaRequisitos(ID_SOLICITUD);
            modelo.ListaCumpleRequisitos = new List<SelectListItem>();
            modelo.ListaCumpleRequisitos.Add(new SelectListItem { Value = "", Text = "--------------------" });
            modelo.ListaCumpleRequisitos.Add(new SelectListItem { Value = "0", Text = "NO CUMPLE" });
            modelo.ListaCumpleRequisitos.Add(new SelectListItem { Value = "1", Text = "CUMPLE" });
            return View(modelo);
        }
        public ActionResult MantenimientoAplicaPersonal(Cls_Ent_Solicitud entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud PreguntaRspta = null;
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
            entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new PersonalCvRepositorio().UpdAplicaRegistoCv(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                itemRespuesta.success = true;
                if (entidad.ACCION == "ACAD")
                {
                    if (entidad.FLG_ESTADO == 1)
                    { itemRespuesta.message = "El registro se aplicó correctamente."; }
                    else
                    { itemRespuesta.message = "El registro se desactivo correctamente."; }
                }
                if (entidad.ACCION == "ESP")
                {
                    if (entidad.FLG_ESTADO == 1)
                    { itemRespuesta.message = "El registro se aplicó correctamente."; }
                    else
                    { itemRespuesta.message = "El registro se desactivo correctamente."; }
                }
                if (entidad.ACCION == "CAPA")
                {
                    if (entidad.FLG_ESTADO == 1)
                    { itemRespuesta.message = "El registro se aplicó correctamente."; }
                    else
                    { itemRespuesta.message = "El registro se desactivo correctamente."; }
                }
                if (entidad.ACCION == "EXPE")
                {
                    if (entidad.FLG_ESTADO == 1)
                    { itemRespuesta.message = "El registro se aplicó correctamente."; }
                    else
                    { itemRespuesta.message = "El registro se desactivo correctamente."; }
                }
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult UpdCalificacionRequisitos(Cls_Ent_Solicitud entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud PreguntaRspta = null;
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
            entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new PersonalCvRepositorio().UpdCalificacionRequisitos(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
            }
            else
            {
                itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                itemRespuesta.success = true;
                itemRespuesta.message = "Se registró correctamente la calificación del Profesional.";
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult UpdNotificaUTP(Cls_Ent_Solicitud entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud PreguntaRspta = null;
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
            entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            // PROCESO TRAMITE

            var permite = ConfigurationManager.AppSettings["PermiteTramite"].ToString();
            if (permite == "1")
            {
                if (entidad.FLG_CON_HR=="2")
                {
                    entidad.ID_TRAMITE = 1;
                  //  entidad.NR_TRAMITE =;
                    PreguntaRspta = new SolicitudRepositorio().UpdNotificaUTP(entidad);
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
                        entidad = new SolicitudRepositorio().ListaSolicitudes(entidad).First();
                        if (EnviarEmailAccionCoordinador(entidad))
                        {
                            itemRespuesta.message = "Se realizó la notificación para la generación de contrato.";
                        }
                        else
                        {
                            itemRespuesta.message = "Error al realizar la notificación.";
                        }
                    }
                }
                else
                {
                    if (entidad.ID_TRAMITE == 0)
                    {
                        Cls_Ent_Entidades entidad_desc = new Cls_Ent_Entidades();
                        entidad_desc.ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD;
                        entidad_desc = new CoordinadorRepositorio().ListaEntidades(entidad_desc).First();
                        Datos.NUM_REGISTRO = entidad.ID_SOLICITUD.ToString();
                        Datos.NUM_OFICIO = entidad.NR_OFICIO;//"1021-2022/GR-AREQUIPA";
                        Datos.NUM_FOLIOS = entidad.NR_FOLIOS;
                        Datos.ASUNTO = entidad.ASUNTO;//"SOLICITA LISTA DE EXPEDIENTES";
                        if (entidad.TIPO_PROCESO == "F")
                        {
                            Datos.CLASIFICACION = "SOLICITUD DE CONTRATACIÓN FAG";
                        }
                        else
                        {
                            Datos.CLASIFICACION = "SOLICITUD DE CONTRATACIÓN PAC";
                        }


                        Datos.RAZONSOCIAL = UsuarioSistemaSesion.DESC_ENTIDAD_PRINCIPAL;
                        Datos.RUC = entidad_desc.RUC;
                        Datos.SEC_EJECT = "";
                        Datos.DIRECCION = entidad_desc.DIRECCION;
                        Datos.DEPARTAMENTO = entidad_desc.DES_DEPARTAMENTO;
                        Datos.PROVINCIA = entidad_desc.DES_PROVINCIA;
                        Datos.DISTRITO = entidad_desc.DES_DISTRITO;
                        Datos.CORREO = UsuarioSistemaSesion.CORREO_NOTIFICADOR;
                        Datos.OBSERVACION = "SIN OBSERVACIONES";
                        try
                        {
                            using (StdRepositorio xx = new StdRepositorio())
                            {
                                // byte[] archivo = UtilLaserfiche.ExportarDocumentoPDF(1653, ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", "", "");
                                var files = Request.Files[0] as HttpPostedFileBase;
                                var tempFile = new byte[files.ContentLength];
                                files.InputStream.Read(tempFile, 0, files.ContentLength);

                                var files2 = Request.Files[1] as HttpPostedFileBase;
                                var tempFile2 = new byte[files2.ContentLength];
                                files2.InputStream.Read(tempFile2, 0, files2.ContentLength);

                                Datos.OFICIO = xx.Archivo_Adjunto(tempFile, "OFICIO.PDF", tempFile.Length);
                                Datos.RESUMEN = xx.Archivo_Adjunto(tempFile2, "RESUMEN.PDF", tempFile2.Length);
                                WCF_STD22.anexoDto anexoDto = null;
                                List<WCF_STD22.anexoDto> intermediate_list = new List<WCF_STD22.anexoDto>();
                                List<Cls_Ent_Archivo_STD> lista_Arch = xx.ListaArchivoSTD(entidad.ID_SOLICITUD);
                                foreach (var item in lista_Arch)
                                {
                                    anexoDto = new WCF_STD22.anexoDto();
                                    string nom = "";
                                    anexoDto.archivo = UtilLaserfiche.ExportarDocumentoPDF(item.ID_ARCHIVO, ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nom, "");
                                    anexoDto.name = item.FORMATO;
                                    anexoDto.length = anexoDto.archivo.Length;
                                    intermediate_list.Add(anexoDto);
                                }
                                Datos.ANEXOS = intermediate_list.ToArray();
                                Datos.REMOTEADDRESS = entidad.IP_PC;
                                Resultado = xx.Crear_Expediente_Std(Datos);
                            }
                            entidad.ID_TRAMITE = Resultado.iddoc;
                            entidad.NR_TRAMITE = Resultado.numeroSid + '-' + Resultado.numeroAnio;
                            /*VALIDAR*/
                            if (Resultado.iddoc > 0)
                            {
                                PreguntaRspta = new SolicitudRepositorio().UpdNotificaUTP(entidad);
                            }
                            else
                            {
                                PreguntaRspta.FLG_OK = false;
                                entidad.DES_ERROR = "No se pudo generar la Hoja de Ruta, intentar en unos minutos.";
                            }

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
                                if (Resultado.iddoc > 0)
                                {
                                    itemRespuesta.message2 = "Se genero el numero de tramite " + entidad.NR_TRAMITE;
                                }
                                else
                                {
                                    itemRespuesta.message2 = "0"; ;
                                }

                                entidad = new SolicitudRepositorio().ListaSolicitudes(entidad).First();
                                if (EnviarEmailAccionCoordinador(entidad))
                                {
                                    itemRespuesta.message = "Se realizó la notificación para la generación de contrato.";
                                }
                                else
                                {
                                    itemRespuesta.message = "Error al realizar la notificación.";
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            itemRespuesta.success = false;
                            itemRespuesta.extra = "No se pudo generar la Hoja de Ruta, intentar en unos minutos.";
                            //   itemRespuesta.extra = ex.ToString();
                        }

                    }
                    else
                    {
                        PreguntaRspta = new SolicitudRepositorio().UpdNotificaUTP(entidad);
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

                            entidad = new SolicitudRepositorio().ListaSolicitudes(entidad).First();
                            if (EnviarEmailAccionCoordinador(entidad))
                            {
                                itemRespuesta.message = "Se realizó la notificación para la generación de contrato.";
                            }
                            else
                            {
                                itemRespuesta.message = "Error al realizar la notificación.";
                            }
                        }
                    }
                }
       
            }
            else
            {
                PreguntaRspta = new SolicitudRepositorio().UpdNotificaUTP(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                    itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                    itemRespuesta.success = true;
                    itemRespuesta.message2 = "0"; 
         
                    entidad = new SolicitudRepositorio().ListaSolicitudes(entidad).First();
                    if (EnviarEmailAccionCoordinador(entidad))
                    {
                        itemRespuesta.message = "Se realizó la notificación para la generación de contrato.";
                    }
                    else
                    {
                        itemRespuesta.message = "Error al realizar la notificación.";
                    }
                }
            }

            // FIN PROCESO TRAMITE 

     

            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailAccionCoordinador(Cls_Ent_Solicitud entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarUTP_Contrato.html"))
            {
                mensaje = sr.ReadToEnd();
            }
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
            mensaje = mensaje.Replace("{0}", UsuarioSistemaSesion.APELLIDO_PATERNO + " " + UsuarioSistemaSesion.APELLIDO_MATERNO + " " + UsuarioSistemaSesion.NOMBRES);
            mensaje = mensaje.Replace("{1}", UsuarioSistemaSesion.DESC_ENTIDAD);
            mensaje = mensaje.Replace("{2}", entidad.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{3}", entidad.TIPO_PROCESO == "F" ? "FAG LEY N° 25650" : "PAC LEY N° 29806");
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = ConfigurationManager.AppSettings["CorreoCopia"];
            bool strRet = EnviarMail.SendMailMessage(ref strMsgError, destinatarios, "", "", titulo, mensaje, "", ConfigurationManager.AppSettings["Usermail"]);

            if (strRet)
            {
                return true;
            }
            else
            {
                ModelState.AddModelError("Error", strMsgError);
                return false;
            }
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
        #region SOLICITUDES V.2
        public ActionResult NuevoEditaSolicitud_v2(int ID_SOLICITUD, string PROCESO, int ID_ENTIDAD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
            List<Cls_Ent_Entidades> Lista_Entidad = null;
            Cls_Ent_Entidades xxx = new Cls_Ent_Entidades();
            Lista_Entidad = new SolicitudRepositorio().ListaEntidades(xxx).FindAll(A => A.ID_ENTIDAD == ID_ENTIDAD);
            ViewBag.DcboEntidades = Lista_Entidad;
            modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString();
            IList<Cls_Ent_Puesto> lista_Puesto;
            Cls_Ent_Puesto xx = new Cls_Ent_Puesto();
            lista_Puesto = new CoordinadorRepositorio().ListaPuestos(xx).FindAll(A => A.ID_ENTIDAD == ID_ENTIDAD && A.FLG_ESTADO == 1);
            lista_Puesto.Insert(0, new Cls_Ent_Puesto() { ID_PUESTO = 0, DES_PUESTO = "--Seleccione--" });
            ViewBag.DcboPuestos = lista_Puesto;
            modelo.TIPO_PROCESO = "P";
            ViewBag.ListaServicio = JsonConvert.SerializeObject(new List<Cls_Ent_Funciones_Solicitud>());
            ViewBag.ListaAcademica = JsonConvert.SerializeObject(new List<Cls_Ent_Academico>());
            ViewBag.ListaCursos = JsonConvert.SerializeObject(new List<Cls_Ent_Curso>());
            ViewBag.ListaConocimientos = JsonConvert.SerializeObject(new List<Cls_Ent_Conocimientos>());
            ViewBag.ListaActividad = JsonConvert.SerializeObject(new List<Cls_Ent_Actividad>());
            ViewBag.ListaActividadOtro = JsonConvert.SerializeObject(new List<Cls_Ent_Actividad_Otro>());
            ViewBag.ListaExperiencia = JsonConvert.SerializeObject(new List<Cls_Ent_Experiencia>());
            ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(new List<Cls_Ent_Renumeracion>());
            if (ID_SOLICITUD == 0)
            {
                modelo.FLG_PROCESO = "I";
            }
            else
            {
                var lista = new SolicitudRepositorio().ListaSolicitudes(new Cls_Ent_Solicitud { ID_SOLICITUD = ID_SOLICITUD }).First();
                modelo = lista;
                modelo.FLG_PROCESO = "U";
                modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString();
                List<Cls_Ent_Funciones_Solicitud> X;
                Cls_Ent_Funciones_Solicitud EX = new Cls_Ent_Funciones_Solicitud();
                EX.ID_SOLICITUD = ID_SOLICITUD;
                X = new SolicitudRepositorio().ListaServicio(EX);
                if (X.Count > 0)
                {
                    ViewBag.ListaServicio = JsonConvert.SerializeObject(X);
                }
                List<Cls_Ent_Experiencia> XX;
                Cls_Ent_Experiencia EXX = new Cls_Ent_Experiencia();
                EXX.ID_SOLICITUD = ID_SOLICITUD;
                XX = new SolicitudRepositorio().ListaExperiencia(EXX);
                if (XX.Count > 0)
                {
                    ViewBag.ListaExperiencia = JsonConvert.SerializeObject(XX);
                }
                List<Cls_Ent_Academico> XXX;
                Cls_Ent_Academico EXXX = new Cls_Ent_Academico();
                EXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXX = new SolicitudRepositorio().ListaAcademica(EXXX);
                if (XXX.Count > 0)
                {
                    ViewBag.ListaAcademica = JsonConvert.SerializeObject(XXX);
                }
                List<Cls_Ent_Curso> XXXX;
                Cls_Ent_Curso EXXXX = new Cls_Ent_Curso();
                EXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXX = new SolicitudRepositorio().ListaCursos(EXXXX);
                if (XXXX.Count > 0)
                {
                    ViewBag.ListaCursos = JsonConvert.SerializeObject(XXXX);
                }
                List<Cls_Ent_Conocimientos> XXXXX;
                Cls_Ent_Conocimientos EXXXXX = new Cls_Ent_Conocimientos();
                EXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXX = new SolicitudRepositorio().ListaConocimientos(EXXXXX);
                if (XXXXX.Count > 0)
                {
                    ViewBag.ListaConocimientos = JsonConvert.SerializeObject(XXXXX);
                }
                List<Cls_Ent_Actividad> XXXXXX;
                Cls_Ent_Actividad EXXXXXX = new Cls_Ent_Actividad();
                EXXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXXX = new SolicitudRepositorio().ListaActividad(EXXXXXX);
                if (XXXXXX.Count > 0)
                {
                    ViewBag.ListaActividad = JsonConvert.SerializeObject(XXXXXX);

                }
                List<Cls_Ent_Actividad_Otro> XXXXXXX;
                Cls_Ent_Actividad_Otro EXXXXXXX = new Cls_Ent_Actividad_Otro();
                EXXXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXXXX = new SolicitudRepositorio().ListaActividadOtro(EXXXXXXX);
                if (XXXXXXX.Count > 0)
                {
                    ViewBag.ListaActividadOtro = JsonConvert.SerializeObject(XXXXXXX);
                }
                List<Cls_Ent_Renumeracion> XXXXXXXX;
                Cls_Ent_Renumeracion EXXXXXXXX = new Cls_Ent_Renumeracion();
                EXXXXXXXX.ID_SOLICITUD = ID_SOLICITUD;
                XXXXXXXX = new SolicitudRepositorio().ListaPeriodoPagoSolicitud(EXXXXXXXX);
                if (XXXXXXXX.Count > 0)
                {
                    ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(XXXXXXXX);
                }
            }
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            return View(modelo);
        }
        #endregion
    }
}
