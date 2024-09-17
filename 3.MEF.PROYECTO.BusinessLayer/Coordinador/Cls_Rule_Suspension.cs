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

    public class Cls_Rule_Suspension
    {
        private static Cls_Dat_Suspension ODatos = new Cls_Dat_Suspension();
        public static Cls_Ent_Reevaluacion InsReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            return ODatos.InsReevaluacionSuspension(entidad);
        }
        public static List<Cls_Ent_Reevaluacion> ListaReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            return ODatos.ListaReevaluacionSuspension(entidad);
        }
        public static Cls_Ent_Descanso UpdArchivoSuspension(Cls_Ent_Descanso entidad)
        {
            return ODatos.UpdArchivoSuspension(entidad);
        }
        public static Cls_Ent_Descanso UpdEnvioSuspension(Cls_Ent_Descanso entidad)
        {
            return ODatos.UpdEnvioSuspension(entidad);
        }
    }
}
