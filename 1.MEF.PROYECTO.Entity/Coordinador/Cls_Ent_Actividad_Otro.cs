using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Coordinador
{
   public class Cls_Ent_Actividad_Otro : Cls_Ent_Base
    {
        public int ID_OTRO_ACT { get; set; }
        public int ID_SOLICITUD { get; set; }
        public string DESC_ACT_OTRO { get; set; }
        public List<Cls_Ent_Actividad_Otro> Items { get; set; }
    }
}
