using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Renumeracion : Cls_Ent_Base
    {
        public int DIAS { get; set; }
        public decimal MONTO { get; set; }
        public string DES_MES { get; set; }
        public int ANIO { get; set; }
        public int NUM_MES { get; set; }
        public int ID_PAGO { get; set; }
        public int ID_SOLICITUD { get; set; }
        public int ID_ENTIDAD { get; set; }
        public string FLG_PAGO { get; set; }
        public string TIPO_PROCESO { get; set; }
        public string ID_MES { get; set; }

        public string PO_VALIDO { get; set; }
        public string PO_MENSAJE { get; set; }
        public List<Cls_Ent_Renumeracion> Lista { get; set; }
        public long ID_ARCHIVO_RECIBO { get; set; }
        public long ID_ARCHIVO_CPE { get; set; }
        public string NR_COMPROBANTE { get; set; }
        public DateTime FECHA_GENERACION { get; set; }
        public decimal IMPORTE_COMPROBANTE { get; set; }
        public string DESC_SERVICIO { get; set; }
        public string IDEDOCODIGO { get; set; }
        public long ID_CONFORMIDAD { get; set; }
        public long ID_ARCHIVO_INFORME { get; set; }
        public long ID_CONTRATO_DET { get; set; }
        public long ID_CONTRATO_ADENDA { get; set; }
        public string SERIE_COMPROBANTE { get; set; }
        public string FLG_CUARTA_CATEGORIA { get; set; }
        
    }

}
