using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class CommandBuilderForSubCommands : ICommandBuilderForSubCommands
    {
        private const string Template =
            @"namespace $namespace$
{
    using System.Linq;
    using System.Collections.Generic;    
    using System.CommandLine;    

    public class $command-name$CommandBuilder : I$parent-command-name$SubCommandBuilder
    {
        private readonly IEnumerable<I$command-name$SubCommandBuilder> _$command-argument-name$SubCommandBuilders;

        public $command-name$CommandBuilder(IEnumerable<I$command-name$SubCommandBuilder> $command-argument-name$SubCommandBuilders)
        {
            _$command-argument-name$SubCommandBuilders = $command-argument-name$SubCommandBuilders;
        }

        public Command Build()
        {
            var $command-argument-name$Command = new Command(""$command-argument-name$"", ""$command-description$"");
            _$command-argument-name$SubCommandBuilders.ToList().ForEach(builder => $command-argument-name$Command.AddCommand(builder.Build()));            
            return $command-argument-name$Command;
        }
    }
}";

        private readonly ICommandHandlerStringBuilder _commandHandlerStringBuilder;

        public CommandBuilderForSubCommands(ICommandHandlerStringBuilder commandHandlerStringBuilder)
        {
            _commandHandlerStringBuilder = commandHandlerStringBuilder;
        }

        public string Build(string project, ParameterInfo parameterInfo, ParameterInfo parent, string nameSpace)
        {
            var commandHandler = _commandHandlerStringBuilder.Build(parameterInfo);

            var newTemplate = Template.Replace("$command-name$", parameterInfo.NormalizedName)
                                      .Replace("$command-argument-name$", parameterInfo.Name)
                                      .Replace("$namespace$", nameSpace)
                                      .Replace("$command-description$", parameterInfo.Description)
                                      .Replace("$command-handler$", commandHandler)
                                      .Replace("$parent-command-name$", parent.NormalizedName)
                                      .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}
