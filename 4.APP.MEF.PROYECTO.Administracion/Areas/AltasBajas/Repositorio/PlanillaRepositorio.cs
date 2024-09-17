using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.Coordinador;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.AltasBajas.Repositorio
{
    public class PlanillaRepositorio
    {
        public List<Cls_Ent_Solicitud_Pago> ListaPeriodoPlanilla(Cls_Ent_Solicitud_Pago entidad)
        {
            List<Cls_Ent_Solicitud_Pago> lista = null;
            lista = Cls_Rule_Solicitudes_Coordinador.ListaPeriodoPlanilla(entidad);
            return lista;
        }
        public Cls_Ent_Solicitud_Pago UpdAsignarPlanilla(Cls_Ent_Solicitud_Pago entidad)
        {
            return Cls_Rule_Solicitudes_Coordinador.UpdAsignarPlanilla(entidad);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}