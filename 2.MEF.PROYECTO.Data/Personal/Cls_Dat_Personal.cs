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
using MEF.PROYECTO.Entity.Coordinador;
namespace MEF.PROYECTO.Data.Personal
{
    public class Cls_Dat_Personal: DataBaseHelper
    {
        public List<Cls_Ent_Personal> ListaPersonal(Cls_Ent_Personal entidad)
        {
            List<Cls_Ent_Personal> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_PERSONAL";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_NUM_DOCUMENTO", entidad.NUM_DOCUMENTO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Personal>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Personal UpdateContrasenaPersonal(Cls_Ent_Personal entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_UPD_CONTRASENA";
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("P_ID_COORDINADOR", entidad.ID_PERSONAL);
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
        public Cls_Ent_Personal UpdateRestablecerContrasenaPersonal(Cls_Ent_Personal entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_UPD_CAMBIO_CLAVE";
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
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
        //ESTUDIOS REALIZADOS
        public List<Cls_Ent_Nivel_Academico> ListaNivelAcademico()
        {
            List<Cls_Ent_Nivel_Academico> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_NIVEL_ACADEMICO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Nivel_Academico>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Grado_Academico> ListaNivelGrado()
        {
            List<Cls_Ent_Grado_Academico> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_NIVEL_GRADO";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Grado_Academico>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Estudios Mentenimiento_Estudios(Cls_Ent_Estudios entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_MNT_FORMACION";
            try
            {
                OracleParameter[] param = new OracleParameter[13];
                //OracleParameter BLOB_ARCHIVO = new OracleParameter();
                //BLOB_ARCHIVO.OracleDbType = OracleDbType.Blob;
                //BLOB_ARCHIVO.ParameterName = "P_ARCHIVO";
                //BLOB_ARCHIVO.Value = entidad.ARCHIVO;

                param[0] = new OracleParameter("P_ID_FORMAC_ACADEMICA", entidad.ID_FORMAC_ACADEMICA);
                param[1] = new OracleParameter("P_FEC_EMISION_FORMAC_ACADEMICA", entidad.FEC_EMISION_FORMAC_ACADEMICA.ToString("dd/MM/yyyy"));
                param[2] = new OracleParameter("P_DESC_ACADEMICA", entidad.DESC_ACADEMICA);
                param[3] = new OracleParameter("P_ID_NIVEL_ACADEMICO", entidad.ID_NIVEL_ACADEMICO);
                param[4] = new OracleParameter("P_NOMBRE_ARCHIVO", entidad.NOMBRE_ARCHIVO);
                param[5] = new OracleParameter("P_ARCHIVO", entidad.ARCHIVO); //BLOB_ARCHIVO;
                param[6] = new OracleParameter("P_ID_NIVEL_GRADO", entidad.ID_NIVEL_GRADO);
                param[7] = new OracleParameter("P_DESC_CENTRO_ESTUDIOS", entidad.DESC_CENTRO_ESTUDIOS);
                param[8] = new OracleParameter("P_DESC_CIUDAD_PAIS", entidad.DESC_CIUDAD_PAIS);
                param[9] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
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
        public List<Cls_Ent_Estudios> ListaEstudios(Cls_Ent_Estudios entidad)
        {
            List<Cls_Ent_Estudios> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_FORMACION_ACADEMICA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_PERSONAL", entidad.ID_PERSONAL);
                    p.Add("P_ID_FORMAC_ACADEMICA", entidad.ID_FORMAC_ACADEMICA);
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
        //FIN ESTUDIOS REALIZADOS
        //ESTUDIOS ESPECIALIZACION
        public List<Cls_Ent_Tipo_Especializacion> ListaTipoEspecializacion()
        {
            List<Cls_Ent_Tipo_Especializacion> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_TIPO_ESPECIALIZACION";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Tipo_Especializacion>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Especializacion Mentenimiento_Especializacion(Cls_Ent_Especializacion entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_MNT_ESPECIALIZACION";
            try
            {
                OracleParameter[] param = new OracleParameter[14];
                //OracleParameter BLOB_ARCHIVO = new OracleParameter();
                //BLOB_ARCHIVO.OracleDbType = OracleDbType.Blob;
                //BLOB_ARCHIVO.ParameterName = "P_ARCHIVO";
                //BLOB_ARCHIVO.Value = entidad.ARCHIVO;

                param[0] = new OracleParameter("P_ID_ESPECIALIZACION", entidad.ID_ESPECIALIZACION);
                param[1] = new OracleParameter("P_FEC_INICIO_ESPECIALIZACION", entidad.FEC_INICIO_ESPECIALIZACION.ToString("dd/MM/yyyy"));
                param[2] = new OracleParameter("P_FEC_FIN_ESPECIALIZACION", entidad.FEC_FIN_ESPECIALIZACION.ToString("dd/MM/yyyy"));
                param[3] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
                param[4] = new OracleParameter("P_DESC_CENTRO_ESTUDIOS", entidad.DESC_CENTRO_ESTUDIOS);
                param[5] = new OracleParameter("P_ID_TIPO_ESPECIALIZACION", entidad.ID_TIPO_ESPECIALIZACION);
                param[6] = new OracleParameter("P_NOMBRE_ESPECIALIZACION", entidad.NOMBRE_ESPECIALIZACION);
                param[7] = new OracleParameter("P_NOMBRE_CIUDAD_PAIS", entidad.NOMBRE_CIUDAD_PAIS);
                param[8] = new OracleParameter("P_HORAS", entidad.HORAS);

                param[9] = new OracleParameter("P_NOMBRE_ARCHIVO", entidad.NOMBRE_ARCHIVO);
                param[10] = new OracleParameter("P_ARCHIVO", entidad.ARCHIVO);///BLOB_ARCHIVO;
                param[11] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[12] = new OracleParameter("P_IP", entidad.IP_PC);
                param[13] = new OracleParameter("P_TIPO", entidad.ACCION);
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
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_ESPECIALIZACION";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_PERSONAL", entidad.ID_PERSONAL);
                    p.Add("P_ID_ESPECIALIZACION", entidad.ID_ESPECIALIZACION);
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
        //FIN ESPECIALIZACION REALIZADOS
        //ESTUDIOS CAPACITACIÓN
        public Cls_Ent_Capacitacion Mentenimiento_Capacitacion(Cls_Ent_Capacitacion entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_MNT_CAPACITACION";
            try
            {
                OracleParameter[] param = new OracleParameter[12];
                //OracleParameter BLOB_ARCHIVO = new OracleParameter();
                //BLOB_ARCHIVO.OracleDbType = OracleDbType.Blob;
                //BLOB_ARCHIVO.ParameterName = "P_ARCHIVO";
                //BLOB_ARCHIVO.Value = entidad.ARCHIVO;

                param[0] = new OracleParameter("P_ID_CAPACITACION", entidad.ID_CAPACITACION);
                param[1] = new OracleParameter("P_FEC_INICIO_CAPACITACION", entidad.FEC_INICIO_CAPACITACION.ToString("dd/MM/yyyy"));
                param[2] = new OracleParameter("P_FEC_FIN_CAPACITACION", entidad.FEC_FIN_CAPACITACION.ToString("dd/MM/yyyy"));
                param[3] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
                param[4] = new OracleParameter("P_DESC_CENTRO_ESTUDIOS", entidad.DESC_CENTRO_ESTUDIOS);
                param[5] = new OracleParameter("P_NOMBRE_CAPACITACION", entidad.NOMBRE_CAPACITACION);
                param[6] = new OracleParameter("P_NOMBRE_CIUDAD_PAIS", entidad.NOMBRE_CIUDAD_PAIS);
                param[7] = new OracleParameter("P_NOMBRE_ARCHIVO", entidad.NOMBRE_ARCHIVO);
                param[8] = new OracleParameter("P_ARCHIVO", entidad.ARCHIVO);//BLOB_ARCHIVO;
                param[9] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[10] = new OracleParameter("P_IP", entidad.IP_PC);
                param[11] = new OracleParameter("P_TIPO", entidad.ACCION);
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
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_CAPACITACION";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_PERSONAL", entidad.ID_PERSONAL);
                    p.Add("P_ID_CAPACITACION", entidad.ID_CAPACITACION);
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
        //FIN CAPACITACIÓN REALIZADOS
        //EXPERIENCIA CAPACITACIÓN
        public List<Cls_Ent_Tipo_Experiencia> ListaTipoExperiencia()
        {
            List<Cls_Ent_Tipo_Experiencia> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_TIPO_EXPERIENCIA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Tipo_Experiencia>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Experiencia_Laboral Mentenimiento_Experiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_MNT_EXPERIENCIA";
            try
            {
                OracleParameter[] param = new OracleParameter[16];
                //OracleParameter BLOB_ARCHIVO = new OracleParameter();
                //BLOB_ARCHIVO.OracleDbType = OracleDbType.Blob;
                //BLOB_ARCHIVO.ParameterName = "P_ARCHIVO";
                //BLOB_ARCHIVO.Value = entidad.ARCHIVO;
                param[0] = new OracleParameter("P_ID_EXPERIENCIA", entidad.ID_EXPERIENCIA);
                param[1] = new OracleParameter("P_ID_TIPO_EXPERIENCIA", entidad.ID_TIPO_EXPERIENCIA);
                param[2] = new OracleParameter("P_NOMBRE_ENTIDAD", entidad.NOMBRE_ENTIDAD);
                param[3] = new OracleParameter("P_CARGO_EMPRESA", entidad.CARGO_EMPRESA);
                param[4] = new OracleParameter("P_FEC_INICIO_EXPERIENCIA", entidad.FEC_INICIO_EXPERIENCIA.ToString("dd/MM/yyyy"));
                param[5] = new OracleParameter("P_FEC_FIN_EXPERIENCIA", entidad.FEC_FIN_EXPERIENCIA.ToString("dd/MM/yyyy"));  
                param[6] = new OracleParameter("P_NUM_ANIOS", entidad.NUM_ANIOS);
                param[7] = new OracleParameter("P_NUM_MESES", entidad.NUM_MESES);
                param[8] = new OracleParameter("P_NUM_DIAS", entidad.NUM_DIAS);
                param[9] = new OracleParameter("P_FUNCIONES", entidad.FUNCIONES);
                param[10] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
                param[11] = new OracleParameter("P_NOMBRE_ARCHIVO", entidad.NOMBRE_ARCHIVO);
                param[12] = new OracleParameter("P_ARCHIVO", entidad.ARCHIVO); //BLOB_ARCHIVO;
                param[13] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[14] = new OracleParameter("P_IP", entidad.IP_PC);
                param[15] = new OracleParameter("P_TIPO", entidad.ACCION);
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
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_EXPERIENCIA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_PERSONAL", entidad.ID_PERSONAL);
                    p.Add("P_ID_EXPERIENCIA", entidad.ID_EXPERIENCIA);
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
        public List<Cls_Ent_Tipo_Sector_Experiencia> ListaTipoSectorExperiencia()
        {
            List<Cls_Ent_Tipo_Sector_Experiencia> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_SECTOR_EXPE";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Tipo_Sector_Experiencia>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        //FIN EXPERIENCIA REALIZADOS
        public List<Cls_Ent_Nacionalidad> ListaTipoNacionalidad()
        {
            List<Cls_Ent_Nacionalidad> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_NACIONALIDAD";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Nacionalidad>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Banco> ListaTipoBanco()
        {
            List<Cls_Ent_Banco> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_BANCOS";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Banco>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Personal UpdatePersonal(Cls_Ent_Personal entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_UPDATE_PERSONAL";
            try
            {
                OracleParameter[] param = new OracleParameter[28];
                param[0] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
                param[1] = new OracleParameter("P_FECHA_NACIMIENTO", entidad.FECHA_NACIMIENTO.ToString("dd/MM/yyyy"));
                param[2] = new OracleParameter("P_ID_SEXO", entidad.ID_SEXO);
                param[3] = new OracleParameter("P_ID_TIPO_DOCUMENTO", entidad.ID_TIPO_DOCUMENTO);
                param[4] = new OracleParameter("P_TELEFONO", entidad.TELEFONO);
                param[5] = new OracleParameter("P_CELULAR", entidad.CELULAR);
                param[6] = new OracleParameter("P_EMAIL", entidad.EMAIL);
                param[7] = new OracleParameter("P_RUC", entidad.RUC);
                param[8] = new OracleParameter("P_ID_TIPO_NACIONALIDAD", entidad.ID_TIPO_NACIONALIDAD);
                param[9] = new OracleParameter("P_COD_DEPA_NAC", entidad.COD_DEPA_NAC);
                param[10] = new OracleParameter("P_COD_PROV_NAC", entidad.COD_PROV_NAC);
                param[11] = new OracleParameter("P_COD_DIST_NAC", entidad.COD_DIST_NAC);
                param[12] = new OracleParameter("P_DPTO_PROV_EXTRANJERO", entidad.DPTO_PROV_EXTRANJERO);
                param[13] = new OracleParameter("P_COD_DEPA", entidad.COD_DEPA);
                param[14] = new OracleParameter("P_COD_PROV", entidad.COD_PROV);
                param[15] = new OracleParameter("P_COD_DIST", entidad.COD_DIST);
                param[16] = new OracleParameter("P_DIRECCION", entidad.DIRECCION);
                param[17] = new OracleParameter("P_ID_TIPO_BANCO", entidad.ID_TIPO_BANCO);
                param[18] = new OracleParameter("P_CUENTA_BANCARIA", entidad.CUENTA_BANCARIA);
                param[19] = new OracleParameter("P_CUENTA_CCI", entidad.CUENTA_CCI);
                param[20] = new OracleParameter("P_APE_NOMBRES_CONTACTO", entidad.APE_NOMBRES_CONTACTO);
                param[21] = new OracleParameter("P_TELEFONO_CONTACTO", entidad.TELEFONO_CONTACTO);
                param[22] = new OracleParameter("P_CELULAR_CONTACTO", entidad.CELULAR_CONTACTO);
                param[23] = new OracleParameter("P_USU_MODIFICA", entidad.USU_INGRESO);
                param[24] = new OracleParameter("P_IP_MODIFICA", entidad.IP_PC);
                param[25] = new OracleParameter("P_ESTADO_CIVIL", entidad.ESTADO_CIVIL);
                param[26] = new OracleParameter("P_GRADOS", entidad.GRADOS);
                param[27] = new OracleParameter("P_TITULOS", entidad.TITULOS);
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
        public List<Cls_Ent_Solicitud_Personal> ListasolicitudPersonal(Cls_Ent_Solicitud_Personal entidad)
        {
            List<Cls_Ent_Solicitud_Personal> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.USP_LISTA_PERSONAL_SOLICITUD";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_PERSONAL",entidad.ID_PERSONAL);
                    p.Add("P_ID_SOLICITUD",entidad.ID_SOLICITUD);
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("P_NUM_PROCESO", entidad.NUM_PROCESO);
                    p.Add("P_ANIO_PROCESO", entidad.ANIO_PROCESO);
                    p.Add("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Solicitud_Personal>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Solicitud_Personal UpdateDJ_Fag(Cls_Ent_Solicitud_Personal entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_UPDATE_DJ_FAG";
            try
            {
                OracleParameter[] param = new OracleParameter[13];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
                param[2] = new OracleParameter("P_MONTO_RECIBO", entidad.MONTO_RECIBO);
                param[3] = new OracleParameter("P_FLG_CHECK_4", entidad.FLG_CHECK_4);
                param[4] = new OracleParameter("P_FLG_PENSIONISTA_ESTADO", entidad.FLG_PENSIONISTA_ESTADO);
                param[5] = new OracleParameter("P_FLG_PENSIONISTA_POLICIA", entidad.FLG_PENSIONISTA_POLICIA);
                param[6] = new OracleParameter("P_USUARIO", entidad.USU_MODIFICA);
                param[7] = new OracleParameter("P_IP", entidad.IP_PC);
                param[8] = new OracleParameter("P_ID_ANEXO2", entidad.ID_ANEXO2);
                param[9] = new OracleParameter("P_ID_ANEXO3", entidad.ID_ANEXO3);
                param[10] = new OracleParameter("P_ID_ANEXO7", entidad.ID_ANEXO7);
                param[11] = new OracleParameter("P_ID_BANCO", entidad.ID_BANCO);
                param[12] = new OracleParameter("P_FLG_PENSIONES", entidad.FLG_PENSIONES);
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
        public Cls_Ent_Solicitud_Personal UpdateDJ_Pac(Cls_Ent_Solicitud_Personal entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_UPDATE_DJ_PAC";
            try
            {
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
                param[2] = new OracleParameter("P_MONTO_RECIBO", entidad.MONTO_RECIBO);
                param[3] = new OracleParameter("P_FLG_CHECK_4", entidad.FLG_CHECK_4);
                param[4] = new OracleParameter("P_USUARIO", entidad.USU_MODIFICA);
                param[5] = new OracleParameter("P_IP", entidad.IP_PC);

                param[6] = new OracleParameter("P_ID_FORMATOB", entidad.ID_FORMATOB);
                param[7] = new OracleParameter("P_ID_FORMATOC", entidad.ID_FORMATOC);
                param[8] = new OracleParameter("P_ID_PAC_ANEXO2", entidad.ID_PAC_ANEXO2);
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
        public Cls_Ent_Solicitud_Personal UpdatePropuesta_Envio(Cls_Ent_Solicitud_Personal entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_UPDATE_ENVIO_PROPUESTA";
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
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
        public Cls_Ent_Renumeracion UpdateReciboHonorario(Cls_Ent_Renumeracion entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_UPD_RECIBO_HONORARIO";
            try
            {
                OracleParameter[] param = new OracleParameter[13];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_NUM_MES", entidad.NUM_MES);
                param[2] = new OracleParameter("P_FECHA_GENERACION", entidad.FECHA_GENERACION.ToString("dd/MM/yyyy"));
                param[3] = new OracleParameter("P_DESC_SERVICIO", entidad.DESC_SERVICIO);
                param[4] = new OracleParameter("P_NR_COMPROBANTE", entidad.NR_COMPROBANTE);
                param[5] = new OracleParameter("P_IMPORTE_COMPROBANTE", entidad.IMPORTE_COMPROBANTE);
                param[6] = new OracleParameter("P_ID_ARCHIVO_RECIBO", entidad.ID_ARCHIVO_RECIBO);
                param[7] = new OracleParameter("P_ID_ARCHIVO_CPE", entidad.ID_ARCHIVO_CPE);
                param[8] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[9] = new OracleParameter("P_IP", entidad.IP_PC);
                param[10] = new OracleParameter("P_ID_ARCHIVO_INFORME", entidad.ID_ARCHIVO_INFORME);
                param[11] = new OracleParameter("P_SERIE_COMPROBANTE", entidad.SERIE_COMPROBANTE);
                param[12] = new OracleParameter("P_FLG_CUARTA_CATEGORIA", entidad.FLG_CUARTA_CATEGORIA);
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
        public Cls_Ent_Renumeracion UpdateReciboEstado(Cls_Ent_Renumeracion entidad)
        {
            string sp = "FAGPAC.PACK_EXTRANET_PERSONAL.PRC_UPD_RECIBO_ESTADO";
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("P_ID_SOLICITUD", entidad.ID_SOLICITUD);
                param[1] = new OracleParameter("P_NUM_MES", entidad.NUM_MES);
                param[2] = new OracleParameter("P_IDEDOCODIGO", entidad.IDEDOCODIGO);
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
        public List<Cls_Ent_Personal> ListaPersonalAdministrador(Cls_Ent_Personal entidad)
        {
            List<Cls_Ent_Personal> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_CONSULTORES";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_NUM_DOCUMENTO", entidad.NUM_DOCUMENTO);
                    p.Add("P_APELLIDO_NOMBRES", entidad.APELLIDO_NOMBRES);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Personal>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public Cls_Ent_Personal MentenimientoInformacion_Personal(Cls_Ent_Personal entidad)
        {
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.PRC_MNT_INFORMACION";
            try
            {
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("P_ID_INFORMACION", entidad.ID_INFORMACION);
                param[1] = new OracleParameter("P_ID_PERSONAL", entidad.ID_PERSONAL);
                param[2] = new OracleParameter("P_DESC_INFORMACION", entidad.DES_INFORMACION);
                param[3] = new OracleParameter("P_ENTIDAD", entidad.ENTIDAD);
                param[4] = new OracleParameter("P_PERSONA", entidad.PERSONA);
                param[5] = new OracleParameter("P_CELULAR", entidad.CELULAR);
                param[6] = new OracleParameter("P_USUARIO", entidad.USU_INGRESO);
                param[7] = new OracleParameter("P_IP", entidad.IP_PC);
                param[8] = new OracleParameter("P_TIPO", entidad.ACCION);
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
        public List<Cls_Ent_Personal> ListaInformacionPersonal(Cls_Ent_Personal entidad)
        {
            List<Cls_Ent_Personal> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_INFORMACION";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_INFORMACION", entidad.ID_INFORMACION);
                    p.Add("P_ID_PERSONAL", entidad.ID_PERSONAL);
                    p.Add("P_DESCRIPCION", entidad.DES_INFORMACION);
                    
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Personal>(sp, p, commandType: CommandType.StoredProcedure).ToList();
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
