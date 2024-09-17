using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Personal;

namespace MEF.PROYECTO.BusinessLayer.Coordinador
{
    public class Cls_Rule_Solicitud_Covid
    {
        private static Cls_Dat_Covid ODatos = new Cls_Dat_Covid();
        public static Cls_Ent_Reevaluacion InsReevaluacionCovid(Cls_Ent_Reevaluacion entidad)
        {
            return ODatos.InsReevaluacionCovid(entidad);
        }
        public static List<Cls_Ent_Reevaluacion> ListaReevaluacionCovid(Cls_Ent_Reevaluacion entidad)
        {
            return ODatos.ListaReevaluacionCovid(entidad);
        }
        public static Cls_Ent_Covid UpdArchivoCovid(Cls_Ent_Covid entidad)
        {
            return ODatos.UpdArchivoCovid(entidad);
        }
        public static Cls_Ent_Covid UpdEnvioCovid(Cls_Ent_Covid entidad)
        {
            return ODatos.UpdEnvioCovid(entidad);
        }
    }
}
