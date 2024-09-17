using System;

namespace MEF.PROYECTO.Entity.Base
{
    public class Cls_Ent_Base
    {
        public int FLG_ESTADO { get; set; }
        public string USU_INGRESO { get; set; }
        public DateTime FEC_INGRESO { get; set; }
        public string USU_MODIFICA { get; set; }
        public DateTime FEC_MODIFICA { get; set; }
        public long CODIGO { get; set; }
        public string ID { get; set; }
        public string IP_PC { get; set; }
        public long ID_USUARIO { get; set; }
        /// <summary>
        /// -------- PARA MOSTRAR MENSAJES EN EL INSERT, UPDATE, DELETE DE LOS REGISTROS. --------
        /// </summary>
        public bool FLG_OK { get; set; }
        public long ID_ERROR { get; set; }
        public string DES_ERROR { get; set; }
        /// <summary>
        /// Para el manejo del estado del registro, C (Lectura desde la BD), I (Insert), U (Update)
        /// </summary>
        public string ACCION { get; set; }
        public string ACCION_FILE { get; set; }
        public string IP_INGRESO { get; set; }
        public string DESCRIPCION { get; set; }
        public string FLG_PROCESO { get; set; }
        public string NR_OFICIO { get; set; }
        public string ASUNTO { get; set; }
        public int NR_FOLIOS { get; set; }

    }
}