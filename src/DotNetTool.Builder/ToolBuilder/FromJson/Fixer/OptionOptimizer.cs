using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal class OptionOptimizer : ICommandFixer
    {
        private readonly IArgumentTypeOptimizer _argumentTypeOptimizer;
        private readonly IDotNetToolNameNormalizer _dotNetToolNameNormalizer;

        public OptionOptimizer(IArgumentTypeOptimizer argumentTypeOptimizer, IDotNetToolNameNormalizer dotNetToolNameNormalizer)
        {
            _argumentTypeOptimizer = argumentTypeOptimizer;
            _dotNetToolNameNormalizer = dotNetToolNameNormalizer;
        }

        public CommandInfo Optimize(CommandInfo commandInfo)
        {
            if (commandInfo.Options.IsNullOrEmpty())
            {
                return commandInfo;
            }

            foreach (var option in commandInfo.Options)
            {
                var argument = option.Argument;
                if (argument.IsNull())
                {
                    continue;
                }

                argument.OptimizedType = _argumentTypeOptimizer.OptimizeType(argument.Type);
                option.NormalizedName = option.Name.Split('-').Select(s => s.FirstCharToUpper()).Flatten();
            }

            return commandInfo;
        }
    }
}