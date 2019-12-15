using trumpf.hmi.dotnettool.builder.Extensions;
using trumpf.hmi.dotnettool.builder.Models;

namespace trumpf.hmi.dotnettool.builder.Builder.Options
{
    public class OptionInterfaceBuilder
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