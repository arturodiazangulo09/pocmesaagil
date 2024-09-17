using System.Collections.Generic;
using System.Net;

namespace MEF.PROYECTO.Utilitario
{
    public class FuncionUtil
    {
        public string MiDireccionIP()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }


            return localIP;
        }

        public List<string> ListaCorreoCopiaOculto(string correos)
        {
            List<string> correoCopiaoculta = new List<string>();
            string[] matriz = correos.ToString().Split(';');
            if (matriz.Length > 0)
            {
                foreach (var item in matriz)
                {
                    correoCopiaoculta.Add(item);
                }
            }

            return correoCopiaoculta;
        }
        public static string Mi_Mes (int id_mes)
        {

            string mes = "";
            switch (id_mes)
            {
                case 1:
                    mes = "ENERO";
                    break;
                case 2:
                    mes = "FEBRERO";
                    break;
                case 3:
                    mes = "MARZO";
                    break;
                case 4:
                    mes = "ABRIL";
                    break;
                case 5:
                    mes = "MAYO";
                    break;
                case 6:
                    mes = "JUNIO";
                    break;
                case 7:
                    mes = "JULIO";
                    break;
                case 8:
                    mes = "AGOSTO";
                    break;
                case 9:
                    mes = "SEPTIEMBRE";
                    break;
                case 10:
                    mes = "OCTUBRE";
                    break;
                case 11:
                    mes = "NOVIEMBRE";
                    break;
                case 12:
                    mes = "DICIEMBRE";
                    break;

            }

            return mes;
        }
        ///////////////////////////////////
    }
}
