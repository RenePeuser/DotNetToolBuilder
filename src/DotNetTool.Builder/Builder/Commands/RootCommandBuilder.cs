using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class RootCommandBuilder : IRootCommandBuilder
    {
        private const string template =
            @"namespace $namespace$
{
    using System.Collections.Generic;
    using Trumpf.Hmi.Extensions;
    using System.CommandLine;    
    using $project-name$.Rendering;

    public class $command-name$CommandBuilder : I$command-name$CommandBuilder
    {
        private readonly IEnumerable<I$command-name$SubCommandBuilder> _$command-argument-name$SubCommandBuilders;

        public $command-name$CommandBuilder(IEnumerable<I$command-name$SubCommandBuilder> $command-argument-name$SubCommandBuilders)
        {
            _$command-argument-name$SubCommandBuilders = $command-argument-name$SubCommandBuilders;
        }

        public Command Build()
        {
            var $command-argument-name$Command = new Command(""$command-argument-name$"", ""$command-description$"".AsDescription());
            _$command-argument-name$SubCommandBuilders.ForEach(builder => $command-argument-name$Command.AddCommand(builder.Build()));            
            return $command-argument-name$Command;
        }
    }
}";

        private readonly ICommandHandlerStringBuilder _commandHandlerStringBuilder;

        public RootCommandBuilder(ICommandHandlerStringBuilder commandHandlerStringBuilder)
        {
            _commandHandlerStringBuilder = commandHandlerStringBuilder;
        }

        public string Build(string project, ParameterInfo parameterInfo, string nameSpace)
        {
            var commandHandler = _commandHandlerStringBuilder.Build(parameterInfo);

            var newTemplate = template.Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$command-argument-name$", parameterInfo.Name)
                .Replace("$namespace$", nameSpace)
                .Replace("$command-description$", parameterInfo.Description)
                .Replace("$command-handler$", commandHandler)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}