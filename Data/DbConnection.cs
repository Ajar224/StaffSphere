using Microsoft.Data.SqlClient;

namespace MiniHRMS.Data
{
    public class DbConnection
    {
        private static string connectionString = 
            "Server=localhost\\SQLEXPRESS;Database=MiniHRMSDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}