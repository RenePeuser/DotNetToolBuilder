using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal sealed class ArgumentInfoOptimizer
    {
        private readonly IArgumentTypeOptimizer _argumentTypeOptimizer;

        public ArgumentInfoOptimizer(IArgumentTypeOptimizer argumentTypeOptimizer)
        {
            _argumentTypeOptimizer = argumentTypeOptimizer;
        }

        public ArgumentInfo Optimize(ArgumentInfo argumentInfo)
        {
            if (argumentInfo.IsNull())
            {
                return argumentInfo;
            }

            argumentInfo.OptimizedType = _argumentTypeOptimizer.OptimizeType(argumentInfo.Type);
            argumentInfo.NormalizedName = argumentInfo.Name.Split('-').Select(s => s.FirstCharToUpper()).Flatten();

            return argumentInfo;
        }
    }
}