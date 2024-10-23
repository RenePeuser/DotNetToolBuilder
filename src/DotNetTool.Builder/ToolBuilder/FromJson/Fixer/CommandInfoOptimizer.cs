using System;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal sealed class CommandInfoOptimizer(IDotNetToolNameNormalizer dotNetToolNameNormalizer,
                                               ArgumentInfoOptimizer argumentInfoOptimizer,
                                               OptionInfoOptimizer optionInfoOptimizer)
    {
        public CommandInfo Optimize(CommandInfo commandInfo)
        {
            OptimizeInternal(commandInfo);
            foreach (var subCommand in commandInfo.SubCommands)
            {
                Optimize(subCommand);
            }

            return commandInfo;
        }

        private CommandInfo OptimizeInternal(CommandInfo commandInfo)
        {
            commandInfo.NormalizedName = dotNetToolNameNormalizer.Normalize(commandInfo.Name);
            commandInfo.Argument = argumentInfoOptimizer.Optimize(commandInfo.Argument);
            commandInfo.Options.ForEach(option => optionInfoOptimizer.Optimize(option));
            return commandInfo;
        }
    }
}