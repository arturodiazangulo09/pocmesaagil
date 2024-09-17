using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Administracion;
using MEF.PROYECTO.Entity.Administracion;
namespace MEF.PROYECTO.BusinessLayer.Administracion
{
    public class Cls_Rule_Entidades
    {
        private static Cls_Dat_Entidades ODatos = new Cls_Dat_Entidades();
        public static List<Cls_Ent_Entidades> ListaEntidades(Cls_Ent_Entidades entidad)
        {
            return ODatos.ListaEntidades(entidad);
        }

        public static List<Cls_Datos_Mef> ListaGenerales()
        {
            return ODatos.ListaGenerales();
        }

        
        public static List<Cls_Ent_Entidades> ListaEntidadesEvaluador(Cls_Ent_Entidades entidad)
        {
            return ODatos.ListaEntidadesEvaluador(entidad);
        }
        public static List<Cls_Ent_Entidades> ListaEntidadesConsultor(Cls_Ent_Entidades entidad)
        {
            return ODatos.ListaEntidadesConsultor(entidad);
        }
        public static Cls_Ent_Entidades MantenimientoEntidades(Cls_Ent_Entidades entidad)
        {
            return ODatos.MantenimientoEntidades(entidad);
        }
        public static Cls_Datos_Mef MantenimientoGenerales(Cls_Datos_Mef entidad)
        {
            return ODatos.MantenimientoGenerales(entidad);
        }
        
        public static Cls_Ent_Entidades MantenimientoPeriodoEntidad(Cls_Ent_Entidades entidad)
        {
            return ODatos.MantenimientoPeriodoEntidad(entidad);
        }
        public static List<Cls_Ent_Entidades> ListaPeriodoEntidades()
        {
            return ODatos.ListaPeriodoEntidades();
        }
        public static List<Cls_Ent_Entidades> ListaPeriodoDetalleEntidades()
        {
            return ODatos.ListaPeriodoDetalleEntidades();
        }
        public static Cls_Ent_Entidades UpdateMensualPeriodoEntidad(Cls_Ent_Entidades entidad)
        {
            return ODatos.UpdateMensualPeriodoEntidad(entidad);
        }

        public static Cls_Ent_Entidades UpdateDatosEntidad(Cls_Ent_Entidades entidad)
        {
            return ODatos.UpdateDatosEntidad(entidad);
        }
        public static Cls_Datos_Mef UpdateDatosMef(Cls_Datos_Mef entidad)
        {
            return ODatos.UpdateDatosMef(entidad);
        }
        public static List<Cls_Datos_Mef> ListaDatosMef()
        {
            return ODatos.ListaDatosMef();
        }
        public static Cls_Ent_Evaluador MantenimientoEvaluador(Cls_Ent_Evaluador entidad)
        {
            return ODatos.MantenimientoEvaluador(entidad);
        }
        public static List<Cls_Ent_Evaluador> ListaEvaluador(Cls_Ent_Evaluador entidad)
        {
            return ODatos.ListaEvaluador(entidad);
        }
        public static Cls_Ent_Entidades UDP_EvaluadorEntidad(Cls_Ent_Entidades entidad)
        {
            return ODatos.UDP_EvaluadorEntidad(entidad);
        }
        ////////PDT INICIO
        public static List<Cls_Ent_Planilla_PDT> ListaPlanillaPDT(Cls_Ent_Planilla_PDT entidad)
        {
            return ODatos.ListaPlanillaPDT(entidad);
        }

        public static List<Cls_Ent_Planilla_PDT> ListaPlanillaGeneral(Cls_Ent_Planilla_PDT entidad)
        {
            return ODatos.ListaPlanillaGeneral(entidad);
        }
        
        ///      ///       ////////PDT FIN
    }
}
