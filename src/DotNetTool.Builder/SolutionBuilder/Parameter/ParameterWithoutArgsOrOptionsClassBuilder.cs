using Argument.Check;
using DotNetTool.Builder.Models;
using Extensions.Pack;

namespace DotNetTool.Builder.SolutionBuilder.Parameter
{
    internal class ParameterWithoutArgsOrOptionsClassBuilder : IParameterSpecificClassBuilder
    {
        private const string Template =
@"
using System;
using System.IO;

namespace $namespace$
{    
    internal class $command-name$Parameters
    {
        public $command-name$Parameters()
        {
        }
    }
}";

        public string Build(string projectName, CommandInfo parameterInfo, string nameSpace)
        {
            Throw.IfNullOrWhiteSpace(() => projectName);
            Throw.IfNull(() => parameterInfo);
            Throw.IfNullOrWhiteSpace(() => nameSpace);

            var newTemplate = Template.Replace("$projectName$", projectName)
                .Replace("$namespace$", nameSpace)
                .Replace("$command-name$", parameterInfo.NormalizedName);

            return newTemplate;
        }

        public bool IsThisBuilderFor(CommandInfo parameterInfo)
        {
            Throw.IfNull(() => parameterInfo);

            return parameterInfo.Argument.IsNull() && parameterInfo.Options.IsNullOrEmpty();
        }
    }
}
