using MEF.PROYECTO.Entity.CargaMasiva;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static MEF.PROYECTO.Utilitario.Constants;

namespace MEF.PROYECTO.Utilitario
{
    public  class Respuesta
    {

        String _mensaje;
        public int NroItem { set; get; }
        public string Valor { set; get; }
        public String Campo { set; get; }
        public Boolean IsError { set; get; }
        public String Mensaje { set; get; }


        public Respuesta()
        {
            IsError = false;
            Campo = "";
            Valor = "";
            NroItem = -1;

        }
        public void SetMensaje(String mensajeError)
        {
            Mensaje= mensajeError;
        }

        public void SetMensaje (String valor, Int32 nroItem, String campo, String mensajeError)
        {
            NroItem = nroItem;
            Campo = campo;
            Valor=valor;
            Mensaje = mensajeError;
        }
     
    }

    public class RespuestaOperacion
    {
       public  Boolean Status { get; set; }
       public List<Respuesta> ListaErrores { get; set; } = new List<Respuesta>();

    
    }
    public class ErrorInfo
    {
        String _mensaje = null;
        Int32 _codigo = 0;
        String  _detalle = null;
        String _origen = null;

        public ErrorInfo(String mensaje, Int32 codigo,string detalle, string Origen) {
            this._mensaje = mensaje;
            this._codigo = codigo;
            this._detalle = detalle;
            this._origen = Origen;
        }

    }

    public class Respuesta<T>
    {
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public T Data { get; set; }
    }

    public class CargaInicial
    {
        public List<Catalogo> Estado { get; set; }
        public List<Catalogo> SiNo { get; set; } 
        public List<Catalogo> Sexo { get; set; }
        public List<Catalogo> Region { get; set; }
        public List<Catalogo> Provincia { get; set; }
        public List<Catalogo> Distrito { get; set; }
    }

    
}
