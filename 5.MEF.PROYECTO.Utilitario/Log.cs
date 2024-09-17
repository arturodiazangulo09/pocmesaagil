using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Configuration;

namespace MEF.PROYECTO.Utilitario
{
    public static class Log
    {
        public static void MensajeLog(String mensaje, String ubicacion)
        {
            try
            {

                    String file = "";
                    if (!Directory.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\Log\\"))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Request.PhysicalApplicationPath + "\\Log");
                    }
                    file = HttpContext.Current.Request.PhysicalApplicationPath + "\\Log\\" + "Aplicación" + DateTime.Today.Date.ToString("yyyyMMdd") + ".log";

                    StreamWriter sw = new StreamWriter(file, true);
                    sw.WriteLine(DateTime.Now + ": [" + ubicacion + "] - " + mensaje);
                    sw.Close();
                
            }
            catch (Exception)
            {
            }
        }
        public static void Mensaje(Exception ex)
        {
            try
            {
                String file = "";
                if (!Directory.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\Log\\"))
                {
                    Directory.CreateDirectory(HttpContext.Current.Request.PhysicalApplicationPath + "\\Log");
                }
                file = HttpContext.Current.Request.PhysicalApplicationPath + "\\Log\\" + "Aplicación" + DateTime.Today.Date.ToString("yyyyMMdd") + ".log";

                StreamWriter sw = new StreamWriter(file, true);
                StackTrace st = new StackTrace(ex, true);
                for (int i = 0; i < st.FrameCount; i++)
                {
                    StackFrame sf = st.GetFrame(i);
                    sw.WriteLine(DateTime.Now + ": [Método: " + sf.GetMethod() + ", Línea: " + sf.GetFileLineNumber() + "] - " + ex.Message);
                }
                sw.Close();
            }
            catch (Exception)
            {
            }
        }
        public static string MensajeLogText()
        {
            string Mensaje_salida = "\r\n" + "\r\n" + "Error: ocurrió un error interno en el proceso, por favor comunique al administrador del sistema." + ", su código de error es : Aplicación" + DateTime.Today.Date.ToString("yyyyMMdd") + ".log"; ;
            return Mensaje_salida;
        }

        public static string MensajeInterno()
        {
            string Mensaje_salida = "\r\n" + "\r\n" + "Error: ocurrió un error interno en el proceso, por favor comunique al administrador del sistema.";
            return Mensaje_salida;
        }

    }
}
