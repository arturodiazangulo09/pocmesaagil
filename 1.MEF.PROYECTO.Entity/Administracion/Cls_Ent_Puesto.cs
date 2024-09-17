using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
using System.Web.Mvc;
namespace MEF.PROYECTO.Entity.Administracion
{
    public class Cls_Ent_Puesto: Cls_Ent_Base
    {
        public int ID_PUESTO { get; set; }
        public int ID_ENTIDAD { get; set; }
        public string DES_PUESTO { get; set; }
        public int TIPO_FICHA { get; set; }
        public List<SelectListItem> ListaFicha { get; set; }
        public int P_TOTAL { get; set; }
        public decimal MONTO_PUESTO { get; set; }
        public List<SelectListItem> ListaP_H_1_1 { get; set; }
        public int P_H_1_1 { get; set; }
        public List<SelectListItem> ListaP_H_1_2 { get; set; }
        public int P_H_1_2 { get; set; }
        public List<SelectListItem> ListaP_H_1_3 { get; set; }
        public int P_H_1_3 { get; set; }
        public List<SelectListItem> ListaP_H_2_1 { get; set; }
        public int P_H_2_1 { get; set; }
        public List<SelectListItem> ListaP_H_2_2 { get; set; }
        public int P_H_2_2 { get; set; }
        public List<SelectListItem> ListaP_H_3_1 { get; set; }
        public int P_H_3_1 { get; set; }
        public List<SelectListItem> ListaP_H_3_2 { get; set; }
        public int P_H_3_2 { get; set; }
        public List<SelectListItem> ListaP_I_1_1 { get; set; }
        public int P_I_1_1 { get; set; }
        public List<SelectListItem> ListaP_I_2_1 { get; set; }
        public int P_I_2_1 { get; set; }
        public List<SelectListItem> ListaP_I_3_1 { get; set; }
        public int P_I_3_1 { get; set; }
        public List<SelectListItem> ListaP_I_4_1 { get; set; }
        public int P_I_4_1 { get; set; }
        public List<SelectListItem> ListaPuntaje { get; set; }
        public string NOMBRE_ARCHIVO_PUESTO { get; set; }
        public long ARCHIVO_PUESTO { get; set; }
        public string DESC_TIPO_FICHA { get; set; }

        
    }
}
