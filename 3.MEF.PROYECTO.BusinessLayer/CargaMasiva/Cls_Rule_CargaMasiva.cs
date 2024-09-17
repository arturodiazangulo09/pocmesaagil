using MEF.PROYECTO.Data.CargaMasiva;
using MEF.PROYECTO.Entity.CargaMasiva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MEF.PROYECTO.Utilitario;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Administracion;

namespace MEF.PROYECTO.BusinessLayer.CargaMasiva
{
    public class Cls_Rule_CargaMasiva
    {
        private static Cls_Dat_CargaMasiva oDatos = new Cls_Dat_CargaMasiva();

        public Cls_Ent_Carga MantenimientoCarga(Cls_Ent_Carga carga) => oDatos.MantenimientoCarga(carga);
        public Respuesta MantenimientoCargaCabecera(DataTable carga) => oDatos.MantenimientoCargaCabecera_Bulk(carga);
        public Respuesta MantenimientoCargaDetalle(DataTable carga) => oDatos.MantenimientoCargaAdendas_Bulk(carga);
        public Respuesta MantenimientoPago(DataTable carga) => oDatos.MantenimientoPago_Bulk(carga);

        public Respuesta ActualizarCargaCabecera(Cls_Ent_Carga_Cabecera carga) => oDatos.ActualizarCargaCabecera(carga);
        public Respuesta ActualizarCargaDetalle(Cls_Ent_Carga_Detalle carga) => oDatos.ActualizarCargaDetalle(carga);

        public List<Cls_Ent_Carga> ListaCarga(String usuario)
        {
            return oDatos.ListaCarga(usuario);
        }
        public List<Cls_Ent_Carga_Cabecera> ListaCabeceraContratos(Cls_Ent_Carga entidad)
        {
            return oDatos.ListaCabeceraContratos(entidad);
        }
        public List<Cls_Ent_Carga> ListaCargaHistorica(String usuario)
        {
            return oDatos.ListaCargaHistorica(usuario);

           
        }
        public Cls_Ent_Carga_Cabecera ObtenerCabeceraContratos(int IdRegistro)
        {
            return oDatos.ObtenerCabeceraContratos(IdRegistro);
        }
        public List<Cls_Ent_Carga_Detalle> ListaContratosAdendas(Cls_Ent_Carga entidad)
        {
            return oDatos.ListaContratosAdendas(entidad);
        }

        public List<Cls_Ent_Carga_Pago> ListaCabeceraPagos(Cls_Ent_Carga entidad)
        {
            return oDatos.ListaCabeceraPagos(entidad);
        }
        public List<Cls_Ent_Entidades> ListarEntidades() => oDatos.ListarEntidades();
        public List<Cls_Ent_Ubigeo> ListarUbigeo() => oDatos.ListarUbigeo();


        public bool EliminarCarga(int idCarga, string usuario) => oDatos.EliminarCarga(idCarga, usuario);

        public bool EliminarContrato(int idRegistro) => oDatos.EliminarContrato(idRegistro);
        public List<Cls_Ent_Carga> ListaCargaCompleta(int idCarga) => oDatos.ListaCargaCompleta(idCarga);
        public bool ProcesarCarga(int idCarga, string username) => oDatos.ProcesarCarga(idCarga, username);

        public List<Cls_Ent_Carga_Detalle> ListaAdendasxContrato(int IdRegistro)
        {
            return oDatos.ListaAdendasxContrato(IdRegistro);
        }

        public Cls_Ent_Carga_Detalle ObtenerCargaDetallexId(string idCargaDetalle) => oDatos.ObtenerCargaDetallexId(idCargaDetalle);
        public bool EditarCargaDetalle(Cls_Ent_Carga_Detalle detalle) => oDatos.EditarCargaDetalle(detalle);
        public bool EliminarCargaDetalle(string idCargaDetalle) => oDatos.EliminarCargaDetalle(idCargaDetalle);

        public Cls_Ent_Carga_Pago ObtenerCargaSalarioxId(string idCargaSalario) => oDatos.ObtenerCargaSalarioxId(idCargaSalario);
        public bool EditarCargaSalario(Cls_Ent_Carga_Pago salario) => oDatos.EditarCargaSalario(salario);
        public bool EliminarCargaSalario(string idCargaSalario) => oDatos.EliminarCargaSalario(idCargaSalario);
    }
}
