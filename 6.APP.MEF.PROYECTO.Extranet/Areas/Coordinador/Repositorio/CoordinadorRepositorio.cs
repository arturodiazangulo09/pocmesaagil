using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.BusinessLayer.Administracion;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio
{
    public class CoordinadorRepositorio : IDisposable  
    {
        public List<Cls_Ent_Coordinador> ListaCoordinadores(Cls_Ent_Coordinador entidad)
        {
            List<Cls_Ent_Coordinador> lista = null;
            lista = Cls_Rule_Coordinador.ListaCoordinadores(entidad);
            return lista;
        }
        public Cls_Ent_Coordinador InsSolicitudCoordinador(Cls_Ent_Coordinador entidad)
        {
            return Cls_Rule_Coordinador.InsSolicitudCoordinador(entidad);
        }
        public Cls_Ent_Coordinador UpdateContrasenaCoordinador(Cls_Ent_Coordinador entidad)
        {
            return Cls_Rule_Coordinador.UpdateContrasenaCoordinador(entidad);
        }
        public Cls_Ent_Coordinador UpdateRecuperarClave(Cls_Ent_Coordinador entidad)
        {
            return Cls_Rule_Coordinador.UpdateRecuperarClave(entidad);
        }
        public List<Cls_Ent_Puesto> ListaPuestos(Cls_Ent_Puesto entidad)
        {
            List<Cls_Ent_Puesto> lista = null;
            lista = Cls_Rule_Puesto.ListaPuestos(entidad);
            return lista;
        }
        public Cls_Ent_Puesto MantenimientoPuestos(Cls_Ent_Puesto entidad)
        {
            return Cls_Rule_Puesto.MantenimientoPuestos(entidad);
        }

        public Cls_Ent_Entidades UpdateDatosEntidad(Cls_Ent_Entidades entidad)
        {
            return Cls_Rule_Entidades.UpdateDatosEntidad(entidad);
        }
        public List<Cls_Ent_Entidades> ListaEntidades(Cls_Ent_Entidades entidad)
        {
            List<Cls_Ent_Entidades> lista = null;
            lista = Cls_Rule_Entidades.ListaEntidades(entidad);
            return lista;
        }
        public Cls_Ent_Archivo InsertAchivoSustento(Cls_Ent_Archivo entidad)
        {
            return Cls_Rule_Achivo.InsertAchivoSustento(entidad);
        }
        public List<Cls_Ent_Archivo> ListaArchivoSustento(Cls_Ent_Archivo entidad)
        {
            List<Cls_Ent_Archivo> lista = null;
            lista = Cls_Rule_Achivo.ListaArchivoSustento(entidad);
            return lista;
        }
   
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}