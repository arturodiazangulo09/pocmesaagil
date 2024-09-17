using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.DataBaseHelpers;
using Dapper;
using Oracle.DataAccess.Client;
using System.Data;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Utilitario;

namespace MEF.PROYECTO.Data.Administracion
{
    public class Cls_Dat_Solicitudes_Coordinador : DataBaseHelper
    {
        public List<Cls_Ent_Solicitud_Personal> ListaSolicitudesCoordinador(Cls_Ent_Solicitud_Personal entidad)
        {
            List<Cls_Ent_Solicitud_Personal> lista = null;
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.USP_LISTA_SOLICITUD";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("P_ANIO_PROCESO", entidad.ANIO_PROCESO);
                    
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Solicitud_Personal>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Solicitud_Personal UPD_CALIFICAR_DOCUMENTOS(Cls_Ent_Solicitud_Personal entidad)
        {
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.PRC_CALIFICAR_DOCUMENTOS";
            try
            {
                OracleParameter[] param = new OracleParameter[25];

                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_FLG_ANEXO1", entidad.FLG_ANEXO1);
                param[2] = new OracleParameter("P_FLG_ANEXO2", entidad.FLG_ANEXO2);
                param[3] = new OracleParameter("P_FLG_ANEXO3", entidad.FLG_ANEXO3);
                param[4] = new OracleParameter("P_FLG_ANEXO4", entidad.FLG_ANEXO4);
                param[5] = new OracleParameter("P_FLG_ANEXO5", entidad.FLG_ANEXO5);
                param[6] = new OracleParameter("P_FLG_ANEXO7", entidad.FLG_ANEXO7);
                param[7] = new OracleParameter("P_FLG_BANCO", entidad.FLG_BANCO);
                param[8] = new OracleParameter("P_FLG_H_PROFESIONAL", entidad.FLG_H_PROFESIONAL);
                param[9] = new OracleParameter("P_FLG_OTROS", entidad.FLG_OTROS);
                param[10] = new OracleParameter("P_FLG_INFORME_F", entidad.FLG_INFORME_F);
                param[11] = new OracleParameter("P_FLG_DATOS_SECTOR", entidad.FLG_DATOS_SECTOR);
                param[12] = new OracleParameter("P_FLG_FORMATOA", entidad.FLG_FORMATOA);
                param[13] = new OracleParameter("P_FLG_FORMATOB", entidad.FLG_FORMATOB);
                param[14] = new OracleParameter("P_FLG_FORMATOC", entidad.FLG_FORMATOC);
                param[15] = new OracleParameter("P_FLG_FORMATOD", entidad.FLG_FORMATOD);
                param[16] = new OracleParameter("P_FLG_FORMATOE", entidad.FLG_FORMATOE);
                param[17] = new OracleParameter("P_FLG_FORMATOH", entidad.FLG_FORMATOH);
                param[18] = new OracleParameter("P_FLG_PAC_ANEXO2", entidad.FLG_PAC_ANEXO2);
                param[19] = new OracleParameter("P_FLG_PAC_H_PROFESIONAL", entidad.FLG_PAC_H_PROFESIONAL);
                param[20] = new OracleParameter("P_FLG_CUMPLE_REQUISITOS_UTP", entidad.FLG_CUMPLE_REQUISITOS_UTP);
                param[21] = new OracleParameter("P_FLG_CALIFICA_REQUISITOS_UTP", entidad.FLG_CALIFICA_REQUISITOS_UTP);
                param[22] = new OracleParameter("P_TIPO", entidad.ACCION);
                param[23] = new OracleParameter("P_USU_MODIFICA", entidad.USU_INGRESO);
                param[24] = new OracleParameter("P_IP_MODIFICA", entidad.IP_INGRESO);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.FLG_OK = true;
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                entidad.DES_ERROR = ex.Message;
                entidad.FLG_OK = false;
            }
            return entidad;
        }
        public List<Cls_Ent_Solicitud_Personal> ProximoCodContrato()
        {
            List<Cls_Ent_Solicitud_Personal> lista = null;
            string sp = "FAGPAC.PACK_CONTRATOS_FAGPAC.PRC_PROXIMO_CONTRATO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Solicitud_Personal>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch(Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }
            return lista;
        }
       
        public List<Cls_Ent_Solicitud_Personal> GetDatosSolicitud( int ID_SOLICITUD)
        {
            List<Cls_Ent_Solicitud_Personal> lista = null;
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.USP_DOCUMENTO_SOLICITUD";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Solicitud_Personal>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }
            return lista;
        }
        public List<Cls_Ent_Solicitud_Personal> GetDatosAdenda(int ID_CONTRATO_DET)
        {
            List<Cls_Ent_Solicitud_Personal> lista = null;
            string sp = "FAGPAC.PACK_CONTRATOS_FAGPAC.PRC_DOCUMENTO_CONTRATO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CONTRATO_DET", ID_CONTRATO_DET);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Solicitud_Personal>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }
            return lista;
        }
        

