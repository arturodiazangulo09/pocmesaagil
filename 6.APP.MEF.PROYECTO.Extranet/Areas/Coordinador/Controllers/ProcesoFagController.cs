using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Administracion;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using APP.MEF.EXTRANET.FAG.PAG.Core;
using APP.MEF.EXTRANET.FAG.PAG.Response;
using MEF.PROYECTO.Utilitario;
using MEF.PROYECTO.Entity.Coordinador;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Models;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Reportes;
using System.Net.Mime;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Controllers
{
    public class ProcesoFagController : BaseController
    {
        //
        // GET: /Coordinador/ProcesoFag/
        Cls_Ent_Coordinador UsuarioSistemaSesion = new Cls_Ent_Coordinador();
        public ActionResult Index()
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
            Cls_Ent_Entidades Modelo = new Cls_Ent_Entidades();
            Modelo = new CoordinadorRepositorio().ListaEntidades(Modelo).First(A => A.ID_ENTIDAD== UsuarioSistemaSesion.ID_ENTIDAD);
            using (UbigeoRepositorio repositorio = new UbigeoRepositorio())
            {
                Modelo.ListaDpto = repositorio.ListaDepartamento().Select(x => new SelectListItem()
                {
                    Text = x.CNOMDEPARTAMENTO.Trim(),
                    Value = x.CCODDEPARTAMENTO.ToString()
                }).ToList();
                Modelo.ListaDpto.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }
            return View(Modelo);
        }
        public ActionResult NuevoCalificarPro(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
            modelo.TIPO_PROCESO = "F";
            modelo = new SolicitudRepositorio().ListaSolicitudes(modelo).First(A => A.ID_SOLICITUD == ID_SOLICITUD);
            modelo.listaRequisitos = new SolicitudRepositorio().ListaRequisitos(ID_SOLICITUD);
            modelo.ListaCumpleRequisitos = new List<SelectListItem>();
            modelo.ListaCumpleRequisitos.Add(new SelectListItem { Value = "", Text = "--------------------" });
            modelo.ListaCumpleRequisitos.Add(new SelectListItem { Value = "0", Text = "NO CUMPLE" });
            modelo.ListaCumpleRequisitos.Add(new SelectListItem { Value = "1", Text = "CUMPLE" });
            return View(modelo);
        }
        public async Task<ActionResult> Ver_Contrato(int ID_PERSONAL, int ID_SOLICITUD , string FLG_TIPO)
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
            Byte[] bytes = null;
            try
            {
                bytes = new ReporteRepositorio().GenerarContratoWORD(ID_SOLICITUD, ID_PERSONAL, FLG_TIPO);
                using (var stream = new System.IO.MemoryStream(bytes))
                {
                    byte[] buffer = new byte[stream.Length];
                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = Guid.NewGuid() + ".docx",
                        Inline = false,
                    };
                    Response.AppendHeader("Content-Disposition", cd.ToString());
                    return await Task.FromResult(File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document"));
                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), "CEU.ProcesoFagController.Ver_Contrato");
            }
            return await FormatoError();

            //BusquedaModelView modelo = new BusquedaModelView();
            //modelo.ID_SOLICITUD = ID_SOLICITUD;
            //modelo.ID_PERSONAL = ID_PERSONAL;
            //    if (FLG_TIPO == "CONTRATO_FAG")
            //{
            //    modelo.ACCION = "FORMATO DE CONTRATO LEY FAG";
            //}
            //else {
            //    modelo.ACCION = "FORMATO DE CONTRATO LEY PAC";
            //}

            //modelo.FLG_TIPO = FLG_TIPO;
            //return View(modelo);
        }
        public ActionResult UpdateDatosEntidad(Cls_Ent_Entidades entidad)
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
            Cls_Ent_Entidades PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();

                PreguntaRspta = new CoordinadorRepositorio().UpdateDatosEntidad(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                    itemRespuesta.message = "Los datos de la entidad se actualizaron correctamente.";
                    itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                    itemRespuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "<br/>"+ ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult Ver_Integrar_STD(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo = new SolicitudRepositorio().ListaSolicitudes(modelo).First();
            return View(modelo);
        }

        public ActionResult WizNotificar(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo = new SolicitudRepositorio().ListaSolicitudes(modelo).First();
            return View(modelo);
        }
        

        /*  public ActionResult WizardNuevaSolicitud(int ID_SOLICITUD)

          {
              int ID_SOLICITUD = 0;
              Cls_Ent_Solicitud model = new Cls_Ent_Solicitud();
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


              if (ID_SOLICITUD == 0)
              {
                  var Lista_Entidad = new SolicitudRepositorio().ListaEntidades(new Cls_Ent_Entidades { ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD });
                  ViewBag.DcboEntidades = Lista_Entidad;
                  model.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString(); //
                  var ListaGrado = new SolicitudRepositorio().ListaNivelGrado();
                  ViewBag.DcboGrado = ListaGrado;
                  var ListaTipoExp = new SolicitudRepositorio().ListaTipoExperiencia();
                  ViewBag.DcboTipoExp = ListaTipoExp;
                  var ListaTipoSecExp = new SolicitudRepositorio().ListaTipoSectorExperiencia();
                  ViewBag.DcboTipoSecExp = ListaTipoSecExp;

                  model.FLG_PROCESO = "I";
                  model.TIPO_PROCESO = "F";
                  //ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(new List<Cls_Ent_Renumeracion>());

                  //WizNuevaSolicitudViewModel model = new WizNuevaSolicitudViewModel();
                  //model.Entidades = new SolicitudRepositorio().ListaEntidades(new Cls_Ent_Entidades { ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD });
                  //model.GradosAcademicos = new SolicitudRepositorio().ListaNivelGrado();
                  //model.ExperienciaTipos = new SolicitudRepositorio().ListaTipoExperiencia();
                  //model.SectorExperienciaTipos = new SolicitudRepositorio().ListaTipoSectorExperiencia();
                  //model.Solicitud.ID = model.Entidades.ElementAt(0).ID_PADRE_ENTIDAD.ToString();
              }
              else {
                  model.ID_SOLICITUD = ID_SOLICITUD;
                  model = new SolicitudRepositorio().ListaSolicitudes(model).First();
              }

              model.FLG_PROCESO = "I";
              model.TIPO_PROCESO = "F";

              return View(model);
          }*/
        public ActionResult WizardNuevaSolicitud()
        {
            int ID_SOLICITUD = 0;
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
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

            //int contratacionPendiente = SolicitudRepositorio.GetSolicitudPendiente();

            var Lista_Entidad = new SolicitudRepositorio().ListaEntidades(new Cls_Ent_Entidades { ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD });
            ViewBag.DcboEntidades = Lista_Entidad;
            modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString(); //
            var ListaGrado = new SolicitudRepositorio().ListaNivelGrado();
            ViewBag.DcboGrado = ListaGrado;
            var ListaTipoExp = new SolicitudRepositorio().ListaTipoExperiencia();
            ViewBag.DcboTipoExp = ListaTipoExp;
            var ListaTipoSecExp = new SolicitudRepositorio().ListaTipoSectorExperiencia();
            ViewBag.DcboTipoSecExp = ListaTipoSecExp;
            if (ID_SOLICITUD == 0)
            {
                modelo.FLG_PROCESO = "I";
                modelo.TIPO_PROCESO = "F";
                ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(new List<Cls_Ent_Renumeracion>());
            }
            else
            {
                var lista = new SolicitudRepositorio().ListaSolicitudes(new Cls_Ent_Solicitud { ID_SOLICITUD = ID_SOLICITUD }).First();
                modelo = lista;
                modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString();
                var XXXXXXXX = new SolicitudRepositorio().ListaPeriodoPagoSolicitud(new Cls_Ent_Renumeracion { ID_SOLICITUD = ID_SOLICITUD });
                if (XXXXXXXX.Count > 0)
                {
                    ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(XXXXXXXX);
                }
                modelo.TIPO_PROCESO = "F";
                modelo.FLG_PROCESO = "U";
            }
            modelo.FLG_VERSION = "2";
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            return View(modelo);
        }
        #region SOLICITUDES V.2
        public ActionResult NuevoEditaSolicitud_v2(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
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
            var Lista_Entidad = new SolicitudRepositorio().ListaEntidades(new Cls_Ent_Entidades { ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD });
            ViewBag.DcboEntidades = Lista_Entidad;
            modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString(); //
            var ListaGrado = new SolicitudRepositorio().ListaNivelGrado();
            ViewBag.DcboGrado = ListaGrado;
            var ListaTipoExp = new SolicitudRepositorio().ListaTipoExperiencia();
            ViewBag.DcboTipoExp = ListaTipoExp;
            var ListaTipoSecExp = new SolicitudRepositorio().ListaTipoSectorExperiencia();
            ViewBag.DcboTipoSecExp = ListaTipoSecExp;
            if (ID_SOLICITUD == 0)
            {
                modelo.FLG_PROCESO = "I";
                modelo.TIPO_PROCESO = "F";
                ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(new List<Cls_Ent_Renumeracion>());
            }
            else
            {
                var lista = new SolicitudRepositorio().ListaSolicitudes(new Cls_Ent_Solicitud { ID_SOLICITUD = ID_SOLICITUD }).First();
                modelo = lista;
                modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString();
                var XXXXXXXX = new SolicitudRepositorio().ListaPeriodoPagoSolicitud(new Cls_Ent_Renumeracion { ID_SOLICITUD = ID_SOLICITUD });
                if (XXXXXXXX.Count > 0)
                {
                    ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(XXXXXXXX);
                }
                modelo.TIPO_PROCESO = "F";
                modelo.FLG_PROCESO = "U";
            }
            modelo.FLG_VERSION = "2";
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            return View(modelo);
        }
        /////FORMACION ACADEMICA
        public ActionResult GenerarFormacionHTML(Cls_Ent_Academico entidad)
        {
            var respuesta = new ResponseEntity();
            try
            {
                respuesta.extra = FilaTablaFormacion(entidad);
                if (respuesta.extra == "error")
                {
                    respuesta.success = false;
                    respuesta.message = "Error al generar registrar la formación académica";
                }
                else
                {
                    respuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.message = "Error al generar registrar la formación académica";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.GenerarFormacionHTML");
            }

            return this.Respuesta(respuesta);

        }
        private string FilaTablaFormacion(Cls_Ent_Academico modelo)
        {
            string html = "";
            try
            {
                var lista = new List<Cls_Ent_Academico>();
                var det = new SolicitudRepositorio().ListaAcademica(modelo);
                if (modelo.Items != null)
                {
                    lista = modelo.Items.Count == 0 ? (det.Count == 0 ? new List<Cls_Ent_Academico>() : det) : modelo.Items;
                }
                else
                {
                    if (modelo.ID_ACADEMICA == 1)
                    {
                        lista = det.Count == 0 ? new List<Cls_Ent_Academico>() : det;
                    }
             
                }
                if (modelo.ID_ACADEMICA == 0)
                {
                    var entity_ = new Cls_Ent_Academico
                    {
                        ID_ACADEMICA = modelo.ID_ACADEMICA,
                        ID_SOLICITUD = modelo.ID_SOLICITUD,
                        DESC_ACADEMICA = modelo.DESC_ACADEMICA,
                        ID_NIVEL_GRADO = modelo.ID_NIVEL_GRADO,
                        NOMBRE_NIVEL = modelo.NOMBRE_NIVEL,
                    };
                    lista.Add(entity_);
                }
                int num = 0;
                if (lista.Count() > 0)
                {

                    foreach (var item in lista)
                    {
                        num += 1;
                        html += GrupoRegistroFormacion(item.ID_ACADEMICA, item.ID_SOLICITUD, item.DESC_ACADEMICA, item.ID_NIVEL_GRADO, item.NOMBRE_NIVEL, num);
                    }
                }
            }
            catch (Exception ex)
            {
                html = "error";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.FilaTablaFormacion");

            }

            return html;

        }
        private string GrupoRegistroFormacion(long ID_ACADEMICA, long ID_SOLICITUD, string DESC_ACADEMICA, int ID_NIVEL_GRADO, string NOMBRE_NIVEL, int num)
        {

            string html = "";
            string remove = deleteFila("deleform");
            html += "<tr>";
            html += "<td class='tdID_ACADEMICA' style='display:none;'>" + ID_ACADEMICA + "</td>";
            html += "<td class='tdID_SOLICITUD' style='display:none;'>" + ID_SOLICITUD + "</td>";
            html += "<td class='tdID_NIVEL_GRADO' style='display:none;'>" + ID_NIVEL_GRADO + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + num + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + remove + "</td>";
            html += "<td>";
            html += "<label style='font-weight:bold' class='form-control tdNOMBRE_NIVEL'>" + NOMBRE_NIVEL + "</label>";
            html += "</td>";
            html += "<td>";
            html += "<input type='text' style='font-weight:bold' class='form-control tdDESC_ACADEMICA' value='" + DESC_ACADEMICA + "'" + ">";
            html += "</td>";
            html += "</tr>";
            return html;
        }
        private string deleteFila(string text)
        {
            string html = "<a  href ='javascript:void(0);' style='color:#EB5767' title='Eliminar Archivo'><i class='far fa-trash-alt " + text + "'></i></a>";
            return html;
        }
        /////FIN FORMACION ACADEMICA
        /////CURSO
        public ActionResult GenerarCursoHTML(Cls_Ent_Curso entidad)
        {
            var respuesta = new ResponseEntity();
            try
            {
                respuesta.extra = FilaTablaCurso(entidad);
                if (respuesta.extra == "error")
                {
                    respuesta.success = false;
                    respuesta.message = "Error al generar registrar la formación académica";
                }
                else
                {
                    respuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.message = "Error al generar registrar la formación académica";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.GenerarFormacionHTML");
            }

            return this.Respuesta(respuesta);

        }
        private string FilaTablaCurso(Cls_Ent_Curso modelo)
        {
            string html = "";
            try
            {
                var lista = new List<Cls_Ent_Curso>();
                var det = new SolicitudRepositorio().ListaCursos(modelo);
                if (modelo.Items != null)
                {
                    lista = modelo.Items.Count == 0 ? (det.Count == 0 ? new List<Cls_Ent_Curso>() : det) : modelo.Items;
                }
                else
                {
                    if (modelo.ID_CURSOS_PRO == 1)
                    {
                        lista = det.Count == 0 ? new List<Cls_Ent_Curso>() : det;
                    }
                }
                if (modelo.ID_CURSOS_PRO == 0)
                {
                    var entity_ = new Cls_Ent_Curso
                    {
                        ID_CURSOS_PRO = modelo.ID_CURSOS_PRO,
                        ID_SOLICITUD = modelo.ID_SOLICITUD,
                        DESC_CURSO_PRO = modelo.DESC_CURSO_PRO,
                    };
                    lista.Add(entity_);
                }
                int num = 0;
                if (lista.Count() > 0)
                {

                    foreach (var item in lista)
                    {
                        num += 1;
                        html += GrupoRegistroCursos(item.ID_CURSOS_PRO, item.ID_SOLICITUD, item.DESC_CURSO_PRO, num);
                    }
                }
            }
            catch (Exception ex)
            {
                html = "error";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.FilaTablaCurso");

            }

            return html;

        }
        private string GrupoRegistroCursos(long ID_CURSOS_PRO, long ID_SOLICITUD, string DESC_CURSO_PRO, int num)
        {

            string html = "";
            string remove = deleteFila("delecurs");
            html += "<tr>";
            html += "<td class='tdID_CURSOS_PRO' style='display:none;'>" + ID_CURSOS_PRO + "</td>";
            html += "<td class='tdID_SOLICITUD' style='display:none;'>" + ID_SOLICITUD + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + num + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + remove + "</td>";
            html += "<td>";
            html += "<input type='text' style='font-weight:bold' class='form-control tdDESC_CURSO_PRO' value='" + DESC_CURSO_PRO + "'" + ">";
            html += "</td>";
            html += "</tr>";
            return html;
        }
        /////FIN CURSO
        /////CONO
        public ActionResult GenerarConoHTML(Cls_Ent_Conocimientos entidad)
        {
            var respuesta = new ResponseEntity();
            try
            {
                respuesta.extra = FilaTablaCono(entidad);
                if (respuesta.extra == "error")
                {
                    respuesta.success = false;
                    respuesta.message = "Error al generar registrar la formación académica";
                }
                else
                {
                    respuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.message = "Error al generar registrar la formación académica";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.GenerarConoHTML");
            }

            return this.Respuesta(respuesta);

        }
        private string FilaTablaCono(Cls_Ent_Conocimientos modelo)
        {
            string html = "";
            try
            {
                var lista = new List<Cls_Ent_Conocimientos>();
                var det = new SolicitudRepositorio().ListaConocimientos(modelo);
                if (modelo.Items != null)
                {
                    lista = modelo.Items.Count == 0 ? (det.Count == 0 ? new List<Cls_Ent_Conocimientos>() : det) : modelo.Items;
                }
                else
                {
                    if (modelo.ID_CONOCIMIENTOS == 1)
                    {
                        lista = det.Count == 0 ? new List<Cls_Ent_Conocimientos>() : det;
                    }
                
                }
                if (modelo.ID_CONOCIMIENTOS == 0)
                {
                    var entity_ = new Cls_Ent_Conocimientos
                    {
                        ID_CONOCIMIENTOS = modelo.ID_CONOCIMIENTOS,
                        ID_SOLICITUD = modelo.ID_SOLICITUD,
                        DESC_CONOCIMIENTO = modelo.DESC_CONOCIMIENTO,
                    };
                    lista.Add(entity_);
                }
                int num = 0;
                if (lista.Count() > 0)
                {

                    foreach (var item in lista)
                    {
                        num += 1;
                        html += GrupoRegistroCono(item.ID_CONOCIMIENTOS, item.ID_SOLICITUD, item.DESC_CONOCIMIENTO, num);
                    }
                }
            }
            catch (Exception ex)
            {
                html = "error";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.FilaTablaCono");

            }

            return html;

        }
        private string GrupoRegistroCono(long ID_CONOCIMIENTOS, long ID_SOLICITUD, string DESC_CONOCIMIENTO, int num)
        {

            string html = "";
            string remove = deleteFila("delecono");
            html += "<tr>";
            html += "<td class='tdID_CONOCIMIENTOS' style='display:none;'>" + ID_CONOCIMIENTOS + "</td>";
            html += "<td class='tdID_SOLICITUD' style='display:none;'>" + ID_SOLICITUD + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + num + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + remove + "</td>";
            html += "<td>";
            html += "<input type='text' style='font-weight:bold' class='form-control tdDESC_CONOCIMIENTO' value='" + DESC_CONOCIMIENTO + "'" + ">";
            html += "</td>";
            html += "</tr>";
            return html;
        }
        /////FIN CONO
        /////EXPERIENCIA
        public ActionResult GenerarExpHTML(Cls_Ent_Experiencia entidad)
        {
            var respuesta = new ResponseEntity();
            try
            {
                respuesta.extra = FilaTablaExp(entidad);
                if (respuesta.extra == "error")
                {
                    respuesta.success = false;
                    respuesta.message = "Error al generar registrar la formación académica";
                }
                else
                {
                    respuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.message = "Error al generar registrar la formación académica";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.GenerarConoHTML");
            }

            return this.Respuesta(respuesta);

        }
        private string FilaTablaExp(Cls_Ent_Experiencia modelo)
        {
            string html = "";
            try
            {
                var lista = new List<Cls_Ent_Experiencia>();
                var det = new SolicitudRepositorio().ListaExperiencia(modelo);
                if (modelo.Items != null)
                {
                    lista = modelo.Items.Count == 0 ? (det.Count == 0 ? new List<Cls_Ent_Experiencia>() : det) : modelo.Items;
                }
                else
                {
                    if (modelo.ID_EXPERIENCIA == 1)
                    {
                        lista = det.Count == 0 ? new List<Cls_Ent_Experiencia>() : det;
                    }
                }
                if (modelo.ID_EXPERIENCIA == 0)
                {
                    var entity_ = new Cls_Ent_Experiencia
                    {
                        ID_EXPERIENCIA = modelo.ID_EXPERIENCIA,
                        ID_SOLICITUD = modelo.ID_SOLICITUD,
                        DESC_EXPERIENCIA = modelo.DESC_EXPERIENCIA,
                        ID_TIPO_EXPERIENCIA = modelo.ID_TIPO_EXPERIENCIA,
                        ANOS = modelo.ANOS,
                        ID_TIPO_SECTOR = modelo.ID_TIPO_SECTOR,
                        DESC_TIPO_SECTOR = modelo.DESC_TIPO_SECTOR,
                        DESC_TIPO_EXPERIENCIA = modelo.DESC_TIPO_EXPERIENCIA,
                    };
                    lista.Add(entity_);
                }
                int num = 0;
                if (lista.Count() > 0)
                {

                    foreach (var item in lista)
                    {
                        num += 1;
                        html += GrupoRegistroExp(item.ID_EXPERIENCIA, item.ID_SOLICITUD, item.DESC_EXPERIENCIA,item.ID_TIPO_EXPERIENCIA,item.ANOS,item.ID_TIPO_SECTOR, item.DESC_TIPO_EXPERIENCIA, item.DESC_TIPO_SECTOR, num);
                    }
                }
            }
            catch (Exception ex)
            {
                html = "error";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.FilaTablaCono");

            }

            return html;

        }
        private string GrupoRegistroExp(long ID_EXPERIENCIA, long ID_SOLICITUD, string DESC_EXPERIENCIA, int ID_TIPO_EXPERIENCIA, int ANOS,int ID_TIPO_SECTOR,string DESC_TIPO_EXPERIENCIA, string DESC_TIPO_SECTOR,  int num)
        {

            string html = "";
            string remove = deleteFila("deleexp");
            html += "<tr>";
            html += "<td class='tdID_EXPERIENCIA' style='display:none;'>" + ID_EXPERIENCIA + "</td>";
            html += "<td class='tdID_SOLICITUD' style='display:none;'>" + ID_SOLICITUD + "</td>";
            html += "<td class='tdID_TIPO_EXPERIENCIA' style='display:none;'>" + ID_TIPO_EXPERIENCIA + "</td>";
            html += "<td class='tdID_TIPO_SECTOR' style='display:none;'>" + ID_TIPO_SECTOR + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + num + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + remove + "</td>";
            html += "<td>";
            html += "<label style='font-weight:bold; width:100%;' class='form-control tdDESC_TIPO_EXPERIENCIA'>" + DESC_TIPO_EXPERIENCIA + "</label>";
            html += "</td>";
            html += "<td>";
            html += "<label style='font-weight:bold; width:100%;' class='form-control tdDESC_TIPO_SECTOR'>" + DESC_TIPO_SECTOR + "</label>";
            html += "</td>";
            html += "<td>";
            html += "<input type='text' style='font-weight:bold' onkeypress='DESARROLLO.SOLONUMEROS(event);' maxlength='2' class='form-control tdANOS' value='" + ANOS + "'" + " >";
            html += "</td>";
            html += "<td>";
            html += "<input type='text' style='font-weight:bold' class='form-control tdDESC_EXPERIENCIA' value='" + DESC_EXPERIENCIA + "'" + ">";
            html += "</td>";
            html += "</tr>";
            return html;
        }
        /////FIN EXPERIENCIA
        /////ACTIVIDAD
        public ActionResult GenerarActHTML(Cls_Ent_Actividad entidad)
        {
            var respuesta = new ResponseEntity();
            try
            {
                respuesta.extra = FilaTablaAct(entidad);
                if (respuesta.extra == "error")
                {
                    respuesta.success = false;
                    respuesta.message = "Error al generar el registrar de la actividad";
                }
                else
                {
                    respuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.message = "Error al generar el registrar de la actividad";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.GenerarActHTML");
            }

            return this.Respuesta(respuesta);

        }
        private string FilaTablaAct(Cls_Ent_Actividad modelo)
        {
            string html = "";
            try
            {
                var lista = new List<Cls_Ent_Actividad>();
                var det = new SolicitudRepositorio().ListaActividad(modelo);
                if (modelo.Items != null)
                {
                    lista = modelo.Items.Count == 0 ? (det.Count == 0 ? new List<Cls_Ent_Actividad>() : det) : modelo.Items;
                }
                else
                {
                    if (modelo.ID_ACTIVIDAD == 1)
                    {
                        lista = det.Count == 0 ? new List<Cls_Ent_Actividad>() : det;
                    }
                    
                }
                if (modelo.ID_ACTIVIDAD == 0)
                {
                    var entity_ = new Cls_Ent_Actividad
                    {
                        ID_ACTIVIDAD = modelo.ID_ACTIVIDAD,
                        ID_SOLICITUD = modelo.ID_SOLICITUD,
                        DESC_ACTIVIDAD = modelo.DESC_ACTIVIDAD,
                    };
                    lista.Add(entity_);
                }
                int num = 0;
                if (lista.Count() > 0)
                {

                    foreach (var item in lista)
                    {
                        num += 1;
                        html += GrupoRegistroAct(item.ID_ACTIVIDAD, item.ID_SOLICITUD, item.DESC_ACTIVIDAD, num);
                    }
                }
            }
            catch (Exception ex)
            {
                html = "error";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.FilaTablaAct");

            }

            return html;

        }
        private string GrupoRegistroAct(long ID_ACTIVIDAD, long ID_SOLICITUD, string DESC_ACTIVIDAD, int num)
        {

            string html = "";
            string remove = deleteFila("deleact");
            html += "<tr>";
            html += "<td class='tdID_ACTIVIDAD' style='display:none;'>" + ID_ACTIVIDAD + "</td>";
            html += "<td class='tdID_SOLICITUD' style='display:none;'>" + ID_SOLICITUD + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + num + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + remove + "</td>";
            html += "<td>";
            html += "<input type='text' style='font-weight:bold' class='form-control tdDESC_ACTIVIDAD' value='" + DESC_ACTIVIDAD + "'" + ">";
            html += "</td>";
            html += "</tr>";
            return html;
        }
        /////FIN ACTIVIDAD
        /////ACTIVIDAD OTRO
        public ActionResult GenerarActOtroHTML(Cls_Ent_Actividad_Otro entidad)
        {
            var respuesta = new ResponseEntity();
            try
            {
                respuesta.extra = FilaTablaActOtro(entidad);
                if (respuesta.extra == "error")
                {
                    respuesta.success = false;
                    respuesta.message = "Error al generar el registrar otra actividad";
                }
                else
                {
                    respuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.message = "Error al generar el registrar otra actividad";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.GenerarActOtroHTML");
            }

            return this.Respuesta(respuesta);

        }
        private string FilaTablaActOtro(Cls_Ent_Actividad_Otro modelo)
        {
            string html = "";
            try
            {
                var lista = new List<Cls_Ent_Actividad_Otro>();
                var det = new SolicitudRepositorio().ListaActividadOtro(modelo);
                if (modelo.Items != null)
                {
                    lista = modelo.Items.Count == 0 ? (det.Count == 0 ? new List<Cls_Ent_Actividad_Otro>() : det) : modelo.Items;
                }
                else
                {
                    if (modelo.ID_OTRO_ACT == 1)
                    {
                        lista = det.Count == 0 ? new List<Cls_Ent_Actividad_Otro>() : det;
                    }
            
                }
                if (modelo.ID_OTRO_ACT == 0)
                {
                    var entity_ = new Cls_Ent_Actividad_Otro
                    {
                        ID_OTRO_ACT = modelo.ID_OTRO_ACT,
                        ID_SOLICITUD = modelo.ID_SOLICITUD,
                        DESC_ACT_OTRO = modelo.DESC_ACT_OTRO,
                    };
                    lista.Add(entity_);
                }
                int num = 0;
                if (lista.Count() > 0)
                {

                    foreach (var item in lista)
                    {
                        num += 1;
                        html += GrupoRegistroActOtro(item.ID_OTRO_ACT, item.ID_SOLICITUD, item.DESC_ACT_OTRO, num);
                    }
                }
            }
            catch (Exception ex)
            {
                html = "error";
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.FilaTablaActOtro");

            }

            return html;

        }
        private string GrupoRegistroActOtro(long ID_OTRO_ACT, long ID_SOLICITUD, string DESC_ACT_OTRO, int num)
        {

            string html = "";
            string remove = deleteFila("deleactotro");
            html += "<tr>";
            html += "<td class='tdID_OTRO_ACT' style='display:none;'>" + ID_OTRO_ACT + "</td>";
            html += "<td class='tdID_SOLICITUD' style='display:none;'>" + ID_SOLICITUD + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + num + "</td>";
            html += "<td style='text-align:center;font-size:16px'>" + remove + "</td>";
            html += "<td>";
            html += "<input type='text' style='font-weight:bold' class='form-control tdDESC_ACT_OTRO' value='" + DESC_ACT_OTRO + "'" + ">";
            html += "</td>";
            html += "</tr>";
            return html;
        }
        /////FIN ACTIVIDAD OTRO
        /////OBTENER NIVEL
        public ActionResult ObtenerNivelPerfil(Cls_Ent_Solicitud entidad)
        {
            int num = 0;
            var respuesta = new ResponseEntity();
            try
            {
                int exp_gen = 0;
                int exp_exp = 0;
                var b = entidad.listaAcademico.Where(x => x.ID_NIVEL_GRADO == 2);
                var t = entidad.listaAcademico.Where(x => x.ID_NIVEL_GRADO == 3);
                var e = entidad.listaExperiencia;

                foreach (var item in e)
                {
                    exp_gen += item.ANOS;
                }
                var ee = entidad.listaExperiencia.Where(x => x.ID_TIPO_EXPERIENCIA == 2);
                foreach (var item in ee)
                {
                    exp_exp += item.ANOS;
                }
                if (t.Count() > 0)
                {
                    if (exp_gen >= 3 && exp_exp >= 1)
                    {
                        num = 4;
                    }
                    if (exp_gen >= 3 && exp_exp >= 2)
                    {
                        num = 3;
                    }
                    if (exp_gen >= 5 && exp_exp >= 3)
                    {
                        num = 2;
                    }
                    if (exp_gen >= 6 && exp_exp >= 4)
                    {
                        num = 1;
                    }
                }
                else
                {
                    if (b.Count() > 0)
                    {
                        if (exp_gen >= 3 && exp_exp >= 1)
                        {
                            num = 4;
                        }
                        if (exp_gen >= 3 && exp_exp >= 2)
                        {
                            num = 3;
                        }
                    }
                }
                respuesta.success = true;
                respuesta.nivel = num;
            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.nivel = num;
                Log.MensajeLog(ex.ToString(), "Coordinador.ProcesoFagController.ObtenerNivelPerfil");
            }
            return Respuesta(respuesta);
        }
        /////FIN OBTENER NIVEL

        /// <summary>
        /// Hace el registro de la solicitud de Contratación
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns>JSON</returns>
        public ActionResult WizSaveRequerimiento(Cls_Ent_Solicitud entidad)
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
            Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();

                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                Cls_Ent_Documento ent_doc = new Cls_Ent_Documento();

                PreguntaRspta = new SolicitudRepositorio().WizGrabarRequerimientoRepo(entidad);

                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;

                    return Respuesta(itemRespuesta);
                }

                var sustento = new SolicitudRepositorio().MantenimientoDOcumento(new Cls_Ent_Documento { ID_PROCESO = PreguntaRspta.ID_SOLICITUD, FLG_TIPO = "S", ACCION = "T" });
                if (!sustento.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error en registrar los archivos de la solicitud.";
                    itemRespuesta.success = false;
                    itemRespuesta.message2 = "0";
                    itemRespuesta.extra = Log.MensajeLogText();

                    return Respuesta(itemRespuesta);
                }

                var listaSustentos = jsSerializer.Deserialize<List<Cls_Ent_Documento>>(Request.Form["listaDocumentos"]);
                foreach (var item in listaSustentos)
                {
                    if (item.ID_LF == 0)
                    {
                        var carpeta = Server.MapPath("~//ArchivoTemp//Registro_Multiple//");
                        string NombreArchivo = carpeta + item.NOM_DOCUMENTO + ".pdf";
                        string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + PreguntaRspta.ID_SOLICITUD;
                        //ID_LF = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, item.NOM_DOCUMENTO + ".pdf", UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                    }

                    long ID_LF = 0;
                    var entity_ = new Cls_Ent_Documento
                    {
                        ID_DOCUMENTO = item.ID_DOCUMENTO,
                        ID_PROCESO = PreguntaRspta.ID_SOLICITUD,
                        FLG_TIPO = item.FLG_TIPO,
                        DES_DOCUMENTO = item.DES_DOCUMENTO,
                        ID_LF = ID_LF == 0 ? item.ID_LF : ID_LF,
                        RUTA_ARCHIVO = item.RUTA_ARCHIVO,
                        NOM_DOCUMENTO = item.NOM_DOCUMENTO,
                    };
                    ent_doc = new SolicitudRepositorio().MantenimientoDOcumento(entity_);
                }
    
                itemRespuesta.message = "Se registro correctamente la solicitud.";
                itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                itemRespuesta.extra2 = entidad.ID_SOLICITUD.ToString();
                itemRespuesta.success = true;
            }
            catch
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno();
            }

            return Respuesta(itemRespuesta);
        }

        public ActionResult WizSaveRequisitos(Cls_Ent_Solicitud entidad)
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
            Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                if (entidad.FLG_PROCESO != "D")
                {
                    entidad.listaExperiencia = jsSerializer.Deserialize<List<Cls_Ent_Experiencia>>(Request.Form["listaExperiencia"]);
                    entidad.listaCurso = jsSerializer.Deserialize<List<Cls_Ent_Curso>>(Request.Form["listaCurso"]);
                    entidad.listaAcademico = jsSerializer.Deserialize<List<Cls_Ent_Academico>>(Request.Form["listaAcademico"]);
                    entidad.listaConocimiento = jsSerializer.Deserialize<List<Cls_Ent_Conocimientos>>(Request.Form["listaConocimiento"]);
                }
                PreguntaRspta = new SolicitudRepositorio().WizGrabarRequisitoRepo(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;

                    return Respuesta(itemRespuesta);
                }

                itemRespuesta.message = "Se registro correctamente la solicitud.";
                itemRespuesta.success = true;
            } catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno();
            }
            return Respuesta(itemRespuesta);
        }
        public ActionResult WizSaveContrato(Cls_Ent_Solicitud entidad)
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
            Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                if (entidad.FLG_PROCESO != "D")
                {
                    entidad.listaActividad = jsSerializer.Deserialize<List<Cls_Ent_Actividad>>(Request.Form["listaActividad"]);
                    entidad.listaAcividadOtro = jsSerializer.Deserialize<List<Cls_Ent_Actividad_Otro>>(Request.Form["listaAcividadOtro"]);
                    entidad.listaRenumeracion = jsSerializer.Deserialize<List<Cls_Ent_Renumeracion>>(Request.Form["listaRenumeracion"]);
                }
                PreguntaRspta = new SolicitudRepositorio().WizGrabarContratoRepo(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;

                    return Respuesta(itemRespuesta);
                }

                itemRespuesta.message = "Se registro correctamente la solicitud.";
                itemRespuesta.success = true;
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno();
            }
            return Respuesta(itemRespuesta);
        }

        public ActionResult WizardPaso4FAC_v2(Cls_Ent_Solicitud entidad)
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
             Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
               
                PreguntaRspta = new SolicitudRepositorio().WizGrabarLocador(entidad);
                if (PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Se actualizo correctamente el pase 4.";
                    itemRespuesta.extra2 = entidad.ID_SOLICITUD.ToString();
                    itemRespuesta.success = true;
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno();
            }

            return Respuesta(itemRespuesta);
        }
        /////// Fin Wizard Controllers
        public ActionResult MantenimientoSolicitudFag_V2(Cls_Ent_Solicitud entidad)
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
            Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                if (entidad.FLG_PROCESO != "D")
                {
                    entidad.listaExperiencia = jsSerializer.Deserialize<List<Cls_Ent_Experiencia>>(Request.Form["listaExperiencia"]);
                    entidad.listaCurso = jsSerializer.Deserialize<List<Cls_Ent_Curso>>(Request.Form["listaCurso"]);
                    entidad.listaAcademico = jsSerializer.Deserialize<List<Cls_Ent_Academico>>(Request.Form["listaAcademico"]);
                    entidad.listaConocimiento = jsSerializer.Deserialize<List<Cls_Ent_Conocimientos>>(Request.Form["listaConocimiento"]);
                    entidad.listaActividad = jsSerializer.Deserialize<List<Cls_Ent_Actividad>>(Request.Form["listaActividad"]);
                    entidad.listaAcividadOtro = jsSerializer.Deserialize<List<Cls_Ent_Actividad_Otro>>(Request.Form["listaAcividadOtro"]);
                    entidad.listaRenumeracion = jsSerializer.Deserialize<List<Cls_Ent_Renumeracion>>(Request.Form["listaRenumeracion"]);

                }
                PreguntaRspta = new SolicitudRepositorio().MantenimientoSolicitudFag_V2(entidad);
                Cls_Ent_Documento ent_doc = new Cls_Ent_Documento();
                if (entidad.FLG_PROCESO != "D")
                {
                    if (PreguntaRspta.FLG_OK)
                    {
                        var delete = new SolicitudRepositorio().MantenimientoDOcumento(new Cls_Ent_Documento { ID_PROCESO = PreguntaRspta.ID_SOLICITUD, FLG_TIPO = "S", ACCION = "T" });
                        if (delete.FLG_OK)
                        {
                            var doc = jsSerializer.Deserialize<List<Cls_Ent_Documento>>(Request.Form["listaDocumentos"]);
                            long ID_LF = 0;
                            foreach (var item in doc)
                            {
                                if (item.ID_LF == 0)
                                {
                                    var carpeta = Server.MapPath("~//ArchivoTemp//Registro_Multiple//");
                                    string NombreArchivo = carpeta + item.NOM_DOCUMENTO + ".pdf";
                                    string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + PreguntaRspta.ID_SOLICITUD;
                                    //ID_LF = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, item.NOM_DOCUMENTO + ".pdf", UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                                }

                                var entity_ = new Cls_Ent_Documento
                                {
                                    ID_DOCUMENTO = item.ID_DOCUMENTO,
                                    ID_PROCESO = PreguntaRspta.ID_SOLICITUD,
                                    FLG_TIPO = item.FLG_TIPO,
                                    DES_DOCUMENTO = item.DES_DOCUMENTO,
                                    ID_LF = ID_LF == 0 ? item.ID_LF : ID_LF,
                                    RUTA_ARCHIVO = item.RUTA_ARCHIVO,
                                    NOM_DOCUMENTO = item.NOM_DOCUMENTO,
                                };
                                ent_doc = new SolicitudRepositorio().MantenimientoDOcumento(entity_);
                            }
                        }

                    }
                }
                else
                {
                    ent_doc.FLG_OK = true;
                }


                if (ent_doc.FLG_OK)
                {
                    if (!PreguntaRspta.FLG_OK)
                    {
                        itemRespuesta.message = "Ocurrio un error.";
                        itemRespuesta.success = false;
                        itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
                    }
                    else
                    {
                        if (entidad.FLG_PROCESO == "I")
                        {
                            itemRespuesta.message = "Se registro correctamente la solicitud.";
                        }
                        else
                        {
                            if (entidad.FLG_PROCESO == "U")
                            {
                                itemRespuesta.message = "Se actualizo correctamente la solicitud.";
                            }
                            else
                            {
                                itemRespuesta.message = "Se elimino correctamente la solicitud.";
                            }
                        }


                        itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                        itemRespuesta.success = true;
                    }
                }
                else
                {
                    itemRespuesta.message = "Ocurrio un error en registrar los archivos de la solicitud.";
                    itemRespuesta.success = false;
                    itemRespuesta.message2 = "0";
                    itemRespuesta.extra = Log.MensajeLogText();
                }

            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno();
            }

            return Respuesta(itemRespuesta);
        }
        #endregion
        #region SOLICITUDES V.2 CEU
        public ActionResult NuevoEditaSolicitudCEU_v2(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud modelo = new Cls_Ent_Solicitud();
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
            var Lista_Entidad = new SolicitudRepositorio().ListaEntidades(new Cls_Ent_Entidades { ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD });
            ViewBag.DcboEntidades = Lista_Entidad;
            modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString();
            ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(new List<Cls_Ent_Renumeracion>());
            if (ID_SOLICITUD == 0)
            {
                modelo.FLG_PROCESO = "I";
                modelo.TIPO_PROCESO = "F";
            }
            else
            {
                var lista = new SolicitudRepositorio().ListaSolicitudes(new Cls_Ent_Solicitud { ID_SOLICITUD = ID_SOLICITUD }).First();
                modelo = lista;
                modelo.ID = Lista_Entidad[0].ID_PADRE_ENTIDAD.ToString();
                var XXXXXXXX = new SolicitudRepositorio().ListaPeriodoPagoSolicitud(new Cls_Ent_Renumeracion { ID_SOLICITUD = ID_SOLICITUD });
                if (XXXXXXXX.Count > 0)
                {
                    ViewBag.ListaRenumeracion = JsonConvert.SerializeObject(XXXXXXXX);
                }
                modelo.TIPO_PROCESO = "F";
                modelo.FLG_PROCESO = "U";
            }
            modelo.FLG_VERSION = "2";
            modelo.ID_SOLICITUD = ID_SOLICITUD;
            modelo.MONTO_MENSUAL_CEU = 3000;
            return View(modelo);
        }
        public ActionResult MantenimientoSolicitudFagCeu_V2(Cls_Ent_Solicitud entidad)
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
            Cls_Ent_Solicitud PreguntaRspta = null;
            try
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.USUARIO;
                entidad.IP_INGRESO = Request.UserHostAddress.ToString().Trim();
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                if (entidad.FLG_PROCESO != "D")
                {
                    entidad.listaActividad = jsSerializer.Deserialize<List<Cls_Ent_Actividad>>(Request.Form["listaActividad"]);
                    entidad.listaAcividadOtro = jsSerializer.Deserialize<List<Cls_Ent_Actividad_Otro>>(Request.Form["listaAcividadOtro"]);
                    entidad.listaRenumeracion = jsSerializer.Deserialize<List<Cls_Ent_Renumeracion>>(Request.Form["listaRenumeracion"]);
                }
                PreguntaRspta = new SolicitudRepositorio().MantenimientoSolicitudFagCeu_V2(entidad);
                Cls_Ent_Documento ent_doc = new Cls_Ent_Documento();
                if (entidad.FLG_PROCESO != "D")
                {
                    if (PreguntaRspta.FLG_OK)
                    {
                        var delete = new SolicitudRepositorio().MantenimientoDOcumento(new Cls_Ent_Documento { ID_PROCESO = PreguntaRspta.ID_SOLICITUD, FLG_TIPO = "S", ACCION = "T" });
                        if (delete.FLG_OK)
                        {
                            var doc = jsSerializer.Deserialize<List<Cls_Ent_Documento>>(Request.Form["listaDocumentos"]);
                            long ID_LF = 0;
                            foreach (var item in doc)
                            {
                                if (item.ID_LF == 0)
                                {
                                    var carpeta = Server.MapPath("~//ArchivoTemp//Registro_Multiple//");
                                    string NombreArchivo = carpeta + item.NOM_DOCUMENTO + ".pdf";
                                    string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + PreguntaRspta.ID_SOLICITUD;
                                    //ID_LF = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, UsuarioSistemaSesion.DESC_ENTIDAD, SubcarpetaLF, item.NOM_DOCUMENTO + ".pdf", UsuarioSistemaSesion.USUARIO, entidad.IP_PC);
                                    ID_LF = 0;
                                }

                                var entity_ = new Cls_Ent_Documento
                                {
                                    ID_DOCUMENTO = item.ID_DOCUMENTO,
                                    ID_PROCESO = PreguntaRspta.ID_SOLICITUD,
                                    FLG_TIPO = item.FLG_TIPO,
                                    DES_DOCUMENTO = item.DES_DOCUMENTO,
                                    ID_LF = ID_LF == 0 ? item.ID_LF : ID_LF,
                                    RUTA_ARCHIVO = item.RUTA_ARCHIVO,
                                    NOM_DOCUMENTO = item.NOM_DOCUMENTO,
                                };
                                ent_doc = new SolicitudRepositorio().MantenimientoDOcumento(entity_);
                            }
                        }

                    }
                }
                else
                {
                    ent_doc.FLG_OK = true;
                }


                if (ent_doc.FLG_OK)
                {
                    if (!PreguntaRspta.FLG_OK)
                    {
                        itemRespuesta.message = "Ocurrio un error.";
                        itemRespuesta.success = false;
                        itemRespuesta.extra = Log.MensajeLogText();// entidad.DES_ERROR;
                    }
                    else
                    {
                        if (entidad.FLG_PROCESO == "I")
                        {
                            itemRespuesta.message = "Se registro correctamente la solicitud.";
                        }
                        else
                        {
                            if (entidad.FLG_PROCESO == "U")
                            {
                                itemRespuesta.message = "Se actualizo correctamente la solicitud.";
                            }
                            else
                            {
                                itemRespuesta.message = "Se elimino correctamente la solicitud.";
                            }
                        }


                        itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                        itemRespuesta.success = true;
                    }
                }
                else
                {
                    itemRespuesta.message = "Ocurrio un error en registrar los archivos de la solicitud.";
                    itemRespuesta.success = false;
                    itemRespuesta.message2 = "0";
                    itemRespuesta.extra = Log.MensajeLogText();
                }

            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno();
            }

            return Respuesta(itemRespuesta);
        }
        #endregion
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
