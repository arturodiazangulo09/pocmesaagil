using Dapper;
using MEF.PROYECTO.Data.DataBaseHelpers;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.CargaMasiva;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Utilitario;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MEF.PROYECTO.Data.CargaMasiva
{
    public class Cls_Dat_CargaMasiva : DataBaseHelper
    {

        public Cls_Ent_Carga MantenimientoCarga(Cls_Ent_Carga carga)
        {
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_MNT_CARGA";
            try
            {
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("P_ID_CARGA", carga.ID_CARGA);
                param[1] = new OracleParameter("P_TIPO_DOC", carga.TIPO_DOC);
                param[2] = new OracleParameter("P_TIPO_FORMATO", carga.TIPO_FORMATO);
                param[3] = new OracleParameter("P_USU_INGRESO", carga.USU_INGRESO);
                param[4] = new OracleParameter("P_USU_MODIFICA", carga.USU_MODIFICA);
                param[5] = new OracleParameter("P_FLG_ESTADO", carga.FLG_ESTADO);
                param[6] = new OracleParameter("P_FLG_PROCESADO", carga.FLG_PROCESADO);
                param[7] = new OracleParameter("P_NRO_REGISTROS", carga.NRO_REGISTROS);
                param[8] = new OracleParameter("PO_ID_CARGA", OracleDbType.Int32, carga.ID_CARGA, ParameterDirection.Output);

                OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);
                carga.ID_CARGA = Int32.Parse(param[8].Value.ToString());
                carga.Result = true;

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                carga.Result = false;
                throw ex;
            }
            return carga;
        }
        public Respuesta MantenimientoCargaCabecera_Bulk(DataTable carga)
        {
            Respuesta respuesta =new Respuesta();
            try
            {
                using (var bulkCopy = new OracleBulkCopy(this.cnSTR))
                {
                        bulkCopy.DestinationTableName = "T_FAGPAC_T_CARGA_CABECERA";
                        bulkCopy.WriteToServer(carga);
                }
                respuesta.IsError=false;
                respuesta.SetMensaje("ok");

                
            }
            catch(Exception ex)
            {
                respuesta.IsError=false;
                respuesta.SetMensaje(ex.Message);
                return respuesta;
            }

            return respuesta;
        }
        public Respuesta MantenimientoCargaAdendas_Bulk(DataTable carga)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (OracleConnection connection = new OracleConnection(this.cnSTR))
                {
                    connection.Open();
                    using (OracleBulkCopy bulkCopy = new OracleBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "T_FAGPAC_T_CARGA_DETALLE";
                        foreach (DataColumn column in carga.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                        }
                        bulkCopy.WriteToServer(carga);
                    }
                }
                respuesta.IsError = false;
                respuesta.SetMensaje("ok");
            }
            catch(Exception ex)
            {
                respuesta.IsError = true;
                respuesta.SetMensaje(ex.Message);
                return respuesta;
            }

            return respuesta;
        }
        public Respuesta MantenimientoPago_Bulk(DataTable carga)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (var bulkCopy = new OracleBulkCopy(this.cnSTR))
                {
                    bulkCopy.DestinationTableName = "T_FAGPAC_T_PAGO_CABECERA";
                    bulkCopy.WriteToServer(carga);
                }
                respuesta.IsError = false;
                respuesta.SetMensaje("ok");


            }
            catch (Exception ex)
            {
                respuesta.IsError = false;
                respuesta.SetMensaje(ex.Message);
                return respuesta;
            }

            return respuesta;
        }
        public bool ActualizarCargaCabecera(DataTable carga)
        {
            try
            {
                using (var bulkCopy = new OracleBulkCopy(this.cnSTR))
                {
                    bulkCopy.DestinationTableName = "T_FAGPAC_T_CARGA_CABECERA";
                    bulkCopy.WriteToServer(carga);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Respuesta ActualizarCargaCabecera(Cls_Ent_Carga_Cabecera carga)
        {
            Respuesta respuesta =new Respuesta();
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_UPD_T_CARGA_CABECERA";
            try
            {
                OracleParameter[] param = new OracleParameter[79];
                param[0] = new OracleParameter("P_ID_REGITRO", carga.ID_REGISTRO);
                param[1] = new OracleParameter("P_ID_CARGA", carga.ID_CARGA );
                param[2] = new OracleParameter("P_COD_CONTRATO", carga.COD_CONTRATO);
                param[3] = new OracleParameter("P_ALERTA_LA", carga.ALERTA_LA);
                param[4] = new OracleParameter("P_ALERTA_LA_PERSONA", carga.ALERTA_LA_PERSONA);
                param[5] = new OracleParameter("P_FEC_REGISTRO", carga.FEC_REGISTRO);
                param[6] = new OracleParameter("P_MODALIDAD", carga.MODALIDAD);
                param[7] = new OracleParameter("P_ESTADO", carga.ESTADO);
                param[8] = new OracleParameter("P_NRO_EXP_SISPER", carga.NRO_EXP_SISPER);
                param[9] = new OracleParameter("P_LEY31419", carga.LEY31419);

                param[10] = new OracleParameter("P_APELLIDO_PATERNO", carga.APELLIDO_PATERNO);
                param[11] = new OracleParameter("P_APELLIDO_MATERNO", carga.APELLIDO_MATERNO);
                param[12] = new OracleParameter("P_NOMBRES", carga.NOMBRES);
                param[13] = new OracleParameter("P_DNI", carga.DNI);
                param[14] = new OracleParameter("P_RUC", carga.RUC);
                param[15] = new OracleParameter("P_SEXO", carga.SEXO);
                param[16] = new OracleParameter("P_FEC_NACIMIENTO", carga.FEC_NACIMIENTO);
                param[17] = new OracleParameter("P_EDAD", carga.EDAD);
                param[18] = new OracleParameter("P_NACIONALIDAD", carga.NACIONALIDAD);
                param[19] = new OracleParameter("P_PADRE", carga.PADRE);

                param[20] = new OracleParameter("P_TELEFONO_CELULAR", carga.TELEFONO_CELULAR);
                param[21] = new OracleParameter("P_CORREO_ELECTRONICO", carga.CORREO_ELECTRONICO);
                param[22] = new OracleParameter("P_DIRECCION", carga.DIRECCION);
                param[23] = new OracleParameter("P_DISTRITO", carga.DISTRITO);
                param[24] = new OracleParameter("P_PROVINCIA", carga.PROVINCIA);
                param[25] = new OracleParameter("P_REGION", carga.REGION);
                param[26] = new OracleParameter("P_NIVEL_GOBIERNO", carga.NIVEL_GOBIERNO);
                param[27] = new OracleParameter("P_CODIGO_ENTIDAD", carga.CODIGO_ENTIDAD);
                param[28] = new OracleParameter("P_TIPO_DOC_SOLICITA_REG", carga.TIPO_DOC_SOLICITA_REG);
                param[29] = new OracleParameter("P_FEC_DOC_SOLICITA_REG", carga.FEC_DOC_SOLICITA_REG);

                param[30] = new OracleParameter("P_HR_DOC_SOLICITA_REG", carga.HR_DOC_SOLICITA_REG);
                param[31] = new OracleParameter("P_DOC_RES_MINISTERIAL", carga.DOC_RES_MINISTERIAL);
                param[32] = new OracleParameter("P_ENTIDAD_BENEF", carga.ENTIDAD_BENEF);
                param[33] = new OracleParameter("P_FLAG_DESIGNADO", carga.FLAG_DESIGNADO);
                param[34] = new OracleParameter("P_ALTA_DIRECCION", carga.ALTA_DIRECCION);
                param[35] = new OracleParameter("P_DEPENDENCIA_PRESTACION", carga.DEPENDENCIA_PRESTACION);
                param[36] = new OracleParameter("P_CARGO_ANEXO1", carga.CARGO_ANEXO1);
                param[37] = new OracleParameter("P_CARGO_FUNCIONARIO_SUSCRIBE", carga.CARGO_FUNCIONARIO_SUSCRIBE);
                param[38] = new OracleParameter("P_IMPORTE_HONORARIO", carga.IMPORTE_HONORARIO);
                param[39] = new OracleParameter("P_FEC_SUSCRIPCION", carga.FEC_SUSCRIPCION);

                param[40] = new OracleParameter("P_FEC_INICIO", carga.FEC_INICIO);
                param[41] = new OracleParameter("P_FEC_CULMINACION", carga.FEC_CULMINACION);
                param[42] = new OracleParameter("P_FEC_RECEPCION", carga.FEC_RECEPCION);
                param[43] = new OracleParameter("P_NRO_OFICIO", carga.NRO_OFICIO);
                param[44] = new OracleParameter("P_FIN_CONTRATO", carga.FIN_CONTRATO);
                param[45] = new OracleParameter("P_EXP_REMITIDO", carga.EXP_REMITIDO);
                param[46] = new OracleParameter("P_ADENDA_VIG_DOC_SOL", carga.ADENDA_VIG_DOC_SOL);
                param[47] = new OracleParameter("P_ADENDA_VIG_NRO", carga.ADENDA_VIG_NRO);
                param[48] = new OracleParameter("P_ADENDA_VIG_FEC_INICIO", carga.ADENDA_VIG_FEC_INICIO);
                param[49] = new OracleParameter("P_ADENDA_VIG_FEC_FIN", carga.ADENDA_VIG_FEC_FIN);

                param[50] = new OracleParameter("P_ADENDA_VIG_HOJARUTA", carga.ADENDA_VIG_HOJARUTA);
                param[51] = new OracleParameter("P_ADENDA_VIG_SUSCRIPCION", carga.ADENDA_VIG_SUSCRIPCION);
                param[52] = new OracleParameter("P_NOT_FEC_RECEPCION", carga.NOT_FEC_RECEPCION);
                param[53] = new OracleParameter("P_NOT_DOCUMENTO", carga.NOT_DOCUMENTO);
                param[54] = new OracleParameter("P_FEC_CULMINACION_CONTRATO", carga.FEC_CULMINACION_CONTRATO);
                param[55] = new OracleParameter("P_GRADO_ACADEMICO_GEN", carga.GRADO_ACADEMICO_GEN);
                param[56] = new OracleParameter("P_GRADO_ACADEMICO_FAG", carga.GRADO_ACADEMICO_FAG);
                param[57] = new OracleParameter("P_CARRERA_PROFESIONAL", carga.CARRERA_PROFESIONAL);
                param[58] = new OracleParameter("P_UNIVERSIDAD", carga.UNIVERSIDAD);
                param[59] = new OracleParameter("P_FLAG_REG_SUNEDU", carga.FLAG_REG_SUNEDU);

                param[60] = new OracleParameter("P_GRADO_ACADEMICO_ESP", carga.GRADO_ACADEMICO_ESP);
                param[61] = new OracleParameter("P_REQ_HAB_PROF", carga.REQ_HAB_PROF);
                param[62] = new OracleParameter("P_EXP_LAB_GENERAL", carga.EXP_LAB_GENERAL);
                param[63] = new OracleParameter("P_EXP_LAB_ESPECIFICA", carga.EXP_LAB_ESPECIFICA);
                param[64] = new OracleParameter("P_EXP_LAB_MATERIA", carga.EXP_LAB_MATERIA);
                param[65] = new OracleParameter("P_EXP_LAB_GENERAL_GRADO", carga.EXP_LAB_GENERAL_GRADO);
                param[66] = new OracleParameter("P_EXP_LAB_ACT_TDRS", carga.EXP_LAB_ACT_TDRS);
                param[67] = new OracleParameter("P_RES_CONTRATO_FEC_CULM", carga.RES_CONTRATO_FEC_CULM);
                param[68] = new OracleParameter("P_RES_CONTRATO_DOC", carga.RES_CONTRATO_DOC);
                param[69] = new OracleParameter("P_RES_CONTRATO_HR", carga.RES_CONTRATO_HR);

                param[70] = new OracleParameter("P_SEC_RESPONSABLE", carga.SEC_RESPONSABLE);
                param[71] = new OracleParameter("P_FISCAL_POST", carga.FISCAL_POST);
                param[72] = new OracleParameter("P_ENT_FIN_NOMBRE", carga.ENT_FIN_NOMBRE);
                param[73] = new OracleParameter("P_ENT_FIN_CTA_AHORRO", carga.ENT_FIN_CTA_AHORRO);
                param[74] = new OracleParameter("P_ENT_FIN_CTA_BANCARIA", carga.ENT_FIN_CTA_BANCARIA);
                param[75] = new OracleParameter("P_ENT_FIN_CTA_CCI", carga.ENT_FIN_CTA_CCI);
                param[76] = new OracleParameter("P_PJE_PUESTO", carga.PJE_PUESTO);
                param[77] = new OracleParameter("P_FLG_ESTADO", carga.FLG_ESTADO);
                param[78] = new OracleParameter("P_FLG_PROCESADO", carga.FLG_PROCESADO);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                respuesta.IsError=false;
                respuesta.Mensaje="OK";
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                respuesta.IsError=true;
                respuesta.SetMensaje(ex.Message);
            }
            return respuesta;
        }
        public Respuesta ActualizarCargaDetalle(Cls_Ent_Carga_Detalle entidad)
        {
            Respuesta respuesta = new Respuesta();
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_UPD_T_CARGA_DETALLE";
            try
            {
                OracleParameter[] param = new OracleParameter[6];
                param[0] = new OracleParameter("P_ID_CARGA", entidad.ID_CARGA);
                param[1] = new OracleParameter("P_COD_CONTRATO", entidad.COD_CONTRATO);
                param[2] = new OracleParameter("P_DOC_SOLICITUD", entidad.DOC_SOLICITUD);
                param[3] = new OracleParameter("P_HOJA_RUTA", entidad.HOJA_RUTA);
                param[4] = new OracleParameter("P_FEC_SUSCRIPCION", entidad.FEC_SUSCRIPCION);
                param[5] = new OracleParameter("P_NUMERO", entidad.NUMERO);
                param[6] = new OracleParameter("P_FEC_INICIO", entidad.FEC_INICIO);
                param[7] = new OracleParameter("P_FEC_FIN", entidad.FEC_FIN);
                param[8] = new OracleParameter("P_FEC_RECEPCION", entidad.FEC_RECEPCION);
                param[9] = new OracleParameter("P_DOCUMENTO", entidad.DOCUMENTO);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                respuesta.IsError=false;
                respuesta.SetMensaje("OK");
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                respuesta.IsError=true;
                respuesta.SetMensaje(ex.Message);
            }
            return respuesta;
        }
        public List<Cls_Ent_Carga> ListaCarga(String usuario)
        {
            List<Cls_Ent_Carga> lista = null;
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_LISTA_M_CARGA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    
                    var p = new OracleDynamicParameters();
                    p.Add("P_USU_INGRESO", usuario);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Carga>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Carga_Cabecera> ListaCabeceraContratos(Cls_Ent_Carga entidad)
        {
            List<Cls_Ent_Carga_Cabecera> lista = null;
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_LISTA_T_CARGA_CAB_DET";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CARGA", entidad.ID_CARGA);
                    p.Add("P_TIPO_DOC", entidad.TIPO_DOC);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Carga_Cabecera>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }

        public List<Cls_Ent_Carga> ListaCargaHistorica( String UsuIngreso)
        {
            List<Cls_Ent_Carga> lista = null;
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_LISTA_M_CARGA_HISTORIAL";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_USU_INGRESO", UsuIngreso);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Carga>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }

        
        public Cls_Ent_Carga_Cabecera ObtenerCabeceraContratos(int Id_Registro)
        {
            Cls_Ent_Carga_Cabecera obj = null;
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_OBTENER_CONTRATO_X_ID";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_REGISTRO", Id_Registro);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

                    obj = db.Query<Cls_Ent_Carga_Cabecera>(sp, p, commandType: CommandType.StoredProcedure).First();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return obj;
        }


        
        public List<Cls_Ent_Carga_Detalle> ListaContratosAdendas(Cls_Ent_Carga entidad)
        {
            List<Cls_Ent_Carga_Detalle> lista = null;
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_LISTA_T_CARGA_CAB_DET";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CARGA", entidad.ID_CARGA);
                    p.Add("P_TIPO_DOC", entidad.TIPO_DOC);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Carga_Detalle>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }

        public List<Cls_Ent_Carga_Pago> ListaCabeceraPagos(Cls_Ent_Carga entidad)
        {
            List<Cls_Ent_Carga_Pago> lista = null;
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_LISTA_T_CARGA_CAB_DET";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CARGA", entidad.ID_CARGA);
                    p.Add("P_TIPO_DOC", entidad.TIPO_DOC);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Carga_Pago>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Entidades> ListarEntidades()
        {
            List<Cls_Ent_Entidades> lista = null;
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_LISTA_T_CARGA_ENTIDAD";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Entidades>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch(Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }
            return lista;
        }
        public List<Cls_Ent_Ubigeo> ListarUbigeo()
        {
            List<Cls_Ent_Ubigeo> lista = null;
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_LISTA_T_CARGA_UBIGEO";
            try
            {
                using(IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Ubigeo>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch(Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }
            return lista;
        }

        public bool EliminarCarga(int idCarga, string usuario)
        {
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_DEL_CARGA";
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                int rpta = 0;
                param[0] = new OracleParameter("P_ID_CARGA", idCarga);
                param[1] = new OracleParameter("P_USU_MODIFICA", usuario);
                param[2] = new OracleParameter("PO_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);

                OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);
                int result = Int32.Parse(param[2].Value.ToString());
                if (result == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                return false;
            }
        }

        public bool EliminarContrato(int IdRegistro)
        {
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_DEL_T_CARGA_CABECERA";
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                int rpta = 0;
                param[0] = new OracleParameter("P_ID_REGISTRO", IdRegistro);
                param[1] = new OracleParameter("PO_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);
                int result = Int32.Parse(param[1].Value.ToString());
                if (result == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                return false;
            }
        }

        public List<Cls_Ent_Carga> ListaCargaCompleta(int idCarga)
        {
            var listaCarga = new List<Cls_Ent_Carga>();
            
            string spCarga = "FAGPAC.PACK_CARGA_DATOS.PRC_LIST_CARGA_X_ID";
            string spCargaContratos = "FAGPAC.PACK_CARGA_DATOS.PRC_LIST_CARGA_CONTRATOS";
            string spCargaAdendas = "FAGPAC.PACK_CARGA_DATOS.PRC_LIST_CARGA_ADENDAS";
            string spCargaSalario = "FAGPAC.PACK_CARGA_DATOS.PRC_LIST_CARGA_PAGO";

            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CARGA", dbType: OracleDbType.Int32, value: idCarga);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    listaCarga = db.Query<Cls_Ent_Carga>(spCarga, p, commandType: CommandType.StoredProcedure).ToList();

                    if(listaCarga[0].TIPO_DOC == Constants.TipoDocumento.Adendas.Id)
                    {
                        List<Cls_Ent_Carga_Detalle> listaAdendas = db.Query<Cls_Ent_Carga_Detalle>(spCargaAdendas, p, commandType: CommandType.StoredProcedure).ToList();
                        listaCarga[0].CARGA_DETALLE = listaAdendas;
                    }
                    else if (listaCarga[0].TIPO_DOC == Constants.TipoDocumento.SAL.Id)
                    {
                        List<Cls_Ent_Carga_Pago> listaSalario = db.Query<Cls_Ent_Carga_Pago>(spCargaSalario, p, commandType: CommandType.StoredProcedure).ToList();
                        listaCarga[0].CARGA_SALARIO = listaSalario;
                    }
                    else
                    {
                        List<Cls_Ent_Carga_Cabecera> listaContratos = db.Query<Cls_Ent_Carga_Cabecera>(spCargaContratos, p, commandType: CommandType.StoredProcedure).ToList();
                        listaCarga[0].CARGA_CABECERA = listaContratos;
                    }
                }
            }catch(Exception ex)
            {
                Log.MensajeLog(ex.ToString(), spCarga);
            }
            return listaCarga;
        }
        public bool ProcesarCarga(int idCarga, string username)
        {
            string spProcesarCarga = "FAGPAC.PACK_CARGA_DATOS.PRC_PROCESAR_CARGA";

            try
            {
                OracleParameter[] param = new OracleParameter[3];
                int rpta = 0;
                param[0] = new OracleParameter("P_ID_CARGA", idCarga);
                param[1] = new OracleParameter("P_USUARIO", username);
                param[2] = new OracleParameter("PO_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, spProcesarCarga, param);
                int result = Int32.Parse(param[2].Value.ToString());
                if (result == 1)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                Log.MensajeLog(ex.Message, spProcesarCarga);
                return false;
            }
        }

        public List<Cls_Ent_Carga_Detalle> ListaAdendasxContrato(int IdRegistro)
        {
            List<Cls_Ent_Carga_Detalle> lista = null;
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_OBTENER_ADENDAS_X_CONTRATO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_REGISTRO", IdRegistro);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Carga_Detalle>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }


        public Cls_Ent_Carga_Detalle ObtenerCargaDetallexId(string idCargaDetalle)
        {
            var res = new Cls_Ent_Carga_Detalle();
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_CARGA_ADENDAS_OBTENER_X_ID";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CARGA_DETALLE", idCargaDetalle);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    res = db.Query<Cls_Ent_Carga_Detalle>(sp, p, commandType: CommandType.StoredProcedure).ToList()[0];
                    res.Result = true;
                }
            }
            catch(Exception ex)
            {
                res.Result = false;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return res;
        }
        public bool EditarCargaDetalle(Cls_Ent_Carga_Detalle detalle)
        {
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_CARGA_ADENDAS_EDITAR";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    int rpta = 0;

                    OracleParameter[] param = new OracleParameter[12];

                    param[0] = new OracleParameter("P_ID_CARGA_DETALLE", detalle.ID_CARGA_DETALLE);
                    param[1] = new OracleParameter("P_ID_CARGA", detalle.ID_CARGA);
                    param[2] = new OracleParameter("P_COD_CONTRATO", detalle.COD_CONTRATO);
                    param[3] = new OracleParameter("P_DOC_SOLICITUD", detalle.DOC_SOLICITUD);
                    param[4] = new OracleParameter("P_HOJA_RUTA", detalle.HOJA_RUTA);
                    param[5] = new OracleParameter("P_FEC_SUSCRIPCION", detalle.FEC_SUSCRIPCION);
                    param[6] = new OracleParameter("P_NUMERO", detalle.NUMERO);
                    param[7] = new OracleParameter("P_FEC_INICIO", detalle.FEC_INICIO);
                    param[8] = new OracleParameter("P_FEC_FIN", detalle.FEC_FIN);
                    param[9] = new OracleParameter("P_FEC_RECEPCION", detalle.FEC_RECEPCION);
                    param[10] = new OracleParameter("P_DOCUMENTO", detalle.DOCUMENTO);
                    param[11] = new OracleParameter("PO_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);
                    OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);

                    int result = Int32.Parse(param[11].Value.ToString());
                    if (result == 1)
                        return true;
                    else
                        return false;

                }
            }catch(Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
                return false;
            }
        }
        public bool EliminarCargaDetalle(string idCargaDetalle)
        {
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_CARGA_ADENDAS_ELIMINAR";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    int rpta = 0;

                    OracleParameter[] param = new OracleParameter[2];

                    param[0] = new OracleParameter("P_ID_CARGA_DETALLE", idCargaDetalle);
                    param[1] = new OracleParameter("PO_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);
                    OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);

                    int result = Int32.Parse(param[1].Value.ToString());
                    if (result == 1)
                        return true;
                    else
                        return false;

                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
                return false;
            }
        }

        public Cls_Ent_Carga_Pago ObtenerCargaSalarioxId(string idCargaSalario)
        {
            var res = new Cls_Ent_Carga_Pago();
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_CARGA_SALARIO_OBTENER_X_ID";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_REGISTRO", idCargaSalario);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    res = db.Query<Cls_Ent_Carga_Pago>(sp, p, commandType: CommandType.StoredProcedure).ToList()[0];
                    res.Result = true;
                }
            }
            catch (Exception ex)
            {
                res.Result = false;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return res;
        }
        public bool EditarCargaSalario(Cls_Ent_Carga_Pago detalle)
        {
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_CARGA_SALARIO_EDITAR";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    int rpta = 0;

                    OracleParameter[] param = new OracleParameter[60];

                    param[0] = new OracleParameter("P_ID_REGISTRO", detalle.ID_REGISTRO);
                    param[1] = new OracleParameter("P_ID_CARGA", detalle.ID_CARGA);
                    param[2] = new OracleParameter("P_PERIODO", detalle.PERIODO);
                    param[3] = new OracleParameter("P_COD_CONTRATO", detalle.COD_CONTRATO);

                    param[4] = new OracleParameter("P_NUM_DOCUMENTO", detalle.NUM_DOCUMENTO);
                    param[5] = new OracleParameter("P_RUC_CAS", detalle.RUC_CAS);
                    param[6] = new OracleParameter("P_NOM_DEPENDENCIA", detalle.NOM_DEPENDENCIA);
                    param[7] = new OracleParameter("P_ING_ENE", detalle.ING_ENE);
                    param[8] = new OracleParameter("P_EGR_ENE", detalle.EGR_ENE);
                    param[9] = new OracleParameter("P_EMP_ENE", detalle.EMP_ENE);
                    param[10] = new OracleParameter("P_NET_ENE", detalle.NET_ENE);
                    param[11] = new OracleParameter("P_ING_FEB", detalle.ING_FEB);
                    param[12] = new OracleParameter("P_EGR_FEB", detalle.EGR_FEB);
                    param[13] = new OracleParameter("P_EMP_FEB", detalle.EMP_FEB);
                    param[14] = new OracleParameter("P_NET_FEB", detalle.NET_FEB);
                    param[15] = new OracleParameter("P_ING_MAR", detalle.ING_MAR);
                    param[16] = new OracleParameter("P_EGR_MAR", detalle.EGR_MAR);
                    param[17] = new OracleParameter("P_EMP_MAR", detalle.EMP_MAR);
                    param[18] = new OracleParameter("P_NET_MAR", detalle.NET_MAR);
                    param[19] = new OracleParameter("P_ING_ABR", detalle.ING_ABR);
                    param[20] = new OracleParameter("P_EGR_ABR", detalle.EGR_ABR);
                    param[21] = new OracleParameter("P_EMP_ABR", detalle.EMP_ABR);
                    param[22] = new OracleParameter("P_NET_ABR", detalle.NET_ABR);
                    param[23] = new OracleParameter("P_ING_MAY", detalle.ING_MAY);
                    param[24] = new OracleParameter("P_EGR_MAY", detalle.EGR_MAY);
                    param[25] = new OracleParameter("P_EMP_MAY", detalle.EMP_MAY);
                    param[26] = new OracleParameter("P_NET_MAY", detalle.NET_MAY);
                    param[27] = new OracleParameter("P_ING_JUN", detalle.ING_JUN);
                    param[28] = new OracleParameter("P_EGR_JUN", detalle.EGR_JUN);
                    param[29] = new OracleParameter("P_EMP_JUN", detalle.EMP_JUN);
                    param[30] = new OracleParameter("P_NET_JUN", detalle.NET_JUN);
                    param[31] = new OracleParameter("P_ING_JUL", detalle.ING_JUL);
                    param[32] = new OracleParameter("P_EGR_JUL", detalle.EGR_JUL);
                    param[33] = new OracleParameter("P_EMP_JUL", detalle.EMP_JUL);
                    param[34] = new OracleParameter("P_NET_JUL", detalle.NET_JUL);
                    param[35] = new OracleParameter("P_ING_AGO", detalle.ING_AGO);
                    param[36] = new OracleParameter("P_EGR_AGO", detalle.EGR_AGO);
                    param[37] = new OracleParameter("P_EMP_AGO", detalle.EMP_AGO);
                    param[38] = new OracleParameter("P_NET_AGO", detalle.NET_AGO);
                    param[39] = new OracleParameter("P_ING_SET", detalle.ING_SET);
                    param[40] = new OracleParameter("P_EGR_SET", detalle.EGR_SET);
                    param[41] = new OracleParameter("P_EMP_SET", detalle.EMP_SET);
                    param[42] = new OracleParameter("P_NET_SET", detalle.NET_SET);
                    param[43] = new OracleParameter("P_ING_OCT", detalle.ING_OCT);
                    param[44] = new OracleParameter("P_EGR_OCT", detalle.EGR_OCT);
                    param[45] = new OracleParameter("P_EMP_OCT", detalle.EMP_OCT);
                    param[46] = new OracleParameter("P_NET_OCT", detalle.NET_OCT);
                    param[47] = new OracleParameter("P_ING_NOV", detalle.ING_NOV);
                    param[48] = new OracleParameter("P_EGR_NOV", detalle.EGR_NOV);
                    param[49] = new OracleParameter("P_EMP_NOV", detalle.EMP_NOV);
                    param[50] = new OracleParameter("P_NET_NOV", detalle.NET_NOV);
                    param[51] = new OracleParameter("P_ING_DIC", detalle.ING_DIC);
                    param[52] = new OracleParameter("P_EGR_DIC", detalle.EGR_DIC);
                    param[53] = new OracleParameter("P_EMP_DIC", detalle.EMP_DIC);
                    param[54] = new OracleParameter("P_NET_DIC", detalle.NET_DIC);
                    param[55] = new OracleParameter("P_ING_TOT", detalle.ING_TOT);
                    param[56] = new OracleParameter("P_EGR_TOT", detalle.EGR_TOT);
                    param[57] = new OracleParameter("P_EMP_TOT", detalle.EMP_TOT);
                    param[58] = new OracleParameter("P_NET_TOT", detalle.NET_TOT);
                    param[59] = new OracleParameter("PO_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);
                    OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);

                    int result = Int32.Parse(param[59].Value.ToString());
                    if (result == 1)
                        return true;
                    else
                        return false;

                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
                return false;
            }
        }
        public bool EliminarCargaSalario(string idCargaSalario)
        {
            string sp = "FAGPAC.PACK_CARGA_DATOS.PRC_CARGA_SALARIO_ELIMINAR";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    int rpta = 0;

                    OracleParameter[] param = new OracleParameter[2];

                    param[0] = new OracleParameter("P_ID_REGISTRO", idCargaSalario);
                    param[1] = new OracleParameter("PO_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);
                    OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);

                    int result = Int32.Parse(param[1].Value.ToString());
                    if (result == 1)
                        return true;
                    else
                        return false;

                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
                return false;
            }
        }
    }
}
