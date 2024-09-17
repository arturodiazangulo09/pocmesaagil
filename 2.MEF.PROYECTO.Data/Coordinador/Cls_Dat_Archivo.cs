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
    public class Cls_Dat_Archivo: DataBaseHelper
    {
        public Cls_Ent_Archivo InsertAchivoSustento(Cls_Ent_Archivo entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_ARCHIVOS_UTP";
            try
            {
                OracleParameter[] param = new OracleParameter[6];
                OracleParameter BLOB_ARCHIVO = new OracleParameter();
                BLOB_ARCHIVO.OracleDbType = OracleDbType.Blob;
                BLOB_ARCHIVO.ParameterName = "P_ARCHIVO";
                BLOB_ARCHIVO.Value = entidad.ARCHIVO;
                param[0] = BLOB_ARCHIVO;
                param[1] = new OracleParameter("P_NOMBRE_ARCHIVO", entidad.NOMBRE_ARCHIVO);
                param[2] = new OracleParameter("P_DESCRIPCION", entidad.DESCRIPCION);
                param[3] = new OracleParameter("P_USUARIO_CREACION", entidad.USU_INGRESO);
                param[4] = new OracleParameter("P_IP_CREACION", entidad.IP_INGRESO);
                param[5] = new OracleParameter("PO_ID_SOLICITUD", OracleDbType.Int32, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.ID_ARCHIVO = int.Parse(param[5].Value.ToString());
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
        public List<Cls_Ent_Archivo> ListaArchivoSustento(Cls_Ent_Archivo entidad)
        {
            List<Cls_Ent_Archivo> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_ARCHIVO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_ARCHIVO", entidad.ID_ARCHIVO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Archivo>(sp, p, commandType: CommandType.StoredProcedure).ToList();
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
