using System.Collections.Generic;
using System.Linq;

namespace DotNetTool.Builder.Services.Optimizer
{
    internal sealed class ArgumentTypeOptimizer(IEnumerable<ITypeNameOptimizer> typeNameOptimizers) : IArgumentTypeOptimizer
    {
        public string OptimizeType(string typeName)
        {
            var result = typeNameOptimizers.Aggregate(typeName, (current, next) => next.Optimize(current));
            return result;
        }
    }
}
