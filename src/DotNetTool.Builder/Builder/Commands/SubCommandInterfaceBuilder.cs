using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    

    internal class SubCommandInterfaceBuilder : ISubCommandInterfaceBuilder
    {
        private const string Template =
            @"namespace $namespace$
{
    using System.CommandLine;

    public interface I$command-name$SubCommandBuilder
    {
        Command Build();
    }
}";

        public string Build(string project, CommandInfo parameterInfo, CommandInfo parent, string nameSpace)
        {
            Throw.IfNullOrWhiteSpace(() => project);
            Throw.IfNull(() => parameterInfo);
            Throw.IfNull(() => parent);
            Throw.IfNullOrWhiteSpace(() => nameSpace);

            var newTemplate = Template.Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$namespace$", nameSpace)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}
