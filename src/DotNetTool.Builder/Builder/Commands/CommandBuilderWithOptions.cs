using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class CommandBuilderWithOptions : ICommandBuilderWithOptions
    {
        private const string Template =
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
        private readonly I$command-name$OptionsBuilder _optionsBuilder;        

        public $command-name$CommandBuilder(I$command-name$Service $command-service-argument-name$Service, I$command-name$OptionsBuilder optionsBuilder)
        {                    
            _$command-service-argument-name$Service = $command-service-argument-name$Service;
            _optionsBuilder = optionsBuilder;
            _argumentBuilder = argumentBuilder;
        }

        public Command Build()
        {
            var command = new Command(""$command-argument-name$"", ""$command-description$"".AsDescription());
            _optionsBuilder.Build().ForEach(option => command.AddOption(option));            
            command.Handler = $command-handler$;
            return command;
        }
    }
}";

        private readonly ICommandHandlerStringBuilder _commandHandlerStringBuilder;

        public CommandBuilderWithOptions(ICommandHandlerStringBuilder commandHandlerStringBuilder)
        {
            _commandHandlerStringBuilder = commandHandlerStringBuilder;
        }

        public string Build(string project, ParameterInfo parameterInfo, ParameterInfo parent, string nameSpace)
        {
            var commandHandler = _commandHandlerStringBuilder.Build(parameterInfo);

            var newTemplate = Template.Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$parent-command-name$", parent.NormalizedName)
                .Replace("$command-description$", parameterInfo.Description)
                .Replace("$command-argument-name$", parameterInfo.Name)
                .Replace("$command-service-argument-name$", parameterInfo.Name)
                .Replace("$command-handler$", commandHandler)
                .Replace("$namespace$", nameSpace)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}