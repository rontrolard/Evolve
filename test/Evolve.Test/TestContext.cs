using System;
using System.IO;
using System.Reflection;
using Evolve.Test.Utilities;
using Xunit;

namespace Evolve.Test
{
    public static class TestContext
    {
        private const string NetCore11SupportedDrivers = "Evolve.Core.Test.Resources.SupportedDrivers";
        private const string NetCore20SupportedDrivers = "Evolve.Core2.Test.Resources.SupportedDrivers";
        private const string NetCore21SupportedDrivers = "Evolve.Core21.Test.Resources.SupportedDrivers";
        
        static TestContext()
        {
#if DEBUG
            Configuration = "Debug";
#endif
            ProjectFolder = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(new Uri(typeof(TestContext).GetTypeInfo().Assembly.CodeBase).AbsolutePath), @"../../"));

            NetCore11DepsFile = Path.Combine(GetDriverResourcesProjectFolder(NetCore11SupportedDrivers), $"bin/{Configuration}/netcoreapp1.1/{NetCore11SupportedDrivers}.deps.json");
            NetCore20DepsFile = Path.Combine(GetDriverResourcesProjectFolder(NetCore20SupportedDrivers), $"bin/{Configuration}/netcoreapp2.0/{NetCore20SupportedDrivers}.deps.json");
            NetCore21DepsFile = Path.Combine(GetDriverResourcesProjectFolder(NetCore21SupportedDrivers), $"bin/{Configuration}/netcoreapp2.1/{NetCore21SupportedDrivers}.deps.json");

            Environment.SetEnvironmentVariable("EVOLVE_HOST", "127.0.0.1");
            Environment.SetEnvironmentVariable("EVOLVE_DB_USER", "myUsername");
            Environment.SetEnvironmentVariable("EVOLVE_DB_PWD", "myPassword");
        }

        public static string Configuration { get; } = "Release";
        public static string ProjectFolder { get; }
        public static string NetCore11DepsFile { get; }
        public static string NetCore20DepsFile { get; }
        public static string NetCore21DepsFile { get; }
        public static string IntegrationTestFolder => Path.GetFullPath(Path.Combine(ProjectFolder, $"../../test/Evolve.IntegrationTest/bin/{Configuration}"));
        public static string NugetPackageFolder => $"{EnvHome}/.nuget/packages";
        public static string EnvHome => Environment.GetEnvironmentVariable("USERPROFILE") ?? Environment.GetEnvironmentVariable("HOME");
        public static string ResourcesFolder => Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath), "Resources");
        public static string AppConfigPath => Path.Combine(ResourcesFolder, "App.config");
        public static string WebConfigPath => Path.Combine(ResourcesFolder, "Web.config");
        public static string JsonConfigPath => Path.Combine(ResourcesFolder, "evolve.json");
        public static string Json2ConfigPath => Path.Combine(ResourcesFolder, "evolve2.json");
        public static bool AppVeyor => Environment.GetEnvironmentVariable("APPVEYOR") == "True";
        public static bool Travis => Environment.GetEnvironmentVariable("TRAVIS") == "True";
        public static bool AzureDevOps => Environment.GetEnvironmentVariable("TF_BUILD") == "True";

        private static string GetDriverResourcesProjectFolder(string netCoreVersion) => Path.GetFullPath(Path.Combine(ProjectFolder, $"../{netCoreVersion}"));

        [CollectionDefinition("Database collection")]
        public class DatabaseCollection : ICollectionFixture<MySQLFixture>,
                                          ICollectionFixture<PostgreSqlFixture>,
                                          ICollectionFixture<SQLServerFixture>,
                                          ICollectionFixture<CassandraFixture> { }
    }
}
