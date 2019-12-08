using Trumpf.Hmi.Uif.List.Dependencies;
using Trumpf.Hmi.Uif.List.Dependencies.Services;
using Trumpf.Hmi.Uif.Push;
using Trumpf.Hmi.Uif.Push.Service;

namespace Trumpf.Hmi.Uif
{
    using Microsoft.Extensions.DependencyInjection;
    using Trumpf.Hmi.FileSystemAbstraction.Services;
    using Trumpf.Hmi.Uif.List;
    
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IListDependenciesService, ListDependenciesService>();
            
            services.AddSingleton<TiFileService, TcFileService>();
            services.AddSingleton<IListDependenciesCommandBuilder, ListDependenciesCommandBuilder>();
           
            services.AddSingleton<IUifRootCommandBuilder, UifRootCommandBuilder>();

            services.AddSingleton<ICommandBuilder, ListCommandBuilder>();
            services.AddSingleton<ICommandBuilder, PushCommandBuilder>();
            services.AddSingleton<IPushToScapsService, PushToScapsService>();
         
        }
    }
}