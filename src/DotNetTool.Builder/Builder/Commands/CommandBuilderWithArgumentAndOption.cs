using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class CommandBuilderWithArgumentAndOption : ICommandBuilderWithArgumentAndOption
    {
        private const string Template =
@"namespace $namespace$
{                
    using System.Linq;
    using System.Collections.Generic;    
    using System.CommandLine;
    using System.CommandLine.Invocation;    

    public class $command-name$CommandBuilder : I$parent-command-name$SubCommandBuilder
    {
        private readonly I$command-name$Service _$command-service-argument-name$Service;
        private readonly I$command-name$OptionsBuilder _optionsBuilder;
        private readonly I$command-name$ArgumentBuilder _argumentBuilder;

        public $command-name$CommandBuilder(I$command-name$Service $command-service-argument-name$Service, I$command-name$OptionsBuilder optionsBuilder, I$command-name$ArgumentBuilder argumentBuilder)
        {                    
            _$command-service-argument-name$Service = $command-service-argument-name$Service;
            _optionsBuilder = optionsBuilder;
            _argumentBuilder = argumentBuilder;
        }

        public Command Build()
        {
            var command = new Command(""$command-argument-name$"", ""$command-description$"");
            _optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.AddArgument(_argumentBuilder.Build());
            command.Handler = $command-handler$;
            return command;
        }
    }
}";

        private readonly ICommandHandlerStringBuilder _commandHandlerStringBuilder;

        public CommandBuilderWithArgumentAndOption(ICommandHandlerStringBuilder commandHandlerStringBuilder)
        {
            _commandHandlerStringBuilder = commandHandlerStringBuilder;
        }

        public string Build(string project, ParameterInfo parameterInfo, ParameterInfo parent, string nameSpace)
        {
            var commandHandler = _commandHandlerStringBuilder.Build(parameterInfo);

            var newTemplate = Template.Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$parent-command-name$", parent.NormalizedName)
                .Replace("$command-description$", parameterInfo.Description)
                .Replace("$command-service-argument-name$", parameterInfo.Name)
                .Replace("$command-argument-name$", parameterInfo.Name)
                .Replace("$command-handler$", commandHandler)
                .Replace("$namespace$", nameSpace)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}
