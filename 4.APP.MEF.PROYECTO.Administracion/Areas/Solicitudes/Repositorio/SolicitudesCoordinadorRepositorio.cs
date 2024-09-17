using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using MEF.PROYECTO.BusinessLayer.Personal;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Entity.Coordinador;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Solicitudes.Repositorio
{
    public class SolicitudesCoordinadorRepositorio: IDisposable
    {
        public List<Cls_Ent_Solicitud_Personal> ListaSolicitudesCoordinador(Cls_Ent_Solicitud_Personal entidad)
        { 
            List<Cls_Ent_Solicitud_Personal> lista = null;
            lista = Cls_Rule_Solicitudes_Coordinador.ListaSolicitudesCoordinador(entidad);
            return lista;
        }
        public List<Cls_Ent_Solicitud_Personal> ProximoCodContrato() => Cls_Rule_Solicitudes_Coordinador.ProximoCodContrato();

        public List<Cls_Ent_Solicitud_Personal> GetDatosSolicitud(int ID_SOLICITUD) => Cls_Rule_Solicitudes_Coordinador.GetDatosSolicitud(ID_SOLICITUD);

        public List<Cls_Ent_Solicitud_Personal> GetDatosAdenda(int ID_CONTRATO_DET) => Cls_Rule_Solicitudes_Coordinador.GetDatosAdenda(ID_CONTRATO_DET);

        

        //INICIO CV
        public List<Cls_Ent_Estudios> ListaEstudios(Cls_Ent_Estudios entidad)
        {
            List<Cls_Ent_Estudios> lista = null;
            lista = Cls_Rule_Personal_Cv.ListaEstudios(entidad);
            return lista;
        }
        public Cls_Ent_Estudios MentenimientoEstudios_Verificar(Cls_Ent_Estudios entidad)
        {
            return Cls_Rule_Personal_Cv.MentenimientoEstudios_Verificar(entidad);
        }
        public List<Cls_Ent_Especializacion> ListaEspecializacion(Cls_Ent_Especializacion entidad)
        {
            List<Cls_Ent_Especializacion> lista = null;
            lista = Cls_Rule_Personal_Cv.ListaEspecializacion(entidad);
            return lista;
        }
        public Cls_Ent_Especializacion MentenimientoEstudios_Especializacion(Cls_Ent_Especializacion entidad)
        {
            return Cls_Rule_Personal_Cv.MentenimientoEstudios_Especializacion(entidad);
        }
        public List<Cls_Ent_Capacitacion> ListaCapacitacion(Cls_Ent_Capacitacion entidad)
        {
            List<Cls_Ent_Capacitacion> lista = null;
            lista = Cls_Rule_Personal_Cv.ListaCapacitacion(entidad);
            return lista;
        }
        public Cls_Ent_Capacitacion MentenimientoEstudios_Capacitacion(Cls_Ent_Capacitacion entidad)
        {
            return Cls_Rule_Personal_Cv.MentenimientoEstudios_Capacitacion(entidad);
        }
        public List<Cls_Ent_Experiencia_Laboral> ListaExperiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            List<Cls_Ent_Experiencia_Laboral> lista = null;
            lista = Cls_Rule_Personal_Cv.ListaExperiencia(entidad);
            return lista;
        }
        public Cls_Ent_Experiencia_Laboral MentenimientoEstudios_Experiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            return Cls_Rule_Personal_Cv.MentenimientoEstudios_Experiencia(entidad);
        }
        //FIN CV
        public Cls_Ent_Solicitud_Personal UPD_CALIFICAR_DOCUMENTOS(Cls_Ent_Solicitud_Personal entidad)
        {
            return Cls_Rule_Solicitudes_Coordinador.UPD_CALIFICAR_DOCUMENTOS(entidad);
        }
        public Cls_Ent_Reevaluacion InsReevaluacion(Cls_Ent_Reevaluacion entidad)
        {
            return Cls_Rule_Soliciudes.InsReevaluacion(entidad);
        }
        public List<Cls_Ent_Coordinador> ListaCoordinadores(Cls_Ent_Coordinador entidad)
        {
            List<Cls_Ent_Coordinador> lista = null;
            lista = Cls_Rule_Coordinador.ListaCoordinadores(entidad);
            return lista;
        }
        public List<Cls_Ent_Reevaluacion> ListaReevaluacion(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            lista = Cls_Rule_Soliciudes.ListaReevaluacion(entidad);
            return lista;
        }
        public Cls_Ent_Solicitud_Personal InsContratoSolicitud(Cls_Ent_Solicitud_Personal entidad)
        {
            return Cls_Rule_Solicitudes_Coordinador.InsContratoSolicitud(entidad);
        }
        public Cls_Ent_Archivo InsertAchivoSustento(Cls_Ent_Archivo entidad)
        {
            return Cls_Rule_Achivo.InsertAchivoSustento(entidad);
        }
        /****** INICIO PROCESO SOLICITUD DE PAGO ****/
        public List<Cls_Ent_Solicitud_Pago> ListaSolicitudPagoEntidadUTP(Cls_Ent_Solicitud_Pago entidad)
        {
            List<Cls_Ent_Solicitud_Pago> lista = null;
            lista = Cls_Rule_Solicitudes_Coordinador.ListaSolicitudPagoEntidadUTP(entidad);
            return lista;
        }
        public Cls_Ent_Solicitud_Pago UpdateReevaluacionPago(Cls_Ent_Solicitud_Pago entidad)
        {
            return Cls_Rule_Solicitudes_Coordinador.UpdateReevaluacionPago(entidad);
        }
        public Cls_Ent_Solicitud_Pago Update_Conformidad_Pago(Cls_Ent_Solicitud_Pago entidad)
        {
            return Cls_Rule_Soliciudes.Update_Conformidad_Pago(entidad);
        }

        /****** FIN PROCESO SOLICITUD DE PAGO  ****/


        /****** INICIO PROCESO SOLICITUD DE ADENDA  ****/

        public Cls_Ent_Reevaluacion InsReevaluacionAdenda(Cls_Ent_Reevaluacion entidad)
        {
            return Cls_Rule_Adenda.InsReevaluacionAdenda(entidad);
        }
        public List<Cls_Ent_Reevaluacion> ListaReevaluacionAdenda(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            lista = Cls_Rule_Adenda.ListaReevaluacionAdenda(entidad);
            return lista;
        }
        public Cls_Ent_Adenda UpdEstadoAdenda(Cls_Ent_Adenda entidad)
        {
            return Cls_Rule_Adenda.UpdEstadoAdenda(entidad);
        }
        /****** FIN PROCESO PROCESO DE ADENDA  ****/
        /****** INICIO PROCESO SOLICITUD DE SUSPENSION  ****/
        public List<Cls_Ent_Descanso> ListaSolicitud_Suspension(Cls_Ent_Descanso entidad)
        {
            List<Cls_Ent_Descanso> lista = null;
            lista = Cls_Rule_Descanso.ListaSolicitud_Suspension(entidad);
            return lista;
        }
        public Cls_Ent_Reevaluacion InsReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            return Cls_Rule_Suspension.InsReevaluacionSuspension(entidad);
        }
        public List<Cls_Ent_Reevaluacion> ListaReevaluacionSuspension(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            lista = Cls_Rule_Suspension.ListaReevaluacionSuspension(entidad);
            return lista;
        }
        public Cls_Ent_Descanso UpdEnvioSuspensionAprueba(Cls_Ent_Descanso entidad)
        {
            return Cls_Rule_Solicitudes_Coordinador.UpdEnvioSuspensionAprueba(entidad);
        }
        /****** FIN PROCESO PROCESO DE SUSPENSION  ****/
        /****** INICIO PROCESO SOLICITUD DE COVID  ****/
        public List<Cls_Ent_Covid> ListaSolicitud_Covid(Cls_Ent_Covid entidad)
        {
            List<Cls_Ent_Covid> lista = null;
            lista = Cls_Rule_Covid.ListaSolicitud_Covid(entidad);
            return lista;
        }
        public Cls_Ent_Reevaluacion InsReevaluacionCovid(Cls_Ent_Reevaluacion entidad)
        {
            return Cls_Rule_Solicitud_Covid.InsReevaluacionCovid(entidad);
        }
        public List<Cls_Ent_Reevaluacion> ListaReevaluacionCovid(Cls_Ent_Reevaluacion entidad)
        {
            List<Cls_Ent_Reevaluacion> lista = null;
            lista = Cls_Rule_Solicitud_Covid.ListaReevaluacionCovid(entidad);
            return lista;
        }
        public Cls_Ent_Covid UpdEnvioCovidAprueba(Cls_Ent_Covid entidad)
        {
            return Cls_Rule_Solicitudes_Coordinador.UpdEnvioCovidAprueba(entidad);
        }
        /****** FIN PROCESO PROCESO DE COVID  ****/
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}