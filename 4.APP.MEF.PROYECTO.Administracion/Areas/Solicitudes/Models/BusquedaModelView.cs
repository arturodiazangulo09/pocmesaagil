using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes.Models
{
    public class BusquedaModelView
    {
        public string FLG_TIPO { get; set; }
        public string ACCION { get; set; }
        public int ID { get; set; }
        public int ID_PERSONAL { get; set; }
        public int ID_SOLICITUD { get; set; }
    }
}