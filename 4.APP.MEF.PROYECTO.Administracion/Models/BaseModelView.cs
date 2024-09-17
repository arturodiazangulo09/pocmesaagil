using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace APP.ADMINISTRAR.FAG.PAG.Models
{
    public class BaseModelView
    {
        [Display(Name = "Usuario creación:")]
        public int USU_INGRESO { get; set; }
        [Display(Name = "Fecha creación:")]
        public DateTime FEC_INGRESO { get; set; }
        [Display(Name = "Usuario modificación:")]
        public int USU_MODIFICA { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha modificación:")]
        public DateTime FEC_MODIFICA { get; set; }

        public string URL_HOJATRAMITE { get; set; }

        public string URL_RESPUESTA { get; set; }

        public string URL_VALIDAR_DOCUMENTO { get; set; }

        public List<SelectListItem> ListaSistemas { get; set; }
    }
}