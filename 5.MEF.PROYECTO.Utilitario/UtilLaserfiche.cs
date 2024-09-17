using Laserfiche.DocumentServices;
using Laserfiche.RepositoryAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF.PROYECTO.Entity;
using System.Web.Configuration;
using System.IO;

namespace MEF.PROYECTO.Utilitario
{
    public static class UtilLaserfiche
    {
        private static Session IniciarSesion(string Servidor, string Repositorio, string Usuario, string IP)
        {
            String usuarioLF1 = "";
            String clave1 = "";
            Session session = null;

            try
            {
                usuarioLF1 = WebConfigurationManager.AppSettings["UserLaserfiche"].ToString();
                clave1 = WebConfigurationManager.AppSettings["PasswordLaserfiche"].ToString();
                session = new Session();
                session.LogIn(usuarioLF1, clave1, new RepositoryRegistration(Servidor, Repositorio));
                Log.MensajeLog("User: " + Usuario + "[" + IP + "] - LASERFICHE 1", "UtilLaserfiche.IniciarSesion");
            }
            catch (Exception ex)
            {
                Log.MensajeLog("User: " + Usuario + "[" + IP + "] - LASERFICHE 1", "UtilLaserfiche.IniciarSesion");
                Log.Mensaje(ex);
            }
            return session;
        }
        public static byte[] ExportarDocumentoPDF(int IdArchivo, string Servidor, string Repositorio, string Usuario, string Carpeta, ref string  nombreArchivo, string IP)
        {
            byte[] valorDevuelve = null;
            Session session = new Session();
            try
            {
                session = IniciarSesion(Servidor, Repositorio, Usuario, IP);
                if (session != null)
                {
                    DocumentExporter DocEx = new DocumentExporter();
                    DocumentInfo documentInfo = Document.GetDocumentInfo(IdArchivo, session);
                    nombreArchivo = Guid.NewGuid() + "." + documentInfo.Extension;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        DocEx.ExportElecDoc(documentInfo, ms);
                        valorDevuelve = ms.ToArray();
                    }

                }
            }
            catch (Exception err)
            {
                Log.Mensaje(err);
            }
            finally
            {
                try
                {
                    session.LogOut();
                }
                catch (Exception ex)
                {
                    Log.Mensaje(ex);
                }
            }

