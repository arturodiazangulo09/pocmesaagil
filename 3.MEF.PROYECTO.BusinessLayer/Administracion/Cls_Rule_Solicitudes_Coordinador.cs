using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Administracion;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Administracion;
namespace MEF.PROYECTO.BusinessLayer.Administracion
{
    public class Cls_Rule_Solicitudes_Coordinador
    {
        private static Cls_Dat_Solicitudes_Coordinador ODatos = new Cls_Dat_Solicitudes_Coordinador();
        public static List<Cls_Ent_Solicitud_Personal> ListaSolicitudesCoordinador(Cls_Ent_Solicitud_Personal entidad)
        {
            return ODatos.ListaSolicitudesCoordinador(entidad);
        }
        public static List<Cls_Ent_Solicitud_Personal> ProximoCodContrato() => ODatos.ProximoCodContrato();

        public static List<Cls_Ent_Solicitud_Personal> GetDatosSolicitud(int ID_SOLICITUD)
        {
            return ODatos.GetDatosSolicitud(ID_SOLICITUD);
        }
        public static List<Cls_Ent_Solicitud_Personal> GetDatosAdenda(int ID_CONTRATO_DET)
        {
            return ODatos.GetDatosAdenda(ID_CONTRATO_DET);
        }

        
        public static Cls_Ent_Solicitud_Personal UPD_CALIFICAR_DOCUMENTOS(Cls_Ent_Solicitud_Personal entidad)
        {
            return ODatos.UPD_CALIFICAR_DOCUMENTOS(entidad);
        }
        public static Cls_Ent_Solicitud_Personal InsContratoSolicitud(Cls_Ent_Solicitud_Personal entidad)
        {
            return ODatos.InsContratoSolicitud(entidad);
        }
        /****** INICIO PROCESO SOLICITUD DE PAGO ****/
        public static List<Cls_Ent_Solicitud_Pago> ListaSolicitudPagoEntidadUTP(Cls_Ent_Solicitud_Pago entidad)
        {
            return ODatos.ListaSolicitudPagoEntidadUTP(entidad);
        }
        public static Cls_Ent_Solicitud_Pago UpdateReevaluacionPago(Cls_Ent_Solicitud_Pago entidad)
        {
            return ODatos.UpdateReevaluacionPago(entidad);
        }

        /****** FIN PROCESO SOLICITUD DE PAGO  ****/
        public static List<Cls_Ent_Lista_Contr_Aden_Consultor> ListaContraAdendaPersona(Cls_Ent_Lista_Contr_Aden_Consultor entidad)
        {
            return ODatos.ListaContraAdendaPersona(entidad);
        }
        public static Cls_Ent_Lista_Contr_Aden_Consultor UpdateBajaAnulacion(Cls_Ent_Lista_Contr_Aden_Consultor entidad)
        {
            return ODatos.UpdateBajaAnulacion(entidad);
        }
        /****** INICIO PROCESO SOLICITUD SUSPENSION****/
        public static Cls_Ent_Descanso UpdEnvioSuspensionAprueba(Cls_Ent_Descanso entidad)
        {
            return ODatos.UpdEnvioSuspensionAprueba(entidad);
        }
        /****** FIN PROCESO SOLICITUD SUSPENSION  ****/
        /****** INICIO PROCESO SOLICITUD COVID****/
        public static Cls_Ent_Covid UpdEnvioCovidAprueba(Cls_Ent_Covid entidad)
        {
            return ODatos.UpdEnvioCovidAprueba(entidad);
        }
        /****** FIN PROCESO SOLICITUD COVID  ****/
        /****** INICIO DETALLE DE DOCUMENTO ****/
        public static List<Cls_Ent_Documentos_Contrato> ListaDocumentosContrato(Cls_Ent_Documentos_Contrato entidad)
        {
            return ODatos.ListaDocumentosContrato(entidad);
        }
        /****** FIN DETALLE DE DOCUMENTO   ****/
        /****** INICIO PLANILLA DE PAGO ****/
        public static List<Cls_Ent_Solicitud_Pago> ListaPeriodoPlanilla(Cls_Ent_Solicitud_Pago entidad)
        {
            return ODatos.ListaPeriodoPlanilla(entidad);
        }
        public static Cls_Ent_Solicitud_Pago UpdAsignarPlanilla(Cls_Ent_Solicitud_Pago entidad)
        {
            return ODatos.UpdAsignarPlanilla(entidad);
        }
        /****** FIN PLANILLA DE PAGO   ****/
    }
}
