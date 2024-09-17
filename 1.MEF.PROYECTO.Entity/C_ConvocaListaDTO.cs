using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.OGA.ConvCAS.Entidad
{
    public class C_ConvocaListaDTO : Base.BaseDTO
    {
        public Int32 ID_CONVOCATORIA { get; set; }
        public DateTime FEC_CONVOCATORIA { get; set; }
        public String COD_PROCESO { get; set; }
        public String MOFIDESCRI { get; set; }
        public String EDODESCRIP { get; set; }
        public String DESCRIPCION_SERVICIO { get; set; }

        public String FLG_ESTADO { get; set; }
        public String USU_INGRESO { get; set; }
        public String FEC_INGRESO { get; set; }
        public String IP_INGRESO { get; set; }
        public String USU_MODIFICA { get; set; }
        public String FEC_MODIFICA { get; set; }
        public String IP_MODIFICA { get; set; }
    }
}
