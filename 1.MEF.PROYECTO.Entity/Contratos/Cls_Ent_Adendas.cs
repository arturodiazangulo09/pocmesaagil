using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.PROYECTO.Entity.Contratos
{
    public class Cls_Ent_Adendas
    {
        public int ID_CARGA_ADENDA { get; set; }
        public int ID_CARGA { get; set; }
        public string COD_CONTRATO { get; set; }
        public string DOC_SOLICITUD { get; set; }
        public string HOJA_RUTA { get; set; }
        public DateTime FEC_SUSCRIPCION { get; set; }
        public int NUMERO { get; set; }
        public DateTime FEC_INICIO { get; set; }
        public DateTime FEC_FIN { get; set; }
        public DateTime FEC_RECEPCION { get; set; }
        public string DOCUMENTO { get; set; }
        public string FLG_ESTADO { get; set; }
    }
}
