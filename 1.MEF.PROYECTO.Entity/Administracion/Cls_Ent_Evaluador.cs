using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
using System.Web.Mvc;
namespace MEF.PROYECTO.Entity.Administracion
{
   public  class Cls_Ent_Evaluador: Cls_Ent_Base
    {
        public int ID_EVALUADOR { get; set; }
        public string  DESC_EVALUADOR { get; set; }
    }
}
