using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal class DotNetToolCommandFixer : IDotNetToolFromJsonFixer
    {
        private readonly IEnumerable<ICommandFixer> _commandFixers;

        public DotNetToolCommandFixer(IEnumerable<ICommandFixer> commandFixers)
        {
            _commandFixers = commandFixers;
        }

        public Models.DotNetTool FixMissingValues(Models.DotNetTool dotNetTool)
        {
            var fixedCommandInfo = FixAll(dotNetTool.ParameterInfo);
            return new Models.DotNetTool(dotNetTool.ProjectName, dotNetTool.DotNetToolName, fixedCommandInfo);
        }

        private CommandInfo FixAll(CommandInfo commandInfo)
        {
            _commandFixers.Aggregate(commandInfo, (command, optimizer) => optimizer.Optimize(command));
            foreach (var commandInfoSubCommand in commandInfo.SubCommands)
            {
                return FixAll(commandInfoSubCommand);
            }

            return commandInfo;
        }
    }
}