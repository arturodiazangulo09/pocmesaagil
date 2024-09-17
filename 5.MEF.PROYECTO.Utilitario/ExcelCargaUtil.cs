using MEF.PROYECTO.Entity.CargaMasiva;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Net;
using OfficeOpenXml.Drawing.Chart;
using MEF.PROYECTO.Entity.Administracion;
using System.Reflection;

namespace MEF.PROYECTO.Utilitario
{
    public class ExcelCargaUtil
    {
        #region CargaMasiva 
        public DataTable ReadExcelDataFag(Stream excelStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            DataTable dt = DT_DataFagPac();

            using (ExcelPackage package = new ExcelPackage(excelStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow dr = dt.NewRow();

                    dr["COD_CONTRATO"] = worksheet.Cells[row, 1].Value is null ? null : Convert.ToString(worksheet.Cells[row, 1].Value);
                    dr["FEC_REGISTRO"] = worksheet.Cells[row, 2].Value is null ? null : Convert.ToString(worksheet.Cells[row, 2].Value);
                    dr["ESTADO"] = worksheet.Cells[row, 3].Value is null ? null : Convert.ToString(worksheet.Cells[row, 3].Value);
                    dr["NRO_EXP_SISPER"] = worksheet.Cells[row, 4].Value is null ? null : Convert.ToString(worksheet.Cells[row, 4].Value);
                    dr["LEY31419"] = worksheet.Cells[row, 5].Value is null ? null : Convert.ToString(worksheet.Cells[row, 5].Value);
                    dr["APELLIDO_PATERNO"] = worksheet.Cells[row, 6].Value is null ? null : Convert.ToString(worksheet.Cells[row, 6].Value);
                    dr["APELLIDO_MATERNO"] = worksheet.Cells[row, 7].Value is null ? null : Convert.ToString(worksheet.Cells[row, 7].Value);
                    dr["NOMBRES"] = worksheet.Cells[row, 8].Value is null ? null : Convert.ToString(worksheet.Cells[row, 8].Value);
                    dr["DNI"] = worksheet.Cells[row, 9].Value is null ? null : Convert.ToString(worksheet.Cells[row, 9].Value);
                    dr["RUC"] = worksheet.Cells[row, 10].Value is null ? null : Convert.ToString(worksheet.Cells[row, 10].Value);
                    
                    dr["SEXO"] = worksheet.Cells[row, 11].Value is null ? null : Convert.ToString(worksheet.Cells[row, 11].Value);
                    dr["FEC_NACIMIENTO"] = worksheet.Cells[row, 12].Value is null ? null : Convert.ToString(worksheet.Cells[row, 12].Value);
                    dr["EDAD"] = worksheet.Cells[row, 13].Value is null ? null : Convert.ToString(worksheet.Cells[row, 13].Value);
                    dr["TELEFONO_CELULAR"] = worksheet.Cells[row, 14].Value is null ? null : Convert.ToString(worksheet.Cells[row, 14].Value);
                    dr["CORREO_ELECTRONICO"] = worksheet.Cells[row, 15].Value is null ? null : Convert.ToString(worksheet.Cells[row, 15].Value);
                    dr["DIRECCION"] = worksheet.Cells[row, 16].Value is null ? null : Convert.ToString(worksheet.Cells[row, 16].Value);
                    dr["DISTRITO"] = worksheet.Cells[row, 17].Value is null ? null : Convert.ToString(worksheet.Cells[row, 17].Value);
                    dr["PROVINCIA"] = worksheet.Cells[row, 18].Value is null ? null : Convert.ToString(worksheet.Cells[row, 18].Value);
                    dr["REGION"] = worksheet.Cells[row, 19].Value is null ? null : Convert.ToString(worksheet.Cells[row, 19].Value);
                    dr["NIVEL_GOBIERNO"] = worksheet.Cells[row, 20].Value is null ? null : Convert.ToString(worksheet.Cells[row, 20].Value);
                    
                    dr["CODIGO_ENTIDAD"] = worksheet.Cells[row, 21].Value is null ? null : Convert.ToString(worksheet.Cells[row, 21].Value);
                    dr["TIPO_DOC_SOLICITA_REG"] = worksheet.Cells[row, 22].Value is null ? null : Convert.ToString(worksheet.Cells[row, 22].Value);
                    dr["FEC_DOC_SOLICITA_REG"] = worksheet.Cells[row, 23].Value is null ? null : Convert.ToString(worksheet.Cells[row, 23].Value);
                    dr["HR_DOC_SOLICITA_REG"] = worksheet.Cells[row, 24].Value is null ? null : Convert.ToString(worksheet.Cells[row, 24].Value);
                    dr["FLAG_DESIGNADO"] = worksheet.Cells[row, 25].Value is null ? null : Convert.ToString(worksheet.Cells[row, 25].Value);
                    dr["ALTA_DIRECCION"] = worksheet.Cells[row, 26].Value is null ? null : Convert.ToString(worksheet.Cells[row, 26].Value);
                    dr["DEPENDENCIA_PRESTACION"] = worksheet.Cells[row, 27].Value is null ? null : Convert.ToString(worksheet.Cells[row, 27].Value);
                    dr["CARGO_ANEXO1"] = worksheet.Cells[row, 28].Value is null ? null : Convert.ToString(worksheet.Cells[row, 28].Value);
                    dr["CARGO_FUNCIONARIO_SUSCRIBE"] = worksheet.Cells[row, 29].Value is null ? null : Convert.ToString(worksheet.Cells[row, 29].Value);
                    dr["IMPORTE_HONORARIO"] = worksheet.Cells[row, 30].Value is null ? null : Convert.ToString(worksheet.Cells[row, 30].Value);
                    
                    dr["FEC_INICIO"] = worksheet.Cells[row, 31].Value is null ? null : Convert.ToString(worksheet.Cells[row, 31].Value);
                    dr["FEC_CULMINACION"] = worksheet.Cells[row, 32].Value is null ? null : Convert.ToString(worksheet.Cells[row, 32].Value);
                    dr["FEC_RECEPCION"] = worksheet.Cells[row, 33].Value is null ? null : Convert.ToString(worksheet.Cells[row, 33].Value);
                    dr["FEC_SUSCRIPCION"] = worksheet.Cells[row, 34].Value is null ? null : Convert.ToString(worksheet.Cells[row, 34].Value);
                    dr["FIN_CONTRATO"] = worksheet.Cells[row, 35].Value is null ? null : Convert.ToString(worksheet.Cells[row, 35].Value);
                    dr["EXP_REMITIDO"] = worksheet.Cells[row, 36].Value is null ? null : Convert.ToString(worksheet.Cells[row, 36].Value);
                    dr["ADENDA_VIG_NRO"] = worksheet.Cells[row, 37].Value is null ? null : Convert.ToString(worksheet.Cells[row, 37].Value);
                    dr["ADENDA_VIG_FEC_INICIO"] = worksheet.Cells[row, 38].Value is null ? null : Convert.ToString(worksheet.Cells[row, 38].Value);
                    dr["ADENDA_VIG_FEC_FIN"] = worksheet.Cells[row, 39].Value is null ? null : Convert.ToString(worksheet.Cells[row, 39].Value);
                    dr["ADENDA_VIG_HOJARUTA"] = worksheet.Cells[row, 40].Value is null ? null : Convert.ToString(worksheet.Cells[row, 40].Value);
                    
                    dr["ADENDA_VIG_SUSCRIPCION"] = worksheet.Cells[row, 41].Value is null ? null : Convert.ToString(worksheet.Cells[row, 41].Value);
                    dr["GRADO_ACADEMICO_GEN"] = worksheet.Cells[row, 42].Value is null ? null : Convert.ToString(worksheet.Cells[row, 42].Value);
                    dr["GRADO_ACADEMICO_FAG"] = worksheet.Cells[row, 43].Value is null ? null : Convert.ToString(worksheet.Cells[row, 43].Value);
                    dr["EXP_LAB_GENERAL"] = worksheet.Cells[row, 44].Value is null ? null : Convert.ToString(worksheet.Cells[row, 44].Value);
                    dr["EXP_LAB_ESPECIFICA"] = worksheet.Cells[row, 45].Value is null ? null : Convert.ToString(worksheet.Cells[row, 45].Value);
                    dr["CARRERA_PROFESIONAL"] = worksheet.Cells[row, 46].Value is null ? null : Convert.ToString(worksheet.Cells[row, 46].Value);
                    dr["UNIVERSIDAD"] = worksheet.Cells[row, 47].Value is null ? null : Convert.ToString(worksheet.Cells[row, 47].Value);
                    dr["FLAG_REG_SUNEDU"] = worksheet.Cells[row, 48].Value is null ? null : Convert.ToString(worksheet.Cells[row, 48].Value).Trim().ToUpper();
                    dr["GRADO_ACADEMICO_ESP"] = worksheet.Cells[row, 49].Value is null ? null : Convert.ToString(worksheet.Cells[row, 49].Value);
                    dr["REQ_HAB_PROF"] = worksheet.Cells[row, 50].Value is null ? null : Convert.ToString(worksheet.Cells[row, 50].Value).Trim().ToUpper();
                    
                    dr["EXP_LAB_GENERAL_GRADO"] = worksheet.Cells[row, 51].Value is null ? null : Convert.ToString(worksheet.Cells[row, 51].Value);
                    dr["EXP_LAB_MATERIA"] = worksheet.Cells[row, 52].Value is null ? null : Convert.ToString(worksheet.Cells[row, 52].Value);
                    dr["EXP_LAB_ESPECIFICA"] = worksheet.Cells[row, 53].Value is null ? null : Convert.ToString(worksheet.Cells[row, 53].Value);
                    dr["RES_CONTRATO_FEC_CULM"] = worksheet.Cells[row, 54].Value is null ? null : Convert.ToString(worksheet.Cells[row, 54].Value);
                    dr["RES_CONTRATO_DOC"] = worksheet.Cells[row, 55].Value is null ? null : Convert.ToString(worksheet.Cells[row, 55].Value);
                    dr["RES_CONTRATO_HR"] = worksheet.Cells[row, 56].Value is null ? null : Convert.ToString(worksheet.Cells[row, 56].Value);
                    dr["SEC_RESPONSABLE"] = worksheet.Cells[row, 57].Value is null ? null : Convert.ToString(worksheet.Cells[row, 57].Value);
                    dr["FISCAL_POST"] = worksheet.Cells[row, 58].Value is null ? null : Convert.ToString(worksheet.Cells[row, 58].Value);
                    dr["ENT_FIN_NOMBRE"] = worksheet.Cells[row, 59].Value is null ? null : Convert.ToString(worksheet.Cells[row, 59].Value);
                    dr["ENT_FIN_CTA_AHORRO"] = worksheet.Cells[row, 60].Value is null ? null : Convert.ToString(worksheet.Cells[row, 60].Value);
                    
                    dr["ENT_FIN_CTA_CCI"] = worksheet.Cells[row, 61].Value is null ? null : Convert.ToString(worksheet.Cells[row, 61].Value);

                    dr["FLG_ESTADO"] = "1";
                    dr["FLG_PROCESADO"] = "0";

                    dt.Rows.Add(dr);

                }

            }
            return dt;
        }
        public DataTable ReadExcelDataPac(Stream excelStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            DataTable dt = DT_DataFagPac();

            using (ExcelPackage package = new ExcelPackage(excelStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow dr = dt.NewRow();
                    dr["COD_CONTRATO"] = worksheet.Cells[row, 1].Value is null ? null : Convert.ToString(worksheet.Cells[row, 1].Value);
                    dr["ALERTA_LA"] = worksheet.Cells[row, 2].Value is null ? null : Convert.ToString(worksheet.Cells[row, 2].Value);
                    dr["ALERTA_LA_PERSONA"] = worksheet.Cells[row, 3].Value is null ? null : Convert.ToString(worksheet.Cells[row, 3].Value);
                    dr["FEC_REGISTRO"] = worksheet.Cells[row, 4].Value is null ? null : Convert.ToString(worksheet.Cells[row, 4].Value);
                    dr["MODALIDAD"] = worksheet.Cells[row, 5].Value is null ? null : Convert.ToString(worksheet.Cells[row, 5].Value);
                    dr["ESTADO"] = worksheet.Cells[row, 6].Value is null ? null : Convert.ToString(worksheet.Cells[row, 6].Value);
                    dr["NRO_EXP_SISPER"] = worksheet.Cells[row, 7].Value is null ? null : Convert.ToString(worksheet.Cells[row, 7].Value);
                    dr["APELLIDO_PATERNO"] = worksheet.Cells[row, 8].Value is null ? null : Convert.ToString(worksheet.Cells[row, 8].Value);
                    dr["APELLIDO_MATERNO"] = worksheet.Cells[row, 9].Value is null ? null : Convert.ToString(worksheet.Cells[row, 9].Value);
                    dr["NOMBRES"] = worksheet.Cells[row, 10].Value is null ? null : Convert.ToString(worksheet.Cells[row, 10].Value);


                    dr["DNI"] = worksheet.Cells[row, 11].Value is null ? null : Convert.ToString(worksheet.Cells[row, 11].Value);
                    dr["RUC"] = worksheet.Cells[row, 12].Value is null ? null : Convert.ToString(worksheet.Cells[row, 12].Value);
                    dr["SEXO"] = worksheet.Cells[row, 13].Value is null ? null : Convert.ToString(worksheet.Cells[row, 13].Value);
                    dr["FEC_NACIMIENTO"] = worksheet.Cells[row, 14].Value is null ? null : Convert.ToString(worksheet.Cells[row, 14].Value);
                    dr["EDAD"] = worksheet.Cells[row, 15].Value is null ? null : Convert.ToString(worksheet.Cells[row, 15].Value);
                    dr["NACIONALIDAD"] = worksheet.Cells[row, 16].Value is null ? null : Convert.ToString(worksheet.Cells[row, 16].Value);
                    dr["PADRE"] = worksheet.Cells[row, 17].Value is null ? null : Convert.ToString(worksheet.Cells[row, 17].Value);
                    dr["TELEFONO_CELULAR"] = worksheet.Cells[row, 18].Value is null ? null : Convert.ToString(worksheet.Cells[row, 18].Value);
                    dr["CORREO_ELECTRONICO"] = worksheet.Cells[row, 19].Value is null ? null : Convert.ToString(worksheet.Cells[row, 19].Value);
                    dr["DIRECCION"] = worksheet.Cells[row, 20].Value is null ? null : Convert.ToString(worksheet.Cells[row, 20].Value);
                   

                    dr["DISTRITO"] = worksheet.Cells[row, 21].Value is null ? null : Convert.ToString(worksheet.Cells[row, 21].Value);
                    dr["PROVINCIA"] = worksheet.Cells[row, 22].Value is null ? null : Convert.ToString(worksheet.Cells[row, 22].Value);
                    dr["REGION"] = worksheet.Cells[row, 23].Value is null ? null : Convert.ToString(worksheet.Cells[row, 23].Value);
                    dr["NIVEL_GOBIERNO"] = worksheet.Cells[row, 24].Value is null ? null : Convert.ToString(worksheet.Cells[row, 24].Value);
                    dr["CODIGO_ENTIDAD"] = worksheet.Cells[row, 25].Value is null ? null : Convert.ToString(worksheet.Cells[row, 25].Value);
                    dr["TIPO_DOC_SOLICITA_REG"] = worksheet.Cells[row, 26].Value is null ? null : Convert.ToString(worksheet.Cells[row, 26].Value);
                    dr["FEC_DOC_SOLICITA_REG"] = worksheet.Cells[row, 27].Value is null ? null : Convert.ToString(worksheet.Cells[row, 27].Value);
                    dr["HR_DOC_SOLICITA_REG"] = worksheet.Cells[row, 28].Value is null ? null : Convert.ToString(worksheet.Cells[row, 28].Value);
                    dr["DOC_RES_MINISTERIAL"] = worksheet.Cells[row, 29].Value is null ? null : Convert.ToString(worksheet.Cells[row, 29].Value);
                    dr["ENTIDAD_BENEF"] = worksheet.Cells[row, 30].Value is null ? null : Convert.ToString(worksheet.Cells[row, 30].Value);
                    
                     dr["ALTA_DIRECCION"] = worksheet.Cells[row, 31].Value is null ? null : Convert.ToString(worksheet.Cells[row, 31].Value);
                     dr["DEPENDENCIA_PRESTACION"] = worksheet.Cells[row, 32].Value is null ? null : Convert.ToString(worksheet.Cells[row, 32].Value);
                     dr["CARGO_ANEXO1"] = worksheet.Cells[row, 33].Value is null ? null : Convert.ToString(worksheet.Cells[row, 33].Value);
                     dr["CARGO_FUNCIONARIO_SUSCRIBE"] = worksheet.Cells[row, 34].Value is null ? null : Convert.ToString(worksheet.Cells[row, 34].Value);
                     dr["IMPORTE_HONORARIO"] = worksheet.Cells[row, 35].Value is null ? null : Convert.ToString(worksheet.Cells[row, 35].Value);
                     dr["FEC_SUSCRIPCION"] = worksheet.Cells[row, 36].Value is null ? null : Convert.ToString(worksheet.Cells[row, 36].Value);
                     dr["FEC_INICIO"] = worksheet.Cells[row, 37].Value is null ? null : Convert.ToString(worksheet.Cells[row, 37].Value);
                     dr["FEC_CULMINACION"] = worksheet.Cells[row, 38].Value is null ? null : Convert.ToString(worksheet.Cells[row, 38].Value);
                     dr["FEC_RECEPCION"] = worksheet.Cells[row, 39].Value is null ? null : Convert.ToString(worksheet.Cells[row, 39].Value);
                     dr["NRO_OFICIO"] = worksheet.Cells[row, 40].Value is null ? null : Convert.ToString(worksheet.Cells[row, 40].Value);

                     dr["ADENDA_VIG_DOC_SOL"] = worksheet.Cells[row, 41].Value is null ? null : Convert.ToString(worksheet.Cells[row, 41].Value);
                     dr["ADENDA_VIG_HOJARUTA"] = worksheet.Cells[row, 42].Value is null ? null : Convert.ToString(worksheet.Cells[row, 42].Value);                   
                     dr["ADENDA_VIG_NRO"] = worksheet.Cells[row, 43].Value is null ? null : Convert.ToString(worksheet.Cells[row, 43].Value);
                     dr["ADENDA_VIG_FEC_INICIO"] = worksheet.Cells[row, 44].Value is null ? null : Convert.ToString(worksheet.Cells[row, 44].Value);
                     dr["ADENDA_VIG_FEC_FIN"] = worksheet.Cells[row, 45].Value is null ? null : Convert.ToString(worksheet.Cells[row, 45].Value);
                     dr["NOT_FEC_RECEPCION"] = worksheet.Cells[row, 46].Value is null ? null : Convert.ToString(worksheet.Cells[row, 46].Value);
                     dr["NOT_DOCUMENTO"] = worksheet.Cells[row, 47].Value is null ? null : Convert.ToString(worksheet.Cells[row, 47].Value);
                     dr["FEC_CULMINACION_CONTRATO"] = worksheet.Cells[row, 48].Value is null ? null : Convert.ToString(worksheet.Cells[row, 48].Value);
                     dr["GRADO_ACADEMICO_GEN"] = worksheet.Cells[row, 49].Value is null ? null : Convert.ToString(worksheet.Cells[row, 49].Value);
                     dr["UNIVERSIDAD"] = worksheet.Cells[row, 50].Value is null ? null : Convert.ToString(worksheet.Cells[row, 50].Value);

                     dr["GRADO_ACADEMICO_ESP"] = worksheet.Cells[row, 51].Value is null ? null : Convert.ToString(worksheet.Cells[row, 51].Value);
                     dr["CARRERA_PROFESIONAL"] = worksheet.Cells[row, 52].Value is null ? null : Convert.ToString(worksheet.Cells[row, 52].Value);
                     dr["FISCAL_POST"] = worksheet.Cells[row, 53].Value is null ? null : Convert.ToString(worksheet.Cells[row, 53].Value);
                     dr["EXP_LAB_GENERAL"] = worksheet.Cells[row, 54].Value is null ? null : Convert.ToString(worksheet.Cells[row, 54].Value);
                     dr["EXP_LAB_ESPECIFICA"] = worksheet.Cells[row, 55].Value is null ? null : Convert.ToString(worksheet.Cells[row, 55].Value);
                     dr["RES_CONTRATO_FEC_CULM"] = worksheet.Cells[row, 56].Value is null ? null : Convert.ToString(worksheet.Cells[row, 56].Value);
                     dr["RES_CONTRATO_DOC"] = worksheet.Cells[row, 57].Value is null ? null : Convert.ToString(worksheet.Cells[row, 57].Value);
                     dr["RES_CONTRATO_HR"] = worksheet.Cells[row, 58].Value is null ? null : Convert.ToString(worksheet.Cells[row, 58].Value);
                     dr["SEC_RESPONSABLE"] = worksheet.Cells[row, 59].Value is null ? null : Convert.ToString(worksheet.Cells[row, 59].Value);
                     dr["ENT_FIN_NOMBRE"] = worksheet.Cells[row, 60].Value is null ? null : Convert.ToString(worksheet.Cells[row, 60].Value);

                     dr["ENT_FIN_CTA_AHORRO"] = worksheet.Cells[row, 61].Value is null ? null : Convert.ToString(worksheet.Cells[row, 61].Value);
                     dr["ENT_FIN_CTA_CCI"] = worksheet.Cells[row, 62].Value is null ? null : Convert.ToString(worksheet.Cells[row, 62].Value);
                     dr["PJE_PUESTO"] = worksheet.Cells[row, 63].Value is null ? null : Convert.ToString(worksheet.Cells[row, 63].Value);
                     dr["FLG_ESTADO"] ="1";
                     dr["FLG_PROCESADO"] ="0";


                    dt.Rows.Add(dr);

                }

            }
            return dt;
        }
        public DataTable ReadExcelDataAdendas(Stream excelStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            DataTable dt = DT_DataAdendas();

            using (ExcelPackage package = new ExcelPackage(excelStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 3; row <= rowCount; row++)
                {
                    DataRow dr = dt.NewRow();

                    dr["COD_CONTRATO"] = worksheet.Cells[row, 1].Value is null ? null : Convert.ToString(worksheet.Cells[row, 1].Value);
                    dr["DOC_SOLICITUD"] = worksheet.Cells[row, 2].Value is null ? null : Convert.ToString(worksheet.Cells[row, 2].Value);
                    dr["HOJA_RUTA"] = worksheet.Cells[row, 3].Value is null ? null : Convert.ToString(worksheet.Cells[row, 3].Value);
                    dr["NUMERO"] = worksheet.Cells[row, 4].Value is null ? null : Convert.ToString(worksheet.Cells[row, 4].Value);
                    dr["FEC_INICIO"] = worksheet.Cells[row, 5].Value is null ? null : Convert.ToString(worksheet.Cells[row, 5].Value);
                    dr["FEC_FIN"] = worksheet.Cells[row, 6].Value is null ? null : Convert.ToString(worksheet.Cells[row, 6].Value);
                    dr["FEC_RECEPCION"] = worksheet.Cells[row, 7].Value is null ? null : Convert.ToString(worksheet.Cells[row, 7].Value);
                    dr["DOCUMENTO"] = worksheet.Cells[row, 8].Value is null ? null : Convert.ToString(worksheet.Cells[row, 8].Value);

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        #endregion

        #region CargaMasiva Pagos
        public DataTable ReadExcelDataPag(Stream excelStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            DataTable dt = DT_DataPagos();

            using (ExcelPackage package = new ExcelPackage(excelStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow dr = dt.NewRow();
                    int contColumnas = 1;

                    foreach (var item in ListaPagos().ToList())
                    {
                        dr[item.Id] = worksheet.Cells[row, contColumnas].Value == DBNull.Value ? null : Convert.ToString(worksheet.Cells[row, contColumnas].Value);
                        contColumnas++;
                    }

                    dt.Rows.Add(dr);

                }

            }
            return dt;
        }
        #endregion

        #region Validaciones
        public List<Respuesta> ValidacionCargaMasivaDataFag(Stream excelStream, List<Cls_Ent_Entidades> listaEntidades = null, List<Cls_Ent_Ubigeo> listaUbigeos = null)
        {
            var lista = new List<Respuesta>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(excelStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                
                List<Cls_Ent_Ubigeo> listaUbigeosExiste = new List<Cls_Ent_Ubigeo>();
                List<Cls_Ent_Entidades> listaEntidadesExiste = new List<Cls_Ent_Entidades>();

                int rowCount = worksheet.Dimension.Rows;

                if (worksheet.Dimension.Columns != ListaFag().Count)
                {
                    var rpta = new Respuesta();
                    rpta.IsError=true;
                    rpta.SetMensaje(Constants.Mensajes.Error.NroColumnasFormato);
                    lista.Add(rpta);

                    return lista;
                }

                var validator = new Validaciones();
                var campos = ListaFag().ToList();

                for (int row = 2; row < rowCount; row++)
                {
                    Catalogo campo;
                    var rpta = new Respuesta();

                    // Validación COL 1: COD_CONTRATO
                    campo = campos[0];
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 1].Value is null )
                        {
                            //string codContrato = worksheet.Cells[row, 1].Value.ToString();
                            rpta.IsError = true;
                            rpta.SetMensaje(string.Empty, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                        else {
                            if (worksheet.Cells[row, 1].Value.ToString() == "")
                            {
                                //string codContrato = worksheet.Cells[row, 1].Value.ToString();
                                rpta.IsError = true;
                                rpta.SetMensaje(string.Empty, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                                lista.Add(rpta);
                            }
                        }
                    }

                    // Validación COL 2: FEC_REGISTRO
                    campo = campos[1];
                    string fecRegistro = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 2].Value is null)
                        {
                            //fecRegistro = worksheet.Cells[row, 1].Value.ToString();
                            rpta.IsError=true;
                            rpta.SetMensaje(fecRegistro, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 2].Value is null))
                    {
                        fecRegistro = worksheet.Cells[row, 2].Value.ToString();
                        var valida = validator.ValidarFecha(fecRegistro);
                        rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(fecRegistro, row, campo.Descripcion, Constants.Mensajes.Error.FormatoFecha);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 3: ESTADO
                    campo = campos[2];
                    string estado = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 3].Value is null)
                        {
                            //estado = worksheet.Cells[row, 3].Value.ToString();
                            rpta.IsError=true;
                            rpta.SetMensaje(estado, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 3].Value is null))
                    {
                        string[] permitidos = new string[3];
                        permitidos[0] = Constants.Estados.Activo.Id;
                        permitidos[1] = Constants.Estados.Baja.Id;
                        permitidos[2] = Constants.Estados.Devolucion.Id;

                        estado = worksheet.Cells[row, 3].Value.ToString();
                        var valida = validator.ValoresFijos(permitidos, estado);

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(estado, row, campo.Descripcion, Constants.Mensajes.Error.ValoresFijos);
                            lista.Add(rpta);
                        }

                    }

