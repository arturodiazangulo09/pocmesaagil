using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio;
using APP.MEF.EXTRANET.FAG.PAG.Core;
using APP.MEF.EXTRANET.FAG.PAG.Response;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Utilitario;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Models;
using System.IO;
using System.Configuration;
using System.Text;
using MEF.PROYECTO.Entity.Administracion;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Controllers
{
    public class SolicitudPagoController : BaseController
    {
        //
        // GET: /Coordinador/SolicitudPago/
        StdModel Datos = new StdModel();
        WCF_STD22.hrDto Resultado = new WCF_STD22.hrDto();

        Cls_Ent_Coordinador UsuarioSistemaSesion = new Cls_Ent_Coordinador();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListaSolicitudPagoEntidad(Cls_Ent_Solicitud_Pago entidad)
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
            entidad.ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD;
            List<Cls_Ent_Solicitud_Pago> lista;
            lista = new SolicitudRepositorio().ListaSolicitudPagoEntidad(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //public ActionResult NuevoEditConformidadFag(int ID_CONTRATO, int NUM_MES, long ID_CONFORMIDAD,int ID_SOLICITUD)
        //{
        //    Cls_Ent_Solicitud_Pago modelo = new Cls_Ent_Solicitud_Pago();
        //    Cls_Ent_Solicitud_Personal Ent = new Cls_Ent_Solicitud_Personal();
        //    Ent.ID_SOLICITUD = ID_SOLICITUD;
        //    Ent = new PersonalReposiorio().ListasolicitudPersonal(Ent).First();
        //    ViewBag.ENTIDAD = Ent.ENTIDAD;
        //    ViewBag.CONSULTOR = Ent.APELLIDO_PATERNO + " " + Ent.APELLIDO_MATERNO + " " + Ent.NOMBRES;
        //    ViewBag.NR_CONTRATO = Ent.NUM_CONTRATO + "-" + Ent.ANIO_CONTRATO;
        //    ViewBag.TIPO_PROCESO = Ent.TIPO_PROCESO;
        //    ViewBag.DENOMINACION_PUESTO = Ent.DENOMINACION_PUESTO;
        //    ViewBag.NUM_CONTRATO = Ent.NUM_CONTRATO;
        //    Cls_Ent_Renumeracion Ent_ = new Cls_Ent_Renumeracion();
        //    Ent_.ID_SOLICITUD = ID_SOLICITUD;
        //    Ent_ = new SolicitudPagoRepositorio().ListaPeriodoPagoSolicitud(Ent_).First(A => A.NUM_MES == NUM_MES);
        //    ViewBag.DES_MES = Ent_.DES_MES;
        //    if (ID_CONTRATO == 0)
        //    {
        //        modelo.FLG_PROCESO = "I";
        //    }
        //    else
        //    {
        //        //Cls_Ent_Solicitud lista;
        //        //lista = new SolicitudRepositorio().ListaSolicitudes(modelo).First(A => A.ID_SOLICITUD == ID_SOLICITUD);
        //        //modelo = lista;
        //        modelo.FLG_PROCESO = "U";
        //    }
        //    modelo.ID_CONTRATO = ID_CONTRATO;
        //    modelo.NUM_MES = NUM_MES;
        //    modelo.ID_CONFORMIDAD = ID_CONFORMIDAD;
        //    modelo.ID_SOLICITUD = ID_SOLICITUD;
            
        //    return View(modelo);
        //}
        //public ActionResult NuevoEditConformidadPac(int ID_CONTRATO, int NUM_MES, long ID_CONFORMIDAD, int ID_SOLICITUD)
        //{
        //    Cls_Ent_Solicitud_Pago modelo = new Cls_Ent_Solicitud_Pago();
        //    Cls_Ent_Solicitud_Personal Ent = new Cls_Ent_Solicitud_Personal();
        //    Ent.ID_SOLICITUD = ID_SOLICITUD;
        //    Ent = new PersonalReposiorio().ListasolicitudPersonal(Ent).First();
        //    ViewBag.ENTIDAD = Ent.ENTIDAD;
        //    ViewBag.CONSULTOR = Ent.APELLIDO_PATERNO + " " + Ent.APELLIDO_MATERNO + " " + Ent.NOMBRES;
        //    ViewBag.NR_CONTRATO = Ent.NUM_CONTRATO + "-" + Ent.ANIO_CONTRATO;
        //    ViewBag.TIPO_PROCESO = Ent.TIPO_PROCESO;
        //    ViewBag.DENOMINACION_PUESTO = Ent.DENOMINACION_PUESTO;
        //    ViewBag.NUM_CONTRATO = Ent.NUM_CONTRATO;
        //    Cls_Ent_Renumeracion Ent_ = new Cls_Ent_Renumeracion();
        //    Ent_.ID_SOLICITUD = ID_SOLICITUD;
        //    Ent_ = new SolicitudPagoRepositorio().ListaPeriodoPagoSolicitud(Ent_).First(A => A.NUM_MES == NUM_MES);
        //    ViewBag.DES_MES = Ent_.DES_MES;
        //    if (ID_CONTRATO == 0)
        //    {
        //        modelo.FLG_PROCESO = "I";
        //    }
        //    else
        //    {
        //        //Cls_Ent_Solicitud lista;
        //        //lista = new SolicitudRepositorio().ListaSolicitudes(modelo).First(A => A.ID_SOLICITUD == ID_SOLICITUD);
        //        //modelo = lista;
        //        modelo.FLG_PROCESO = "U";
        //    }
        //    modelo.ID_CONTRATO = ID_CONTRATO;
        //    modelo.NUM_MES = NUM_MES;
        //    modelo.ID_CONFORMIDAD = ID_CONFORMIDAD;
        //    modelo.ID_SOLICITUD = ID_SOLICITUD;

        //    return View(modelo);
        //}
        public ActionResult VerConformidad(int ID_SOLICITUD, int NR_MES, string  TIPO)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.NR_MES = NR_MES;
            modelo.FLG_TIPO = TIPO;
            return View(modelo);

        }
        public ActionResult UpdateArchivoConformidad(int ID_SOLICITUD, int NR_MES)
        {
            Cls_Ent_Solicitud_Pago modelo = new Cls_Ent_Solicitud_Pago();
            //Cls_Ent_Solicitud_Pago Datos=new Cls_Ent_Solicitud_Pago();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.NUM_MES = NR_MES;
            modelo = new SolicitudRepositorio().ListaSolicitudPagoEntidad(modelo).First();
            ViewBag.ENTIDAD = modelo.ENTIDAD;
            ViewBag.CONSULTOR = modelo.CONSULTOR;
            ViewBag.NR_CONTRATO = modelo.CODIGO_CONTRATO;
            ViewBag.DENOMINACION_PUESTO = modelo.DENOMINACION_PUESTO;
            ViewBag.DES_MES = modelo.DES_MES + ' ' + modelo.ANIO;
            ViewBag.IMPORTE_COMPROBANTE = modelo.IMPORTE_COMPROBANTE;
            ViewBag.NUM_ANIO = modelo.ANIO;
            if (modelo.ID_CONFORMIDAD >0)
            {
                modelo.ACCION = "U";
            }
            else
            {
                modelo.ACCION = "I";
            }

            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.NR_MES = NR_MES;
            return View(modelo);
        }
        public ActionResult Mnt_Conformidad_Pago(Cls_Ent_Solicitud_Pago entidad)
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
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud_Pago PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                var httpRequest = Request.Files;
                if (httpRequest.Count > 0)
                {
                  
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSaveConformidad = "";
                    string nomArchivoSaveInforme = "";
                    string nomArchivoSaveRecibo = "";

                    long IdLaserficheConformidad = 0;
                    long IdLaserficheInforme = 0;
                    long IdLaserficheRecibo = 0;

                    if (entidad.ID_ARCHIVO_CONFORMIDAD == 0) {
                        var fileConformidad = Request.Files[0] as HttpPostedFileBase;
                        if (fileConformidad != null)
                        {
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileConformidad)))
                            {
                                string NombreArchivo = Path.Combine(carpeta, fileConformidad.FileName.ToString());
                                fileConformidad.SaveAs(NombreArchivo);
                                string SubcarpetaLF = "SOLICITUD_" + entidad.ID_SOLICITUD + "\\" + "MES_" + entidad.NUM_MES;
                                nomArchivoSaveConformidad = "CONFORMIDAD_COORDINADOR_" + entidad.NUM_MES + "_" + RandomString(5, true) + ".pdf";
                                IdLaserficheConformidad = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "SOLICITUD_PAGO_" + DateTime.Now.Year.ToString(), SubcarpetaLF, nomArchivoSaveConformidad, UsuarioSistemaSesion.USUARIO, entidad.IP_INGRESO);
                                if (IdLaserficheConformidad == 0)
                                {
                                    throw new Exception("Error al registrar la información del archivo.");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_CONFORMIDAD = IdLaserficheConformidad;
                                }
                            }
                        }
                    }
                    if (entidad.ID_ARCHIVO_INFORME == 0)
                    {
                        var fileInforme = Request.Files[1] as HttpPostedFileBase;
                        if (fileInforme != null)
                        {
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileInforme)))
                            {
                                string NombreArchivo = Path.Combine(carpeta, fileInforme.FileName.ToString());
                                fileInforme.SaveAs(NombreArchivo);
                                string SubcarpetaLF = "SOLICITUD_" + entidad.ID_SOLICITUD + "\\" + "MES_" + entidad.NUM_MES;
                                nomArchivoSaveInforme = "INFORME_" + entidad.NUM_MES + "_" + RandomString(5, true) + ".pdf";
                                IdLaserficheInforme = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "SOLICITUD_" + DateTime.Now.Year.ToString(), SubcarpetaLF, nomArchivoSaveInforme, UsuarioSistemaSesion.USUARIO, entidad.IP_INGRESO);
                                if (IdLaserficheInforme == 0)
                                {
                                    throw new Exception("Error al registrar la información del archivo");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_INFORME = IdLaserficheInforme;
                                }
                            }
                        }

                    }
                    if (entidad.ID_ARCHIVO_RECIBO == 0)
                    {
                        var fileRecibo = Request.Files[2] as HttpPostedFileBase;
                        if (fileRecibo != null)
                        {
                            using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(fileRecibo)))
                            {
                                string NombreArchivo = Path.Combine(carpeta, fileRecibo.FileName.ToString());
                                fileRecibo.SaveAs(NombreArchivo);
                                string SubcarpetaLF = "SOLICITUD_" + entidad.ID_SOLICITUD + "\\MES_" + entidad.NUM_MES;
                                nomArchivoSaveRecibo = "RECIBO_" + entidad.NUM_MES + "_" + RandomString(5, true) + ".pdf";
                                IdLaserficheRecibo = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "SOLICITUD_" + DateTime.Now.Year.ToString(), SubcarpetaLF, nomArchivoSaveInforme, UsuarioSistemaSesion.USUARIO, entidad.IP_INGRESO);
                                if (IdLaserficheRecibo == 0)
                                {
                                    throw new Exception("Error al registrar información del archivo");
                                }
                                else
                                {
                                    entidad.ID_ARCHIVO_RECIBO = IdLaserficheRecibo;
                                }
                            }
                        }
                    }
                }

                PreguntaRspta = new SolicitudRepositorio().Mnt_Conformidad_Pago(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                    if (entidad.ACCION == "I")
                    {
                        itemRespuesta.message = "Se registro correctamente la solicitud.";
                    }
                    else
                    {
                        if (entidad.ACCION == "U")
                        {
                            itemRespuesta.message = "Se actualizo correctamente la solicitud.";
                        }
                    }
                    itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                    itemRespuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "</br>" + ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult VerSolicitudPago(int NR_MES, string TIPO ,int ANIO)
        {
            BusquedaModelView modelo = new BusquedaModelView();
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
           
            modelo.ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD;
            modelo.NR_MES = NR_MES;
            modelo.FLG_TIPO = TIPO;
            modelo.ANIO = ANIO;
            return View(modelo);

        }
        public ActionResult Update_Conformidad_Pago(Cls_Ent_Solicitud_Pago entidad)
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
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud_Pago PreguntaRspta = null;
            try
            {
                entidad.ID_ENTIDAD=UsuarioSistemaSesion.ID_ENTIDAD;
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                // PROCESO TRAMITE
                var permite = ConfigurationManager.AppSettings["PermiteTramite"].ToString();
                if (permite=="1")
                {
                    if (entidad.FLG_CON_HR=="2")
                    {
                        entidad.ID_TRAMITE = 1;
                        PreguntaRspta = new SolicitudRepositorio().Update_Conformidad_Pago(entidad);
                        if (!PreguntaRspta.FLG_OK)
                        {
                            itemRespuesta.message = "Ocurrio un error.";
                            itemRespuesta.success = false;
                            itemRespuesta.extra = Log.MensajeLogText();
                        }
                        else
                        {
                            if (entidad.ACCION == "E_UTP" || entidad.ACCION == "E_UTP_I")
                            {
                                if (EnviarEmailAccionCoordinador(FuncionUtil.Mi_Mes(entidad.NUM_MES), entidad.TIPO_PROCESO))
                                {

                                    itemRespuesta.message = "Se realizó la notificación a la oficina de UTP del Ministerio Economía y Finanzas.";
                                }
                                else
                                {
                                    itemRespuesta.message = "Error al realizar la notificación.";
                                }

                            }

                            itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                            itemRespuesta.success = true;
                        }
                    }
                    else
                    {
                        if (entidad.ACCION == "E_UTP")
                        {
                            Cls_Ent_Entidades entidad_desc = new Cls_Ent_Entidades();
                            entidad_desc.ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD;
                            entidad_desc = new CoordinadorRepositorio().ListaEntidades(entidad_desc).First();
                            Datos.NUM_REGISTRO = DateTime.Now.Second.ToString();
                            Datos.NUM_OFICIO = entidad.NR_OFICIO;//"1021-2022/GR-AREQUIPA";
                            Datos.NUM_FOLIOS = entidad.NR_FOLIOS;
                            Datos.ASUNTO = entidad.ASUNTO;//"SOLICITA LISTA DE EXPEDIENTES";
                            if (entidad.TIPO_PROCESO == "F")
                            {
                                Datos.CLASIFICACION = "SOLICITUD DE PAGO FAG PERIODO " + FuncionUtil.Mi_Mes(entidad.NUM_MES);
                            }
                            else
                            {
                                Datos.CLASIFICACION = "SOLICITUD DE PAGO PAC PERIODO " + FuncionUtil.Mi_Mes(entidad.NUM_MES);
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
                                    Cls_Ent_Solicitud_Pago TEMP_ = new Cls_Ent_Solicitud_Pago();
                                    TEMP_.TIPO_PROCESO = entidad.TIPO_PROCESO;
                                    TEMP_.NUM_MES = entidad.NUM_MES;
                                    TEMP_.IDEDOCODIGO = "012";///entidad.IDEDOCODIGO;
                                    TEMP_.ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD;

                                    List<Cls_Ent_Solicitud_Pago> lista_Arch = new SolicitudRepositorio().ListaSolicitudPagoEntidad(TEMP_);
                                    foreach (var item in lista_Arch)
                                    {
                                        anexoDto = new WCF_STD22.anexoDto();
                                        string nom = "";
                                        anexoDto.archivo = UtilLaserfiche.ExportarDocumentoPDF(int.Parse(item.ID_ARCHIVO_CONFORMIDAD.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nom, "");
                                        anexoDto.name = item.NUM_DOCUMENTO + "_CONFORMIDAD.pdf";
                                        anexoDto.length = anexoDto.archivo.Length;
                                        intermediate_list.Add(anexoDto);
                                    }
                                    Datos.ANEXOS = intermediate_list.ToArray();
                                    Datos.REMOTEADDRESS = entidad.IP_PC;
                                    Resultado = xx.Crear_Expediente_Std(Datos);
                                }
                                entidad.ID_TRAMITE = Resultado.iddoc;
                                entidad.NR_TRAMITE = Resultado.numeroSid + '-' + Resultado.numeroAnio;
                                if (Resultado.iddoc > 0)
                                {
                                    PreguntaRspta = new SolicitudRepositorio().Update_Conformidad_Pago(entidad);
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
                                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                                }
                                else
                                {
                                    if (entidad.ACCION == "E_UTP" || entidad.ACCION == "E_UTP_I")
                                    {
                                        if (EnviarEmailAccionCoordinador(FuncionUtil.Mi_Mes(entidad.NUM_MES), entidad.TIPO_PROCESO))
                                        {

                                            itemRespuesta.message = "Se realizó la notificación a la oficina de UTP del Ministerio Economía y Finanzas.";
                                        }
                                        else
                                        {
                                            itemRespuesta.message = "Error al realizar la notificación.";
                                        }

                                    }
                                    else
                                    {
                                            if (entidad.ACCION == "E_UTP_DE")
                                            {
                                                itemRespuesta.message = "Se eliminó la conformidad de la solicitud.";

                                            }
                                    }

                                    itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                                    itemRespuesta.success = true;
                                }
                            }
                            catch (Exception ex)
                            {

                                itemRespuesta.success = false;
                                itemRespuesta.extra = Log.MensajeInterno() + "</br>" + ex.ToString();
                            }

                            // entidad.ID_TRAMITE = DateTime.Now.Second;/////ACA VA EL NUMERO DE TRAMITE
                            // entidad.NR_TRAMITE = "NR-" + DateTime.Now.Second + "-" + DateTime.Now.Year;
                        }
                        else
                        {
                            PreguntaRspta = new SolicitudRepositorio().Update_Conformidad_Pago(entidad);
                            if (!PreguntaRspta.FLG_OK)
                            {
                                itemRespuesta.message = "Ocurrio un error.";
                                itemRespuesta.success = false;
                                itemRespuesta.extra = entidad.DES_ERROR;
                            }
                            else
                            {
                                if (entidad.ACCION == "E_UTP" || entidad.ACCION == "E_UTP_I")
                                {
                                    if (EnviarEmailAccionCoordinador(FuncionUtil.Mi_Mes(entidad.NUM_MES), entidad.TIPO_PROCESO))
                                    {

                                        itemRespuesta.message = "Se realizó la notificación a la oficina de UTP del Ministerio Economía y Finanzas.";
                                    }
                                    else
                                    {
                                        itemRespuesta.message = "Error al realizar la notificación.";
                                    }

                                }
                                else
                                {
                                    if (entidad.ACCION == "E_UTP_DE")
                                    {
                                        itemRespuesta.message = "Se eliminó la conformidad de la solicitud.";

                                    }

                                }

                                itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                                itemRespuesta.success = true;
                            }
                        }
                    }
         
                }
                else
                {
                    PreguntaRspta = new SolicitudRepositorio().Update_Conformidad_Pago(entidad);
                    if (!PreguntaRspta.FLG_OK)
                    {
                        itemRespuesta.message = "Ocurrio un error.";
                        itemRespuesta.success = false;
                        itemRespuesta.extra = Log.MensajeLogText();
                    }
                    else
                    {
                        if (entidad.ACCION == "E_UTP" || entidad.ACCION == "E_UTP_I")
                        {
                            if (EnviarEmailAccionCoordinador(FuncionUtil.Mi_Mes(entidad.NUM_MES), entidad.TIPO_PROCESO))
                            {

                                itemRespuesta.message = "Se realizó la notificación a la oficina de UTP del Ministerio Economía y Finanzas.";
                            }
                            else
                            {
                                itemRespuesta.message = "Error al realizar la notificación.";
                            }

                        }
                        else
                        {
                            if (entidad.ACCION == "E_UTP_DE")
                            {
                                itemRespuesta.message = "Se eliminó la conformidad de la solicitud.";

                            }

                        }

                        itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                        itemRespuesta.success = true;
                    }
                }
             
                // FIN PROCESO TRAMITE 
         
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "</br>" + ex.Message.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailAccionCoordinador(string periodo,string tipo)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarUTP_Conformidad.html"))
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
            mensaje = mensaje.Replace("{2}", periodo);
            mensaje = mensaje.Replace("{3}", tipo == "F" ? "FAG LEY N° 25650" : "PAC LEY N° 29806");
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
        public ActionResult ObservacionProfesional(int ID_SOLICITUD, int ID_CONFORMIDAD, int NR_MES)
        {

            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.ID_CONFORMIDAD = ID_CONFORMIDAD;
            modelo.NR_MES = NR_MES;
            return View(modelo);
        }
        public ActionResult UpdateReevaluacionPago(Cls_Ent_Solicitud_Pago entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud_Pago PreguntaRspta = null;
            try
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
    

                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                entidad.CONSULTOR= UsuarioSistemaSesion.APELLIDO_PATERNO + ' '  + UsuarioSistemaSesion.APELLIDO_MATERNO +' '+ UsuarioSistemaSesion.NOMBRES; ;
                PreguntaRspta = new SolicitudRepositorio().UpdateReevaluacionPago(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {

                    Cls_Ent_Solicitud ent_s = new Cls_Ent_Solicitud();
                    ent_s.ID_SOLICITUD = entidad.ID_SOLICITUD;
                    ent_s = new SolicitudRepositorio().ListaSolicitudes(ent_s).First();
                    ent_s.DESCRIPCION = FuncionUtil.Mi_Mes(entidad.NUM_MES);
                    ent_s.ACCION = entidad.DESCRIPCION;
                    if (EnviarEmailCoordinadorObservadoPeriodo(ent_s))
                    {
                        itemRespuesta.message = "Se realizo la notificación al coordinador.";
                    }
                    else
                    {
                        itemRespuesta.message = "Error al realizar la notificación al coordinador, vuelva a intentarlo en unos minutos.";
                    }

                    itemRespuesta.extra = entidad.ID_SOLICITUD.ToString();
                    itemRespuesta.success = true;
                }

            }
            catch (Exception ex)
            {
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "</br>" + ex.ToString();
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        private bool EnviarEmailCoordinadorObservadoPeriodo(Cls_Ent_Solicitud entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarObservacionPagoPersonal.html"))
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
            mensaje = mensaje.Replace("{0}", entidad.APELLIDO_PAT_CONSULTOR + " " + entidad.APELLIDO_MAT_CONSULTOR + " " + entidad.NOMBRES_CONSULTOR);
           mensaje = mensaje.Replace("{1}", entidad.NUM_DOCUMENTO_CONSULTOR);
            mensaje = mensaje.Replace("{2}", entidad.DESCRIPCION);
            mensaje = mensaje.Replace("{3}", UsuarioSistemaSesion.DESC_ENTIDAD);
            mensaje = mensaje.Replace("{4}", entidad.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{5}", entidad.ACCION);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.CORREO_CONSULTOR;
            bool strRet = EnviarMail.SendMailMessage(ref strMsgError, destinatarios, ConfigurationManager.AppSettings["CorreoCopia"], "", titulo, mensaje, "", ConfigurationManager.AppSettings["Usermail"]);

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
        public ActionResult Ver_Integrar_STD(int NUM_MES, string TIPO_PROCESO)
        {
            Cls_Ent_Solicitud_Pago modelo = new Cls_Ent_Solicitud_Pago();
            modelo.IDEDOCODIGO = "013";
            modelo.ACCION = "E_UTP";
            modelo.NUM_MES = NUM_MES;
            modelo.TIPO_PROCESO = TIPO_PROCESO;
            return View(modelo);
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
