using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity.Base;
namespace MEF.PROYECTO.Entity.Coordinador
    
{
    public class Cls_Ent_Coordinador : Cls_Ent_Base
    {
        public string NOMBRES { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string DNI { get; set; }
        public string USUARIO { get; set; }
        public string CONTRASENA { get; set; }
        public string NUM_DOCUMENTO { get; set; }
        public string CARGO { get; set; }
        public string DOCUMENTO_ACRE { get; set; }
        public string CORREO_NOTIFICADOR { get; set; }
        public string DIRECCION_ENT { get; set; }
        public string NOMBRE_ARCHIVO_DNI { get; set; }
        public string NOMBRE_ARCHIVO_SUS { get; set; }
        public int ID_COORDINADOR { get; set; }
        public int ID_ENTIDAD { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public int ID_TIPO_VINCULO_ENT { get; set; }
        public string COD_DEPARTAMENTO_ENT { get; set; }
        public string COD_PROVINCIA_ENT { get; set; }
        public string COD_DISTRITO_ENT { get; set; }
        public long ARCHIVO_DNI { get; set; }
        public long ARCHIVO_SUSTENTO { get; set; }
        public List<Cls_Ent_Contacto> listaContacto { get; set; }
        public string FLG_SOLICITUD { get; set; }
        public string FLG_CAMBIO_CLAVE { get; set; }
        public string FEC_REGISTRO { get; set; }
        public string DESC_ENTIDAD { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string DESC_VINCULO_LAB { get; set; }
        public string DES_DEPARTAMENTO { get; set; }
        public string DES_PROVINCIA { get; set; }
        public string DES_DISTRITO { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string FLG_FAG { get; set; }
        public string FLG_PAC { get; set; }
        public string TIPO_USUSARIO { get; set; }
        public string DESC_TIPO_USUSARIO { get; set; }
        public string DEPENDENCIA { get; set; }
        public string OBSERVACION_SOLICITUD { get; set; }
        public string FEC_EVALUACION { get; set; }
        public string DESC_ENTIDAD_PRINCIPAL { get; set; }
        

    }
}
