using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Entity.Coordinador;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio;
using APP.MEF.EXTRANET.FAG.PAG.Core;
using APP.MEF.EXTRANET.FAG.PAG.Response;
using MEF.PROYECTO.Utilitario;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Configuration;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.Entity.Administracion;
using System.Text;
using OfficeOpenXml;
using System.Threading.Tasks;
using System.Net.Mime;

namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Controllers
{

    public class PersonalController : BaseController
    {
        //
        // GET: /Usuario/Personal/


        Cls_Ent_Personal PersonalSistemaSesion = new Cls_Ent_Personal();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inicio()
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
            List<Cls_Ent_Entidades> lista = null;
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            xx.USU_CONSULTOR = PersonalSistemaSesion.NUM_DOCUMENTO;
            lista = Cls_Rule_Entidades.ListaEntidadesConsultor(xx);//.FindAll(A => A.ListaEntidadesConsultor.Equals(0));
            lista = lista.OrderBy(A => A.DESC_UNIDAD).ToList();
            List<Cls_Ent_Entidades> Listdata = new List<Cls_Ent_Entidades>();
            Cls_Ent_Entidades data = null;
            foreach (var item in lista)
            {
                data = new Cls_Ent_Entidades();
                data.ID_ENTIDAD = item.ID_ENTIDAD;
                data.DESC_ENTIDAD = item.DESC_UNIDAD;
                Listdata.Add(data);
            }
            ViewBag.DcboEntidades = Listdata;
            return View();
        }
        public ActionResult MisDatos(int ID_PERSONAL)
        {
            Cls_Ent_Personal Modelo = new Cls_Ent_Personal();
            using (PersonalReposiorio repositorio = new PersonalReposiorio())
            {
                Modelo = repositorio.ListaPersonal(Modelo).First(A=> A.ID_PERSONAL== ID_PERSONAL);
            }
            using (PersonalReposiorio repositorio = new PersonalReposiorio())
            {
                Modelo.ListaPais = repositorio.ListaTipoNacionalidad().Select(x => new SelectListItem()
                {
                    Text = x.DESC_NACIONALIDAD.Trim(),
                    Value = x.ID_TIPO_NACIONALIDAD.ToString()
                }).ToList();
                Modelo.ListaPais.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
                Modelo.ListaBanco = repositorio.ListaTipoBanco().Select(x => new SelectListItem()
                {
                    Text = x.DESC_BANCO.Trim(),
                    Value = x.ID_TIPO_BANCO.ToString()
                }).ToList();
                Modelo.ListaBanco.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });

            }
            using (UbigeoRepositorio repositorio = new UbigeoRepositorio())
            {
                Modelo.ListaDptoNac = repositorio.ListaDepartamento().Select(x => new SelectListItem()
                {
                    Text = x.CNOMDEPARTAMENTO.Trim(),
                    Value = x.CCODDEPARTAMENTO.ToString()
                }).ToList();
                Modelo.ListaDptoNac.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }
            using (UbigeoRepositorio repositorio = new UbigeoRepositorio())
            {
                Modelo.ListaDptoDom = repositorio.ListaDepartamento().Select(x => new SelectListItem()
                {
                    Text = x.CNOMDEPARTAMENTO.Trim(),
                    Value = x.CCODDEPARTAMENTO.ToString()
                }).ToList();
                Modelo.ListaDptoDom.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }
            return View(Modelo);
        }


        public ActionResult MiCv()
        {
            return View();
        }
        public ActionResult MisPostulaciones()
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
            List<Cls_Ent_Entidades> lista = null;
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            xx.USU_CONSULTOR = PersonalSistemaSesion.NUM_DOCUMENTO;
            lista = Cls_Rule_Entidades.ListaEntidadesConsultor(xx);//.FindAll(A => A.ListaEntidadesConsultor.Equals(0));
            lista = lista.OrderBy(A => A.DESC_UNIDAD).ToList();
            List<Cls_Ent_Entidades> Listdata = new List<Cls_Ent_Entidades>();
            Cls_Ent_Entidades data = null;
            foreach (var item in lista)
            {
                data = new Cls_Ent_Entidades();
                data.ID_ENTIDAD = item.ID_ENTIDAD;
                data.DESC_ENTIDAD = item.DESC_UNIDAD;
                Listdata.Add(data);
            }
            ViewBag.DcboEntidades = Listdata;
            return View();
        }
        //ESTUDIOS REALIZADOS
        public ActionResult MantenimientoEstudios(int ESTUDIOS)
        {
            Cls_Ent_Estudios Modelo = new Cls_Ent_Estudios();

            Modelo.ACCION = "I";
            if (ESTUDIOS > 0)
            {
                Modelo.ID_PERSONAL = 0;
                Modelo.ID_FORMAC_ACADEMICA = ESTUDIOS;
                Modelo = new PersonalReposiorio().ListaEstudios(Modelo).First();
                Modelo.ACCION = "U";
            }
            using (PersonalReposiorio repositorio = new PersonalReposiorio())
            {
                Modelo.ListaNivel = repositorio.ListaNivelAcademico().Select(x => new SelectListItem()
                {
                    Text = x.DESC_NIVEL_ACADEMICO.Trim(),
                    Value = x.ID_NIVEL_ACADEMICO.ToString()
                }).ToList();
                Modelo.ListaNivel.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
                Modelo.ListaGrado = repositorio.ListaNivelGrado().Select(x => new SelectListItem()
                {
                    Text = x.NOMBRE_NIVEL.Trim(),
                    Value = x.ID_NIVEL_GRADO.ToString()
                }).ToList();
                Modelo.ListaGrado.Insert(0, new SelectListItem() { Value = "", Text = "--Otro--" });
            }
            return View(Modelo);
        }
        public ActionResult DML_MantenimientoEstudio(Cls_Ent_Estudios entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Estudios PreguntaRspta = null;
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
            try
            {
                entidad.USU_INGRESO = PersonalSistemaSesion.USUARIO;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                var httpRequest = Request.Files;
                int IdLaserfiche = 0;
                if (httpRequest.Count > 0)
                {
                    var files = Request.Files[0] as HttpPostedFileBase;
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSave = "";
                    using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                    {
                        string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                        files.SaveAs(NombreArchivo);
                        string SubcarpetaLF = entidad.USU_INGRESO + "\\" + "ESTUDIOS";
                        nomArchivoSave = entidad.DESC_ACADEMICA.Replace(" ", String.Empty) + RandomString(5, true) + ".pdf";
                        IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "CONSULTORES", SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_PC);
                        entidad.ARCHIVO = IdLaserfiche;
                    }
                }

                entidad.ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL;
                PreguntaRspta = new PersonalReposiorio().Mentenimiento_Estudios(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {
                    itemRespuesta.extra = entidad.ID_FORMAC_ACADEMICA.ToString();
                    itemRespuesta.success = true;
                    if (entidad.ACCION == "I")
                    { itemRespuesta.message = "El estudio se registro correctamente."; }
                    if (entidad.ACCION == "U")
                    { itemRespuesta.message = "El estudio se actualizo correctamente."; }
                    if (entidad.ACCION == "D")
                    { itemRespuesta.message = "El estudio se elimino correctamente."; }
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
        public ActionResult ListaEstudios(Cls_Ent_Estudios entidad)
        {
            List<Cls_Ent_Estudios> lista;
            lista = new PersonalReposiorio().ListaEstudios(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //FIN ESTUDIOS
        //ESTUDIOS ESPECIALIZACION
        //ESTUDIOS REALIZADOS
        public ActionResult MantenimientoEspecializacon(int ESPECIALIZACION)
        {
            Cls_Ent_Especializacion Modelo = new Cls_Ent_Especializacion();

            Modelo.ACCION = "I";
            if (ESPECIALIZACION > 0)
            {
                Modelo.ID_PERSONAL = 0;
                Modelo.ID_ESPECIALIZACION = ESPECIALIZACION;
                Modelo = new PersonalReposiorio().ListaEspecializacion(Modelo).First();
                Modelo.ACCION = "U";
            }
            using (PersonalReposiorio repositorio = new PersonalReposiorio())
            {
                Modelo.ListaTipoEspecializacion = repositorio.ListaTipoEspecializacion().Select(x => new SelectListItem()
                {
                    Text = x.DESC_TIPO_ESPECIALIZACION.Trim(),
                    Value = x.ID_TIPO_ESPECIALIZACION.ToString()
                }).ToList();
                Modelo.ListaTipoEspecializacion.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }
            return View(Modelo);
        }
        public ActionResult DML_MantenimientoEspecializacion(Cls_Ent_Especializacion entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Especializacion PreguntaRspta = null;
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
            try
            {
                var httpRequest = Request.Files;
                int IdLaserfiche = 0;
                entidad.USU_INGRESO = PersonalSistemaSesion.USUARIO;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                entidad.ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL;
                if (httpRequest.Count > 0)
                {
                    var files = Request.Files[0] as HttpPostedFileBase;
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSave = "";
                    using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                    {
                        string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                        files.SaveAs(NombreArchivo);
                        string SubcarpetaLF = entidad.USU_INGRESO + "\\" + "CURSOS";
                        nomArchivoSave = entidad.NOMBRE_ESPECIALIZACION.Replace(" ", String.Empty) + RandomString(5, true) + ".pdf";
                        IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "CONSULTORES", SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_PC);
                        entidad.ARCHIVO = IdLaserfiche;
                    }
                }
                PreguntaRspta = new PersonalReposiorio().Mentenimiento_Especializacion(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {
                    itemRespuesta.extra = entidad.ID_ESPECIALIZACION.ToString();
                    itemRespuesta.success = true;
                    if (entidad.ACCION == "I")
                    { itemRespuesta.message = "La especialización se registro correctamente."; }
                    if (entidad.ACCION == "U")
                    { itemRespuesta.message = "La especialización se actualizo correctamente."; }
                    if (entidad.ACCION == "D")
                    { itemRespuesta.message = "La especialización se elimino correctamente."; }
                }

            }
            catch (Exception ex )
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeInterno() + "</br>" + ex.ToString();
            }
        
            return Respuesta(itemRespuesta);
        }
        public ActionResult ListaEspecializacion(Cls_Ent_Especializacion entidad)
        {
            List<Cls_Ent_Especializacion> lista;
            lista = new PersonalReposiorio().ListaEspecializacion(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //FIN ESPECIALIZACION
        //ESTUDIOS CAPACITACION
        public ActionResult MantenimientoCapacitacion(int ID_CAPACITACION)
        {
            Cls_Ent_Capacitacion Modelo = new Cls_Ent_Capacitacion();

            Modelo.ACCION = "I";
            if (ID_CAPACITACION > 0)
            {
                Modelo.ID_PERSONAL = 0;
                Modelo.ID_CAPACITACION = ID_CAPACITACION;
                Modelo = new PersonalReposiorio().ListaCapacitacion(Modelo).First();
                Modelo.ACCION = "U";
            }
            return View(Modelo);
        }
        public ActionResult DML_MantenimientoCapacitacion(Cls_Ent_Capacitacion entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Capacitacion PreguntaRspta = null;
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
            try
            {
                var httpRequest = Request.Files;
                int IdLaserfiche = 0;
                entidad.USU_INGRESO = PersonalSistemaSesion.USUARIO;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                entidad.ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL;
                if (httpRequest.Count > 0)
                {
                    var files = Request.Files[0] as HttpPostedFileBase;
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSave = "";
                    using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                    {
                        string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                        files.SaveAs(NombreArchivo);
                        string SubcarpetaLF = entidad.USU_INGRESO + "\\" + "CAPACITACION";
                        nomArchivoSave = entidad.NOMBRE_CAPACITACION.Replace(" ", String.Empty) + RandomString(5, true) + ".pdf";
                        IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "CONSULTORES", SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_PC);
                        entidad.ARCHIVO = IdLaserfiche;
                    }
                }
                PreguntaRspta = new PersonalReposiorio().Mentenimiento_Capacitacion(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();//entidad.DES_ERROR;
                }
                else
                {
                    itemRespuesta.extra = entidad.ID_CAPACITACION.ToString();
                    itemRespuesta.success = true;
                    if (entidad.ACCION == "I")
                    { itemRespuesta.message = "La capacitación se registro correctamente."; }
                    if (entidad.ACCION == "U")
                    { itemRespuesta.message = "La capacitación se actualizo correctamente."; }
                    if (entidad.ACCION == "D")
                    { itemRespuesta.message = "La capacitación se elimino correctamente."; }
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
        public ActionResult ListaCapacitacion(Cls_Ent_Capacitacion entidad)
        {
            List<Cls_Ent_Capacitacion> lista;
            lista = new PersonalReposiorio().ListaCapacitacion(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //FIN CAPACITACION
        //EXPERIENCIA
        public ActionResult MantenimientoExperiencia(int ID_EXPERIENCIA)
        {
            Cls_Ent_Experiencia_Laboral Modelo = new Cls_Ent_Experiencia_Laboral();

            Modelo.ACCION = "I";
            if (ID_EXPERIENCIA > 0)
            {
                Modelo.ID_PERSONAL = 0;
                Modelo.ID_EXPERIENCIA = ID_EXPERIENCIA;
                Modelo = new PersonalReposiorio().ListaExperiencia(Modelo).First();
                Modelo.ACCION = "U";
            }
            using (PersonalReposiorio repositorio = new PersonalReposiorio())
            {
                Modelo.ListaTipoExpeiencia = repositorio.ListaTipoExperiencia().Select(x => new SelectListItem()
                {
                    Text = x.DESC_TIPO_EXPERIENCIA.Trim(),
                    Value = x.ID_TIPO_EXPERIENCIA.ToString()
                }).ToList();
                Modelo.ListaTipoExpeiencia.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--" });
            }
            return View(Modelo);
        }
        public ActionResult DML_MantenimientoExperiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Experiencia_Laboral PreguntaRspta = null;
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
            try
            {
                var httpRequest = Request.Files;
                entidad.USU_INGRESO = PersonalSistemaSesion.USUARIO;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                entidad.ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL;
                int IdLaserfiche = 0;
                if (httpRequest.Count > 0)
                {
                    var files = Request.Files[0] as HttpPostedFileBase;
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSave = "";
                    string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                    files.SaveAs(NombreArchivo);
                    string SubcarpetaLF = entidad.USU_INGRESO + "\\" + "EXPERIENCIA_LABORAL";
                    nomArchivoSave = entidad.CARGO_EMPRESA.Replace(" ", String.Empty) + RandomString(5, true) + ".pdf";
                    IdLaserfiche = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "CONSULTORES", SubcarpetaLF, nomArchivoSave, entidad.USU_INGRESO, entidad.IP_PC);
                    entidad.ARCHIVO = IdLaserfiche;
                }
                PreguntaRspta = new PersonalReposiorio().Mentenimiento_Experiencia(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {
                    itemRespuesta.extra = entidad.ID_EXPERIENCIA.ToString();
                    itemRespuesta.success = true;
                    if (entidad.ACCION == "I")
                    { itemRespuesta.message = "La experiencia se registro correctamente."; }
                    if (entidad.ACCION == "U")
                    { itemRespuesta.message = "La experiencia se actualizo correctamente."; }
                    if (entidad.ACCION == "D")
                    { itemRespuesta.message = "La experiencia se elimino correctamente."; }
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
        public ActionResult ListaExperiencia (Cls_Ent_Experiencia_Laboral entidad)
        {
            List<Cls_Ent_Experiencia_Laboral> lista;
            lista = new PersonalReposiorio().ListaExperiencia(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult CalcularExperiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            Cls_Ent_Experiencia_Laboral lExperiencia = new Cls_Ent_Experiencia_Laboral();
            int anio = 0;
            int mes = 0;
            int dia = 0;
            DateTime ff = entidad.FEC_FIN_EXPERIENCIA;
            entidad.FEC_FIN_EXPERIENCIA = entidad.FEC_FIN_EXPERIENCIA.AddDays(1);

            string FECHA_FIN = entidad.FEC_FIN_EXPERIENCIA.ToString("dd/MM/yyyy");
            string FECHA_INI = entidad.FEC_INICIO_EXPERIENCIA.ToString("dd/MM/yyyy");

            TimeSpan difference = DateTime.Parse(FECHA_FIN, new CultureInfo("es-PE")).Subtract(DateTime.Parse(FECHA_INI, new CultureInfo("es-PE")));
            if (difference.Ticks > 0)
            {

                var anioDif = Math.Truncate((Decimal)(difference.Days / 365));
                var FechaInicio = DateTime.Parse(FECHA_INI, new CultureInfo("es-PE"));
                var FechaFinal = DateTime.Parse(FECHA_FIN, new CultureInfo("es-PE"));

                FechaInicio = FechaInicio.AddYears((int)anioDif);
                if (FechaInicio > FechaFinal)
                {
                    FechaInicio = FechaInicio.AddYears(-1);
                    anioDif -= 1;
                }


                TimeSpan Diferencia2 = FechaFinal.Subtract(FechaInicio);
                var mesDif2 = Math.Truncate((Decimal)(Diferencia2.Days / 30));

                FechaInicio = FechaInicio.AddMonths((int)mesDif2); // new Date(fechaInicioAnio.setMonth(fechaInicioAnio.getMonth() + 1));
                if (FechaInicio > FechaFinal)
                {
                    FechaInicio = FechaInicio.AddMonths(-1);
                    mesDif2 -= 1;
                }

                var Diferencia3 = FechaFinal.Subtract(FechaInicio);
                anio = (int)anioDif;
                mes = (int)mesDif2;
                dia = Diferencia3.Days;

            }
            lExperiencia.NUM_ANIOS = anio;
            lExperiencia.NUM_MESES = mes;
            lExperiencia.NUM_DIAS = dia;
            var jsonResult = Json(lExperiencia, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //FIN EXPERIENCIA
        public ActionResult UpdatePersonal(Cls_Ent_Personal entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Personal PreguntaRspta = null;
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
            entidad.USU_INGRESO = PersonalSistemaSesion.USUARIO;
            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            entidad.ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL;
            PreguntaRspta = new PersonalReposiorio().UpdatePersonal(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();
            }
            else
            {
                itemRespuesta.extra = entidad.ID_PERSONAL.ToString();
                itemRespuesta.success = true;
                itemRespuesta.message = "Los Datos se actualizarón correctamente."; 
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ListasolicitudPersonal(Cls_Ent_Solicitud_Personal entidad)
        {
            List<Cls_Ent_Solicitud_Personal> lista;
            lista = new PersonalReposiorio().ListasolicitudPersonal(entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public async Task<ActionResult> ExportarExcelSolicitudes(Cls_Ent_Solicitud_Personal entidad)
        {
            Cls_Ent_Solicitud_Personal UsuarioSistemaSesion = new Cls_Ent_Solicitud_Personal();
            try
            {
                if (Session["Usuario"] != null)
                {
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                }
                else
                {
                    var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                    var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                    if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                    {
                        PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                    }
                }
                entidad.ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL;

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                int row = 0;
                using (ExcelPackage package = new ExcelPackage())
                {
                    var ws1 = package.Workbook.Worksheets.Add("Registros_Consulta");
                    row += 1;
                    int fila = 1;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "FECHA REGISTRO", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "H.R. ASIGNADA", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "N° SOLICITUD", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "ESTADO", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 40; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "DNI / CE ", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "APELLIDOS Y NOMBRES", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 50; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "N° DE CONTRATO", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    if (entidad.TIPO_PROCESO == "P")
                    {
                        ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "FICHA", row, fila, 0, 0, "L", true);
                        ws1.Column(fila).Width = 30; fila++;
                    }
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "CARGO", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 40; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "HONORARIOS S/", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "FECHA INICIO", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(ws1, "FECHA FIN", row, fila, 0, 0, "L", true);
                    ws1.Column(fila).Width = 20; fila++;
                    ws1.Row(row).Height = 30;
                    entidad.ID_ENTIDAD = UsuarioSistemaSesion.ID_ENTIDAD;
                    var data = new PersonalReposiorio().ListasolicitudPersonal(entidad);
                    if (data.Count > 0)
                    {
                        foreach (var item in data)
                        {
                            row++;
                            int columna = 1;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.FEC_INGRESO, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NR_TRAMITE, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NUM_PROCESO, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NUM_PROCESO, row, columna, 0, 0, "L"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NUM_DOCUMENTO, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.APELLIDO_PATERNO + " " + item.APELLIDO_MATERNO + " " + item.NOMBRES, row, columna, 0, 0, "L"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.NUM_CONTRATO, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.DENOMINACION_PUESTO, row, columna, 0, 0, "L"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.MONTO_MENSUAL, row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.FECHA_INICIO.ToShortDateString(), row, columna, 0, 0, "C"); columna++;
                            ExcelUtil.CeldaFormatoEtiqueta_V2(ws1, item.FECHA_FIN.ToShortDateString(), row, columna, 0, 0, "C"); columna++;
                        }
                    }
                    using (var stream = new System.IO.MemoryStream(package.GetAsByteArray()))
                    {
                        byte[] buffer = new byte[stream.Length];
                        var cd = new System.Net.Mime.ContentDisposition
                        {
                            FileName = Guid.NewGuid().ToString() + ".xlsx",
                            Inline = false,
                        };
                        Response.AppendHeader("Content-Disposition", cd.ToString());
                        return await Task.FromResult(File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), "Personal.PersonalController.ExportarExcelSolicitudes");
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
        //REGISTRO DE POSTULACIÓN A PROCESO  ASIGNADO
        public ActionResult PropuestaFag(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud_Personal Modelo = new Cls_Ent_Solicitud_Personal();

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
            Modelo = new PersonalReposiorio().ListasolicitudPersonal(new Cls_Ent_Solicitud_Personal { ID_SOLICITUD = ID_SOLICITUD , ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL }).First();
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            Modelo.MONTO_RECIBO = Modelo.MONTO_MENSUAL;
            return View(Modelo);
        }

        public ActionResult PropuestaFagWiz(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud_Personal Modelo = new Cls_Ent_Solicitud_Personal();

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
            Modelo = new PersonalReposiorio().ListasolicitudPersonal(new Cls_Ent_Solicitud_Personal { ID_SOLICITUD = ID_SOLICITUD, ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL }).First();
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            Modelo.MONTO_RECIBO = Modelo.MONTO_MENSUAL;
            return View(Modelo);
        }
        public ActionResult FormatoWizFag(int ID_SOLICITUD,string FLG_PENSIONISTA_POLICIA,string FLG_PENSIONISTA_ESTADO,string FLG_PENSIONES,string FLG_CHECK_4,string FLG_TERMINOS)
        {
            Cls_Ent_Solicitud_Personal Modelo = new Cls_Ent_Solicitud_Personal();

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
            Modelo = new PersonalReposiorio().ListasolicitudPersonal(new Cls_Ent_Solicitud_Personal { ID_SOLICITUD = ID_SOLICITUD, ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL }).First();
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            Modelo.MONTO_RECIBO = Modelo.MONTO_MENSUAL;

            Modelo.FLG_PENSIONISTA_POLICIA = FLG_PENSIONISTA_POLICIA;
            Modelo.FLG_PENSIONISTA_ESTADO = FLG_PENSIONISTA_ESTADO;
            Modelo.FLG_PENSIONES = FLG_PENSIONES;
            Modelo.FLG_CHECK_4 = FLG_CHECK_4;
            Modelo.FLG_TERMINOS = FLG_TERMINOS;
            return View(Modelo);
        }

        public ActionResult NotificarWizFag(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud_Personal Modelo = new Cls_Ent_Solicitud_Personal();

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
            Modelo = new PersonalReposiorio().ListasolicitudPersonal(new Cls_Ent_Solicitud_Personal { ID_SOLICITUD = ID_SOLICITUD, ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL }).First();
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            Modelo.MONTO_RECIBO = Modelo.MONTO_MENSUAL;
            return View(Modelo);
        }
        public ActionResult PropuestaPac(int ID_SOLICITUD)
        {
            Cls_Ent_Solicitud_Personal Modelo = new Cls_Ent_Solicitud_Personal();
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
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            Modelo.ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL;
            Modelo = new PersonalReposiorio().ListasolicitudPersonal(Modelo).First();
            Modelo.ID_SOLICITUD = ID_SOLICITUD;
            Modelo.MONTO_RECIBO = Modelo.MONTO_MENSUAL;
            return View(Modelo);
        }
        public ActionResult UpdateDJ_Fag(Cls_Ent_Solicitud_Personal xx)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud_Personal PreguntaRspta = null;
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
            try
            {
                xx.USU_MODIFICA = PersonalSistemaSesion.USUARIO;
                xx.IP_PC = Request.UserHostAddress.ToString().Trim();
                var filesAnexo2 = Request.Files["FileArchivoAnexo2"] as HttpPostedFileBase;
                int IdLaserficheAnexo02 = 0;
                if (filesAnexo2 != null)
                {
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSave = "";
                    string NombreArchivo = Path.Combine(carpeta, filesAnexo2.FileName.ToString());
                    filesAnexo2.SaveAs(NombreArchivo);
                    string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + xx.ID_SOLICITUD;
                    nomArchivoSave = "ANEXO02_FIRMADO_" + RandomString(5, true) + ".pdf";
                    IdLaserficheAnexo02 = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, xx.ENTIDAD, SubcarpetaLF, nomArchivoSave, xx.USU_MODIFICA, xx.IP_PC);
                    xx.ID_ANEXO2 = IdLaserficheAnexo02;
                }
                var filesAnexo3 = Request.Files["FileArchivoAnexo3"] as HttpPostedFileBase;
                int IdLaserficheAnexo03 = 0;
                if (filesAnexo3 != null)
                {
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSave = "";
                    string NombreArchivo = Path.Combine(carpeta, filesAnexo3.FileName.ToString());
                    filesAnexo3.SaveAs(NombreArchivo);
                    string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + xx.ID_SOLICITUD;
                    nomArchivoSave = "ANEXO03_FIRMADO_" + RandomString(5, true) + ".pdf";
                    IdLaserficheAnexo03 = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, xx.ENTIDAD, SubcarpetaLF, nomArchivoSave, xx.USU_MODIFICA, xx.IP_PC);
                    xx.ID_ANEXO3 = IdLaserficheAnexo03;
                }
                var filesAnexo7 = Request.Files["FileArchivoAnexo7"] as HttpPostedFileBase;
                int IdLaserficheAnexo07 = 0;
                if (filesAnexo7 != null)
                {
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSave = "";
                    string NombreArchivo = Path.Combine(carpeta, filesAnexo7.FileName.ToString());
                    filesAnexo7.SaveAs(NombreArchivo);
                    string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + xx.ID_SOLICITUD;
                    nomArchivoSave = "ANEXO07_FIRMADO_" + RandomString(5, true) + ".pdf";
                    IdLaserficheAnexo07 = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, xx.ENTIDAD, SubcarpetaLF, nomArchivoSave, xx.USU_MODIFICA, xx.IP_PC);
                    xx.ID_ANEXO7 = IdLaserficheAnexo07;
                }
                var filesAnexo8 = Request.Files["FileArchivoBancario"] as HttpPostedFileBase;
                int IdLaserficheAnexo08 = 0;
                if (filesAnexo8 != null)
                {
                    var carpeta = Server.MapPath("~/ArchivoTemp");
                    string nomArchivoSave = "";
                    string NombreArchivo = Path.Combine(carpeta, filesAnexo8.FileName.ToString());
                    filesAnexo8.SaveAs(NombreArchivo);
                    string SubcarpetaLF = "FAG_" + DateTime.Now.Year.ToString() + "\\" + xx.ID_SOLICITUD;
                    nomArchivoSave = "REPORTE_ENTIDAD_BANCARIA" + RandomString(5, true) + ".pdf";
                    IdLaserficheAnexo08 = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, xx.ENTIDAD, SubcarpetaLF, nomArchivoSave, xx.USU_MODIFICA, xx.IP_PC);
                    xx.ID_BANCO = IdLaserficheAnexo08;
                }
                xx.ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL;
                PreguntaRspta = new PersonalReposiorio().UpdateDJ_Fag(xx);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = Log.MensajeLogText();
                }
                else
                {
                    itemRespuesta.extra = xx.ID_SOLICITUD.ToString();
                    itemRespuesta.success = true;
                    itemRespuesta.message = "Los datos se registraron correctamente.";
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
        public ActionResult UpdateDJ_Pac(Cls_Ent_Solicitud_Personal xx)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud_Personal PreguntaRspta = null;
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
            xx.USU_MODIFICA = PersonalSistemaSesion.USUARIO;
            xx.IP_PC = Request.UserHostAddress.ToString().Trim();
            var filesAnexo2 = Request.Files["FileArchivoFORMATOB"] as HttpPostedFileBase;
            int IdLaserficheAnexo02 = 0;
            if (filesAnexo2 != null)
            {
                var carpeta = Server.MapPath("~/ArchivoTemp");
                string nomArchivoSave = "";
                string NombreArchivo = Path.Combine(carpeta, filesAnexo2.FileName.ToString());
                filesAnexo2.SaveAs(NombreArchivo);
                string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + xx.ID_SOLICITUD;
                nomArchivoSave = "FORMATOB_FIRMADO_" + RandomString(5, true) + ".pdf";
                IdLaserficheAnexo02 = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, xx.ENTIDAD, SubcarpetaLF, nomArchivoSave, xx.USU_MODIFICA, xx.IP_PC);
                xx.ID_FORMATOB = IdLaserficheAnexo02;
            }
            var filesAnexo3 = Request.Files["FileArchivoFORMATOC"] as HttpPostedFileBase;
            int IdLaserficheAnexo03 = 0;
            if (filesAnexo3 != null)
            {
                var carpeta = Server.MapPath("~/ArchivoTemp");
                string nomArchivoSave = "";
                string NombreArchivo = Path.Combine(carpeta, filesAnexo3.FileName.ToString());
                filesAnexo3.SaveAs(NombreArchivo);
                string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + xx.ID_SOLICITUD;
                nomArchivoSave = "FORMATOC_FIRMADO_" + RandomString(5, true) + ".pdf";
                IdLaserficheAnexo03 = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, xx.ENTIDAD, SubcarpetaLF, nomArchivoSave, xx.USU_MODIFICA, xx.IP_PC);
                xx.ID_FORMATOC = IdLaserficheAnexo03;
            }
            var filesAnexo7 = Request.Files["FileArchivoFORMATOCV"] as HttpPostedFileBase;
            int IdLaserficheAnexo07 = 0;
            if (filesAnexo7 != null)
            {
                var carpeta = Server.MapPath("~/ArchivoTemp");
                string nomArchivoSave = "";
                string NombreArchivo = Path.Combine(carpeta, filesAnexo7.FileName.ToString());
                filesAnexo7.SaveAs(NombreArchivo);
                string SubcarpetaLF = "PAC_" + DateTime.Now.Year.ToString() + "\\" + xx.ID_SOLICITUD;
                nomArchivoSave = "ANEXO02_FIRMADO_" + RandomString(5, true) + ".pdf";
                IdLaserficheAnexo07 = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, xx.ENTIDAD, SubcarpetaLF, nomArchivoSave, xx.USU_MODIFICA, xx.IP_PC);
                xx.ID_PAC_ANEXO2 = IdLaserficheAnexo07;
            }
            xx.ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL;
            PreguntaRspta = new PersonalReposiorio().UpdateDJ_Pac(xx);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();
            }
            else
            {
                itemRespuesta.extra = xx.ID_SOLICITUD.ToString();
                itemRespuesta.success = true;
                itemRespuesta.message = "Los datos se registraron correctamente.";
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult UpdatePropuesta_Envio(Cls_Ent_Solicitud_Personal entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Solicitud_Personal PreguntaRspta = null;
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
            entidad.ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL;
            PreguntaRspta = new PersonalReposiorio().UpdatePropuesta_Envio(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = Log.MensajeLogText();
            }
            else
            {
                itemRespuesta.extra = entidad.ID_SOLICITUD.ToString();
                itemRespuesta.success = true;
                Cls_Ent_Coordinador xx = new Cls_Ent_Coordinador();
                xx = new CoordinadorRepositorio().ListaCoordinadores(xx).First(A => A.NUM_DOCUMENTO == entidad.USER_COORDINADOR);
                entidad = new PersonalReposiorio().ListasolicitudPersonal(entidad).First();
                EnviarEmailAccionSolicitud(xx, entidad);
                itemRespuesta.message = "Los datos se enviaron correctamente al coordinador de la entidad para su revisión.";
            }

            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailAccionSolicitud(Cls_Ent_Coordinador entidad, Cls_Ent_Solicitud_Personal solicitud)
        {
            string mensaje = "";
            string titulo_correo = "";
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";
            StreamReader sr = null;
            using (sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "NotificarPropuestaPersonal.html"))
            {
                mensaje = sr.ReadToEnd();
            }
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
            mensaje = mensaje.Replace("{0}", PersonalSistemaSesion.APELLIDO_PATERNO + " " + PersonalSistemaSesion.APELLIDO_MATERNO + " " + PersonalSistemaSesion.NOMBRES);
            mensaje = mensaje.Replace("{1}", PersonalSistemaSesion.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", solicitud.DENOMINACION_PUESTO);
            mensaje = mensaje.Replace("{3}", solicitud.ENTIDAD);
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = entidad.CORREO_NOTIFICADOR;
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

        // FIN PROCESO DE POSTULACIÓN
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

        public ActionResult WizardContainer(int consultorId, int solicitudId)
        {
            ViewBag.ConsultorId = consultorId;
            ViewBag.SolicitudId = solicitudId;
            return View();
        }
    }
}
