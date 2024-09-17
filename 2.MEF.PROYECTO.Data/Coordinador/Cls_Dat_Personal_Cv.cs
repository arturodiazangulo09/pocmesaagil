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
using MEF.PROYECTO.Entity.Personal;
namespace MEF.PROYECTO.Data.Coordinador
{
    public class Cls_Dat_Personal_Cv: DataBaseHelper
    {
        public List<Cls_Ent_Estudios> ListaEstudios(Cls_Ent_Estudios entidad)
        {
            List<Cls_Ent_Estudios> lista = null;
            string sp = "FAGPAC.PACK_FORMATOS_FAGPAC.USP_LISTA_FORMACION_ACADEMICA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_PERSONAL", entidad.ID_PERSONAL);
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Estudios>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Estudios MentenimientoEstudios_Verificar(Cls_Ent_Estudios entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_ACADEMICA_VERIFICA_UTP";
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("P_ID_FORMAC_ACADEMICA", entidad.ID_FORMAC_ACADEMICA);
                param[1] = new OracleParameter("P_TIPO", entidad.APLICA);
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
        public List<Cls_Ent_Especializacion> ListaEspecializacion(Cls_Ent_Especializacion entidad)
        {
            List<Cls_Ent_Especializacion> lista = null;
            string sp = "FAGPAC.PACK_FORMATOS_FAGPAC.USP_LISTA_ESPECIALIZACION";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_PERSONAL", entidad.ID_PERSONAL);
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Especializacion>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Especializacion MentenimientoEstudios_Especializacion(Cls_Ent_Especializacion entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_ESPECIA_VERIFICA_UTP";
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("P_ID_ESPECIALIZACION", entidad.ID_ESPECIALIZACION);
                param[1] = new OracleParameter("P_TIPO", entidad.APLICA);
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
        public List<Cls_Ent_Capacitacion> ListaCapacitacion(Cls_Ent_Capacitacion entidad)
        {
            List<Cls_Ent_Capacitacion> lista = null;
            string sp = "FAGPAC.PACK_FORMATOS_FAGPAC.USP_LISTA_CAPACITACION";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_PERSONAL", entidad.ID_PERSONAL);
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Capacitacion>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Capacitacion MentenimientoEstudios_Capacitacion(Cls_Ent_Capacitacion entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_CAPACITA_VERIFICA_UTP";
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("P_ID_CAPACITACION", entidad.ID_CAPACITACION);
                param[1] = new OracleParameter("P_TIPO", entidad.APLICA);
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
        public List<Cls_Ent_Experiencia_Laboral> ListaExperiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            List<Cls_Ent_Experiencia_Laboral> lista = null;
            string sp = "FAGPAC.PACK_FORMATOS_FAGPAC.USP_LISTA_EXPERIENCIA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_PERSONAL", entidad.ID_PERSONAL);
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Experiencia_Laboral>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Experiencia_Laboral MentenimientoEstudios_Experiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_EXPERIENCIA_VERIFICA_UTP";
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("P_ID_EXPERIENCIA", entidad.ID_EXPERIENCIA);
                param[1] = new OracleParameter("P_TIPO", entidad.APLICA);
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
        public Cls_Ent_Solicitud UpdAplicaRegistoCv(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPD_APLICA_PERSONAL";
            try
            {
                OracleParameter[] param = new OracleParameter[7];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
                param[2] = new OracleParameter("P_ID_APLICA", entidad.ID);
                param[3] = new OracleParameter("P_FLG_ESTADO", entidad.FLG_ESTADO);
                param[4] = new OracleParameter("P_TIPO_ACCION", entidad.ACCION);
                param[5] = new OracleParameter("P_USU_MODIFICA", entidad.USU_INGRESO);
                param[6] = new OracleParameter("P_IP_MODIFICA", entidad.IP_PC);
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
        public Cls_Ent_Solicitud UpdCalificacionRequisitos(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPD_EVALUACION_REQUISITOS";
            try
            {
                OracleParameter[] param = new OracleParameter[4];

                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_FLG_CUMPLE_REQUISITOS", entidad.FLG_CUMPLE_REQUISITOS);
                param[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_PC);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                if (entidad.listaRequisitos != null)
                {
                    foreach (var item in entidad.listaRequisitos)
                    {
                        string sp_3 = "FAGPAC.PACK_EXTRANET_COORDINADOR.UPD_REQUISITO_CALIFICACION";
                        var paramC = new OracleParameter[8];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
                        paramC[2] = new OracleParameter("P_ID_PERFIL", item.ID_PERFIL);
                        paramC[3] = new OracleParameter("P_ID_REQUISITO", item.ID_REQUISITO);
                        paramC[4] = new OracleParameter("P_PERFIL", item.PERFIL);
                        paramC[5] = new OracleParameter("P_DETALLE", item.DETALLE);
                        paramC[6] = new OracleParameter("P_CALIFICACION", item.FLG_VALIDADO);
                        paramC[7] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_3, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_3);
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
    }
}
