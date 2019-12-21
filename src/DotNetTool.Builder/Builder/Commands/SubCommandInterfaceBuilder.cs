using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class SubCommandInterfaceBuilder : ISubCommandInterfaceBuilder
    {
        private const string template =
@"namespace $namespace$
{
    using System.CommandLine;

    public interface I$command-name$SubCommandBuilder
    {
        Command Build();
    }
}";

        public string Build(string project, CliParameterInfo cliParameterInfo, CliParameterInfo parent, string nameSpace)
        {
            var newTemplate = template.Replace("$command-name$", cliParameterInfo.Name.FirstCharToUpper())
                .Replace("$namespace$", nameSpace)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}