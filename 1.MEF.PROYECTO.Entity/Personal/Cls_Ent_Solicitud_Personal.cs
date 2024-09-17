using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
using System.Web.Mvc;

namespace MEF.PROYECTO.Entity.Personal
{
  public  class Cls_Ent_Solicitud_Personal:Cls_Ent_Base
    {
        public int ID_SOLICITUD { get; set; }
        public DateTime FEC_REGI_SOLICITUD { get; set; }
        public string DENOMINACION_PUESTO { get; set; }
        public int ID_PERSONAL { get; set; }
        public string ENTIDAD { get; set; }
        public string FLG_ENVIO_PROPUESTA { get; set; }
        public decimal MONTO_RECIBO { get; set; }
        public string FLG_CHECK_4 { get; set; }
        public decimal MONTO_MENSUAL { get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public DateTime FECHA_LIM_REN { get; set; }
        public string DESC_PROCESO { get; set; }
        public string TIPO_PROCESO { get; set; }
        public string FLG_PENSIONISTA_ESTADO { get; set; }
        public string FLG_PENSIONISTA_POLICIA { get; set; }
        public string FLG_TERMINOS { get; set; }
        public string FLG_HISTORICO { get; set; }

        public string USER_COORDINADOR { get; set; }
        public DateTime FEC_ENVIO_PROPUESTA { get; set; }
        public int ID_ENTIDAD { get; set; }
        public string NUM_PROCESO { get; set; }
        public string ANIO_PROCESO { get; set; }
        public string IDEDOCODIGO { get; set; }
        public int CANT_OBSERVACION { get; set; }

        public string FLG_PAC_H_PROFESIONAL { get; set; }
        public string FLG_PAC_ANEXO2 { get; set; }
        public string FLG_FORMATOH { get; set; }
        public string FLG_FORMATOE { get; set; }
        public string FLG_FORMATOD { get; set; }
        public string FLG_FORMATOC { get; set; }
        public string FLG_FORMATOB { get; set; }
        public string FLG_FORMATOA { get; set; }
        public string FLG_DATOS_SECTOR { get; set; }
        public string FLG_INFORME_F { get; set; }
        public string FLG_OTROS { get; set; }
        public string FLG_H_PROFESIONAL { get; set; }
        public string FLG_BANCO { get; set; }
        public string FLG_ANEXO7 { get; set; }
        public string FLG_ANEXO5 { get; set; }
        public string FLG_ANEXO4 { get; set; }
        public string FLG_ANEXO3 { get; set; }
        public string FLG_ANEXO2 { get; set; }
        public string FLG_ANEXO1 { get; set; }
        public long ID_ANEXO1 { get; set; }
        public long ID_ANEXO2 { get; set; }
        public long ID_ANEXO3 { get; set; }
        public long ID_ANEXO4 { get; set; }
        public long ID_ANEXO5 { get; set; }
        public long ID_ANEXO7 { get; set; }
        public long ID_BANCO { get; set; }
        public long ID_H_PROFESIONAL { get; set; }
        public long ID_OTROS { get; set; }
        public long ID_INFORME_F { get; set; }
        public long ID_DATOS_SECTOR { get; set; }
        public long ID_FORMATOA { get; set; }
        public long ID_FORMATOB { get; set; }
        public long ID_FORMATOC { get; set; }
        public long ID_FORMATOD { get; set; }
        public long ID_FORMATOE { get; set; }
        public long ID_FORMATOH { get; set; }
        public long ID_PAC_ANEXO2 { get; set; }
        public int ID_PAC_H_PROFESIONAL { get; set; }
        public string FLG_CUMPLE_REQUISITOS_UTP { get; set; }
        public string FLG_CALIFICA_REQUISITOS_UTP { get; set; }
        public string NUM_DOCUMENTO { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public List<SelectListItem> ListaCalificacion { get; set; }
        public List<SelectListItem> ListaCumpleRequisitos { get; set; }
        public DateTime FECHA_ENVIO_UTP { get; set; }
        public DateTime FECHA_APROBADO_UTP { get; set; }

        public long ID_ARCHIVO_CONTRATO { get; set; }
        public int ID_CONTRATO { get; set; }
        public long ID_ARCHIVO { get; set; }
        public string NUM_CONTRATO { get; set; }
        public string ANIO_CONTRATO { get; set; }
        public long ARCHIVO_TDR { get; set; }
        public string ENTIDAD_PADRE { get; set; }
        public string NR_TRAMITE { get; set; }
        public string NUM_CONTRATO_TEXT { get; set; }
        public string FLG_PENSIONES { get; set; }


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
        public int COD_CONTRATO { get; set; }
        public int ARCHIVO_PUESTO_SUS_SOLICITUD { get; set; }
    }
}
