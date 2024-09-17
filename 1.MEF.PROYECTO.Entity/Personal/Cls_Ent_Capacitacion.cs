using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
using System.Web;
using System.Web.Mvc;
namespace MEF.PROYECTO.Entity.Personal
{
    public class Cls_Ent_Capacitacion: Cls_Ent_Base
    {
        public int ID_CAPACITACION { get; set; }
        public DateTime FEC_INICIO_CAPACITACION { get; set; }
        public DateTime FEC_FIN_CAPACITACION { get; set; }
        public int ID_PERSONAL { get; set; }
        public string DESC_CENTRO_ESTUDIOS { get; set; }
        public string NOMBRE_CAPACITACION { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public int ARCHIVO { get; set; }
        public string NOMBRE_CIUDAD_PAIS { get; set; }
        public int ID_SOLICITUD { get; set; }
        public string APLICA { get; set; }
        public string VERIFICAR_ENTIDAD { get; set; }
        public string VERIFICACION_CORRECTA { get; set; }
        public string OBSERVACION_VERIFICACION { get; set; }
    }
}
