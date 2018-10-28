using System.Data;
using System.IO;
using Evolve.Driver;
using Evolve.Test.Utilities;
using Xunit;

namespace Evolve.Test.Driver
{
    [Collection("Database collection")]
    public partial class NetDriverTest
    {
        private readonly MySQLFixture _mySqlfixture;
        private readonly PostgreSqlFixture _pgFixture;
        private readonly SQLServerFixture _sqlServerFixture;
        private readonly CassandraFixture _cassandraFixture;

        public NetDriverTest(MySQLFixture mySqlfixture, PostgreSqlFixture pgFixture, SQLServerFixture sqlServerFixture, CassandraFixture cassandraFixture)
        {
            _mySqlfixture = mySqlfixture;
            _pgFixture = pgFixture;
            _sqlServerFixture = sqlServerFixture;
            _cassandraFixture = cassandraFixture;

            if (!TestContext.Travis && !TestContext.AppVeyor)
            {
                _mySqlfixture.Start();
                _pgFixture.Start();
                _sqlServerFixture.Start();
                _cassandraFixture.Start();
            }
        }

        [Fact]
        public void Load_ConnectionType_from_an_already_loaded_assembly()
        {
            var driver = new SystemDataSQLiteDriver();
            var cnn = driver.CreateConnection("Data Source=:memory:");
            cnn.Open();

            Assert.True(cnn.State == ConnectionState.Open);
        }

        [Fact]
        public void SqlClientDriver_works()
        {
            var driver = new SqlClientDriver();
            var cnn = driver.CreateConnection($"Server=127.0.0.1;Database=master;User Id={_sqlServerFixture.DbUser};Password={_sqlServerFixture.DbPwd};");
            cnn.Open();

            Assert.True(cnn.State == ConnectionState.Open);
        }

        [Fact]
        public void NpgsqlDriver_works()
        {
            string originalCurrentDirectory = Directory.GetCurrentDirectory();

            try
            {
                Directory.SetCurrentDirectory(TestContext.IntegrationTestFolder);
                var driver = new NpgsqlDriver();
                var cnn = driver.CreateConnection(_pgFixture.CnxStr);
                cnn.Open();

                Assert.True(cnn.State == ConnectionState.Open);
            }
            finally
            {
                Directory.SetCurrentDirectory(originalCurrentDirectory);
            }
        }

        [Fact]
        public void MySqlDataDriver_works()
        {
            string originalCurrentDirectory = Directory.GetCurrentDirectory();

            try
            {
                Directory.SetCurrentDirectory(TestContext.IntegrationTestFolder);
                var driver = new MySqlDataDriver();
                var cnn = driver.CreateConnection(_mySqlfixture.CnxStr);
                cnn.Open();

                Assert.True(cnn.State == ConnectionState.Open);
            }
            finally
            {
                Directory.SetCurrentDirectory(originalCurrentDirectory);
            }
        }

        [Fact]
        public void MySqlConnectorDriver_works()
        {
            string originalCurrentDirectory = Directory.GetCurrentDirectory();

            try
            {
                Directory.SetCurrentDirectory(TestContext.IntegrationTestFolder);
                var driver = new MySqlConnectorDriver();
                var cnn = driver.CreateConnection(_mySqlfixture.CnxStr);
                cnn.Open();

                Assert.True(cnn.State == ConnectionState.Open);
            }
            finally
            {
                Directory.SetCurrentDirectory(originalCurrentDirectory);
            }
        }

        [Fact]
        public void CassandraDriver_works()
        {
            if (!TestContext.AppVeyor)
            {
                string originalCurrentDirectory = Directory.GetCurrentDirectory();

                try
                {
                    Directory.SetCurrentDirectory(TestContext.IntegrationTestFolder);
                    var driver = new CassandraDriver();
                    var cnn = driver.CreateConnection(_cassandraFixture.CnxStr);
                    cnn.Open();

                    Assert.True(cnn.State == ConnectionState.Open);
                }
                finally
                {
                    Directory.SetCurrentDirectory(originalCurrentDirectory);
                }
            }
        }
    }
}
