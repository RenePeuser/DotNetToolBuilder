namespace Trumpf.Hmi.Uif
{
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Trumpf.Hmi.Uif.Scaps;

    public class AppBuilder
    {
        public App Build()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, false)
                .AddUserSecrets<ScapsConfiguration>();

            var configuration = builder.Build();

            var services = new ServiceCollection()
                .Configure<ScapsConfiguration>(configuration.GetSection(nameof(ScapsConfiguration)))
                .Configure<UifPackToolConfiguration>(configuration.GetSection(nameof(UifPackToolConfiguration)))
                .AddOptions();

            var startup = new Startup();
            
            startup.ConfigureServices(services);

            return new App(services.BuildServiceProvider());
        }
    }
}