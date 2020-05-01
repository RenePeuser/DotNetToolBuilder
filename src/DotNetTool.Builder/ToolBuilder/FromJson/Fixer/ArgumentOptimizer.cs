using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal class ArgumentOptimizer : ICommandFixer
    {
        private readonly IArgumentTypeOptimizer _argumentTypeOptimizer;

        public ArgumentOptimizer(IArgumentTypeOptimizer argumentTypeOptimizer)
        {
            _argumentTypeOptimizer = argumentTypeOptimizer;
        }

        public CommandInfo Optimize(CommandInfo commandInfo)
        {
            var argument = commandInfo.Argument;
            if (argument.IsNull())
            {
                return commandInfo;
            }

            if (argument.Type.IsNullOrWhiteSpace())
            {
                return commandInfo;
            }

            argument.OptimizedType = _argumentTypeOptimizer.OptimizeType(argument.Type);

            return commandInfo;
        }
    }
}