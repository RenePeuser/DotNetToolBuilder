using System;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal class CommandInfoOptimizer
    {
        private readonly IDotNetToolNameNormalizer _dotNetToolNameNormalizer;
        private readonly ArgumentInfoOptimizer _argumentInfoOptimizer;
        private readonly OptionInfoOptimizer _optionInfoOptimizer;

        public CommandInfoOptimizer(IDotNetToolNameNormalizer dotNetToolNameNormalizer, ArgumentInfoOptimizer argumentInfoOptimizer, OptionInfoOptimizer optionInfoOptimizer)
        {
            _dotNetToolNameNormalizer = dotNetToolNameNormalizer;
            _argumentInfoOptimizer = argumentInfoOptimizer;
            _optionInfoOptimizer = optionInfoOptimizer;
        }

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
            commandInfo.NormalizedName = _dotNetToolNameNormalizer.Normalize(commandInfo.Name);
            commandInfo.Argument = _argumentInfoOptimizer.Optimize(commandInfo.Argument);
            commandInfo.Options.ForEach(option => _optionInfoOptimizer.Optimize(option));
            return commandInfo;
        }
    }
}