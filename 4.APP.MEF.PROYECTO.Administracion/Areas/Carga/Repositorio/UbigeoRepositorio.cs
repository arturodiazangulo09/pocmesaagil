using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.BusinessLayer.CargaMasiva;
using MEF.PROYECTO.Entity.Administracion;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Carga.Repositorio
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

        public List<Cls_Ent_Ubigeo> Carga_listaProvincias(Cls_Ent_Ubigeo entidad)
        {
            List<Cls_Ent_Ubigeo> lista = null;
            lista = Cls_Rule_Ubigeo.Carga_listaProvincias(entidad);
            return lista;
        }
        public List<Cls_Ent_Ubigeo> Carga_listaDistritos(Cls_Ent_Ubigeo entidad)
        {
            List<Cls_Ent_Ubigeo> lista = null;
            lista = Cls_Rule_Ubigeo.Carga_listaDistritos(entidad);
            return lista;
        }

        public List<Cls_Ent_Ubigeo> ListarUbigeo()
        {
            Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
            return  service.ListarUbigeo();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}