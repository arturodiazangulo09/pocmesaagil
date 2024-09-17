using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web;
using System.Security.Cryptography;

namespace MEF.PROYECTO.Utilitario
{
    public static class Encriptar
    {
        public static string Right(this string sValue, int iMaxLength)
        {
            if (string.IsNullOrEmpty(sValue))
            {
                sValue = string.Empty;
            }
            else if (sValue.Length > iMaxLength)
            {
                sValue = sValue.Substring(sValue.Length - iMaxLength, iMaxLength);
            }

            return sValue;
        }

        public static string Left(this string sValue, int iMaxLength)
        {
            if (string.IsNullOrEmpty(sValue))
            {
                sValue = string.Empty;
            }
            else if (sValue.Length > iMaxLength)
            {
                sValue = sValue.Substring(0, iMaxLength);
            }

            return sValue;
        }

        public static string encriptar(string usuario)
        {
            string fechaLocal;
            string yy = DateTime.Now.Year.ToString();

            string mm = Right((100 + DateTime.Now.Month).ToString(), 2);
            string dd = Right((100 + DateTime.Now.Day).ToString(), 2);
            fechaLocal = yy + mm + dd;


            string user = usuario + "--" + fechaLocal;

            string key = user.Length.ToString().Length.ToString() + user.Length.ToString();

            int totalDigitos = 0;
            int fmax = fechaLocal.Length;

            for (int p = 0; p < fmax; p++)
            {
                totalDigitos = totalDigitos + int.Parse(fechaLocal[p].ToString());
            }
            int i = totalDigitos;
            int umax = user.Length;

            for (int k = 0; k < umax; k++)
            {
                char index = (char)user[k];
                int ord = ((int)index + 159) * i;
                key = key + ord.ToString().Length.ToString();
                key = key + ord.ToString();

                i = i + 7;
            }
            string valid = GetValidarId(key);
            return key;


        }

        public static string GetValidarId(string id)
        {
            string str6;
#pragma warning disable CS0219 // La variable 'flag' está asignada pero su valor nunca se usa
            bool flag = false;
#pragma warning restore CS0219 // La variable 'flag' está asignada pero su valor nunca se usa
            int length = 0;
            int num3 = 0;
            string expression = "";
            string str3 = "";
            int num2 = 0;
            int num = 0;
            int num4 = 0;

            int valor1 = 0;
            int valor2 = 0;
            //length = int.Parse(Strings.Left(id, 1));
           
            length = int.Parse(Left(id, 1));
            
            //num3 = int.Parse(Strings.Mid(id, 2, length));
            num3 = int.Parse(id.Substring(1, length));

            //id = Strings.Mid(id, length + 2, Strings.Len(id));
            id = id.Substring(length + 1, id.Length - (length + 1));

            expression = "";
            int num9 = 100 + DateTime.Now.Month;
            int num8 = 100 + DateTime.Now.Day;

            str3 = DateTime.Now.Year.ToString() + Right(num9.ToString(), 2) + Right(num8.ToString(), 2);
            num2 = 0;
            int num11 = str3.Length;
            for (int i = 0; i < num11; i++)
            {
                num2 += int.Parse(str3.Substring(i, 1).ToString());
            }
            num = num2;
            int num12 = num3;
            for (int j = 0; j < num12; j++)
            {
                num4 = int.Parse(Left(id, 1));
                valor1 = (char)int.Parse(id.Substring(1, num4));
                valor2 = (num + ((j) * 7));

                expression = expression + Convert.ToChar((valor1 / valor2 - 159));
                id = id.Substring(num4 + 1, id.Length - (num4 + 1));

                //num4 = int.Parse(id.Substring(0, 1));
                ////// expression = expression + Conversions.ToString(Strings.Chr((int) Math.Round((double) ((((double) int.Parse(Strings.Mid(id, 2, num4))) / ((double) (num + ((j - 1) * 7)))) - 159.0))));
                /////  id = Strings.Mid(id, num4 + 2, Strings.Len(id));
            }

            string[] strArray = { "11", "d" };
            ////Strings.Split(expression, "--", -1, CompareMethod.Binary);

            strArray = expression.Split('-');

            string str7 = strArray[0];
            string str2 = strArray[2];
            if (str2 == str3)
            {
                str6 = str7.ToUpper();
            }
            else
            {
                str6 = "";
            }
            if (str6 == "")
            {
                flag = false;
                return str6;
            }
            flag = true;
            return str6;



        }

        public static string Encriptar_Pass(string texto)
        {
            string key;
            key = "ABCDEFGHIJKLMÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz";
            //arreglo de bytes donde guardaremos la llave 
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto 
            //que vamos a encriptar 
            byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);
            //se utilizan las clases de encriptación 
            //provistas por el Framework 
            //Algoritmo MD5 
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice 
            //hashing 
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            //Algoritmo 3DAS 
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB; tdes.Padding = PaddingMode.PKCS7;
            //se empieza con la transformación de la cadena 
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //arreglo de bytes donde se guarda la //cadena cifrada 
            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length); tdes.Clear();
            //se regresa el resultado en forma de una cadena 
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);

        }
        public static string Desencriptar_Pass(string textoEncriptado)
        {
            string key;
            key = "ABCDEFGHIJKLMÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz";
            byte[] keyArray;
            //convierte el texto en una secuencia de bytes 
            byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);
            //se llama a las clases que tienen los algoritmos 
            //de encriptación se le aplica hashing 
            //algoritmo MD5 
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray; tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
            tdes.Clear();
            //se regresa en forma de cadena 
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
     
    }
}
