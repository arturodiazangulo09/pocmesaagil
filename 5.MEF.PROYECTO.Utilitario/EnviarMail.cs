using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
//using System.Web.HttpContext.Current.Server;
namespace MEF.PROYECTO.Utilitario
{
    public class EnviarMail
    {

        public static bool SendMailMessage(ref string strError, string recepient, string bcc, string cc, string subject, string body, string strRutaAdjunto = "", string sfrom = "")
        {
            string[] arrRecepient = null;

            if (recepient.IndexOf(",") > -1)
            {
                arrRecepient = recepient.Split(',');
            }
            else if (recepient.IndexOf(";") > -1)
            {
                arrRecepient = recepient.Split(';');
            }
            else
            {
                arrRecepient = new string[1];
                arrRecepient[0] = recepient;
            }
            System.IO.Stream streamArchivo = null;
            return SendMailMessage_Private(ref strError, arrRecepient, bcc, cc, subject, body, ref streamArchivo, "", strRutaAdjunto, sfrom);
        }
        public static bool SendMailMessage(ref string strError, string[] recepient, string bcc, string cc, string subject, string body, string strRutaAdjunto = "", string sfrom = "")
        {
            System.IO.Stream streamArchivo = null;
            return SendMailMessage_Private(ref strError, recepient, bcc, cc, subject, body, ref streamArchivo, "", strRutaAdjunto, sfrom);
        }
        private static bool SendMailMessage_Private(ref string strError, string[] recepient, string bcc, string cc, string subject,
                                                    string body, ref System.IO.Stream streamArchivo, string strNomArchivo, string strRutaAdjunto,
                                                    string sfrom)
        {
            try
            {
                MailMessage mMailMessage = new MailMessage();
                SmtpClient mSmtpClient = new SmtpClient();

                if ((sfrom != null) & sfrom != string.Empty)
                {
                    if (!string.IsNullOrWhiteSpace(sfrom))
                        mMailMessage.From = new MailAddress(sfrom);
                }

                foreach (string unmail in recepient)
                {
                    if (!string.IsNullOrWhiteSpace(unmail))
                        mMailMessage.To.Add(new MailAddress(unmail));
                }

                if ((bcc != null) & bcc != string.Empty)
                {
                    string[] correos = bcc.Split(';');
                    foreach (string correo in correos)
                    {
                        mMailMessage.Bcc.Add(new MailAddress(correo));
                    }
                }

                if ((cc != null) & cc != string.Empty)
                {
                    if (!string.IsNullOrWhiteSpace(cc))
                        mMailMessage.CC.Add(new MailAddress(cc));
                }

                if (mMailMessage.To.Count == 0 & mMailMessage.CC.Count == 0 & mMailMessage.Bcc.Count == 0)
                {
                    return false;
                }

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s
                   , X509Certificate certificate
                   , X509Chain chain
                   , SslPolicyErrors sslPolicyErrors)

                { return true; };

                mMailMessage.Subject = subject;
                mMailMessage.Body = body;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Html);
                try
                {
                    LinkedResource imgLogo = new LinkedResource(System.Web.Hosting.HostingEnvironment.MapPath("~/assets/img/") + "mef.jpg", MediaTypeNames.Image.Jpeg);
                    imgLogo.ContentId = "imagenLogo";
                    htmlView.LinkedResources.Add(imgLogo);


                    LinkedResource imgPPPT = new LinkedResource(System.Web.Hosting.HostingEnvironment.MapPath("~/assets/img/") + "logo.png", MediaTypeNames.Image.Jpeg);
                    imgPPPT.ContentId = "imagenLogoApli";
                    htmlView.LinkedResources.Add(imgPPPT);

                    mMailMessage.AlternateViews.Add(htmlView);

                    
                }
                catch (Exception err)
                {

                    Console.WriteLine("ERROR: Al exportar el archivo escaneado... " + err.ToString());
                }

                mMailMessage.IsBodyHtml = true;
                mMailMessage.Priority = MailPriority.Normal;
                if (!string.IsNullOrWhiteSpace(strRutaAdjunto))
                {
                    System.Net.Mail.Attachment adjunto = new System.Net.Mail.Attachment(strRutaAdjunto);
                    mMailMessage.Attachments.Add(adjunto);
                }

                if ((streamArchivo != null) & !(string.IsNullOrWhiteSpace(strNomArchivo)))
                {
                    System.Net.Mail.Attachment adjunto2 = new System.Net.Mail.Attachment(streamArchivo, strNomArchivo);
                    mMailMessage.Attachments.Add(adjunto2);
                }

                mSmtpClient.Host = ConfigurationManager.AppSettings.Get("IpMail");
                //mSmtpClient.Port = 25;
                string UserMail, ClaveMail;
                UserMail = ConfigurationManager.AppSettings.Get("Usermail");
                ClaveMail = ConfigurationManager.AppSettings.Get("Clavemail");
                mSmtpClient.Credentials = new NetworkCredential(UserMail, ClaveMail);
                mSmtpClient.EnableSsl =Convert.ToBoolean(ConfigurationManager.AppSettings.Get("SslEmail")); 
                mSmtpClient.Send(mMailMessage);

                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message.ToString();
                Log.MensajeLog(ex.ToString(), "Error al realizar la notificaciones Externas.");
                return false;
            }
        }



    }
}