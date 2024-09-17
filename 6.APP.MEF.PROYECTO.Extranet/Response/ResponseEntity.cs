using System;
using System.Collections.Generic;

namespace APP.MEF.EXTRANET.FAG.PAG.Response
{
    public class ResponseEntity
    {
        public string extra { get; set; }
        public string extra2 { get; set; }
        public String message { get; set; }
        public String message2 { get; set; }
        public long cantidad { get; set; }
        public string codigo { get; set; }
        public long idDocumento { get; set; }
        public long idMovimientoDoc { get; set; }
        public long idAsignacion { get; set; }
        public DateTime dtFecha { get; set; }
        public int nivel { get; set; }


        public virtual bool success { get; set; }

        #region "Métodos"
        public void SetMessage(Exception ex)
        {
            success = false;
            message = ex.Message + Environment.NewLine + (ex.InnerException != null ? ex.InnerException.Message : String.Empty);
        }

        public void SetMessage(String message)
        {
            this.message = message;
        }

        public void SetMessage(params string[] messages)
        {
            foreach (string message in messages)
            {
                this.message += message + Environment.NewLine;
            }
        }

        #endregion "Métodos"
    }


    public sealed class RespuestaEntity<T> : ResponseEntity //where T : class
    {
        private bool _success;

        #region "Métodos"
        public override bool success { get { if ((items != null && items.Count > 0) || items != null || item != null) return true; else return false; } set { _success = value; } }
        public T item { get; set; }
        public List<T> items { get; set; }
        public int total { get { return (items == null || items.Count == 0) ? 0 : items.Count - 1; } set { } }
        #endregion "Métodos"
    }
}