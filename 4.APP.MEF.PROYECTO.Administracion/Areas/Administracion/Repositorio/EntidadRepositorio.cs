using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.BusinessLayer.Coordinador;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Repositorio
{
    public class EntidadRepositorio : IDisposable
    {
        public List<Cls_Ent_Entidades> ListaEntidades(Cls_Ent_Entidades entidad)
        {
            List<Cls_Ent_Entidades> lista = null;
            lista = Cls_Rule_Entidades.ListaEntidades(entidad);
            return lista;
        }
        public List<Cls_Datos_Mef> ListaGenerales()
        {
            List<Cls_Datos_Mef> lista = null;
            lista = Cls_Rule_Entidades.ListaGenerales();
            return lista;
        }

        
        public  Cls_Ent_Entidades MantenimientoEntidades(Cls_Ent_Entidades entidad)
        {
            return Cls_Rule_Entidades.MantenimientoEntidades(entidad);
        }
        public Cls_Datos_Mef MantenimientoGenerales(Cls_Datos_Mef entidad)
        {
            return Cls_Rule_Entidades.MantenimientoGenerales(entidad);
        }

        
        public Cls_Ent_Entidades MantenimientoPeriodoEntidad(Cls_Ent_Entidades entidad)
        {
            return Cls_Rule_Entidades.MantenimientoPeriodoEntidad(entidad);
        }
        public List<Cls_Ent_Entidades> ListaPeriodoEntidades()
        {
            List<Cls_Ent_Entidades> lista = null;
            lista = Cls_Rule_Entidades.ListaPeriodoEntidades();
            return lista;
        }
        public List<Cls_Ent_Entidades> ListaPeriodoDetalleEntidades()
        {
            List<Cls_Ent_Entidades> lista = null;
            lista = Cls_Rule_Entidades.ListaPeriodoDetalleEntidades();
            return lista;
        }
        public Cls_Ent_Entidades UpdateMensualPeriodoEntidad(Cls_Ent_Entidades entidad)
        {
            return Cls_Rule_Entidades.UpdateMensualPeriodoEntidad(entidad);
        }
        public Cls_Datos_Mef UpdateDatosMef(Cls_Datos_Mef entidad)
        {
            return Cls_Rule_Entidades.UpdateDatosMef(entidad);
        }
        public List<Cls_Datos_Mef> ListaDatosMef()
        {
            List<Cls_Datos_Mef> lista = null;
            lista = Cls_Rule_Entidades.ListaDatosMef();
            return lista;
        }
        public Cls_Ent_Evaluador MantenimientoEvaluador(Cls_Ent_Evaluador entidad)
        {
            return Cls_Rule_Entidades.MantenimientoEvaluador(entidad);
        }
        public List<Cls_Ent_Evaluador> ListaEvaluador(Cls_Ent_Evaluador entidad)
        {
            List<Cls_Ent_Evaluador> lista = null;
            lista = Cls_Rule_Entidades.ListaEvaluador(entidad);
            return lista;
        }
        public Cls_Ent_Entidades UDP_EvaluadorEntidad(Cls_Ent_Entidades entidad)
        {
            return Cls_Rule_Entidades.UDP_EvaluadorEntidad(entidad);
        }
        public Cls_Ent_Archivo InsertAchivoSustento(Cls_Ent_Archivo entidad)
        {
            return Cls_Rule_Achivo.InsertAchivoSustento(entidad);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}