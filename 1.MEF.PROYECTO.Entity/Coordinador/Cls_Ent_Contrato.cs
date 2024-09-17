using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;


namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Contrato : Cls_Ent_Base
    {
        public int ID_CONTRATO { get; set; }
        public int NUM_CONTRATO { get; set; }
        public string ANIO_CONTRATO { get; set; }
        public string CODIGO_CONTRATO { get; set; }
        public string TIPO_PROCESO { get; set; }
        public int ID_ENTIDAD { get; set; }
        public decimal MONTO_MENSUAL { get; set; }
        public string DENOMINACION_PUESTO { get; set; }
        public string FECHA_INICIO { get; set; }
        public string FECHA_FIN { get; set; }
        public string DESC_ENTIDAD { get; set; }
        public string CONSULTOR { get; set; }
        public string DOCUMENTO_CONSULTOR { get; set; }
        public string EMAIL_CONSULTOR { get; set; }
        public string COORDINADOR { get; set; }
        public string EMAIL_COORDINADOR { get; set; }
        public int ID_SOLICITUD { get; set; }
        
    }

    public class Cls_Ent_Contrato_Ren
    {
        public string CODIGO_CONTRATO { get; set; }
        public string DENOMINACION_PUESTO { get; set; }
        public string CONSULTOR { get; set; }
        public string DOCUMENTO_CONSULTOR { get; set; } 
        public string COORDINADOR { get; set; }
        public string FECHA_LIM_REN { get; set; }



    }
}
