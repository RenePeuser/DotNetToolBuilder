using trumpf.hmi.dotnettool.builder;
using trumpf.hmi.dotnettool.builder.Builder.Commands;

internal class RootCommandInterfaceBuilder
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

    internal string Build(string project, CliParameterInfo cliParameterInfo, string nameSpace)
    {
        var newTemplate = template.Replace("$command-name$", cliParameterInfo.Name.FirstCharToUpper())
            .Replace("$namespace$", nameSpace)
            .Replace("$project-name$", project);        

        return newTemplate;
    }
}