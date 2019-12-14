namespace trumpf.hmi.dotnettool.builder
{
    public class IOptionInterfaceBuilder
    {
        private const string template = 
@"namespace $projectName$
{
    using System.Collections.Generic;
    using System.CommandLine;

    public interface I$command-name$OptionsBuilder
    {
        IEnumerable<Option> Build();
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