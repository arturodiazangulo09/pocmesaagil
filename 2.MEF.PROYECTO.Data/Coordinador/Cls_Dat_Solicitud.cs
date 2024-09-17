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
using System.Web.Mvc;

namespace MEF.PROYECTO.Data.Coordinador
{
    public class Cls_Dat_Solicitud: DataBaseHelper
    {
        public Cls_Ent_Solicitud MantenimientoSolicitudPac(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_SOLICITUD_PAC";
            try
            {
                OracleParameter[] param = new OracleParameter[25];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[2] = new OracleParameter("P_DEPENDENCIA", entidad.DEPENDENCIA);
                param[3] = new OracleParameter("P_FECHA_INICIO", entidad.FECHA_INICIO.ToString("dd/MM/yyyy"));
                param[4] = new OracleParameter("P_FECHA_FIN", entidad.FECHA_FIN.ToString("dd/MM/yyyy"));
                param[5] = new OracleParameter("P_MONTO_MENSUAL", entidad.MONTO_MENSUAL);
                param[6] = new OracleParameter("P_ID_PUESTO", entidad.ID_PUESTO);
                param[7] = new OracleParameter("P_CONFORMIDAD_SERVICIO", entidad.CONFORMIDAD_SERVICIO);
                param[8] = new OracleParameter("P_APE_NOMB_CERTIFICA", entidad.APE_NOMB_CERTIFICA);
                param[9] = new OracleParameter("P_NUM_DOCUMENTO_CONSULTOR", entidad.NUM_DOCUMENTO_CONSULTOR);
                param[10] = new OracleParameter("P_APELLIDO_PAT_CONSULTOR", entidad.APELLIDO_PAT_CONSULTOR);
                param[11] = new OracleParameter("P_APELLIDO_MAT_CONSULTOR", entidad.APELLIDO_MAT_CONSULTOR);
                param[12] = new OracleParameter("P_NOMBRES_CONSULTOR", entidad.NOMBRES_CONSULTOR);
                param[13] = new OracleParameter("P_CORREO_CONSULTOR", entidad.CORREO_CONSULTOR);
                param[14] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[15] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                param[16] = new OracleParameter("P_TIPO", entidad.FLG_PROCESO);
                param[17] = new OracleParameter("P_FLG_DESIGNADO", entidad.FLG_DESIGNADO);
                param[18] = new OracleParameter("P_NR_RESOLUCION", entidad.NR_RESOLUCION);
                param[19] = new OracleParameter("P_FECHA_RESOLUCION", entidad.FECHA_RESOLUCION.ToString("dd/MM/yyyy"));
                param[20] = new OracleParameter("P_OFICINA_CERTIFICA", entidad.OFICINA_CERTIFICA);
                param[21] = new OracleParameter("P_NOMBRE_ARCHIVO_SUS_SOLICITUD", entidad.NOMBRE_ARCHIVO_SUS_SOLICITUD);
                param[22] = new OracleParameter("P_ARCHIVO_PUESTO_SUS_SOLICITUD", entidad.ARCHIVO_PUESTO_SUS_SOLICITUD);//BLOB_ARCHIVO;
                param[23] = new OracleParameter("P_DESC_ORGANO", entidad.DESC_ORGANO);//BLOB_ARCHIVO;
                param[24] = new OracleParameter("PO_ID_SOLICITUD", OracleDbType.Int32, ParameterDirection.Output);     
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.ID_SOLICITUD = int.Parse(param[24].Value.ToString());
                if (entidad.listaFunciones!=null)
                {
                    foreach (var item in entidad.listaFunciones)
                    {
                        string sp_2 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_SERVICIO";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_FUNCIONES", item.DESC_FUNCIONES);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
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
                }
            
                if (entidad.listaExperiencia !=null)
                {
                    foreach (var item in entidad.listaExperiencia)
                    {
                        string sp_3 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_EXPERIENCIA";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_EXPERIENCIA", item.DESC_EXPERIENCIA);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
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
                if (entidad.listaCurso!=null)
                {
                    foreach (var item in entidad.listaCurso)
                    {
                        string sp_4 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_CURSOS";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_CURSO_PRO", item.DESC_CURSO_PRO);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_4, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_4);
                            entidad.DES_ERROR = ex.Message;
                            entidad.FLG_OK = false;
                        }
                    }
                }
                if (entidad.listaAcademico!=null)
                {
                    foreach (var item in entidad.listaAcademico)
                    {
                        string sp_5 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_ACADEMICA";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_ACADEMICA", item.DESC_ACADEMICA);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_5, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_5);
                            entidad.DES_ERROR = ex.Message;
                            entidad.FLG_OK = false;
                        }
                    }
                }
                if (entidad.listaConocimiento!=null)
                {
                    foreach (var item in entidad.listaConocimiento)
                    {
                        string sp_6 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_CONOCIMIENTO";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_CONOCIMIENTO", item.DESC_CONOCIMIENTO);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_6, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_6);
                            entidad.DES_ERROR = ex.Message;
                            entidad.FLG_OK = false;
                        }
                    }
                }
                if (entidad.listaActividad!=null)
                {
                    foreach (var item in entidad.listaActividad)
                    {
                        string sp_7 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_ACTIVIDAD";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_ACTIVIDAD", item.DESC_ACTIVIDAD);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_7, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_7);
                            entidad.DES_ERROR = ex.Message;
                            entidad.FLG_OK = false;
                        }
                    }
                }
                if (entidad.listaAcividadOtro!=null)
                {
                    foreach (var item in entidad.listaAcividadOtro)
                    {
                        string sp_8 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_OTR_ACT";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_ACT_OTRO", item.DESC_ACT_OTRO);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_8, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_8);
                            entidad.DES_ERROR = ex.Message;
                            entidad.FLG_OK = false;
                        }
                    }
                }
                if (entidad.listaRenumeracion != null)
                {
                    foreach (var item in entidad.listaRenumeracion)
                    {
                        string sp_9 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_PERIODO_PAGO";
                        var paramC = new OracleParameter[9];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_ID_ENTIDAD", item.ID_ENTIDAD);
                        paramC[2] = new OracleParameter("P_ANIO", item.ANIO);
                        paramC[3] = new OracleParameter("P_NUM_MES", item.ID_MES);
                        paramC[4] = new OracleParameter("P_DES_MES", item.DES_MES);
                        paramC[5] = new OracleParameter("P_MONTO", item.MONTO);
                        paramC[6] = new OracleParameter("P_TIPO_PROCESO", item.TIPO_PROCESO);
                        paramC[7] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[8] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
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

        public List<Cls_Ent_Solicitud> ListaSolicitudes(Cls_Ent_Solicitud entidad)
        {
            List<Cls_Ent_Solicitud> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_SOLICITUD";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("P_NR_SOLICITUD", entidad.NUM_PROCESO);
                    p.Add("P_DENOMINACION_PUESTO", entidad.DENOMINACION_PUESTO);
                    p.Add("P_FECHA_SOLICITUD", entidad.FECHA_REGISTRO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Solicitud>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Funciones_Solicitud> ListaServicio(Cls_Ent_Funciones_Solicitud entidad)
        {
            List<Cls_Ent_Funciones_Solicitud> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_SERVICIO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Funciones_Solicitud>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Experiencia> ListaExperiencia(Cls_Ent_Experiencia entidad)
        {
            List<Cls_Ent_Experiencia> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_EXPERIENCIA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Experiencia>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Academico> ListaAcademica(Cls_Ent_Academico entidad)
        {
            List<Cls_Ent_Academico> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_ACADEMICA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Academico>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Curso> ListaCursos(Cls_Ent_Curso entidad)
        {
            List<Cls_Ent_Curso> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_CURSOS";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Curso>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Conocimientos> ListaConocimientos(Cls_Ent_Conocimientos entidad)
        {
            List<Cls_Ent_Conocimientos> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_CONOCIMIENTOS";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Conocimientos>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Actividad> ListaActividad(Cls_Ent_Actividad entidad)
        {
            List<Cls_Ent_Actividad> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_ACTIVIDAD";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Actividad>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Actividad_Otro> ListaActividadOtro(Cls_Ent_Actividad_Otro entidad)
        {
            List<Cls_Ent_Actividad_Otro> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_OTRO_ACT";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Actividad_Otro>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Solicitud MantenimientoSolicitudFag(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_SOLICITUD_FAG";
            try
            {
                OracleParameter[] param = new OracleParameter[28];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[2] = new OracleParameter("P_DEPENDENCIA", entidad.DEPENDENCIA);
                param[3] = new OracleParameter("P_FECHA_INICIO", entidad.FECHA_INICIO.ToString("dd/MM/yyyy"));
                param[4] = new OracleParameter("P_FECHA_FIN", entidad.FECHA_FIN.ToString("dd/MM/yyyy"));
                param[5] = new OracleParameter("P_MONTO_MENSUAL", entidad.MONTO_MENSUAL);
                param[6] = new OracleParameter("P_DENOMINACION_PUESTO", entidad.DENOMINACION_PUESTO);
                param[7] = new OracleParameter("P_CONFORMIDAD_SERVICIO", entidad.CONFORMIDAD_SERVICIO);
                param[8] = new OracleParameter("P_APE_NOMB_CERTIFICA", entidad.APE_NOMB_CERTIFICA);
                param[9] = new OracleParameter("P_NUM_DOCUMENTO_CONSULTOR", entidad.NUM_DOCUMENTO_CONSULTOR);
                param[10] = new OracleParameter("P_APELLIDO_PAT_CONSULTOR", entidad.APELLIDO_PAT_CONSULTOR);
                param[11] = new OracleParameter("P_APELLIDO_MAT_CONSULTOR", entidad.APELLIDO_MAT_CONSULTOR);
                param[12] = new OracleParameter("P_NOMBRES_CONSULTOR", entidad.NOMBRES_CONSULTOR);
                param[13] = new OracleParameter("P_CORREO_CONSULTOR", entidad.CORREO_CONSULTOR);
                param[14] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[15] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                param[16] = new OracleParameter("P_TIPO", entidad.FLG_PROCESO);
                param[17] = new OracleParameter("P_OFICINA_CERTIFICA", entidad.OFICINA_CERTIFICA);
                param[18] = new OracleParameter("P_FLG_DESIGNADO", entidad.FLG_DESIGNADO);
                param[19] = new OracleParameter("P_NR_RESOLUCION", entidad.NR_RESOLUCION);
                param[20] = new OracleParameter("P_FECHA_RESOLUCION", entidad.FECHA_RESOLUCION.ToString("dd/MM/yyyy"));    
                param[21] = new OracleParameter("P_NOMBRE_ARCHIVO_SUS_SOLICITUD", entidad.NOMBRE_ARCHIVO_SUS_SOLICITUD);
                param[22] = new OracleParameter("P_ARCHIVO_PUESTO_SUS_SOLICITUD", entidad.ARCHIVO_PUESTO_SUS_SOLICITUD); 
                param[23] = new OracleParameter("P_DESC_ORGANO", entidad.DESC_ORGANO); 
                param[24] = new OracleParameter("P_FLG_VERSION", entidad.FLG_VERSION);
                param[25] = new OracleParameter("P_FLG_NIVEL", "0");
                param[26] = new OracleParameter("P_FLG_CEU", "0");
                param[27] = new OracleParameter("PO_ID_SOLICITUD", OracleDbType.Int32, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.ID_SOLICITUD = int.Parse(param[27].Value.ToString());
                if (entidad.listaExperiencia != null)
                {
                    foreach (var item in entidad.listaExperiencia)
                    {
                        string sp_3 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_EXPERIENCIA";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_EXPERIENCIA", item.DESC_EXPERIENCIA);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
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
                if (entidad.listaCurso != null)
                {
                    foreach (var item in entidad.listaCurso)
                    {
                        string sp_4 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_CURSOS";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_CURSO_PRO", item.DESC_CURSO_PRO);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_4, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_4);
                            entidad.DES_ERROR = ex.Message;
                            entidad.FLG_OK = false;
                        }
                    }
                }
                if (entidad.listaAcademico != null)
                {
                    foreach (var item in entidad.listaAcademico)
                    {
                        string sp_5 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_ACADEMICA";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_ACADEMICA", item.DESC_ACADEMICA);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_5, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_5);
                            entidad.DES_ERROR = ex.Message;
                            entidad.FLG_OK = false;
                        }
                    }
                }
                if (entidad.listaConocimiento != null)
                {
                    foreach (var item in entidad.listaConocimiento)
                    {
                        string sp_6 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_CONOCIMIENTO";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_CONOCIMIENTO", item.DESC_CONOCIMIENTO);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_6, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_6);
                            entidad.DES_ERROR = ex.Message;
                            entidad.FLG_OK = false;
                        }
                    }
                }
                if (entidad.listaActividad != null)
                {
                    foreach (var item in entidad.listaActividad)
                    {
                        string sp_7 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_ACTIVIDAD";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_ACTIVIDAD", item.DESC_ACTIVIDAD);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_7, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_7);
                            entidad.DES_ERROR = ex.Message;
                            entidad.FLG_OK = false;
                        }
                    }
                }
                if (entidad.listaAcividadOtro != null)
                {
                    foreach (var item in entidad.listaAcividadOtro)
                    {
                        string sp_8 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_OTR_ACT";
                        var paramC = new OracleParameter[4];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_DESC_ACT_OTRO", item.DESC_ACT_OTRO);
                        paramC[2] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[3] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_8, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp_8);
                            entidad.DES_ERROR = ex.Message;
                            entidad.FLG_OK = false;
                        }
                    }
                }
                if (entidad.listaRenumeracion != null)
                {
                    foreach (var item in entidad.listaRenumeracion)
                    {
                        string sp_9 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_PERIODO_PAGO";
                        var paramC = new OracleParameter[9];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_ID_ENTIDAD", item.ID_ENTIDAD);
                        paramC[2] = new OracleParameter("P_ANIO", item.ANIO);
                        paramC[3] = new OracleParameter("P_NUM_MES", item.ID_MES);
                        paramC[4] = new OracleParameter("P_DES_MES", item.DES_MES);
                        paramC[5] = new OracleParameter("P_MONTO", item.MONTO);
                        paramC[6] = new OracleParameter("P_TIPO_PROCESO", item.TIPO_PROCESO);
                        paramC[7] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[8] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
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
        public Cls_Ent_Documento MantenimientoDOcumento(Cls_Ent_Documento entidad)
        {
            string sp_10 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_DOCUMENTO";
            var paramC = new OracleParameter[8];
            paramC[0] = new OracleParameter("P_ID_DOCUMENTO", entidad.ID_DOCUMENTO);
            paramC[1] = new OracleParameter("P_ID_PROCESO", entidad.ID_PROCESO);
            paramC[2] = new OracleParameter("P_FLG_TIPO", entidad.FLG_TIPO);
            paramC[3] = new OracleParameter("P_DES_DOCUMENTO", entidad.DES_DOCUMENTO);
            paramC[4] = new OracleParameter("P_ID_LF", entidad.ID_LF);
            paramC[5] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
            paramC[6] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
            paramC[7] = new OracleParameter("P_TIPO", entidad.ACCION == "T" ? "T" : entidad.ID_DOCUMENTO == 0 ? "I" : "U") ;
            try
            {
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp_10, paramC);
                entidad.FLG_OK = true;
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp_10);
                entidad.DES_ERROR = ex.Message;
                entidad.FLG_OK = false;
            }
            return entidad;
        }
        public List<Cls_Ent_Documento> ListaDocumentos(Cls_Ent_Documento entidad)
        {
            List<Cls_Ent_Documento> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_DOCUMENTO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_PROCESO", entidad.ID_PROCESO);
                    p.Add("P_FLG_TIPO", entidad.FLG_TIPO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Documento>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Renumeracion> ListaPeriodoPagoSolicitud(Cls_Ent_Renumeracion entidad)
        {
            List<Cls_Ent_Renumeracion> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_PERIODO_PAGO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Renumeracion>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        /*VALIDAR PERIODO DE PAGOS*/
        public Cls_Ent_Renumeracion ValidarRemuneracion(Cls_Ent_Renumeracion entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_VALIDAR_PERIODO_ENTIDAD";
            try
            {
                var paramC = new OracleParameter[9];
                paramC[0] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                paramC[1] = new OracleParameter("P_PROCESO", entidad.TIPO_PROCESO);
                paramC[2] = new OracleParameter("P_ANIO", entidad.ANIO);
                paramC[3] = new OracleParameter("P_MES", entidad.ID_MES);
                paramC[4] = new OracleParameter("P_DES_MES", entidad.DES_MES);
                paramC[5] = new OracleParameter("P_MONTO_MENSUAL", entidad.MONTO);
                paramC[6] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                paramC[7] = new OracleParameter("PO_VALIDO", OracleDbType.Int32, ParameterDirection.Output);
                paramC[8] = new OracleParameter("PO_MENSAJE", OracleDbType.Varchar2, 2000, OracleCollectionType.None, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, paramC);
                entidad.PO_VALIDO = paramC[7].Value.ToString();
                entidad.PO_MENSAJE = paramC[8].Value.ToString();
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
        /*FIN PERIODO DE PAGOS*/
        public Cls_Ent_Solicitud Enviar_Solicitud_Participante(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_PERSONAL_SOLICITUD";
            try
            {
                OracleParameter[] param = new OracleParameter[7];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_DENOMINACION_PUESTO", entidad.DENOMINACION_PUESTO);
                param[2] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
                param[3] = new OracleParameter("P_ENTIDAD", entidad.DESC_ENTIDAD);
                param[4] = new OracleParameter("P_CONTRASENA", entidad.CONTRASENA);               
                param[5] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[6] = new OracleParameter("P_IP", entidad.IP_PC);
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
        public Cls_Ent_Solicitud UpdateArchivoTdr(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPD_TDR_SOLICITUD";
            try
            {
                var paramC = new OracleParameter[5];
                paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                paramC[1] = new OracleParameter("P_NOMBRE_ARCHIVO_TDR", entidad.NOMBRE_ARCHIVO_TDR);
                paramC[2] = new OracleParameter("P_NOMBRE_ARCHIVO_TDR", entidad.ARCHIVO_TDR);//BLOB_ARCHIVO;
                paramC[3] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                paramC[4] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, paramC);
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
        public List<Cls_Ent_Requisitos_Solicitud> ListaRequisitos(int ID_SOLICITUD)
        {
            List<Cls_Ent_Requisitos_Solicitud> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_REQUISITOS_SOLICITUD";
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("P_ID_SOLICITUD", ID_SOLICITUD);
                param[1] = new OracleParameter("PO_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                int idRequisito = 1;
                using (OracleDataReader reader = OracleHelper.ExecuteReader(this.cnSTR, CommandType.StoredProcedure, sp, param))
                {
                    if (reader.HasRows)
                    {
                        lista = new List<Cls_Ent_Requisitos_Solicitud>();
                        while (reader.Read())
                        {
                            Cls_Ent_Requisitos_Solicitud entidad = new Cls_Ent_Requisitos_Solicitud();
                            entidad.ID_SOLICITUD = ID_SOLICITUD;
                            entidad.ID_PERFIL = int.Parse(reader["ID_PERFIL"].ToString());

                            entidad.PERFIL = reader["PERFIL"].ToString();
                            entidad.DETALLE = reader["DETALLE"].ToString();
                            entidad.ID_REQUISITO = idRequisito;//int.Parse(reader["ID_REQUISITO"].ToString());
                            entidad.FLG_VALIDADO= reader["FLG_VALIDADO"].ToString();
                            entidad.ListaCalificacion = new List<SelectListItem>();
                            entidad.ListaCalificacion.Insert(0, new SelectListItem() { Value = "", Text = "--Seleccione--", Selected = entidad.FLG_VALIDADO == "" });
                            entidad.ListaCalificacion.Insert(1, new SelectListItem() { Value = "1", Text = "CUMPLE", Selected = entidad.FLG_VALIDADO == "1" });
                            entidad.ListaCalificacion.Insert(2, new SelectListItem() { Value = "0", Text = "NO CUMPLE", Selected = entidad.FLG_VALIDADO == "0" });
                            lista.Add(entidad);
                            idRequisito += 1;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }
          
            return lista;
        }
        public Cls_Ent_Solicitud UpdNotificaUTP(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPD_CONTRATO_UTP";
            try
            {
                OracleParameter[] param = new OracleParameter[5];

                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
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
        public Cls_Ent_Reevaluacion InsReevaluacion(Cls_Ent_Reevaluacion entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.SP_REEVALUACION_INSERT";
            try
            {
                OracleParameter[] param = new OracleParameter[6];

                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
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
        public List<Cls_Ent_Reevaluacion> ListaReevaluacion(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_REEVALUACION";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
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
        public Cls_Ent_Solicitud UpdArchivosFAG(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPDATE_ARCHIVOS_UTP_FAG";
            try
            {
                OracleParameter[] param = new OracleParameter[12];

                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_ID_ANEXO1", entidad.ID_ANEXO1);
                param[2] = new OracleParameter("P_ID_ANEXO2", entidad.ID_ANEXO2);
                param[3] = new OracleParameter("P_ID_ANEXO3", entidad.ID_ANEXO3);
                param[4] = new OracleParameter("P_ID_ANEXO4", entidad.ID_ANEXO4);
                param[5] = new OracleParameter("P_ID_ANEXO5", entidad.ID_ANEXO5);
                param[6] = new OracleParameter("P_ID_ANEXO7", entidad.ID_ANEXO7);
                param[7] = new OracleParameter("P_ID_BANCO", entidad.ID_BANCO);
                param[8] = new OracleParameter("P_ID_H_PROFESIONAL", entidad.ID_H_PROFESIONAL);
                param[9] = new OracleParameter("P_ID_OTROS", entidad.ID_OTROS);
                param[10] = new OracleParameter("P_USU_MODIFICA", entidad.USU_INGRESO);
                param[11] = new OracleParameter("P_IP_MODIFICA", entidad.IP_PC);
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
        public Cls_Ent_Solicitud UpdArchivosPAC(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_UPDATE_ARCHIVOS_UTP_PAC";
            try
            {
                OracleParameter[] param = new OracleParameter[14];

                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_ID_OTROS", entidad.ID_OTROS);
                param[2] = new OracleParameter("P_ID_INFORME_F", entidad.ID_INFORME_F);
                param[3] = new OracleParameter("P_ID_DATOS_SECTOR", entidad.ID_DATOS_SECTOR);
                param[4] = new OracleParameter("P_ID_FORMATOA", entidad.ID_FORMATOA);
                param[5] = new OracleParameter("P_ID_FORMATOB", entidad.ID_FORMATOB);
                param[6] = new OracleParameter("P_ID_FORMATOC", entidad.ID_FORMATOC);
                param[7] = new OracleParameter("P_ID_FORMATOD", entidad.ID_FORMATOD);
                param[8] = new OracleParameter("P_ID_FORMATOE", entidad.ID_FORMATOE);
                param[9] = new OracleParameter("P_ID_FORMATOH", entidad.ID_FORMATOH);
                param[10] = new OracleParameter("P_ID_PAC_ANEXO2", entidad.ID_PAC_ANEXO2);
                param[11] = new OracleParameter("P_ID_PAC_H_PROFESIONAL", entidad.ID_PAC_H_PROFESIONAL);
                param[12] = new OracleParameter("P_USU_MODIFICA", entidad.USU_INGRESO);
                param[13] = new OracleParameter("P_IP_MODIFICA", entidad.IP_PC);
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
        /*INICIO SOLICITUD DE PAGO*/
        public List<Cls_Ent_Solicitud_Pago> ListaSolicitudPagoEntidad(Cls_Ent_Solicitud_Pago entidad)
        {
            List<Cls_Ent_Solicitud_Pago> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_PERIODO_PAGO_ENTIDAD";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                    p.Add("P_NUM_PROCESO", entidad.NUM_PROCESO);
                    p.Add("P_ANIO_PROCESO", entidad.ANIO_PROCESO);
                    p.Add("P_NUM_DOCUMENTO", entidad.NUM_DOCUMENTO);
                    p.Add("P_NUM_MES", entidad.NUM_MES);
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
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
        public Cls_Ent_Solicitud_Pago Mnt_Conformidad_Pago(Cls_Ent_Solicitud_Pago entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_CONFORMIDAD";
            try
            {
                OracleParameter[] param = new OracleParameter[15];

                param[0] = new OracleParameter("P_ID_CONFORMIDAD", entidad.ID_CONFORMIDAD);
                param[1] = new OracleParameter("P_ID_CONTRATO", entidad.ID_CONTRATO);
                param[2] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[3] = new OracleParameter("P_RENUMERACION", entidad.IMPORTE_COMPROBANTE);
                param[4] = new OracleParameter("P_NR_MES", entidad.NUM_MES);
                param[5] = new OracleParameter("P_ID_ARCHIVO", entidad.ID_ARCHIVO_CONFORMIDAD);
                param[6] = new OracleParameter("P_PORCENTAJE", entidad.PORCENTAJE);
                param[7] = new OracleParameter("P_TIPO", entidad.ACCION);
                param[8] = new OracleParameter("P_USU_MODIFICA", entidad.USU_INGRESO);
                param[9] = new OracleParameter("P_IP_MODIFICA", entidad.IP_PC);
                param[10] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[11] = new OracleParameter("P_ID_ANIO", entidad.ANIO);
                param[12] = new OracleParameter("P_CALIFICACION", entidad.NIVEL_CONFORMIDAD);
                param[13] = new OracleParameter("P_ID_ARCHIVO_INFORME", entidad.ID_ARCHIVO_INFORME);
                param[14] = new OracleParameter("P_ID_ARCHIVO_RECIBO", entidad.ID_ARCHIVO_RECIBO);
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
        public Cls_Ent_Solicitud_Pago Update_Conformidad_Pago(Cls_Ent_Solicitud_Pago entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_ESTADO_CONFORMIDAD";
            try
            {
                OracleParameter[] param = new OracleParameter[11];

                param[0] = new OracleParameter("P_ID_CONFORMIDAD", entidad.ID_CONFORMIDAD);
                param[1] = new OracleParameter("P_NR_MES", entidad.NUM_MES);
                param[2] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[3] = new OracleParameter("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
                param[4] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[5] = new OracleParameter("P_TIPO", entidad.ACCION);
                param[6] = new OracleParameter("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                param[7] = new OracleParameter("P_USU_MODIFICA", entidad.USU_INGRESO);
                param[8] = new OracleParameter("P_IP_MODIFICA", entidad.IP_PC);
                param[9] = new OracleParameter("P_NR_TRAMITE", entidad.NR_TRAMITE);
                param[10] = new OracleParameter("P_ID_TRAMITE", entidad.ID_TRAMITE);
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
        /*FIN  SOLICITUD DE PAGO*/
        public List<Cls_Ent_Archivo_STD> ListaArchivoSTD(int ID)
        {
            List<Cls_Ent_Archivo_STD> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_DOC_TRAMITE";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_SOLICITUD", ID);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Archivo_STD>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        #region wizard coordinador
        public int SolicitudContratoPendiente()
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_SEL_CONTRATO_PENDIENTE";
            var result = 0;
            try
            {
                OracleParameter[] param = new OracleParameter[1];
                param[0] = new OracleParameter("PO_ID_SOLICITUD", OracleDbType.Int32, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                result = int.Parse(param[0].Value.ToString());
            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, sp);
            }

            return result;
        }
        public Cls_Ent_Solicitud WizGrabarRequerimiento(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_WIZ_REQUERIMIENTO";
            try
            {
                OracleParameter[] param = new OracleParameter[6];
                param[0] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[1] = new OracleParameter("P_DEPENDENCIA", entidad.DEPENDENCIA);
                param[2] = new OracleParameter("P_DESC_ORGANO", entidad.DESC_ORGANO);
                param[3] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[4] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                param[5] = new OracleParameter("PO_ID_SOLICITUD", OracleDbType.Int32, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.ID_SOLICITUD = int.Parse(param[5].Value.ToString());
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
        public Cls_Ent_Solicitud WizGrabarRequisito(Cls_Ent_Solicitud entidad)
        {
            if (entidad.listaAcademico.Count != 0 && entidad.listaCurso.Count != 0 && entidad.listaExperiencia.Count != 0 && entidad.listaConocimiento.Count != 0)
            {
                foreach (var item in entidad.listaExperiencia)
                {
                    string sp_3 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_EXPERIENCIA_V2";
                    try
                    {
                        using (IDbConnection db = new OracleConnection(this.cnSTR))
                        {
                            var p = new OracleDynamicParameters();
                            p.Add("P_ID_EXPERIENCIA", item.ID_EXPERIENCIA);
                            p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                            p.Add("P_DESC_EXPERIENCIA", item.DESC_EXPERIENCIA);
                            p.Add("P_ID_TIPO_EXPERIENCIA", item.ID_TIPO_EXPERIENCIA);
                            p.Add("P_ID_TIPO_SECTOR", item.ID_TIPO_SECTOR);
                            p.Add("P_ANOS", item.ANOS);
                            p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                            p.Add("P_IP_INGRESO", entidad.IP_PC);
                            p.Add("P_TIPO", item.ID_EXPERIENCIA == 0 ? "I" : "U");
                            db.Query(sp_3, p, commandType: CommandType.StoredProcedure);
                        }
                        entidad.FLG_OK = true;
                    }
                    catch (Exception ex)
                    {
                        entidad.FLG_OK = false;
                        Log.MensajeLog(ex.Message, sp_3);
                    }
                }

                foreach (var item in entidad.listaCurso)
                {
                    string sp_4 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_CURSOS_V2";
                    try
                    {
                        using (IDbConnection db = new OracleConnection(this.cnSTR))
                        {
                            var p = new OracleDynamicParameters();
                            p.Add("P_ID_CURSOS_PRO", item.ID_CURSOS_PRO);
                            p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                            p.Add("P_DESC_CURSO_PRO", item.DESC_CURSO_PRO);
                            p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                            p.Add("P_IP_INGRESO", entidad.IP_PC);
                            p.Add("P_TIPO", item.ID_CURSOS_PRO == 0 ? "I" : "U");
                            db.Query(sp_4, p, commandType: CommandType.StoredProcedure);
                        }
                        entidad.FLG_OK = true;
                    }
                    catch (Exception ex)
                    {
                        entidad.FLG_OK = false;
                        Log.MensajeLog(ex.Message, sp_4);
                    }
                }

                foreach (var item in entidad.listaAcademico)
                {
                    string sp_5 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_ACADEMICA_V2";
                    try
                    {
                        using (IDbConnection db = new OracleConnection(this.cnSTR))
                        {
                            var p = new OracleDynamicParameters();
                            p.Add("P_ID_ACADEMICA", item.ID_ACADEMICA);
                            p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                            p.Add("P_DESC_ACADEMICA", item.DESC_ACADEMICA);
                            p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                            p.Add("P_IP_INGRESO", entidad.IP_PC);
                            p.Add("P_ID_NIVEL_GRADO", item.ID_NIVEL_GRADO);
                            p.Add("P_TIPO", item.ID_ACADEMICA == 0 ? "I" : "U");
                            db.Query(sp_5, p, commandType: CommandType.StoredProcedure);
                        }
                        entidad.FLG_OK = true;
                    }
                    catch (Exception ex)
                    {
                        entidad.FLG_OK = false;
                        Log.MensajeLog(ex.Message, sp_5);
                    }
                }

                foreach (var item in entidad.listaConocimiento)
                {
                    string sp_6 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_CONOCIMIENTO_V2";
                    try
                    {
                        using (IDbConnection db = new OracleConnection(this.cnSTR))
                        {
                            var p = new OracleDynamicParameters();
                            p.Add("P_ID_CONOCIMIENTOS", item.ID_CONOCIMIENTOS);
                            p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                            p.Add("P_DESC_CONOCIMIENTO", item.DESC_CONOCIMIENTO);
                            p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                            p.Add("P_IP_INGRESO", entidad.IP_PC);
                            p.Add("P_TIPO", item.ID_CONOCIMIENTOS == 0 ? "I" : "U");
                            db.Query(sp_6, p, commandType: CommandType.StoredProcedure);
                        }
                        entidad.FLG_OK = true;
                    }
                    catch (Exception ex)
                    {
                        entidad.FLG_OK = false;
                        Log.MensajeLog(ex.Message, sp_6);
                    }
                }
            }
            return entidad;
        }
        public Cls_Ent_Solicitud WizGrabarContrato(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_WIZ_PASO3";
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_FECHA_INICIO", entidad.FECHA_INICIO.ToString("dd/MM/yyyy"));
                param[2] = new OracleParameter("P_FECHA_FIN", entidad.FECHA_FIN.ToString("dd/MM/yyyy"));
                param[3] = new OracleParameter("P_MONTO_MENSUAL", entidad.MONTO_MENSUAL);
                param[4] = new OracleParameter("P_DENOMINACION_PUESTO", entidad.DENOMINACION_PUESTO);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.FLG_OK = true;

                if (entidad.listaActividad != null)
                {
                    sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_ACTIVIDAD_V2";
                    foreach (var item in entidad.listaActividad)
                    {
                        try
                        {
                            using (IDbConnection db = new OracleConnection(this.cnSTR))
                            {
                                var p = new OracleDynamicParameters();
                                p.Add("P_ID_ACTIVIDAD", item.ID_ACTIVIDAD);
                                p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                                p.Add("P_DESC_ACTIVIDAD", item.DESC_ACTIVIDAD);
                                p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                                p.Add("P_IP_INGRESO", entidad.IP_PC);
                                p.Add("P_TIPO", item.ID_ACTIVIDAD == 0 ? "I" : "U");
                                db.Query(sp, p, commandType: CommandType.StoredProcedure);
                            }
                            entidad.FLG_OK = true;
                        }
                        catch (Exception ex)
                        {
                            entidad.FLG_OK = false;
                            Log.MensajeLog(ex.Message, sp);
                        }
                    }
                }
                if (entidad.listaAcividadOtro != null)
                {
                    sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_OTR_ACT_V2";
                    foreach (var item in entidad.listaAcividadOtro)
                    {
                        try
                        {
                            using (IDbConnection db = new OracleConnection(this.cnSTR))
                            {
                                var p = new OracleDynamicParameters();
                                p.Add("P_ID_OTRO_ACT", item.ID_OTRO_ACT);
                                p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                                p.Add("P_DESC_ACT_OTRO", item.DESC_ACT_OTRO);
                                p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                                p.Add("P_IP_INGRESO", entidad.IP_PC);
                                p.Add("P_TIPO", item.ID_OTRO_ACT == 0 ? "I" : "U");
                                db.Query(sp, p, commandType: CommandType.StoredProcedure);
                            }
                            entidad.FLG_OK = true;
                        }
                        catch (Exception ex)
                        {
                            entidad.FLG_OK = false;
                            Log.MensajeLog(ex.Message, sp);
                        }
                    }
                }
                if (entidad.listaRenumeracion != null)
                {
                    sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_PERIODO_PAGO";
                    foreach (var item in entidad.listaRenumeracion)
                    {
                        var paramC = new OracleParameter[9];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_ID_ENTIDAD", item.ID_ENTIDAD);
                        paramC[2] = new OracleParameter("P_ANIO", item.ANIO);
                        paramC[3] = new OracleParameter("P_NUM_MES", item.ID_MES);
                        paramC[4] = new OracleParameter("P_DES_MES", item.DES_MES);
                        paramC[5] = new OracleParameter("P_MONTO", item.MONTO);
                        paramC[6] = new OracleParameter("P_TIPO_PROCESO", item.TIPO_PROCESO);
                        paramC[7] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[8] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                        try
                        {
                            OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, paramC);
                        }
                        catch (Exception ex)
                        {
                            Log.MensajeLog(ex.Message, sp);
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

        public Cls_Ent_Solicitud WizGrabarLocador(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_WIZ_PASO4";
            try
            {
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_DENOMINACION_PUESTO", entidad.DENOMINACION_PUESTO);
                param[2] = new OracleParameter("P_CONFORMIDAD_SERVICIO", entidad.CONFORMIDAD_SERVICIO);
                param[3] = new OracleParameter("P_APE_NOMB_CERTIFICA", entidad.APE_NOMB_CERTIFICA);
                param[4] = new OracleParameter("P_NUM_DOCUMENTO_CONSULTOR", entidad.NUM_DOCUMENTO_CONSULTOR);
                param[5] = new OracleParameter("P_APELLIDO_PAT_CONSULTOR", entidad.APELLIDO_PAT_CONSULTOR);
                param[6] = new OracleParameter("P_APELLIDO_MAT_CONSULTOR", entidad.APELLIDO_MAT_CONSULTOR);
                param[7] = new OracleParameter("P_NOMBRES_CONSULTOR", entidad.NOMBRES_CONSULTOR);
                param[8] = new OracleParameter("P_CORREO_CONSULTOR", entidad.CORREO_CONSULTOR);

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

        #endregion

        #region MANTENIMIENTO DE REQUISITOS V2 DIRECTIVA 2023
        public Cls_Ent_Solicitud MantenimientoSolicitudFag_V2(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_SOLICITUD_FAG";
            try
            {
                OracleParameter[] param = new OracleParameter[28];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[2] = new OracleParameter("P_DEPENDENCIA", entidad.DEPENDENCIA);
                param[3] = new OracleParameter("P_FECHA_INICIO", entidad.FECHA_INICIO.ToString("dd/MM/yyyy"));
                param[4] = new OracleParameter("P_FECHA_FIN", entidad.FECHA_FIN.ToString("dd/MM/yyyy"));
                param[5] = new OracleParameter("P_MONTO_MENSUAL", entidad.MONTO_MENSUAL);
                param[6] = new OracleParameter("P_DENOMINACION_PUESTO", entidad.DENOMINACION_PUESTO);
                param[7] = new OracleParameter("P_CONFORMIDAD_SERVICIO", entidad.CONFORMIDAD_SERVICIO);
                param[8] = new OracleParameter("P_APE_NOMB_CERTIFICA", entidad.APE_NOMB_CERTIFICA);
                param[9] = new OracleParameter("P_NUM_DOCUMENTO_CONSULTOR", entidad.NUM_DOCUMENTO_CONSULTOR);
                param[10] = new OracleParameter("P_APELLIDO_PAT_CONSULTOR", entidad.APELLIDO_PAT_CONSULTOR);
                param[11] = new OracleParameter("P_APELLIDO_MAT_CONSULTOR", entidad.APELLIDO_MAT_CONSULTOR);
                param[12] = new OracleParameter("P_NOMBRES_CONSULTOR", entidad.NOMBRES_CONSULTOR);
                param[13] = new OracleParameter("P_CORREO_CONSULTOR", entidad.CORREO_CONSULTOR);
                param[14] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[15] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                param[16] = new OracleParameter("P_TIPO", entidad.FLG_PROCESO);
                param[17] = new OracleParameter("P_OFICINA_CERTIFICA", entidad.OFICINA_CERTIFICA);
                param[18] = new OracleParameter("P_FLG_DESIGNADO", entidad.FLG_DESIGNADO);
                param[19] = new OracleParameter("P_NR_RESOLUCION", entidad.NR_RESOLUCION);
                param[20] = new OracleParameter("P_FECHA_RESOLUCION", entidad.FECHA_RESOLUCION.ToString("dd/MM/yyyy"));
                param[21] = new OracleParameter("P_NOMBRE_ARCHIVO_SUS_SOLICITUD", entidad.NOMBRE_ARCHIVO_SUS_SOLICITUD);
                param[22] = new OracleParameter("P_ARCHIVO_PUESTO_SUS_SOLICITUD", entidad.ARCHIVO_PUESTO_SUS_SOLICITUD);
                param[23] = new OracleParameter("P_DESC_ORGANO", entidad.DESC_ORGANO);
                param[24] = new OracleParameter("P_FLG_VERSION", entidad.FLG_VERSION);
                param[25] = new OracleParameter("P_FLG_NIVEL", entidad.FLG_NIVEL);
                param[26] = new OracleParameter("P_FLG_CEU", "0");
                param[27] = new OracleParameter("PO_ID_SOLICITUD", OracleDbType.Int32, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.ID_SOLICITUD = int.Parse(param[27].Value.ToString());
                if (entidad.listaExperiencia != null)
                {
                    foreach (var item in entidad.listaExperiencia)
                    {
                        string sp_3 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_EXPERIENCIA_V2";
                        try
                        {
                            using (IDbConnection db = new OracleConnection(this.cnSTR))
                            {
                                var p = new OracleDynamicParameters();
                                p.Add("P_ID_EXPERIENCIA", item.ID_EXPERIENCIA);
                                p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                                p.Add("P_DESC_EXPERIENCIA", item.DESC_EXPERIENCIA);
                                p.Add("P_ID_TIPO_EXPERIENCIA", item.ID_TIPO_EXPERIENCIA);
                                p.Add("P_ID_TIPO_SECTOR", item.ID_TIPO_SECTOR);
                                p.Add("P_ANOS", item.ANOS);
                                p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                                p.Add("P_IP_INGRESO", entidad.IP_PC);
                                p.Add("P_TIPO", item.ID_EXPERIENCIA==0 ?"I":"U");
                                db.Query(sp_3, p, commandType: CommandType.StoredProcedure);
                            }
                            entidad.FLG_OK = true;
                        }
                        catch (Exception ex)
                        {
                            entidad.FLG_OK = false;
                            Log.MensajeLog(ex.Message, sp_3);
                        }
                    }
                }
                if (entidad.listaCurso != null)
                {
                    foreach (var item in entidad.listaCurso)
                    {
                        string sp_4 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_CURSOS_V2";
                        try
                        {
                            using (IDbConnection db = new OracleConnection(this.cnSTR))
                            {
                                var p = new OracleDynamicParameters();
                                p.Add("P_ID_CURSOS_PRO", item.ID_CURSOS_PRO);
                                p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                                p.Add("P_DESC_CURSO_PRO", item.DESC_CURSO_PRO);
                                p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                                p.Add("P_IP_INGRESO", entidad.IP_PC);
                                p.Add("P_TIPO", item.ID_CURSOS_PRO == 0 ? "I" : "U");
                                db.Query(sp_4, p, commandType: CommandType.StoredProcedure);
                            }
                            entidad.FLG_OK = true;
                        }
                        catch (Exception ex)
                        {
                            entidad.FLG_OK = false;
                            Log.MensajeLog(ex.Message, sp_4);
                        }
                    }
                }
                if (entidad.listaAcademico != null)
                {
                    foreach (var item in entidad.listaAcademico)
                    {
                        string sp_5 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_ACADEMICA_V2";
                        try
                        {
                            using (IDbConnection db = new OracleConnection(this.cnSTR))
                            {
                                var p = new OracleDynamicParameters();
                                p.Add("P_ID_ACADEMICA", item.ID_ACADEMICA);
                                p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                                p.Add("P_DESC_ACADEMICA", item.DESC_ACADEMICA);
                                p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                                p.Add("P_IP_INGRESO", entidad.IP_PC);
                                p.Add("P_ID_NIVEL_GRADO", item.ID_NIVEL_GRADO);
                                p.Add("P_TIPO", item.ID_ACADEMICA == 0 ? "I" : "U");
                                db.Query(sp_5, p, commandType: CommandType.StoredProcedure);
                            }
                            entidad.FLG_OK = true;
                        }
                        catch (Exception ex)
                        {
                            entidad.FLG_OK = false;
                            Log.MensajeLog(ex.Message, sp_5);
                        }
                    }
                }
                if (entidad.listaConocimiento != null)
                {
                    foreach (var item in entidad.listaConocimiento)
                    {
                        string sp_6 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_CONOCIMIENTO_V2";
                        try
                        {
                            using (IDbConnection db = new OracleConnection(this.cnSTR))
                            {
                                var p = new OracleDynamicParameters();
                                p.Add("P_ID_CONOCIMIENTOS", item.ID_CONOCIMIENTOS);
                                p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                                p.Add("P_DESC_CONOCIMIENTO", item.DESC_CONOCIMIENTO);
                                p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                                p.Add("P_IP_INGRESO", entidad.IP_PC);
                                p.Add("P_TIPO", item.ID_CONOCIMIENTOS == 0 ? "I" : "U");
                                db.Query(sp_6, p, commandType: CommandType.StoredProcedure);
                            }
                            entidad.FLG_OK = true;
                        }
                        catch (Exception ex)
                        {
                            entidad.FLG_OK = false;
                            Log.MensajeLog(ex.Message, sp_6);
                        }
                    }
                }
                if (entidad.listaActividad != null)
                {
                    foreach (var item in entidad.listaActividad)
                    {
                        string sp_7 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_ACTIVIDAD_V2";
                        try
                        {
                            using (IDbConnection db = new OracleConnection(this.cnSTR))
                            {
                                var p = new OracleDynamicParameters();
                                p.Add("P_ID_ACTIVIDAD", item.ID_ACTIVIDAD);
                                p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                                p.Add("P_DESC_ACTIVIDAD", item.DESC_ACTIVIDAD);
                                p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                                p.Add("P_IP_INGRESO", entidad.IP_PC);
                                p.Add("P_TIPO", item.ID_ACTIVIDAD == 0 ? "I" : "U");
                                db.Query(sp_7, p, commandType: CommandType.StoredProcedure);
                            }
                            entidad.FLG_OK = true;
                        }
                        catch (Exception ex)
                        {
                            entidad.FLG_OK = false;
                            Log.MensajeLog(ex.Message, sp_7);
                        }
                    }
                }
                if (entidad.listaAcividadOtro != null)
                {
                    foreach (var item in entidad.listaAcividadOtro)
                    {
                        string sp_8 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_OTR_ACT_V2";
                        try
                        {
                            using (IDbConnection db = new OracleConnection(this.cnSTR))
                            {
                                var p = new OracleDynamicParameters();
                                p.Add("P_ID_OTRO_ACT", item.ID_OTRO_ACT);
                                p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                                p.Add("P_DESC_ACT_OTRO", item.DESC_ACT_OTRO);
                                p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                                p.Add("P_IP_INGRESO", entidad.IP_PC);
                                p.Add("P_TIPO", item.ID_OTRO_ACT == 0 ? "I" : "U");
                                db.Query(sp_8, p, commandType: CommandType.StoredProcedure);
                            }
                            entidad.FLG_OK = true;
                        }
                        catch (Exception ex)
                        {
                            entidad.FLG_OK = false;
                            Log.MensajeLog(ex.Message, sp_8);
                        }
                    }
                }
                if (entidad.listaRenumeracion != null)
                {
                    foreach (var item in entidad.listaRenumeracion)
                    {
                        string sp_9 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_PERIODO_PAGO";
                        var paramC = new OracleParameter[9];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_ID_ENTIDAD", item.ID_ENTIDAD);
                        paramC[2] = new OracleParameter("P_ANIO", item.ANIO);
                        paramC[3] = new OracleParameter("P_NUM_MES", item.ID_MES);
                        paramC[4] = new OracleParameter("P_DES_MES", item.DES_MES);
                        paramC[5] = new OracleParameter("P_MONTO", item.MONTO);
                        paramC[6] = new OracleParameter("P_TIPO_PROCESO", item.TIPO_PROCESO);
                        paramC[7] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[8] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
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
        #endregion
        #region MANTENIMIENTO DE REQUISITOS V2 DIRECTIVA CEU 2023 
        public Cls_Ent_Solicitud MantenimientoSolicitudFagCeu_V2(Cls_Ent_Solicitud entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_SOLICITUD_FAG";
            try
            {
                OracleParameter[] param = new OracleParameter[28];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                param[2] = new OracleParameter("P_DEPENDENCIA", entidad.DEPENDENCIA);
                param[3] = new OracleParameter("P_FECHA_INICIO", entidad.FECHA_INICIO.ToString("dd/MM/yyyy"));
                param[4] = new OracleParameter("P_FECHA_FIN", entidad.FECHA_FIN.ToString("dd/MM/yyyy"));
                param[5] = new OracleParameter("P_MONTO_MENSUAL", entidad.MONTO_MENSUAL);
                param[6] = new OracleParameter("P_DENOMINACION_PUESTO", entidad.DENOMINACION_PUESTO);
                param[7] = new OracleParameter("P_CONFORMIDAD_SERVICIO", entidad.CONFORMIDAD_SERVICIO);
                param[8] = new OracleParameter("P_APE_NOMB_CERTIFICA", entidad.APE_NOMB_CERTIFICA);
                param[9] = new OracleParameter("P_NUM_DOCUMENTO_CONSULTOR", entidad.NUM_DOCUMENTO_CONSULTOR);
                param[10] = new OracleParameter("P_APELLIDO_PAT_CONSULTOR", entidad.APELLIDO_PAT_CONSULTOR);
                param[11] = new OracleParameter("P_APELLIDO_MAT_CONSULTOR", entidad.APELLIDO_MAT_CONSULTOR);
                param[12] = new OracleParameter("P_NOMBRES_CONSULTOR", entidad.NOMBRES_CONSULTOR);
                param[13] = new OracleParameter("P_CORREO_CONSULTOR", entidad.CORREO_CONSULTOR);
                param[14] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                param[15] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
                param[16] = new OracleParameter("P_TIPO", entidad.FLG_PROCESO);
                param[17] = new OracleParameter("P_OFICINA_CERTIFICA", entidad.OFICINA_CERTIFICA);
                param[18] = new OracleParameter("P_FLG_DESIGNADO", entidad.FLG_DESIGNADO);
                param[19] = new OracleParameter("P_NR_RESOLUCION", entidad.NR_RESOLUCION);
                param[20] = new OracleParameter("P_FECHA_RESOLUCION", entidad.FECHA_RESOLUCION.ToString("dd/MM/yyyy"));
                param[21] = new OracleParameter("P_NOMBRE_ARCHIVO_SUS_SOLICITUD", entidad.NOMBRE_ARCHIVO_SUS_SOLICITUD);
                param[22] = new OracleParameter("P_ARCHIVO_PUESTO_SUS_SOLICITUD", entidad.ARCHIVO_PUESTO_SUS_SOLICITUD);
                param[23] = new OracleParameter("P_DESC_ORGANO", entidad.DESC_ORGANO);
                param[24] = new OracleParameter("P_FLG_VERSION", entidad.FLG_VERSION);
                param[25] = new OracleParameter("P_FLG_NIVEL", entidad.FLG_NIVEL);
                param[26] = new OracleParameter("P_FLG_CEU", "1");
                param[27] = new OracleParameter("PO_ID_SOLICITUD", OracleDbType.Int32, ParameterDirection.Output);
                OracleHelper.ExecuteNonQuery(this.cnSTR, CommandType.StoredProcedure, sp, param);
                entidad.ID_SOLICITUD = int.Parse(param[27].Value.ToString());
                if (entidad.listaActividad != null)
                {
                    foreach (var item in entidad.listaActividad)
                    {
                        string sp_7 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_ACTIVIDAD_V2";
                        try
                        {
                            using (IDbConnection db = new OracleConnection(this.cnSTR))
                            {
                                var p = new OracleDynamicParameters();
                                p.Add("P_ID_ACTIVIDAD", item.ID_ACTIVIDAD);
                                p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                                p.Add("P_DESC_ACTIVIDAD", item.DESC_ACTIVIDAD);
                                p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                                p.Add("P_IP_INGRESO", entidad.IP_PC);
                                p.Add("P_TIPO", item.ID_ACTIVIDAD == 0 ? "I" : "U");
                                db.Query(sp_7, p, commandType: CommandType.StoredProcedure);
                            }
                            entidad.FLG_OK = true;
                        }
                        catch (Exception ex)
                        {
                            entidad.FLG_OK = false;
                            Log.MensajeLog(ex.Message, sp_7);
                        }
                    }
                }
                if (entidad.listaAcividadOtro != null)
                {
                    foreach (var item in entidad.listaAcividadOtro)
                    {
                        string sp_8 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_MNT_OTR_ACT_V2";
                        try
                        {
                            using (IDbConnection db = new OracleConnection(this.cnSTR))
                            {
                                var p = new OracleDynamicParameters();
                                p.Add("P_ID_OTRO_ACT", item.ID_OTRO_ACT);
                                p.Add("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                                p.Add("P_DESC_ACT_OTRO", item.DESC_ACT_OTRO);
                                p.Add("P_USU_INGRESO", entidad.USU_INGRESO);
                                p.Add("P_IP_INGRESO", entidad.IP_PC);
                                p.Add("P_TIPO", item.ID_OTRO_ACT == 0 ? "I" : "U");
                                db.Query(sp_8, p, commandType: CommandType.StoredProcedure);
                            }
                            entidad.FLG_OK = true;
                        }
                        catch (Exception ex)
                        {
                            entidad.FLG_OK = false;
                            Log.MensajeLog(ex.Message, sp_8);
                        }
                    }
                }
                if (entidad.listaRenumeracion != null)
                {
                    foreach (var item in entidad.listaRenumeracion)
                    {
                        string sp_9 = "FAGPAC.PACK_EXTRANET_COORDINADOR.PRC_INS_PERIODO_PAGO";
                        var paramC = new OracleParameter[9];
                        paramC[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                        paramC[1] = new OracleParameter("P_ID_ENTIDAD", item.ID_ENTIDAD);
                        paramC[2] = new OracleParameter("P_ANIO", item.ANIO);
                        paramC[3] = new OracleParameter("P_NUM_MES", item.ID_MES);
                        paramC[4] = new OracleParameter("P_DES_MES", item.DES_MES);
                        paramC[5] = new OracleParameter("P_MONTO", item.MONTO);
                        paramC[6] = new OracleParameter("P_TIPO_PROCESO", item.TIPO_PROCESO);
                        paramC[7] = new OracleParameter("P_USU_INGRESO", entidad.USU_INGRESO);
                        paramC[8] = new OracleParameter("P_IP_INGRESO", entidad.IP_INGRESO);
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
        #endregion
    }
}
