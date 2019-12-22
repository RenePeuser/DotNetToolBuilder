using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class RootCommandInterfaceBuilder : IRootCommandInterfaceBuilder
    {
        private const string template =
            @"namespace $namespace$
{
    using System.CommandLine;

    public interface I$command-name$CommandBuilder
    {
        Command Build();
    }
}";

        public string Build(string project, ParameterInfo parameterInfo, string nameSpace)
        {
            var newTemplate = template.Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$namespace$", nameSpace)
                .Replace("$project-name$", project);        

            return newTemplate;
        }
    }
}