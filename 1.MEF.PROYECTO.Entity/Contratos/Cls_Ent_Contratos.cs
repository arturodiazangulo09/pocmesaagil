using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MEF.PROYECTO.Entity.Contratos
{
    public class Cls_Ent_Contratos
    {
        public int ID_REGISTRO { get; set; }
        public string TIPO_DOC { get; set; }
        public int ID_CARGA { get; set; }
        public string COD_CONTRATO { get; set; }
        public string ALERTA_LA { get; set; }
        public string ALERTA_LA_PERSONA { get; set; }
        public DateTime FEC_REGISTRO { get; set; }
        public string MODALIDAD { get; set; }
        public string ESTADO { get; set; }
        public string NRO_EXP_SISPER { get; set; }
        public string LEY31419 { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string DNI { get; set; }
        public string RUC { get; set; }
        public string SEXO { get; set; }
        public DateTime FEC_NACIMIENTO { get; set; }
        public int EDAD { get; set; }
        public string NACIONALIDAD { get; set; }
        public string PADRE { get; set; }
        public string TELEFONO_CELULAR { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
        public string DIRECCION { get; set; }
        public string DISTRITO { get; set; }
        public string PROVINCIA { get; set; }
        public string REGION { get; set; }
        public string NIVEL_GOBIERNO { get; set; }
        public int CODIGO_ENTIDAD { get; set; }
        public string TIPO_DOC_SOLICITA_REG { get; set; }
        public DateTime FEC_DOC_SOLICITA_REG { get; set; }
        public string HR_DOC_SOLICITA_REG { get; set; }
        public string DOC_RES_MINISTERIAL { get; set; }
        public string ENTIDAD_BENEF { get; set; }
        public string FLAG_DESIGNADO { get; set; }
        public string ALTA_DIRECCION { get; set; }
        public string DEPENDENCIA_PRESTACION { get; set; }
        public string CARGO_ANEXO1 { get; set; }
        public string CARGO_FUNCIONARIO_SUSCRIBE { get; set; }
        public decimal IMPORTE_HONORARIO { get; set; }
        public DateTime FEC_SUSCRIPCION { get; set; }
        public DateTime FEC_INICIO { get; set; }
        public DateTime FEC_CULMINACION { get; set; }
        public DateTime FEC_RECEPCION { get; set; }
        public string NRO_OFICIO { get; set; }
        public DateTime FIN_CONTRATO { get; set; }
        public string EXP_REMITIDO { get; set; }
        public string ADENDA_VIG_DOC_SOL { get; set; }
        public string ADENDA_VIG_NRO { get; set; }
        public DateTime ADENDA_VIG_FEC_INICIO { get; set; }
        public DateTime ADENDA_VIG_FEC_FIN { get; set; }
        public string ADENDA_VIG_HOJARUTA { get; set; }
        public DateTime ADENDA_VIG_SUSCRIPCION { get; set; }
        public DateTime NOT_FEC_RECEPCION { get; set; }
        public string NOT_DOCUMENTO { get; set; }
        public DateTime FEC_CULMINACION_CONTRATO { get; set; }
        public int GRADO_ACADEMICO_GEN { get; set; }
        public int GRADO_ACADEMICO_FAG { get; set; }
        public string CARRERA_PROFESIONAL { get; set; }
        public string UNIVERSIDAD { get; set; }
        public string FLAG_REG_SUNEDU { get; set; }
        public int GRADO_ACADEMICO_ESP { get; set; }
        public string REQ_HAB_PROF { get; set; }
        public int EXP_LAB_GENERAL { get; set; }
        public int EXP_LAB_ESPECIFICA { get; set; }
        public int EXP_LAB_MATERIA { get; set; }
        public int EXP_LAB_GENERAL_GRADO { get; set; }
        public int EXP_LAB_ACT_TDRS { get; set; }
        public DateTime RES_CONTRATO_FEC_CULM { get; set; }
        public DateTime RES_CONTRATO_DOC { get; set; }
        public string RES_CONTRATO_HR { get; set; }
        public string SEC_RESPONSABLE { get; set; }
        public int FISCAL_POST { get; set; }
        public string ENT_FIN_NOMBRE { get; set; }
        public string ENT_FIN_CTA_AHORRO { get; set; }
        public string ENT_FIN_CTA_BANCARIA { get; set; }
        public string ENT_FIN_CTA_CCI { get; set; }
        public string PJE_PUESTO { get; set; }
        public string FLG_ESTADO { get; set; }

        public List<SelectListItem> ListaDpto { get; set; }
    }
}
