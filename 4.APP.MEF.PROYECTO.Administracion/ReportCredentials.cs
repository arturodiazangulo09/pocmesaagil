using Microsoft.Reporting.WebForms;

namespace APP.ADMINISTRAR.FAG.PAG
{
    public class ReportCredentials : IReportServerCredentials
    {
        private string _userName, _password, _domain;

        public ReportCredentials(string userName, string password, string domain)
        {
            _userName = userName;
            _password = password;
            _domain = domain;
        }
        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
        {
            userName = _userName;
            password = _password;
            authority = _domain;
            authCookie = new System.Net.Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "Domain");
            return true;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                return new System.Net.NetworkCredential(_userName, _password, _domain);
            }
        }
    }
}