            return valorDevuelve;
        }
        public static int SubirArchivoSubSubCarpeta(string vFile, string Servidor, string Repositorio, string Usuario, string Carpeta, string Volumen, string SubCarpeta, string SubSubCarpeta, string NombreArchivo, string IP)
        {
            int IdLaserFiche = 0;
            Session session = new Session();
            try
            {
                vFile = vFile.Trim();
                session = IniciarSesion(Servidor, Repositorio, Usuario, IP);
                if (session != null)
                {
                    DocumentImporter importer = new DocumentImporter() { Document = new DocumentInfo(session), OverwritePages = true, ExtractTextFromEdoc = true };

                    /* VALIDAMOS LA SUB CARPETA*/
                    Search lfSearch1 = new Search(session);
                    lfSearch1.Command = "{LF:Name=\"" + SubCarpeta + "\", Type=\"F\"} & {LF:LOOKIN=\"" + Repositorio + "\\" + Carpeta + "\"}";
                    lfSearch1.Run();
                    SearchListingSettings searchSettings1 = new SearchListingSettings();
                    searchSettings1.SortDirection = SortDirection.Ascending;
                    SearchResultListing results1 = lfSearch1.GetResultListing(searchSettings1);

                    if (results1.RowCount <= 0)
                    {
                        //No Existe, Creamos la SubCarpeta
                        FolderInfo carpetaContent = Folder.GetFolderInfo("\\" + Carpeta, session);
                        FolderInfo carpetaExpediente = new FolderInfo(session);
                        carpetaExpediente.Create(carpetaContent, SubCarpeta, EntryNameOption.None);
                        carpetaExpediente.Unlock();
                        carpetaExpediente.Refresh(true);
                    }

                    /* VALIDAMOS LA SUB-SUBCARPETA*/
                    if (SubSubCarpeta.Contains("\\"))
                    {
                        String[] carpetas = SubSubCarpeta.Split('\\');
                        String SubSubAuxiliar = "";
                        string SubBuscar = "";
                        if (carpetas.Length > 0)
                        {
                            for (int i = 0; i < carpetas.Length; i++)
                            {
                                SubSubAuxiliar += carpetas[i];
                                Search lfSearch2 = new Search(session);
                                if (i == 0)
                                {
                                    lfSearch2.Command = "{LF:Name=\"" + carpetas[i] + "\", Type=\"F\"} & {LF:LOOKIN=\"" + Repositorio + "\\" + Carpeta + "\\" + SubCarpeta + "\"}";
                                }
                                else
                                {
                                    SubBuscar += carpetas[i - 1];
                                    lfSearch2.Command = "{LF:Name=\"" + carpetas[i] + "\", Type=\"F\"} & {LF:LOOKIN=\"" + Repositorio + "\\" + Carpeta + "\\" + SubCarpeta + "\\" + SubBuscar + "\"}";
                                    SubBuscar += "\\";
                                }
                                lfSearch2.Run();
                                SearchListingSettings searchSettings2 = new SearchListingSettings();
                                searchSettings2.SortDirection = SortDirection.Ascending;
                                SearchResultListing results2 = lfSearch2.GetResultListing(searchSettings2);

                                if (results2.RowCount <= 0)
                                {
                                    //No existe, creamos ls SubSubCarpeta
                                    FolderInfo carpetaContent = Folder.GetFolderInfo("\\" + Carpeta + "\\" + SubCarpeta, session);
                                    FolderInfo carpetaExpediente = new FolderInfo(session);

                                    if (SubSubAuxiliar.Contains("\\"))
                                    {
                                        String[] carpetas2 = SubSubCarpeta.Split('\\');
                                        carpetaContent = Folder.GetFolderInfo("\\" + Carpeta + "\\" + SubCarpeta + '\\' + carpetas2[0], session);
                                    }

                                    //carpetaExpediente.Create(carpetaContent, SubSubAuxiliar, EntryNameOption.None);
                                    carpetaExpediente.Create(carpetaContent, carpetas[i], EntryNameOption.None);
                                    carpetaExpediente.Unlock();
                                    carpetaExpediente.Refresh(true);
                                }
                                SubSubAuxiliar += "\\";
                            }
                        }
                    }
                    else
                    {
                        Search lfSearch = new Search(session);
                        lfSearch.Command = "{LF:Name=\"" + SubSubCarpeta + "\", Type=\"F\"} & {LF:LOOKIN=\"" + Repositorio + "\\" + Carpeta + "\\" + SubCarpeta + "\"}";
                        lfSearch.Run();
                        SearchListingSettings searchSettings = new SearchListingSettings();
                        searchSettings.SortDirection = SortDirection.Ascending;
                        SearchResultListing results = lfSearch.GetResultListing(searchSettings);

                        if (results.RowCount <= 0)
                        {
                            //No existe, creamos la SubSubCarpeta
                            FolderInfo carpetaContent = Folder.GetFolderInfo("\\" + Carpeta + "\\" + SubCarpeta, session);
                            FolderInfo carpetaExpediente = new FolderInfo(session);
                            carpetaExpediente.Create(carpetaContent, SubSubCarpeta, EntryNameOption.None);
                            carpetaExpediente.Unlock();
                            carpetaExpediente.Refresh(true);
                        }
                    }

                    importer.Document.Create(Folder.GetFolderInfo("\\" + Carpeta + "\\" + SubCarpeta + "\\" + SubSubCarpeta, session), NombreArchivo, Volumen, EntryNameOption.AutoRename);
                    importer.Document.Extension = vFile.Substring(vFile.LastIndexOf(".") + 1, vFile.Length - (vFile.LastIndexOf(@".") + 1));
                    importer.GetType();

                    importer.ImportEdoc("application/vnd.ms-word", vFile);
                    importer.Document.Save();
                    session.LogOut();
                    IdLaserFiche = importer.Document.Id;
                    if (System.IO.File.Exists(vFile))
                    {
                        File.Delete(vFile);
                    }

                }
                else
                {
                    IdLaserFiche = 0;
                }
            }
            catch (Exception EX)
            {
                session.LogOut();
                Log.Mensaje(EX);
                try
                {
                    session.LogOut();
                }
                catch (Exception ex)
                {
                    Log.Mensaje(ex);
                }
            }
            return IdLaserFiche;
        }
        public static bool EliminarArchivo(int IdArchivo, string Servidor, string Repositorio, string Usuario, string Clave)
        {
            Session session = new Session();
            session.LogIn(Usuario, Clave, new RepositoryRegistration(Servidor, Repositorio));
            EntryInfo myEntry = Entry.GetEntryInfo(IdArchivo, session);
            myEntry.Lock(LockType.Exclusive);
            myEntry.Delete();
            myEntry.Save();
            myEntry.Unlock();
            myEntry.Dispose();
            session.LogOut();
            return true;
        }
        public static bool MoverArchivo(int IdArchivo, string Servidor, string Repositorio, string Usuario, string Clave, string Carpeta, string newSubCarpeta, string nombreArchivo)
        {
            Session session = new Session();
            session.LogIn(Usuario, Clave, new RepositoryRegistration(Servidor, Repositorio));
            EntryInfo myEntry = Entry.GetEntryInfo(IdArchivo, session);
            myEntry.Lock(LockType.Exclusive);

            bool valor = true;
            try
            {
                valor = Folder.GetFolderInfo("\\" + Carpeta + "\\" + newSubCarpeta, session).IsNew;
            }
            catch (Exception err)
            {
                Console.WriteLine("ERROR: Al mover el archivo escaneado... " + err.ToString());
            }
            if (valor)
            {
                FolderInfo carpetaContent = Folder.GetFolderInfo("\\" + Carpeta, session);
                FolderInfo carpetaExpediente = new FolderInfo(session);
                valor = carpetaExpediente.IsNew;
                carpetaExpediente.Create(carpetaContent, newSubCarpeta, EntryNameOption.None);
                carpetaExpediente.Unlock();
                carpetaExpediente.Refresh(true);
            }

            try
            {

                string nameArch = "";
                nameArch = "\\" + Carpeta + "\\" + newSubCarpeta + "\\" + nombreArchivo;
                myEntry.MoveTo(nameArch, EntryNameOption.AutoRename);
                myEntry.Save();
                myEntry.Unlock();
                myEntry.Dispose();

            }
            catch (Exception err)
            {
                Console.WriteLine("ERROR: Al mover el archivo escaneado... " + err.ToString());
            }


            session.LogOut();
            return true;
        }
        public static string CrearCarpeta(string rutaPadre, string Carpeta, string file1, string file2, string Servidor, string Repositorio, string Usuario, string Clave)
        {
            string RutaFin = string.Empty;
            try
            {
                Session session = new Session();
                session.LogIn(Usuario, Clave, new RepositoryRegistration(Servidor, Repositorio));
                FolderInfo carpetaContent = Folder.GetFolderInfo(rutaPadre, session);
                FolderInfo carpetaDocAdm = new FolderInfo(session);
                FolderInfo carpetaDocGen = new FolderInfo(session);
                FolderInfo carpetaExpediente = new FolderInfo(session);
                carpetaExpediente.Create(carpetaContent, Carpeta, EntryNameOption.None);
                carpetaExpediente.Unlock();
                carpetaExpediente.Refresh(true);

                carpetaDocAdm.Create(carpetaExpediente, file1, EntryNameOption.None);
                carpetaDocAdm.Unlock();
                carpetaDocAdm.Refresh(true);
                carpetaDocGen.Create(carpetaExpediente, file2, EntryNameOption.None);
                carpetaDocGen.Unlock();
                carpetaDocGen.Refresh(true);
            }
            catch (Exception)
            {
                throw;
            }

            return rutaPadre + "\\" + Carpeta;
        }
   
    }
}
