using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Personal
{
    public class Cls_Ent_Covid: Cls_Ent_Base
    {
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public int ID_COVID { get; set; }
        public int ID_CONTRATO { get; set; }
        
        //Archivo Solicitud Firmada
        public int ID_ARCHIVO_U { get; set; }
        public int ID_ARCHIVO_C { get; set; }

        //Archivo Certificado Médico
        public int ID_ARCHIVO_CM { get; set; }
        public DateTime FECHA_SOLICITUD { get; set; }
        public DateTime FECHA_APRUEBA { get; set; }
        public DateTime FECHA_ENVIO_UTP { get; set; }
        public string IDEDOCODIGO { get; set; }
        public string CODIGO_CONTRATO { get; set; }
        public string NUMERO_MESES { get; set; }
        public string TIPO_PROCESO { get; set; }
        public int ANIO { get; set; }
        public int NUM_CONTRATO { get; set; }
        public int ID_ENTIDAD { get; set; }
        public string NUM_DOCUMENTO { get; set; }
        public string CONSULTOR { get; set; }
        public string DENOMINACION_PUESTO { get; set; }
        public string CORREO_CONSULTOR { get; set; }
        public int ID_TRAMITE { get; set; }
        public string NR_TRAMITE { get; set; }
        public string CORREO_COORDINADOR { get; set; }
        public string COORDINADOR { get; set; }
        public string DESC_ENTIDAD { get; set; }
        public DateTime FECHA_INICIO_CONTRATO { get; set; }
        public string FECHA_FIN_CONTRATO { get; set; }
        public int NUM_DIAS { get; set; }
        public string FLG_CON_HR { get; set; }

    }
}
