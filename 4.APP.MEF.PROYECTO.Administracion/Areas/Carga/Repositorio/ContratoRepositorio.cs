using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.BusinessLayer.CargaMasiva;
using MEF.PROYECTO.BusinessLayer.Contratos;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.CargaMasiva;
using MEF.PROYECTO.Entity.Contratos;
using MEF.PROYECTO.Utilitario;
using static OfficeOpenXml.ExcelErrorValue;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Carga.Repositorio
{
    public class ContratoRepositorio : IDisposable
    {
        
        public Respuesta<Cls_Ent_Contratos> EditarContrato(int idContrato, Cls_Ent_Contratos contrato)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
            return service.EditarContrato(idContrato, contrato);
        }
        public Respuesta<bool> EliminarContrato(int idContrato)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
            return service.EliminarContrato(idContrato);
        }
        public Respuesta<Cls_Ent_Adendas> EditarAdenda(int idAdenda,Cls_Ent_Adendas adenda)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
            return service.EditarAdenda(idAdenda, adenda);
        }
        public Respuesta<bool> EliminarAdenda(int idAdenda)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
            return service.EliminarAdenda(idAdenda);
        }

        public Cls_Ent_Adendas ObtenerAdendas(int idCargaAdenda)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
            return service.ObtenerAdendas(idCargaAdenda).Data;
        }
        public Respuesta<Cls_Ent_Contratos> ObtenerContrato (int idRegistro, int idCarga)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
           return service.ObtenerContrato(idRegistro, idCarga);
        }
        public Respuesta<List<Cls_Ent_Contratos>> ListarContratos(string numDocumento, string cargo, decimal idEntidad, string campos, string valores, int pagina, int nregistros)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
            return service.ListarContratos(numDocumento, cargo, idEntidad, campos, valores, pagina, nregistros);
        }
        public Respuesta<List<Cls_Ent_Adendas>> ListaAdendas(string codContrato, string campos, string valores, int pagina, int nregistros)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
            return service.ListaAdendas(codContrato, campos, valores, pagina, nregistros);
        }
        public List<Cls_Ent_Entidades>  listarEntidades()
        {
            Cls_Rule_Contratos servicio = new Cls_Rule_Contratos();
            return  servicio.ListarEntidades();
        }

        public Respuesta<List<Cls_Ent_Pagos>>  listarPagos(string codContrato, string periodo, string numDocumento, string rucCas, decimal idEntidad, string campos, string valores, int pagina, int nregistros)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
            return service.ListaPagos(codContrato, periodo, numDocumento, rucCas, idEntidad, campos, valores, pagina, nregistros);
        }
        public Respuesta<bool> EditarPago(Cls_Ent_Pagos pago)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
            return service.EditarPago(pago);
        }
        public Respuesta<bool> EliminarPago(int idPago)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
            return service.EliminarPago(idPago);
        }

        public Respuesta<Cls_Ent_Pagos> ObtenerPagoxId(string idPago)
        {
            Cls_Rule_Contratos service = new Cls_Rule_Contratos();
            return service.ObtenerPagoxId(idPago);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}