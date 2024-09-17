using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
using System.Web.Mvc;

namespace MEF.PROYECTO.Entity.Administracion
{
    public class Cls_Datos_Mef: Cls_Ent_Base
    {
        public string DIRECCION { get; set; }
        public string DNI_AUTORIDAD { get; set; }
        public string APE_PATERNO { get; set; }
        public string APE_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string TIPO_RESOLUCION { get; set; }
        public string NUMERO_RESOLUCION { get; set; }
        public string RUC { get; set; }
        public string ENTIDAD { get; set; }
        public decimal ID_GENERAL { get; set; }
        public string ANIO { get; set; }
        public decimal PIA { get; set; }
        public decimal PIM { get; set; }
        public string ACCION { get; set; }




    }
}
