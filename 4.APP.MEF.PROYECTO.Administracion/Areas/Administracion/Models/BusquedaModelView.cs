using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Models
{
    public class BusquedaModelView
    {
        public string ACCION { get; set; }
        public int ID { get; set; }
        public int ID_ENTIDAD { get; set; }
        public string TIPO_CONSULTOR { get; set; }
        public string FECHA { get; set; }
        public string FECHA_INICIO { get; set; }

        public string TIPO_DESCARGA { get; set; }
        public string ESTADO_SOLICITUD { get; set; }
        public string TIPO { get; set; }
        public string DOCUMENTO { get; set; }
        public string ANIO { get; set; }
        public string TIPO_REPORTE { get; set; }
        public string FECHA_FINAL { get; set; }
    }
}