using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using MEF.PROYECTO.BusinessLayer.Personal;
using MEF.PROYECTO.Entity.Coordinador;

namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio
{
    public class SolicitudPagoRepositorio: IDisposable
    {
        public List<Cls_Ent_Renumeracion> ListaPeriodoPagoSolicitud(Cls_Ent_Renumeracion entidad)
        {
            List<Cls_Ent_Renumeracion> lista = null;
            lista = Cls_Rule_Soliciudes.ListaPeriodoPagoSolicitud(entidad);
            return lista;
        }
        public Cls_Ent_Renumeracion UpdateReciboHonorario(Cls_Ent_Renumeracion entidad)
        {
            return CLs_Rule_Personal.UpdateReciboHonorario(entidad);
        }
        public Cls_Ent_Renumeracion UpdateReciboEstado(Cls_Ent_Renumeracion entidad)
        {
            return CLs_Rule_Personal.UpdateReciboEstado(entidad);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}