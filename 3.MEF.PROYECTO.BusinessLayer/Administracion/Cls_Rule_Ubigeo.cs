using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Administracion;
using MEF.PROYECTO.Entity.Administracion;
namespace MEF.PROYECTO.BusinessLayer.Administracion
{
    public class Cls_Rule_Ubigeo
    {
        private static Cls_Dat_Ubigeo ODatos = new Cls_Dat_Ubigeo();
        public static List<Cls_Ent_Ubigeo> ListaDepartamento()
        {
            return ODatos.ListaDepartamento();
        }
        public static List<Cls_Ent_Ubigeo> listaProvincias(Cls_Ent_Ubigeo entidad)
        {
            return ODatos.listaProvincias(entidad);
        }
        public static List<Cls_Ent_Ubigeo> listaDistritos(Cls_Ent_Ubigeo entidad)
        {
            return ODatos.listaDistritos(entidad);
        }
        public static List<Cls_Ent_Ubigeo> Carga_listaProvincias(Cls_Ent_Ubigeo entidad)
        {
            return ODatos.CargaContratos_listaProvincias(entidad);
        }
        public static List<Cls_Ent_Ubigeo> Carga_listaDistritos(Cls_Ent_Ubigeo entidad)
        {
            return ODatos.Carga_Contratos_listaDistritos(entidad);
        }
    }
}
