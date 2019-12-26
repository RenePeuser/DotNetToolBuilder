using rps.template.Services;
using Microsoft.Extensions.DependencyInjection;

namespace rps.template
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRpsCommandBuilder, RpsCommandBuilder>();
            services.AddSingleton<IConsoleService, ConsoleService>();
        }
    }
}