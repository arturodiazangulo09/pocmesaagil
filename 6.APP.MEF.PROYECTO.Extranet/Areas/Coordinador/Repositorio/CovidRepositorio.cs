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
    public class CovidRepositorio: IDisposable
    {
        public List<Cls_Ent_Covid> ListaSolicitud_Covid(Cls_Ent_Covid entidad)
        {
            List<Cls_Ent_Covid> lista = null;
            lista = Cls_Rule_Covid.ListaSolicitud_Covid(entidad);
            return lista;
        }
        public Cls_Ent_Reevaluacion InsReevaluacionCovid(Cls_Ent_Reevaluacion entidad)
        {
            return Cls_Rule_Solicitud_Covid.InsReevaluacionCovid(entidad);
        }
        public List<Cls_Ent_Reevaluacion> ListaReevaluacionCovid(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            lista = Cls_Rule_Solicitud_Covid.ListaReevaluacionCovid(entidad);
            return lista;
        }
        public Cls_Ent_Covid UpdArchivoCovid(Cls_Ent_Covid entidad)
        {
            return Cls_Rule_Solicitud_Covid.UpdArchivoCovid(entidad);
        }
        public Cls_Ent_Covid UpdEnvioCovid(Cls_Ent_Covid entidad)
        {
            return Cls_Rule_Solicitud_Covid.UpdEnvioCovid(entidad);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}