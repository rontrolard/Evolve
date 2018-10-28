using System.Data.SqlClient;

namespace Evolve.IntegrationTest
{
    public static class TestUtil
    {
        public static void CreateSqlServerDatabase(string dbName, string cnxStr)
        {
            var cnn = new SqlConnection(cnxStr);
            cnn.Open();

            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = $"CREATE DATABASE {dbName};";
                cmd.ExecuteNonQuery();
            }

            cnn.Close();
        }
    }
}
