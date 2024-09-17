using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Coordinador;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Entity.Coordinador;

namespace MEF.PROYECTO.BusinessLayer.Coordinador
{
   public  class Cls_Rule_Personal_Cv
    {
        private static Cls_Dat_Personal_Cv ODatos = new Cls_Dat_Personal_Cv();
        public static List<Cls_Ent_Estudios> ListaEstudios(Cls_Ent_Estudios entidad)
        {
            return ODatos.ListaEstudios(entidad);
        }
        public static Cls_Ent_Estudios MentenimientoEstudios_Verificar(Cls_Ent_Estudios entidad)
        {
            return ODatos.MentenimientoEstudios_Verificar(entidad);
        }
        public static List<Cls_Ent_Especializacion> ListaEspecializacion(Cls_Ent_Especializacion entidad)
        {
            return ODatos.ListaEspecializacion(entidad);
        }
        public static Cls_Ent_Especializacion MentenimientoEstudios_Especializacion(Cls_Ent_Especializacion entidad)
        {
            return ODatos.MentenimientoEstudios_Especializacion(entidad);
        }
        public static List<Cls_Ent_Capacitacion> ListaCapacitacion(Cls_Ent_Capacitacion entidad)
        {
            return ODatos.ListaCapacitacion(entidad);
        }
        public static Cls_Ent_Capacitacion MentenimientoEstudios_Capacitacion(Cls_Ent_Capacitacion entidad)
        {
            return ODatos.MentenimientoEstudios_Capacitacion(entidad);
        }
        public static List<Cls_Ent_Experiencia_Laboral> ListaExperiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            return ODatos.ListaExperiencia(entidad);
        }
        public static Cls_Ent_Experiencia_Laboral MentenimientoEstudios_Experiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            return ODatos.MentenimientoEstudios_Experiencia(entidad);
        }
        public static Cls_Ent_Solicitud UpdAplicaRegistoCv(Cls_Ent_Solicitud entidad)
        {
            return ODatos.UpdAplicaRegistoCv(entidad);
        }
        public static Cls_Ent_Solicitud UpdCalificacionRequisitos(Cls_Ent_Solicitud entidad)
        {
            return ODatos.UpdCalificacionRequisitos(entidad);
        }
    }
}
