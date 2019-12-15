namespace trumpf.hmi.dotnettool.builder.Builder.Commands
{
    internal class SubCommandInterfaceBuilder
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

        internal string Build(string project, CliParameterInfo cliParameterInfo, CliParameterInfo parent, string nameSpace)
        {
            var newTemplate = template.Replace("$command-name$", cliParameterInfo.Name.FirstCharToUpper())
                .Replace("$namespace$", nameSpace)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}