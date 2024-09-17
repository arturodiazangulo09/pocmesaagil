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
    public class Cls_Dat_Entidades : DataBaseHelper
    {
        public List<Cls_Ent_Entidades> ListaEntidades(Cls_Ent_Entidades entidad)
        {
            List<Cls_Ent_Entidades> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_ENTIDADES";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ESTADO", entidad.FLG_ESTADO);
                    p.Add("P_DESC_ENTIDAD", entidad.DESCRIPCION);
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
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
        public List<Cls_Datos_Mef> ListaGenerales()
        {
            List<Cls_Datos_Mef> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_GENERALES";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Datos_Mef>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }

        
        public List<Cls_Ent_Entidades> ListaEntidadesEvaluador(Cls_Ent_Entidades entidad)
        {
            List<Cls_Ent_Entidades> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_ENTIDADES_EVALUADOR";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_USUARIO_EVALUADOR", entidad.EVALUADOR);
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
        public List<Cls_Ent_Entidades> ListaEntidadesConsultor(Cls_Ent_Entidades entidad)
        {
            List<Cls_Ent_Entidades> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_ENTIDADES_CONSULTOR";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_USUARIO_CONSULTOR", entidad.USU_CONSULTOR);
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
        public Cls_Ent_Entidades MantenimientoEntidades(Cls_Ent_Entidades entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_MNT_ENTIDAD";
            try
            {          
                OracleParameter[] param = new OracleParameter[7];
                param[0] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[1] = new OracleParameter("P_DESC_ENTIDAD", entidad.DESC_ENTIDAD);
                param[2] = new OracleParameter("P_DESC_UNIDAD", entidad.DESC_UNIDAD);
                param[3] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[4] = new OracleParameter("P_IP", entidad.IP_INGRESO);
                param[5] = new OracleParameter("P_TIPO", entidad.ACCION);
                param[6] = new OracleParameter("P_ACRONIMO", entidad.ACRONIMO);
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
        public Cls_Datos_Mef MantenimientoGenerales(Cls_Datos_Mef entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_MNT_GENERALES";
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("P_ID_GENERAL", entidad.ID_GENERAL);
                param[1] = new OracleParameter("P_ANIO", entidad.ANIO);
                param[2] = new OracleParameter("P_PIA", entidad.PIA);
                param[3] = new OracleParameter("P_PIM", entidad.PIM);
                param[4] = new OracleParameter("P_TIPO", entidad.ACCION);


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

        
        public Cls_Ent_Entidades MantenimientoPeriodoEntidad(Cls_Ent_Entidades entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_MNT_PERIODO_ENTIDAD";
            try
            {
                OracleParameter[] param = new OracleParameter[13];
                //OracleParameter blobParameter = new OracleParameter();
                //blobParameter.OracleDbType = OracleDbType.Blob;
                //blobParameter.ParameterName = "P_ARCHIVO";
                //blobParameter.Value = entidad.ARCHIVO;
              
                param[0] = new OracleParameter("P_ID_PERIODO", entidad.ID_PERIODO);
                param[1] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[2] = new OracleParameter("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                param[3] = new OracleParameter("P_ANIO_PERIODO", entidad.ANIO_PERIODO);
                param[4] = new OracleParameter("P_FEC_PERIODO_INI", entidad.FEC_PERIODO_INI.ToString("dd/MM/yyyy"));
                param[5] = new OracleParameter("P_FEC_PERIODO_FIN", entidad.FEC_PERIODO_FIN.ToString("dd/MM/yyyy"));
                param[6] = new OracleParameter("P_MONTO_MENSUAL", entidad.MONTO_MENSUAL);          
                param[7] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[8] = new OracleParameter("P_IP", entidad.IP_INGRESO);
                param[9] = new OracleParameter("P_TIPO", entidad.ACCION);
                param[10] = new OracleParameter("P_ARCHIVO", entidad.ARCHIVO);
                param[11] = new OracleParameter("P_NOMBRE", entidad.NOMBRE_ARCHIVO);
                param[12] = new OracleParameter("PO_ID_PERIODO", OracleDbType.Int32, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.ID_PERIODO = int.Parse(param[12].Value.ToString());
                Mantenimiento_D_PeriodoEntidad(entidad);
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
        public Cls_Ent_Entidades Mantenimiento_D_PeriodoEntidad(Cls_Ent_Entidades entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_MNT_D_PERIODO_ENTIDAD";
            OracleConnection cn = new OracleConnection(this.cnSTR);
            OracleTransaction tx = null;
            try
            {
                cn.Open();
                tx = cn.BeginTransaction();
                int ini = Convert.ToInt32(entidad.FEC_PERIODO_INI.Month);
                int fini = Convert.ToInt32(entidad.FEC_PERIODO_FIN.Month);
                for (int i = ini; i <= fini; i++)
                {
                    OracleParameter[] param = new OracleParameter[7];
                    param[0] = new OracleParameter("P_ID_PERIODO", entidad.ID_PERIODO);
                    param[1] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    param[2] = new OracleParameter("P_NUM_MES", i);
                    param[3] = new OracleParameter("P_DES_MES", FuncionUtil.Mi_Mes(i)) ;
                    param[4] = new OracleParameter("P_MONTO_MENSUAL", entidad.MONTO_MENSUAL);
                    param[5] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                    param[6] = new OracleParameter("P_IP", entidad.IP_INGRESO);
                    OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);

                }
                tx.Commit(); 
                entidad.FLG_OK = true;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Log.MensajeLog(ex.Message, sp);
                entidad.DES_ERROR = ex.Message;
                entidad.FLG_OK = false;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            return entidad;
        }
        public List<Cls_Ent_Entidades> ListaPeriodoEntidades()
        {
            List<Cls_Ent_Entidades> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_PERIODO_ENTIDAD";
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
        public List<Cls_Ent_Entidades> ListaPeriodoDetalleEntidades()
        {
            List<Cls_Ent_Entidades> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_DET_PERIODO_ENTIDAD";
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
        public Cls_Ent_Entidades UpdateMensualPeriodoEntidad(Cls_Ent_Entidades entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_UPD_D_PERIODO_MES";
            try
            {
                OracleParameter[] param = new OracleParameter[8];
                //OracleParameter blobParameter = new OracleParameter();
                //blobParameter.OracleDbType = OracleDbType.Blob;
                //blobParameter.ParameterName = "P_ARCHIVO";
                //blobParameter.Value = entidad.ARCHIVO;
                param[0] = new OracleParameter("P_ID_PERIODO", entidad.ID_PERIODO);
                param[1] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[2] = new OracleParameter("P_NUM_MES", entidad.NUM_MES);
                param[3] = new OracleParameter("P_MONTO_MENSUAL", entidad.MONTO_MENSUAL);
                param[4] = new OracleParameter("P_ARCHIVO", entidad.ARCHIVO);
                param[5] = new OracleParameter("P_NOMBRE", entidad.NOMBRE_ARCHIVO);
                param[6] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[7] = new OracleParameter("P_IP", entidad.IP_INGRESO);
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
        public Cls_Ent_Entidades UpdateDatosEntidad(Cls_Ent_Entidades entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_UPD_DATOS_ENTIDAD";
            try
            {
                OracleParameter[] param = new OracleParameter[13];
                param[0] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[1] = new OracleParameter("P_RUC", entidad.RUC);
                param[2] = new OracleParameter("P_DIRECCION", entidad.DIRECCION);
                param[3] = new OracleParameter("P_COD_DEPA", entidad.COD_DEPA);
                param[4] = new OracleParameter("P_COD_PROV", entidad.COD_PROV);
                param[5] = new OracleParameter("P_COD_DIST", entidad.COD_DIST);
                param[6] = new OracleParameter("P_REPRESENTANTE", entidad.REPRESENTANTE);
                param[7] = new OracleParameter("P_TIPO_RESOLUCION", entidad.TIPO_RESOLUCION);
                param[8] = new OracleParameter("P_NUMERO_RESOLUCION", entidad.NUMERO_RESOLUCION);
                param[9] = new OracleParameter("P_CARGO_AUTORIDAD", entidad.CARGO_AUTORIDAD);
                param[10] = new OracleParameter("P_DNI_AUTORIDAD", entidad.DNI_AUTORIDAD);
                param[11] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[12] = new OracleParameter("P_IP", entidad.IP_INGRESO);
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
        public Cls_Datos_Mef UpdateDatosMef(Cls_Datos_Mef entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_UPD_DATOS_MEF";
            try
            {
                OracleParameter[] param = new OracleParameter[10];
                param[0] = new OracleParameter("P_RUC", entidad.RUC);
                param[1] = new OracleParameter("P_DIRECCION", entidad.DIRECCION);
                param[2] = new OracleParameter("P_DNI_AUTORIDAD", entidad.DNI_AUTORIDAD);
                param[3] = new OracleParameter("P_APE_PATERNO", entidad.APE_PATERNO);
                param[4] = new OracleParameter("P_APE_MATERNO", entidad.APE_MATERNO);
                param[5] = new OracleParameter("P_NOMBRES", entidad.NOMBRES);
                param[6] = new OracleParameter("P_TIPO_RESOLUCION", entidad.TIPO_RESOLUCION);
                param[7] = new OracleParameter("P_NUMERO_RESOLUCION", entidad.NUMERO_RESOLUCION);
                param[8] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[9] = new OracleParameter("P_IP", entidad.IP_PC);


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
        public List<Cls_Datos_Mef> ListaDatosMef()
        {
            List<Cls_Datos_Mef> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_DATOS_MEF";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Datos_Mef>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Evaluador MantenimientoEvaluador(Cls_Ent_Evaluador entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_MNT_EVALUADOR";
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("P_ID_EVALUADOR", entidad.ID_EVALUADOR);
                param[1] = new OracleParameter("P_DESC_EVALUADOR", entidad.DESC_EVALUADOR);
                param[2] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[3] = new OracleParameter("P_IP", entidad.IP_INGRESO);
                param[4] = new OracleParameter("P_TIPO", entidad.ACCION);
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
        public List<Cls_Ent_Evaluador> ListaEvaluador(Cls_Ent_Evaluador entidad)
        {
            List<Cls_Ent_Evaluador> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_EVALUADOR";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_DESC_EVALUADOR", entidad.DESC_EVALUADOR);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Evaluador>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Entidades UDP_EvaluadorEntidad(Cls_Ent_Entidades entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_EVALUADOR_ENTIDAD";
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[1] = new OracleParameter("P_DESC_EVALUADOR", entidad.EVALUADOR);
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
        ////////PDT INICIO
        public List<Cls_Ent_Planilla_PDT> ListaPlanillaPDT(Cls_Ent_Planilla_PDT entidad)
        {
            List<Cls_Ent_Planilla_PDT> lista = null;
            string sp = "FAGPAC.PACK_FORMATOS_FAGPAC.USP_LISTA_PLANILLA_PDT";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("P_ANIO", entidad.ANIO);
                    p.Add("P_NUM_MES", entidad.MES);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Planilla_PDT>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Planilla_PDT> ListaPlanillaGeneral(Cls_Ent_Planilla_PDT entidad)
        {
            List<Cls_Ent_Planilla_PDT> lista = null;
            string sp = "FAGPAC.PACK_FORMATOS_FAGPAC.USP_LISTA_PLANILLA_GENERAL";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_PERIODO_PLANILLA", entidad.PERIODO_PLANILLA);
                    p.Add("P_ANIO_PLANILLA", entidad.ANIO_PLANILLA);
                    p.Add("P_CODIGO_PLANILLA", entidad.CODIGO_PLANILLA);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Planilla_PDT>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        ///       ////////PDT FIN
        ///////////CONSULTORES INICIO
        //////////CONSULTORES FIN
    }
}
