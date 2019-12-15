using trumpf.hmi.dotnettool.builder.Extensions;
using trumpf.hmi.dotnettool.builder.Models;

namespace trumpf.hmi.dotnettool.builder.Builder.Argument
{
    public class ArgumentBuilder
    {
        private const string template = 
@"namespace $namespace$
{
    using System.CommandLine;

    public class $command-name$ArgumentBuilder : I$command-name$ArgumentBuilder
    {                                        
        public Argument Build()
        {
            var argument = new Argument<object>()
            {
                Name = ""$argument-name$"",
                Description = ""$argument-description$""
            };
            
            return argument;
        }
    }
}";

        public string Build(string projectName, CliParameterInfo parameterInfo, string nameSpace)
        {
            var newTemplate = template.Replace("$project-name$", projectName)
                .Replace("$command-name$", parameterInfo.Name.FirstCharToUpper())
                .Replace("$argument-name$", parameterInfo.ArgumentInfo.Name)
                .Replace("$namespace$", nameSpace)
                .Replace("$argument-description$", parameterInfo.ArgumentInfo.Description);

            return newTemplate;
        }
    }
}