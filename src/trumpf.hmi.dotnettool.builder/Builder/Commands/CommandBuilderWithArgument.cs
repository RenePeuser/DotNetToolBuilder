using trumpf.hmi.dotnettool.builder.Extensions;
using trumpf.hmi.dotnettool.builder.Models;

namespace trumpf.hmi.dotnettool.builder.Builder.Commands
{
    internal class CommandBuilderWithArgument
    {
        private const string template =
@"namespace $namespace$
{                
    using System.Collections.Generic;
    using Trumpf.Hmi.Extensions;
    using System.CommandLine;
    using System.CommandLine.Invocation;    
    using $project-name$.Rendering;

    public class $command-name$CommandBuilder : I$parent-command-name$SubCommandBuilder
    {
        private readonly I$command-name$Service _$command-service-argument-name$Service;        
        private readonly I$command-name$ArgumentBuilder _argumentBuilder;

        public $command-name$CommandBuilder(I$command-name$Service $command-service-argument-name$Service, I$command-name$ArgumentBuilder argumentBuilder)
        {                    
            _$command-service-argument-name$Service = $command-service-argument-name$Service;            
            _argumentBuilder = argumentBuilder;
        }

        public Command Build()
        {
            var command = new Command(""$command-argument-name$"", ""$command-description$"".AsDescription());            
            command.AddArgument(_argumentBuilder.Build());
            command.Handler = $command-handler$;
            return command;
        }
    }
}";

        internal string Build(string project, CliParameterInfo cliParameterInfo, CliParameterInfo parent, string nameSpace)
        {
            var commandHandler = new CommandHandlerStringBuilder().Build(cliParameterInfo);

            var newTemplate = template.Replace("$command-name$", cliParameterInfo.Name.FirstCharToUpper())
                .Replace("$parent-command-name$", parent.Name.FirstCharToUpper())
                .Replace("$command-argument-name$", cliParameterInfo.Name)
                .Replace("$command-description$", cliParameterInfo.Decsription)
                .Replace("$command-service-argument-name$", cliParameterInfo.Name)
                .Replace("$command-handler$", commandHandler)
                .Replace("$namespace$", nameSpace)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}