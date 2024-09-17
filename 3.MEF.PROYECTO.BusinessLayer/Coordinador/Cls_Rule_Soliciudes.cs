using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;

namespace MEF.PROYECTO.BusinessLayer.Coordinador
{
    public class Cls_Rule_Soliciudes
    {
        private static Cls_Dat_Solicitud ODatos = new Cls_Dat_Solicitud();
        public static Cls_Ent_Solicitud MantenimientoSolicitudPac(Cls_Ent_Solicitud entidad)
        {
            return ODatos.MantenimientoSolicitudPac(entidad);
        }
        public static Cls_Ent_Solicitud MantenimientoSolicitudFag(Cls_Ent_Solicitud entidad)
        {
            return ODatos.MantenimientoSolicitudFag(entidad);
        }
        public static Cls_Ent_Documento MantenimientoDOcumento(Cls_Ent_Documento entidad)
        {
            return ODatos.MantenimientoDOcumento(entidad);
        }
        public static List<Cls_Ent_Documento> ListaDocumentos(Cls_Ent_Documento entidad)
        {
            return ODatos.ListaDocumentos(entidad);
        }
        public static Cls_Ent_Solicitud Enviar_Solicitud_Participante(Cls_Ent_Solicitud entidad)
        {
            return ODatos.Enviar_Solicitud_Participante(entidad);
        }
        public static List<Cls_Ent_Solicitud> ListaSolicitudes(Cls_Ent_Solicitud entidad)
        {
            return ODatos.ListaSolicitudes(entidad);
        }

        public static List<Cls_Ent_Funciones_Solicitud> ListaServicio(Cls_Ent_Funciones_Solicitud entidad)
        {
            return ODatos.ListaServicio(entidad);
        }
        public static List<Cls_Ent_Experiencia> ListaExperiencia(Cls_Ent_Experiencia entidad)
        {
            return ODatos.ListaExperiencia(entidad);
        }
        public static List<Cls_Ent_Academico> ListaAcademica(Cls_Ent_Academico entidad)
        {
            return ODatos.ListaAcademica(entidad);
        }
        public static List<Cls_Ent_Curso> ListaCursos(Cls_Ent_Curso entidad)
        {
            return ODatos.ListaCursos(entidad);
        }
        public static List<Cls_Ent_Conocimientos> ListaConocimientos(Cls_Ent_Conocimientos entidad)
        {
            return ODatos.ListaConocimientos(entidad);
        }
        public static List<Cls_Ent_Actividad> ListaActividad(Cls_Ent_Actividad entidad)
        {
            return ODatos.ListaActividad(entidad);
        }
        public static List<Cls_Ent_Actividad_Otro> ListaActividadOtro(Cls_Ent_Actividad_Otro entidad)
        {
            return ODatos.ListaActividadOtro(entidad);
        }
        public static List<Cls_Ent_Renumeracion> ListaPeriodoPagoSolicitud(Cls_Ent_Renumeracion entidad)
        {
            return ODatos.ListaPeriodoPagoSolicitud(entidad);
        }
        public static Cls_Ent_Renumeracion ValidarRemuneracion(Cls_Ent_Renumeracion entidad)
        {
            return ODatos.ValidarRemuneracion(entidad);
        }
        public static Cls_Ent_Solicitud UpdateArchivoTdr(Cls_Ent_Solicitud entidad)
        {
            return ODatos.UpdateArchivoTdr(entidad);
        }
        public static List<Cls_Ent_Requisitos_Solicitud> ListaRequisitos(int ID_SOLICITUD)
        {
            return ODatos.ListaRequisitos(ID_SOLICITUD);
        }
        public static Cls_Ent_Solicitud UpdNotificaUTP(Cls_Ent_Solicitud entidad)
        {
            return ODatos.UpdNotificaUTP(entidad);
        }
        public static Cls_Ent_Reevaluacion InsReevaluacion(Cls_Ent_Reevaluacion entidad)
        {
            return ODatos.InsReevaluacion(entidad);
        }
        public static List<Cls_Ent_Reevaluacion> ListaReevaluacion(Cls_Ent_Reevaluacion entidad)
        {
            return ODatos.ListaReevaluacion(entidad);
        }
        public static Cls_Ent_Solicitud UpdArchivosFAG(Cls_Ent_Solicitud entidad)
        {
            return ODatos.UpdArchivosFAG(entidad);
        }
        public static Cls_Ent_Solicitud UpdArchivosPAC(Cls_Ent_Solicitud entidad)
        {
            return ODatos.UpdArchivosPAC(entidad);
        }
        public static List<Cls_Ent_Solicitud_Pago> ListaSolicitudPagoEntidad(Cls_Ent_Solicitud_Pago entidad)
        {
            return ODatos.ListaSolicitudPagoEntidad(entidad);
        }
        public static Cls_Ent_Solicitud_Pago Mnt_Conformidad_Pago(Cls_Ent_Solicitud_Pago entidad)
        {
            return ODatos.Mnt_Conformidad_Pago(entidad);
        }
        public static Cls_Ent_Solicitud_Pago Update_Conformidad_Pago(Cls_Ent_Solicitud_Pago entidad)
        {
            return ODatos.Update_Conformidad_Pago(entidad);
        }
        public static List<Cls_Ent_Archivo_STD> ListaArchivoSTD(int ID)
        {
            return ODatos.ListaArchivoSTD(ID);
        }
        /*FIN PERIODO DE PAGOS*/
        #region MANTENIMIENTO DE REQUISITOS V2 DIRECTIVA 2023
        public static Cls_Ent_Solicitud MantenimientoSolicitudFag_V2(Cls_Ent_Solicitud entidad)
        {
            return ODatos.MantenimientoSolicitudFag_V2(entidad);
        }
        #endregion
        
        #region MANTENIMIENTO DE REQUISITOS V2 DIRECTIVA CEU 2023 
        public static Cls_Ent_Solicitud MantenimientoSolicitudFagCeu_V2(Cls_Ent_Solicitud entidad)
        {
            return ODatos.MantenimientoSolicitudFagCeu_V2(entidad);
        }
        #endregion

        #region Nuevo wizard de coordinador para contratacion de consultor fag
        public static Cls_Ent_Solicitud WizGrabarRequerimientoBL(Cls_Ent_Solicitud entidad)
        {
            return ODatos.WizGrabarRequerimiento(entidad);
        }
        public static Cls_Ent_Solicitud WizGrabarRequisitoBL(Cls_Ent_Solicitud entidad)
        {
            return ODatos.WizGrabarRequisito(entidad);
        }

        public static Cls_Ent_Solicitud WizGrabarContratoBL(Cls_Ent_Solicitud entidad)
        {
            return ODatos.WizGrabarContrato(entidad);
        }
        public static Cls_Ent_Solicitud WizGrabarLocador(Cls_Ent_Solicitud entidad)
        {
            return ODatos.WizGrabarLocador(entidad);
        }

        #endregion
    }
}
