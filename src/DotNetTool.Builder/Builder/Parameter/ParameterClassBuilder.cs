using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Parameter
{
    internal class ParameterClassBuilder : IParameterClassBuilder
    {
        private readonly IEnumerable<IParameterSpecificClassBuilder> _parameterSpecificClassBuilders;

        public ParameterClassBuilder(IEnumerable<IParameterSpecificClassBuilder> parameterSpecificClassBuilders)
        {
            _parameterSpecificClassBuilders = parameterSpecificClassBuilders;
        }

        public string Build(string projectName, ParameterInfo parameterInfo, string nameSpace)
        {
            var builder = _parameterSpecificClassBuilders.Single(builder => builder.IsThisBuilderFor(parameterInfo));
            return builder.Build(projectName, parameterInfo, nameSpace);
        }
    }
}