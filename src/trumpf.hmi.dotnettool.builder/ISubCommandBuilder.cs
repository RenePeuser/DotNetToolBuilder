namespace trumpf.hmi.dotnettool.builder
{
    internal class ISubCommandBuilder
    {
        private const string template =
@"namespace $project-name$
{
    using System.CommandLine;

    public interface I$command-name$SubCommandBuilder
    {
        Command Build();
    }
}";

        internal string Build(string project, CliParameterInfo cliParameterInfo, CliParameterInfo parent)
        {
            var newTemplate = template.Replace("$command-name$", cliParameterInfo.Name.FirstCharToUpper())
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}