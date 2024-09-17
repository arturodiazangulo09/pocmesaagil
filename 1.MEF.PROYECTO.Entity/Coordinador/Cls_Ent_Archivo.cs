using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Archivo : Cls_Ent_Base
    {
        public int ID_ARCHIVO { get; set; }
        public byte[] ARCHIVO { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public string EXTENSION { get; set; }
        public string RUTA_ARCHIVO { get; set; }
    }
}
