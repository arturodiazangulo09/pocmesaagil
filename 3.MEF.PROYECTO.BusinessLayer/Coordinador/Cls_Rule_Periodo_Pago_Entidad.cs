using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;
namespace MEF.PROYECTO.BusinessLayer.Coordinador
{
    public class Cls_Rule_Periodo_Pago_Entidad
    {
        private static Cls_Dat_Periodo_Pago_Entidad ODatos = new Cls_Dat_Periodo_Pago_Entidad();
        public static List<Cls_Periodo_Pago_Entidad> ListaPeriodoPagoEntidad(Cls_Periodo_Pago_Entidad entidad)
        {
            return ODatos.ListaPeriodoPagoEntidad(entidad);
        }
    }
}
