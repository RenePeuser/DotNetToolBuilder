using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using Trumpf.Hmi.Extensions;
using Trumpf.Hmi.FileSystemAbstraction.FileSystem;

namespace DotNetTool.Builder.Builder
{
    public interface IStartUpBuilder
    {
        void AddRegistrationsFrom(string projectName, TiFileInfo solutionFile, ICommandTypeCollector commandTypeCollector, ParameterInfo rootCommand, INameSpaceCollector nameSpaceCollector);
    }

    public class StartUpBuilder : IStartUpBuilder
    {
        private const string registerServiceMethod =
@"private static void Configure$command-name$(IServiceCollection services)
    {
        $registrations$
    }
";

        private const string template =
@"namespace $projectName$
{
    using Microsoft.Extensions.DependencyInjection;
    using Trumpf.Hmi.FileSystemAbstraction.Services;
    using $projectName$.ErrorHandling;
    $usings$

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<TiFileService, TcFileService>();
            services.AddSingleton<IErrorHandler, ErrorHandler>();
            services.AddSingleton<I$root-command$CommandBuilder, $root-command$CommandBuilder>();
            
            $command-registrations$
        }

        $methods$
    }
}";

        public void AddRegistrationsFrom(string projectName, TiFileInfo solutionFile, ICommandTypeCollector commandTypeCollector, ParameterInfo rootCommand, INameSpaceCollector nameSpaceCollector)
        {
            var startUpFile = solutionFile.Directory.EnumerateFiles("*.cs", SearchOption.AllDirectories).FirstOrDefault(file => file.Name.ToLower().EqualsTo("startup.cs"));

            var methods = GenerateMethods(commandTypeCollector).ToList();
            var commandRegistrations = methods.Select(m => $"{m.MethodName}(services);").Flatten(Environment.NewLine);
            var registrationMethods = methods.Select(m => m.MethodSyntax).Flatten(Environment.NewLine);

            var usings = nameSpaceCollector.GetAll().Select(n => $"using {n};").Flatten(Environment.NewLine);

            var newStartUp = template.Replace("$projectName$", projectName)
            .Replace("$command-registrations$", commandRegistrations)
            .Replace("$root-command$", rootCommand.Name.FirstCharToUpper())
            .Replace("$methods$", registrationMethods)
            .Replace("$usings$", usings);

            File.WriteAllText(startUpFile.FullName, newStartUp);
        }

        private IEnumerable<MethodInfo> GenerateMethods(ICommandTypeCollector commandTypeCollector)
        {
            var allRegistrations = commandTypeCollector.GetAll();
            foreach (var registration in allRegistrations)
            {
                var command = registration.Key;
                var typeRegistrations = GetTypeRegistrations(registration.Value).Flatten(Environment.NewLine);

                var newMethodSyntax = registerServiceMethod.Replace("$command-name$", command.FirstCharToUpper())
                    .Replace("$registrations$", typeRegistrations);

                yield return new MethodInfo($"Configure{command.FirstCharToUpper()}", newMethodSyntax);
            }
        }

        private IEnumerable<string> GetTypeRegistrations(IEnumerable<TypeToRegister> registrations)
        {
            foreach (var typeToRegister in registrations)
            {
                yield return $"services.AddSingleton<{typeToRegister.InterfaceType}, {typeToRegister.ImplementationType}>();";
            }
        }
    }
}