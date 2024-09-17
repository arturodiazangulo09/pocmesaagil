using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MEF.PROYECTO.Data.DataBaseHelpers
{
    public class DataBaseHelper
    {
        string _cnSTR = string.Empty;
        public DataBaseHelper() 
        {
            _cnSTR = ConfigurationManager.ConnectionStrings["cnnOracle"].ConnectionString;
        }

        public String cnSTR
        {
            get { return _cnSTR; }            
        }
        public DbConnection GetNewConnection()
        {
            SqlConnection cn = new SqlConnection(_cnSTR);
            cn.Open();
            return cn;
        }

        public DbTransaction GetBeginTransaction()
        {
            SqlConnection cn = new SqlConnection(_cnSTR);
            cn.Open();
            return cn.BeginTransaction();
        }

    }
}
