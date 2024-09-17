using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Data.Coordinador;
using MEF.PROYECTO.Entity.Coordinador;

namespace MEF.PROYECTO.BusinessLayer.Coordinador
{
    public class Cls_Rule_Achivo
    {
        private static Cls_Dat_Archivo ODatos = new Cls_Dat_Archivo();
        public static Cls_Ent_Archivo InsertAchivoSustento(Cls_Ent_Archivo entidad)
        {
            return ODatos.InsertAchivoSustento(entidad);
        }
        public static List<Cls_Ent_Archivo> ListaArchivoSustento(Cls_Ent_Archivo entidad)
        {
            return ODatos.ListaArchivoSustento(entidad);
        }
    }
}
