using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Personal;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Entity.Coordinador;

namespace MEF.PROYECTO.BusinessLayer.Personal
{
    public class CLs_Rule_Personal
    {
        private static Cls_Dat_Personal ODatos = new Cls_Dat_Personal();
        public static List<Cls_Ent_Personal> ListaPersonal(Cls_Ent_Personal entidad)
        {
            return ODatos.ListaPersonal(entidad);
        }
        public static Cls_Ent_Personal UpdateContrasenaPersonal(Cls_Ent_Personal entidad)
        {
            return ODatos.UpdateContrasenaPersonal(entidad);
        }
        public static Cls_Ent_Personal UpdateRestablecerContrasenaPersonal(Cls_Ent_Personal entidad)
        {
            return ODatos.UpdateRestablecerContrasenaPersonal(entidad);
        }
        public static List<Cls_Ent_Grado_Academico> ListaNivelGrado( )
        {
            return ODatos.ListaNivelGrado();
        }
        public static Cls_Ent_Estudios Mentenimiento_Estudios(Cls_Ent_Estudios entidad)
        {
            return ODatos.Mentenimiento_Estudios(entidad);
        }
        public static List<Cls_Ent_Nivel_Academico> ListaNivelAcademico( )
        {
            return ODatos.ListaNivelAcademico();
        }
        public static List<Cls_Ent_Estudios> ListaEstudios(Cls_Ent_Estudios entidad)
        {
            return ODatos.ListaEstudios(entidad);
        }
        public static List<Cls_Ent_Tipo_Especializacion> ListaTipoEspecializacion()
        {
            return ODatos.ListaTipoEspecializacion();
        }
        public static Cls_Ent_Especializacion Mentenimiento_Especializacion(Cls_Ent_Especializacion entidad)
        {
            return ODatos.Mentenimiento_Especializacion(entidad);
        }
        public static List<Cls_Ent_Especializacion> ListaEspecializacion(Cls_Ent_Especializacion entidad)
        {
            return ODatos.ListaEspecializacion(entidad);
        }
        public static Cls_Ent_Capacitacion Mentenimiento_Capacitacion(Cls_Ent_Capacitacion entidad)
        {
            return ODatos.Mentenimiento_Capacitacion(entidad);
        }
        public static List<Cls_Ent_Capacitacion> ListaCapacitacion(Cls_Ent_Capacitacion entidad)
        {
            return ODatos.ListaCapacitacion(entidad);
        }

        public static List<Cls_Ent_Tipo_Experiencia> ListaTipoExperiencia()
        {
            return ODatos.ListaTipoExperiencia();
        }
        public static Cls_Ent_Experiencia_Laboral Mentenimiento_Experiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            return ODatos.Mentenimiento_Experiencia(entidad);
        }
        public static List<Cls_Ent_Experiencia_Laboral> ListaExperiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            return ODatos.ListaExperiencia(entidad);
        }
        public static List<Cls_Ent_Tipo_Sector_Experiencia> ListaTipoSectorExperiencia()
        {
            return ODatos.ListaTipoSectorExperiencia();
        }
        public static List<Cls_Ent_Nacionalidad> ListaTipoNacionalidad()
        {
            return ODatos.ListaTipoNacionalidad();
        }
        public static List<Cls_Ent_Banco> ListaTipoBanco()
        {
            return ODatos.ListaTipoBanco();
        }
        public static Cls_Ent_Personal UpdatePersonal(Cls_Ent_Personal entidad)
        {
            return ODatos.UpdatePersonal(entidad);
        }
        public static List<Cls_Ent_Solicitud_Personal> ListasolicitudPersonal(Cls_Ent_Solicitud_Personal entidad)
        {
            return ODatos.ListasolicitudPersonal(entidad);
        }
        public static Cls_Ent_Solicitud_Personal UpdateDJ_Fag(Cls_Ent_Solicitud_Personal entidad)
        {
            return ODatos.UpdateDJ_Fag(entidad);
        }
        public static Cls_Ent_Solicitud_Personal UpdateDJ_Pac(Cls_Ent_Solicitud_Personal entidad)
        {
            return ODatos.UpdateDJ_Pac(entidad);
        }
        public static Cls_Ent_Solicitud_Personal UpdatePropuesta_Envio(Cls_Ent_Solicitud_Personal entidad)
        {
            return ODatos.UpdatePropuesta_Envio(entidad);
        }
        public static Cls_Ent_Renumeracion UpdateReciboHonorario(Cls_Ent_Renumeracion entidad)
        {
            return ODatos.UpdateReciboHonorario(entidad);
        }
        public static Cls_Ent_Renumeracion UpdateReciboEstado(Cls_Ent_Renumeracion entidad)
        {
            return ODatos.UpdateReciboEstado(entidad);
        }
        public static List<Cls_Ent_Personal> ListaPersonalAdministrador(Cls_Ent_Personal entidad)
        {
            return ODatos.ListaPersonalAdministrador(entidad);
        }
        public static Cls_Ent_Personal MentenimientoInformacion_Personal(Cls_Ent_Personal entidad)
        {
            return ODatos.MentenimientoInformacion_Personal(entidad);
        }
        public static List<Cls_Ent_Personal> ListaInformacionPersonal(Cls_Ent_Personal entidad)
        {
            return ODatos.ListaInformacionPersonal(entidad);
        }
    }
}
