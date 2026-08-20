using Microsoft.Data.SqlClient;

namespace StaffSphere.Data
{
    public class DbConnection
    {
        private static string connectionString = 
            "Server=localhost\\SQLEXPRESS;Database=StaffSphere;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}