using Microsoft.Extensions.DependencyInjection;

namespace DotNetTool.Builder.App
{
    public class AppBuilder
    {
        public App Build()
        {
            var services = new ServiceCollection();
            var startup = new Startup();

            startup.ConfigureServices(services);

            return new App(services.BuildServiceProvider());
        }
    }
}