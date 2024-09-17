using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace APP.MEF.ADMINISTRAR.FAG.PAG.Controllers
{
    public class CaptchaController : Controller
    {
        // GET: Captcha
        private static Random Randomizer = new Random(DateTime.Now.Second);
        public byte[] ImageAsByteArray { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public void VisorLogin(int id)
        {
            string Clave = string.Empty;
            Clave = GenerarCapcha();
            Session["ClaveIntraCapchaFagPagLogin"] = Clave;
            Font[] fonts = {
            new Font("Arial", 24, FontStyle.Bold),
            new Font("Arial", 24, FontStyle.Bold),
            new Font("Arial", 24, FontStyle.Bold),
            new Font("Arial", 24,  FontStyle.Bold) };
            using (var bmp = new Bitmap(180, 60))
            {
                using (var graphic = Graphics.FromImage(bmp))
                {
                    using (var hb = new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.Silver, Color.White)) graphic.FillRectangle(hb, 0, 0, bmp.Width, bmp.Height);
                    for (int i = 0; i < Clave.Length; i++)
                    {
                        var point = new PointF(15 + (i * 25), 25);
                        graphic.DrawString(Clave.Substring(i, 1), fonts[Randomizer.Next(0, 4)], Brushes.Gray, point, new StringFormat { LineAlignment = StringAlignment.Center });
                    }
                }
                using (var stream = new MemoryStream())
                {
                    bmp.Save(stream, ImageFormat.Png);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.AddHeader("Content-Length", stream.GetBuffer().Length.ToString());
                    Response.ContentType = "image/Jpeg";
                    Response.BinaryWrite(stream.GetBuffer()); ;
                    Response.End();
                }
            }
            try
            {
                Response.Flush();
                Response.Close();
            }
            catch (Exception) { }
        }
        private string GenerarCapcha()
        {
            string Clave = string.Empty;
            Random oAzar = new Random();
            int N = 0;
            N = oAzar.Next(65, 90);
            Char C1 = Convert.ToChar(N);
            N = oAzar.Next(48, 57);
            Char C2 = Convert.ToChar(N);
            N = oAzar.Next(65, 90);
            Char C3 = Convert.ToChar(N);
            N = oAzar.Next(48, 57);
            Char C4 = Convert.ToChar(N);
            N = oAzar.Next(65, 90);
            Char C5 = Convert.ToChar(N);
            N = oAzar.Next(48, 57);
            Char C6 = Convert.ToChar(N);
            N = oAzar.Next(65, 90);
            Char C7 = Convert.ToChar(N);
            StringBuilder sb = new StringBuilder(C1);
            sb.Append(C2);
            sb.Append(C3);
            sb.Append(C4);
            sb.Append(C5);
            sb.Append(C6);

            Clave = sb.ToString();
            return Clave;
        }
    }
}