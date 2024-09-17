using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.BusinessLayer.Personal;
using MEF.PROYECTO.Entity.Personal;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio
{
    public class SolicitudRepositorio: IDisposable
    {
        public List<Cls_Ent_Entidades> ListaEntidades(Cls_Ent_Entidades entidad)
        {
            List<Cls_Ent_Entidades> lista = null;
            lista = Cls_Rule_Entidades.ListaEntidades(entidad);
            return lista;
        }
        public Cls_Ent_Solicitud MantenimientoSolicitudPac(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.MantenimientoSolicitudPac(entidad);
        }
        public Cls_Ent_Solicitud MantenimientoSolicitudFag(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.MantenimientoSolicitudFag(entidad);
        }
        public Cls_Ent_Documento MantenimientoDOcumento(Cls_Ent_Documento entidad)
        {
            return Cls_Rule_Soliciudes.MantenimientoDOcumento(entidad);
        }
        public List<Cls_Ent_Documento> ListaDocumentos(Cls_Ent_Documento entidad)
        {
           List<Cls_Ent_Documento> lista = null;
           lista = Cls_Rule_Soliciudes.ListaDocumentos(entidad);
            return lista;
        }
        public Cls_Ent_Solicitud Enviar_Solicitud_Participante(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.Enviar_Solicitud_Participante(entidad);
        }
        public List<Cls_Ent_Solicitud> ListaSolicitudes(Cls_Ent_Solicitud entidad)
        {
            List<Cls_Ent_Solicitud> lista = null;
            lista = Cls_Rule_Soliciudes.ListaSolicitudes(entidad);
            return lista;
        }

        public List<Cls_Ent_Funciones_Solicitud> ListaServicio(Cls_Ent_Funciones_Solicitud entidad)
        {
            List<Cls_Ent_Funciones_Solicitud> lista = null;
            lista = Cls_Rule_Soliciudes.ListaServicio(entidad);
            return lista;
        }
        public List<Cls_Ent_Experiencia> ListaExperiencia(Cls_Ent_Experiencia entidad)
        {
            List<Cls_Ent_Experiencia> lista = null;
            lista = Cls_Rule_Soliciudes.ListaExperiencia(entidad);
            return lista;
        }
        public List<Cls_Ent_Academico> ListaAcademica(Cls_Ent_Academico entidad)
        {
            List<Cls_Ent_Academico> lista = null;
            lista = Cls_Rule_Soliciudes.ListaAcademica(entidad);
            return lista;
        }
        public List<Cls_Ent_Curso> ListaCursos(Cls_Ent_Curso entidad)
        {
            List<Cls_Ent_Curso> lista = null;
            lista = Cls_Rule_Soliciudes.ListaCursos(entidad);
            return lista;
        }
        public List<Cls_Ent_Conocimientos> ListaConocimientos(Cls_Ent_Conocimientos entidad)
        {
            List<Cls_Ent_Conocimientos> lista = null;
            lista = Cls_Rule_Soliciudes.ListaConocimientos(entidad);
            return lista;
        }
        public List<Cls_Ent_Actividad> ListaActividad(Cls_Ent_Actividad entidad)
        {
            List<Cls_Ent_Actividad> lista = null;
            lista = Cls_Rule_Soliciudes.ListaActividad(entidad);
            return lista;
        }
        public List<Cls_Ent_Actividad_Otro> ListaActividadOtro(Cls_Ent_Actividad_Otro entidad)
        {
            List<Cls_Ent_Actividad_Otro> lista = null;
            lista = Cls_Rule_Soliciudes.ListaActividadOtro(entidad);
            return lista;
        }
        public List<Cls_Ent_Personal> ListaPersonal(Cls_Ent_Personal entidad)
        {
            List<Cls_Ent_Personal> lista = null;
            lista = CLs_Rule_Personal.ListaPersonal(entidad);
            return lista;
        }
        public List<Cls_Ent_Renumeracion> ListaPeriodoPagoSolicitud(Cls_Ent_Renumeracion entidad)
        {
            List<Cls_Ent_Renumeracion> lista = null;
            lista = Cls_Rule_Soliciudes.ListaPeriodoPagoSolicitud(entidad);
            return lista;
        }
        public Cls_Ent_Renumeracion ValidarRemuneracion(Cls_Ent_Renumeracion entidad)
        {
            return Cls_Rule_Soliciudes.ValidarRemuneracion(entidad);
        }
        public Cls_Ent_Solicitud UpdateArchivoTdr(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.UpdateArchivoTdr(entidad);
        }
        public List<Cls_Ent_Requisitos_Solicitud> ListaRequisitos(int ID_SOLICITUD)
        {
            List<Cls_Ent_Requisitos_Solicitud> lista = null;
            lista = Cls_Rule_Soliciudes.ListaRequisitos(ID_SOLICITUD);
            return lista;
        }
        public Cls_Ent_Solicitud UpdNotificaUTP(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.UpdNotificaUTP(entidad);
        }
        public Cls_Ent_Reevaluacion InsReevaluacion(Cls_Ent_Reevaluacion entidad)
        {
            return Cls_Rule_Soliciudes.InsReevaluacion(entidad);
        }
        public List<Cls_Ent_Reevaluacion> ListaReevaluacion(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            lista = Cls_Rule_Soliciudes.ListaReevaluacion(entidad);
            return lista;
        }
        public Cls_Ent_Solicitud UpdArchivosFAG(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.UpdArchivosFAG(entidad);
        }
        public Cls_Ent_Solicitud UpdArchivosPAC(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.UpdArchivosPAC(entidad);
        }
        public List<Cls_Ent_Solicitud_Pago> ListaSolicitudPagoEntidad(Cls_Ent_Solicitud_Pago entidad)
        {
            List<Cls_Ent_Solicitud_Pago> lista = null;
            lista = Cls_Rule_Soliciudes.ListaSolicitudPagoEntidad(entidad);
            return lista;
        }
        public List<Cls_Ent_Solicitud_Personal> ListaSolicitudesCoordinador(Cls_Ent_Solicitud_Personal entidad)
        {
            return Cls_Rule_Solicitudes_Coordinador.ListaSolicitudesCoordinador(entidad);
        }
        public List<Cls_Ent_Estudios> ListaEstudios(Cls_Ent_Estudios entidad)
        {
            return Cls_Rule_Personal_Cv.ListaEstudios(entidad);
        }
        public List<Cls_Ent_Especializacion> ListaEspecializacion(Cls_Ent_Especializacion entidad)
        {
            return Cls_Rule_Personal_Cv.ListaEspecializacion(entidad);
        }
        public List<Cls_Ent_Experiencia_Laboral> ListaExperienciaLaboral(Cls_Ent_Experiencia_Laboral entidad)
        {
            return Cls_Rule_Personal_Cv.ListaExperiencia(entidad);
        }
        public Cls_Ent_Solicitud_Pago Mnt_Conformidad_Pago(Cls_Ent_Solicitud_Pago entidad)
        {
            return Cls_Rule_Soliciudes.Mnt_Conformidad_Pago(entidad);
        }
        public Cls_Ent_Solicitud_Pago Update_Conformidad_Pago(Cls_Ent_Solicitud_Pago entidad)
        {
            return Cls_Rule_Soliciudes.Update_Conformidad_Pago(entidad);
        }
        public Cls_Ent_Solicitud_Pago UpdateReevaluacionPago(Cls_Ent_Solicitud_Pago entidad)
        {
            return Cls_Rule_Solicitudes_Coordinador.UpdateReevaluacionPago(entidad);
        }
        public List<Cls_Ent_Lista_Contr_Aden_Consultor> ListaContraAdendaPersona(Cls_Ent_Lista_Contr_Aden_Consultor entidad)
        {
            List<Cls_Ent_Lista_Contr_Aden_Consultor> lista = null;
            lista = Cls_Rule_Solicitudes_Coordinador.ListaContraAdendaPersona(entidad);
            return lista;
        }
        public Cls_Ent_Lista_Contr_Aden_Consultor UpdateBajaAnulacion(Cls_Ent_Lista_Contr_Aden_Consultor entidad)
        {
            return Cls_Rule_Solicitudes_Coordinador.UpdateBajaAnulacion(entidad);
        }

        public List<Cls_Ent_Grado_Academico> ListaNivelGrado()
        {
            List<Cls_Ent_Grado_Academico> lista = null;
            lista = CLs_Rule_Personal.ListaNivelGrado();
            return lista;
        }
        public List<Cls_Ent_Tipo_Experiencia> ListaTipoExperiencia()
        {
            List<Cls_Ent_Tipo_Experiencia> lista = null;
            lista = CLs_Rule_Personal.ListaTipoExperiencia();
            return lista;
        }
        public List<Cls_Ent_Tipo_Sector_Experiencia> ListaTipoSectorExperiencia()
        {
            List<Cls_Ent_Tipo_Sector_Experiencia> lista = null;
            lista = CLs_Rule_Personal.ListaTipoSectorExperiencia();
            return lista;
        }
        #region MANTENIMIENTO DE REQUISITOS V2 DIRECTIVA 2023
        public Cls_Ent_Solicitud MantenimientoSolicitudFag_V2(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.MantenimientoSolicitudFag_V2(entidad);
        }
        #endregion
        
        #region MANTENIMIENTO DE REQUISITOS V2 DIRECTIVA CEU 2023
        public Cls_Ent_Solicitud MantenimientoSolicitudFagCeu_V2(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.MantenimientoSolicitudFagCeu_V2(entidad);
        }
        #endregion

        #region Nuevo wizard de coordinador para contratacion de consultor fag
        public Cls_Ent_Solicitud WizGrabarRequerimientoRepo(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.WizGrabarRequerimientoBL(entidad);
        }
        public Cls_Ent_Solicitud WizGrabarRequisitoRepo(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.WizGrabarRequisitoBL(entidad);
        }
        internal Cls_Ent_Solicitud WizGrabarContratoRepo(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.WizGrabarContratoBL(entidad);
        }
        internal Cls_Ent_Solicitud WizGrabarLocador(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Soliciudes.WizGrabarLocador(entidad);
        }
          
        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}