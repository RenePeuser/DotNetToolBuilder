using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Argument
{
    internal class ArgumentInterfaceBuilder : IArgumentInterfaceBuilder
    {
        private const string Template =
@"using System;
using System.IO;
using System.CommandLine;

namespace $namespace$
{    
    internal interface I$command-name$ArgumentBuilder
    {
        Argument Build();
    }
}";

        public string Build(string projectName, CommandInfo parameterInfo, string nameSpace)
        {
            Throw.IfNullOrWhiteSpace(projectName);
            Throw.IfNull(() => parameterInfo);
            Throw.IfNullOrWhiteSpace(nameSpace);

            var currentNamespace = $"{nameSpace}.Arguments";
            var newTemplate = Template.Replace("$projectName$", projectName)
                .Replace("$namespace$", currentNamespace)
                .Replace("$command-name$", parameterInfo.NormalizedName);

            return newTemplate;
        }
    }
}
