using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Contacto : Cls_Ent_Base
    {
        public int ID_COORDINADOR { get; set; }
        public int ID_CONTACTO { get; set; }
        public string TELEFONO { get; set; }
        public string ANEXO { get; set; }
        public string CELULAR { get; set; }
        public string CORREO { get; set; }
    }
}
