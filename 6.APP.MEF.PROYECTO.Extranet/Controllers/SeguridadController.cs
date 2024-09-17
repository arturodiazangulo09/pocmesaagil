using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP.MEF.EXTRANET.FAG.PAG.Response;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.Entity.Administracion;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio;
using APP.MEF.EXTRANET.FAG.PAG.Core;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Personal;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio;
using MEF.PROYECTO.Utilitario;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Text;
using APP.MEF.EXTRANET.FAG.PAG.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace APP.MEF.EXTRANET.FAG.PAG.Controllers
{
    public class SeguridadController : BaseController
    {
        //
        // GET: /Seguridad/
        Cls_Ent_Coordinador UsuarioSistemaSesion = new Cls_Ent_Coordinador();
        Cls_Ent_Personal PersonalSistemaSesion = new Cls_Ent_Personal();
        public ActionResult Index()
        {
            Session["Usuario"] = "";
            LIMPIAR_COOKIS("MEF-ID-U-FAGPAC");
            LIMPIAR_COOKIS("MEF-TIPO-U-FAGPAC");
            return View();
        }
        [HttpPost]
        public ActionResult Validar_usuario(Cls_Ent_Coordinador entidad)
        {
            Session["Usuario"] = null;
            ResponseEntity itemRespuesta = new ResponseEntity();
            List<Cls_Ent_Coordinador> lista = null;
            Cls_Ent_Coordinador datos = new Cls_Ent_Coordinador();
            entidad.CONTRASENA = Encriptar.Encriptar_Pass(entidad.CONTRASENA);
            lista = new CoordinadorRepositorio().ListaCoordinadores(new Cls_Ent_Coordinador { FLG_SOLICITUD = "1" }).FindAll(A => A.FLG_ESTADO == 1  && A.USUARIO == entidad.USUARIO);
            if (lista.Count>0)
            {
                lista = lista.FindAll(A => A.CONTRASENA==entidad.CONTRASENA);
                if (lista.Count == 1)
                {
                    datos = lista[0];
                    Session["Usuario"] = datos;
                    itemRespuesta.success = true;
                    itemRespuesta.codigo = Encriptar.Encriptar_Pass(datos.ID_USUARIO.ToString());
                    itemRespuesta.message = datos.FLG_CAMBIO_CLAVE;
                    itemRespuesta.extra2 = "1";
                }
                else
                {
                    itemRespuesta.success = false;
                    itemRespuesta.message = ("Contraseña incorrecta.");
                }        
            }
            else {
                itemRespuesta.success = false;
                itemRespuesta.message = ("El usuario no se encuentra registrado en el sistema.");
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Validar_usuario_Personal(Cls_Ent_Personal entidad)
        {
            Session["Usuario"] = null;
            ResponseEntity itemRespuesta = new ResponseEntity();
            List<Cls_Ent_Personal> lista = null;
            Cls_Ent_Personal datos = null;
            entidad.CONTRASENA = Encriptar.Encriptar_Pass(entidad.CONTRASENA);
            entidad.NUM_DOCUMENTO = entidad.USUARIO;
            lista = new PersonalReposiorio().ListaPersonal(entidad);
            if (lista!= null)
            {
                lista = lista.FindAll(A => A.CONTRASENA == entidad.CONTRASENA);
                if (lista.Count == 1)
                {
                    datos = lista[0];
                    Session["Usuario"] = datos;
                    itemRespuesta.success = true;
                    itemRespuesta.codigo = Encriptar.Encriptar_Pass(datos.ID_PERSONAL.ToString());
                    itemRespuesta.message = datos.FLG_CAMBIO_CLAVE;
                    itemRespuesta.extra2 = "2";
                }
                else
                {
                    itemRespuesta.success = false;
                    itemRespuesta.message = ("Contraseña incorrecta.");
                }
            }
            else
            {
                itemRespuesta.success = false;
                itemRespuesta.message = ("El usuario no se encuentra registrado en el sistema.");
            }
            return Json(itemRespuesta, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CambiaClave()
        {
            Cls_Ent_Coordinador Modelo = new Cls_Ent_Coordinador();
            if (Session["Usuario"] != null)
            {
                UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_coordinador= int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            }
            Modelo.NOMBRE_COMPLETO = UsuarioSistemaSesion.NOMBRE_COMPLETO;
            Modelo.ID_COORDINADOR = UsuarioSistemaSesion.ID_COORDINADOR;
            return View(Modelo);

        }
        public ActionResult CambiaClavePersonal()
        {
            Cls_Ent_Personal Modelo = new Cls_Ent_Personal();
            if (Session["Usuario"] != null)
            {
                PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
            }
            else
            {
                var codigo = HttpContext.Request.Cookies["MEF-ID-U-FAGPAC"];
                var id_personal= int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_Personal(id_personal))
                {
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                }
            }
            Modelo.NOMBRES = PersonalSistemaSesion.NOMBRES;
            Modelo.ID_PERSONAL = PersonalSistemaSesion.ID_PERSONAL;
            return View(Modelo);

        }
        public ActionResult LoGout()
        {
            Session["Usuario"] = "";
            LIMPIAR_COOKIS("MEF-ID-U-FAGPAC");
            return RedirectToAction("Index", "Seguridad");
        }
        public ActionResult AccesoDenegado()
        {
            return View();
        }
        public ActionResult NuevoUsuario()
        {
            BaseModelView Modelo = new BaseModelView();
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            List<Cls_Ent_Entidades> lista = null;
            lista = Cls_Rule_Entidades.ListaEntidades(xx).FindAll(A => A.CANT_DEPENDENCIA.Equals(0));
            
            lista=lista.OrderBy(A => A.DESC_UNIDAD).ToList();
            List<Cls_Ent_Entidades> Listdata = new List<Cls_Ent_Entidades>();
            Cls_Ent_Entidades data = null;
            foreach (var item in lista)
            {
                data = new Cls_Ent_Entidades();
                data.ID_ENTIDAD = item.ID_ENTIDAD;
                data.DESC_ENTIDAD = item.DESC_UNIDAD;
                Listdata.Add(data);
            }
            Listdata.Insert(0, new Cls_Ent_Entidades() { ID_ENTIDAD = 0, DESC_ENTIDAD = "--Seleccione--" });
            ViewBag.DcboEntidades = Listdata;
            IList<Cls_Ent_Ubigeo> cboDep = new UbigeoRepositorio().ListaDepartamento();
            cboDep.Insert(0, new Cls_Ent_Ubigeo() { CCODDEPARTAMENTO = "", CNOMDEPARTAMENTO = "--Seleccione--" });
            ViewBag.Dep = cboDep;
            /* ecg-08022016 - se lee los terminos y condiciones de un archivo */
            Encoding def = Encoding.Default;
            String archivo = Server.MapPath("~/assets/Terminos/TERMINOSCONDICIONES.txt");
            StreamReader sr = new StreamReader(archivo, def);
            Modelo.T_CONDICIONES = sr.ReadToEnd();
            return View(Modelo);
        }
        public JsonResult ListarProvinciaCbo(Cls_Ent_Ubigeo entidad)
        {
            IList<Cls_Ent_Ubigeo> cboProv = new UbigeoRepositorio().listaProvincias(entidad);
            cboProv.Insert(0, new Cls_Ent_Ubigeo() { CCODPROVINCIA = "", CNOMPROVINCIA = "--Seleccione--" });
            ViewBag.Prov = cboProv;
            return Json(cboProv, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarDistritoCbo(Cls_Ent_Ubigeo entidad)
        {
            IList<Cls_Ent_Ubigeo> cboDist = new UbigeoRepositorio().listaDistritos(entidad);
            cboDist.Insert(0, new Cls_Ent_Ubigeo() { CCODDISTRITO = "", CNOMDISTRITO = "--Seleccione--" });
            ViewBag.Dist = cboDist;
            return Json(cboDist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListaEntidadesDetalle(int id_entidad)
        {
            Cls_Ent_Entidades xx = new Cls_Ent_Entidades();
            List<Cls_Ent_Entidades> lista = null;
            lista = Cls_Rule_Entidades.ListaEntidades(xx);
            lista = lista.FindAll(A => A.ID_ENTIDAD == id_entidad);
            var jsonResult = Json(lista, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        public ActionResult ListaDetallePersona(string dni)
        {
            Cls_Ent_Coordinador datos = new Cls_Ent_Coordinador();
            Cls_Ent_Encriptacion resultadoEncriptarDNI = new Cls_Ent_Encriptacion();
            Cls_Ent_Encriptacion resultadoReniecDNI = new Cls_Ent_Encriptacion();
            try
            {
                string llave = "";
                llave = GetLlave();

                if (llave != "") {
                    llave = System.Web.HttpUtility.UrlEncode(llave);
                    resultadoEncriptarDNI = EncriptarLlave(llave, dni);

                    if (resultadoEncriptarDNI.Code == 1)
                    { //Se encripto correctamente el dni
                        string dniEncriptado = resultadoEncriptarDNI.MensajeSalida;

                        resultadoReniecDNI = ObtenerDatosDNI_RENIEC(llave, dniEncriptado);

                        if (resultadoReniecDNI.Code == 1)
                        {
                            datos.APELLIDO_PATERNO = resultadoReniecDNI.Objeto.APE_PATERNO;
                            datos.APELLIDO_MATERNO = resultadoReniecDNI.Objeto.APE_MATERNO;
                            datos.NOMBRES = resultadoReniecDNI.Objeto.NOMBRES;
                            datos.ACCION = "1";
                        }
                    }
                }
               
                //Logica anterior de webservices Reniec
                /*WCF_DatosPersonales.tabla[] resultado = null;
                using (WCF_DatosPersonales.ReniecSoapClient proxy = new WCF_DatosPersonales.ReniecSoapClient())
                {
                    resultado = proxy.TDni(dni);
                    datos.APELLIDO_PATERNO = resultado[0].t02.Trim();
                    datos.APELLIDO_MATERNO = resultado[0].t03.Trim();
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
            var url = ConfigurationManager.AppSettings["Ruta_API_PIDEMEF"].ToString()+ metodo;
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
        public Cls_Ent_Encriptacion EncriptarLlave(string llave, string dni) {
            string metodo = "/sercivios-api/encriptar/encriptar-valor";
            var baseUrl = ConfigurationManager.AppSettings["Ruta_API_PIDEMEF"].ToString() + metodo;
             string responderParameters = "LLAVE="+llave+"&TEXT="+dni;
            var cookieJar = new CookieContainer();
            Cls_Ent_Encriptacion resultado = new Cls_Ent_Encriptacion();
            try {

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
            catch (WebException ex) {
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
            string responderParameters = "LLAVE=" + llave + "&NUM_DOC=" + dni_encriptado+"";
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
                throw ex;
            }
            return resultado;
        }
        public ActionResult ValidarCaptcha(string Captcha)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            var valor = Session["ClaveExtrametCapchaFagPag"].ToString();
            if (Captcha == valor)
            {
                itemRespuesta.success = true;
            }
            else
            {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }

        public ActionResult RegistroSolicitudCoordinador(Cls_Ent_Coordinador entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Coordinador PreguntaRspta = null;
            try
            {
                var httpRequest = Request.Files;
                entidad.USU_INGRESO = entidad.NUM_DOCUMENTO;
                entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
                string nomArchivoSave = "";
                int IdLaserficheDNI = 0;
                int IdLaserficheSUSTENTO = 0;
                if (httpRequest.Count > 0)
                {
                    var files = Request.Files[0] as HttpPostedFileBase;
                    if (files.ContentLength > 0)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files)))
                        {
                            string NombreArchivo = Path.Combine(carpeta, files.FileName.ToString());
                            files.SaveAs(NombreArchivo);
                            string SubcarpetaLF = entidad.NUM_DOCUMENTO + "\\" + "DNI";//"COORDINADORES" + "\\" + entidad.NUM_DOCUMENTO;
                            nomArchivoSave = entidad.NUM_DOCUMENTO + "_" + RandomString(5, true) + ".pdf";
                            IdLaserficheDNI = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "COORDINADORES", SubcarpetaLF, nomArchivoSave, entidad.NUM_DOCUMENTO, entidad.IP_PC);
                        }
                    }
                    var files_ = Request.Files[1] as HttpPostedFileBase;
                    if (files_.ContentLength > 0)
                    {
                        var carpeta = Server.MapPath("~/ArchivoTemp");
                        using (BinaryReader reader = new BinaryReader(this.GetArchivoStream(files_)))
                        {
                            string NombreArchivo = Path.Combine(carpeta, files_.FileName.ToString());
                            files_.SaveAs(NombreArchivo);
                            string SubcarpetaLF = entidad.NUM_DOCUMENTO + "\\" + "SUSTENTO";
                            nomArchivoSave = entidad.NUM_DOCUMENTO +"_"+ RandomString(5, true) + ".pdf";
                            IdLaserficheSUSTENTO = new LaserficheRepositorio().EnviarLaserficheSubCarpeta(NombreArchivo, "COORDINADORES", SubcarpetaLF, nomArchivoSave, entidad.NUM_DOCUMENTO, entidad.IP_PC);
                        }
                    }
                }
                entidad.ARCHIVO_DNI = IdLaserficheDNI; 
                entidad.ARCHIVO_SUSTENTO = IdLaserficheSUSTENTO;
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                var myobj = jsSerializer.Deserialize<List<Cls_Ent_Contacto>>(Request.Form["listaContacto"]);
                entidad.listaContacto = myobj;
            
                entidad.CONTRASENA =  Encriptar.Encriptar_Pass("VaLiDa" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString());
                PreguntaRspta = new CoordinadorRepositorio().InsSolicitudCoordinador(entidad);
                if (!PreguntaRspta.FLG_OK)
                {
                    itemRespuesta.message = "Ocurrio un error.";
                    itemRespuesta.success = false;
                    itemRespuesta.extra = entidad.DES_ERROR;
                }
                else
                {
                    bool validar_correo = EnviarEmailRegistroSolicitud(entidad);
                    itemRespuesta.extra = entidad.ID_ENTIDAD.ToString();
                    itemRespuesta.success = true;
                    if (validar_correo)
                    {
                        itemRespuesta.message = "Se ha registrado su solicitud de Usuario, en breves momentos se le enviará un correo del registro de solicitud para el acceso al sistema.";
                    }
                    else
                    {
                        itemRespuesta.message = "Se ha registrado su solicitud de Usuario, se presentó inconvenientes al realizar la notificación se volverá intentar en breves minutos.";
                    }
                }
            }
            catch (Exception ex)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailRegistroSolicitud(Cls_Ent_Coordinador entidad)
        {
            string mensaje = "";
            string titulo_correo = "";

            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";

            using (StreamReader sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "RegistrarCoordinador.html"))
            {
                mensaje = sr.ReadToEnd();
            }

            mensaje = mensaje.Replace("{0}", entidad.APELLIDO_PATERNO + " " + entidad.APELLIDO_MATERNO+ " " + entidad.NOMBRES);
            mensaje = mensaje.Replace("{1}", entidad.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", entidad.DESCRIPCION);
            mensaje = mensaje.Replace("{3}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            mensaje = mensaje.Replace("{4}", DateTime.Now.Year.ToString());
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
        public void LIMPIAR_COOKIS(string co)
        {
            HttpCookie myCookie = new HttpCookie(co);
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
        }
        public ActionResult ValidarUserPendiente(Cls_Ent_Coordinador entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            int cantidad = new CoordinadorRepositorio().ListaCoordinadores(entidad).FindAll(A => A.FLG_ESTADO==1 && A.FLG_SOLICITUD=="0" && A.NUM_DOCUMENTO==entidad.NUM_DOCUMENTO).Count();
            if (cantidad == 0)
            {
                itemRespuesta.success = true;
            }
            else
            {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }
        public ActionResult ValidarUserAprobado(Cls_Ent_Coordinador entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            int cantidad = new CoordinadorRepositorio().ListaCoordinadores(entidad).FindAll(A => A.FLG_ESTADO == 1 && A.FLG_SOLICITUD == "1" && A.NUM_DOCUMENTO == entidad.NUM_DOCUMENTO).Count();
           
            if (cantidad == 0)
            {
                itemRespuesta.success = true;
            }
            else
            {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }
        public ActionResult ValidarCaptcha_Login(string Captcha)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            var valor = Session["ClaveExtrametCapchaFagPagLogin"].ToString();
            if (Captcha == valor)
            {
                itemRespuesta.success = true;
            }
            else
            {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }
        public ActionResult UpdateContrasenaCoordinador(Cls_Ent_Coordinador entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Coordinador PreguntaRspta = null;
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
            if (entidad.ACCION == "CC")
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.NUM_DOCUMENTO;
            }
            else {
                entidad.CONTRASENA = Encriptar.Encriptar_Pass("VaLiDa" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString());
            }
            entidad.CONTRASENA = Encriptar.Encriptar_Pass(entidad.CONTRASENA);

            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new CoordinadorRepositorio().UpdateContrasenaCoordinador(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = entidad.DES_ERROR;
            }
            else
            {
                if (entidad.ACCION == "CC")
                {
                    itemRespuesta.success = true;
                    itemRespuesta.message = "La contraseña se actualizo correctamente";

                }
                else {
                    //bool validar_correo = EnviarEmailRegistroSolicitud(entidad);
                }
            }
            return Respuesta(itemRespuesta);
        }
        public ActionResult UpdateContrasenaPersonal(Cls_Ent_Personal entidad)
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
                var id_coordinador = int.Parse(Encriptar.Desencriptar_Pass(codigo.Value));
                if (new ReconectaUsuario().ReconectaUsuario_(id_coordinador))
                {
                    PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
                }
            }
            if (entidad.ACCION == "CC")
            {
                entidad.USU_INGRESO = UsuarioSistemaSesion.NUM_DOCUMENTO;
            }
            else
            {
                entidad.CONTRASENA = Encriptar.Encriptar_Pass("VaLiDa" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString());
            }
            entidad.CONTRASENA = Encriptar.Encriptar_Pass(entidad.CONTRASENA);

            entidad.IP_PC = Request.UserHostAddress.ToString().Trim();
            PreguntaRspta = new PersonalReposiorio().UpdateContrasenaPersonal(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = entidad.DES_ERROR;
            }
            else
            {
                if (entidad.ACCION == "CC")
                {
                    itemRespuesta.success = true;
                    itemRespuesta.message = "La contraseña se actualizo correctamente";

                }
                else
                {
                    //bool validar_correo = EnviarEmailRegistroSolicitud(entidad);
                }
            }
            return Respuesta(itemRespuesta);
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
        public ActionResult UpdateRecuperarClaveCoordinador(Cls_Ent_Coordinador entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Coordinador PreguntaRspta = null;
            entidad.CONTRASENA = Encriptar.Encriptar_Pass("VaLiDa" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString());
            
          //  entidad.CONTRASENA = Encriptar.Encriptar_Pass(entidad.CONTRASENA);
            PreguntaRspta = new CoordinadorRepositorio().UpdateRecuperarClave(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = entidad.DES_ERROR;
            }
            else
            {
                bool validar_correo = EnviarEmailRecuperarClave(entidad);
                if (validar_correo)
                {
                    itemRespuesta.success = true;
                    itemRespuesta.message = "Se realizo la notifición de las nuevas credenciales.";
                }
                else
                {
                    itemRespuesta.success = false;
                    itemRespuesta.message = "Vuelva a intentarlo en breves minutos";
                }
         
               

            }
            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailRecuperarClave(Cls_Ent_Coordinador entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
                if (new ReconectaUsuario().ReconectaUsuario_(entidad.ID_COORDINADOR))
                {
                    UsuarioSistemaSesion = (Cls_Ent_Coordinador)Session["Usuario"];
                }
            
            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";

            using (StreamReader sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "RestablecerClaveCoordinador.html"))
            {
                mensaje = sr.ReadToEnd();
            }

            mensaje = mensaje.Replace("{0}", UsuarioSistemaSesion.NOMBRES);
            mensaje = mensaje.Replace("{1}", UsuarioSistemaSesion.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", UsuarioSistemaSesion.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{3}", Encriptar.Desencriptar_Pass(entidad.CONTRASENA));
            mensaje = mensaje.Replace("{4}", ConfigurationManager.AppSettings["UrlAplicacion"].ToString());
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = UsuarioSistemaSesion.CORREO_NOTIFICADOR;

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
        public ActionResult restablecerClaveCoordinador()
        {
            return View();
        }
        public ActionResult ListaDetalleCoordinador(string Correo, string usuario)
        {
            Cls_Ent_Coordinador datos = new Cls_Ent_Coordinador();
            try
            {
                using (CoordinadorRepositorio proxy = new CoordinadorRepositorio())
                {
                    datos = proxy.ListaCoordinadores(datos).First(A=> A.CORREO_NOTIFICADOR==Correo && A.USUARIO == usuario && A.FLG_SOLICITUD=="1" && A.FLG_ESTADO==1);
                    datos.ACCION = "1";
                }
            }
            catch (Exception ex)
            {
                datos.ACCION = "0";
                datos.DES_ERROR = ex.ToString();

            }

            var jsonResult = Json(datos, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        public ActionResult UpdateRecuperarClavePersonal(Cls_Ent_Personal entidad)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            Cls_Ent_Personal PreguntaRspta = null;
            entidad.CONTRASENA = Encriptar.Encriptar_Pass("VaLiDa" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString());
            PreguntaRspta = new PersonalReposiorio().UpdateRestablecerContrasenaPersonal(entidad);
            if (!PreguntaRspta.FLG_OK)
            {
                itemRespuesta.message = "Ocurrio un error.";
                itemRespuesta.success = false;
                itemRespuesta.extra = entidad.DES_ERROR;
            }
            else
            {
                bool validar_correo = EnviarEmailRecuperarClavePersonal(entidad);
                if (validar_correo)
                {
                    itemRespuesta.success = true;
                    itemRespuesta.message = "Se realizo la notifición de las nuevas credenciales.";
                }
                else
                {
                    itemRespuesta.success = false;
                    itemRespuesta.message = "Vuelva a intentarlo en breves minutos";
                }



            }
            return Respuesta(itemRespuesta);
        }
        private bool EnviarEmailRecuperarClavePersonal(Cls_Ent_Personal entidad)
        {
            string mensaje = "";
            string titulo_correo = "";
            if (new ReconectaUsuario().ReconectaUsuario_Personal(entidad.ID_PERSONAL))
            {
                PersonalSistemaSesion = (Cls_Ent_Personal)Session["Usuario"];
            }

            titulo_correo = "SISTEMA INTEGRADO FAG-PAC";

            using (StreamReader sr = new StreamReader(Server.MapPath("~/Formato_Correos/") + "RestablecerClaveCoordinador.html"))
            {
                mensaje = sr.ReadToEnd();
            }

            mensaje = mensaje.Replace("{0}", PersonalSistemaSesion.NOMBRES);
            mensaje = mensaje.Replace("{1}", PersonalSistemaSesion.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{2}", PersonalSistemaSesion.NUM_DOCUMENTO);
            mensaje = mensaje.Replace("{3}", Encriptar.Desencriptar_Pass(entidad.CONTRASENA));
            mensaje = mensaje.Replace("{4}", ConfigurationManager.AppSettings["UrlAplicacion"].ToString());
            string titulo = titulo_correo;
            string strMsgError = "";
            string destinatarios = PersonalSistemaSesion.EMAIL;

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
        public ActionResult ListaDetallePersonal(string Correo)
        {
            Cls_Ent_Personal datos = new Cls_Ent_Personal();
            try
            {
                using (PersonalReposiorio proxy = new PersonalReposiorio())
                {
                    datos = proxy.ListaPersonal(datos).First(A => A.EMAIL == Correo);
                    datos.ACCION = "1";
                }
            }
            catch (Exception ex)
            {
                datos.ACCION = "0";
                datos.DES_ERROR = ex.ToString();

            }

            var jsonResult = Json(datos, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        public ActionResult restablecerClavePersonal()
        {
            return View();
        }

        public ActionResult restablecerClave()
        {
            return View();
        }
        public ActionResult ValidarFormatoPdfCorrecto()
        {
            ResponseEntity itemRespuesta = new ResponseEntity();

            byte[] data;
            try
            {
                var httpRequest = Request.Files;
                if (httpRequest.Count > 0)
                {
                    var files = Request.Files[0] as HttpPostedFileBase;
                    if (files.ContentLength > 0)
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            files.InputStream.CopyTo(memoryStream);
                            data = memoryStream.ToArray();
                            PdfReader reader = new PdfReader(data);
                            itemRespuesta.success = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                itemRespuesta.message = "El documento no es un archivo PDF.";
                itemRespuesta.success = false;
                itemRespuesta.extra = ex.ToString();
            }

            return Respuesta(itemRespuesta);
        }
        public ActionResult ValidarCaptchaDni(string Captcha)
        {
            ResponseEntity itemRespuesta = new ResponseEntity();
            var valor = Session["ClaveExtranetDni"].ToString();
            if (Captcha == valor)
            {
                itemRespuesta.success = true;
            }
            else
            {
                itemRespuesta.success = false;
            }
            return this.Respuesta(itemRespuesta);
        }
    }
}
