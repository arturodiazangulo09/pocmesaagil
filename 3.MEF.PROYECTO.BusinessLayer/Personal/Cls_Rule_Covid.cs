using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Personal;
using MEF.PROYECTO.Entity.Personal;


namespace MEF.PROYECTO.BusinessLayer.Personal
{
    public class Cls_Rule_Covid
    {
        private static Cls_Dat_Covid ODatos = new Cls_Dat_Covid();
        public static Cls_Ent_Covid MentenimientoSolicitud_Covid(Cls_Ent_Covid entidad)
        {
            return ODatos.MentenimientoSolicitud_Covid(entidad);
        }
        public static List<Cls_Ent_Covid> ListaSolicitud_Covid(Cls_Ent_Covid entidad)
        {
            return ODatos.ListaSolicitud_Covid(entidad);
        }
        public static Cls_Ent_Covid UpdEstado_Covid(Cls_Ent_Covid entidad)
        {
            return ODatos.UpdEstado_Covid(entidad);
        }
    }
}
