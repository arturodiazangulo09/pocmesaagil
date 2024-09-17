using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Repositorio
{
    public class CoordinadorRepositorio : IDisposable
    {
        public List<Cls_Ent_Coordinador> ListaCoordinadores(Cls_Ent_Coordinador entidad)
        {
            List<Cls_Ent_Coordinador> lista = null;
            lista = Cls_Rule_Coordinador.ListaCoordinadores(entidad);
            return lista;
        }
        public Cls_Ent_Coordinador MantenimientoAccionesCoordinador(Cls_Ent_Coordinador entidad)
        {
            return Cls_Rule_Coordinador.MantenimientoAccionesCoordinador(entidad);
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