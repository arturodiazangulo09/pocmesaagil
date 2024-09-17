using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
using System.Web;
using System.Web.Mvc;
namespace MEF.PROYECTO.Entity.Personal
{
    public class Cls_Ent_Personal: Cls_Ent_Base
    {
        
        public int ID_INFORMACION { get; set; }
        public int ID_PERSONAL { get; set; }
        public string  USUARIO { get; set; }
        public string NUM_DOCUMENTO { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string CONTRASENA { get; set; }
        public string EMAIL { get; set; }
        public string FLG_CAMBIO_CLAVE { get; set; }

        public DateTime FECHA_NACIMIENTO { get; set; }
        public string ID_SEXO { get; set; }
        public string ID_TIPO_DOCUMENTO { get; set; }
        public string DOMICILIO { get; set; }
        public string TELEFONO { get; set; }
        public string CELULAR { get; set; }
        public string RUC { get; set; }
        public string ID_TIPO_NACIONALIDAD { get; set; }
        public string COD_DEPA_NAC { get; set; }
        public string COD_PROV_NAC { get; set; }
        public string COD_DIST_NAC { get; set; }
        public string DPTO_PROV_EXTRANJERO { get; set; }
        public string COD_DEPA { get; set; }
        public string COD_PROV { get; set; }
        public string COD_DIST { get; set; }
        public string DIRECCION { get; set; }
        public string ID_TIPO_BANCO { get; set; }
        public string CUENTA_BANCARIA { get; set; }
        public string CUENTA_CCI { get; set; }
        public string APE_NOMBRES_CONTACTO { get; set; }
        public string TELEFONO_CONTACTO { get; set; }
        public string CELULAR_CONTACTO { get; set; }
        public string FLG_DATOS_PERSONALES { get; set; }
        public string FLG_RETENCION_4 { get; set; }
        public string ESTADO_CIVIL { get; set; }
        
        public List<SelectListItem> ListaTipoDocumento { get; set; }
        public List<SelectListItem> ListaEstadoCivil { get; set; }
        public List<SelectListItem> ListaPais { get; set; }
        public List<SelectListItem> ListaDptoNac { get; set; }
        public List<SelectListItem> ListaDptoDom { get; set; }
        public List<SelectListItem> ListaBanco { get; set; }
        public string TITULOS { get; set; }
        public string GRADOS { get; set; }
        public string APELLIDO_NOMBRES { get; set; }
        public string DESC_NACIONALIDAD { get; set; }

        public string DES_INFORMACION { get; set; }
        public string ENTIDAD { get; set; }
        public string PERSONA { get; set; }

    }
}
