using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.Entity.Administracion;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.AltasBajas.Repositorio
{
    public class PdtRepositorio: IDisposable
    {
        public List<Cls_Ent_Planilla_PDT> ListaPlanillaPDT(Cls_Ent_Planilla_PDT entidad)
        {
            List<Cls_Ent_Planilla_PDT> lista = null;
            lista = Cls_Rule_Entidades.ListaPlanillaPDT(entidad);
            return lista;
        }
        public List<Cls_Ent_Planilla_PDT> ListaPlanillaGeneral(Cls_Ent_Planilla_PDT entidad)
        {
            List<Cls_Ent_Planilla_PDT> lista = null;
            lista = Cls_Rule_Entidades.ListaPlanillaGeneral(entidad);
            return lista;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}