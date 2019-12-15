namespace Trumpf.Hmi.Uif
{
    using Microsoft.Extensions.DependencyInjection;
    using Trumpf.Hmi.FileSystemAbstraction.Services;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<TiFileService, TcFileService>();
            services.AddSingleton<IUifCommandBuilder, UifCommandBuilder>();
        }
    }
}