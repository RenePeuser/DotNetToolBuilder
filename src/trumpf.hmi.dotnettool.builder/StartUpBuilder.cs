using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder
{
    public class StartUpBuilder
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

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<TiFileService, TcFileService>();
            services.AddSingleton<I$root-command$RootCommandBuilder, $root-command$RootCommandBuilder>();
            
            $command-registrations$
        }

        $methods$
    }
}";

        public void AddRegistrationsFrom(string projectName, FileInfo solutionFile, CommandTypeCollector commandTypeCollector, CliParameterInfo rootCommand)
        {
            var startUpFile = solutionFile.Directory.EnumerateFiles("*.cs",SearchOption.AllDirectories).FirstOrDefault(file => file.Name.ToLower().EqualsTo("startup.cs"));

            var methods = GenerateMethods(commandTypeCollector).ToList();
            var commandRegistrations = methods.Select(m => $"{m.MethodName}(services);").Flatten(Environment.NewLine);
            var registrationMethods = methods.Select(m => m.MethodSyntax).Flatten(Environment.NewLine);

            var folder = solutionFile.Directory.EnumerateDirectories(rootCommand.Name, SearchOption.AllDirectories).FirstOrDefault();
            var allDirectories = folder.EnumerateDirectories("*", SearchOption.AllDirectories).ToList();

            var newStartUp = template.Replace("$projectName$", projectName)
                .Replace("$command-registrations$", commandRegistrations)
                .Replace("$root-command$", rootCommand.Name.FirstCharToUpper())
                .Replace("$methods$", registrationMethods);

            File.WriteAllText(startUpFile.FullName, newStartUp);
        }

        private IEnumerable<MethodInfo> GenerateMethods(CommandTypeCollector commandTypeCollector)
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