using System;
using System.Collections.Generic;
using System.Data;
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
using MEF.PROYECTO.Utilitario;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Carga.Repositorio
{
    public class CargaRepositorio : IDisposable
    {
        public List<Cls_Ent_Carga> ListaCarga(string user)
        {
            Cls_Rule_CargaMasiva servicio = new Cls_Rule_CargaMasiva();
            List<Cls_Ent_Carga>  lista = servicio.ListaCarga(user);
            return lista;
        }

        public List<Cls_Ent_Carga> ListaCargaCompleta(int idCarga)
        {
            Cls_Rule_CargaMasiva servicio = new Cls_Rule_CargaMasiva();
            List<Cls_Ent_Carga> lista = servicio.ListaCargaCompleta(idCarga);
            return lista;
        }

        public bool EliminarCarga(int idCarga, string Usuario)
        {
            Cls_Rule_CargaMasiva servicio = new Cls_Rule_CargaMasiva();
            return servicio.EliminarCarga( idCarga, Usuario);
        }

      

        public bool ProcesarCarga(int idCarga, string Usuario)
        {
            Cls_Rule_CargaMasiva servicio = new Cls_Rule_CargaMasiva();
            return servicio.ProcesarCarga(idCarga, Usuario);
        }
        public List<Cls_Ent_Carga_Detalle> ListaAdendasxContrato(int IdRegistro)
        {
            Cls_Rule_CargaMasiva servicio = new Cls_Rule_CargaMasiva();
           return  servicio.ListaAdendasxContrato(IdRegistro);
        }
       



        public bool EliminarContrato(int idRegistro)
        {
            Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
            return service.EliminarContrato(idRegistro);
        }
        public List<Cls_Ent_Carga_Pago> ListaContratosPago(Cls_Ent_Carga entidad)
        {
            List<Cls_Ent_Carga_Pago> lista;
            Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
            lista = service.ListaCabeceraPagos(entidad);
            return lista;
        }
        public List<Cls_Ent_Carga_Detalle> ListaContratosDet(Cls_Ent_Carga entidad)
        {
            List<Cls_Ent_Carga_Detalle> listaAdendas;
            Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
            listaAdendas = service.ListaContratosAdendas(entidad);
            return listaAdendas;
        }

        public List<Cls_Ent_Carga_Cabecera> ListaContratosCab(Cls_Ent_Carga entidad)
        {
            List<Cls_Ent_Carga_Cabecera> lista;
            Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
            lista = service.ListaCabeceraContratos(entidad);
            return lista;
        }

        public Respuesta ActualizarCargaCabecera(Cls_Ent_Carga_Cabecera contrato)
        {
            Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
            return service.ActualizarCargaCabecera(contrato);
 
        }

        public List<Cls_Ent_Entidades> ListarEntidades()
        {
            Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
            List<Cls_Ent_Entidades> listaEntidades = service.ListarEntidades();
            return listaEntidades;
        }

        public Respuesta MantenimientoCargaDetalle(DataTable lista)
        {
            Cls_Rule_CargaMasiva service = new Cls_Rule_CargaMasiva();
            return service.MantenimientoCargaDetalle(lista);
        }
            



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}