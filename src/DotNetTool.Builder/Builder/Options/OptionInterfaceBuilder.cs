using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    public class OptionInterfaceBuilder : IOptionInterfaceBuilder
    {
        private const string template =
@"namespace $namespace$
{
    using System.Collections.Generic;
    using System.CommandLine;

    public interface I$command-name$OptionsBuilder
    {
        IEnumerable<Option> Build();
    }
}";

        public string Build(string projectName, CliParameterInfo parameterInfo, string nameSpace)
        {
            var newTemplate = template.Replace("$projectName$", projectName)
                .Replace("$namespace$", nameSpace)
                .Replace("$command-name$", parameterInfo.Name.FirstCharToUpper());

            return newTemplate;
        }
    }
}