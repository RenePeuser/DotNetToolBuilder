namespace rps.template
{
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AppBuilder
    {
        public App Build()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, false);

            var configuration = builder.Build();
            var services = new ServiceCollection().AddOptions();

            var startup = new Startup();
            
            startup.ConfigureServices(services);

            return new App(services.BuildServiceProvider());
        }
    }
}