                    // Validación COL 4: NRO_EXP_SISPER
                    campo = campos[3];
                    string nroExpediente = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 4].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(nroExpediente, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 4].Value is null))
                    {
                        nroExpediente = worksheet.Cells[row, 4].Value.ToString();
                        var valida = validator.ValidarSoloNumeros(nroExpediente);

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(nroExpediente, row, campo.Descripcion, Constants.Mensajes.Error.SoloNumeros);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 5: LEY31419
                    campo = campos[4];
                    string ley31419 = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 5].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(ley31419, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 5].Value is null))
                    {
                        string[] permitidos = new string[2];
                        permitidos[0] = Constants.SiNo.Si;
                        permitidos[1] = Constants.SiNo.No;

                        ley31419 = worksheet.Cells[row, 5].Value.ToString();
                        var valida = validator.ValoresFijos(permitidos, ley31419);

                        rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(ley31419, row, campo.Descripcion, Constants.Mensajes.Error.ValoresFijos);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 6: APELLIDO_PATERNO
                    campo = campos[5];
                    string apellidoPaterno = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 6].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(apellidoPaterno, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 6].Value is null))
                    {
                        apellidoPaterno = worksheet.Cells[row, 6].Value.ToString();
                        var valida = validator.ValidarSoloLetras(apellidoPaterno.Trim());

                        rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(apellidoPaterno, row, campo.Descripcion, Constants.Mensajes.Error.SoloTexto);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 7: APELLIDO_MATERNO
                    campo = campos[6];
                    string apellidoMaterno = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 7].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(apellidoMaterno, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 7].Value is null))
                    {
                        apellidoMaterno = worksheet.Cells[row, 7].Value.ToString();
                        var valida = validator.ValidarSoloLetras(apellidoMaterno.Trim());

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(apellidoMaterno, row, campo.Descripcion, Constants.Mensajes.Error.SoloTexto);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 8: NOMBRES
                    campo = campos[7];
                    string nombres = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 8].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(nombres, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 8].Value is null))
                    {
                        nombres = worksheet.Cells[row, 8].Value.ToString();
                        var valida = validator.ValidarSoloLetras(nombres.Trim());

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(nombres, row, campo.Descripcion, Constants.Mensajes.Error.SoloTexto);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 9: DNI O CARNET DE EXTRANJERIA
                    campo = campos[8];
                    string dni = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 9].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(dni, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 9].Value is null))
                    {
                        dni = worksheet.Cells[row, 9].Value.ToString();
                        var valida = validator.ValidarSoloNumeros(dni);

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(dni, row, campo.Descripcion, Constants.Mensajes.Error.SoloTexto);
                            lista.Add(rpta);
                        }

                            valida = validator.Tamanio(8, 8, dni);
                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(dni, row, campo.Descripcion, Constants.Mensajes.Error.Tamanio);
                            lista.Add(rpta);
                        }

                    }

                    // Validación COL 10: RUC
                    campo = campos[9];
                    string ruc = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 10].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(ruc, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 10].Value is null))
                    {
                        ruc = worksheet.Cells[row, 10].Value.ToString();
                        var valida = validator.esRUCInvalido(ruc);
                        rpta.IsError=valida;

                        if (valida)
                        {
                            rpta.SetMensaje(ruc, row, campo.Descripcion, Constants.Mensajes.Error.ruc);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 11: SEXO
                    campo = campos[10];
                    string sexo = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 11].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(sexo, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 11].Value is null))
                    {
                        string[] permitidos = new string[2];
                        permitidos[0] = Constants.Sexo.Masculino;
                        permitidos[1] = Constants.Sexo.Femenino;

                        sexo = worksheet.Cells[row, 11].Value.ToString();
                        var valida = validator.ValoresFijos(permitidos, sexo);

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(sexo, row, campo.Descripcion, Constants.Mensajes.Error.ValoresFijos);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 12: FEC_NACIMIENTO
                    campo = campos[11];
                    string fechaNacimiento = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 12].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(fechaNacimiento, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 12].Value is null))
                    {
                        fechaNacimiento = worksheet.Cells[row, 12].Value.ToString();
                        var valida = validator.ValidarFecha(fechaNacimiento);

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(fechaNacimiento, row, campo.Descripcion, Constants.Mensajes.Error.FormatoFecha);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 13: EDAD
                    campo = campos[12];
                    string edad = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 13].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(edad, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 13].Value is null))
                    {
                        edad = worksheet.Cells[row, 13].Value.ToString();
                        var valida = validator.ValidarSoloNumeros(edad);

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(edad, row, campo.Descripcion, Constants.Mensajes.Error.SoloNumeros);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 14: TELEFONO_CELULAR
                    campo = campos[13];
                    string telefono = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 14].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(telefono, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    //if (!(worksheet.Cells[row, 14].Value is null))
                    //{
                    //    telefono = worksheet.Cells[row, 14].Value.ToString();
                    //    var valida = validator.ValidarSoloNumeros(telefono);

                    //        rpta.IsError=valida.IsError;

                    //    if (valida.IsError)
                    //    {
                    //        rpta.SetMensaje(telefono, row, campo.Descripcion, Constants.Mensajes.Error.SoloNumeros);
                    //        lista.Add(rpta);
                    //    }
                    //}

                    // Validación COL 15: CORREO_ELECTRONICO
                    campo = campos[14];
                    string correoElectronico = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 15].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(correoElectronico, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 15].Value is null))
                    {
                        correoElectronico = worksheet.Cells[row, 15].Value.ToString();
                        var valida = validator.ValidarFormatoCorreo(correoElectronico.Trim());

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(correoElectronico, row, campo.Descripcion, Constants.Mensajes.Error.FormatoCorreo);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 16: DIRECCION
                    campo = campos[15];
                    string direccion = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 16].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(direccion, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 17: DISTRITO
                    campo = campos[16];
                    string distrito = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 17].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(distrito, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 18: PROVINCIA
                    campo = campos[17];
                    string provincia = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 18].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(provincia, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 19: REGION
                    campo = campos[18];
                    string region = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 19].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(region, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    
                    // Validación COL 17, 18, 19
                    if (!(worksheet.Cells[row, 17].Value is null) && !(worksheet.Cells[row, 18].Value is null) && !(worksheet.Cells[row, 19].Value is null))
                    {
                        distrito = worksheet.Cells[row, 17].Value.ToString().Trim().ToUpper();
                        provincia = worksheet.Cells[row, 18].Value.ToString().Trim().ToUpper();
                        region = worksheet.Cells[row, 19].Value.ToString().Trim().ToUpper();

                        listaUbigeosExiste = listaUbigeos.Where(x => 
                                                (x.CNOMDISTRITO != null ? x.CNOMDISTRITO.Trim() == distrito : false) && 
                                                (x.CNOMPROVINCIA != null ? x.CNOMPROVINCIA.Trim() == provincia : false) && 
                                                (x.CNOMDEPARTAMENTO != null ? x.CNOMDEPARTAMENTO.Trim() == region : false)
                                            ).ToList();

                        if (listaUbigeosExiste.Count() == 0)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(distrito + "/" + provincia + "/" + region, row, "DISTRITO/PROVINCIA/REGION", Constants.Mensajes.Error.UbigeoIncorrecto);
                            lista.Add(rpta);    
                        }
                    }

                    // Validación COL 20: NIVEL_GOBIERNO
                    campo = campos[19];
                    string nivelGobierno = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 20].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(nivelGobierno, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 20].Value is null))
                    {
                        string[] permitido = new string[2];
                        permitido[0] = Constants.Gobierno.GobiernoNacional;
                        permitido[1] = Constants.Gobierno.GobiernoRegional;

                        nivelGobierno = worksheet.Cells[row, 20].Value.ToString();
                        var valida = validator.ValoresFijos(permitido, nivelGobierno);

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(nivelGobierno, row, campo.Descripcion, Constants.Mensajes.Error.ValoresFijos);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 21: CODIGO_ENTIDAD
                    campo = campos[20];
                    string codigoEntidad = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 21].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(codigoEntidad, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 21].Value is null))
                    {
                        codigoEntidad = worksheet.Cells[row, 21].Value.ToString();
                        var valida = validator.ValidarSoloLetras(codigoEntidad);

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(codigoEntidad, row, campo.Descripcion, Constants.Mensajes.Error.SoloTexto);
                            lista.Add(rpta);
                        }
                    }

                    //Validando que exista en la lista de Entidades
                    String ENTIDAD_BENEF = "";
                    if (!(worksheet.Cells[row, 21].Value is null))
                    {
                        Respuesta respuesta = new Respuesta();
                        ENTIDAD_BENEF = worksheet.Cells[row, 21].Value.ToString();

                        listaEntidadesExiste = listaEntidades.Where(x => x.ACRONIMO == ENTIDAD_BENEF.Trim().ToUpper()).ToList();
                        if (listaEntidadesExiste.Count() == 0)
                        {
                            respuesta.IsError = true; ;
                            respuesta.SetMensaje(ENTIDAD_BENEF, row, "ENTIDAD", Constants.Mensajes.Error.EntidadIncorrecta);
                            lista.Add(respuesta);
                        }
                    }

                    // Validación COL 22: TIPO_DOC_SOLICITA_REG
                    campo = campos[21];
                    string tipoDocReg = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 22].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(tipoDocReg, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 23: FEC_DOC_SOLICITA_REG
                    campo = campos[22];
                    string fecDocReg = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 22].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(fecDocReg, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 23].Value is null))
                    {
                        fecDocReg = worksheet.Cells[row, 23].Value.ToString();
                        var valida = validator.ValidarFecha(fecDocReg);

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(fecDocReg, row, campo.Descripcion, Constants.Mensajes.Error.FormatoFecha);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 24: HR_DOC_SOLICITA_REG
                    campo = campos[23];
                    string hojaRutaReg = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 24].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(hojaRutaReg, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 24].Value is null))
                    {
                        hojaRutaReg = worksheet.Cells[row, 24].Value.ToString();
                        rpta = validator.ValidarHojaRuta(hojaRutaReg, row, campo.Descripcion);
                        if(rpta.IsError) lista.Add(rpta);
                    }

                    // Validación COL 25: FLAG_DESIGNADO
                    campo = campos[24];
                    string flagDesignado = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 25].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(flagDesignado, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 25].Value is null))
                    {
                        string[] permitido = new string[2];
                        permitido[0] = Constants.SiNo.Si;
                        permitido[1] = Constants.SiNo.No;

                        flagDesignado = worksheet.Cells[row, 25].Value.ToString();
                        var valida = validator.ValoresFijos(permitido, flagDesignado);

                        rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(flagDesignado, row, campo.Descripcion, Constants.Mensajes.Error.ValoresFijos);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 26: ALTA_DIRECCION
                    campo = campos[25];
                    string altaDireccion = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 26].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(altaDireccion, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 26].Value is null))
                    {

                        }

                    // Validación COL 27: DEPENDENCIA_PRESTA_SERVICIOS
                    campo = campos[26];
                    string dependencia = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 27].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(dependencia, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 27].Value is null))
                    {

                        }

                    // Validación COL 28: CARGO_ANEXO1
                    campo = campos[27];
                    string cargoAnexo1 = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 28].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(cargoAnexo1, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 29: CARGO_FUNCIONARIO_SUSCRIBE
                    campo = campos[28];
                    string campoFuncionario = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 29].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(campoFuncionario, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 29].Value is null))
                    {

                        }

                    // Validación COL 30: IMPORTE_HONORARIO
                    campo = campos[29];
                    string importeHonorario = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 30].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(importeHonorario, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 30].Value is null))
                    {
                        importeHonorario = worksheet.Cells[row, 30].Value.ToString();
                        var valida = validator.ValidarMontosDecimales(importeHonorario);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(importeHonorario, row, campo.Descripcion, Constants.Mensajes.Error.SoloMonto);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 31: FEC_INICIO
                    campo = campos[30];
                    string fecInicio = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 31].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(fecInicio, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 31].Value is null))
                    {
                        fecInicio = worksheet.Cells[row, 31].Value.ToString();
                        var valida = validator.ValidarFecha(fecInicio);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(fecInicio, row, campo.Descripcion, Constants.Mensajes.Error.FormatoFecha);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 32: FEC_CULMINACION
                    campo = campos[31];
                    string fecCulminacion = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 32].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(fecCulminacion, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 34].Value is null))
                    {
                        fecCulminacion = worksheet.Cells[row, 34].Value.ToString();
                        var valida = validator.ValidarFecha(fecCulminacion);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(fecCulminacion, row, campo.Descripcion, Constants.Mensajes.Error.FormatoFecha);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 33: FEC_RECEPCION
                    campo = campos[32];
                    string fecRecepcion = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 32].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(fecRecepcion, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 33].Value is null))
                    {
                        fecRecepcion = worksheet.Cells[row, 33].Value.ToString();
                        var valida = validator.ValidarFecha(fecRecepcion);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(fecRecepcion, row, campo.Descripcion, Constants.Mensajes.Error.FormatoFecha);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 34: FEC_SUSCRIPCION
                    campo = campos[33];
                    string fecSuscripcion = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 34].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(fecSuscripcion, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 34].Value is null))
                    {
                        fecSuscripcion = worksheet.Cells[row, 34].Value.ToString();
                        var valida = validator.ValidarFecha(fecSuscripcion);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(fecSuscripcion, row, campo.Descripcion, Constants.Mensajes.Error.FormatoFecha);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 35: FIN_CONTRATO
                    campo = campos[34];
                    string fecFinContrato = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 35].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(fecFinContrato, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 35].Value is null))
                    {
                        fecFinContrato = worksheet.Cells[row, 35].Value.ToString();
                        var valida = validator.ValidarFecha(fecFinContrato);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(fecFinContrato, row, campo.Descripcion, Constants.Mensajes.Error.FormatoFecha);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 36: EXP_REMITIDO
                    campo = campos[35];
                    string expRemitido = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 36].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(expRemitido, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 36].Value is null))
                    {
                        string[] permitido = new string[2];
                        permitido[0] = Constants.TipoExpediente.Fisico;
                        permitido[1] = Constants.TipoExpediente.Virtual;

                        expRemitido = worksheet.Cells[row, 36].Value.ToString().Trim();
                        var valida = validator.ValoresFijos(permitido, expRemitido.ToUpper());

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(expRemitido, row, campo.Descripcion, Constants.Mensajes.Error.ValoresFijos);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 37: ADENDA_VIG_NRO
                    campo = campos[36];
                    string adendaVigente = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 37].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(adendaVigente, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 37].Value is null))
                    {
                        adendaVigente = worksheet.Cells[row, 37].Value.ToString();
                        var valida = validator.ValidarSoloNumeros(adendaVigente);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(adendaVigente, row, campo.Descripcion, Constants.Mensajes.Error.SoloNumeros);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 38: ADENDA_VIG_FEC_INICIO
                    campo = campos[37];
                    string fecInicioAdenda = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 38].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(fecInicioAdenda, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 38].Value is null))
                    {
                        fecInicioAdenda = worksheet.Cells[row, 38].Value.ToString();
                        var valida = validator.ValidarFecha(fecInicioAdenda);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(fecInicioAdenda, row, campo.Descripcion, Constants.Mensajes.Error.FormatoFecha);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 39: ADENDA_VIG_FEC_FIN
                    campo = campos[38];
                    string fecFinAdenda = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 39].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(fecFinAdenda, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 39].Value is null))
                    {
                        fecFinAdenda = worksheet.Cells[row, 39].Value.ToString();
                        var valida = validator.ValidarFecha(fecFinAdenda);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(fecFinAdenda, row, campo.Descripcion, Constants.Mensajes.Error.FormatoFecha);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 40: ADENDA_VIG_HOJARUTA
                    campo = campos[39];
                    string hojaRutaAdenda = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 40].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(hojaRutaAdenda, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 40].Value is null))
                    {
                        hojaRutaReg = worksheet.Cells[row, 40].Value.ToString();
                        rpta = validator.ValidarHojaRuta(hojaRutaReg, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }

                    // Validación COL 41: ADENDA_VIG_SUSCRIPCION
                    campo = campos[40];
                    string suscripcionAdenda = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 41].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(suscripcionAdenda, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 41].Value is null))
                    {
                        suscripcionAdenda = worksheet.Cells[row, 41].Value.ToString();
                        var valida = validator.ValidarFecha(suscripcionAdenda);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(suscripcionAdenda, row, campo.Descripcion, Constants.Mensajes.Error.FormatoFecha);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 42: GRADO_ACADEMICO_GEN
                    campo = campos[41];
                    string gradoAcademico = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 42].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(gradoAcademico, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 42].Value is null))
                    {
                        string[] permitido = new string[2];
                        permitido[0] = Constants.GradoAcademico.Bachiller;
                        permitido[1] = Constants.GradoAcademico.Titulado;

                        gradoAcademico = worksheet.Cells[row, 42].Value.ToString();
                        var valida = validator.ValoresFijos(permitido, gradoAcademico);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(gradoAcademico, row, campo.Descripcion, Constants.Mensajes.Error.ValoresFijos);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 43: GRADO_ACADEMICO_FAG
                    campo = campos[42];
                    string gradoAcademicoFag = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 43].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(gradoAcademicoFag, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 43].Value is null))
                    {
                        string[] permitido = new string[3];
                        permitido[0] = Constants.GradoAcademico.Bachiller;
                        permitido[1] = Constants.GradoAcademico.Titulado;
                        permitido[2] = Constants.GradoAcademico.MagisterDoctorado;

                        gradoAcademicoFag = worksheet.Cells[row, 43].Value.ToString();
                        var valida = validator.ValoresFijos(permitido, gradoAcademicoFag);

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(gradoAcademicoFag, row, campo.Descripcion, Constants.Mensajes.Error.ValoresFijos);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 44: EXP_LAB_GENERAL
                    campo = campos[43];
                    string experiencia = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 44].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(experiencia, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 44].Value is null))
                    {
                        experiencia = worksheet.Cells[row, 44].Value.ToString();
                        var valida = validator.ValidarSoloNumeros(experiencia);

                            rpta.IsError=valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(experiencia, row, campo.Descripcion, Constants.Mensajes.Error.SoloNumeros);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 45: EXP_LAB_ESPECIFICA
                    campo = campos[44];
                    string experienciaEspecifica = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 45].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(experienciaEspecifica, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 45].Value is null))
                    {
                        experienciaEspecifica = worksheet.Cells[row, 45].Value.ToString();
                        var valida = validator.ValidarSoloNumeros(experienciaEspecifica);

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(experienciaEspecifica, row, campo.Descripcion, Constants.Mensajes.Error.SoloNumeros);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 46: TITULO O GRADO ACADEMICO
                    campo = campos[45];
                    string titulo = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 46].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(titulo, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 46].Value is null))
                    {

                    }

                    // Validación COL 47: UNIVERSIDAD
                    campo = campos[46];
                    string universidad = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 47].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(universidad, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 48: FLAG_REG_SUNEDU
                    campo = campos[47];
                    string flagSunedu = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 48].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(flagSunedu, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 48].Value is null))
                    {
                        string[] permitido = new string[2];
                        permitido[0] = Constants.SiNo.Si;
                        permitido[1] = Constants.SiNo.No;

                        flagSunedu = worksheet.Cells[row, 48].Value.ToString();
                        var valida = validator.ValoresFijos(permitido, flagSunedu.Trim().ToUpper());

                            rpta.IsError = valida.IsError;

                        if (valida.IsError)
                        {
                            rpta.SetMensaje(flagSunedu, row, campo.Descripcion, Constants.Mensajes.Error.ValoresFijos);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 49: GRADO_ACADEMICO_ESP
                    campo = campos[48];
                    string gradoAcademicoEsp = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if (worksheet.Cells[row, 49].Value is null)
                        {
                            rpta.IsError=true;
                            rpta.SetMensaje(gradoAcademicoEsp, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 50: REQ_HAB_PROF
                    campo = campos[49];
                    string habilitacionProf = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 50].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(habilitacionProf, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if(!(worksheet.Cells[row, 50].Value is null))
                    {
                        string[] permitidos = new string[2];
                        permitidos[0] = Constants.SiNo.Si;
                        permitidos[1] = Constants.SiNo.No;

                        habilitacionProf = worksheet.Cells[row, 50].Value.ToString();
                        rpta = validator.ValoresFijos(permitidos, habilitacionProf.Trim().ToUpper(), row, campo.Descripcion);
                        
                        if(rpta.IsError) lista.Add(rpta);
                    }

                    // Validación COL 51: EXP_LAB_GENERAL_GRADO
                    campo = campos[50];
                    string experienciaGeneral = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 51].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(experienciaGeneral, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 51].Value is null))
                    {
                        experienciaGeneral = worksheet.Cells[row, 51].Value.ToString();
                        rpta = validator.ValidarSoloNumeros(experienciaGeneral, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }

                    // Validación COL 52: EXPERIENCIA EN LA MATERIA
                    campo = campos[51];
                    string experienciaLab = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 52].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(experienciaLab, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 52].Value is null))
                    {
                        experienciaLab = worksheet.Cells[row, 52].Value.ToString();
                        rpta = validator.ValidarSoloNumeros(experienciaLab, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }

                    // Validación COL 53: EXPERIENCIA ESPECIFICA
                    campo = campos[52];
                    string expEspecifica = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 53].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(expEspecifica, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 53].Value is null))
                    {
                        expEspecifica = worksheet.Cells[row, 53].Value.ToString();
                        rpta = validator.ValidarSoloNumeros(expEspecifica, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }

                    // Validación COL 54: FECHA ULTIMO DIA CONTRATO
                    campo = campos[53];
                    string fechaUltimoDia = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 54].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(fechaUltimoDia, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 54].Value is null))
                    {
                        fechaUltimoDia = worksheet.Cells[row, 54].Value.ToString();
                        rpta = validator.ValidarFecha(fechaUltimoDia, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }

                    // Validación COL 55: FECHA RECEPCION CONTRATO
                    campo = campos[54];
                    string fechaRecepcionContrato = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 55].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(fechaRecepcionContrato, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 55].Value is null))
                    {
                        fechaRecepcionContrato = worksheet.Cells[row, 55].Value.ToString();
                        rpta = validator.ValidarFecha(fechaRecepcionContrato, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }

                    // Validación COL 56: RES_CONTRATO_Hoja Ruta
                    campo = campos[55];
                    string hojaRutaResContrato = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 56].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(hojaRutaResContrato, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 56].Value is null))
                    {
                        hojaRutaResContrato = worksheet.Cells[row, 56].Value.ToString();
                        rpta = validator.ValidarHojaRuta(hojaRutaResContrato, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }

                    // Validación COL 57: SEC_RESPONSABLE
                    campo = campos[56];
                    string sectoristaResponsable = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 57].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(sectoristaResponsable, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if (!(worksheet.Cells[row, 57].Value is null))
                    {
                        sectoristaResponsable = worksheet.Cells[row, 57].Value.ToString();
                        rpta = validator.ValidarSoloLetras(sectoristaResponsable, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }

                    // Validación COL 58: FISCAL_POST
                    campo = campos[57];
                    string fiscalizacionPosterior = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 58].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(fiscalizacionPosterior, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 59: NOMBRE ENTIDAD FINANCIERA
                    campo = campos[58];
                    string entidadFinanciera = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 59].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(entidadFinanciera, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 60: NRO CUENTA
                    campo = campos[59];
                    string nroCuenta = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 60].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(nroCuenta, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    // Validación COL 61: CCI
                    campo = campos[60];
                    string nroCuentaCCI = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 61].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(nroCuentaCCI, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                }

            }
            return lista;
        }
        
        public List<Respuesta> ValidacionCargaMasivaDataPAC(Stream excelStream, List<Cls_Ent_Entidades> listaEntidades = null, List<Cls_Ent_Ubigeo> listaUbigeos = null)
        {
            //VALIDAR CANTIDAD DE COLUMNAS
            //TABLA CAMPO 
            var lista = new List<Respuesta>();
            Validaciones validar = new Validaciones();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<Cls_Ent_Ubigeo> listaUbigeosExiste = new List<Cls_Ent_Ubigeo>();
            List<Cls_Ent_Entidades> listaEntidadesExiste = new List<Cls_Ent_Entidades>();
            var campos = ListaPac().ToList();
            try {
                using (ExcelPackage package = new ExcelPackage(excelStream))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int columnas = worksheet.Dimension.Columns;

                    if (columnas != campos.Count)
                    {
                        var rpta = new Respuesta();
                        rpta.IsError=true;
                        rpta.SetMensaje(Constants.Mensajes.Error.NroColumnasFormato);
                        lista.Add(rpta);
                        return lista;
                    }

                    string campo = "COD_CONTRATO";

                    for (int row = 2; row <= rowCount; row++)
                    {
                        Catalogo campo_;



                        campo_ = campos[1];

                        if (campo_.Obligatorio.Value)
                        {
                            if (worksheet.Cells[row, 1].Value is null)
                            {
                                Respuesta respuesta = new Respuesta();
                                respuesta.IsError = true;
                                respuesta.SetMensaje("NULL", row, campo, Constants.Mensajes.Error.formatoCodigo);
                                lista.Add(respuesta);
                            }
                            else {
                                if (worksheet.Cells[row, 1].Value.ToString() =="")
                                {
                                    Respuesta respuesta = new Respuesta();
                                    respuesta.IsError = true;
                                    respuesta.SetMensaje("vacio", row, campo, Constants.Mensajes.Error.formatoCodigo);
                                    lista.Add(respuesta);
                                }


                            }

                        }
                    
                       

                        // ALERTA_LA NO OBLIGATORIO
                        campo = "ALERTA_LA";
                        String alerta_la = "";
                        string[] permitidos = new string[3];
                        permitidos[0] = Constants.SiNo.Si;
                        permitidos[1] = Constants.SiNo.No;
                        permitidos[2] = "";
                        if (!(worksheet.Cells[row, 2].Value == null))
                        {
                            Respuesta respuesta = new Respuesta();
                            alerta_la = worksheet.Cells[row, 2].Value.ToString();
                            respuesta = validar.ValoresFijos(permitidos, alerta_la, row, campo);
                            if (respuesta.IsError) {
                                lista.Add(respuesta);
                            }
                        }

                        // ALERTA_LA_PERSONA
                        campo = "ALERTA_LA_PERSONA";
                        String ALERTA_LA_PERSONA = "";
                        campo_ = campos[3];

                        if (campo_.Obligatorio.Value)
                        {
                            if (worksheet.Cells[row, 3].Value is null)
                            {
                                Respuesta respuesta = new Respuesta();
                                ALERTA_LA_PERSONA = worksheet.Cells[row, 3].Value.ToString();
                                respuesta = validar.ValidarSoloLetras(ALERTA_LA_PERSONA, row, campo);
                                if (respuesta.IsError)
                                {
                                    lista.Add(respuesta);
                                }
                            }
                            else {
                                Respuesta respuesta = new Respuesta();
                                respuesta = validar.CampoObligatorio("", row, campo);
                            }
                        }
                        else {
                            if (!(worksheet.Cells[row, 3].Value is null))
                            {
                                Respuesta respuesta = new Respuesta();
                                ALERTA_LA_PERSONA = worksheet.Cells[row, 3].Value.ToString();
                                respuesta = validar.ValidarSoloLetras(ALERTA_LA_PERSONA, row, campo);
                                if (respuesta.IsError)
                                {
                                    lista.Add(respuesta);
                                }
                            }
                        }

                        campo = "FEC_REGISTRO";
                        string FEC_REGISTRO;
                        campo_ = campos[4];

                        if (campo_.Obligatorio.Value)
                        {
                            if (!(worksheet.Cells[row, 4].Value is null))
                            {
                                FEC_REGISTRO = worksheet.Cells[row, 4].Value.ToString();
                                Respuesta respuesta = new Respuesta();
                                respuesta = validar.ValidarFecha(FEC_REGISTRO, row, campo);
                                if (respuesta.IsError)
                                {
                                    lista.Add(respuesta);
                                }
                            }
                            else {
                                Respuesta respuesta = new Respuesta();
                                respuesta = validar.CampoObligatorio("", row, campo);
                            }
                        }
                        else {
                            if (!(worksheet.Cells[row, 4].Value is null))
                            {
                                FEC_REGISTRO = worksheet.Cells[row, 4].Value.ToString();
                                Respuesta respuesta = new Respuesta();
                                respuesta = validar.ValidarFecha(FEC_REGISTRO, row, campo);
                                if (respuesta.IsError)
                                {
                                    lista.Add(respuesta);
                                }
                            }
                        }

                        campo = "MODALIDAD";
                        String MODALIDAD = "";
                        if (!(worksheet.Cells[row, 5].Value == null))
                        {
                            Respuesta respuesta = new Respuesta();
                            MODALIDAD = worksheet.Cells[row, 5].Value.ToString();
                            respuesta = validar.ValidarSoloLetras(MODALIDAD, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        campo = "ESTADO";
                        string[] estados = new string[2];
                        estados[0] = Constants.Estados.Activo.Id;
                        estados[1] = Constants.Estados.Inactivo.Id;
                        String ESTADO = "";
                        if (!(worksheet.Cells[row, 6].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            ESTADO = worksheet.Cells[row, 6].Value.ToString();
                            respuesta = validar.ValoresFijos(estados, ESTADO, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        campo = "NRO_EXP_SISPER";
                        String NRO_EXP_SISPER = "";
                        if (!(worksheet.Cells[row, 7].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            NRO_EXP_SISPER = worksheet.Cells[row, 7].Value.ToString();
                            respuesta = validar.soloAlfanumerico(NRO_EXP_SISPER, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        campo = "APELLIDO_PATERNO";
                        String APELLIDO_PATERNO = "";
                        if (!(worksheet.Cells[row, 8].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            NRO_EXP_SISPER = worksheet.Cells[row, 8].Value.ToString();
                            respuesta = validar.ValidarSoloLetras(APELLIDO_PATERNO.Trim(), row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        campo = "APELLIDO_MATERNO";
                        String APELLIDO_MATERNO = "";
                        if (!(worksheet.Cells[row, 9].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            NRO_EXP_SISPER = worksheet.Cells[row, 9].Value.ToString();
                            respuesta = validar.ValidarSoloLetras(APELLIDO_MATERNO.Trim(), row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }
                        campo = "NOMBRES";
                        String NOMBRES = "";

                        if (!(worksheet.Cells[row, 10].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            NOMBRES = worksheet.Cells[row, 10].Value.ToString();
                            respuesta = validar.ValidarSoloLetras(NOMBRES.Trim(), row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        campo = "DNI";
                        String DNI = "";
                        if (!(worksheet.Cells[row, 11].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            DNI = worksheet.Cells[row, 11].Value.ToString();
                            respuesta = validar.ValidarSoloNumeros(DNI, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        campo = "RUC";
                        String RUC = "";
                        if (!(worksheet.Cells[row, 12].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            RUC = worksheet.Cells[row, 12].Value.ToString();
                            respuesta = validar.ValidarRUC(RUC, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }

                        }

                        // SEXO
                        campo = "SEXO";
                        String SEXO = "";
                        if (!(worksheet.Cells[row, 13].Value is null))
                        {
                            string[] p_sexo = new string[2];
                            p_sexo[0] = Constants.Sexo.Masculino;
                            p_sexo[1] = Constants.Sexo.Femenino;
                            Respuesta respuesta = new Respuesta();
                            SEXO = worksheet.Cells[row, 13].Value.ToString();
                            respuesta = validar.ValoresFijos(p_sexo, SEXO, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }

                        }
                        else {
                            Respuesta respuesta = new Respuesta();
                            respuesta.IsError=true;
                            respuesta.SetMensaje("Registro "+row+", campo:"+campo+" , es obligatorio.");
                            lista.Add(respuesta);
                        }

                        // FEC_NACIMIENTO
                        campo = "FEC_NACIMIENTO";
                        string FEC_NACIMIENTO;
                        if (!(worksheet.Cells[row, 14].Value is null))
                        {
                            FEC_NACIMIENTO = worksheet.Cells[row, 14].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarFecha(FEC_NACIMIENTO, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        // EDAD
                        campo = "EDAD";
                        String EDAD = "";
                        if (!(worksheet.Cells[row, 15].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            EDAD = worksheet.Cells[row, 15].Value.ToString();
                            respuesta = validar.ValidarSoloNumeros(EDAD, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }

                        }

                        // NACIONALIDAD
                        campo = "NACIONALIDAD";
                        String NACIONALIDAD = "";

                        if (!(worksheet.Cells[row, 16].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            NACIONALIDAD = worksheet.Cells[row, 16].Value.ToString();
                            respuesta = validar.ValidarSoloLetras(NACIONALIDAD, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }
                        // PADRE
                        campo = "PADRE";
                        String PADRE = "";

                        if (!(worksheet.Cells[row, 17].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            PADRE = worksheet.Cells[row, 17].Value.ToString();
                            respuesta = validar.ValidarSoloLetras(PADRE, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        // TELEFONO_CELULAR
                        campo = "TELEFONO_CELULAR";
                        String TELEFONO_CELULAR = "";

                        if (!(worksheet.Cells[row, 18].Value is null))
                        {
                            //Respuesta respuesta = new Respuesta();
                            //TELEFONO_CELULAR = worksheet.Cells[row, 18].Value.ToString();
                            //respuesta = validar.ValidarTelefono(TELEFONO_CELULAR, row, campo);
                            //if (respuesta.IsError)
                            //{
                            //    lista.Add(respuesta);
                            //}
                        }

                        // CORREO_ELECTRONICO
                        campo = "CORREO_ELECTRONICO";
                        String CORREO_ELECTRONICO = "";

                        if (!(worksheet.Cells[row, 19].Value is null))
                        {
                            //Respuesta respuesta = new Respuesta();
                            //CORREO_ELECTRONICO = worksheet.Cells[row, 19].Value.ToString();
                            //respuesta = validar.ValidarFormatoCorreo(CORREO_ELECTRONICO, row, campo);
                            //if (respuesta.IsError)
                            //{
                            //    lista.Add(respuesta);
                            //}
                        }
                        // DIRECCION
                        campo = "DIRECCION";
                        if ((worksheet.Cells[row, 20].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            respuesta.IsError=true;
                            respuesta.SetMensaje("Registro " + row + ", campo:" + campo + " , es obligatorio.");
                            lista.Add(respuesta);
                        }

                        if (!(worksheet.Cells[row, 21].Value is null) && !(worksheet.Cells[row, 22].Value is null) && !(worksheet.Cells[row, 23].Value is null))
                        {
                            string distrito = worksheet.Cells[row, 21].Value.ToString();
                            string provincia = worksheet.Cells[row, 22].Value.ToString();
                            string region = worksheet.Cells[row, 23].Value.ToString();

                            Respuesta rpta = new Respuesta();
                            listaUbigeosExiste = listaUbigeos.Where(x => x.CNOMDISTRITO.Trim().ToUpper() == distrito.Trim().ToUpper() && x.CNOMPROVINCIA.Trim().ToUpper() == provincia.Trim().ToUpper() && x.CNOMDEPARTAMENTO.Trim().ToUpper() == region.Trim().ToUpper()).ToList();
                            if (listaUbigeosExiste.Count() == 0)
                            {
                                rpta.IsError = true; ;
                                rpta.SetMensaje(distrito + "/" + provincia + "/" + region, row, "DISTRITO/PROVINCIA/REGION", Constants.Mensajes.Error.UbigeoIncorrecto);
                                lista.Add(rpta);
                            }
                        }

                        // NIVEL_GOBIERNO
                        campo = "NIVEL_GOBIERNO";
                        String NIVEL_GOBIERNO = "";
                        string[] p_ng = new string[2];
                        p_ng[0] = Constants.Gobierno.GobiernoNacional;
                        p_ng[1] = Constants.Gobierno.GobiernoRegional;

                        if (!(worksheet.Cells[row, 24].Value == null))
                        {
                            Respuesta respuesta = new Respuesta();
                            NIVEL_GOBIERNO = worksheet.Cells[row, 24].Value.ToString();
                            respuesta = validar.ValoresFijos(p_ng, NIVEL_GOBIERNO, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        // CODIGO_ENTIDAD
                        campo = "CODIGO_ENTIDAD";
                        //Validando que exista en la lista de Entidades
                        String CODIGO_ENTIDAD = "";
                        if (!(worksheet.Cells[row, 25].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            CODIGO_ENTIDAD = worksheet.Cells[row, 25].Value.ToString();

                            listaEntidadesExiste = listaEntidades.Where(x => x.ACRONIMO == CODIGO_ENTIDAD.Trim().ToUpper()).ToList();
                            if (listaEntidadesExiste.Count() == 0)
                            {
                                respuesta.IsError = true; ;
                                respuesta.SetMensaje(CODIGO_ENTIDAD, row, campo, Constants.Mensajes.Error.EntidadIncorrecta);
                                lista.Add(respuesta);
                            }
                        }
                        // Validación COL 23: FEC_DOC_SOLICITA_REG
                        campo = "FECHA";
                        String FECHA_SOLICITA = "";
                        if (!(worksheet.Cells[row, 27].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();

                            FECHA_SOLICITA = worksheet.Cells[row, 27].Value.ToString();
                            var valida = validar.ValidarFecha(FECHA_SOLICITA);

                            respuesta.IsError = valida.IsError;

                            if (valida.IsError)
                            {
                                respuesta.SetMensaje(FECHA_SOLICITA, row, campo, Constants.Mensajes.Error.FormatoFecha);
                                lista.Add(respuesta);
                            }
                        }

                        // TIPO_DOC_SOLICITA_REG


                        // FEC_DOC_SOLICITA_REG
                        campo = "FEC_DOC_SOLICITA_REG";
                        string FEC_DOC_SOLICITA_REG;
                        if (!(worksheet.Cells[row, 27].Value is null))
                        {
                            FEC_DOC_SOLICITA_REG = worksheet.Cells[row, 27].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarFecha(FEC_DOC_SOLICITA_REG, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        // HR_DOC_SOLICITA_REG
                         campo = "HR_DOC_SOLICITA_REG";
                        string HR_DOC_SOLICITA_REG;
                        if (!(worksheet.Cells[row, 28].Value is null))
                        {
                            HR_DOC_SOLICITA_REG = worksheet.Cells[row, 28].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarHojaRuta(HR_DOC_SOLICITA_REG, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }


                        //// DOC_RES_MINISTERIAL
                        
                        // ENTIDAD_BENEF
                        campo = "ENTIDAD_BENEF";
                        String ENTIDAD_BENEF = "";

                        if (!(worksheet.Cells[row, 30].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            ENTIDAD_BENEF = worksheet.Cells[row, 30].Value.ToString();

                            listaEntidadesExiste = listaEntidades.Where(x => x.ACRONIMO == ENTIDAD_BENEF.Trim().ToUpper()).ToList();
                            if (listaEntidadesExiste.Count() == 0)
                            {
                                respuesta.IsError = true; ;
                                respuesta.SetMensaje(ENTIDAD_BENEF, row, "ENTIDAD", Constants.Mensajes.Error.EntidadIncorrecta);
                                lista.Add(respuesta);
                            }
                        }

                        

                        // ALTA_DIRECCION
                        campo = "ALTA_DIRECCION";
                        String ALTA_DIRECCION = "";

                        if (!(worksheet.Cells[row, 31].Value is null))
                        {
                            //Respuesta respuesta = new Respuesta();
                            //ALTA_DIRECCION = worksheet.Cells[row, 31].Value.ToString();
                            //respuesta = validar.ValidarSoloLetras(ALTA_DIRECCION, row, campo);
                            //if (respuesta.IsError)
                            //{
                            //    lista.Add(respuesta);
                            //}
                        }

                        // DEPENDENCIA_PRESTACION
                        campo = "DEPENDENCIA_PRESTACION";
                        String DEPENDENCIA_PRESTACION = "";

                        if (!(worksheet.Cells[row, 32].Value is null))
                        {
                            //Respuesta respuesta = new Respuesta();
                            //DEPENDENCIA_PRESTACION = worksheet.Cells[row, 32].Value.ToString();
                            //respuesta = validar.ValidarSoloLetras(DEPENDENCIA_PRESTACION, row, campo);
                            //if (respuesta.IsError)
                            //{
                            //    lista.Add(respuesta);
                            //}
                        }


                        // CARGO_FUNCIONARIO_SUSCRIBE
                        campo = "CARGO_FUNCIONARIO_SUSCRIBE";
                        String CARGO_FUNCIONARIO_SUSCRIBE = "";

                        if (!(worksheet.Cells[row, 34].Value is null))
                        {
                            //Respuesta respuesta = new Respuesta();
                            //CARGO_FUNCIONARIO_SUSCRIBE = worksheet.Cells[row, 34].Value.ToString();
                            //respuesta = validar.ValidarSoloLetras(CARGO_FUNCIONARIO_SUSCRIBE, row, campo);
                            //if (respuesta.IsError)
                            //{
                            //    lista.Add(respuesta);
                            //}
                        }

                        // IMPORTE_HONORARIO

                        campo = "IMPORTE_HONORARIO";
                        String IMPORTE_HONORARIO = "";
                        if (!(worksheet.Cells[row, 35].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            IMPORTE_HONORARIO = worksheet.Cells[row, 35].Value.ToString();
                            respuesta = validar.ValidarMontosDecimales(IMPORTE_HONORARIO, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        // FEC_SUSCRIPCION
                        campo = "FEC_SUSCRIPCION";
                        string FEC_SUSCRIPCION;
                        if (!(worksheet.Cells[row, 36].Value is null))
                        {
                            FEC_SUSCRIPCION = worksheet.Cells[row, 36].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarFecha(FEC_SUSCRIPCION, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }
                        // FEC_INICIO
                        campo = "FEC_INICIO";
                        string FEC_INICIO;
                        if (!(worksheet.Cells[row, 37].Value is null))
                        {
                            FEC_INICIO = worksheet.Cells[row, 37].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarFecha(FEC_INICIO, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }
                        // FEC_CULMINACION
                        campo = "FEC_CULMINACION";
                        string FEC_CULMINACION;
                        if (!(worksheet.Cells[row, 38].Value is null))
                        {
                            FEC_CULMINACION = worksheet.Cells[row, 38].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarFecha(FEC_CULMINACION, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }
                        // FEC_RECEPCION
                        campo = "FEC_RECEPCION";
                        string FEC_RECEPCION;
                        if (!(worksheet.Cells[row, 39].Value is null))
                        {
                            FEC_RECEPCION = worksheet.Cells[row, 39].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarFecha(FEC_RECEPCION, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        // NRO_OFICIO


                        //// EXP_REMITIDO
                        //respuesta = validar.soloAlfanumerico(worksheet.Cells[row, 46].Value.ToString());
                        //if (respuesta.IsError) lista.Add(respuesta);
                        //// ADENDA_VIG_DOC_SOL
                        //respuesta = validar.soloAlfanumerico(worksheet.Cells[row, 47].Value.ToString());
                        //if (respuesta.IsError) lista.Add(respuesta);
                        //// ADENDA_VIG_NRO
                        //respuesta = validar.soloAlfanumerico(worksheet.Cells[row, 48].Value.ToString());
                        //if (respuesta.IsError) lista.Add(respuesta);
                        // ADENDA_VIG_FEC_INICIO
                        campo = "ADENDA_VIG_FEC_INICIO";
                        string ADENDA_VIG_FEC_INICIO;
                        if (!(worksheet.Cells[row, 44].Value is null))
                        {
                            ADENDA_VIG_FEC_INICIO = worksheet.Cells[row, 44].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarFecha(ADENDA_VIG_FEC_INICIO, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }
                        // ADENDA_VIG_FEC_FIN
                        campo = "ADENDA_VIG_FEC_FIN";
                        string ADENDA_VIG_FEC_FIN;
                        if (!(worksheet.Cells[row, 45].Value is null))
                        {
                            ADENDA_VIG_FEC_FIN = worksheet.Cells[row, 45].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarFecha(ADENDA_VIG_FEC_FIN, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }
                        //// ADENDA_VIG_HOJARUTA
                        //respuesta = validar.soloAlfanumerico(worksheet.Cells[row, 51].Value.ToString());
                        //if (respuesta.IsError) lista.Add(respuesta);


                        // NOT_FEC_RECEPCION


                        //// NOT_DOCUMENTO
                       

                        // FEC_CULMINACION_CONTRATO
                        campo = "FEC_CULMINACION_CONTRATO";
                        string FEC_CULMINACION_CONTRATO;
                        if (!(worksheet.Cells[row, 48].Value is null))
                        {
                            FEC_CULMINACION_CONTRATO = worksheet.Cells[row, 48].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarFecha(FEC_CULMINACION_CONTRATO, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        // GRADO_ACADEMICO_GEN
                        String GRADO_ACADEMICO_GEN = "";
                        string[] p_ga = new string[2];
                        p_ga[0] = Constants.GradoAcademico.Titulado;
                        p_ga[1] = Constants.GradoAcademico.Bachiller;

                        if (!(worksheet.Cells[row, 49].Value == null))
                        {
                            Respuesta respuesta = new Respuesta();
                            GRADO_ACADEMICO_GEN = worksheet.Cells[row, 49].Value.ToString();
                            respuesta = validar.ValoresFijos(p_ga, GRADO_ACADEMICO_GEN, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        // UNIVERSIDAD
                        campo = "UNIVERSIDAD";
                        String UNIVERSIDAD = "";
                        if (!(worksheet.Cells[row, 50].Value is null))
                        {
                            //Respuesta respuesta = new Respuesta();
                            //UNIVERSIDAD = worksheet.Cells[row, 50].Value.ToString();
                            //respuesta = validar.ValidarSoloLetras(UNIVERSIDAD, row, campo);
                            //if (respuesta.IsError)
                            //{
                            //    lista.Add(respuesta);
                            //}
                        }



                        //// GRADO_ACADEMICO_ESP
                        //respuesta = validar.soloAlfanumerico(worksheet.Cells[row, 52].Value.ToString());
                        //if (respuesta.IsError) lista.Add(respuesta);


                        // CARRERA_PROFESIONAL
                        campo = "CARRERA_PROFESIONAL";
                        String CARRERA_PROFESIONAL = "";

                        if (!(worksheet.Cells[row, 52].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            CARRERA_PROFESIONAL = worksheet.Cells[row, 52].Value.ToString();
                            respuesta = validar.ValidarSoloLetras(CARRERA_PROFESIONAL.Trim().ToUpper(), row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }


                        //// FISCAL_POST
                       

                        // EXP_LAB_GENERAL
                        campo = "EXP_LAB_GENERAL";
                        String EXP_LAB_GENERAL = "";
                        if (!(worksheet.Cells[row, 54].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            EXP_LAB_GENERAL = worksheet.Cells[row, 54].Value.ToString();
                            respuesta = validar.ValidarSoloNumeros(EXP_LAB_GENERAL, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }

                        }

                        // EXP_LAB_ESPECIFICA
                        campo = "EXP_LAB_ESPECIFICA";
                        String EXP_LAB_ESPECIFICA = "";
                        if (!(worksheet.Cells[row, 55].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            EXP_LAB_ESPECIFICA = worksheet.Cells[row, 55].Value.ToString();
                            respuesta = validar.ValidarSoloNumeros(EXP_LAB_ESPECIFICA.Trim().ToUpper(), row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }

                        }

                        // RES_CONTRATO_FEC_CULM
                        campo = "RES_CONTRATO_FEC_CULM";
                        string RES_CONTRATO_FEC_CULM;
                        if (!(worksheet.Cells[row, 56].Value is null))
                        {
                            RES_CONTRATO_FEC_CULM = worksheet.Cells[row, 56].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarFecha(RES_CONTRATO_FEC_CULM, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        // RES_CONTRATO_DOC


                        // RES_CONTRATO_HR
                        campo = "RES_CONTRATO_HR";
                        string RES_CONTRATO_HR;
                        if (!(worksheet.Cells[row, 56].Value is null))
                        {
                            RES_CONTRATO_HR = worksheet.Cells[row, 56].Value.ToString();
                            Respuesta respuesta = new Respuesta();
                            respuesta = validar.ValidarHojaRuta(RES_CONTRATO_HR, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }


                        // SEC_RESPONSABLE
                        campo = "SEC_RESPONSABLE";
                        String SEC_RESPONSABLE = "";

                        if (!(worksheet.Cells[row, 59].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            SEC_RESPONSABLE = worksheet.Cells[row, 59].Value.ToString();
                            respuesta = validar.ValidarSoloLetras(SEC_RESPONSABLE, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }


                        // ENT_FIN_NOMBRE
                        campo = "ENT_FIN_NOMBRE";
                        String ENT_FIN_NOMBRE = "";

                        if (!(worksheet.Cells[row, 60].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            NACIONALIDAD = worksheet.Cells[row, 60].Value.ToString();
                            respuesta = validar.ValidarSoloLetras(ENT_FIN_NOMBRE, row, campo);
                            if (respuesta.IsError)
                            {
                                lista.Add(respuesta);
                            }
                        }

                        //// ENT_FIN_CTA_AHORRO
                        //respuesta = validar.soloAlfanumerico(worksheet.Cells[row, 62].Value.ToString());
                        //if (respuesta.IsError) lista.Add(respuesta);
                        //// ENT_FIN_CTA_CCI
                        //respuesta = validar.soloAlfanumerico(worksheet.Cells[row, 63].Value.ToString());
                        //if (respuesta.IsError) lista.Add(respuesta);


                        // PJE_PUESTO
                        campo = "PJE_PUESTO";
                        String PJE_PUESTO = "";
                        if ((worksheet.Cells[row, 63].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            respuesta.IsError=true;
                            //respuesta.SetMensaje("Registro:"+row+", campo ogligatorio:"+campo  ); ;
                            respuesta.SetMensaje("", row, campo, "El campo es obligatorio");

                            lista.Add(respuesta);
                           

                        }
                    }

                }
            }
            catch(Exception ex)
            {
                Respuesta respuesta = new Respuesta();
                respuesta.IsError=true;
                respuesta.SetMensaje("FORMATO INCORRECTO:"+ex.Message);
                lista.Add(respuesta);
            }
           
            return lista;
        }
        
        public List<Respuesta> ValidacionAdendas(Stream excelStream)
        {
            var lista = new List<Respuesta>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(excelStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                if(worksheet.Dimension.Columns != ListaAdendas().Count)
                {
                    var rpta = new Respuesta();
                    rpta.IsError = true;;
                    rpta.SetMensaje(Constants.Mensajes.Error.NroColumnasFormato);
                    lista.Add(rpta);

                    return lista;
                }

                var validator = new Validaciones();
                var campos = ListaAdendas().ToList();

                for(int row = 3; row <= rowCount; row++)
                {
                    Catalogo campo;
                    var rpta = new Respuesta();

                    //Validación COL 1: COD_CONTRATO
                    campo = campos[0];
                    string codContrato = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 1].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(codContrato, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    //Validación COL 2: DOC_SOLICITUD
                    campo = campos[1];
                    string docSolicitud = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 2].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(docSolicitud, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    //Validación COL 3: HOJA_RUTA
                    campo = campos[2];
                    string hojaRuta = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 3].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(hojaRuta, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }

                    //Validación COL 4: NRO
                    campo = campos[3];
                    string nro = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 4].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(nro, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if(!(worksheet.Cells[row, 4].Value is null))
                    {
                        nro = worksheet.Cells[row, 4].Value.ToString();
                        rpta = validator.ValidarSoloNumeros(nro, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }

                    //Validación COL 5: FECHA_INICIO
                    campo = campos[4];
                    string fechaInicio = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 5].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(fechaInicio, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if(!(worksheet.Cells[row, 5].Value is null))
                    {
                        fechaInicio = worksheet.Cells[row, 5].Value.ToString();
                        rpta = validator.ValidarFecha(fechaInicio, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }

                    //Validación COL 6: FECHA_CULMINACION
                    campo = campos[5];
                    string fechaCulminacion = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 6].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(fechaCulminacion, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if(!(worksheet.Cells[row, 6].Value is null))
                    {
                        fechaCulminacion = worksheet.Cells[row, 6].Value.ToString();
                        rpta = validator.ValidarFecha(fechaCulminacion, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }
                    //Validación COL 7: FECHA_RECEPCION
                    campo = campos[6];
                    string fechaRecepcion = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 7].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(fechaRecepcion, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    if(!(worksheet.Cells[row, 7].Value is null))
                    {
                        fechaRecepcion = worksheet.Cells[row, 7].Value.ToString();
                        rpta = validator.ValidarFecha(fechaRecepcion, row, campo.Descripcion);
                        if (rpta.IsError) lista.Add(rpta);
                    }

                    //Validación COL 8: DOCUMENTO
                    campo = campos[7];
                    string documento = string.Empty;
                    if (campo.Obligatorio.Value)
                    {
                        if(worksheet.Cells[row, 8].Value is null)
                        {
                            rpta.IsError = true;;
                            rpta.SetMensaje(documento, row, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                            lista.Add(rpta);
                        }
                    }
                    
                }
            }
            return lista;
        }
        public List<Respuesta> ValidacionCargaMasivaDataPag(Stream excelStream, List<Cls_Ent_Entidades> listaEntidades = null)
        {
            //VALIDAR CANTIDAD DE COLUMNAS
            //TABLA CAMPO 
            var lista = new List<Respuesta>();
            //Validaciones validar = new Validaciones();
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //List<Cls_Ent_Ubigeo> listaUbigeosExiste = new List<Cls_Ent_Ubigeo>();
            List<Cls_Ent_Entidades> listaEntidadesExiste = new List<Cls_Ent_Entidades>();

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(excelStream))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int columnas = worksheet.Dimension.Columns;
                    Respuesta rpta;

                    if (columnas != ListaPagos().Count)
                    {
                        rpta = new Respuesta();
                        rpta.IsError = true;
                        rpta.SetMensaje(Constants.Mensajes.Error.NroColumnasFormato);
                        lista.Add(rpta);
                        return lista;
                    }

                    Validaciones validar = new Validaciones();
                    var campos = ListaPagos().ToList();

                    var coltrab = 0; var coldoc = 1; var colruc = 2; var coldep = 3; var colmon = 4;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        //COD_CONTRATO
                        validarObligatorio(campos[coltrab], worksheet.Cells[row, coltrab + 1].Value, row, lista);
                        //DOCUMENTO
                        validarObligatorio(campos[coldoc], worksheet.Cells[row, coldoc + 1].Value, row, lista);
                        validarCampo(lista, validar.ValidarSoloNumeros(worksheet.Cells[row, coldoc + 1].Value.ToString(), row, campos[coldoc].Descripcion));
                        //RUC_CAS
                        validarObligatorio(campos[colruc], worksheet.Cells[row, colruc + 1].Value, row, lista);
                        validarCampo(lista, validar.ValidarRUC(worksheet.Cells[row, colruc + 1].Value.ToString(), row, campos[colruc].Descripcion));
                        //NOMBRE_DEPENDENCIA
                        validarObligatorio(campos[coldep], worksheet.Cells[row, coldep + 1].Value, row, lista);
                        validarCampo(lista, validar.ValidarSoloLetras(worksheet.Cells[row, coldep + 1].Value.ToString(), row, campos[coldep].Descripcion));
                        //Validando que exista en la lista de Entidades
                        String ENTIDAD_BENEF = "";
                        if (!(worksheet.Cells[row, coldep + 1].Value is null))
                        {
                            Respuesta respuesta = new Respuesta();
                            ENTIDAD_BENEF = worksheet.Cells[row, coldep + 1].Value.ToString();

                            listaEntidadesExiste = listaEntidades.Where(x => x.ACRONIMO == ENTIDAD_BENEF.Trim().ToUpper()).ToList();
                            if (listaEntidadesExiste.Count() == 0)
                            {
                                respuesta.IsError = true; ;
                                respuesta.SetMensaje(ENTIDAD_BENEF, row, "ENTIDAD", Constants.Mensajes.Error.EntidadIncorrecta);
                                lista.Add(respuesta);
                            }
                        }

                        //Validando montos mensuales
                        for (int iMes = 0; iMes < 12; iMes++)
                        {
                            for (int iTipo = 0; iTipo < 4; iTipo++)
                            {
                                validarObligatorio(campos[colmon + iMes + iTipo], worksheet.Cells[row, colmon + 1 + iMes + iTipo].Value, row, lista);
                                validarCampo(lista, validar.ValidarMontosDecimales(worksheet.Cells[row, colmon + 1 + iMes + iTipo].Value.ToString(), row, campos[colmon + iMes + iTipo].Descripcion));
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Respuesta respuesta = new Respuesta();
                respuesta.IsError = true;
                respuesta.SetMensaje("FORMATO INCORRECTO:" + ex.Message);
                lista.Add(respuesta);
            }

            return lista;
        }

        private List<Respuesta> validarObligatorio(Catalogo campo, object celda, int fila, List<Respuesta> lista) {
            var rpta = new Respuesta();

            if (campo.Obligatorio.Value)
            {
                if (celda is null)
                {
                    rpta.IsError = true;
                    rpta.SetMensaje(string.Empty, fila, campo.Descripcion, Constants.Mensajes.Error.Requerido);
                    lista.Add(rpta);
                }
            }

            return lista;
        }

        private List<Respuesta> validarCampo(List<Respuesta> lista, Respuesta respuesta) {
            Validaciones validar = new Validaciones();
            var rpta = new Respuesta();
            rpta = respuesta;
            if (rpta.IsError)
            {
                lista.Add(rpta);
            }
            return lista;
        }
        #endregion Validaciones


        #region DataTable
        private DataTable DT_DataFagPac()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID_REGISTRO");
            dt.Columns.Add("ID_CARGA");
            dt.Columns.Add("COD_CONTRATO");
            dt.Columns.Add("ALERTA_LA");
            dt.Columns.Add("ALERTA_LA_PERSONA");
            dt.Columns.Add("FEC_REGISTRO");
            dt.Columns.Add("MODALIDAD");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("NRO_EXP_SISPER");
            dt.Columns.Add("LEY31419");
            
            dt.Columns.Add("APELLIDO_PATERNO");
            dt.Columns.Add("APELLIDO_MATERNO");
            dt.Columns.Add("NOMBRES");
            dt.Columns.Add("DNI");
            dt.Columns.Add("RUC");
            dt.Columns.Add("SEXO");
            dt.Columns.Add("FEC_NACIMIENTO");
            dt.Columns.Add("EDAD");
            dt.Columns.Add("NACIONALIDAD");
            dt.Columns.Add("PADRE");
            
            dt.Columns.Add("TELEFONO_CELULAR");
            dt.Columns.Add("CORREO_ELECTRONICO");
            dt.Columns.Add("DIRECCION");
            dt.Columns.Add("DISTRITO");
            dt.Columns.Add("PROVINCIA");
            dt.Columns.Add("REGION");
            dt.Columns.Add("NIVEL_GOBIERNO");
            dt.Columns.Add("CODIGO_ENTIDAD");
            dt.Columns.Add("TIPO_DOC_SOLICITA_REG");
            dt.Columns.Add("FEC_DOC_SOLICITA_REG");
            
            dt.Columns.Add("HR_DOC_SOLICITA_REG");
            dt.Columns.Add("DOC_RES_MINISTERIAL");
            dt.Columns.Add("ENTIDAD_BENEF");
            dt.Columns.Add("FLAG_DESIGNADO");
            dt.Columns.Add("ALTA_DIRECCION");
            dt.Columns.Add("DEPENDENCIA_PRESTACION");
            dt.Columns.Add("CARGO_ANEXO1");
            dt.Columns.Add("CARGO_FUNCIONARIO_SUSCRIBE");
            dt.Columns.Add("IMPORTE_HONORARIO");
            dt.Columns.Add("FEC_SUSCRIPCION");

            dt.Columns.Add("FEC_INICIO");
            dt.Columns.Add("FEC_CULMINACION");
            dt.Columns.Add("FEC_RECEPCION");
            dt.Columns.Add("NRO_OFICIO");
            dt.Columns.Add("FIN_CONTRATO");
            dt.Columns.Add("EXP_REMITIDO");
            dt.Columns.Add("ADENDA_VIG_DOC_SOL");
            dt.Columns.Add("ADENDA_VIG_NRO");
            dt.Columns.Add("ADENDA_VIG_FEC_INICIO");
            dt.Columns.Add("ADENDA_VIG_FEC_FIN");
            
            dt.Columns.Add("ADENDA_VIG_HOJARUTA");
            dt.Columns.Add("ADENDA_VIG_SUSCRIPCION");
            dt.Columns.Add("NOT_FEC_RECEPCION");
            dt.Columns.Add("NOT_DOCUMENTO");
            dt.Columns.Add("FEC_CULMINACION_CONTRATO");
            dt.Columns.Add("GRADO_ACADEMICO_GEN");
            dt.Columns.Add("GRADO_ACADEMICO_FAG");
            dt.Columns.Add("CARRERA_PROFESIONAL");
            dt.Columns.Add("UNIVERSIDAD");
            dt.Columns.Add("FLAG_REG_SUNEDU");
            
            dt.Columns.Add("GRADO_ACADEMICO_ESP");
            dt.Columns.Add("REQ_HAB_PROF");
            dt.Columns.Add("EXP_LAB_GENERAL");
            dt.Columns.Add("EXP_LAB_ESPECIFICA");
            dt.Columns.Add("EXP_LAB_MATERIA");
            dt.Columns.Add("EXP_LAB_GENERAL_GRADO");
            dt.Columns.Add("EXP_LAB_ACT_TDRS");
            dt.Columns.Add("RES_CONTRATO_FEC_CULM");
            dt.Columns.Add("RES_CONTRATO_DOC");
            dt.Columns.Add("RES_CONTRATO_HR");

            dt.Columns.Add("SEC_RESPONSABLE");
            dt.Columns.Add("FISCAL_POST");
            dt.Columns.Add("ENT_FIN_NOMBRE");
            dt.Columns.Add("ENT_FIN_CTA_AHORRO");
            dt.Columns.Add("ENT_FIN_CTA_BANCARIA");
            dt.Columns.Add("ENT_FIN_CTA_CCI");
            dt.Columns.Add("PJE_PUESTO");
            dt.Columns.Add("FLG_ESTADO");
            dt.Columns.Add("FLG_PROCESADO");

            return dt;
        }
        private DataTable DT_DataAdendas()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("ID_CARGA_DETALLE");
            dt.Columns.Add("ID_CARGA");
            dt.Columns.Add("COD_CONTRATO");
            dt.Columns.Add("DOC_SOLICITUD");
            dt.Columns.Add("HOJA_RUTA");
            dt.Columns.Add("FEC_SUSCRIPCION");
            dt.Columns.Add("NUMERO");
            dt.Columns.Add("FEC_INICIO");
            dt.Columns.Add("FEC_FIN");
            dt.Columns.Add("FEC_RECEPCION");
            dt.Columns.Add("DOCUMENTO");

            return dt;
        }
        private DataTable DT_DataPagos()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID_REGISTRO");
            dt.Columns.Add("ID_CARGA");
            dt.Columns.Add("PERIODO");
            foreach (var item in ListaPagos().ToList())
            {
                dt.Columns.Add(item.Id);
            }
            dt.Columns.Add("FLG_ESTADO");

            return dt;
        }
        #endregion DataTable


        #region Concepto Campos 
        public List<Catalogo> ListaFag()
        {
            List<Catalogo> listaFag = new List<Catalogo>
            {
                new Catalogo() { Id = "COD_CONTRATO", Descripcion = "CODIGO CONTRATO", Obligatorio = true, Ejemplo = "" },
                new Catalogo() { Id = "FEC_REGISTRO", Descripcion = "FECHA DE REGISTRO", Obligatorio = false, Ejemplo = "dd/MM/yyyy 00:00" },
                new Catalogo() { Id = "ESTADO", Descripcion = "ESTADO", Obligatorio = true, Ejemplo = "Activo (A), Baja (B), Devolución (D)" },
                new Catalogo() { Id = "NRO_EXP_SISPER", Descripcion = "EXPEDIENTE NRO. SIS", Obligatorio = false, Ejemplo = "004900" },
                new Catalogo() { Id = "LEY31419", Descripcion = "LEY 31419", Obligatorio = false, Ejemplo = "SI o NO" },
                new Catalogo() { Id = "APELLIDO_PATERNO", Descripcion = "APELLIDO PATERNO", Obligatorio = true, Ejemplo = "JIMÉNEZ" },
                new Catalogo() { Id = "APELLIDO_MATERNO", Descripcion = "APELLIDO MATERNO", Obligatorio = false, Ejemplo = "MENDOZA" },
                new Catalogo() { Id = "NOMBRES", Descripcion = "NOMBRES", Obligatorio = true, Ejemplo = "JUAN LUIS" },
                new Catalogo() { Id = "DNI", Descripcion = "DNI O CARNET DE EXTRANJERIA", Obligatorio = true, Ejemplo = "85456987" },
                new Catalogo() { Id = "RUC", Descripcion = "RUC", Obligatorio = true, Ejemplo = "10442614925" },
                
                new Catalogo() { Id = "SEXO", Descripcion = "SEXO", Obligatorio = true, Ejemplo = "Femenino (F) o Masculino (M)" },
                new Catalogo() { Id = "FEC_NACIMIENTO", Descripcion = "FECHA DE NACIMIENTO", Obligatorio = true, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "EDAD", Descripcion = "EDAD", Obligatorio = false, Ejemplo = "33" },
                new Catalogo() { Id = "TELEFONO_CELULAR", Descripcion = "TELÉFONO CELULAR Y/O TELEFONO FIJO", Obligatorio = false, Ejemplo = "981575525" },
                new Catalogo() { Id = "CORREO_ELECTRONICO", Descripcion = "CORREO ELECTRONICO", Obligatorio = false, Ejemplo = "kleinht.acuña@gmail.com" },
                new Catalogo() { Id = "DIRECCION", Descripcion = "DIRECCIÓN CALLE/JIRÓN/AVENIDA NRO.", Obligatorio = false, Ejemplo = "MZ C LOTE 101 COOPERATIVA PNP LA FRAGATA" },
                new Catalogo() { Id = "DISTRITO", Descripcion = "DISTRITO", Obligatorio = false, Ejemplo = "LA MOLIDA" },
                new Catalogo() { Id = "PROVINCIA", Descripcion = "PROVINCIA", Obligatorio = false, Ejemplo = "LIMA" },
                new Catalogo() { Id = "REGION", Descripcion = "REGIÓN", Obligatorio = false, Ejemplo = "ANCASH" },
                new Catalogo() { Id = "NIVEL_GOBIERNO", Descripcion = "GOBIERNO", Obligatorio = true, Ejemplo = "Gobierno Nacional (1) o Gobierno Regional (2)" },
                
                new Catalogo() { Id = "CODIGO_ENTIDAD", Descripcion = "ENTIDAD", Obligatorio = true, Ejemplo = "MEF" },
                new Catalogo() { Id = "TIPO_DOC_SOLICITA_REG", Descripcion = "TIPO", Obligatorio = false, Ejemplo = "MEMORANDO 008-2021-EF/11.01" },
                new Catalogo() { Id = "FEC_DOC_SOLICITA_REG", Descripcion = "FECHA", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "HR_DOC_SOLICITA_REG", Descripcion = "HOJA DE RUTA NRO.", Obligatorio = false, Ejemplo = "03253" },
                //NOMBRE ENTIDAD
                new Catalogo() { Id = "FLAG_DESIGNADO", Descripcion = "DESIGNADO", Obligatorio = false, Ejemplo = "SI o NO" },
                new Catalogo() { Id = "ALTA_DIRECCION", Descripcion = "ORGANO DE ALTA DIRECCIÓN MEF", Obligatorio = false, Ejemplo = "DESPACHO VICEMINISTERIAL DE HACIENDA" },
                new Catalogo() { Id = "DEPENDENCIA_PRESTACION", Descripcion = "DEPENDENCIA DONDE SE PRESTA SERVICIOS, SEGÚN ANEXO NRO.1", Obligatorio = true, Ejemplo = "DIRECCIÓN GENERAL DE GESTIÓN FISCAL DE LOS RECURSOS HUMANOS" },
                new Catalogo() { Id = "CARGO_ANEXO1", Descripcion = "CARGO, SEGÚN ANEXO NRO. 1", Obligatorio = true, Ejemplo = "CONSULTOR FAG" },
                new Catalogo() { Id = "CARGO_FUNCIONARIO_SUSCRIBE", Descripcion = "CARGO DEL FUNCIONARIO QUE OTORGA LA CONFORMIDAD", Obligatorio = true, Ejemplo = "GOBERNADOR REGIONAL" },

                new Catalogo() { Id = "IMPORTE_HONORARIO", Descripcion = "IMPORTE MENSUAL DE HONORARIOS S/.", Obligatorio = true, Ejemplo = "7000" },
                new Catalogo() { Id = "FEC_INICIO", Descripcion = "FECHA DE INICIO", Obligatorio = true, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "FEC_CULMINACION", Descripcion = "FECHA DE CULMINACIÓN", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "FEC_RECEPCION", Descripcion = "FECHA DE RECEPCIÓN POR EL MEF", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "FEC_SUSCRIPCION", Descripcion = "FECHA DE SUSCRIPCIÓN", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "FIN_CONTRATO", Descripcion = "FECHA FIN DEL CONTRATO", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "EXP_REMITIDO", Descripcion = "EXPEDIENTE REMITIDO", Obligatorio = false, Ejemplo = "FISICO o VIRTUAL" },
                new Catalogo() { Id = "NRO_OFICIO", Descripcion = "NRO.", Obligatorio = false, Ejemplo = "001" },
                new Catalogo() { Id = "ADENDA_VIG_FEC_INICIO", Descripcion = "INICIO", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "ADENDA_VIG_FEC_FIN", Descripcion = "FIN", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },

                new Catalogo() { Id = "ADENDA_VIG_HOJARUTA", Descripcion = "HOJA DE RUTA NRO.", Obligatorio = false, Ejemplo = "066630-2023" },
                new Catalogo() { Id = "ADENDA_VIG_SUSCRIPCION", Descripcion = "FECHA DE SUSCRIPCION", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "GRADO_ACADEMICO_GEN", Descripcion = "GRADO ACADEMICO", Obligatorio = false, Ejemplo = "Bachiller (1) o Titulado (2)" },
                new Catalogo() { Id = "GRADO_ACADEMICO_FAG", Descripcion = "GRADO ACADEMICO 2", Obligatorio = false, Ejemplo = "Bachiller (1), Título Profesional (2), Grado de Magister o Doctorado (3)" },
                new Catalogo() { Id = "EXP_LAB_GENERAL", Descripcion = "EXPERIENCIA GENERAL (AÑOS)", Obligatorio = false, Ejemplo = "2, 3, 4, ..." },
                new Catalogo() { Id = "EXP_LAB_MATERIA", Descripcion = "EXPERIENCIA ESPECIFICA EN LA FUNCIÓN Y/O EN LA MATERIA (AÑOS)", Obligatorio = false, Ejemplo = "4" },
                new Catalogo() { Id = "CARRERA_PROFESIONAL", Descripcion = "TÍTULO O GRADO ACADÉMICO", Obligatorio = true, Ejemplo = "LICENCIADO EN PSICOLOGÍA" },
                new Catalogo() { Id = "UNIVERSIDAD", Descripcion = "UNIVERSIDAD", Obligatorio = true, Ejemplo = "UNIVERSIDAD DE LIMA, UNIVERSIDAD ALAS PERUANAS" },
                new Catalogo() { Id = "FLAG_REG_SUNEDU", Descripcion = "SUNEDU (REGISTRADO TITULO)", Obligatorio = false, Ejemplo = "SI o NO" },
                new Catalogo() { Id = "GRADO_ACADEMICO_ESP", Descripcion = "GRADO", Obligatorio = false, Ejemplo = "Bachiller (1), Titulado (2), Estudios de Maestria (3), Magister (4), Estudios Doctorado (5), Doctorado(6)" },
                
                new Catalogo() { Id = "REQ_HAB_PROF", Descripcion = "REQUISITO HABILITACIÓN PROFESIONAL", Obligatorio = false, Ejemplo = "SI o NO" },
                new Catalogo() { Id = "EXP_LAB_GENERAL_GRADO", Descripcion = "EXPERIENCIA GENERAL (DESDE EL GRADO BACHILLER O CONSTANCIA DE EGRESADO)", Obligatorio = false, Ejemplo = "8" },
                new Catalogo() { Id = "EXP_LAB_ACT_TDRS", Descripcion = "EXPERIENCIA EN LA MATERIA O FUNCION, SEGÚN LAS ACTIVIDADES A REALIZAR TDRS", Obligatorio = false, Ejemplo = "4" },
                new Catalogo() { Id = "EXP_LAB_ESPECIFICA", Descripcion = "EXPERIENCIA ESPECIFICA (AÑOS)", Obligatorio = false, Ejemplo = "4" },
                new Catalogo() { Id = "RES_CONTRATO_FEC_CULM", Descripcion = "FECHA ÚLTIMO DÍA DE PRESTACIÓN DE SERVICIOS", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "RES_CONTRATO_DOC", Descripcion = "DOCUMENTO QUE RESUELVE CONTRATO FECHA DE RECEPCIÓN", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "RES_CONTRATO_HR", Descripcion = "HOJA DE RUTA", Obligatorio = false, Ejemplo = "NUM + CARACTERES" },
                new Catalogo() { Id = "SEC_RESPONSABLE", Descripcion = "SECTORISTA RESPONSABLE", Obligatorio = false, Ejemplo = "JORGE PÉREZ" },
                new Catalogo() { Id = "FISCAL_POST", Descripcion = "FISCALIZACIÓN POSTERIOR", Obligatorio = false, Ejemplo = "" },
                new Catalogo() { Id = "ENT_FIN_NOMBRE", Descripcion = "NOMBRE DE ENTIDAD FINANCIERA", Obligatorio = true, Ejemplo = "BBVA CONTINENTAL" },
                new Catalogo() { Id = "ENT_FIN_CTA_AHORRO", Descripcion = "CUENTA DE AHORROS NRO.", Obligatorio = true, Ejemplo = "24591097961067" },
                new Catalogo() { Id = "ENT_FIN_CTA_CCI", Descripcion = "CCI NRO.", Obligatorio = true, Ejemplo = "393893245910979610674" }
            };

            return listaFag;
        }
        public List<Catalogo> ListaPac()
        {
            List<Catalogo> listaPac = new List<Catalogo>
            {
                new Catalogo() { Id = "COD_CONTRATO", Descripcion = "CODIGO CONTRATO", Obligatorio = true, Ejemplo = "" },
                new Catalogo() { Id = "ALERTAS_LA", Descripcion = "ALERTAS LA", Obligatorio = false, Ejemplo = ""},
                new Catalogo() { Id = "ALERTA_LA_PERSONA", Descripcion = "ALERTAS - PERSONAL QUE USÓ LA SUSPECIÓN DE PRESTACIÓN CON CONTRAPRESTACIÓN EN EL MES", Obligatorio = false, Ejemplo = "" },
                new Catalogo() { Id = "FECHA_REGISTRO", Descripcion = "FECHA DE REGISTRO", Obligatorio = false, Ejemplo = "dd/MM/yyyy 00:00" },
                new Catalogo() { Id = "MODALIDAD", Descripcion = "MODALIDAD", Obligatorio = false, Ejemplo = "VIRTUAL O PRESENCIAL" },
                new Catalogo() { Id = "ESTADO", Descripcion = "ESTADO", Obligatorio = true, Ejemplo = "Activo (A), Baja (B), Devolución (D)" },
                new Catalogo() { Id = "EXPEDIENTE_SISPER", Descripcion = "EXPEDIENTE NRO. SIS", Obligatorio = false, Ejemplo = "004900" },
                new Catalogo() { Id = "APELLIDO_PATERNO", Descripcion = "APELLIDO PATERNO", Obligatorio = true, Ejemplo = "JIMÉNEZ" },
                new Catalogo() { Id = "APELLIDO_MATERNO", Descripcion = "APELLIDO MATERNO", Obligatorio = false, Ejemplo = "MENDOZA" },
                new Catalogo() { Id = "NOMBRES", Descripcion = "NOMBRES", Obligatorio = true, Ejemplo = "JUAN LUIS" },
                
                new Catalogo() { Id = "DNI_CARNET_EXTRANJERIA", Descripcion = "DNI O CARNET DE EXTRANJERIA", Obligatorio = true, Ejemplo = "85456987" },
                new Catalogo() { Id = "RUC", Descripcion = "RUC", Obligatorio = true, Ejemplo = "10442614925" },
                new Catalogo() { Id = "SEXO", Descripcion = "SEXO", Obligatorio = true, Ejemplo = "Femenino (F) o Masculino (M)" },
                new Catalogo() { Id = "FECHA_NACIMIENTO", Descripcion = "FECHA DE NACIMIENTO", Obligatorio = true, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "EDAD", Descripcion = "EDAD", Obligatorio = false, Ejemplo = "33" },
                new Catalogo() { Id = "NACIONALIDAD", Descripcion = "NACIONALIDAD", Obligatorio = false, Ejemplo = "Peruana"},
                new Catalogo() { Id = "PADRE", Descripcion = "PADRE", Obligatorio = true, Ejemplo = "JUAN" },
                new Catalogo() { Id = "CELULAR_TELEFONO", Descripcion = "TELÉFONO CELULAR Y/O TELEFONO FIJO", Obligatorio = false, Ejemplo = "981575525" },
                new Catalogo() { Id = "CORREO_ELECTRONICO", Descripcion = "CORREO ELECTRONICO", Obligatorio = false, Ejemplo = "kleinht.acuña@gmail.com" },
                new Catalogo() { Id = "DIRECCION", Descripcion = "DIRECCIÓN CALLE/JIRÓN/AVENIDA NRO.", Obligatorio = false, Ejemplo = "MZ C LOTE 101 COOPERATIVA PNP LA FRAGATA" },
               
                new Catalogo() { Id = "DISTRITO", Descripcion = "DISTRITO", Obligatorio = false, Ejemplo = "LA MOLIDA" },
                new Catalogo() { Id = "PROVINCIA", Descripcion = "PROVINCIA", Obligatorio = false, Ejemplo = "LIMA" },
                new Catalogo() { Id = "REGION", Descripcion = "REGIÓN", Obligatorio = false, Ejemplo = "ANCASH" },
                new Catalogo() { Id = "GOBIERNO", Descripcion = "GOBIERNO", Obligatorio = true, Ejemplo = "Gobierno Nacional (1) o Gobierno Regional (2)" },
                new Catalogo() { Id = "CODIGO_ENTIDAD", Descripcion = "ENTIDAD", Obligatorio = true, Ejemplo = "MEF" },
                new Catalogo() { Id = "TIPO", Descripcion = "TIPO", Obligatorio = false, Ejemplo = "MEMORANDO 008-2021-EF/11.01" },
                new Catalogo() { Id = "FECHA", Descripcion = "FECHA", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "HOJA_RUTA_NRO", Descripcion = "HOJA DE RUTA NRO.", Obligatorio = false, Ejemplo = "03253-2023" },
                new Catalogo() { Id = "DESIGNADO", Descripcion = "DESIGNADO", Obligatorio = false, Ejemplo = "SI o NO" },
                new Catalogo() { Id = "DEPENDENCIA", Descripcion = "ENTIDAD BENEFICIARIA DE ENTIDAD", Obligatorio = false, Ejemplo = "MINEDU" },
               
                new Catalogo() { Id = "ORGANO_ALTA_DIRECCION_MEF", Descripcion = "ORGANO DE ALTA DIRECCIÓN MEF", Obligatorio = false, Ejemplo = "DESPACHO VICEMINISTERIAL DE HACIENDA" },
                new Catalogo() { Id = "DEPENDENCIA_SERVICIOS", Descripcion = "DEPENDENCIA DONDE SE PRESTA SERVICIOS, SEGÚN ANEXO NRO.1", Obligatorio = true, Ejemplo = "DIRECCIÓN GENERAL DE GESTIÓN FISCAL DE LOS RECURSOS HUMANOS" },
                new Catalogo() { Id = "CARGO", Descripcion = "CARGO, SEGÚN ANEXO NRO. 1", Obligatorio = true, Ejemplo = "CONSULTOR PAC" },
                new Catalogo() { Id = "CARGO_FUNCIONARIO", Descripcion = "CARGO DEL FUNCIONARIO QUE OTORGA LA CONFORMIDAD", Obligatorio = true, Ejemplo = "GOBERNADOR REGIONAL" },
                new Catalogo() { Id = "IMPORTE_HONORARIOS", Descripcion = "IMPORTE MENSUAL DE HONORARIOS S/.", Obligatorio = true, Ejemplo = "7000" },
                new Catalogo() { Id = "FECHA_SUSCRIPCION", Descripcion = "FECHA DE SUSCRIPCIÓN", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "FECHA_INICIO", Descripcion = "FECHA DE INICIO", Obligatorio = true, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "FECHA_CULMINACION", Descripcion = "FECHA DE CULMINACIÓN", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "FECHA_RECEPCION_MEF", Descripcion = "FECHA DE RECEPCIÓN POR EL MEF", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "NRO.", Descripcion = "NRO. OFICIO", Obligatorio = true, Ejemplo = "OFICIO N° 607-2023-EF/43.07" },
               
                new Catalogo() { Id = "DOCUMENTO_SOLICITUD", Descripcion = "ADENDA DOCUMENTO DE SOLICITUD", Obligatorio = false, Ejemplo = "5487-98521"},
                new Catalogo() { Id = "DOCUMENTO_NRO", Descripcion = "NRO. DE HOJA DE RUTA", Obligatorio = false, Ejemplo = "123" },
                new Catalogo() { Id = "INCIO", Descripcion = "INICIO", Obligatorio = true, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "FIN", Descripcion = "FIN", Obligatorio = true, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "DOCUMENTO", Descripcion = "DOCUMENTO", Obligatorio = false, Ejemplo = "OFICIO N°035-2023-EF/43.0<7" },
                new Catalogo() { Id = "FECHA_RECEPCION", Descripcion = "FECHA DE RECEPCION", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "GRADO", Descripcion = "GRADO ACADEMICO", Obligatorio = true, Ejemplo = "Bachiller (1) o Titulado (2)" },
                new Catalogo() { Id = "FECHA_CULMINACION_CONTRATO_VIGENTE", Descripcion = "FECHA DE CULMINACIÓN DEL CONTRATO VIGENTE", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "TITULO_GRADO", Descripcion = "TITULO O GRADO ACADÉMICO", Obligatorio = true, Ejemplo = "Bachiller (1), Título Profesional (2), Grado de Magister o Doctorado (3)" },
                new Catalogo() { Id = "UNIVERSIDAD", Descripcion = "UNIVERSIDAD", Obligatorio = true, Ejemplo = "UNIVERSIDAD DE LIMA, UNIVERSIDAD ALAS PERUANAS" },

                new Catalogo() { Id = "GRADO_2", Descripcion = "GRADO ACADEMICO 2", Obligatorio = true, Ejemplo = "Bachiller (1), Título Profesional (2), Grado de Magister o Doctorado (3)" },
                new Catalogo() { Id = "CARRERA", Descripcion = "CARRERA PROFESIONAL", Obligatorio = true, Ejemplo = "Psicología" },
                new Catalogo() { Id = "FIS_POST", Descripcion = "FISCALIZACIÓN POSTERIOR", Obligatorio = true, Ejemplo = "SI O NO" },
                new Catalogo() { Id = "EXPERIENCIA_GENERAL_2", Descripcion = "EXPERIENCIA GENERAL (DESDE EL GRADO BACHILLER O CONSTANCIA DE EGRESADO)", Obligatorio = false, Ejemplo = "8" },
                new Catalogo() { Id = "EXPERIENCIA_ESPECIFICA_2", Descripcion = "EXPERIENCIA ESPECIFICA (AÑOS)", Obligatorio = true, Ejemplo = "4" },
                new Catalogo() { Id = "FECHA_ULTIMO_DIA", Descripcion = "FECHA ÚLTIMO DÍA DE PRESTACIÓN DE SERVICIOS", Obligatorio = true, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "DOCUMENTO_RESUELVE_CONTRATO", Descripcion = "DOCUMENTO QUE RESUELVE CONTRATO FECHA DE RECEPCIÓN", Obligatorio = false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo() { Id = "HORA_RUTA_3", Descripcion = "HOJA DE RUTA", Obligatorio = false, Ejemplo = "NUM + CARACTERES" },
                new Catalogo() { Id = "SECTORISTA_RESPONSABLE", Descripcion = "SECTORISTA RESPONSABLE", Obligatorio = false, Ejemplo = "JORGE PÉREZ" },
                new Catalogo() { Id = "NOMBRE_ENTIDAD_FINANCIERA", Descripcion = "NOMBRE DE ENTIDAD FINANCIERA", Obligatorio = true, Ejemplo = "BBVA CONTINENTAL" },
                
                new Catalogo() { Id = "CUENTA_AHORROS", Descripcion = "CUENTA DE AHORROS NRO.", Obligatorio = true, Ejemplo = "24591097961067" },
                new Catalogo() { Id = "CCI", Descripcion = "CCI NRO.", Obligatorio = true, Ejemplo = "393893245910979610674" },
                new Catalogo() { Id = "PUNTAJE", Descripcion = "PUNTAJE.", Obligatorio = true, Ejemplo = "56" }
            };
            return listaPac;
        }
        public List<Catalogo> ListaAdendas()
        {
            List<Catalogo> listaAdenda = new List<Catalogo>
            {
                new Catalogo(){ Id = "COD_CONTRATO", Descripcion = "COD C ONTRATO", Obligatorio =  false, Ejemplo = "001-2023-MEF" },
                new Catalogo(){ Id = "DOC_SOLICITUD", Descripcion = "DOC. SOLICITUD", Obligatorio =  false, Ejemplo = "002" },
                new Catalogo(){ Id = "HOJA_RUTA", Descripcion = "HOJA DE RUTA", Obligatorio =  false, Ejemplo = "222" },
                new Catalogo(){ Id = "NUMERO", Descripcion = "NRO", Obligatorio =  false, Ejemplo = "1225-2021" },
                new Catalogo(){ Id = "FEC_INICIO", Descripcion = "FECHA DE INICIO", Obligatorio =  true, Ejemplo = "dd/MM/yyyy" },
                new Catalogo(){ Id = "FEC_FIN", Descripcion = "FECHA DE CULMINACION", Obligatorio =  true, Ejemplo = "dd/MM/yyyy" },
                new Catalogo(){ Id = "FEC_RECEPCION", Descripcion = "FECHA DE RECEPCION", Obligatorio =  false, Ejemplo = "dd/MM/yyyy" },
                new Catalogo(){ Id = "DOCUMENTO", Descripcion = "DOCUMENTO", Obligatorio =  false, Ejemplo = "325" }

            };

            return listaAdenda;
        }

        public List<Catalogo> ListaPagos()
        {
            List<Catalogo> listaSalario = new List<Catalogo>
            {
                //new Catalogo(){ Id = "COD_TRABAJADOR", Descripcion = "COD TRABAJADOR", Obligatorio =  true, Ejemplo = "000003" },
                new Catalogo(){ Id = "COD_CONTRATO", Descripcion = "COD CONTRATO", Obligatorio =  true, Ejemplo = "281-PAC-2023" },
                new Catalogo(){ Id = "NUM_DOCUMENTO", Descripcion = "NÚMERO DE DOCUMENTO", Obligatorio =  true, Ejemplo = "06984888" },
                new Catalogo(){ Id = "RUC_CAS", Descripcion = "RUC DE CAS", Obligatorio =  true, Ejemplo = "10069848881" },
                new Catalogo(){ Id = "NOM_DEPENDENCIA", Descripcion = "NOMBRE DE LA ENTIDAD", Obligatorio =  true, Ejemplo = "MINISTERIO DE ENERGÍA Y MINAS" },

                new Catalogo(){ Id = "ING_ENE", Descripcion = "INGRESO DEL MES DE ENERO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_ENE", Descripcion = "EGRESO DEL MES DE ENERO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_ENE", Descripcion = "EMP DEL MES DE ENERO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_ENE", Descripcion = "NETO DEL MES DE ENERO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_FEB", Descripcion = "INGRESO DEL MES DE FEBRERO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_FEB", Descripcion = "EGRESO DEL MES DE FEBRERO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_FEB", Descripcion = "EMP DEL MES DE FEBRERO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_FEB", Descripcion = "NETO DEL MES DE FEBRERO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_MAR", Descripcion = "INGRESO DEL MES DE MARZO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_MAR", Descripcion = "EGRESO DEL MES DE MARZO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_MAR", Descripcion = "EMP DEL MES DE MARZO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_MAR", Descripcion = "NETO DEL MES DE MARZO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_ABR", Descripcion = "INGRESO DEL MES DE ABRIL", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_ABR", Descripcion = "EGRESO DEL MES DE ABRIL", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_ABR", Descripcion = "EMP DEL MES DE ABRIL", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_ABR", Descripcion = "NETO DEL MES DE ABRIL", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_MAY", Descripcion = "INGRESO DEL MES DE MAYO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_MAY", Descripcion = "EGRESO DEL MES DE MAYO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_MAY", Descripcion = "EMP DEL MES DE MAYO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_MAY", Descripcion = "NETO DEL MES DE MAYO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_JUN", Descripcion = "INGRESO DEL MES DE JUNIO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_JUN", Descripcion = "EGRESO DEL MES DE JUNIO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_JUN", Descripcion = "EMP DEL MES DE JUNIO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_JUN", Descripcion = "NETO DEL MES DE JUNIO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_JUL", Descripcion = "INGRESO DEL MES DE JULIO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_JUL", Descripcion = "EGRESO DEL MES DE JULIO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_JUL", Descripcion = "EMP DEL MES DE JULIO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_JUL", Descripcion = "NETO DEL MES DE JULIO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_AGO", Descripcion = "INGRESO DEL MES DE AGOSTO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_AGO", Descripcion = "EGRESO DEL MES DE AGOSTO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_AGO", Descripcion = "EMP DEL MES DE AGOSTO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_AGO", Descripcion = "NETO DEL MES DE AGOSTO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_SET", Descripcion = "INGRESO DEL MES DE SETIEMBRE", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_SET", Descripcion = "EGRESO DEL MES DE SETIEMBRE", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_SET", Descripcion = "EMP DEL MES DE SETIEMBRE", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_SET", Descripcion = "NETO DEL MES DE SETIEMBRE", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_OCT", Descripcion = "INGRESO DEL MES DE OCTUBRE", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_OCT", Descripcion = "EGRESO DEL MES DE OCTUBRE", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_OCT", Descripcion = "EMP DEL MES DE OCTUBRE", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_OCT", Descripcion = "NETO DEL MES DE OCTUBRE", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_NOV", Descripcion = "INGRESO DEL MES DE NOVIEMBRE", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_NOV", Descripcion = "EGRESO DEL MES DE NOVIEMBRE", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_NOV", Descripcion = "EMP DEL MES DE NOVIEMBRE", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_NOV", Descripcion = "NETO DEL MES DE NOVIEMBRE", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_DIC", Descripcion = "INGRESO DEL MES DE DICIEMBRE", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_DIC", Descripcion = "EGRESO DEL MES DE DICIEMBRE", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_DIC", Descripcion = "EMP DEL MES DE DICIEMBRE", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_DIC", Descripcion = "NETO DEL MES DE DICIEMBRE", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_TOT", Descripcion = "INGRESO DEL MES DE TOTAL", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_TOT", Descripcion = "EGRESO DEL MES DE TOTAL", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_TOT", Descripcion = "EMP DEL MES DE TOTAL", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_TOT", Descripcion = "NETO DEL MES DE TOTAL", Obligatorio =  false, Ejemplo = "17500" },
            };
            return listaSalario;
        }
        public List<Catalogo> ListaPagosExcel()
        {
            List<Catalogo> listaSalario = new List<Catalogo>
            {
                new Catalogo(){ Id = "COD_CONTRATO", Descripcion = "COD CONTRATO", Obligatorio =  true, Ejemplo = "" },
                new Catalogo(){ Id = "PERIODO", Descripcion = "AÑO DE PAGO", Obligatorio =  true, Ejemplo = "2023" },
                new Catalogo(){ Id = "NUM_DOCUMENTO", Descripcion = "NÚMERO DE DOCUMENTO", Obligatorio =  true, Ejemplo = "06984888" },
                new Catalogo(){ Id = "RUC_CAS", Descripcion = "RUC DE CAS", Obligatorio =  true, Ejemplo = "10069848881" },
                new Catalogo(){ Id = "NOM_DEPENDENCIA", Descripcion = "NOMBRE DE LA DEPENDENCIA", Obligatorio =  true, Ejemplo = "MINISTERIO DE ENERGÍA Y MINAS" },

                new Catalogo(){ Id = "ING_ENE", Descripcion = "INGRESO DEL MES DE ENERO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_ENE", Descripcion = "EGRESO DEL MES DE ENERO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_ENE", Descripcion = "EMP DEL MES DE ENERO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_ENE", Descripcion = "NETO DEL MES DE ENERO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_FEB", Descripcion = "INGRESO DEL MES DE FEBRERO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_FEB", Descripcion = "EGRESO DEL MES DE FEBRERO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_FEB", Descripcion = "EMP DEL MES DE FEBRERO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_FEB", Descripcion = "NETO DEL MES DE FEBRERO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_MAR", Descripcion = "INGRESO DEL MES DE MARZO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_MAR", Descripcion = "EGRESO DEL MES DE MARZO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_MAR", Descripcion = "EMP DEL MES DE MARZO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_MAR", Descripcion = "NETO DEL MES DE MARZO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_ABR", Descripcion = "INGRESO DEL MES DE ABRIL", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_ABR", Descripcion = "EGRESO DEL MES DE ABRIL", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_ABR", Descripcion = "EMP DEL MES DE ABRIL", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_ABR", Descripcion = "NETO DEL MES DE ABRIL", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_MAY", Descripcion = "INGRESO DEL MES DE MAYO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_MAY", Descripcion = "EGRESO DEL MES DE MAYO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_MAY", Descripcion = "EMP DEL MES DE MAYO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_MAY", Descripcion = "NETO DEL MES DE MAYO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_JUN", Descripcion = "INGRESO DEL MES DE JUNIO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_JUN", Descripcion = "EGRESO DEL MES DE JUNIO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_JUN", Descripcion = "EMP DEL MES DE JUNIO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_JUN", Descripcion = "NETO DEL MES DE JUNIO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_JUL", Descripcion = "INGRESO DEL MES DE JULIO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_JUL", Descripcion = "EGRESO DEL MES DE JULIO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_JUL", Descripcion = "EMP DEL MES DE JULIO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_JUL", Descripcion = "NETO DEL MES DE JULIO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_AGO", Descripcion = "INGRESO DEL MES DE AGOSTO", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_AGO", Descripcion = "EGRESO DEL MES DE AGOSTO", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_AGO", Descripcion = "EMP DEL MES DE AGOSTO", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_AGO", Descripcion = "NETO DEL MES DE AGOSTO", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_SET", Descripcion = "INGRESO DEL MES DE SETIEMBRE", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_SET", Descripcion = "EGRESO DEL MES DE SETIEMBRE", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_SET", Descripcion = "EMP DEL MES DE SETIEMBRE", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_SET", Descripcion = "NETO DEL MES DE SETIEMBRE", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_OCT", Descripcion = "INGRESO DEL MES DE OCTUBRE", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_OCT", Descripcion = "EGRESO DEL MES DE OCTUBRE", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_OCT", Descripcion = "EMP DEL MES DE OCTUBRE", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_OCT", Descripcion = "NETO DEL MES DE OCTUBRE", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_NOV", Descripcion = "INGRESO DEL MES DE NOVIEMBRE", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_NOV", Descripcion = "EGRESO DEL MES DE NOVIEMBRE", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_NOV", Descripcion = "EMP DEL MES DE NOVIEMBRE", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_NOV", Descripcion = "NETO DEL MES DE NOVIEMBRE", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_DIC", Descripcion = "INGRESO DEL MES DE DICIEMBRE", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_DIC", Descripcion = "EGRESO DEL MES DE DICIEMBRE", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_DIC", Descripcion = "EMP DEL MES DE DICIEMBRE", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_DIC", Descripcion = "NETO DEL MES DE DICIEMBRE", Obligatorio =  false, Ejemplo = "17500" },

                new Catalogo(){ Id = "ING_TOT", Descripcion = "INGRESO DEL MES DE TOTAL", Obligatorio =  true, Ejemplo = "25000" },
                new Catalogo(){ Id = "EGR_TOT", Descripcion = "EGRESO DEL MES DE TOTAL", Obligatorio =  false, Ejemplo = "7500" },
                new Catalogo(){ Id = "EMP_TOT", Descripcion = "EMP DEL MES DE TOTAL", Obligatorio =  false, Ejemplo = "0" },
                new Catalogo(){ Id = "NET_TOT", Descripcion = "NETO DEL MES DE TOTAL", Obligatorio =  false, Ejemplo = "17500" },
            };
            return listaSalario;
        }
        #endregion

        #region Exportar
        public MemoryStream ExportarCarga(Cls_Ent_Carga carga)
        {
            try
            {
                var listaCabecera = new List<Catalogo>();
                switch (carga.TIPO_DOC)
                {
                    case Constants.TipoDocumento.FAG.Id:
                        listaCabecera = ListaFag().ToList();
                        break;
                    case Constants.TipoDocumento.PAC.Id:
                        listaCabecera = ListaPac().ToList();
                        break;
                    case Constants.TipoDocumento.Adendas.Id:
                        listaCabecera = ListaAdendas().ToList();
                        break;
                    case Constants.TipoDocumento.SAL.Id:
                        listaCabecera = ListaPagosExcel().ToList();
                        break;
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage())
                {

                    var worksheet = package.Workbook.Worksheets.Add("CARGA MASIVA");

                    

                    int row = 1;
                    int col = 1;
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(worksheet, "REPORTE DE CARGA", row, col, 0, 0, "C", true);
                    row++;
                    row++;

                    /** Datos de Carga **/
                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(worksheet, "TIPO DOCUMENTO", row, col, 0, 0, "C", true);
                    col++;
                    ExcelUtil.CeldaFormatoEtiqueta_V2(worksheet, carga.TIPO_DOC, row, col, 0, 0, "C");
                    row++;
                    col = 1;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(worksheet, "DESCRIPCION", row, col, 0, 0, "C", true);
                    col++;
                    ExcelUtil.CeldaFormatoEtiqueta_V2(worksheet, carga.DES_CARGA, row, col, 0, 0, "C");
                    row++;
                    col = 1;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(worksheet, "FEC. REGISTRO", row, col, 0, 0, "C", true);
                    col++;
                    ExcelUtil.CeldaFormatoEtiqueta_V2(worksheet, carga.FEC_REGISTRO.ToString(), row, col, 0, 0, "C");
                    row++;
                    col = 1;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(worksheet, "USUARIO REGISTRO", row, col, 0, 0, "C", true);
                    col++;
                    ExcelUtil.CeldaFormatoEtiqueta_V2(worksheet, carga.USU_INGRESO, row, col, 0, 0, "C");
                    row++;
                    col = 1;

                    ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(worksheet, "NRO. REGISTROS", row, col, 0, 0, "C", true);
                    col++;
                    ExcelUtil.CeldaFormatoEtiqueta_V2(worksheet, carga.NRO_REGISTROS, row, col, 0, 0, "C");
                    row++;
                    col = 1;

                    /** Datos de Carga**/

                    row++;
                    col = 1;

                    listaCabecera.ForEach(x =>
                    {
                        ExcelUtil.CeldaFormatoEtiqueta_Cabecera_V2(worksheet, x.Descripcion, row, col, 0, 0, "C", true);
                        col++;
                    });
                    row++;

                    if (carga.TIPO_DOC == Constants.TipoDocumento.Adendas.Id)
                    {
                        carga.CARGA_DETALLE.ForEach(a =>
                        {
                            col = 1;
                            listaCabecera.ForEach(b =>
                            {
                                var value = a.GetType().GetProperties().FirstOrDefault(p => p.Name == b.Id).GetValue(a);
                                ExcelUtil.CeldaFormatoEtiqueta_V2(worksheet, value, row, col, 0, 0, "C");
                                col++;
                            });
                            row++;
                        });
                    }
                    else if(carga.TIPO_DOC == Constants.TipoDocumento.SAL.Id)
                    {
                        carga.CARGA_SALARIO.ForEach(a =>
                        {
                            col = 1;
                            listaCabecera.ForEach(b =>
                            {
                                var value = a.GetType().GetProperties().FirstOrDefault(p => p.Name == b.Id).GetValue(a);
                                ExcelUtil.CeldaFormatoEtiqueta_V2(worksheet, value, row, col, 0, 0, "C");
                                col++;
                            });
                            row++;
                        });
                    }
                    else
                    {
                        carga.CARGA_CABECERA.ForEach(a =>
                        {
                            col = 1;
                            listaCabecera.ForEach(b =>
                            {
                                var value = a.GetType().GetProperties().FirstOrDefault(p => p.Name == b.Id).GetValue(a);
                                ExcelUtil.CeldaFormatoEtiqueta_V2(worksheet, value, row, col, 0, 0, "C");
                                col++;
                            });
                            row++;
                        });

                    }

                    int xy = 0;

                    listaCabecera.ForEach(n =>
                    {
                        xy++;
                        worksheet.Column(xy).AutoFit();
                    });


                    return new System.IO.MemoryStream(package.GetAsByteArray());
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        } 
        #endregion
    }
}
