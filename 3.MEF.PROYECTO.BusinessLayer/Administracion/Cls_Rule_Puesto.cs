using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Administracion;
using MEF.PROYECTO.Entity.Administracion;
namespace MEF.PROYECTO.BusinessLayer.Administracion
{
    public class Cls_Rule_Puesto
    {
        private static Cls_Dat_Puesto ODatos = new Cls_Dat_Puesto();
        public static List<Cls_Ent_Puesto> ListaPuestos(Cls_Ent_Puesto entidad)
        {
            return ODatos.ListaPuestos(entidad);
        }
        public static Cls_Ent_Puesto MantenimientoPuestos(Cls_Ent_Puesto entidad)
        {
            return ODatos.MantenimientoPuestos(entidad);
        }
    }
}
