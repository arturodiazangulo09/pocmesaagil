using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Personal;
using MEF.PROYECTO.Entity.Personal;



namespace MEF.PROYECTO.BusinessLayer.Personal
{
    public class Cls_Rule_Descanso
    {
        private static Cls_Dat_Descanso ODatos = new Cls_Dat_Descanso();
        public static Cls_Ent_Descanso MentenimientoSolicitud_Suspension(Cls_Ent_Descanso entidad)
        {
            return ODatos.MentenimientoSolicitud_Suspension(entidad);
        }
        public static List<Cls_Ent_Descanso> ListaDetalle_contrato(Cls_Ent_Descanso entidad)
        {
            return ODatos.ListaDetalle_contrato(entidad);
        }
        public static List<Cls_Ent_Descanso> ListaSolicitud_Suspension(Cls_Ent_Descanso entidad)
        {
            return ODatos.ListaSolicitud_Suspension(entidad);
        }
        public static Cls_Ent_Descanso UpdEstado_Suspension(Cls_Ent_Descanso entidad)
        {
            return ODatos.UpdEstado_Suspension(entidad);
        }
    }
}
