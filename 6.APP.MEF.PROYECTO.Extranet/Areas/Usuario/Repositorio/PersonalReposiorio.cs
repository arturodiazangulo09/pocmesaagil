using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Personal;
using MEF.PROYECTO.Entity.Personal;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Usuario.Repositorio
{
    public class PersonalReposiorio: IDisposable
    {
        public List<Cls_Ent_Personal> ListaPersonal(Cls_Ent_Personal entidad)
        {
            List<Cls_Ent_Personal> lista = null;
            lista = CLs_Rule_Personal.ListaPersonal(entidad);
            return lista;
        }
        public Cls_Ent_Personal UpdateContrasenaPersonal(Cls_Ent_Personal entidad)
        {
            return CLs_Rule_Personal.UpdateContrasenaPersonal(entidad);
        }
        public Cls_Ent_Personal UpdateRestablecerContrasenaPersonal(Cls_Ent_Personal entidad)
        {
            return CLs_Rule_Personal.UpdateRestablecerContrasenaPersonal(entidad);
        }
        public List<Cls_Ent_Grado_Academico> ListaNivelGrado( )
        {
            List<Cls_Ent_Grado_Academico> lista = null;
            lista = CLs_Rule_Personal.ListaNivelGrado();
            return lista;
        }
        public List<Cls_Ent_Nivel_Academico> ListaNivelAcademico( )
        {
            List<Cls_Ent_Nivel_Academico> lista = null;
            lista = CLs_Rule_Personal.ListaNivelAcademico();
            return lista;
        }
        public Cls_Ent_Estudios Mentenimiento_Estudios(Cls_Ent_Estudios entidad)
        {
            return CLs_Rule_Personal.Mentenimiento_Estudios(entidad);
        }
        public List<Cls_Ent_Estudios> ListaEstudios(Cls_Ent_Estudios entidad)
        {
            List<Cls_Ent_Estudios> lista = null;
            lista = CLs_Rule_Personal.ListaEstudios(entidad);
            return lista;
        }
        public List<Cls_Ent_Tipo_Especializacion> ListaTipoEspecializacion()
        {
            List<Cls_Ent_Tipo_Especializacion> lista = null;
            lista = CLs_Rule_Personal.ListaTipoEspecializacion();
            return lista;
        }
        public Cls_Ent_Especializacion Mentenimiento_Especializacion(Cls_Ent_Especializacion entidad)
        {
            return CLs_Rule_Personal.Mentenimiento_Especializacion(entidad);
        }
        public List<Cls_Ent_Especializacion> ListaEspecializacion(Cls_Ent_Especializacion entidad)
        {
            List<Cls_Ent_Especializacion> lista = null;
            lista = CLs_Rule_Personal.ListaEspecializacion(entidad);
            return lista;
        }
        public Cls_Ent_Capacitacion Mentenimiento_Capacitacion(Cls_Ent_Capacitacion entidad)
        {
            return CLs_Rule_Personal.Mentenimiento_Capacitacion(entidad);
        }
        public List<Cls_Ent_Capacitacion> ListaCapacitacion(Cls_Ent_Capacitacion entidad)
        {
            List<Cls_Ent_Capacitacion> lista = null;
            lista = CLs_Rule_Personal.ListaCapacitacion(entidad);
            return lista;
        }

        public List<Cls_Ent_Tipo_Experiencia> ListaTipoExperiencia()
        {
            List<Cls_Ent_Tipo_Experiencia> lista = null;
            lista = CLs_Rule_Personal.ListaTipoExperiencia();
            return lista;
        }
        public Cls_Ent_Experiencia_Laboral Mentenimiento_Experiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            return CLs_Rule_Personal.Mentenimiento_Experiencia(entidad);
        }
        public List<Cls_Ent_Experiencia_Laboral> ListaExperiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            int xdias = 0;
            int xmeses = 0;
            int xanios = 0;
            int I = 1;
            List<Cls_Ent_Experiencia_Laboral> lista = CLs_Rule_Personal.ListaExperiencia(entidad);
            if (lista != null)
            {
                int anio_o = 0;
                int mes_o = 0;
                int dia_o = 0;
                int anio = 0;
                int mes = 0;
                int dia = 0;
                DateTime FechaFinAnterior = DateTime.Now;
                foreach (Cls_Ent_Experiencia_Laboral item in lista)
                {
                    //FECHAS SIN TOMAR EN CUENTA TRASLAPE

                    TimeSpan difference_o = item.FEC_FIN_EXPERIENCIA.Subtract(item.FEC_INICIO_EXPERIENCIA);
                    if (difference_o.Ticks > 0)
                    {
                        var anioDif = Math.Truncate((Decimal)(difference_o.Days / 365));
                        var FechaInicio = item.FEC_INICIO_EXPERIENCIA;
                        var FechaFinal = item.FEC_FIN_EXPERIENCIA.AddDays(1);
                        FechaInicio = FechaInicio.AddYears((int)anioDif);
                        if (FechaInicio > FechaFinal)
                        {
                            FechaInicio = FechaInicio.AddYears(-1);
                            anioDif -= 1;
                        }
                        TimeSpan Diferencia2 = FechaFinal.Subtract(FechaInicio);
                        var mesDif2 = Math.Truncate((Decimal)(Diferencia2.Days / 30));
                        FechaInicio = FechaInicio.AddMonths((int)mesDif2);
                        if (FechaInicio > FechaFinal)
                        {
                            FechaInicio = FechaInicio.AddMonths(-1);
                            mesDif2 -= 1;
                        }

                        var Diferencia3 = FechaFinal.Subtract(FechaInicio);
                        anio_o = (int)anioDif;
                        mes_o = (int)mesDif2;
                        dia_o = Diferencia3.Days;
                    }
                    item.NUM_ANIOS = anio_o;
                    item.NUM_MESES = mes_o;
                    item.NUM_DIAS = dia_o;
                    anio_o = mes_o = dia_o = 0;
                    if (I == 1)
                    {
                        TimeSpan difference = item.FEC_FIN_EXPERIENCIA.Subtract(item.FEC_INICIO_EXPERIENCIA);
                        if (difference.Ticks > 0)
                        {
                            var anioDif = Math.Truncate((Decimal)(difference.Days / 365));
                            var FechaInicio = item.FEC_INICIO_EXPERIENCIA;
                            var FechaFinal = item.FEC_FIN_EXPERIENCIA.AddDays(1);
                            FechaInicio = FechaInicio.AddYears((int)anioDif);
                            if (FechaInicio > FechaFinal)
                            {
                                FechaInicio = FechaInicio.AddYears(-1);
                                anioDif -= 1;
                            }


                            TimeSpan Diferencia2 = FechaFinal.Subtract(FechaInicio);
                            var mesDif2 = Math.Truncate((Decimal)(Diferencia2.Days / 30));
                            FechaInicio = FechaInicio.AddMonths((int)mesDif2);
                            if (FechaInicio > FechaFinal)
                            {
                                FechaInicio = FechaInicio.AddMonths(-1);
                                mesDif2 -= 1;
                            }

                            var Diferencia3 = FechaFinal.Subtract(FechaInicio);
                            anio = (int)anioDif;
                            mes = (int)mesDif2;
                            dia = Diferencia3.Days;
                        }
                        item.NUM_ANIOS_TOTAL = anio;
                        item.NUM_MESES_TOTAL = mes;
                        item.NUM_DIAS_TOTAL = dia;
                        FechaFinAnterior = item.FEC_FIN_EXPERIENCIA.AddDays(1);
                    }
                    else
                    {
                        if (item.FEC_INICIO_EXPERIENCIA >= FechaFinAnterior)
                        {
                            TimeSpan difference = item.FEC_FIN_EXPERIENCIA.Subtract(item.FEC_INICIO_EXPERIENCIA);
                            if (difference.Ticks > 0)
                            {
                                var anioDif = Math.Truncate((Decimal)(difference.Days / 365));
                                var FechaInicio = item.FEC_INICIO_EXPERIENCIA;
                                var FechaFinal = item.FEC_FIN_EXPERIENCIA.AddDays(1);
                                FechaInicio = FechaInicio.AddYears((int)anioDif);
                                if (FechaInicio > FechaFinal)
                                {
                                    FechaInicio = FechaInicio.AddYears(-1);
                                    anioDif -= 1;
                                }
                                TimeSpan Diferencia2 = FechaFinal.Subtract(FechaInicio);
                                var mesDif2 = Math.Truncate((Decimal)(Diferencia2.Days / 30));
                                FechaInicio = FechaInicio.AddMonths((int)mesDif2);
                                if (FechaInicio > FechaFinal)
                                {
                                    FechaInicio = FechaInicio.AddMonths(-1);
                                    mesDif2 -= 1;
                                }
                                var Diferencia3 = FechaFinal.Subtract(FechaInicio);
                                anio = (int)anioDif;
                                mes = (int)mesDif2;
                                dia = Diferencia3.Days;
                            }
                            item.NUM_ANIOS_TOTAL = anio;
                            item.NUM_MESES_TOTAL = mes;
                            item.NUM_DIAS_TOTAL = dia;
                        }
                        if (item.FEC_INICIO_EXPERIENCIA < FechaFinAnterior && item.FEC_FIN_EXPERIENCIA < FechaFinAnterior)
                        {
                            item.NUM_ANIOS_TOTAL = anio;
                            item.NUM_MESES_TOTAL = mes;
                            item.NUM_DIAS_TOTAL = dia;
                        }
                        if (item.FEC_INICIO_EXPERIENCIA < FechaFinAnterior && item.FEC_FIN_EXPERIENCIA > FechaFinAnterior)
                        {
                            TimeSpan difference = item.FEC_FIN_EXPERIENCIA.Subtract(FechaFinAnterior.AddDays(1));
                            if (difference.Ticks > 0)
                            {
                                var anioDif = Math.Truncate((Decimal)(difference.Days / 365));
                                var FechaInicio = item.FEC_INICIO_EXPERIENCIA;
                                var FechaFinal = item.FEC_FIN_EXPERIENCIA.AddDays(1);

                                FechaInicio = FechaInicio.AddYears((int)anioDif);
                                if (FechaInicio > FechaFinal)
                                {
                                    FechaInicio = FechaInicio.AddYears(-1);
                                    anioDif -= 1;
                                }
                                TimeSpan Diferencia2 = FechaFinal.Subtract(FechaInicio);
                                var mesDif2 = Math.Truncate((Decimal)(Diferencia2.Days / 30));
                                FechaInicio = FechaInicio.AddMonths((int)mesDif2);
                                if (FechaInicio > FechaFinal)
                                {
                                    FechaInicio = FechaInicio.AddMonths(-1);
                                    mesDif2 -= 1;
                                }

                                var Diferencia3 = FechaFinal.Subtract(FechaInicio);
                                anio = (int)anioDif;
                                mes = (int)mesDif2;
                                dia = Diferencia3.Days;
                            }
                            item.NUM_ANIOS_TOTAL = anio;
                            item.NUM_MESES_TOTAL = mes;
                            item.NUM_DIAS_TOTAL = dia;
                        }
                    }
                    FechaFinAnterior = item.FEC_FIN_EXPERIENCIA;
                    anio = mes = dia = 0;
                    I += 1;

                    xdias += item.NUM_ANIOS_TOTAL;
                    xmeses += item.NUM_MESES_TOTAL;
                    xanios += item.NUM_DIAS_TOTAL;
                }

            }
            return lista;
        }
        public List<Cls_Ent_Nacionalidad> ListaTipoNacionalidad()
        {
            List<Cls_Ent_Nacionalidad> lista = null;
            lista = CLs_Rule_Personal.ListaTipoNacionalidad();
            return lista;
        }
        public List<Cls_Ent_Banco> ListaTipoBanco()
        {
            List<Cls_Ent_Banco> lista = null;
            lista = CLs_Rule_Personal.ListaTipoBanco();
            return lista;
        }
        public Cls_Ent_Personal UpdatePersonal(Cls_Ent_Personal entidad)
        {
            return CLs_Rule_Personal.UpdatePersonal(entidad);
        }
        public List<Cls_Ent_Solicitud_Personal> ListasolicitudPersonal(Cls_Ent_Solicitud_Personal entidad)
        {
            List<Cls_Ent_Solicitud_Personal> lista = null;
            lista = CLs_Rule_Personal.ListasolicitudPersonal(entidad);
            return lista;
        }
        public Cls_Ent_Solicitud_Personal UpdateDJ_Fag(Cls_Ent_Solicitud_Personal entidad)
        {
            return CLs_Rule_Personal.UpdateDJ_Fag(entidad);
        }
        public Cls_Ent_Solicitud_Personal UpdateDJ_Pac(Cls_Ent_Solicitud_Personal entidad)
        {
            return CLs_Rule_Personal.UpdateDJ_Pac(entidad);
        }
        public Cls_Ent_Solicitud_Personal UpdatePropuesta_Envio(Cls_Ent_Solicitud_Personal entidad)
        {
            return CLs_Rule_Personal.UpdatePropuesta_Envio(entidad);
        }
   
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}