using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Archivo_STD: Cls_Ent_Base
    {
        public long ID_SOLICITUD { get; set; }
        public int ID_ARCHIVO { get; set; }
        public string TIPO_PROCESO { get; set; }
        public string FORMATO { get; set; }
        public long NUMERAL { get; set; }
        
    }
}
