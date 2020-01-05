using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    

    internal class OptionInterfaceBuilder : IOptionInterfaceBuilder
    {
        private const string Template =
            @"namespace $namespace$
{
    using System.Collections.Generic;
    using System.CommandLine;

    internal interface I$command-name$OptionsBuilder
    {
        IEnumerable<Option> Build();
    }
}";

        public string Build(string projectName, CommandInfo parameterInfo, string nameSpace)
        {
            Throw.IfNullOrWhiteSpace(() => projectName);
            Throw.IfNull(() => parameterInfo);
            Throw.IfNullOrWhiteSpace(() => nameSpace);

            var currentNamespace = $"{nameSpace}.Options";
            var newTemplate = Template.Replace("$projectName$", projectName)
                                      .Replace("$namespace$", currentNamespace)
                                      .Replace("$command-name$", parameterInfo.NormalizedName);

            return newTemplate;
        }
    }
}
