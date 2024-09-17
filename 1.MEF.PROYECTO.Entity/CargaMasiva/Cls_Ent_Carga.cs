using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.PROYECTO.Entity.CargaMasiva
{
    public class Cls_Ent_Carga
    {
        public int ID_CARGA { get; set; }
        public DateTime FEC_REGISTRO { get; set; }
        public string FECHA_REGISTRO { get; set; }

        public string TIPO_DOC { get; set; }
        public string TIPO_FORMATO { get; set; }
        public string USU_INGRESO { get; set; }
        public string USU_MODIFICA { get; set; }
        public DateTime FEC_MODIFICA { get; set; }
        public string FLG_ESTADO { get; set; }
        public string FLG_PROCESADO { get; set; }
        public string DES_CARGA { get; set; }
        public string ESTADO { get; set; }
        public int NRO_REGISTROS { get; set; }
        public bool Result { get; set; }

        public List<Cls_Ent_Carga_Cabecera> CARGA_CABECERA { get; set; } = new List<Cls_Ent_Carga_Cabecera>();
        public List<Cls_Ent_Carga_Detalle> CARGA_DETALLE { get; set; } = new List<Cls_Ent_Carga_Detalle>();
        public List<Cls_Ent_Carga_Pago> CARGA_SALARIO { get; set; } = new List<Cls_Ent_Carga_Pago>();
    }
}
