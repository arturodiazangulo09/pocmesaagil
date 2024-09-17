using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF.PROYECTO.Utilitario
{
    public class Constants
    {
        //Mensajes de Error
        public struct Mensajes
        {
            public struct Error
            {
                public const string Requerido = "El Campo es Obligatorio";
                public const string FormatoFecha = "El Formato de la Fecha no es el Correcto";
                public const string FormatoCorreo = "El Formato del Correo Electrónico no es el Correcto";
                public const string ValorIncorrecto = "El Valor ingresado no es el Correcto";
                public const string SoloNumeros = "El campo solo acepta valores numéricos";
                public const string SoloMonto = "El campo solo acepta montos";
                public const string SoloTexto = "El campo solo acepta texto";
                public const string SoloAlfanumerico = "No se permiten caracteres especiales";
                public const string formatoCodigo = "No tiene el formato de código correcto";
                public const string ruc = "RUC incorrecto,debe tener 11 dígitos";
                public const string CorreoElectronico = "El formato de correo electrónico es incorrecto";
                public const string Tamanio = "El campo no cumple con las especificaciones de la longitud permitida";
                public const string ValoresFijos = "El campo no tiene los valores permitidos";
                public const string NoGuardado = "No se guardó correctamente. Vuelva a Intentarlo";
                public const string NroColumnasFormato = "El número de columnas correspondientes al formato no coinciden. No se puede continuar";
                public const string HojaRuta = "Formato de Hoja de Ruta incorrecto";
                public const string NoExiste = "El valor no existe";
                public const string UbigeoIncorrecto = "El Distrito/Provincia/Region ingresados no es el correcto";
                public const string EntidadIncorrecta = "Acronimo de entidad incorrecta";
            }
            public struct Correcto
            {
                public const string ItemCorrecto = "Item Validado Correctamente";
            }
        }

        public struct TipoDocumento
        {
            public struct FAG
            {
                public const string Id = "FAG";
                public const string Descripcion = "FORMATO 1. Cabecera Formato FAG";
            }

            public struct PAC
            {
                public const string Id = "PAC";
                public const string Descripcion = "FORMATO 2. Cabecera Formato PAC";
            }

            public struct Adendas
            {
                public const string Id = "DET";
                public const string Descripcion = "FORMATO 3. Formato Adendas";
            }

            public struct SAL
            {
                public const string Id = "SAL";
                public const string Descripcion = "FORMATO 4. Formato Pagos";
            }
        }

        public struct TipoFormato
        {
            public const string Cabecera = "C";
            public const string Detalle = "D";
        }

        public struct Estados
        {
            //FAG, PAC
            public struct Activo
            {
                public const string Id = "A";
                public const string Descripcion = "ACTIVO";
            }
            public struct Inactivo
            {
                public const string Id = "I";
                public const string Descripcion = "INACTIVO";
            }
            public struct Baja
            {
                public const string Id = "B";
                public const string Descripcion = "BAJA";
            }
            public struct Devolucion
            {
                public const string Id = "D";
                public const string Descripcion = "DEVOLUCION";
            }
        }

        public struct Sexo
        {
            public const string Masculino = "M";
            public const string Femenino = "F";
        }

        public struct Gobierno
        {
            public const string GobiernoNacional = "1";
            public const string GobiernoRegional = "2";
        }

        public struct SiNo
        {
            public const string Si = "SI";
            public const string No = "NO";
        }

        public struct TipoExpediente
        {
            public const string Fisico = "FISICO";
            public const string Virtual = "VIRTUAL";
        }

        public struct GradoAcademico
        {
            public const string Bachiller = "1";
            public const string Titulado = "2";
            public const string MagisterDoctorado = "3";
        }

    }

}
