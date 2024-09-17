using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio
{
    public class AdendaRepositorio: IDisposable
    {
        public List<Cls_Ent_Contrato> ListaContratos(Cls_Ent_Contrato entidad)
        {
            List<Cls_Ent_Contrato> lista = null;
            lista = Cls_Rule_Adenda.ListaContratos(entidad);
            return lista;
        }
        public List<Cls_Ent_Contrato_Ren> ListaContratosRenovar(int entidad)
        {
            List<Cls_Ent_Contrato_Ren> lista = null;
            lista = Cls_Rule_Adenda.ListaContratosRenovar(entidad);
            return lista;
        }
        public Cls_Ent_Adenda MantenimientoSolicitudAdenda(Cls_Ent_Adenda entidad)
        {
            return Cls_Rule_Adenda.MantenimientoSolicitudAdenda(entidad);
        }
        public List<Cls_Ent_Adenda> ListaDetalleContratos(Cls_Ent_Adenda entidad)
        {
            List<Cls_Ent_Adenda> lista = null;
            lista = Cls_Rule_Adenda.ListaDetalleContratos(entidad);
            return lista;
        }
        public List<Cls_Ent_Adenda> ListaDetalleAdendas(Cls_Ent_Adenda entidad)
        {
            List<Cls_Ent_Adenda> lista = null;
            lista = Cls_Rule_Adenda.ListaDetalleAdendas(entidad);
            return lista;
        } 
        public Cls_Ent_Adenda UpdArchivoAdenda(Cls_Ent_Adenda entidad)
        {
            return Cls_Rule_Adenda.UpdArchivoAdenda(entidad);
        }
        public Cls_Ent_Adenda UpdEstadoAdenda(Cls_Ent_Adenda entidad)
        {
            return Cls_Rule_Adenda.UpdEstadoAdenda(entidad);
        }
        public List<Cls_Ent_Reevaluacion> ListaReevaluacionAdenda(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            lista = Cls_Rule_Adenda.ListaReevaluacionAdenda(entidad);
            return lista;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}