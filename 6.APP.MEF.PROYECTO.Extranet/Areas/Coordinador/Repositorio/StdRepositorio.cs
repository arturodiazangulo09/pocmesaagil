using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.Utilitario;
using APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Models;
using System.Web.Mvc;
using System.ServiceModel;
using System.ServiceModel.Channels;
using APP.MEF.EXTRANET.FAG.PAG.WCF_STD22;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.BusinessLayer.Coordinador;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Repositorio
{
   
    public class StdRepositorio : IDisposable
    {
   
        public WCF_STD22.anexoDto Archivo_Adjunto (byte[] archivo, string nombre,long tamaño)
        {
            WCF_STD22.anexoDto Datos = new WCF_STD22.anexoDto();
            Datos.archivo = archivo;
            Datos.name = nombre;
            Datos.length = tamaño;
            return Datos;
        }
        public WCF_STD22.hrDto Crear_Expediente_Std(StdModel entidad)
        {
            WCF_STD22.hrDto Datos = new WCF_STD22.hrDto();
            entidad.SEC_EJECT = "-";
            try
            {
                using (WCF_STD22.serviciosstd proxy = new serviciosstd())
                {
                    entidad.SEC_EJECT = "-";
                     Datos = proxy.crearExpedienteFAG(entidad.NUM_REGISTRO, entidad.NUM_OFICIO, entidad.NUM_FOLIOS, entidad.ASUNTO, entidad.CLASIFICACION, entidad.RAZONSOCIAL, entidad.RUC, entidad.SEC_EJECT, entidad.DIRECCION, entidad.DEPARTAMENTO, entidad.PROVINCIA, entidad.DISTRITO, entidad.CORREO, entidad.OBSERVACION, entidad.OFICIO, entidad.RESUMEN, entidad.ANEXOS, entidad.REMOTEADDRESS);
                }

            }
            catch (Exception ex)
            {
                Log.MensajeLog(ex.Message, "SERVICIOS STD");
            }
            return Datos;
        }
        public List<Cls_Ent_Archivo_STD> ListaArchivoSTD(int ID)
        {
            List<Cls_Ent_Archivo_STD> lista = null;
            lista = Cls_Rule_Soliciudes.ListaArchivoSTD(ID);
            return lista;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}