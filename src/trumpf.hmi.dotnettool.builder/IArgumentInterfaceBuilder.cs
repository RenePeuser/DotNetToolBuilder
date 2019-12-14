namespace trumpf.hmi.dotnettool.builder
{
    public class IArgumentInterfaceBuilder
    {
        private const string template = 
@"namespace $projectName$
{
    using System.CommandLine;   

    public interface I$command-name$ArgumentBuilder
    {
        Argument Build();
    }
}";

        public string Build(string projectName, CliParameterInfo parameterInfo)
        {
            var newTemplate = template.Replace("$projectName$", projectName)
                .Replace("$command-name$", parameterInfo.Name.FirstCharToUpper());

            return newTemplate;
        }
    }
}