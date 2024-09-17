using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
using MEF.PROYECTO.Entity.Administracion;
using System.Web.Mvc;

namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Adenda: Cls_Ent_Base
    {
        public int ID_ADENDA { get; set; }
        public int ID_CONTRATO_DET { get; set; }
        public string CODIGOS_CONTRATO_DET { get; set; }
        public int ID_CONTRATO { get; set; }
        public int ID_ENTIDAD { get; set; }
        public string TIPO_PROCESO { get; set; }
        public string TIPO { get; set; }
        public int ID_COORDINADOR { get; set; }
        public DateTime FECHA_INI { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public decimal MONTO { get; set; }
        public string NOMBRE_PUESTO { get; set; }
        public int ID_ARCHIVO { get; set; }

        public int ID_ARCHIVO_RNSDD { get; set; }
        public int ID_ARCHIVO_REDAM { get; set; }
        public int ID_ARCHIVO_REDERECI { get; set; }
        public int ID_ARCHIVO_AIRSHP { get; set; }
        public int ID_ARCHIVO_OTROS { get; set; }
        public long ARCHIVO_RNSDD { get; set; }
        public long ARCHIVO_REDAM { get; set; }
        public long ARCHIVO_REDERECI { get; set; }
        public long ARCHIVO_OTROS { get; set; }
        public long ARCHIVO_AIRSHP { get; set; }

        public int ID_SOLICITUD { get; set; }
        public List<Cls_Ent_Renumeracion> listaRenumeracion { get; set; }
        public int ID_ARCHIVO_SUSTENTO { get; set; }
        public int NUM_PROCESO { get; set; }
        public int ANIO_PROCESO { get; set; }
        public string CODIGO_CONTRATO { get; set; }
        public string CODIGO_ADENDA { get; set; }
        public string DESC_ENTIDAD { get; set; }
        public string CONSULTOR { get; set; }
        public string DOCUMENTO_CONSULTOR { get; set; }
        public string EMAIL_CONSULTOR { get; set; }
        public string COORDINADOR { get; set; }
        public string EMAIL_COORDINADOR { get; set; }
        public string IDEDOCODIGO { get; set; }
        public int ID_ARCHIVO_CONTRATO { get; set; }
        public string NR_TRAMITE { get; set; }
        public int ID_TRAMITE { get; set; }
        public string FLG_CON_HR { get; set; }
        
    }

    public class Cls_Ent_Adenda_data 
    {
        public int ID_CONTRATO_DET { get; set; }
        public int ID_TRAMITE { get; set; }

    }
}
