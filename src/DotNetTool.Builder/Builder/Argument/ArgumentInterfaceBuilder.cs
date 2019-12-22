using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Argument
{
    public interface IArgumentInterfaceBuilder
    {
        string Build(string projectName, ParameterInfo parameterInfo, string nameSpace);
    }

    public class ArgumentInterfaceBuilder : IArgumentInterfaceBuilder
    {
        private const string template =
@"namespace $namespace$
{
    using System.CommandLine;

    public interface I$command-name$ArgumentBuilder
    {
        Argument Build();
    }
}";

        public string Build(string projectName, ParameterInfo parameterInfo, string nameSpace)
        {
            var newTemplate = template.Replace("$projectName$", projectName)
                .Replace("$namespace$", nameSpace)
                .Replace("$command-name$", parameterInfo.Name.FirstCharToUpper());

            return newTemplate;
        }
    }
}