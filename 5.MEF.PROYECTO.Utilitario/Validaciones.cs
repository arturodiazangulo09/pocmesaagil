using MEF.PROYECTO.Entity.CargaMasiva;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MEF.PROYECTO.Utilitario.Constants.Mensajes;

namespace MEF.PROYECTO.Utilitario
{
    public class Validaciones
    {
        public Respuesta CampoObligatorio(string valor, int nroItem, String campo)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.IsError = true;
            respuesta.SetMensaje(valor, nroItem, campo, Error.Requerido);
            return respuesta;
        }
        public Respuesta ValidarSoloLetras(string valor,int nroItem,String campo) {
            Respuesta respuesta = new Respuesta();
            bool isSololetras = valor.Replace(" ","").Replace(".","").All(Char.IsLetter);
            if (!isSololetras) {
                respuesta.IsError=true;
                respuesta.SetMensaje(valor, nroItem, campo, Error.SoloTexto);
            }
            return respuesta;
        }

        public Respuesta ValidarSoloLetras(string valor)
        {
            Respuesta respuesta = new Respuesta();
            bool isSololetras = valor.Replace(" ", "").All(Char.IsLetter);
            if (!isSololetras)
            {
                respuesta.IsError=true;
                respuesta.SetMensaje( Error.SoloTexto);
            }
            return respuesta;
        }

        public Respuesta soloAlfanumerico(string Valor)
        {
            Respuesta respuesta = new Respuesta();
            if (!Valor.All(char.IsLetterOrDigit))
            {
                respuesta.IsError=true;
                respuesta.SetMensaje(Error.SoloAlfanumerico);
            }
            
            return respuesta;
        }
        public Respuesta soloAlfanumerico(string valor, int nroItem, String campo)
        {
            Respuesta respuesta = new Respuesta();
            if (!valor.All(char.IsLetterOrDigit))
            {
                respuesta.IsError=true;
                respuesta.SetMensaje(valor, nroItem, campo,Error.SoloAlfanumerico);
            }
            return respuesta;
        }
        public Respuesta ComprobarFormatoEmail(string sEmailAComprobar)
        {
            Respuesta respuesta = new Respuesta();
            String sFormato;
            sFormato = @"\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(sEmailAComprobar, sFormato))
            {
                if (Regex.Replace(sEmailAComprobar, sFormato, String.Empty).Length != 0)
                {
                    respuesta.IsError=true;
                    respuesta.SetMensaje(Error.CorreoElectronico);
                }
            }
            else {
                respuesta.IsError=true;
                respuesta.SetMensaje(Error.CorreoElectronico);

            }
            return respuesta;
        }

        public Respuesta ValidarTelefono(string valor, int nroItem, String campo)
        {
            Respuesta respuesta = new Respuesta();
            string isSolonumeros = valor.Replace(" ", "").Replace("+", "").Replace("/", "").Replace(",", "");
            Int32 resultado = 0;
            bool esNumerico = Int32.TryParse(isSolonumeros, out resultado);
            if (!esNumerico)
            {
                respuesta.IsError=true;
                respuesta.SetMensaje(valor, nroItem, campo, Error.SoloNumeros);
            }

            if(isSolonumeros.Length<7)
                if (!esNumerico)
                {
                    respuesta.IsError=true;
                    respuesta.SetMensaje(valor, nroItem, campo, Error.Tamanio);
                }
            return respuesta;
        }
        public Respuesta ValidarSoloNumeros(string valor, int nroItem, String campo)
        {
            Respuesta respuesta = new Respuesta();
            Int32 resultado = 0;
            bool esNumerico = Int32.TryParse(valor, out resultado);
            if (!esNumerico)
            {
                respuesta.IsError=true;
                respuesta.SetMensaje(valor, nroItem,campo, Error.SoloNumeros);
            }
            return respuesta;
        }

