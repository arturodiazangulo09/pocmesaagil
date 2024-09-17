using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Coordinador
{
   public class Cls_Ent_Conocimientos : Cls_Ent_Base
    {
        public int ID_CONOCIMIENTOS { get; set; }
        public int ID_SOLICITUD { get; set; }
        public string DESC_CONOCIMIENTO { get; set; }
        public List<Cls_Ent_Conocimientos> Items { get; set; }

    }
}
