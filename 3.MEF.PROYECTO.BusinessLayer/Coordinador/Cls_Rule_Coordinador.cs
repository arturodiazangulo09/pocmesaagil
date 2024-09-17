using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;
namespace MEF.PROYECTO.BusinessLayer.Coordinador
{
    public class Cls_Rule_Coordinador
    {
        private static Cls_Dat_Coordinador ODatos = new Cls_Dat_Coordinador();
        public static Cls_Ent_Coordinador InsSolicitudCoordinador(Cls_Ent_Coordinador entidad)
        {
            return ODatos.InsSolicitudCoordinador(entidad);
        }
        public static List<Cls_Ent_Coordinador> ListaCoordinadores(Cls_Ent_Coordinador entidad)
        {
            return ODatos.ListaCoordinadores(entidad);
        }
        public static Cls_Ent_Coordinador MantenimientoAccionesCoordinador(Cls_Ent_Coordinador entidad)
        {
            return ODatos.MantenimientoAccionesCoordinador(entidad);
        }
        public static Cls_Ent_Coordinador UpdateContrasenaCoordinador(Cls_Ent_Coordinador entidad)
        {
            return ODatos.UpdateContrasenaCoordinador(entidad);
        }
        public static Cls_Ent_Coordinador UpdateRecuperarClave(Cls_Ent_Coordinador entidad)
        {
            return ODatos.UpdateRecuperarClave(entidad);
        }
    }
}
