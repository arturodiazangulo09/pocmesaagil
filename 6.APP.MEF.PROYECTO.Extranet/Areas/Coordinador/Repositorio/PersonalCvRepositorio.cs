using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.BusinessLayer.Coordinador;
using MEF.PROYECTO.BusinessLayer.Administracion;
using MEF.PROYECTO.Entity.Personal;
using MEF.PROYECTO.Entity.Coordinador;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio
{
    public class PersonalCvRepositorio: IDisposable
    {
        public List<Cls_Ent_Estudios> ListaEstudios(Cls_Ent_Estudios entidad)
        {
            List<Cls_Ent_Estudios> lista = null;
            lista = Cls_Rule_Personal_Cv.ListaEstudios(entidad);
            return lista;
        }
        public List<Cls_Ent_Especializacion> ListaEspecializacion(Cls_Ent_Especializacion entidad)
        {
            List<Cls_Ent_Especializacion> lista = null;
            lista = Cls_Rule_Personal_Cv.ListaEspecializacion(entidad);
            return lista;
        }
        public List<Cls_Ent_Capacitacion> ListaCapacitacion(Cls_Ent_Capacitacion entidad)
        {
            List<Cls_Ent_Capacitacion> lista = null;
            lista = Cls_Rule_Personal_Cv.ListaCapacitacion(entidad);
            return lista;
        }
        public List<Cls_Ent_Experiencia_Laboral> ListaExperiencia(Cls_Ent_Experiencia_Laboral entidad)
        {
            //List<Cls_Ent_Experiencia_Laboral> lista = null;
            //lista = Cls_Rule_Personal_Cv.ListaExperiencia(entidad);
            //return lista;

            int xdias = 0;
            int xmeses = 0;
            int xanios = 0;
            int I = 1;
            List<Cls_Ent_Experiencia_Laboral> lista = Cls_Rule_Personal_Cv.ListaExperiencia(entidad);
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
        public Cls_Ent_Solicitud UpdAplicaRegistoCv(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Personal_Cv.UpdAplicaRegistoCv(entidad);
        }
        public Cls_Ent_Solicitud UpdCalificacionRequisitos(Cls_Ent_Solicitud entidad)
        {
            return Cls_Rule_Personal_Cv.UpdCalificacionRequisitos(entidad);
        }
        public List<Cls_Ent_Solicitud_Personal> ListaSolicitudesCoordinador(Cls_Ent_Solicitud_Personal entidad)
        {
            List<Cls_Ent_Solicitud_Personal> lista = null;
            lista = Cls_Rule_Solicitudes_Coordinador.ListaSolicitudesCoordinador(entidad);
            return lista;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}