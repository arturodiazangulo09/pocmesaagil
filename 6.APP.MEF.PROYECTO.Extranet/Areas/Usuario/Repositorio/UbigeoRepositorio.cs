using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.Entity.Administracion;


namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio
{
    public class UbigeoRepositorio : IDisposable
    {
        public List<Cls_Ent_Ubigeo> ListaDepartamento()
        {
            List<Cls_Ent_Ubigeo> lista = null;
            lista = Cls_Rule_Ubigeo.ListaDepartamento();
            return lista;
        }
        public List<Cls_Ent_Ubigeo> listaProvincias(Cls_Ent_Ubigeo entidad)
        {
            List<Cls_Ent_Ubigeo> lista = null;
            lista = Cls_Rule_Ubigeo.listaProvincias(entidad);
            return lista;
        }
        public List<Cls_Ent_Ubigeo> listaDistritos(Cls_Ent_Ubigeo entidad)
        {
            List<Cls_Ent_Ubigeo> lista = null;
            lista = Cls_Rule_Ubigeo.listaDistritos(entidad);
            return lista;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}