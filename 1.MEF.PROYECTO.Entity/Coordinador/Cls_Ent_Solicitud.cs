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
    public class Cls_Ent_Solicitud : Cls_Ent_Base
    {
        public int ID_SOLICITUD { get; set; }     
        public string FECHA_REGISTRO { get; set; }
        public string TIPO_PROCESO { get; set; }
        public List<SelectListItem> Lista_Entidad { get; set; }
        public List<Cls_Ent_Funciones_Solicitud> listaFunciones { get; set; }
        public List<Cls_Ent_Experiencia> listaExperiencia { get; set; }
        public List<Cls_Ent_Curso> listaCurso { get; set; }
        public List<Cls_Ent_Academico> listaAcademico { get; set; }
        public List<Cls_Ent_Actividad> listaActividad { get; set; }
        public List<Cls_Ent_Actividad_Otro> listaAcividadOtro { get; set; }
        public List<Cls_Ent_Conocimientos> listaConocimiento { get; set; }
        public List<Cls_Ent_Renumeracion> listaRenumeracion { get; set; }
        public List<Cls_Ent_Requisitos_Solicitud> listaRequisitos { get; set; }
        public List<Cls_Ent_Documento> listaDocumentos { get; set; }
        public List<SelectListItem> ListaCalificacion { get; set; }
        public int ID_ENTIDAD { get; set; }
        public int ID_PERSONAL { get; set; }
        public string DESC_ENTIDAD { get; set; }
        public string CONTRASENA { get; set; }   
        public string DEPENDENCIA { get; set; }
        public string DESC_ACADEMICO { get; set; }
        public string DESC_CURSOS { get; set; }
        public int ANIO_GENERAL { get; set; }
        public int ANIO_ESPECIFICA { get; set; }
        public string DESC_ESPECIFICA { get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public decimal MONTO_MENSUAL { get; set; }
        public int ID_PUESTO { get; set; }
        public string DENOMINACION_PUESTO { get; set; }
        public string CONFORMIDAD_SERVICIO { get; set; }
        public string APE_NOMB_CERTIFICA { get; set; }
        public string NUM_DOCUMENTO_CONSULTOR { get; set; }
        public string APELLIDO_PAT_CONSULTOR { get; set; }
        public string APELLIDO_MAT_CONSULTOR { get; set; }
        public string NOMBRES_CONSULTOR { get; set; }
        public string CORREO_CONSULTOR { get; set; }
        public string FLG_DESIGNADO { get; set; }
        public string NR_RESOLUCION { get; set; }
        public DateTime FECHA_RESOLUCION { get; set; }
        public string ANIO_PROCESO { get; set; }
        public string NUM_PROCESO { get; set; }
        public string DESC_TIPO_FICHA { get; set; }
        public string FLG_NOTIFICA_CONSULOR { get; set; }
        public string OFICINA_CERTIFICA { get; set; }
        //public byte[] ARCHIVO_PUESTO_SUS_SOLICITUD { get; set; }
        public long ARCHIVO_PUESTO_SUS_SOLICITUD { get; set; }
        public string NOMBRE_ARCHIVO_SUS_SOLICITUD { get; set; }
        public long ARCHIVO_TDR { get; set; }
        //public byte[] ARCHIVO_TDR { get; set; }
        public string NOMBRE_ARCHIVO_TDR { get; set; }
        public string FLG_PROPUESTA_CONSULOR { get; set; }
        public string FLG_CUMPLE_REQUISITOS { get; set; }
        public List<SelectListItem> ListaCumpleRequisitos { get; set; }
        public string FLG_CALIFICA_REQUISITOS { get; set; }
        public string FLG_NOTIFICA_UTP { get; set; }
        public string IDEDOCODIGO { get; set; }  
        public int CANT_OBSERVACION { get; set; }
        public int ID_ANEXO1 { get; set; }
        public int ID_ANEXO2 { get; set; }
        public int ID_ANEXO3 { get; set; }
        public int ID_ANEXO4 { get; set; }
        public int ID_ANEXO5 { get; set; }
        public int ID_ANEXO7 { get; set; }
        public int ID_BANCO { get; set; }
        public int ID_H_PROFESIONAL { get; set; }
        public int ID_OTROS { get; set; }
        public int ID_INFORME_F { get; set; }
        public int ID_DATOS_SECTOR { get; set; }
        public int ID_FORMATOA { get; set; }
        public int ID_FORMATOB { get; set; }
        public int ID_FORMATOC { get; set; }
        public int ID_FORMATOD { get; set; }
        public int ID_FORMATOE { get; set; }
        public int ID_FORMATOH { get; set; }
        public int ID_PAC_ANEXO2 { get; set; }
        public int ID_PAC_H_PROFESIONAL { get; set; }
        public long ID_TRAMITE { get; set; }
        public string NR_TRAMITE { get; set; }
        public DateTime FECHA_ENVIO_UTP { get; set; }
        public DateTime FECHA_APROBADO_UTP { get; set; }
        public string FLG_CUMPLE_REQUISITOS_UTP { get; set; }
        public string FLG_CALIFICA_REQUISITOS_UTP { get; set; }
        public long ID_ARCHIVO_CONTRATO { get; set; }
        public string RUC_ENTIDAD { get; set; }
        public string NUM_CONTRATO { get; set; }
        public string FLG_CON_HR { get; set; }
        public string DESC_SOLICITUD { get; set; }
        public string DESC_ORGANO { get; set; }
        public string FLG_VERSION { get; set; }
        public int CANT_DOC { get; set; }
        public string FLG_NIVEL { get; set; }
        public decimal MONTO_MENSUAL_CEU { get; set; }
        public string FLG_CEU { get; set; }
        public string FORMATO { get; set; }
        public string PASO_COORDINADOR { get; set; }
        
    }
}
