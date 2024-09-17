using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;
namespace MEF.PROYECTO.BusinessLayer.Coordinador
{
    public class Cls_Rule_Adenda
    {
        private static Cls_Dat_Adenda ODatos = new Cls_Dat_Adenda();
        public static List<Cls_Ent_Contrato> ListaContratos(Cls_Ent_Contrato entidad)
        {
            return ODatos.ListaContratos(entidad);
        }

        public static List<Cls_Ent_Contrato_Ren> ListaContratosRenovar(int entidad)
        {
            return ODatos.ListaContratosRenovar(entidad);
        }

        public static Cls_Ent_Adenda MantenimientoSolicitudAdenda(Cls_Ent_Adenda entidad)
        {
            return ODatos.MantenimientoSolicitudAdenda(entidad);
        }
        public static List<Cls_Ent_Adenda> ListaDetalleContratos(Cls_Ent_Adenda entidad)
        {
            return ODatos.ListaDetalleContratos(entidad);
        }
        public static List<Cls_Ent_Adenda> ListaDetalleAdendas(Cls_Ent_Adenda entidad)
        {
            return ODatos.ListaDetalleAdendas(entidad);
        }
        public static Cls_Ent_Adenda UpdArchivoAdenda(Cls_Ent_Adenda entidad)
        {
            return ODatos.UpdArchivoAdenda(entidad);
        }
        public static Cls_Ent_Adenda UpdEstadoAdenda(Cls_Ent_Adenda entidad)
        {
            return ODatos.UpdEstadoAdenda(entidad);
        }
        public static Cls_Ent_Reevaluacion InsReevaluacionAdenda(Cls_Ent_Reevaluacion entidad)
        {
            return ODatos.InsReevaluacionAdenda(entidad);
        }
        public static List<Cls_Ent_Reevaluacion> ListaReevaluacionAdenda(Cls_Ent_Reevaluacion entidad)
        {
            return ODatos.ListaReevaluacionAdenda(entidad);
        }
    }
}
