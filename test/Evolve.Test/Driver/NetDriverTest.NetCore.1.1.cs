﻿using System.Data;
using Evolve.Driver;
using Xunit;

namespace Evolve.Test.Driver
{
    public partial class NetDriverTest
    {
        [Fact]
        public void CoreMicrosoftDataSqliteDriverForNet_NET_Core_1_1_works()
        {
            var driver = new CoreMicrosoftDataSqliteDriverForNet(TestContext.NetCore11DepsFile, TestContext.NugetPackageFolder, msBuildExtensionsPath: null);
            var cnn = driver.CreateConnection("Data Source=:memory:");
            cnn.Open();

            Assert.True(cnn.State == ConnectionState.Open);
        }

        [Fact]
        public void CoreNpgsqlDriverForNet_NET_Core_1_1_works()
        {

            var driver = new CoreNpgsqlDriverForNet(TestContext.NetCore11DepsFile, TestContext.NugetPackageFolder, msBuildExtensionsPath: null);
            var cnn = driver.CreateConnection($"Server=127.0.0.1;Port={_pgFixture.HostPort};Database={_pgFixture.DbName};User Id={_pgFixture.DbUser};Password={_pgFixture.DbPwd};");
            cnn.Open();

            Assert.True(cnn.State == ConnectionState.Open);
        }

        [Fact]
        public void CoreMySqlDriverForNet_NET_Core_1_1_works()
        {

            var driver = new CoreMySqlDataDriverForNet(TestContext.NetCore11DepsFile, TestContext.NugetPackageFolder, msBuildExtensionsPath: null);
            var cnn = driver.CreateConnection($"Server=127.0.0.1;Port={_mySqlfixture.HostPort};Database={_mySqlfixture.DbName};Uid={_mySqlfixture.DbUser};Pwd={_mySqlfixture.DbPwd};SslMode=none;");
            cnn.Open();

            Assert.True(cnn.State == ConnectionState.Open);
        }

        [Fact]
        public void CoreMySqlConnectorDriverForNet_NET_Core_1_1_works()
        {

            var driver = new CoreMySqlConnectorDriverForNet(TestContext.NetCore11DepsFile, TestContext.NugetPackageFolder, msBuildExtensionsPath: null);
            var cnn = driver.CreateConnection($"Server=127.0.0.1;Port={_mySqlfixture.HostPort};Database={_mySqlfixture.DbName};Uid={_mySqlfixture.DbUser};Pwd={_mySqlfixture.DbPwd};SslMode=none;");
            cnn.Open();

            Assert.True(cnn.State == ConnectionState.Open);
        }

        [Trait("Category", "Standalone")]
        [Fact]
        public void CassandraDriver_NET_Core_1_1_works()
        {
            if (!TestContext.AppVeyor)
            {
                var driver = new CoreCassandraDriverForNet(TestContext.NetCore11DepsFile, TestContext.NugetPackageFolder, msBuildExtensionsPath: null);
                var cnn = driver.CreateConnection($"Contact Points=127.0.0.1;Port={_cassandraFixture.Cassandra.HostPort};Cluster Name={_cassandraFixture.Cassandra.ClusterName}");
                cnn.Open();

                Assert.True(cnn.State == ConnectionState.Open);
            }
        }
    }
}
