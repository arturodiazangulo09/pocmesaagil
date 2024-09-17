using MEF.PROYECTO.Entity.Administracion;
using MEF.PROYECTO.Entity.Coordinador;
using MEF.PROYECTO.Entity.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Models
{
    public class WizNuevaSolicitudViewModel
    {
        public Cls_Ent_Solicitud Solicitud { get; set; }
        public IEnumerable<Cls_Ent_Entidades> Entidades { get; set; }
        public IEnumerable<Cls_Ent_Grado_Academico> GradosAcademicos { get; set; }
        public IEnumerable<Cls_Ent_Tipo_Experiencia> ExperienciaTipos { get; set; }
        public IEnumerable<Cls_Ent_Tipo_Sector_Experiencia> SectorExperienciaTipos { get; set; }
    }
}