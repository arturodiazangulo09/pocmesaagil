using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Personal;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio
{
    public class SolicitudDesancasoRepositorio: IDisposable
    {
        public Cls_Ent_Descanso MentenimientoSolicitud_Suspension(Cls_Ent_Descanso entidad)
        {
            return Cls_Rule_Descanso.MentenimientoSolicitud_Suspension(entidad);
        }
        public List<Cls_Ent_Descanso> ListaDetalle_contrato(Cls_Ent_Descanso entidad)
        {
            List<Cls_Ent_Descanso> lista = null;
            lista = Cls_Rule_Descanso.ListaDetalle_contrato(entidad);
            return lista;
        }
        public List<Cls_Ent_Descanso> ListaSolicitud_Suspension(Cls_Ent_Descanso entidad)
        {
            List<Cls_Ent_Descanso> lista = null;
            lista = Cls_Rule_Descanso.ListaSolicitud_Suspension(entidad);
            return lista;
        }
        public Cls_Ent_Descanso UpdEstado_Suspension(Cls_Ent_Descanso entidad)
        {
            return Cls_Rule_Descanso.UpdEstado_Suspension(entidad);
        }
        public List<Cls_Ent_Contrato> ListaContratos(Cls_Ent_Contrato entidad)
        {
            List<Cls_Ent_Contrato> lista = null;
            lista = Cls_Rule_Adenda.ListaContratos(entidad);
            return lista;
        }
        public List<Cls_Ent_Reevaluacion> ListaReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            lista = Cls_Rule_Suspension.ListaReevaluacionSuspension(entidad);
            return lista;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}