        public Cls_Ent_Solicitud_Personal InsContratoSolicitud(Cls_Ent_Solicitud_Personal entidad)
        {
            string sp = "FAGPAC.PACK_CONTRATOS_FAGPAC.PRC_INSERT_CONTRATO";
            try
            {
                OracleParameter[] param = new OracleParameter[15];

                param[0] = new OracleParameter("P_ID_CONTRATO", entidad.ID_CONTRATO);
                param[1] = new OracleParameter("P_USUARIO_UPT", entidad.USU_INGRESO);
                param[2] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[3] = new OracleParameter("P_ID_ARCHIVO", entidad.ID_ARCHIVO);
                param[4] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[5] = new OracleParameter("P_IP", entidad.IP_INGRESO);
                param[6] = new OracleParameter("P_NUM_CONTRATO", entidad.NUM_CONTRATO);
                param[7] = new OracleParameter("P_ID_ARCHIVO_RNSDD", entidad.ID_ARCHIVO_RNSDD);
                param[8] = new OracleParameter("P_ID_ARCHIVO_REDAM", entidad.ID_ARCHIVO_REDAM);
                param[9] = new OracleParameter("P_ID_ARCHIVO_REDERECI", entidad.ID_ARCHIVO_REDERECI);
                param[10] = new OracleParameter("P_ID_ARCHIVO_AIRSHP", entidad.ID_ARCHIVO_AIRSHP);
                param[11] = new OracleParameter("P_ID_ARCHIVO_OTROS", entidad.ID_ARCHIVO_OTROS);
                param[12] = new OracleParameter("P_FECHA_LIM_REN", entidad.FECHA_LIM_REN);
                param[13] = new OracleParameter("P_FLG_OTROS", entidad.FLG_OTROS);
                param[14] = new OracleParameter("P_FLG_HIST", entidad.FLG_HISTORICO);
               

                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.FLG_OK = true;
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                entidad.DES_ERROR = ex.Message;
                entidad.FLG_OK = false;
            }
            return entidad;
        }
        /****** INICIO PROCESO SOLICITUD DE PAGO ****/
        public List<Cls_Ent_Solicitud_Pago> ListaSolicitudPagoEntidadUTP(Cls_Ent_Solicitud_Pago entidad)
        
        
        {
            List<Cls_Ent_Solicitud_Pago> lista = null;
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.USP_LISTA_PERIODO_PAGO_UTP";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("P_ANIO_PROCESO", entidad.ANIO_PROCESO);
                    p.Add("P_NUM_MES", entidad.NUM_MES);
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
                    p.Add("P_ID_CONFORMIDAD", entidad.ID_CONFORMIDAD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Solicitud_Pago>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Solicitud_Pago UpdateReevaluacionPago(Cls_Ent_Solicitud_Pago entidad)
        {
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.SP_REEVALUACION_INSERT";
            try
            {
                OracleParameter[] param = new OracleParameter[8];

                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_DESCRIPCION", entidad.DESCRIPCION);
                param[2] = new OracleParameter("P_NOMBRE_COMPLETO", entidad.CONSULTOR);
                param[3] = new OracleParameter("P_TIPO", entidad.TIPO_PROCESO);
                param[4] = new OracleParameter("P_ID_CONFORMIDAD", entidad.ID_CONFORMIDAD);
                param[5] = new OracleParameter("P_NR_MES", entidad.NR_MES);
                param[6] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[7] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.FLG_OK = true;
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                entidad.DES_ERROR = ex.Message;
                entidad.FLG_OK = false;
            }
            return entidad;
        }
        /****** FIN PROCESO SOLICITUD DE PAGO  ****/
        /****** INICIO PROCESO SOLICITUD ADENDA****/
        public List<Cls_Ent_Lista_Contr_Aden_Consultor> ListaContraAdendaPersona (Cls_Ent_Lista_Contr_Aden_Consultor entidad)
        {
            List<Cls_Ent_Lista_Contr_Aden_Consultor> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_CONTRA_ADEN_PERS";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
                    p.Add("P_NUM_DOCUMENTO", entidad.NUM_DOCUMENTO);
                    p.Add("P_CONSULTOR", entidad.CONSULTOR);
                    p.Add("P_ANIO", entidad.ANIO);
                    if (entidad.FECHA_ENVIO_UTP.ToString("dd/MM/yyyy")=="01/01/0001")
                    {
                        p.Add("P_FECHA_ENVIO_UTP", null);
                    }
                    else
                    {
                        p.Add("P_FECHA_ENVIO_UTP", entidad.FECHA_ENVIO_UTP.ToString("dd/MM/yyyy"));
                    }
            
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Lista_Contr_Aden_Consultor>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Lista_Contr_Aden_Consultor UpdateBajaAnulacion(Cls_Ent_Lista_Contr_Aden_Consultor entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPD_BAJA_ANUL_CONTRATO";
            try
            {
                OracleParameter[] param = new OracleParameter[5];

                param[0] = new OracleParameter("P_ID_CONTRATO", entidad.ID_CONTRATO);
                param[1] = new OracleParameter("P_FECHA_BAJA", entidad.FECHA_BAJA.ToString("dd/MM/yyyy"));
                param[2] = new OracleParameter("P_ARCHIVO_AB", entidad.ID_ARCHIVO_A_B);
                param[3] = new OracleParameter("P_TIPO", entidad.TIPO);
                param[4] = new OracleParameter("P_DESC_AB", entidad.DESCRIPCION);

                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.FLG_OK = true;
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                entidad.DES_ERROR = ex.Message;
                entidad.FLG_OK = false;
            }
            return entidad;
        }
        /****** FIN PROCESO SOLICITUD ADENDA  ****/
        /****** INICIO PROCESO SOLICITUD SUSPENSION****/
        public Cls_Ent_Descanso UpdEnvioSuspensionAprueba(Cls_Ent_Descanso entidad)
        {
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.PRC_UPD_SUSPEN_APRUEBA";
            try
            {
                OracleParameter[] param = new OracleParameter[3];

                param[0] = new OracleParameter("P_ID_SUSPENSION", entidad.ID_SUSPENSION);
                param[1] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[2] = new OracleParameter("P_IP_INGRESO", entidad.IP_PC);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.FLG_OK = true;
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                entidad.DES_ERROR = ex.Message;
                entidad.FLG_OK = false;
            }
            return entidad;
        }
        /****** FIN PROCESO SOLICITUD SUSPENSION  ****/
        /****** INICIO PROCESO SOLICITUD SUSPENSION****/
        public Cls_Ent_Covid UpdEnvioCovidAprueba(Cls_Ent_Covid entidad)
        {
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.PRC_UPD_COVID_APRUEBA";
            try
            {
                OracleParameter[] param = new OracleParameter[3];

                param[0] = new OracleParameter("P_ID_COVID", entidad.ID_COVID);
                param[1] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[2] = new OracleParameter("P_IP_INGRESO", entidad.IP_PC);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.FLG_OK = true;
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                entidad.DES_ERROR = ex.Message;
                entidad.FLG_OK = false;
            }
            return entidad;
        }
        /****** FIN PROCESO SOLICITUD SUSPENSION  ****/
        /****** INICIO DETALLE DE DOCUMENTO ****/
        public List<Cls_Ent_Documentos_Contrato> ListaDocumentosContrato(Cls_Ent_Documentos_Contrato entidad)
        {
            List<Cls_Ent_Documentos_Contrato> lista = null;
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.USP_LISTA_DOCUMENTOS_CONTRATO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("P_NUM_CONTRATO", entidad.NUM_CONTRATO);
                    p.Add("P_ANIO_CONTRATO", entidad.ANIO_CONTRATO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Documentos_Contrato>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }

        /****** FIN DETALLE DE DOCUMENTO   ****/
        /****** INICIO PLANILLA DE PAGO ****/
        public List<Cls_Ent_Solicitud_Pago> ListaPeriodoPlanilla(Cls_Ent_Solicitud_Pago entidad)
        {
            List<Cls_Ent_Solicitud_Pago> lista = null;
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.USP_LISTA_PERIODO_PLANILLA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("P_NUM_MES", entidad.NUM_MES);
                    p.Add("P_ANIO", entidad.ANIO);
                    p.Add("P_MES_PLANILLA", entidad.MES_PLANILLA);
                    p.Add("P_NR_PLANILLA", entidad.NR_PLANILLA);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Solicitud_Pago>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Solicitud_Pago UpdAsignarPlanilla(Cls_Ent_Solicitud_Pago entidad)
        {
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.PRC_ASIGNAR_PLANILLA";
            try
            {
                OracleParameter[] param = new OracleParameter[6];

                param[0] = new OracleParameter("P_NR_PLANILLA", entidad.NR_PLANILLA);
                param[1] = new OracleParameter("P_MES_PLANILLA", entidad.MES_PLANILLA);
                param[2] = new OracleParameter("P_FECHA_PAGO", entidad.FECHA_PAGO.ToString("dd/MM/yyyy"));
                param[3] = new OracleParameter("P_ANIO", entidad.ANIO);
                param[4] = new OracleParameter("P_NR_MES", entidad.NR_MES);
                param[5] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.FLG_OK = true;
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
                entidad.DES_ERROR = ex.Message;
                entidad.FLG_OK = false;
            }
            return entidad;
        }
        /****** FIN PLANILLA DE PAGO ****/
    }
}

