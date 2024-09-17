using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
using System.Web.Mvc;

namespace MEF.PROYECTO.Entity.Administracion
{
    public class Cls_Ent_Planilla_PDT: Cls_Ent_Base
    {
        public string FIJO1 { get; set; }
        public string RUC { get; set; }
        public string FIJO2 { get; set; }
        public string SERIE_COMPROBANTE { get; set; }
        public string NR_COMPROBANTE { get; set; }
        public Decimal IMPORTE_COMPROBANTE { get; set; }
        public Decimal MONTO { get; set; }
        
        public string FECHA_EMISION { get; set; }
        public string FECHA_PAGO { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string FIJO3 { get; set; }
        public string FIJO4 { get; set; }
        public string TIPO_PROCESO { get; set; }
        public string ANIO { get; set; }
        public string MES { get; set; }
        public string CONSULTOR { get; set; }
        public string NUM_DOCUMENTO { get; set; }
        public string ENTIDAD { get; set; }
        public int ID_PAGO { get; set; }
        public string FIJO5 { get; set; }
        public string PERIODO_PLANILLA { get; set; }
        public string ANIO_PLANILLA { get; set; }
        public string CODIGO_PLANILLA { get; set; }
        public int ID_CONFORMIDAD { get; set; }
        public int NUM_MES { get; set; }
        public string NR_PLANILLA { get; set; }
    }
}
