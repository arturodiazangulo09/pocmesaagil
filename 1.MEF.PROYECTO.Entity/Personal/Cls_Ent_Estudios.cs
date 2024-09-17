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
    public class Cls_Ent_Estudios: Cls_Ent_Base
    {
        public int ID_FORMAC_ACADEMICA { get; set; }
        public DateTime FEC_INICIO_FORMAC_ACADEMICA { get; set; }
        public DateTime FEC_FIN_FORMAC_ACADEMICA { get; set; }
        public DateTime FEC_EMISION_FORMAC_ACADEMICA { get; set; }
        public string DESC_ACADEMICA { get; set; }
        public int ID_NIVEL_ACADEMICO { get; set; }
        public List<SelectListItem> ListaNivel { get; set; }
        public int ID_NIVEL_GRADO { get; set; }
        public List<SelectListItem> ListaGrado { get; set; }
        public string DESC_CENTRO_ESTUDIOS { get; set; }
        public string DESC_CIUDAD_PAIS { get; set; }
        public int ID_PERSONAL { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public int ARCHIVO { get; set; }
        public string APLICA { get; set; }
        public string DESC_NIVEL_ACADEMICO { get; set; }
        public string NOMBRE_NIVEL { get; set; }
        public int ID_SOLICITUD { get; set; }
        public string VERIFICAR_ENTIDAD { get; set; }
        public string VERIFICACION_CORRECTA { get; set; }
        public string OBSERVACION_VERIFICACION { get; set; }
    }
}
