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
    public class Cls_Dat_Ubigeo : DataBaseHelper
    {
        public List<Cls_Ent_Ubigeo> ListaDepartamento()
        {
            List<Cls_Ent_Ubigeo> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_DEPA";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Ubigeo>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Ubigeo> listaProvincias(Cls_Ent_Ubigeo entidad)
        {
            List<Cls_Ent_Ubigeo> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_PROV";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("p_DEPA", entidad.CCODDEPARTAMENTO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Ubigeo>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Ubigeo> listaDistritos(Cls_Ent_Ubigeo entidad)
        {
            List<Cls_Ent_Ubigeo> lista = null;
            string sp = "FAGPAC.PACK_ADMINISTRACION_TABLAS.USP_LISTA_DIST";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("p_DEPA", entidad.CCODDEPARTAMENTO);
                    p.Add("p_PROV", entidad.CCODPROVINCIA);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Ubigeo>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Ubigeo> CargaContratos_listaProvincias(Cls_Ent_Ubigeo entidad)
        {
            List<Cls_Ent_Ubigeo> lista = null;
            string sp = "FAGPAC.PACK_CARGA_DATOS.USP_LISTA_PROV";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("p_DEPA", entidad.CCODDEPARTAMENTO.Trim());
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Ubigeo>(sp, p, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.ToString(), sp);
            }

            return lista;
        }
        public List<Cls_Ent_Ubigeo> Carga_Contratos_listaDistritos(Cls_Ent_Ubigeo entidad)
        {
            List<Cls_Ent_Ubigeo> lista = null;
            string sp = "FAGPAC.PACK_CARGA_DATOS.USP_LISTA_DIST";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("p_DEPA", entidad.CCODDEPARTAMENTO.Trim());
                    p.Add("p_PROV", entidad.CCODPROVINCIA.Trim());
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Ent_Ubigeo>(sp, p, commandType: CommandType.StoredProcedure).ToList();
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
