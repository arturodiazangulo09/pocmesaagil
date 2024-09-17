using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Lista_Contr_Aden_Consultor: Cls_Ent_Base
    {
        public int ID_CONTRATO { get; set; }
        public int ID_ENTIDAD { get; set; }
        public int ID_ACADEMICA { get; set; }
        public int ANIO { get; set; }
        public string TIPO_PROCESO { get; set; }
        public DateTime FECHA_ENVIO_UTP { get; set; }
        public string NUM_DOCUMENTO { get; set; }
        public string CONSULTOR { get; set; }
        public string CODIGO_CONTRATO { get; set; }
        public string COD_ADENDA { get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public decimal MONTO_MENSUAL { get; set; }
        public DateTime FECHA_BAJA { get; set; }
        public string IDEDOCODIGO { get; set; }
        public string TIPO { get; set; }
        public int CANT_PAGOS { get; set; }
        public int ID_ARCHIVO_A_B { get; set; }
        public int ANIO_FIN { get; set; }
        public int MES_FIN { get; set; }
        public string DENOMINACION_PUESTO { get; set; }
        public string NR_SOLICITUD_SIGLA { get; set; }
        

    }
}
