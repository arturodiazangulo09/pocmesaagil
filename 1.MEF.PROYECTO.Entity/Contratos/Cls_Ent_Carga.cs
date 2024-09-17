using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.PROYECTO.Entity.Contratos
{
    public class Cls_Ent_Carga
    {
        public int ID_CARGA { get; set; }
        public DateTime FEC_REGISTRO { get; set; }
        public string TIPO_DOC { get; set; }
        public string TIPO_FORMATO { get; set; }
        public string USU_INGRESO { get; set; }
        public string USU_MODIFICA { get; set; }
        public DateTime FEC_MODIFICA { get; set; }
        public int NRO_REGISTRO { get; set; }
        public string USU_PROCESAR { get; set; }
        public DateTime FEC_PROCESAR { get; set; }

    }
}
