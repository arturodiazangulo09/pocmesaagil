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
    public class SolicitudCovidRepositorio: IDisposable
    {
        public Cls_Ent_Covid MentenimientoSolicitud_Covid(Cls_Ent_Covid entidad)
        {
            return Cls_Rule_Covid.MentenimientoSolicitud_Covid(entidad);
        }
        public List<Cls_Ent_Covid> ListaSolicitud_Covid(Cls_Ent_Covid entidad)
        {
            List<Cls_Ent_Covid> lista = null;
            lista = Cls_Rule_Covid.ListaSolicitud_Covid(entidad);
            return lista;
        }
        public Cls_Ent_Covid UpdEstado_Covid(Cls_Ent_Covid entidad)
        {
            return Cls_Rule_Covid.UpdEstado_Covid(entidad);
        }
        public List<Cls_Ent_Reevaluacion> ListaReevaluacionCovid(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            lista = Cls_Rule_Solicitud_Covid.ListaReevaluacionCovid(entidad);
            return lista;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}