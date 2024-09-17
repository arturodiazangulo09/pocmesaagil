using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Data.DataBaseHelpers;
using Dapper;
using Oracle.DataAccess.Client;
using System.Data;
using MEF.PROYECTO.Utilitario;

namespace MEF.PROYECTO.Data.Coordinador
{
    public class Cls_Dat_Suspension: DataBaseHelper
    {
        public Cls_Ent_Reevaluacion InsReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.SP_REEVALUACION_INSERT_SUS";
            try
            {
                OracleParameter[] param = new OracleParameter[6];

                param[0] = new OracleParameter("P_ID_SUSPENSION", entidad.ID_SUSPENSION);
                param[1] = new OracleParameter("P_DESCRIPCION", entidad.DES_REEVALUACION);
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
        public List<Cls_Ent_Reevaluacion> ListaReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_REEVALUACION_SUS";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SUSPENSION", entidad.ID_SUSPENSION);
                    p.Add("P_TIPO", entidad.TIPO);
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
        public Cls_Ent_Descanso UpdArchivoSuspension(Cls_Ent_Descanso entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPD_DOCUMENTO_SUSPEN";
            try
            {
                OracleParameter[] param = new OracleParameter[4];

                param[0] = new OracleParameter("P_ID_SUSPENSION", entidad.ID_SUSPENSION);
                param[1] = new OracleParameter("P_ID_ARCHIVO_C", entidad.ID_ARCHIVO_C);
                param[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_PC);
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
        public Cls_Ent_Descanso UpdEnvioSuspension(Cls_Ent_Descanso entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPD_ESTADO_ENVIO_SUSPEN";
            try
            {
                OracleParameter[] param = new OracleParameter[5];

                param[0] = new OracleParameter("P_ID_SUSPENSION", entidad.ID_SUSPENSION);
                param[1] = new OracleParameter("P_ID_TRAMITE", entidad.ID_TRAMITE);
                param[2] = new OracleParameter("P_NR_TRAMITE", entidad.NR_TRAMITE);
                param[3] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[4] = new OracleParameter("P_IP_INGRESO", entidad.IP_PC);
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

    }
}
