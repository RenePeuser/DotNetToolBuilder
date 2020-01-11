using System.Collections.Generic;
using System.Linq;

namespace DotNetTool.Builder.Services.Optimizer
{
    internal class ArgumentTypeOptimizer : IArgumentTypeOptimizer
    {
        private readonly IEnumerable<ITypeNameOptimizer> _typeNameOptimizers;

        public ArgumentTypeOptimizer(IEnumerable<ITypeNameOptimizer> typeNameOptimizers)
        {
            _typeNameOptimizers = typeNameOptimizers;
        }

        public string OptimizeType(string typeName)
        {
            var result = _typeNameOptimizers.Aggregate(typeName, (current, next) => next.Optimize(current));
            return result;
        }
    }
}
