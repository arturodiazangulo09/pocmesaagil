using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Periodo_Pago_Entidad: Cls_Ent_Base
    {
        public int ID_ENTIDAD { get; set; }
        public int NUM_MES { get; set; }
        public int ANIO_PERIODO { get; set; }
        public string DES_MES { get; set; }
        public decimal MONTO { get; set; }
        public string DESC_ENTIDAD { get; set; }
        public string TIPO_PROCESO { get; set; }
        public string DESC_PROCESO { get; set; }
        public decimal MONTO_CONSUMIDO { get; set; }
        public int CANTIDAD_CONSULTORES { get; set; }
        public decimal MONTO_DISPONIBLE { get; set; }

    }
}
