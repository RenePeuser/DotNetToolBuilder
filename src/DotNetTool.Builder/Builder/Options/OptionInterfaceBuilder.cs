using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    public class OptionInterfaceBuilder : IOptionInterfaceBuilder
    {
        private const string Template =
            @"namespace $namespace$
{
    using System.Collections.Generic;
    using System.CommandLine;

    public interface I$command-name$OptionsBuilder
    {
        IEnumerable<Option> Build();
    }
}";

        public string Build(string projectName, ParameterInfo parameterInfo, string nameSpace)
        {
            var currentNamespace = $"{nameSpace}.Options";
            var newTemplate = Template.Replace("$projectName$", projectName)
                                      .Replace("$namespace$", currentNamespace)
                                      .Replace("$command-name$", parameterInfo.NormalizedName);

            return newTemplate;
        }
    }
}
