using MEF.PROYECTO.Entity;
using APP.ADMINISTRAR.FAG.PAG.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace APP.ADMINISTRAR.FAG.PAG.Core
{
    public class BaseController : Controller
    {
        #region Respuesta

        protected JsonResult Respuesta(JsonResult json)
        {
            json.ContentType = "text/plain";
            //TODO: Warning!!!, inline IF is not supported ?
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentEncoding = Encoding.UTF8;
            return json;
        }

        protected JsonResult ResponseImage(List<string> items, bool success = true, string extra = "")
        {
            JsonResult respuesta = Json(new
            {
                success = success,
                message = items,
                extra = extra
            });
            respuesta.ContentType = "text/plain";
            return respuesta;
        }


        protected JsonResult Respuesta(ResponseEntity item)
        {
            JsonResult json = base.Json(item);
            return this.Respuesta(json);
        }

        protected JsonResult Respuesta(string mensaje, bool success, string extra)
        {
            ResponseEntity respuestaJson = new ResponseEntity();
            respuestaJson.message = mensaje;
            respuestaJson.success = success;
            respuestaJson.extra = extra;
            return this.Respuesta(respuestaJson);
        }
        protected JsonResult Respuesta(Exception ex)
        {
            return this.Respuesta(ex.Message, false, string.Empty);
        }

        protected JsonResult Respuesta(List<string> items, bool success, string extra)
        {
            string mensaje = string.Empty;
            foreach (string item in items)
            {
                mensaje = (mensaje + (item + "<br />"));
            }
            return this.Respuesta(mensaje, success, extra);
        }
        #endregion

        public virtual JsonResult ListarMenuRecursivo(string IdOficina, int IdUsuario)
        {
            //ByVal entidad As OBJETOS
            try
            {
                //string html = string.Empty;
                //html += "<div class='navigation-toggler'>";
                //html += "<i class='clip-chevron-left' ></i>";
                //html += "<i class='clip-chevron-right'></i>";
                //html += "</div>";
                //ObjetoSistemaDTO entidad = new ObjetoSistemaDTO();

                //if ((entidad != null))
                //{
                //    entidad.ID_SIS = 37;
                //    //entidad.ID_USUARIO = IdUsuario; //Fernando Severino
                //    entidad.ID_OFICINA = IdOficina;
                //    entidad.ID_TIPO = 6;


                //}
                // return Json(html);
                return Json("");
            }
            catch (Exception ex)
            {
                return Json("Error" + ex.Message);

            }
        }

        public virtual JsonResult MostrarSelectorDetalleDocumento()
        {
            //ByVal entidad As OBJETOS
            try
            {
                string html = string.Empty;

                html += "<div id='style_selector_container' style='display: block'>";
                html += "   <div class='style-main-title'>";
                html += "       Detalle del Expediente N° ";
                html += "       <label id='lbDetalleNroExpediente' style='font-size: 18px; color: blue; font-weight: bold;'></label>";
                html += "   </div>";
                html += "   <div class='input-box' id='divDetalleDocumento'>";
                html += "       <div class='box-title'>";
                html += "           No existe detalle para mostrar.";
                html += "       </div>";
                html += "   </div>";
                html += "   <div style='height: 25px; line-height: 25px; text-align: center' id='divVerHojaTramite'>";
                html += "   </div>";
                html += "</div>";
                html += "<div class='style-toggle open' id='divSelectorDetalleDoc'></div>";

                return Json(html);
            }
            catch (Exception ex)
            {
                return Json("Error" + ex.Message);

            }
        }




        internal void GenerarLista(/*List</*ObjetoSistemaDTO> items,*/ ref string menu, int nivel)
        {
            //string cssUL = string.Empty;
            //string cssLI = string.Empty;
            //string vlInvocar = "javascript:void(0)";

            //if (nivel == 1)
            //{
            //    cssUL = "main-navigation-menu";
            //}
            //else
            //{
            //    cssUL = "sub-menu";
            //}

            //if (nivel == 1)
            //{
            //    string dashboard = " <li class='active open' id='liModSecretarial' ><a id='aModuloSecretarial' href='javascript:" + vlInvocar + ";'><i class='clip-home-3'></i><span class='title'> CONVCAS </span><span'class='selected'></span></a></li>";
            //    menu = menu + "<ul  class='" + cssUL + "'>" + dashboard + Convert.ToChar(13);
            //}
            //else
            //{
            //    menu = menu + "<ul  class='" + cssUL + "' >" + Convert.ToChar(13);
            //}


            //foreach (ObjetoSistemaDTO item in items)
            //{
            //    bool existe = true;
            //    string ls_style_li = "";
            //    if ((existe == true))
            //    {
            //        if (item.ID_TIPO == 2) ls_style_li = "background-color:white";
            //        menu += "<li class='" + cssLI + "' id='" + item.DES_URLLABEL + "' style='" + ls_style_li + "' > ";
            //        if (nivel == 1)
            //        {

            //            menu += "<a href='javascript:void(0);'>";
            //            menu += "<i class='" + item.IMAGEN + "'></i>";
            //            menu += "<span   class='title'> " + item.DES_OPCION + "</span><i class='icon-arrow'></i><span  class='selected'></span>";
            //            menu += "</a>";
            //        }
            //        else
            //        {
            //            menu += "<a  href='javascript:" + item.DES_FUNCION_INI + ";'id=" + item.DES_URL + "  >" + item.DES_OPCION + "</a>";
            //        }

            //        if (item.ID_TIPO == 1)
            //        {
            //            if (item.HIJOS.Count > 0)
            //            {
            //                GenerarLista(item.HIJOS, ref menu, 2);
            //            }
            //        }
            //        menu += "</li>" + Convert.ToChar(13);
            //    }

            //}
            //menu += "</ul>" + Convert.ToChar(13);
        }

    }

}
