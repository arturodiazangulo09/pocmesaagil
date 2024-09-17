using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Data.DataBaseHelpers;
using Dapper;
using Oracle.DataAccess.Client;
using System.Data;
using MEF.PROYECTO.Utilitario;

namespace MEF.PROYECTO.Data.Coordinador
{
    public class Cls_Dat_Adenda: DataBaseHelper
    {
        public List<Cls_Ent_Contrato> ListaContratos(Cls_Ent_Contrato entidad)
        {
            List<Cls_Ent_Contrato> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_CONTRATO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CONTRATO", entidad.ID_CONTRATO);
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Contrato>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Contrato_Ren> ListaContratosRenovar(int entidad)
        {
            List<Cls_Ent_Contrato_Ren> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_CONTRATO_RENOVACION";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_ENTIDAD", entidad);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Contrato_Ren>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Adenda MantenimientoSolicitudAdenda(Cls_Ent_Adenda entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_SOLICITUD_ADENDA";
            try
            {
                OracleParameter[] param = new OracleParameter[14];
                param[0] = new OracleParameter("P_ID_CONTRATO_DET", entidad.ID_CONTRATO_DET);
                param[1] = new OracleParameter("P_ID_CONTRATO", entidad.ID_CONTRATO);
                param[2] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[3] = new OracleParameter("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                param[4] = new OracleParameter("P_ID_COORDINADOR", entidad.ID_COORDINADOR);
                param[5] = new OracleParameter("P_FECHA_INICIO", entidad.FECHA_INI.ToString("dd/MM/yyyy"));
                param[6] = new OracleParameter("P_FECHA_FIN", entidad.FECHA_FIN.ToString("dd/MM/yyyy"));
                param[7] = new OracleParameter("P_MONTO", entidad.MONTO);
                param[8] = new OracleParameter("P_NOMBRE_PUESTO", entidad.NOMBRE_PUESTO);
                param[9] = new OracleParameter("P_ID_ARCHIVO", entidad.ID_ARCHIVO_SUSTENTO);
                param[10] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[11] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                param[12] = new OracleParameter("P_TIPO", entidad.TIPO);
                param[13] = new OracleParameter("PO_ID_CONTRATO_DET", OracleDbType.Int32, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.ID_CONTRATO_DET = int.Parse(param[13].Value.ToString());
                if (entidad.listaRenumeracion != null)
                {
                    foreach (var item in entidad.listaRenumeracion)
                    {
                        string sp_9 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_PERIODO_ADENDA_PAGO";
                        var paramC = new OracleParameter[10];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", item.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_ID_ENTIDAD", item.ID_ENTIDAD);
                        paramC[2] = new OracleParameter("P_ANIO", item.ANIO);
                        paramC[3] = new OracleParameter("P_NUM_MES", item.ID_MES);
                        paramC[4] = new OracleParameter("P_DES_MES", item.DES_MES);
                        paramC[5] = new OracleParameter("P_MONTO", item.MONTO);
                        paramC[6] = new OracleParameter("P_TIPO_PROCESO", item.TIPO_PROCESO);
                        paramC[7] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[8] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        paramC[9] = new OracleParameter("P_ID_CONTRATO_ADENDA", entidad.ID_CONTRATO_DET);
                        
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_9, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_9);
                            entidad.DES_ERROR = ex.Message;
                            entidad.FLG_OK = false;
                        }
                    }
                }
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
        public List<Cls_Ent_Adenda> ListaDetalleContratos(Cls_Ent_Adenda entidad)
        {
            List<Cls_Ent_Adenda> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_DETALLE_CONTRATO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CONTRATO_DET", entidad.ID_CONTRATO_DET);
                    p.Add("P_ID_CONTRATO", entidad.ID_CONTRATO);
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
                    p.Add("P_NUM_PROCESO", entidad.NUM_PROCESO);
                    p.Add("P_ANIO_PROCESO", entidad.ANIO_PROCESO);
                    p.Add("P_DOCUMENTO_CONSULTOR", entidad.DOCUMENTO_CONSULTOR);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Adenda>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }

        public List<Cls_Ent_Adenda> ListaDetalleAdendas(Cls_Ent_Adenda entidad)
        {
            List<Cls_Ent_Adenda> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_SOLICITUD_ADENDA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CONTRATO_DET", entidad.CODIGOS_CONTRATO_DET);
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Adenda>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Adenda UpdArchivoAdenda(Cls_Ent_Adenda entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPD_DOCUMENTO_ADENDA";
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("P_ID_CONTRATO_DET", entidad.ID_CONTRATO_DET);
                param[1] = new OracleParameter("P_ID_ARCHIVO", entidad.ID_ARCHIVO);
                param[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);

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
        public Cls_Ent_Adenda UpdEstadoAdenda(Cls_Ent_Adenda entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPD_ESTADO_ADENDA";
            try
            {
                OracleParameter[] param = new OracleParameter[12];
                param[0] = new OracleParameter("P_ID_CONTRATO_DET", entidad.ID_CONTRATO_DET);
                param[1] = new OracleParameter("P_IDEDOCODIGO", entidad.IDEDOCODIGO);

                param[2] = new OracleParameter("P_ID_TRAMITE", entidad.ID_TRAMITE);
                param[3] = new OracleParameter("P_NR_TRAMITE", entidad.NR_TRAMITE);

                param[4] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[5] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                param[6] = new OracleParameter("P_ID_ARCHIVO", entidad.ID_ARCHIVO);

                param[7] = new OracleParameter("P_ID_ARCHIVO_RNSDD", entidad.ID_ARCHIVO_RNSDD);
                param[8] = new OracleParameter("P_ID_ARCHIVO_REDAM", entidad.ID_ARCHIVO_REDAM);
                param[9] = new OracleParameter("P_ID_ARCHIVO_REDERECI", entidad.ID_ARCHIVO_REDERECI);
                param[10] = new OracleParameter("P_ID_ARCHIVO_AIRSHP", entidad.ID_ARCHIVO_AIRSHP);
                param[11] = new OracleParameter("P_ID_ARCHIVO_OTROS", entidad.ID_ARCHIVO_OTROS);

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
        public Cls_Ent_Reevaluacion InsReevaluacionAdenda(Cls_Ent_Reevaluacion entidad)
        {
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.SP_REEVALUACION_INSERT_ADENDA";
            try
            {
                OracleParameter[] param = new OracleParameter[6];

                param[0] = new OracleParameter("P_ID_CONTRATO_DET", entidad.ID_CONTRATO_DET);
                param[1] = new OracleParameter("P_DES_REEVALUACION", entidad.DES_REEVALUACION);
                param[2] = new OracleParameter("P_NOMBRE_COMPLETO", entidad.NOMBRE_COMPLETO);
                param[3] = new OracleParameter("P_TIPO", entidad.TIPO);
                param[4] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[5] = new OracleParameter("P_IP_INGRESO", entidad.IP_PC);
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
        public List<Cls_Ent_Reevaluacion> ListaReevaluacionAdenda(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            string sp = "FAGPAC.PACK_COORDINADOR_UTP.USP_LISTA_REEVALUACION_ADENDA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CONTRATO_DET", entidad.ID_CONTRATO_DET);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Reevaluacion>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
    }
}
