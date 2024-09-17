using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;

namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio
{
    public class PeriodoPagoEntidadRepositorio: IDisposable
    {
        public List<Cls_Periodo_Pago_Entidad> ListaPeriodoPagoEntidad(Cls_Periodo_Pago_Entidad entidad)
        {
            List<Cls_Periodo_Pago_Entidad> lista = null;
            lista = Cls_Rule_Periodo_Pago_Entidad.ListaPeriodoPagoEntidad(entidad);
            return lista;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}