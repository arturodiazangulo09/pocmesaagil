using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.PROYECTO.Entity.CargaMasiva
{
    public class Cls_Ent_Carga_Detalle
    {
        public string ID_CARGA_DETALLE { get; set; }
        public int ID_CARGA { get; set; }
        public string COD_CONTRATO { get; set; }
        public string DOC_SOLICITUD { get; set; }
        public string HOJA_RUTA { get; set; }
        public string FEC_SUSCRIPCION { get; set; }
        public string NUMERO { get; set; }
        public string FEC_INICIO { get; set; }
        public string FEC_FIN { get; set; }
        public string FEC_RECEPCION { get; set; }
        public string DOCUMENTO { get; set; }
        public bool Result { get; set; }

    }
}
