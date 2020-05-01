using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Commands
{
    internal class RootCommandInterfaceBuilder : IRootCommandInterfaceBuilder
    {
        private const string Template =
            @"namespace $namespace$
{
    using System;
    using System.IO;
    using System.CommandLine;

    internal interface I$command-name$CommandBuilder
    {
        Command Build();
    }
}";

        public string Build(string project, CommandInfo parameterInfo, string nameSpace)
        {
            Throw.IfNullOrWhiteSpace(() => project);
            Throw.IfNull(() => parameterInfo);
            Throw.IfNullOrWhiteSpace(() => nameSpace);

            var newTemplate = Template.Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$namespace$", nameSpace)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}
