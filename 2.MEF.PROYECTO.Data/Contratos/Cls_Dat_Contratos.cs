using MEF.PROYECTO.Entity.Contratos;
using MEF.PROYECTO.Utilitario;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.DataBaseHelpers;
using Dapper;
using MEF.PROYECTO.Entity.Administracion;

namespace MEF.PROYECTO.Data.Contratos
{
    public class Cls_Dat_Contratos : DataBaseHelper
    {
        #region Carga

        public Respuesta<List<Cls_Ent_Carga>> ListaCarga(string campos, string valores, int pagina, int nregistros) 
        {
            var result = new Respuesta<List<Cls_Ent_Carga>>();
            string sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_LISTA_CARGA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_CAMPOS", campos);
                    p.Add("P_VALORES", valores);
                    p.Add("P_PAGINA", pagina);
                    p.Add("P_REG_PAGINA", nregistros);
                    p.Add("P_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    result.Data = db.Query<Cls_Ent_Carga>(sp, p, commandType: CommandType.StoredProcedure).ToList();

                    result.Error = false;
                }
            }
            catch(Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return result;
        }
        public Respuesta<Cls_Ent_Carga> EditarCarga(int idCarga, Cls_Ent_Carga carga)
        {
            var result = new Respuesta<Cls_Ent_Carga>();
            return result;
        }
        
        
        #endregion

        #region Contratos

