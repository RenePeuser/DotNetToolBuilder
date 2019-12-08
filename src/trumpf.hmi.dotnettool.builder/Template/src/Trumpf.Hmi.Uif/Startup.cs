namespace Trumpf.Hmi.Uif
{
    using Microsoft.Extensions.DependencyInjection;
    using Trumpf.Hmi.FileSystemAbstraction.Services;
    using Trumpf.Hmi.Uif.List;
    using Trumpf.Hmi.Uif.List.Dependencies;
    using Trumpf.Hmi.Uif.List.Dependencies.Services;
    using Trumpf.Hmi.Uif.Pack;
    using Trumpf.Hmi.Uif.Pack.Services;
    using Trumpf.Hmi.Uif.Push;
    using Trumpf.Hmi.Uif.Push.Service;
    using Trumpf.Hmi.Uif.Scaps;
    using Trumpf.Hmi.Uif.Scaps.Provider;
    using Trumpf.Hmi.Uif.Tcix;
    using Trumpf.Hmi.Uif.Tcix.Parser;
    using Trumpf.Hmi.Uif.Update;
    using Trumpf.Hmi.Uif.Update.Dependencies;
    using Trumpf.Hmi.Uif.Update.Dependencies.Services;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(s => new ISpecificVersionParser[] { new VersionAsPackageName(), new VersionWithExpression(), new VersionOnly() });

            services.AddScoped<IScapsHttpClient, ScapsHttpClient>();
            services.AddSingleton<ITcixParser, TcixParser>();
            services.AddSingleton<IUifVersionParser, UifVersionParser>();
            services.AddSingleton<IScapsProvider, ScapsProvider>();

            services.AddScoped<IUpdateDependenciesService, UpdateDependenciesService>();

            services.AddSingleton<IUpdateTcixFile, UpdateTcixFile>();
            services.AddSingleton<IVersionUpdater, UpdateDependencies>();
            services.AddSingleton<IVersionUpdater, UpdateMinorVersion>();
            services.AddSingleton<IVersionUpdater, UpdatePatchVersion>();

            services.AddSingleton<IUpdateDependenciesCommandBuilder, UpdateDependenciesCommandBuilder>();

            services.AddSingleton<ListOutdatedDependenciesStrategy>();
            services.AddSingleton<ListDependenciesStrategy>();
            services.AddSingleton(s => new IListDependenciesStrategy[] { s.GetService<ListOutdatedDependenciesStrategy>(), s.GetService<ListDependenciesStrategy>() });
            services.AddSingleton<IListDependenciesService, ListDependenciesService>();
            
            services.AddSingleton<TiFileService, TcFileService>();
            services.AddSingleton<ITcixPathService, TcixPathService>();
            services.AddSingleton<IListDependenciesCommandBuilder, ListDependenciesCommandBuilder>();
           
            services.AddSingleton<IUifRootCommandBuilder, UifRootCommandBuilder>();

            services.AddSingleton<ICommandBuilder, ListCommandBuilder>();
            services.AddSingleton<ICommandBuilder, UpdateCommandBuilder>();
            services.AddSingleton<ICommandBuilder, PackCommandBuilder>();
            services.AddSingleton<ICommandBuilder, PushCommandBuilder>();


            services.AddSingleton<IProcessService, ProcessService>();
            services.AddSingleton<IProcessBuilder, ProcessBuilder>();
            services.AddSingleton<ITaskCompletionSourceBuilder, TaskCompletionSourceBuilder>();

            services.AddSingleton<IDotNetToolService, DotNetToolService>();
            services.AddSingleton<IUifPackService, UifPackService>();
            services.AddSingleton<IPackService, PackService>();

            services.AddSingleton<IPushToScapsService, PushToScapsService>();
         
        }
    }
}