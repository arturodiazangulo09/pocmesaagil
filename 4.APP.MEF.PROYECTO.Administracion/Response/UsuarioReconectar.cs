using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEF.PROYECTO.Utilitario;
namespace APP.MEF.ADMINISTRAR.FAG.PAG.Response
{
    public class UsuarioReconectar : IDisposable
    {
        APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.SWLoginClient proxy = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.SWLoginClient();
        public APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU ConsultaPUsuario(string TOKEN, int ID_SISTEMA)
        {
            APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU usuario = new APP.MEF.ADMINISTRAR.FAG.PAG.WCF_Seguridad.RespuestaSeguridadU();
            try
            {
                 usuario = proxy.Usuario_Sistema(TOKEN, ID_SISTEMA);
            }
            catch (Exception ex)
            {
                usuario.Usuario_Valido = false;
                Log.MensajeLog(ex.ToString(), "UsuarioReconectar.ConsultaPUsuario");

            }
            return usuario;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}