using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class CommandBuilderForSubCommands : ICommandBuilderForSubCommands
    {
        private const string template =
@"namespace $namespace$
{
    using System.Collections.Generic;
    using Trumpf.Hmi.Extensions;
    using System.CommandLine;    
    using $project-name$.Rendering;

    public class $command-name$CommandBuilder : I$parent-command-name$SubCommandBuilder
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

        public string Build(string project, CliParameterInfo cliParameterInfo, CliParameterInfo parent, string nameSpace)
        {
            var commandHandler = new CommandHandlerStringBuilder().Build(cliParameterInfo);

            var newTemplate = template.Replace("$command-name$", cliParameterInfo.Name.FirstCharToUpper())
                .Replace("$command-argument-name$", cliParameterInfo.Name)
                .Replace("$namespace$", nameSpace)
                .Replace("$command-description$", cliParameterInfo.Decsription)
                .Replace("$command-handler$", commandHandler)
                .Replace("$parent-command-name$", parent.Name.FirstCharToUpper())
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}