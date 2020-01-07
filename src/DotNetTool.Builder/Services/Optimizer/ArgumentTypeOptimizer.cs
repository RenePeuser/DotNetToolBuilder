using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Services.Optimizer
{
    internal class ArgumentTypeOptimizer : IArgumentTypeOptimizer
    {
        readonly IEnumerable<ITypeNameOptimizer> _typeNameOptimizers;

        public ArgumentTypeOptimizer(IEnumerable<ITypeNameOptimizer> typeNameOptimizers)
        {
            _typeNameOptimizers = typeNameOptimizers;
        }

        public string OptimizeType(string typeName)
        {
            var optimizer = _typeNameOptimizers.FirstOrDefault(optimizer => optimizer.OptimizerFor(typeName));
            if (optimizer.IsNull())
            {
                return typeName;
            }

            return optimizer.Optimize(typeName);
        }
    }
}
