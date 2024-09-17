using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.Entity.Administracion;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes.Repositorio
{
    public class ExpedienteDigitalRepositorio: IDisposable
    {
        public List<Cls_Ent_Documentos_Contrato> ListaDocumentosContrato(Cls_Ent_Documentos_Contrato entidad)
        {
            List<Cls_Ent_Documentos_Contrato> lista = null;
            lista = Cls_Rule_Solicitudes_Coordinador.ListaDocumentosContrato(entidad);
            return lista;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}