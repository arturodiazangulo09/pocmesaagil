using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
namespace APP.MEF.EXTRANET.FAG.PAG.Areas.Coordinador.Models
{
    [DataContract]
    public class StdModel
    {
        [DataMember]
        public string NUM_REGISTRO { get; set; }
        [DataMember]
        public string NUM_OFICIO { get; set; }
        [DataMember]
        public int NUM_FOLIOS { get; set; }
        [DataMember]
        public string ASUNTO { get; set; }
        [DataMember]
        public string CLASIFICACION { get; set; }
        [DataMember]
        public string RAZONSOCIAL { get; set; }
        [DataMember]
        public string RUC { get; set; }
        [DataMember]
        public string SEC_EJECT { get; set; }
        [DataMember]
        public string DIRECCION { get; set; }
        [DataMember]
        public string DEPARTAMENTO { get; set; }
        [DataMember]
        public string PROVINCIA { get; set; }
        [DataMember]
        public string DISTRITO { get; set; }
        [DataMember]
        public string CORREO { get; set; }
        public string OBSERVACION { get; set; }
        public WCF_STD22.anexoDto OFICIO { get; set; }
        public WCF_STD22.anexoDto RESUMEN { get; set; }
        public WCF_STD22.anexoDto[] ANEXOS { get; set; }
        [DataMember]
        public string REMOTEADDRESS { get; set; }
    }
    //public class anexoDto
    //{
    //    public byte[] archivo { get; set; }
    //    public long length { get; set; }
    //    public string name { get; set; }
    //}
}