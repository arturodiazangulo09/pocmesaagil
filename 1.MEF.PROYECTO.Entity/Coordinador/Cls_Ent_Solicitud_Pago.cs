using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Solicitud_Pago : Cls_Ent_Base
    {
        public int ID_PAGO { get; set; }
        public int ID_SOLICITUD { get; set; }
        public int ID_ENTIDAD { get; set; }
        public int ID_CONTRATO { get; set; }

        public int ANIO { get; set; }
        public int NUM_MES { get; set; }
        public decimal MONTO { get; set; }
        public long ID_ARCHIVO_RECIBO { get; set; }
        public long ID_ARCHIVO_CPE { get; set; }
        public long ID_ARCHIVO_CONTRATO { get; set; }
        public long ID_ARCHIVO_CONFORMIDAD { get; set; }
        public long ARCHIVO_TDR { get; set; }

        public Decimal PORCENTAJE { get; set; }
        public int ANIO_PROCESO { get; set; }
        public int NUM_PROCESO { get; set; }      
        public decimal IMPORTE_COMPROBANTE { get; set; }
        public string DES_MES { get; set; }
        public string FLG_PAGO { get; set; }
        public string TIPO_PROCESO { get; set; }
        public string NR_COMPROBANTE { get; set; }
        public string DESC_SERVICIO { get; set; }
        public string ENTIDAD { get; set; }
        public string NUM_DOCUMENTO { get; set; }
        public string CONSULTOR { get; set; }
        public string CODIGO_CONTRATO { get; set; }
        public string CODIGO_TDR { get; set; }
        public string IDEDOCODIGO { get; set; }
        public string DENOMINACION_PUESTO { get; set; }
        public DateTime FECHA_GENERACION { get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public long ID_CONFORMIDAD { get; set; }
        public long NR_MES { get; set; }
        public long ID_TRAMITE { get; set; }
        public string NR_TRAMITE { get; set; }
        public long CANT_OBSERVACIONES { get; set; }
        public long CANT_REEVALUACION_PAGO { get; set; }
        public long ID_ARCHIVO_INFORME { get; set; }
        public string NIVEL_CONFORMIDAD { get; set; }
        public int MES_PLANILLA { get; set; }
        public DateTime FECHA_PAGO { get; set; }
        
         public string NR_PLANILLA { get; set; }
        public string DESC_PROCESO { get; set; }
        public string RENUMERACION { get; set; }
        public string SERIE_COMPROBANTE { get; set; }
        public string FLG_CON_HR { get; set; }

    }
}
