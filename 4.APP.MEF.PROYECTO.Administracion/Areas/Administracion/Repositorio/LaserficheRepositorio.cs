﻿using MEF.PROYECTO.Utilitario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Configuration;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Areas.Administracion.Repositorio
{
    public class LaserficheRepositorio
    {
        public int EnviarLaserficheSubCarpeta(string ruta, string SubCarpeta, string SubSubCarpeta, string nombreArchivo, string Usuario, string IP)
        {
            return UtilLaserfiche.SubirArchivoSubSubCarpeta(ruta, WebConfigurationManager.AppSettings["IPServidorLaserfiche"].ToString(), WebConfigurationManager.AppSettings["NameServidorLaserfiche"].ToString(),
                    Usuario, WebConfigurationManager.AppSettings["CarpetaLaserfiche"].ToString(),
                    WebConfigurationManager.AppSettings["VolumenLaserfiche"].ToString(), SubCarpeta, SubSubCarpeta, nombreArchivo, IP);
        }
    }
}