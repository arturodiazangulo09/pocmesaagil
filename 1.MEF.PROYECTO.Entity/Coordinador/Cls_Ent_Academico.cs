using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Academico: Cls_Ent_Base
    {
        public long ID_ACADEMICA { get; set; }
        public long ID_SOLICITUD { get; set; }
        public string DESC_ACADEMICA { get; set; }
        public int ID_NIVEL_GRADO { get; set; }
        public string NOMBRE_NIVEL { get; set; }
        public List<Cls_Ent_Academico> Items { get; set; }
    }
}
