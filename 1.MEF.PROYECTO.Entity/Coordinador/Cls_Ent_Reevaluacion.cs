using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Coordinador
{
   public  class Cls_Ent_Reevaluacion : Cls_Ent_Base
    {
        public int ID_REEVALUACION { get; set; }
        public int ID_SOLICITUD { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string DES_REEVALUACION { get; set; }
        public string TIPO { get; set; }
        public int ID_CONFORMIDAD { get; set; }
        public int NR_MES { get; set; }
        public int ID_CONTRATO_DET { get; set; }
        public int ID_SUSPENSION { get; set; }
        public int ID_COVID { get; set; }
        public int COD_CONTRATO { get; set; }
        public bool AIRSH { get; set; }


    }
}