        public Respuesta<List<Cls_Ent_Contratos>> ListaContratos(string numDocumento, string cargo, decimal idEntidad, string campos, string valores, int pagina, int nregistros)
        {
            var result = new Respuesta<List<Cls_Ent_Contratos>>();
            string sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_LISTA_CONTRATOS";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_CAMPOS", campos);
                    p.Add("P_VALORES", valores);
                    p.Add("P_DNI", numDocumento);
                    p.Add("P_CARGO_FUNCIONARIO_SUSCRIBE", cargo);
                    p.Add("P_CODIGO_ENTIDAD", idEntidad);
                    p.Add("P_PAGINA", pagina);
                    p.Add("P_REG_PAGINA", nregistros);
                    p.Add("P_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    result.Data = db.Query<Cls_Ent_Contratos>(sp, p, commandType: CommandType.StoredProcedure).ToList();

                    result.Error = false;
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return result;
        }
        public Respuesta<Cls_Ent_Contratos> ObtenerContrato(int idRegistro, int idCarga)
        {
            var result = new Respuesta<Cls_Ent_Contratos>();
            string sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_OBTENER_CONTRATO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_REGISTRO", idRegistro);
                    p.Add("P_ID_CARGA", idCarga);
                    p.Add("P_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    result.Data = db.Query<Cls_Ent_Contratos>(sp, p, commandType: CommandType.StoredProcedure).ToList()[0];

                    result.Error = false;
                }
            }
            catch(Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return result;
        }
        public Respuesta<Cls_Ent_Contratos> EditarContrato(int idContrato, Cls_Ent_Contratos contratos)
        {
            var result = new Respuesta<Cls_Ent_Contratos>();
            var sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_ACTUALIZAR_CONTRATOS";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    int rpta = 0;

                    OracleParameter[] param = new OracleParameter[79];

                    param[0] = new OracleParameter("P_ID_REGISTRO", contratos.ID_REGISTRO);
                    param[1] = new OracleParameter("P_ID_CARGA", contratos.ID_CARGA);
                    param[2] = new OracleParameter("P_COD_CONTRATO", contratos.COD_CONTRATO);
                    param[3] = new OracleParameter("P_ALERTA_LA", contratos.ALERTA_LA);
                    param[4] = new OracleParameter("P_ALERTA_LA_PERSONA", contratos.ALERTA_LA_PERSONA);
                    param[5] = new OracleParameter("P_FEC_REGISTRO", contratos.FEC_REGISTRO);
                    param[6] = new OracleParameter("P_MODALIDAD", contratos.MODALIDAD);
                    param[7] = new OracleParameter("P_ESTADO", contratos.ESTADO);
                    param[8] = new OracleParameter("P_NRO_EXP_SISPER", contratos.NRO_EXP_SISPER);
                    param[9] = new OracleParameter("P_LEY31419", contratos.LEY31419);
                    param[10] = new OracleParameter("P_APELLIDO_PATERNO", contratos.APELLIDO_PATERNO);
                    param[11] = new OracleParameter("P_APELLIDO_MATERNO", contratos.APELLIDO_MATERNO);
                    param[12] = new OracleParameter("P_NOMBRES", contratos.NOMBRES);
                    param[13] = new OracleParameter("P_DNI", contratos.DNI);
                    param[14] = new OracleParameter("P_RUC", contratos.RUC);
                    param[15] = new OracleParameter("P_SEXO", contratos.SEXO);
                    param[16] = new OracleParameter("P_FEC_NACIMIENTO", contratos.FEC_NACIMIENTO);
                    param[17] = new OracleParameter("P_EDAD", contratos.EDAD);
                    param[18] = new OracleParameter("P_NACIONALIDAD", contratos.NACIONALIDAD);
                    param[19] = new OracleParameter("P_PADRE", contratos.PADRE);
                    param[20] = new OracleParameter("P_TELEFONO_CELULAR", contratos.TELEFONO_CELULAR);
                    param[21] = new OracleParameter("P_CORREO_ELECTRONICO", contratos.CORREO_ELECTRONICO);
                    param[22] = new OracleParameter("P_DIRECCION", contratos.DIRECCION);
                    param[23] = new OracleParameter("P_DISTRITO", contratos.DISTRITO);
                    param[24] = new OracleParameter("P_PROVINCIA", contratos.PROVINCIA);
                    param[25] = new OracleParameter("P_REGION", contratos.REGION);
                    param[26] = new OracleParameter("P_NIVEL_GOBIERNO", contratos.NIVEL_GOBIERNO);
                    param[27] = new OracleParameter("P_CODIGO_ENTIDAD", contratos.CODIGO_ENTIDAD);
                    param[28] = new OracleParameter("P_TIPO_DOC_SOLICITA_REG", contratos.TIPO_DOC_SOLICITA_REG);
                    param[29] = new OracleParameter("P_FEC_DOC_SOLICITA_REG", contratos.FEC_DOC_SOLICITA_REG);
                    param[30] = new OracleParameter("P_HR_DOC_SOLICITA_REG", contratos.HR_DOC_SOLICITA_REG);
                    param[31] = new OracleParameter("P_DOC_RES_MINISTERIAL", contratos.DOC_RES_MINISTERIAL);
                    param[32] = new OracleParameter("P_ENTIDAD_BENEF", contratos.ENTIDAD_BENEF);
                    param[33] = new OracleParameter("P_FLAG_DESIGNADO", contratos.FLAG_DESIGNADO);
                    param[34] = new OracleParameter("P_ALTA_DIRECCION", contratos.ALTA_DIRECCION);
                    param[35] = new OracleParameter("P_DEPENDENCIA_PRESTACION", contratos.DEPENDENCIA_PRESTACION);
                    param[36] = new OracleParameter("P_CARGO_ANEXO1", contratos.CARGO_ANEXO1);
                    param[37] = new OracleParameter("P_CARGO_FUNCIONARIO_SUSCRIBE", contratos.CARGO_FUNCIONARIO_SUSCRIBE);
                    param[38] = new OracleParameter("P_IMPORTE_HONORARIO", contratos.IMPORTE_HONORARIO);
                    param[39] = new OracleParameter("P_FEC_SUSCRIPCION", contratos.FEC_SUSCRIPCION);
                    param[40] = new OracleParameter("P_FEC_INICIO", contratos.FEC_INICIO);
                    param[41] = new OracleParameter("P_FEC_CULMINACION", contratos.FEC_CULMINACION);
                    param[42] = new OracleParameter("P_FEC_RECEPCION", contratos.FEC_RECEPCION);
                    param[43] = new OracleParameter("P_NRO_OFICIO", contratos.NRO_OFICIO);
                    param[44] = new OracleParameter("P_FIN_CONTRATO", contratos.FIN_CONTRATO);
                    param[45] = new OracleParameter("P_EXP_REMITIDO", contratos.EXP_REMITIDO);
                    param[46] = new OracleParameter("P_ADENDA_VIG_DOC_SOL", contratos.ADENDA_VIG_DOC_SOL);
                    param[47] = new OracleParameter("P_ADENDA_VIG_NRO", contratos.ADENDA_VIG_NRO);
                    param[48] = new OracleParameter("P_ADENDA_VIG_FEC_INICIO", contratos.ADENDA_VIG_FEC_INICIO);
                    param[49] = new OracleParameter("P_ADENDA_VIG_FEC_FIN", contratos.ADENDA_VIG_FEC_FIN);
                    param[50] = new OracleParameter("P_ADENDA_VIG_HOJARUTA", contratos.ADENDA_VIG_HOJARUTA);
                    param[51] = new OracleParameter("P_ADENDA_VIG_SUSCRIPCION", contratos.ADENDA_VIG_SUSCRIPCION);
                    param[52] = new OracleParameter("P_NOT_FEC_RECEPCION", contratos.NOT_FEC_RECEPCION);
                    param[53] = new OracleParameter("P_NOT_DOCUMENTO", contratos.NOT_DOCUMENTO);
                    param[54] = new OracleParameter("P_FEC_CULMINACION_CONTRATO", contratos.FEC_CULMINACION_CONTRATO);
                    param[55] = new OracleParameter("P_GRADO_ACADEMICO_GEN", contratos.GRADO_ACADEMICO_GEN);
                    param[56] = new OracleParameter("P_GRADO_ACADEMICO_FAG", contratos.GRADO_ACADEMICO_FAG);
                    param[57] = new OracleParameter("P_CARRERA_PROFESIONAL", contratos.CARRERA_PROFESIONAL);
                    param[58] = new OracleParameter("P_UNIVERSIDAD", contratos.UNIVERSIDAD);
                    param[59] = new OracleParameter("P_FLAG_REG_SUNEDU", contratos.FLAG_REG_SUNEDU);
                    param[60] = new OracleParameter("P_GRADO_ACADEMICO_ESP", contratos.GRADO_ACADEMICO_ESP);
                    param[61] = new OracleParameter("P_REQ_HAB_PROF", contratos.REQ_HAB_PROF);
                    param[62] = new OracleParameter("P_EXP_LAB_GENERAL", contratos.EXP_LAB_GENERAL);
                    param[63] = new OracleParameter("P_EXP_LAB_ESPECIFICA", contratos.EXP_LAB_ESPECIFICA);
                    param[64] = new OracleParameter("P_EXP_LAB_MATERIA", contratos.EXP_LAB_MATERIA);
                    param[65] = new OracleParameter("P_EXP_LAB_GENERAL_GRADO", contratos.EXP_LAB_GENERAL_GRADO);
                    param[66] = new OracleParameter("P_EXP_LAB_ACT_TDRS", contratos.EXP_LAB_ACT_TDRS);
                    param[67] = new OracleParameter("P_RES_CONTRATO_FEC_CULM", contratos.RES_CONTRATO_FEC_CULM);
                    param[68] = new OracleParameter("P_RES_CONTRATO_DOC", contratos.RES_CONTRATO_DOC);
                    param[69] = new OracleParameter("P_RES_CONTRATO_HR", contratos.RES_CONTRATO_HR);
                    param[70] = new OracleParameter("P_SEC_RESPONSABLE", contratos.SEC_RESPONSABLE);
                    param[71] = new OracleParameter("P_FISCAL_POST", contratos.FISCAL_POST);
                    param[72] = new OracleParameter("P_ENT_FIN_NOMBRE", contratos.ENT_FIN_NOMBRE);
                    param[73] = new OracleParameter("P_ENT_FIN_CTA_AHORRO", contratos.ENT_FIN_CTA_AHORRO);
                    param[74] = new OracleParameter("P_ENT_FIN_CTA_BANCARIA", contratos.ENT_FIN_CTA_BANCARIA);
                    param[75] = new OracleParameter("P_ENT_FIN_CTA_CCI", contratos.ENT_FIN_CTA_CCI);
                    param[76] = new OracleParameter("P_PJE_PUESTO", contratos.PJE_PUESTO);
                    param[77] = new OracleParameter("P_FLG_ESTADO", contratos.FLG_ESTADO);
                    param[78] = new OracleParameter("P_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);

                    OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);

                    int res = Int32.Parse(param[78].Value.ToString());
                    result.Data = contratos;
                    result.Error = false;
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return result;
        }
        public Respuesta<bool> EliminarContrato(int idRegistro)
        {
            var result = new Respuesta<bool>();
            var sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_ELIMINAR_CONTRATO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    int rpta = 0;

                    OracleParameter[] param = new OracleParameter[2];
                    param[0] = new OracleParameter("P_ID_REGISTRO", idRegistro);
                    param[1] = new OracleParameter("P_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);
                    OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);

                    int res = Int32.Parse(param[1].Value.ToString());

                    result.Data = (res == 1);
                    result.Error = false;
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return result;
        }

