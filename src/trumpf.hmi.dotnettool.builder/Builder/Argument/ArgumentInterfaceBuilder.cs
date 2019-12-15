namespace trumpf.hmi.dotnettool.builder.Builder.Argument
{
    public class ArgumentInterfaceBuilder
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

        public string Build(string projectName, CliParameterInfo parameterInfo, string nameSpace)
        {
            var newTemplate = template.Replace("$projectName$", projectName)
                .Replace("$namespace$", nameSpace)
                .Replace("$command-name$", parameterInfo.Name.FirstCharToUpper());

            return newTemplate;
        }
    }
}