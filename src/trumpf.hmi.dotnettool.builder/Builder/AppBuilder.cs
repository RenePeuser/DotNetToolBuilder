using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using trumpf.hmi.dotnettool.builder.Extensions;
using trumpf.hmi.dotnettool.builder.Models;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder.Builder
{
    public class AppBuilder
    {
        private const string template =
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

        public void AddStartup(string projectName, FileInfo solutionFile, CliParameterInfo rootCommand)
        {
            var app = solutionFile.Directory.EnumerateFiles("App.cs", SearchOption.AllDirectories).FirstOrDefault();

            var newStartUp = template.Replace("$namespace$", projectName)
                .Replace("$interface-startup-command$", $"I{rootCommand.Name.FirstCharToUpper()}CommandBuilder");

            File.WriteAllText(app.FullName, newStartUp);
        }
    }
}