        #endregion

        #region Adendas
        
        public Respuesta<List<Cls_Ent_Adendas>> ListaAdendas(string codContrato, string campos, string valores, int pagina, int nregistros)
        {
            var result = new Respuesta<List<Cls_Ent_Adendas>>();
            string sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_LISTA_ADENDAS";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_COD_CONTRATO", codContrato);
                    p.Add("P_CAMPOS", campos);
                    p.Add("P_VALORES", valores);
                    p.Add("P_PAGINA", pagina);
                    p.Add("P_REG_PAGINA", nregistros);
                    p.Add("P_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    result.Data = db.Query<Cls_Ent_Adendas>(sp, p, commandType: CommandType.StoredProcedure).ToList();

                    result.Error = false;
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return result;
            return result;
        }
        public Respuesta<Cls_Ent_Adendas> ObtenerAdendas(int idCargaAdenda)
        {
            var result = new Respuesta<Cls_Ent_Adendas>();
            string sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_OBTENER_ADENDA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CARGA_ADENDA", idCargaAdenda);
                    p.Add("P_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    result.Data = db.Query<Cls_Ent_Adendas>(sp, p, commandType: CommandType.StoredProcedure).ToList()[0];

                    result.Error = false;
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return result;
        }
        public Respuesta<Cls_Ent_Adendas> EditarAdendas(int idAdendas, Cls_Ent_Adendas adendas)
        {
            var result = new Respuesta<Cls_Ent_Adendas>();
            string sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_ACTUALIZAR_ADENDAS";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    int rpta = 0;

                    OracleParameter[] param = new OracleParameter[11];

                    param[0] = new OracleParameter("P_ID_CARGA_ADENDAS", idAdendas);
                    param[1] = new OracleParameter("P_COD_CONTRATO", adendas.COD_CONTRATO);
                    param[2] = new OracleParameter("P_DOC_SOLICITUD", adendas.DOC_SOLICITUD);
                    param[3] = new OracleParameter("P_HOJA_RUTA", adendas.HOJA_RUTA);
                    param[4] = new OracleParameter("P_FEC_SUSCRIPCION", adendas.FEC_SUSCRIPCION);
                    param[5] = new OracleParameter("P_NUMERO", adendas.NUMERO);
                    param[6] = new OracleParameter("P_FEC_INICIO", adendas.FEC_INICIO);
                    param[7] = new OracleParameter("P_FEC_FIN", adendas.FEC_FIN);
                    param[8] = new OracleParameter("P_FEC_RECEPCION", adendas.FEC_RECEPCION);
                    param[9] = new OracleParameter("P_DOCUMENTO", adendas.DOCUMENTO);
                    param[10] = new OracleParameter("P_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);

                    OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);

                    int res = Int32.Parse(param[1].Value.ToString());
                    result.Data = adendas;
                    result.Error = false;
                }
            }
            catch(Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return result;
        }
        public Respuesta<bool> EliminarAdendas(int idAdendas)
        {
            var result = new Respuesta<bool>();
            string sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_ELIMINAR_ADENDA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    int rpta = 0;

                    OracleParameter[] param = new OracleParameter[2];

                    param[0] = new OracleParameter("P_ID_CARGA_ADENDA", idAdendas);
                    param[1] = new OracleParameter("P_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);

                    OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);

                    int res = Int32.Parse(param[1].Value.ToString());
                    result.Data = (res == 1);
                    result.Error = false;
                }
            }
            catch(Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return result;
        }

