using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Argument
{
    public interface IArgumentInterfaceBuilder
    {
        string Build(string projectName, ParameterInfo parameterInfo, string nameSpace);
    }

    public class ArgumentInterfaceBuilder : IArgumentInterfaceBuilder
    {
        private const string Template =
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
            var newTemplate = Template.Replace("$projectName$", projectName)
                .Replace("$namespace$", nameSpace)
                .Replace("$command-name$", parameterInfo.NormalizedName);

            return newTemplate;
        }
    }
}
