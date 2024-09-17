using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Documento: Cls_Ent_Base
    {
        public long ID_DOCUMENTO { get; set; }
        public long ID_PROCESO { get; set; }
        public string FLG_TIPO { get; set; }
        public string DES_DOCUMENTO { get; set; }
        public long ID_LF { get; set; }
        public string RUTA_ARCHIVO { get; set; }
        public string NOM_DOCUMENTO { get; set; }
        public List<Cls_Ent_Documento> Items { get; set; }
    }
}
