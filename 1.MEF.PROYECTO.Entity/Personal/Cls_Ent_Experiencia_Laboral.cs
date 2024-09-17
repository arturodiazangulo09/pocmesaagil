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
    public class Cls_Ent_Experiencia_Laboral: Cls_Ent_Base
    {
        public int ID_EXPERIENCIA { get; set; }
        public int ID_TIPO_EXPERIENCIA { get; set; }
        public string NOMBRE_ENTIDAD { get; set; }
        public string CARGO_EMPRESA { get; set; }
        public DateTime FEC_INICIO_EXPERIENCIA { get; set; }
        public DateTime FEC_FIN_EXPERIENCIA { get; set; }
        public int NUM_ANIOS { get; set; }
        public int NUM_MESES { get; set; }
        public int NUM_DIAS { get; set; }
        public string FUNCIONES { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public int ARCHIVO { get; set; }
        public int ID_PERSONAL { get; set; }
        public List<SelectListItem> ListaTipoExpeiencia { get; set; }
        public string DESC_TIPO_EXPERIENCIA { get; set; }

        public int ID_SOLICITUD { get; set; }
        public string APLICA { get; set; }
        public string VERIFICAR_ENTIDAD { get; set; }
        public string VERIFICACION_CORRECTA { get; set; }
        public string OBSERVACION_VERIFICACION { get; set; }
        public string SECTOR { get; set; }
        public string ANOS { get; set; }

        public int NUM_ANIOS_TOTAL { get; set; }
        public int NUM_MESES_TOTAL { get; set; }
        public int NUM_DIAS_TOTAL { get; set; }

        public int NUM_ANIOS_TOTAL_ES { get; set; }
        public int NUM_MESES_TOTAL_ES { get; set; }
        public int NUM_DIAS_TOTAL_ES { get; set; }
    }
}
