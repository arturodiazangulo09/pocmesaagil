using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Personal;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio
{
    public class SolicitudDesancasoRepositorio: IDisposable
    {
        public List<Cls_Ent_Descanso> ListaSolicitud_Suspension(Cls_Ent_Descanso entidad)
        {
            List<Cls_Ent_Descanso> lista = null;
            lista = Cls_Rule_Descanso.ListaSolicitud_Suspension(entidad);
            return lista;
        }
        public Cls_Ent_Reevaluacion InsReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            return Cls_Rule_Suspension.InsReevaluacionSuspension(entidad);
        }
        public List<Cls_Ent_Reevaluacion> ListaReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            lista = Cls_Rule_Suspension.ListaReevaluacionSuspension(entidad);
            return lista;
        }
        public Cls_Ent_Descanso UpdArchivoSuspension(Cls_Ent_Descanso entidad)
        {
            return Cls_Rule_Suspension.UpdArchivoSuspension(entidad);
        }
        public Cls_Ent_Descanso UpdEnvioSuspension(Cls_Ent_Descanso entidad)
        {
            return Cls_Rule_Suspension.UpdEnvioSuspension(entidad);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}