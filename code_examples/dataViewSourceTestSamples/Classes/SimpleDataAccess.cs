using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace dataViewSourceTestSamples
{
    public class SimpleDataAccess : IDisposable
    {
        private System.Data.SqlClient.SqlConnection con;
        private System.Data.SqlClient.SqlCommand cmd;

        public System.Exception LastError { get; set; }

        public bool Open(string connectionString)
        {
            try
            {
                con = new System.Data.SqlClient.SqlConnection();
                con.ConnectionString = connectionString;
                con.Open();
                cmd = con.CreateCommand();
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex;
                return false;
            }

        }
        private string _commandText;
        public string CommandText
        {
            get
            {
                return _commandText;
            }
            set
            {
                _commandText = value;
                this.cmd.CommandText = _commandText;
            }
        }
        public DataSet Fill(System.Data.DataSet ds)
        {
            System.Data.SqlClient.SqlDataAdapter adpt = new System.Data.SqlClient.SqlDataAdapter(cmd);
            adpt.Fill(ds);
            return ds;
        }
        public void Dispose()
        {
            if (cmd != null) { cmd.Dispose(); cmd = null; }
            if (con != null) { con.Close(); con.Dispose(); con = null; }
        }

    }
}
