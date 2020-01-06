using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Parameter
{
    internal class ParameterClassBuilder : IParameterClassBuilder
    {
        private readonly IEnumerable<IParameterSpecificClassBuilder> _parameterSpecificClassBuilders;

        public ParameterClassBuilder(IEnumerable<IParameterSpecificClassBuilder> parameterSpecificClassBuilders)
        {
            Throw.IfNull(() => parameterSpecificClassBuilders);

            _parameterSpecificClassBuilders = parameterSpecificClassBuilders;
        }

        public string Build(string projectName, CommandInfo parameterInfo, string nameSpace)
        {
            Throw.IfNullOrWhiteSpace(() => projectName);
            Throw.IfNull(() => parameterInfo);
            Throw.IfNullOrWhiteSpace(() => nameSpace);

            var builder = _parameterSpecificClassBuilders.Single(b => b.IsThisBuilderFor(parameterInfo));
            return builder.Build(projectName, parameterInfo, nameSpace);
        }
    }
}
