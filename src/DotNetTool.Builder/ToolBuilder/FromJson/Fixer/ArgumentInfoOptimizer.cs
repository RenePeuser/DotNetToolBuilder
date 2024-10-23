using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal sealed class ArgumentInfoOptimizer(IArgumentTypeOptimizer argumentTypeOptimizer)
    {
        public ArgumentInfo Optimize(ArgumentInfo argumentInfo)
        {
            if (argumentInfo.IsNull())
            {
                return argumentInfo;
            }

            argumentInfo.OptimizedType = argumentTypeOptimizer.OptimizeType(argumentInfo.Type);
            argumentInfo.NormalizedName = argumentInfo.Name.Split('-').Select(s => s.FirstCharToUpper()).Flatten();

            return argumentInfo;
        }
    }
}