        #endregion

        #region Pagos
        public Respuesta<List<Cls_Ent_Pagos>> ListaPagos(string codContrato, string periodo, string numDocumento, string rucCas, decimal idEntidad, string campos, string valores, int pagina, int nregistros)
        {
            var result = new Respuesta<List<Cls_Ent_Pagos>>();
            string sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_LISTA_PAGOS";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_COD_CONTRATO", codContrato);
                    p.Add("P_PERIODO", periodo);
                    p.Add("P_NUM_DOCUMENTO", numDocumento);
                    p.Add("P_RUC_CAS", rucCas);
                    p.Add("P_ID_ENTIDAD", idEntidad);
                    p.Add("P_CAMPOS", campos);
                    p.Add("P_VALORES", valores);
                    p.Add("P_PAGINA", pagina);
                    p.Add("P_REG_PAGINA", nregistros);
                    p.Add("P_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    result.Data = db.Query<Cls_Ent_Pagos>(sp, p, commandType: CommandType.StoredProcedure).ToList();

                    result.Error = false;
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return result;
        }
        public Respuesta<bool> EliminarPago(int idPago)
        {
            var result = new Respuesta<bool>();
            string sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_ELIMINAR_PAGO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    int rpta = 0;

                    OracleParameter[] param = new OracleParameter[2];

                    param[0] = new OracleParameter("P_ID_REGISTRO", idPago);
                    param[1] = new OracleParameter("P_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);

                    OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);

                    int res = Int32.Parse(param[1].Value.ToString());
                    result.Data = (res == 1);
                    result.Error = false;
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return result;
        }
        public Respuesta<Cls_Ent_Pagos> ObtenerPagoxId(string idPago)
        {
            var res = new Respuesta<Cls_Ent_Pagos>();
            string sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_PAGO_OBTENER_X_ID";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_REGISTRO", idPago);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    res.Data = db.Query<Cls_Ent_Pagos>(sp, p, commandType: CommandType.StoredProcedure).ToList()[0];
                    res.Data.Result = true;
                    res.Error = false;
                }
            }
            catch (Exception ex)
            {
                res.Error = true;
                res.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }
            return res;
        }
        public Respuesta<bool> EditarPago(Cls_Ent_Pagos detalle)
        {
            var result = new Respuesta<bool>();
            string sp = "FAGPAC.PACK_CARGA_CONTRATOS.PRC_ACTUALIZAR_PAGO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    int rpta = 0;

                    OracleParameter[] param = new OracleParameter[59];

                    int iParam = 0;

                    param[iParam++] = new OracleParameter("P_ID_REGISTRO", detalle.ID_REGISTRO);
                    param[iParam++] = new OracleParameter("P_COD_CONTRATO", detalle.COD_CONTRATO);
                    param[iParam++] = new OracleParameter("P_PERIODO", detalle.PERIODO);
                    param[iParam++] = new OracleParameter("P_NUM_DOCUMENTO", detalle.NUM_DOCUMENTO);
                    param[iParam++] = new OracleParameter("P_RUC_CAS", detalle.RUC_CAS);
                    param[iParam++] = new OracleParameter("P_ID_ENTIDAD", detalle.ID_ENTIDAD);
                    param[iParam++] = new OracleParameter("P_ING_ENE", detalle.ING_ENE);
                    param[iParam++] = new OracleParameter("P_EGR_ENE", detalle.EGR_ENE);
                    param[iParam++] = new OracleParameter("P_EMP_ENE", detalle.EMP_ENE);
                    param[iParam++] = new OracleParameter("P_NET_ENE", detalle.NET_ENE);
                    param[iParam++] = new OracleParameter("P_ING_FEB", detalle.ING_FEB);
                    param[iParam++] = new OracleParameter("P_EGR_FEB", detalle.EGR_FEB);
                    param[iParam++] = new OracleParameter("P_EMP_FEB", detalle.EMP_FEB);
                    param[iParam++] = new OracleParameter("P_NET_FEB", detalle.NET_FEB);
                    param[iParam++] = new OracleParameter("P_ING_MAR", detalle.ING_MAR);
                    param[iParam++] = new OracleParameter("P_EGR_MAR", detalle.EGR_MAR);
                    param[iParam++] = new OracleParameter("P_EMP_MAR", detalle.EMP_MAR);
                    param[iParam++] = new OracleParameter("P_NET_MAR", detalle.NET_MAR);
                    param[iParam++] = new OracleParameter("P_ING_ABR", detalle.ING_ABR);
                    param[iParam++] = new OracleParameter("P_EGR_ABR", detalle.EGR_ABR);
                    param[iParam++] = new OracleParameter("P_EMP_ABR", detalle.EMP_ABR);
                    param[iParam++] = new OracleParameter("P_NET_ABR", detalle.NET_ABR);
                    param[iParam++] = new OracleParameter("P_ING_MAY", detalle.ING_MAY);
                    param[iParam++] = new OracleParameter("P_EGR_MAY", detalle.EGR_MAY);
                    param[iParam++] = new OracleParameter("P_EMP_MAY", detalle.EMP_MAY);
                    param[iParam++] = new OracleParameter("P_NET_MAY", detalle.NET_MAY);
                    param[iParam++] = new OracleParameter("P_ING_JUN", detalle.ING_JUN);
                    param[iParam++] = new OracleParameter("P_EGR_JUN", detalle.EGR_JUN);
                    param[iParam++] = new OracleParameter("P_EMP_JUN", detalle.EMP_JUN);
                    param[iParam++] = new OracleParameter("P_NET_JUN", detalle.NET_JUN);
                    param[iParam++] = new OracleParameter("P_ING_JUL", detalle.ING_JUL);
                    param[iParam++] = new OracleParameter("P_EGR_JUL", detalle.EGR_JUL);
                    param[iParam++] = new OracleParameter("P_EMP_JUL", detalle.EMP_JUL);
                    param[iParam++] = new OracleParameter("P_NET_JUL", detalle.NET_JUL);
                    param[iParam++] = new OracleParameter("P_ING_AGO", detalle.ING_AGO);
                    param[iParam++] = new OracleParameter("P_EGR_AGO", detalle.EGR_AGO);
                    param[iParam++] = new OracleParameter("P_EMP_AGO", detalle.EMP_AGO);
                    param[iParam++] = new OracleParameter("P_NET_AGO", detalle.NET_AGO);
                    param[iParam++] = new OracleParameter("P_ING_SET", detalle.ING_SET);
                    param[iParam++] = new OracleParameter("P_EGR_SET", detalle.EGR_SET);
                    param[iParam++] = new OracleParameter("P_EMP_SET", detalle.EMP_SET);
                    param[iParam++] = new OracleParameter("P_NET_SET", detalle.NET_SET);
                    param[iParam++] = new OracleParameter("P_ING_OCT", detalle.ING_OCT);
                    param[iParam++] = new OracleParameter("P_EGR_OCT", detalle.EGR_OCT);
                    param[iParam++] = new OracleParameter("P_EMP_OCT", detalle.EMP_OCT);
                    param[iParam++] = new OracleParameter("P_NET_OCT", detalle.NET_OCT);
                    param[iParam++] = new OracleParameter("P_ING_NOV", detalle.ING_NOV);
                    param[iParam++] = new OracleParameter("P_EGR_NOV", detalle.EGR_NOV);
                    param[iParam++] = new OracleParameter("P_EMP_NOV", detalle.EMP_NOV);
                    param[iParam++] = new OracleParameter("P_NET_NOV", detalle.NET_NOV);
                    param[iParam++] = new OracleParameter("P_ING_DIC", detalle.ING_DIC);
                    param[iParam++] = new OracleParameter("P_EGR_DIC", detalle.EGR_DIC);
                    param[iParam++] = new OracleParameter("P_EMP_DIC", detalle.EMP_DIC);
                    param[iParam++] = new OracleParameter("P_NET_DIC", detalle.NET_DIC);
                    param[iParam++] = new OracleParameter("P_ING_TOT", detalle.ING_TOT);
                    param[iParam++] = new OracleParameter("P_EGR_TOT", detalle.EGR_TOT);
                    param[iParam++] = new OracleParameter("P_EMP_TOT", detalle.EMP_TOT);
                    param[iParam++] = new OracleParameter("P_NET_TOT", detalle.NET_TOT);
                    param[iParam++] = new OracleParameter("PO_RPTA", OracleDbType.Int32, rpta, ParameterDirection.Output);
                    OracleHelper.ExecuteNonQuery(cnSTR, CommandType.StoredProcedure, sp, param);

                    int res = Int32.Parse(param[58].Value.ToString());

                    result.Data = (res == 1);
                    result.Error = false;
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Mensaje = ex.Message;
                Log.MensajeLog(ex.ToString(), sp);
            }

            return result;
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
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }
            return lista;
        }
        #endregion











    }
}
