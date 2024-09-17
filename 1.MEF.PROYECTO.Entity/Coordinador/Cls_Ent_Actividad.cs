using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Coordinador
{
   public class Cls_Ent_Actividad : Cls_Ent_Base
    {
        public int ID_ACTIVIDAD { get; set; }
        public int ID_SOLICITUD { get; set; }
        public string DESC_ACTIVIDAD { get; set; }
        public List<Cls_Ent_Actividad> Items { get; set; }


    }
}
