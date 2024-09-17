using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace MEF.PROYECTO.Entity.Coordinador
{
    public class Cls_Ent_Requisitos_Solicitud
    {
        public int ID_SOLICITUD { get; set; }
        public int ID_PERSONAL { get; set; }
        public int ID_PERFIL { get; set; }
        public string PERFIL { get; set; }
        public string DETALLE { get; set; }
        public int ID_REQUISITO { get; set; }
        public string FLG_VALIDADO { get; set; }

        public List<SelectListItem> ListaCalificacion { get; set; }
    }
}
