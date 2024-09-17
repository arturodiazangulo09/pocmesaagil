using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Experiencia: Cls_Ent_Base
    {
        public long ID_EXPERIENCIA { get; set; }
        public long ID_SOLICITUD { get; set; }
        public string DESC_EXPERIENCIA { get; set; }
        public int ID_TIPO_EXPERIENCIA { get; set; }
        public int ANOS { get; set; }
        public int ID_TIPO_SECTOR { get; set; }
        public string DESC_TIPO_EXPERIENCIA { get; set; }
        public string DESC_TIPO_SECTOR { get; set; }
        public List<Cls_Ent_Experiencia> Items { get; set; }
    }
}
