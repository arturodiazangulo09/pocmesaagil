using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.Entity.Administracion;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Repositorio
{
    public class PuestoRepositorio: IDisposable
    {
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
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}