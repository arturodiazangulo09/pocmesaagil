namespace MEF.PROYECTO.Utilitario
{
    public class JsonResponse
    {
        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }

        private string m_Message;
        public bool Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private bool m_Success;
        public object Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }

        private object m_Data;

        private string m_tipo;
        public string Tipo
        {
            get { return m_tipo; }
            set { m_tipo = value; }
        }

    }
}
