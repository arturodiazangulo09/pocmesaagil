using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Configuration;
namespace MEF.PROYECTO.Utilitario
{
    public class ActiveDirectory
    {
     private readonly string sServer;
    private readonly string sDefaultOU;
    private readonly string sServiceUser;
    private readonly string sServicePassword;
    private readonly PrincipalContext pc;
    public  ActiveDirectory()
    {
        sServer = ConfigurationManager.AppSettings["ServerAD"].ToString();
        sDefaultOU = ConfigurationManager.AppSettings["DefaultOU"].ToString();
        sServiceUser = ConfigurationManager.AppSettings["UserAD"].ToString();
        sServicePassword = ConfigurationManager.AppSettings["PasswordAD"].ToString();
        pc = new PrincipalContext(ContextType.Domain, sServer, sDefaultOU, ContextOptions.SimpleBind, sServiceUser, sServicePassword);
    }
    public bool ValidateCredentials(string username, string password)
    {
        return pc.ValidateCredentials(username, password, ContextOptions.Negotiate);
    }
        public static bool AuthenticateUser(string user, string password)
    {
        bool respuesta = false;
           ActiveDirectory active = new ActiveDirectory();
        try
        {
            respuesta = active.ValidateCredentials(user, password);
        }
        catch (Exception ex)
        {
                Log.MensajeLog(ex.ToString(), "VALIDAR DIRECTORY ACTIVO");
        }
        return respuesta;
    }
}
 }
