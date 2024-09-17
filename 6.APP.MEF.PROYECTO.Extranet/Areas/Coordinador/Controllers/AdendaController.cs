using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP.MEF.EXTRANET.FAG.PAG.Core;
using APP.MEF.EXTRANET.FAG.PAG.Response;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Administracion;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using MEF.PROYECTO.Utilitario;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Models;
using System.IO;
using System.Text;
using System.Configuration;
using static MEF.PROYECTO.Utilitario.Constants.Mensajes;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Controllers
{
    public class AdendaController : BaseController
    {
        //
        // GET: /Coordinador/Adenda/
        StdModel Datos = new StdModel();
        WCF_STD22.hrDto Resultado = new WCF_STD22.hrDto();
        Cls_Ent_Coordinador UsuarioSistemaSesion = new Cls_Ent_Coordinador();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NuevoEditAdendaSolicitud(int ID_CONTRATO_DET)
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
            Cls_Ent_Adenda modelo = new Cls_Ent_Adenda();
            List<Cls_Ent_Entidades> Lista_Entidad = null;
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            Lista_Entidad = new SolicitudRepositorio().ListaEntidades(xx).FindAll(A => A.ID_ENTIDAD == UsuarioSistemaSesion.ID_ENTIDAD);
            ViewBag.DcboEntidades = Lista_Entidad;
            ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(new List<Cls_Ent_Renumeracion>());
            if (ID_CONTRATO_DET==0)
            {
                modelo.ACCION = "I";
            }
            else
            {
                modelo.ID_CONTRATO_DET = ID_CONTRATO_DET;
                modelo = new AdendaRepositorio().ListaDetalleContratos(modelo).First();
                modelo.ACCION = "U";
                List<Cls_Ent_Renumeracion> XXXXXXXX;
                Cls_Ent_Renumeracion EXXXXXXXX = new Cls_Ent_Renumeracion();
                EXXXXXXXX.ID_SOLICITUD = 0;
                XXXXXXXX = new SolicitudRepositorio().ListaPeriodoPagoSolicitud(EXXXXXXXX).FindAll(A=> A.ID_CONTRATO_ADENDA == ID_CONTRATO_DET);
                if (XXXXXXXX.Count > 0)
                {
                    ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(XXXXXXXX);
                }
            }
            return View(modelo);
        }
        public JsonResult ListarContratoCbo(Cls_Ent_Contrato entidad)
        {
            IList<Cls_Ent_Contrato> cboProv = new AdendaRepositorio().ListaContratos(entidad);
            cboProv.Insert(0, new Cls_Ent_Contrato() { ID_CONTRATO = 0, CODIGO_CONTRATO = "--Seleccione--" });
            //ViewBag.Prov = cboProv;
            return Json(cboProv, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListaContratoDetalle(int ID_CONTRATO)
        {
            Cls_Ent_Contrato lista = new Cls_Ent_Contrato();
            lista.ID_CONTRATO = ID_CONTRATO;
            lista = new AdendaRepositorio().ListaContratos(lista).First();
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        public ActionResult MantenimientoSolicitudAdenda(Cls_Ent_Adenda entidad)
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
            Cls_Ent_Adenda PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                entidad.ID_COORDINADOR= UsuarioSistemaSesion.ID_COORDINADOR;
                var httpRequest = Request.Files;
                if (httpRequest.Count > 0)
                {
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSave = "";
                    var files = Request.Files[0] as HttpPostedFileBase;
                    long IdLaserfiche = 0;
                    using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                    {
                        string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                        files.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "";
                        if (entidad.TIPO_PROCESO == "F")
                        {
                             SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        }
                        else
                        {
                             SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        }
                      
                        nomArchivoSave = "DOCUMENTO_SUSTENTO_ADENDA_" + RandomString(5, true) + ".pdf";
                        IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                        entidad.ID_ARCHIVO_SUSTENTO = int.Parse(IdLaserfiche.ToString());
                    }
                }
                if (entidad.ID_ARCHIVO_SUSTENTO > 0)
                {
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    if (entidad.TIPO != "D")
                    {
                        var pg = jsSerializer.Deserialize<List<Cls_Ent_Renumeracion>>(Request.Form["listaRenumeracion"]);
                        entidad.listaRenumeracion = pg;
                    }

                    PreguntaRspta = new AdendaRepositorio().MantenimientoSolicitudAdenda(entidad);
                }
                else
                {
                    throw new Exception("Error al registrar la información del archivo.");
                }
    
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                    if (entidad.TIPO == "I")
                    {
                        itemRespuesta.message = "Se registro correctamente la solicitud de adenda.";
                    }
                    else
                    {
                        if (entidad.TIPO == "U")
                        {
                            itemRespuesta.message = "Se actualizo correctamente la solicitud de adenda.";
                        }
                        else
                        {
                            itemRespuesta.message = "Se elimino correctamente la solicitud de adenda.";
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
                itemRespuesta.extra = Log.MensajeInterno() +"<br/>"+ ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ListaDetalleContratos(Cls_Ent_Adenda entidad)
        {
            List<Cls_Ent_Adenda> lista;
            lista = new AdendaRepositorio().ListaDetalleContratos(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult ListaDetalleAdendas(Cls_Ent_Adenda entidad)
        {
            List<Cls_Ent_Adenda> lista;
            lista = new AdendaRepositorio().ListaDetalleAdendas(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult Ver_Adenda(int ID, string FLG_TIPO)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            Cls_Ent_Adenda Datos = new Cls_Ent_Adenda();
            Datos.ID_CONTRATO_DET = ID;
            Datos = new AdendaRepositorio().ListaDetalleContratos(Datos).First();

            modelo.ID = ID;
            modelo.FLG_TIPO = FLG_TIPO;
            modelo.ID_ARCHIVO = Datos.ID_ARCHIVO_CONTRATO;
            modelo.ID_SOLICITUD = Datos.ID_SOLICITUD;
           
            if (FLG_TIPO == "P")
            {
                modelo.ACCION = "ANEXO 1";
            }
            else
            {
                modelo.ACCION = "ANEXO 8";
            }

            return View(modelo);
        }

        public ActionResult Ver_AdendaMasiva(string jsondata, string FLG_TIPO)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            Cls_Ent_Adenda Datos = new Cls_Ent_Adenda();
            
            List<Cls_Ent_Adenda_data> adendas;
            adendas = JsonConvert.DeserializeObject<List<Cls_Ent_Adenda_data>>(jsondata);

            foreach (var adenda in adendas)
            {
                Console.WriteLine(adenda);
                Datos.ID_CONTRATO_DET = adenda.ID_CONTRATO_DET;

                Datos = new AdendaRepositorio().ListaDetalleContratos(Datos).First();

                modelo.ID = adenda.ID_CONTRATO_DET;
                modelo.FLG_TIPO = FLG_TIPO;
                modelo.ID_ARCHIVO = Datos.ID_ARCHIVO_CONTRATO;
                modelo.ID_SOLICITUD = Datos.ID_SOLICITUD;
            }


            if (FLG_TIPO == "P")
            {
                modelo.ACCION = "ANEXO 1";
            }
            else
            {
                modelo.ACCION = "ANEXO 8";
            }

            return View(modelo);
        }
        public ActionResult AdendaFirmadaFrm(int ID_CONTRATO_DET, string TIPO)
        {
            BusquedaModelView modelo = new BusquedaModelView();
            modelo.ID = ID_CONTRATO_DET;
            modelo.FLG_TIPO = TIPO;
            return View(modelo);
        }
        //public ActionResult AdendaFirmada(BusquedaModelView entidad)
        //{
        //    if (Session["Usuario"] != null)
        //    {
        //        UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
        //    }
        //    else
        //    {
        //        var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
        //        var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
        //        if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
        //        {
        //            UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
        //        }
        //    }

        //    var httpRequest = Request.Files;
        //    int IdLaserfiche = 0;
        //    string nomArchivoSave = "";
        //    if(httpRequest.Count > 0)
        //    {
        //        var files = Request.Files[0] as HttpPostedFileBase;
        //        var carpeta = Server.MapPath("~/ArchivoTemp");
        //        using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
        //        {
        //            string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
        //            files.SaveAs(NombreArchivo);
        //            string SubcarpetaLF = "";
        //            if (entidad.TIPO_PROCESO == "F")
        //            {
        //                SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
        //            }
        //            else
        //            {
        //                SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
        //            }
        //            nomArchivoSave = "ADENDA_FIRMADO_" + RandomString(5, true) + ".pdf";
        //            IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, UsuarioSistemaSesion.USU_INGRESO, UsuarioSistemaSesion.IP_INGRESO);

        //            entidad.ID_ARCHIVO = IdLaserfiche;
        //        }
        //    }

        //}
        public ActionResult UpdArchivoAdenda(Cls_Ent_Adenda entidad)
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
            Cls_Ent_Adenda PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                var httpRequest = Request.Files;
                if (httpRequest.Count > 0)
                {
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSave = "";
                    var files = Request.Files[0] as HttpPostedFileBase;
                    long IdLaserfiche = 0;
                    using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                    {
                        string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                        files.SaveAs(NombreArchivo);
                        string SubcarpetaLF = "";
                        if (entidad.TIPO_PROCESO == "F")
                        {
                            SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        }
                        else
                        {
                            SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + entidad.ID_SOLICITUD;
                        }

                        nomArchivoSave = "DOCUMENTO_FIRMADO_ADENDA_" + RandomString(5, true) + ".pdf";
                        IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, nomArchivoSave, UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                        entidad.ID_ARCHIVO = int.Parse(IdLaserfiche.ToString());
                    }
                    //var files = Request.Files[0] as HttpPostedFileBase;
                    //var tempFile = new byte[files.ContentLength];
                    //files.InputStream.Read(tempFile, 0, files.ContentLength);
                    //xx.ARCHIVO = tempFile;
                    //xx.NOMBRE_ARCHIVO = "DOCUMENTO_FIRMADO_ADENDA_" + entidad.ID_SOLICITUD + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + ".pdf";
                    //xx.DESCRIPCION = "DOCUMENTO FIRMADO PARA LA GENERACIÓN DE ADENDA - SOLICITUD N° " + entidad.ID_SOLICITUD + " CONTRATO N° " + entidad.ID_CONTRATO;
                    //xx.USU_INGRESO = entidad.USU_INGRESO;
                    //xx.IP_INGRESO = entidad.IP_INGRESO;
                    //ArchivoRspta = new CoordinadorRepositorio().InsertAchivoSustento(xx);
                    //if (ArchivoRspta.FLG_OK)
                    //{
                    //    entidad.ID_ARCHIVO = ArchivoRspta.ID_ARCHIVO;
                    //}
                    //else
                    //{
                    //    entidad.ID_ARCHIVO = 0;
                    //}
                }
                if (entidad.ID_ARCHIVO > 0)
                {
                    PreguntaRspta = new AdendaRepositorio().UpdArchivoAdenda(entidad);
                }
                else
                {
                    throw new Exception("Error al registrar la información del archivo.");
                }
    
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                        itemRespuesta.message = "Se registro correctamente el archivo firmado.";
                    itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                    itemRespuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "<br>" +  ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult Ver_Integrar_STD(int ID_TRAMITE, int ID_CONTRATO_DET)
        {
            Cls_Ent_Adenda modelo = new Cls_Ent_Adenda();
            modelo.ID_TRAMITE = ID_TRAMITE;
            modelo.ID_CONTRATO_DET = ID_CONTRATO_DET;
            return View(modelo);
        }

        public ActionResult Ver_Integrar_STDM(string jsondata)
        {

            List<Cls_Ent_Adenda_data> modelo;
            modelo = JsonConvert.DeserializeObject<List<Cls_Ent_Adenda_data>>(jsondata);
            var jsonResult = Json(modelo, JsonRequestBehavior.AllowGet);

            //ViewBag.ListaAdendas = modelo;
            ViewBag.ListaAdendas = jsonResult;

            return View();
        }
        public ActionResult UpdEstadoAdenda(Cls_Ent_Adenda entidad)
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
            Cls_Ent_Adenda PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                // PROCESO TRAMITE
                if (entidad.ID_TRAMITE == 0)
                {
                    var permite = ConfigurationManager.AppSettings["PermiteTramite"].ToString();
                    if (permite == "1")
                    {
                        if (entidad.FLG_CON_HR == "2" || entidad.FLG_CON_HR == "3")
                        {
                            entidad.ID_TRAMITE = 1;
                            PreguntaRspta = new AdendaRepositorio().UpdEstadoAdenda(entidad);
                            if (!PreguntaRspta.FLG_OK)
                            {
                                itemRespuesta.message = "Ocurrio un error.";
                                itemRespuesta.success = false;
                                itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
                            }
                            else
                            {
                                entidad = new AdendaRepositorio().ListaDetalleContratos(entidad).First();
                                if (EnviarEmailUtpAdenda(entidad))
                                {
                                    itemRespuesta.message = "Se realizo la notificación a la oficina de UTP.";
                                }
                                else
                                {
                                    itemRespuesta.message = "Error al realizar la notificación al participante, vuelva a intentarlo en unos minutos.";
                                }

                                itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                                itemRespuesta.success = true;
                            }
                        }
                        else
                        {
                            Cls_Ent_Entidades entidad_desc = new Cls_Ent_Entidades();
                            entidad_desc.ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD;
                            entidad_desc = new CoordinadorRepositorio().ListaEntidades(entidad_desc).First();
                            Datos.NUM_REGISTRO = entidad.ID_CONTRATO_DET.ToString();
                            Datos.NUM_OFICIO = entidad.NR_OFICIO;//"1021-2022/GR-AREQUIPA";
                            Datos.NUM_FOLIOS = entidad.NR_FOLIOS;
                            Datos.ASUNTO = entidad.ASUNTO;//"SOLICITA LISTA DE EXPEDIENTES";
                            Datos.CLASIFICACION = "SOLICITUD DE ADENDA";
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
                                    Cls_Ent_Adenda TEMP_ = new Cls_Ent_Adenda();
                                    TEMP_.ID_CONTRATO_DET = entidad.ID_CONTRATO_DET;
                                    List<Cls_Ent_Adenda> lista_Arch = new AdendaRepositorio().ListaDetalleContratos(TEMP_); ;
                                    foreach (var item in lista_Arch)
                                    {
                                        anexoDto = new WCF_STD22.anexoDto();
                                        if (item.ID_ARCHIVO_CONTRATO > 0)
                                        {
                                            string nom = "";
                                            anexoDto.archivo = UtilLaserfiche.ExportarDocumentoPDF(int.Parse(item.ID_ARCHIVO_CONTRATO.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nom, "");
                                            anexoDto.name = "CONTRATO_FIRMADO.pdf";
                                        }
                                        else
                                        {
                                            string nom = "";
                                            anexoDto.archivo = UtilLaserfiche.ExportarDocumentoPDF(int.Parse(item.ID_ARCHIVO_SUSTENTO.ToString()), ConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), ConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(), "", "", ref nom, "");
                                            anexoDto.name = "DOCUMENTO_SUSTENTO.pdf";
                                        }
                                        anexoDto.length = anexoDto.archivo.Length;
                                        intermediate_list.Add(anexoDto);
                                    }
                                    Datos.ANEXOS = intermediate_list.ToArray();
                                    Datos.REMOTEADDRESS = entidad.IP_INGRESO;
                                    Resultado = xx.Crear_Expediente_Std(Datos);
                                }

                                if (Resultado.iddoc > 0)
                                {
                                    entidad.ID_TRAMITE = int.Parse(Resultado.iddoc.ToString());
                                    entidad.NR_TRAMITE = Resultado.numeroSid + '-' + Resultado.numeroAnio;
                                    itemRespuesta.extra2 = "1";
                                    itemRespuesta.message2 = "Se genero el tramite " + entidad.NR_TRAMITE;
                                }
                                else
                                {
                                    throw new Exception("No se pudo generar la Hoja de Ruta, intentar en unos minutos.");
                                }
                            }
                            catch (Exception ex)
                            {

                                throw new Exception(ex.ToString());
                            }
                        }
               
                    }
                    else
                    {
                        entidad.ID_TRAMITE = 0;
                        entidad.NR_TRAMITE = "";
                    }
                    //entidad.ID_TRAMITE = DateTime.Now.Second;/////ACA VA EL NUMERO DE TRAMITE
                    //entidad.NR_TRAMITE = "NR-" + DateTime.Now.Second + "-" + DateTime.Now.Year;
                    //itemRespuesta.extra2 = "1";
                    //itemRespuesta.message2 = "Se genero el tramite " + entidad.NR_TRAMITE;
                }
                // FIN PROCESO TRAMITE 
                PreguntaRspta = new AdendaRepositorio().UpdEstadoAdenda(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
                }
                else
                {
                    entidad = new AdendaRepositorio().ListaDetalleContratos(entidad).First();
                    if (EnviarEmailUtpAdenda(entidad))
                    {
                        itemRespuesta.message = "Se realizo la notificación a la oficina de UTP.";
                    }
                    else
                    {
                        itemRespuesta.message = "Error al realizar la notificación al participante, vuelva a intentarlo en unos minutos.";
                    }

                    itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                    itemRespuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "<br/>"+  ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
      


        private bool EnviarEmailUtpAdenda(Cls_Ent_Adenda entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarSolicitudAdenda.html"))
            {
                mensaje = sr.ReadToEnd();
            }
            mensaje = mensaje.Replace("{0}", entidad.COORDINADOR);
            mensaje = mensaje.Replace("{1}", entidad.DESC_ENTIDAD);
            mensaje = mensaje.Replace("{2}", entidad.CODIGO_CONTRATO);
            mensaje = mensaje.Replace("{3}", entidad.NOMBRE_PUESTO);
            mensaje = mensaje.Replace("{4}", entidad.CODIGO_ADENDA);

            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = ConfigurationManager.AppSettings["CorreoCopia"];
            bool strRet = EnviarMail.SendMailMessage(ref strMsgError, destinatarios,"", "", titulo, mensaje, "", ConfigurationManager.AppSettings["Usermail"]);

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
        public ActionResult VerObservacionADENDA(int ID_CONTRATO_DET)
        {
            Cls_Ent_Reevaluacion modelo = new Cls_Ent_Reevaluacion();
            modelo.ID_CONTRATO_DET = ID_CONTRATO_DET;
            return View(modelo);
        }
        public ActionResult ListaReevaluacionAdenda(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista;
            lista = new AdendaRepositorio().ListaReevaluacionAdenda(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
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
        public ActionResult UpdEstadoAdenda_Delete(Cls_Ent_Adenda entidad)
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
            Cls_Ent_Adenda PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                // PROCESO TRAMITE
                PreguntaRspta = new AdendaRepositorio().UpdEstadoAdenda(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
                }
                else
                {
                        itemRespuesta.message = "Se realizo la eliminación de la adenta seleccionada.";
                    itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                    itemRespuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "<br/>" + ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
    }
}