        public Respuesta ValidarRUC(string valor, int nroItem, String campo)
        {
            Respuesta respuesta = new Respuesta();
            bool isruc = esRUCInvalido(valor);
            if (isruc)
            {
                respuesta.IsError=isruc;
                respuesta.SetMensaje(valor, nroItem, campo, Error.ruc);
            }
            return respuesta;
        }
        public  Boolean esRUCInvalido(String ruc)
        {
            try {
                if (ruc == null) return true;


                int[] valores = { 1, 2, 3, 4, 5, 6, 7, 8, 9,0 };
                String[] prefixes = getRucPrefixes();
                int length = valores.Length+1;

                if (ruc.Length != length)
                {
                    return true;
                }

                Boolean isPrefixOk = true;

                foreach (String prefix in prefixes)
                {
                    if (ruc.Substring(0, 2).Equals(prefix))
                    {
                        isPrefixOk = true;
                        break;
                    }
                }

                if (!isPrefixOk) return true;

                for (int i = 0; i < ruc.Length; i++)
                {

                    string valor = ruc.Substring(i, 1);

                    int index = Array.IndexOf(valores, Int32.Parse(valor));
                    if (index == -1)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex) {

                return true;
            }
           

            return false;

        }
        public  static String[] getRucPrefixes()
        {
            return new String[] { "10", "15", "17", "20" };
        }
        public Respuesta ValidarSoloNumeros(string Valor)
        {
            Respuesta respuesta = new Respuesta();
            Int32 resultado = 0;
            bool esNumerico = int.TryParse(Valor, out resultado);
            if (!esNumerico)
            {
                respuesta.IsError=true;
                respuesta.SetMensaje(Error.SoloNumeros);
            }
            return respuesta;
        }

        public Respuesta ValidarHojaRuta(string Valor, int nroItem, String campo)
        {
            Respuesta respuesta = new Respuesta();
            Double resultado = 0;
            Valor = Valor.Replace(" ", "").Replace("-", "");
            bool esNumerico = Double.TryParse(Valor.Replace(" ","").Replace("-", ""), out resultado);
            //if (!esNumerico)
            //{
            //    respuesta.IsError=true;
            //    respuesta.SetMensaje(Valor, nroItem, campo, Error.SoloNumeros);
            //}
            return respuesta;
        }
        public Respuesta Tamanio(int Minimo, int Maximo, String Valor)
        {
            Respuesta respuesta = new Respuesta();
            int length = Valor.Length;

            if (length < Minimo || length > Maximo) {
                respuesta.IsError=true;
                respuesta.SetMensaje(Error.Tamanio);
            }

            return respuesta;
        }

        public Respuesta ValoresFijos(string [] Permitidos, String Valor)
        {
            Respuesta respuesta = new Respuesta();

            bool existe = Permitidos.Contains(Valor);

            if (!existe )
            {
                respuesta.IsError=true;
                respuesta.SetMensaje(Error.ValoresFijos);
            }
           
            return respuesta;
        }

        public Respuesta ValoresFijos(string[] Permitidos, string valor, int nroItem, String campo)
        {
            Respuesta respuesta = new Respuesta();

            bool existe = Permitidos.Contains(valor);

            if (!existe)
            {
                respuesta.IsError=true;
                respuesta.SetMensaje(valor, nroItem, campo, Error.ValoresFijos);
            }

            return respuesta;
        }


        public Respuesta ValidarFecha(string Fecha)
        {
            Respuesta respuesta = new Respuesta();
            DateTime dt1 = DateTime.Now;
            var culture = CultureInfo.CreateSpecificCulture("es-MX");
            var styles = DateTimeStyles.None;
            try
            {
                bool fechaValida = DateTime.TryParse(Fecha.Substring(0, 10), culture, styles, out dt1);
                if (!fechaValida)
                {
                    respuesta.IsError = true;
                    respuesta.SetMensaje(Error.FormatoFecha);
                }
            }
            catch (Exception)
            {
                respuesta.IsError = true;
                respuesta.SetMensaje(Error.FormatoFecha);
            }
           
            return respuesta;
        }

        public Respuesta ValidarFecha(string valor, int nroItem, String campo)
        {
            Respuesta respuesta = new Respuesta();
            DateTime dt1 = DateTime.Now;
            var culture = CultureInfo.CreateSpecificCulture("es-MX");
            var styles = DateTimeStyles.None;
            try
            {
                bool fechaValida = DateTime.TryParse(valor.Substring(0, 10), culture, styles, out dt1);
                if (!fechaValida)
                {
                    respuesta.IsError = true;
                    respuesta.SetMensaje(valor, nroItem, campo, Error.FormatoFecha);
                }
            }
            catch (Exception)
            {
                respuesta.IsError = true;
                respuesta.SetMensaje(valor, nroItem, campo, Error.FormatoFecha);
            }
            
            return respuesta;
        }

        public Respuesta ValidarMontosDecimales(String Valor)
        {
            Respuesta respuesta = new Respuesta();
            Double resultado = 0;
            bool esNumerico = Double.TryParse(Valor, out resultado);
            if (!esNumerico)
            {
                respuesta.IsError=true;
                respuesta.SetMensaje(Error.SoloMonto);
            }
            return respuesta;
        }

        public Respuesta ValidarMontosDecimales(String Valor, int nroItem, String campo)
        {
            Respuesta respuesta = new Respuesta();
            Double resultado = 0;
            bool esNumerico = Double.TryParse(Valor, out resultado);
            if (!esNumerico)
            {
                respuesta.IsError=true;
                respuesta.SetMensaje(Valor, nroItem,campo,Error.SoloMonto);
            }
            return respuesta;
        }

        public Respuesta ValidarFormatoCorreo(String Valor)
        {
            Respuesta respuesta = new Respuesta();
            if (new EmailAddressAttribute().IsValid(Valor.Trim()) == false)
            {
                respuesta.IsError = true;
                respuesta.SetMensaje(Error.FormatoCorreo);
            }
            //string patronCorreo = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            //if(!Regex.IsMatch(Valor, patronCorreo))
            //{
            //    respuesta.IsError=true;
            //    respuesta.SetMensaje(Error.FormatoCorreo);
            //}
            return respuesta;
        }

        public Respuesta ValidarFormatoCorreo(String valor, int nroItem, String campo)
        {
            Respuesta respuesta = new Respuesta();
            //string patronCorreo = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            //if (!Regex.IsMatch(valor, patronCorreo))
            //{
            //    respuesta.IsError=true;
            //    respuesta.SetMensaje(valor, nroItem, campo, Error.FormatoCorreo);
            //}
            if (new EmailAddressAttribute().IsValid(valor) == false)
            {
                respuesta.IsError = true;
                respuesta.SetMensaje(valor, nroItem, campo, Error.FormatoCorreo);
            }
            return respuesta;
        }


    }
}
