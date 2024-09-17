using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Oracle.DataAccess.Client;
using System.Data;
using MEF.PROYECTO.Utilitario;
using MEF.PROYECTO.Data.DataBaseHelpers;
using MEF.PROYECTO.Entity.Personal;

namespace MEF.PROYECTO.Data.Personal
{
    public class Cls_Dat_Covid: DataBaseHelper
    {
        public Cls_Ent_Covid MentenimientoSolicitud_Covid(Cls_Ent_Covid entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_MNT_SOLICITUD_COVID";
            try
            {
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("P_ID_COVID", entidad.ID_COVID);
                param[1] = new OracleParameter("P_ID_CONTRATO", entidad.ID_CONTRATO);
                param[2] = new OracleParameter("P_FECHA_INICIO", entidad.FECHA_INICIO.ToString("dd/MM/yyyy"));
                param[3] = new OracleParameter("P_FECHA_FIN", entidad.FECHA_FIN.ToString("dd/MM/yyyy"));
                param[4] = new OracleParameter("P_ID_ARCHIVO_U", entidad.ID_ARCHIVO_U);
                param[5] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[6] = new OracleParameter("P_IP", entidad.IP_PC);
                param[7] = new OracleParameter("P_TIPO", entidad.ACCION);
                param[8] = new OracleParameter("P_ID_ARCHIVO_CM", entidad.ID_ARCHIVO_CM);
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
        public List<Cls_Ent_Covid> ListaSolicitud_Covid(Cls_Ent_Covid entidad)
        {
            List<Cls_Ent_Covid> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_COVID";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CONTRATO", entidad.ID_CONTRATO);
                    p.Add("P_ID_COVID", entidad.ID_COVID);
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("P_ANIO", entidad.ANIO);
                    p.Add("P_NUM_CONTRATO", entidad.NUM_CONTRATO);
                    p.Add("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Covid>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Covid UpdEstado_Covid(Cls_Ent_Covid entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_UPD_COVID_ESTADO";
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("P_ID_COVID", entidad.ID_COVID);
                param[1] = new OracleParameter("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
                param[2] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[3] = new OracleParameter("P_IP", entidad.IP_PC);
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
