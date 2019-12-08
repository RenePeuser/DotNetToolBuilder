namespace Trumpf.Hmi.Uif
{
    using System;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public class App
    {
        public IServiceProvider ServiceProvider { get; }

        public App(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public Task<int> RunAsync(string[] args)
        {
            var rootCommand = ServiceProvider.GetService<IUifRootCommandBuilder>().Build();

            return rootCommand.InvokeAsync(args);
        }
    }
}