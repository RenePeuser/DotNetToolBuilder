using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.FileSystemAbstraction;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.Builder.Startup
{
    

    public class StartUpBuilder : IStartUpBuilder
    {
        private const string Template =
            @"namespace $projectName$
{
    using Microsoft.Extensions.DependencyInjection;
    using $projectName$.$root-command$;
    using $projectName$.Services;
    using $projectName$.ErrorHandling;    
$usings$

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddSingleton<IErrorHandler, ErrorHandler>();
            services.AddSingleton<IConsoleService, ConsoleService>();
            services.AddSingleton<I$root-command$CommandBuilder, $root-command$CommandBuilder>();
            
$command-registrations$
        }

$methods$
    }
}";

        private readonly IRegisterServiceMethodBuilder _registerServiceMethodBuilder;
        private readonly ITypeRegistrationBuilder _typeRegistrationBuilder;

        public StartUpBuilder(IRegisterServiceMethodBuilder registerServiceMethodBuilder, ITypeRegistrationBuilder typeRegistrationBuilder)
        {
            Throw.IfNull(() => registerServiceMethodBuilder);
            Throw.IfNull(() => typeRegistrationBuilder);

            _registerServiceMethodBuilder = registerServiceMethodBuilder;
            _typeRegistrationBuilder = typeRegistrationBuilder;
        }

        public void AddRegistrationsFrom(string projectName, IFileInfo solutionFile,
            ICommandTypeCollector commandTypeCollector, CommandInfo rootCommand,
            INameSpaceCollector nameSpaceCollector)
        {
            Throw.IfNullOrWhiteSpace(() => projectName);
            Throw.IfNull(() => solutionFile);
            Throw.IfNull(() => rootCommand);
            Throw.IfNull(() => commandTypeCollector);
            Throw.IfNull(() => nameSpaceCollector);

            var startUpFile = solutionFile.Directory.EnumerateFiles("*.cs", SearchOption.AllDirectories).FirstOrDefault(file => file.Name.ToLower().EqualsTo("startup.cs"));
            var methods = GenerateMethods(commandTypeCollector).ToList();
            var commandRegistrations = methods.Select(m => $"            {m.MethodName}(services);").Flatten(Environment.NewLine);
            var registrationMethods = methods.Select(m => $"        {m.MethodSyntax}").Flatten(Environment.NewLine);

            var usings = nameSpaceCollector.GetAll().Select(n => $"    using {n};").Flatten(Environment.NewLine);

            var newStartUp = Template.Replace("$projectName$", projectName)
                .Replace("$command-registrations$", commandRegistrations)
                .Replace("$root-command$", rootCommand.NormalizedName)
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
                var typeRegistrations = _typeRegistrationBuilder.Build(registration.Value).Flatten(Environment.NewLine);
                var newMethodSyntax = _registerServiceMethodBuilder.Build(command.FirstCharToUpper(), typeRegistrations);
                yield return new MethodInfo($"Configure{command.FirstCharToUpper()}", newMethodSyntax);
            }
        }
    }
}
