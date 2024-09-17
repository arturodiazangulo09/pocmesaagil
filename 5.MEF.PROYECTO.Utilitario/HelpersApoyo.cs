namespace MEF.PROYECTO.Utilitario
{
    public static class HelpersApoyo
    {
        public static string fContentType(string istrExt)
        {
            string Formato = string.Empty;
            switch (istrExt.ToLower())
            {

                case ".gif":
                    Formato = "image/gif";
                    break;
                case ".jpg":
                    Formato = "image/jpeg";
                    break;
                case ".jpe":
                    Formato = "image/jpeg";
                    break;
                case ".jpeg":
                    Formato = "image/jpeg";
                    break;
                case ".bmp":
                    Formato = "image/bmp";
                    break;
                case ".tiff":
                    Formato = "image/tiff";
                    break;
                case ".doc":
                    Formato = "application/msword";
                    break;
                case ".rtf":
                    Formato = "application/rtf";
                    break;
                case ".xls":
                    Formato = "application/x-excel";
                    break;
                case ".ppt":
                    Formato = "application/ms-powerpoint";
                    break;
                case ".pdf":
                    Formato = "application/pdf";
                    break;
                case ".zip":
                    Formato = "application/zip";
                    break;
                default:
                    Formato = "text/plain";
                    break;
            }
            return Formato;
        }

    }
}
