using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;

namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Curso: Cls_Ent_Base
    {
        public long ID_CURSOS_PRO { get; set; }
        public long ID_SOLICITUD { get; set; }
        public string DESC_CURSO_PRO { get; set; }
        public List<Cls_Ent_Curso> Items { get; set; }
    }
}
