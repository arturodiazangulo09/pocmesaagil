using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
using System.Web.Mvc;
namespace MEF.PROYECTO.Entity.Administracion
{
    public class Cls_Ent_Entidades: Cls_Ent_Base
    {
        public int ID_ENTIDAD { get; set; }
        public int ID_PERIODO { get; set; }
        public string DESC_ENTIDAD { get; set; }
        public string DESC_UNIDAD { get; set; }
        public string FLG_FAG { get; set; }
        public string FLG_PAC { get; set; }
        public string TIPO_PROCESO { get; set; }

        public string ANIO_PERIODO { get; set; }
        public DateTime FEC_PERIODO_INI { get; set; }
        public DateTime FEC_PERIODO_FIN { get; set; }
        public decimal MONTO_MENSUAL { get; set; }
        public decimal MONTO_TOTAL { get; set; }
        public string DES_MES { get; set; }
        public int NUM_MES { get; set; }
        //public byte[] ARCHIVO { get; set; }
        public long ARCHIVO { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public int ID_PADRE_ENTIDAD { get; set; }
        public int CANT_DEPENDENCIA { get; set; }
        public int ID_PERIODO_PADRE { get; set; }

        public string RUC { get; set; }
        public string DIRECCION { get; set; }
        public List<SelectListItem> ListaDpto { get; set; }
        public string COD_DEPA { get; set; }
        public string COD_PROV { get; set; }
        public string COD_DIST { get; set; }
        public string REPRESENTANTE { get; set; }
        public string TIPO_RESOLUCION { get; set; }
        public string NUMERO_RESOLUCION { get; set; }
        public string CARGO_AUTORIDAD { get; set; }
        public string DNI_AUTORIDAD { get; set; }
        public string EVALUADOR { get; set; }
        public string DES_DEPARTAMENTO { get; set; }
        public string DES_PROVINCIA { get; set; }
        public string DES_DISTRITO { get; set; }
        public string USU_CONSULTOR { get; set; }
        public string FEC_PERIODO_INI_STRING { get; set; }
        public string FEC_PERIODO_FIN_STRING { get; set; }
        public string ACRONIMO { get; set; }

    }
}
