using System.IO;
using System.Linq;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder
{
    using global::Argument.Check;

    public class AppBuilder
    {
        private const string Template =
            @"namespace $namespace$
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
            var rootCommand = ServiceProvider.GetService<$interface-startup-command$>().Build();

            return rootCommand.InvokeAsync(args);
        }
    }
}";

        public void AddStartup(string projectName, FileInfo solutionFile, ParameterInfo rootCommand)
        {
            Throw.IfNullOrWhiteSpace(() => projectName);
            Throw.IfNull(() => solutionFile);
            Throw.IfNotExists(() => solutionFile);
            Throw.IfNull(() => rootCommand);

            var app = solutionFile.Directory.EnumerateFiles("App.cs", SearchOption.AllDirectories).FirstOrDefault();

            var newStartUp = Template.Replace("$namespace$", projectName)
                                     .Replace("$interface-startup-command$", $"I{rootCommand.NormalizedName}CommandBuilder");

            File.WriteAllText(app.FullName, newStartUp);
        }
    }
}
