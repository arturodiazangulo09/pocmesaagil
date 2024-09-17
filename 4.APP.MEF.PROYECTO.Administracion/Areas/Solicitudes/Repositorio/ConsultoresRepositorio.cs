using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Personal;
using MEF.PROYECTO.Entity.Personal;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes.Repositorio
{
    public class ConsultoresRepositorio: IDisposable
    {
        public List<Cls_Ent_Personal> ListaPersonalAdministrador(Cls_Ent_Personal entidad)
        {
            List<Cls_Ent_Personal> lista = null;
            lista = CLs_Rule_Personal.ListaPersonalAdministrador(entidad);
            return lista;
        }
        public Cls_Ent_Personal MentenimientoInformacion_Personal(Cls_Ent_Personal entidad)
        {
            return CLs_Rule_Personal.MentenimientoInformacion_Personal(entidad);
        }
        public List<Cls_Ent_Personal> ListaInformacionPersonal(Cls_Ent_Personal entidad)
        {
            List<Cls_Ent_Personal> lista = null;
            lista = CLs_Rule_Personal.ListaInformacionPersonal(entidad);
            return lista;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}