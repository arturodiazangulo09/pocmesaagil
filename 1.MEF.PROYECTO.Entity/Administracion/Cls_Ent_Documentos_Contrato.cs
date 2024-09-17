using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
using System.Web.Mvc;

namespace MEF.PROYECTO.Entity.Administracion
{
    public class Cls_Ent_Documentos_Contrato: Cls_Ent_Base
    {
        public string TIPO_PROCESO { get; set; }
        public long ANIO_CONTRATO { get; set; }
        public long NUM_CONTRATO { get; set; }
        public string ASUNTO { get; set; }
        public string FORMATO { get; set; }
        public long NUMERAL { get; set; }
        public long ID_CONTRATO { get; set; }
        public long ID_ENTIDAD { get; set; }
        public long ID_ARCHIVO { get; set; }
    }
}
