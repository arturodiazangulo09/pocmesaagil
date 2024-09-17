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
    public class Cls_Dat_Coordinador: DataBaseHelper
    {
        public Cls_Ent_Coordinador InsSolicitudCoordinador(Cls_Ent_Coordinador entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_COORDINADOR";
            try
            {
                OracleParameter[] param = new OracleParameter[28];
                //OracleParameter BLOB_DNI = new OracleParameter();
                //BLOB_DNI.OracleDbType = OracleDbType.Blob;
                //BLOB_DNI.ParameterName = "P_ARCHIVO_DNI";
                //BLOB_DNI.Value = entidad.ARCHIVO_DNI;
                //OracleParameter BLOB_DOCUMENTO = new OracleParameter();
                //BLOB_DOCUMENTO.OracleDbType = OracleDbType.Blob;
                //BLOB_DOCUMENTO.ParameterName = "P_ARCHIVO_SUSTENTO";
                //BLOB_DOCUMENTO.Value = entidad.ARCHIVO_SUSTENTO;
                param[0] = new OracleParameter("P_ID_COORDINADOR", entidad.ID_COORDINADOR);
                param[1] = new OracleParameter("P_USUARIO", entidad.USUARIO);
                param[2] = new OracleParameter("P_CONTRASENA", entidad.CONTRASENA);
                param[3] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[4] = new OracleParameter("P_ID_TIPO_DOCUMENTO", entidad.ID_TIPO_DOCUMENTO);
                param[5] = new OracleParameter("P_NUM_DOCUMENTO", entidad.NUM_DOCUMENTO);
                param[6] = new OracleParameter("P_APELLIDO_PATERNO", entidad.APELLIDO_PATERNO);
                param[7] = new OracleParameter("P_APELLIDO_MATERNO", entidad.APELLIDO_MATERNO);
                param[8] = new OracleParameter("P_NOMBRES", entidad.NOMBRES);
                param[9] = new OracleParameter("P_ID_TIPO_VINCULO_ENT", entidad.ID_TIPO_VINCULO_ENT);
                param[10] = new OracleParameter("P_CARGO", entidad.CARGO);
                param[11] = new OracleParameter("P_DOCUMENTO_ACRE", entidad.DOCUMENTO_ACRE);
                param[12] = new OracleParameter("P_CORREO_NOTIFICADOR", entidad.CORREO_NOTIFICADOR);
                param[13] = new OracleParameter("P_COD_DEPARTAMENTO_ENT", entidad.COD_DEPARTAMENTO_ENT);
                param[14] = new OracleParameter("P_COD_PROVINCIA_ENT", entidad.COD_PROVINCIA_ENT);
                param[15] = new OracleParameter("P_COD_DISTRITO_ENT", entidad.COD_DISTRITO_ENT);
                param[16] = new OracleParameter("P_DIRECCION_ENT", entidad.DIRECCION_ENT);
                param[17] = new OracleParameter("P_ARCHIVO_DNI", entidad.ARCHIVO_DNI); //BLOB_DNI; 
                param[18] = new OracleParameter("P_NOMBRE_ARCHIVO_DNI", entidad.NOMBRE_ARCHIVO_DNI);
                param[19] = new OracleParameter("P_ARCHIVO_SUSTENTO", entidad.ARCHIVO_SUSTENTO);  //BLOB_DOCUMENTO;
                param[20] = new OracleParameter("P_NOMBRE_ARCHIVO_SUS", entidad.NOMBRE_ARCHIVO_SUS);
                param[21] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[22] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                param[23] = new OracleParameter("P_FLG_FAG", entidad.FLG_FAG);
                param[24] = new OracleParameter("P_FLG_PAC", entidad.FLG_PAC);
                param[25] = new OracleParameter("P_TIPO_USUARIO", entidad.TIPO_USUSARIO);
                param[26] = new OracleParameter("P_DEPENDENCIA", entidad.DEPENDENCIA);
                param[27] = new OracleParameter("PO_ID_COORDINADOR", OracleDbType.Int32, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.ID_COORDINADOR = int.Parse(param[27].Value.ToString());
                foreach (var item in entidad.listaContacto)
                {
                    string sp_2 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_CONTACTO_COORDINADOR";
                    var paramC = new OracleParameter[8];
                    paramC[0] = new OracleParameter("P_ID_CONTACTO", item.ID_CONTACTO);
                    paramC[1] = new OracleParameter("P_ID_COORDINADOR", entidad.ID_COORDINADOR);
                    paramC[2] = new OracleParameter("P_TELEFONO", item.TELEFONO);
                    paramC[3] = new OracleParameter("P_ANEXO", item.ANEXO);
                    paramC[4] = new OracleParameter("P_CELULAR", item.CELULAR);
                    paramC[5] = new OracleParameter("P_CORREO", item.CORREO);
                    paramC[6] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                    paramC[7] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                    try
                    {
                        OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_2, paramC);
                    }
                    catch (Exception ex)
                    {
                        Log.MensajeLog(ex.Message, sp_2);
                        entidad.DES_ERROR = ex.Message;
                        entidad.FLG_OK = false;
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
        public List<Cls_Ent_Coordinador> ListaCoordinadores(Cls_Ent_Coordinador entidad)
        {
            List<Cls_Ent_Coordinador> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_COORDINADORES";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ESTADO", entidad.FLG_SOLICITUD);
                    p.Add("P_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Coordinador>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Coordinador MantenimientoAccionesCoordinador(Cls_Ent_Coordinador entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_UPD_COORDINADOR_ACCIONES";
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("P_ID_COORDINADOR", entidad.ID_COORDINADOR);
                param[1] = new OracleParameter("P_OBSERVACION", entidad.OBSERVACION_SOLICITUD);
                param[2] = new OracleParameter("P_TIPO_ACCION", entidad.ACCION);
                param[3] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[4] = new OracleParameter("P_IP", entidad.IP_PC);
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
        public Cls_Ent_Coordinador UpdateContrasenaCoordinador(Cls_Ent_Coordinador entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPD_CONTRASENA";
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("P_ID_COORDINADOR", entidad.ID_COORDINADOR);
                param[1] = new OracleParameter("P_CONTRASENA", entidad.CONTRASENA);
                param[2] = new OracleParameter("P_TIPO_ACCION", entidad.ACCION);
                param[3] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[4] = new OracleParameter("P_IP", entidad.IP_PC);
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
        public Cls_Ent_Coordinador UpdateRecuperarClave(Cls_Ent_Coordinador entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPD_CAMBIO_CLAVE";
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("P_ID_COORDINADOR", entidad.ID_COORDINADOR);
                param[1] = new OracleParameter("P_CONTRASENA", entidad.CONTRASENA);
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
