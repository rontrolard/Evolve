using System;
using System.IO;
using System.Reflection;
using Evolve.Test.Utilities;
using Xunit;

namespace Evolve.IntegrationTest
{
    public static class TestContext
    {
        public static string ProjectFolder => Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath);
        public static bool AppVeyor => Environment.GetEnvironmentVariable("APPVEYOR") == "True";
        public static bool Travis => Environment.GetEnvironmentVariable("TRAVIS") == "True";
        public static bool AzureDevOps => Environment.GetEnvironmentVariable("TF_BUILD") == "True";

        public static class Cassandra
        {
            public static string ResourcesFolder => Path.Combine(ProjectFolder, "Cassandra/Resources");
            public static string SqlScriptsFolder => Path.Combine(ResourcesFolder, "Cql_Scripts");
            public static string MigrationFolder => Path.Combine(SqlScriptsFolder, "Migration");
            public static string ChecksumMismatchFolder => Path.Combine(SqlScriptsFolder, "Checksum_mismatch");
            public static string EmptyMigrationScriptPath => Path.Combine(ResourcesFolder, "V1_3_2__Migration_description.cql");
        }

        public static class MySQL
        {
            public static string ResourcesFolder => Path.Combine(ProjectFolder, "MySQL/Resources");
            public static string SqlScriptsFolder => Path.Combine(ResourcesFolder, "Sql_Scripts");
            public static string MigrationFolder => Path.Combine(SqlScriptsFolder, "Migration");
            public static string ChecksumMismatchFolder => Path.Combine(SqlScriptsFolder, "Checksum_mismatch");
            public static string EmptyMigrationScriptPath => Path.Combine(ResourcesFolder, "V1_3_2__Migration_description.sql");
        }

        public static class PostgreSQL
        {
            public static string ResourcesFolder => Path.Combine(ProjectFolder, "PostgreSQL/Resources");
            public static string SqlScriptsFolder => Path.Combine(ResourcesFolder, "Sql_Scripts");
            public static string MigrationFolder => Path.Combine(SqlScriptsFolder, "Migration");
            public static string ChecksumMismatchFolder => Path.Combine(SqlScriptsFolder, "Checksum_mismatch");
            public static string OutOfOrderFolder => Path.Combine(SqlScriptsFolder, "OutOfOrder");
            public static string EmptyMigrationScriptPath => Path.Combine(ResourcesFolder, "V1_3_2__Migration_description.sql");
        }

        public static class SQLite
        {
            public static string ResourcesFolder => Path.Combine(ProjectFolder, "SQLite/Resources");
            public static string SqlScriptsFolder => Path.Combine(ResourcesFolder, "Sql_Scripts");
            public static string MigrationFolder => Path.Combine(SqlScriptsFolder, "Migration");
            public static string ChecksumMismatchFolder => Path.Combine(SqlScriptsFolder, "Checksum_mismatch");
        }

        public static class SqlServer
        {
            public static string ResourcesFolder => Path.Combine(ProjectFolder, "SQLServer/Resources");
            public static string SqlScriptsFolder => Path.Combine(ResourcesFolder, "Sql_Scripts");
            public static string MigrationFolder => Path.Combine(SqlScriptsFolder, "Migration");
            public static string ChecksumMismatchFolder => Path.Combine(SqlScriptsFolder, "Checksum_mismatch");
            public static string EmptyMigrationScriptPath => Path.Combine(ResourcesFolder, "V1_3_2__Migration_description.sql");
        }

        [CollectionDefinition("Database collection")]
        public class DatabaseCollection : ICollectionFixture<MySQLFixture>,
                                          ICollectionFixture<PostgreSqlFixture>,
                                          ICollectionFixture<SQLServerFixture>,
                                          ICollectionFixture<CassandraFixture> { }
    }
}
