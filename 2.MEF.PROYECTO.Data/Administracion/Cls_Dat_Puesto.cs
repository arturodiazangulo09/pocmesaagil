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
using MEF.PROYECTO.Utilitario;
namespace MEF.PROYECTO.Data.Administracion
{
    public class Cls_Dat_Puesto : DataBaseHelper
    {
        public List<Cls_Ent_Puesto> ListaPuestos(Cls_Ent_Puesto entidad)
        {
            List<Cls_Ent_Puesto> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_PUESTOS_PAC";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_PUESTO", entidad.ID_PUESTO);
                    p.Add("P_TIPO_FICHA", entidad.TIPO_FICHA);
                    p.Add("P_DES_PUESTO", entidad.DES_PUESTO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Puesto>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Puesto MantenimientoPuestos(Cls_Ent_Puesto entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_MNT_PUESTO_PAC";
            try
            {
                OracleParameter[] param = new OracleParameter[22];
                //OracleParameter blobParameter = new OracleParameter();
                //blobParameter.OracleDbType = OracleDbType.Blob;
                //blobParameter.ParameterName = "P_ARCHIVO_PUESTO";
                //blobParameter.Value = entidad.ARCHIVO_PUESTO;
                param[0] = new OracleParameter("P_ID_PUESTO", entidad.ID_PUESTO);
                param[1] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[2] = new OracleParameter("P_DES_PUESTO", entidad.DES_PUESTO);
                param[3] = new OracleParameter("P_TIPO_FICHA", entidad.TIPO_FICHA);
                //param[4] = blobParameter;
                param[4] = new OracleParameter("P_ARCHIVO_PUESTO", entidad.ARCHIVO_PUESTO);
                param[5] = new OracleParameter("P_NOMBRE_ARCHIVO_PUESTO", entidad.NOMBRE_ARCHIVO_PUESTO);
                param[6] = new OracleParameter("P_P_H_1_1", entidad.P_H_1_1);
                param[7] = new OracleParameter("P_P_H_1_2", entidad.P_H_1_2);
                param[8] = new OracleParameter("P_P_H_1_3", entidad.P_H_1_3);
                param[9] = new OracleParameter("P_P_H_2_1", entidad.P_H_2_1);
                param[10] = new OracleParameter("P_P_H_2_2", entidad.P_H_2_2);
                param[11] = new OracleParameter("P_P_H_3_1", entidad.P_H_3_1);
                param[12] = new OracleParameter("P_P_H_3_2", entidad.P_H_3_2);
                param[13] = new OracleParameter("P_P_I_1_1", entidad.P_I_1_1);
                param[14] = new OracleParameter("P_P_I_2_1", entidad.P_I_2_1);
                param[15] = new OracleParameter("P_P_I_3_1", entidad.P_I_3_1);
                param[16] = new OracleParameter("P_P_I_4_1", entidad.P_I_4_1);
                param[17] = new OracleParameter("P_P_TOTAL", entidad.P_TOTAL);
                param[18] = new OracleParameter("P_MONTO_PUESTO", entidad.MONTO_PUESTO);
                param[19] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[20] = new OracleParameter("P_IP", entidad.IP_INGRESO);
                param[21] = new OracleParameter("P_TIPO", entidad.ACCION);
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
