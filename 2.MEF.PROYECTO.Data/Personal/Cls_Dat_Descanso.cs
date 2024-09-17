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
   public  class Cls_Dat_Descanso: DataBaseHelper
    {
        public Cls_Ent_Descanso MentenimientoSolicitud_Suspension(Cls_Ent_Descanso entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_MNT_SOLICITUD_SUSPENSION";
            try
            {
                OracleParameter[] param = new OracleParameter[13];
                param[0] = new OracleParameter("P_ID_SUSPENSION", entidad.ID_SUSPENSION);
                param[1] = new OracleParameter("P_ID_CONTRATO", entidad.ID_CONTRATO);
                param[2] = new OracleParameter("P_FECHA_INICIO", entidad.FECHA_INICIO.ToString("dd/MM/yyyy"));
                param[3] = new OracleParameter("P_FECHA_FIN", entidad.FECHA_FIN.ToString("dd/MM/yyyy"));

                param[4] = new OracleParameter("P_FECHA_INICIO_PERIODO", entidad.FECHA_PERIODO_INICIO.ToString("dd/MM/yyyy"));
                param[5] = new OracleParameter("P_FECHA_FIN_PERIODO", entidad.FECHA_PERIODO_FIN.ToString("dd/MM/yyyy"));

                param[6] = new OracleParameter("P_DIAS_LIBRE", entidad.DIAS_LIBRE);
                param[7] = new OracleParameter("P_FLG_DESCUENTO", entidad.FLG_DESCUENTO); ;
                param[8] = new OracleParameter("P_DIAS_DESCUENTO", entidad.DIAS_DESCUENTO);
                param[9] = new OracleParameter("P_ID_ARCHIVO_U", entidad.ID_ARCHIVO_U);
                param[10] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[11] = new OracleParameter("P_IP", entidad.IP_PC);
                param[12] = new OracleParameter("P_TIPO", entidad.ACCION);
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
        public List<Cls_Ent_Descanso> ListaDetalle_contrato(Cls_Ent_Descanso entidad)
        {
            List<Cls_Ent_Descanso> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_DETALLE_CONTRATO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CONTRATO", entidad.ID_CONTRATO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Descanso>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Descanso> ListaSolicitud_Suspension(Cls_Ent_Descanso entidad)
        {
            List<Cls_Ent_Descanso> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_SUSPENSION";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_CONTRATO", entidad.ID_CONTRATO);
                    p.Add("P_ID_SUSPENSION", entidad.ID_SUSPENSION);

                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("P_ANIO", entidad.ANIO);
                    p.Add("P_NUM_CONTRATO", entidad.NUM_CONTRATO);
                    p.Add("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Descanso>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Descanso UpdEstado_Suspension(Cls_Ent_Descanso entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_UPD_SUSPENSION_ESTADO";
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("P_ID_SUSPENSION", entidad.ID_SUSPENSION);
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
