using MEF.PROYECTO.Entity.Contratos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.PROYECTO.Utilitario
{
    public class ContratosUtil
    {
        public DataTable CrearDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("COLUMNA", typeof(string));
            dt.Columns.Add("DESCRIPCION", typeof(string));
            dt.Columns.Add("ATRIBUTO", typeof(string));
            
            return dt;
        }
        #region Campos
        public DataTable ListaCarga()
        {
            var dtCarga = CrearDataTable();

            //dtCarga.Rows.Add(NuevaFila(dtCarga, 1, "ID_CARGA", "Id Carga", "IdCarga"));
            dtCarga.Rows.Add(NuevaFila(dtCarga, 2, "FEC_REGISTRO", "Fecha Registro", "FechaRegistro"));
            dtCarga.Rows.Add(NuevaFila(dtCarga, 3, "TIPO_DOC", "Tipo Documento", "TipoDocumento"));

            return dtCarga;
        }

        public DataTable ListaContrato()
        {
            var dtContratos = CrearDataTable();

            //dtContratos.Rows.Add(NuevaFila(dtContratos, 2, "ID_CARGA", "Id Carga"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 3, "COD_CONTRATO", "Código Contrato", "COD_CONTRATO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 4, "ALERTA_LA", "Alerta LA", "ALERTA_LA"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 5, "ALERTA_LA_PERSONA", "Alerta LA Persona", "ALERTA_LA_PERSONA"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 6, "FEC_REGISTRO", "Fecha de Registro", "FEC_REGISTRO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 7, "MODALIDAD", "Modalidad", "MODALIDAD"));
            //FAG = Activo, Baja, Devolución  PAC = Activo, Inactivo, Devolución
            dtContratos.Rows.Add(NuevaFila(dtContratos, 8, "ESTADO", "Estado", "ESTADO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 9, "NRO_EXP_SISPER", "Nro. Exp. SISPER", "NRO_EXP_SISPER"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 10, "LEY31419", "Ley 31419", "LEY31419"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 11, "APELLIDO_PATERNO", "Apellido Paterno", "APELLIDO_PATERNO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 12, "APELLIDO_MATERNO", "Apellido Materno", "APELLIDO_MATERNO"));

            dtContratos.Rows.Add(NuevaFila(dtContratos, 13, "NOMBRES", "Nombres", "NOMBRES"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 14, "DNI", "Dni", "DNI"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 15, "RUC", "Ruc", "RUC"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 16, "SEXO", "Sexo", "SEXO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 17, "FEC_NACIMIENTO", "Fecha de Nacimiento", "FEC_NACIMIENTO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 18, "EDAD", "Edad", "EDAD"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 19, "NACIONALIDAD", "Nacionalidad", "NACIONALIDAD"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 20, "PADRE", "Padre", "PADRE"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 21, "TELEFONO_CELULAR", "Telefono Celular", "TELEFONO_CELULAR"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 22, "CORREO_ELECTRONICO", "Correo Electrónico", "CORREO_ELECTRONICO"));

            dtContratos.Rows.Add(NuevaFila(dtContratos, 23, "DIRECCION", "Dirección", "DIRECCION"));
            //dtContratos.Rows.Add(NuevaFila(dtContratos, 24, "DISTRITO", "Distrito", "DISTRITO"));
            //dtContratos.Rows.Add(NuevaFila(dtContratos, 25, "PROVINCIA", "Provincia", "PROVINCIA"));
            //dtContratos.Rows.Add(NuevaFila(dtContratos, 26, "REGION", "Región", "REGION"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 27, "NIVEL_GOBIERNO", "Nivel de Gobierno", "NIVEL_GOBIERNO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 28, "CODIGO_ENTIDAD", "Entidad", "CODIGO_ENTIDAD"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 29, "TIPO_DOC_SOLICITA_REG", "Tipo Doc. Sol. Reg.", "TIPO_DOC_SOLICITA_REG"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 30, "FEC_DOC_SOLICITA_REG", "Fecha Doc. Sol. Reg.", "FEC_DOC_SOLICITA_REG"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 31, "HR_DOC_SOLICITA_REG", "Hoja Ruta Doc. Sol. Reg.", "HR_DOC_SOLICITA_REG"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 32, "DOC_RES_MINISTERIAL", "Doc. Res. Ministerial", "DOC_RES_MINISTERIAL"));

            dtContratos.Rows.Add(NuevaFila(dtContratos, 33, "ENTIDAD_BENEF", "Entidad Benef.", "ENTIDAD_BENEF"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 34, "FLAG_DESIGNADO", "Flag Designado", "FLAG_DESIGNADO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 35, "ALTA_DIRECCION", "Alta Dirección", "ALTA_DIRECCION"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 36, "DEPENDENCIA_PRESTACION", "Dependencia Prestación", "DEPENDENCIA_PRESTACION"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 37, "CARGO_ANEXO1", "Cargo Anexo Nro. 1", "CARGO_ANEXO1"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 38, "CARGO_FUNCIONARIO_SUSCRIBE", "Cargo del Funcionario que Suscribe", "CARGO_FUNCIONARIO_SUSCRIBE"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 39, "IMPORTE_HONORARIO", "Importe Honorario", "IMPORTE_HONORARIO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 40, "FEC_SUSCRIPCION", "Fecha Suscripción", "FEC_SUSCRIPCION"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 41, "FEC_INICIO", "Fecha de Inicio", "FEC_INICIO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 42, "FEC_CULMINACION", "Fecha Culminación", "FEC_CULMINACION"));

            dtContratos.Rows.Add(NuevaFila(dtContratos, 43, "FEC_RECEPCION", "Fecha de Recepción", "FEC_RECEPCION"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 44, "NRO_OFICIO", "Nro. Oficio", "NRO_OFICIO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 45, "FIN_CONTRATO", "Fin de Contrato", "FIN_CONTRATO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 46, "EXP_REMITIDO", "Exp. Remitido", "EXP_REMITIDO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 47, "ADENDA_VIG_DOC_SOL", "Adenda Vigente Doc. Solicitud", "ADENDA_VIG_DOC_SOL"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 48, "ADENDA_VIG_NRO", "Adenda Vigente Número", "ADENDA_VIG_NRO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 49, "ADENDA_VIG_FEC_INICIO", "Adenda Vigente Fecha de Inicio", "ADENDA_VIG_FEC_INICIO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 50, "ADENDA_VIG_FEC_FIN", "Adenda Vigente Fecha Fin", "ADENDA_VIG_FEC_FIN"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 51, "ADENDA_VIG_HOJARUTA", "Adenda Vigente Hoja de Ruta", "ADENDA_VIG_HOJARUTA"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 52, "ADENDA_VIG_SUSCRIPCION", "Adenda Vigente Suscripción", "ADENDA_VIG_SUSCRIPCION"));
            
            dtContratos.Rows.Add(NuevaFila(dtContratos, 53, "NOT_FEC_RECEPCION", "Not. Fecha Recepción", "NOT_FEC_RECEPCION"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 54, "NOT_DOCUMENTO", "Not. Documento", "NOT_DOCUMENTO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 55, "FEC_CULMINACION_CONTRATO", "Fecha Culminación Contrato", "FEC_CULMINACION_CONTRATO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 56, "GRADO_ACADEMICO_GEN", "Grado Académico General", "GRADO_ACADEMICO_GEN"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 57, "GRADO_ACADEMICO_FAG", "Grado Académico FAG", "GRADO_ACADEMICO_FAG"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 58, "CARRERA_PROFESIONAL", "Carrera Profesional", "CARRERA_PROFESIONAL"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 59, "UNIVERSIDAD", "Universidad", "UNIVERSIDAD"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 60, "FLAG_REG_SUNEDU", "Registrado en Sunedu", "FLAG_REG_SUNEDU"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 61, "GRADO_ACADEMICO_ESP", "Grado Académico Específico", "GRADO_ACADEMICO_ESP"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 62, "REQ_HAB_PROF", "Requiere Habilitación Profesional", "REQ_HAB_PROF"));

            dtContratos.Rows.Add(NuevaFila(dtContratos, 63, "EXP_LAB_GENERAL", "Experiencia Laboral General", "EXP_LAB_GENERAL"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 64, "EXP_LAB_ESPECIFICA", "Experiencia Laboral Especifica", "EXP_LAB_ESPECIFICA"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 65, "EXP_LAB_MATERIA", "Experiencia Laboral en la Materia", "EXP_LAB_MATERIA"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 66, "EXP_LAB_GENERAL_GRADO", "Experiencia Laboral General Grado", "EXP_LAB_GENERAL_GRADO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 67, "EXP_LAB_ACT_TDRS", "Experiencia Laboral Actual TDRS", "EXP_LAB_ACT_TDRS"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 68, "RES_CONTRATO_FEC_CULM", "Res. Contrato Fecha Culminación", "RES_CONTRATO_FEC_CULM"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 69, "RES_CONTRATO_DOC", "Res. Contrato Documento", "RES_CONTRATO_DOC"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 70, "RES_CONTRATO_HR", "Res. Contrato Hoja de Ruta", "RES_CONTRATO_HR"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 71, "SEC_RESPONSABLE", "Sec. Responsable", "SEC_RESPONSABLE"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 72, "FISCAL_POST", "Fiscal Post.", "FISCAL_POST"));

            dtContratos.Rows.Add(NuevaFila(dtContratos, 73, "ENT_FIN_NOMBRE", "Entidad Financiera", "ENT_FIN_NOMBRE"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 74, "ENT_FIN_CTA_AHORRO", "Cuenta de Ahorros", "ENT_FIN_CTA_AHORRO"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 75, "ENT_FIN_CTA_BANCARIA", "Cuenta Bancaria", "ENT_FIN_CTA_BANCARIA"));
            dtContratos.Rows.Add(NuevaFila(dtContratos, 76, "ENT_FIN_CTA_CCI", "Cuenta CCI", "ENT_FIN_CTA_CCI"));
            //dtContratos.Rows.Add(NuevaFila(dtContratos, 77, "PJE_PUESTO", "Pje. Puesto", "PjePuesto"));
            //dtContratos.Rows.Add(NuevaFila(dtContratos, 78, "FLG_ESTADO", "Flag Estado", "Estado"));

            return dtContratos;
        }

        public DataTable ListaAdendas()
        {
            var dtAdendas = CrearDataTable();

            dtAdendas.Rows.Add(NuevaFila(dtAdendas, 1, "ID_CARGA", "Id Carga", "ID_CARGA"));
            dtAdendas.Rows.Add(NuevaFila(dtAdendas, 2, "COD_CONTRATO", "Código de Contrato", "COD_CONTRATO"));
            dtAdendas.Rows.Add(NuevaFila(dtAdendas, 3, "DOC_SOLICITUD", "Documento Solicitud", "DOC_SOLICITUD"));
            dtAdendas.Rows.Add(NuevaFila(dtAdendas, 4, "HOJA_RUTA", "Hoja de Ruta", "HOJA_RUTA"));
            dtAdendas.Rows.Add(NuevaFila(dtAdendas, 5, "FEC_SUSCRIPCION", "Fecha de Suscripción", "FEC_SUSCRIPCION"));
            dtAdendas.Rows.Add(NuevaFila(dtAdendas, 6, "NUMERO", "Número", "NUMERO"));
            dtAdendas.Rows.Add(NuevaFila(dtAdendas, 7, "FEC_INICIO", "Fecha de Inicio", "FEC_INICIO"));
            dtAdendas.Rows.Add(NuevaFila(dtAdendas, 8, "FEC_FIN", "Fecha de Culminación", "FEC_FIN"));
            dtAdendas.Rows.Add(NuevaFila(dtAdendas, 9, "FEC_RECEPCION", "Fecha de Recepción", "FEC_RECEPCION"));
            dtAdendas.Rows.Add(NuevaFila(dtAdendas, 10, "DOCUMENTO", "Documento", "DOCUMENTO"));

            return dtAdendas;
        }
        #endregion

        private DataRow NuevaFila(DataTable dt, int Id, string Columna, string Descripcion, string Atributo)
        {
            DataRow dr = dt.NewRow();

            dr["ID"] = Id;
            dr["COLUMNA"] = Columna;
            dr["DESCRIPCION"] = Descripcion;
            dr["ATRIBUTO"] = Atributo;

            return dr;
        }
    }
}
