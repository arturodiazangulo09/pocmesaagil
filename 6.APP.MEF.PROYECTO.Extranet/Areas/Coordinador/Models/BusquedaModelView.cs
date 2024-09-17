using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Models
{
    public class BusquedaModelView
    {
        
       public string FLG_TIPO { get; set; }
        public string ACCION { get; set; }
        public int ID { get; set; }
        public int ID_PERSONAL { get; set; }
        public int ID_SOLICITUD { get; set; }
        public int NR_MES { get; set; }
        public int ID_ENTIDAD { get; set; }
        public int ID_ARCHIVO { get; set; }
        public int ANIO { get; set; }
        
    }
}