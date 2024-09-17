using MEF.PROYECTO.Data.Contratos;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.Contratos;
using MEF.PROYECTO.Utilitario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.PROYECTO.BusinessLayer.Contratos
{
    public class Cls_Rule_Contratos
    {
        private static Cls_Dat_Contratos oDatos = new Cls_Dat_Contratos();

        #region Carga

        public Respuesta<List<Cls_Ent_Carga>> ListaCarga(string campos, string valores, int pagina, int nregistros) => oDatos.ListaCarga(campos, valores, pagina, nregistros);

        #endregion

        #region Contratos

        public Respuesta<List<Cls_Ent_Contratos>> ListarContratos(string numDocumento, string cargo, decimal idEntidad, string campos, string valores, int pagina, int nregistros) => oDatos.ListaContratos(numDocumento, cargo, idEntidad, campos, valores, pagina, nregistros);
        public Respuesta<Cls_Ent_Contratos> ObtenerContrato(int idRegistro, int idCarga) => oDatos.ObtenerContrato(idRegistro, idCarga);
        public Respuesta<Cls_Ent_Contratos> EditarContrato(int idContrato, Cls_Ent_Contratos contrato) => oDatos.EditarContrato(idContrato, contrato);
        public Respuesta<bool> EliminarContrato(int idContrato) => oDatos.EliminarContrato(idContrato);

        #endregion

        #region Adendas

        public Respuesta<List<Cls_Ent_Adendas>> ListaAdendas(string codContrato, string campos, string valores, int pagina, int nregistros) => oDatos.ListaAdendas(codContrato, campos, valores, pagina, nregistros);
        public Respuesta<Cls_Ent_Adendas> ObtenerAdendas(int idCargaAdenda) => oDatos.ObtenerAdendas(idCargaAdenda);
        public Respuesta<Cls_Ent_Adendas> EditarAdenda(int idAdenda, Cls_Ent_Adendas adenda) => oDatos.EditarAdendas(idAdenda, adenda);
        public Respuesta<bool> EliminarAdenda(int idAdenda) => oDatos.EliminarAdendas(idAdenda);

        #endregion
        #region Pagos
        public Respuesta<List<Cls_Ent_Pagos>> ListaPagos(string codContrato, string periodo, string numDocumento, string rucCas, decimal idEntidad, string campos, string valores, int pagina, int nregistros) => oDatos.ListaPagos(codContrato, periodo, numDocumento, rucCas, idEntidad, campos, valores, pagina, nregistros);
        public Respuesta<bool> EditarPago(Cls_Ent_Pagos pago) => oDatos.EditarPago(pago);
        public Respuesta<bool> EliminarPago(int idPago) => oDatos.EliminarPago(idPago);
        public Respuesta<Cls_Ent_Pagos> ObtenerPagoxId(string idPago) => oDatos.ObtenerPagoxId(idPago);
        public List<Cls_Ent_Entidades> ListarEntidades() => oDatos.ListarEntidades();
        #endregion
    }
}
