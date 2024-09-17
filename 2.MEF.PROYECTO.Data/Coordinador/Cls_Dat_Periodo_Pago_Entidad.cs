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
 public    class Cls_Dat_Periodo_Pago_Entidad : DataBaseHelper
    {
        public List<Cls_Periodo_Pago_Entidad> ListaPeriodoPagoEntidad(Cls_Periodo_Pago_Entidad entidad)
        {
            List<Cls_Periodo_Pago_Entidad> lista = null;
            string sp = "FAGPAC.PACK_EXTRANET_COORDINADOR.USP_LISTA_PERIODO_PAGO_ENTIDAD";
            try
            {
                using (IDbConnection db = new OracleConnection(this.cnSTR))
                {
                    var p = new OracleDynamicParameters();
                    p.Add("P_ID_ENTIDAD", entidad.ID_ENTIDAD);
                    p.Add("P_TIPO_PROCESO", entidad.TIPO_PROCESO);
                    p.Add("P_NUM_MES", entidad.NUM_MES);
                    p.Add("P_ANIO", entidad.ANIO_PERIODO);
                    p.Add("PO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    lista = db.Query<Cls_Periodo_Pago_Entidad>(sp, p, commandType: CommandType.StoredProcedure).ToList();
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
