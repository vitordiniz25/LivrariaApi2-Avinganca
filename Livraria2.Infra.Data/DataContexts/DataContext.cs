using Livraria2.Infra.Settings;
using System;
using System.Data.SqlClient;

namespace Livraria2.Infra.Data.DataContexts
{
    public class DataContext : IDisposable
    {
        public SqlConnection SqlConnection { get; set; }

        public DataContext(AppSettings appSettings)
        {
            SqlConnection = new SqlConnection(appSettings.ConnectionString);
            SqlConnection.Open();
        }

        public void Dispose()
        {
            if (SqlConnection.State != System.Data.ConnectionState.Closed)
                SqlConnection.Close();
        }
    }
}
