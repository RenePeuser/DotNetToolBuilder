using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Argument
{
    public class ArgumentBuilder : IArgumentBuilder
    {
        private const string template = 
@"namespace $namespace$
{
    using System.CommandLine;

    public class $command-name$ArgumentBuilder : I$command-name$ArgumentBuilder
    {                                        
        public Argument Build()
        {
            var argument = new Argument<$type$>()
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
                .Replace("$type$", parameterInfo.ArgumentInfo.Type)
                .Replace("$argument-description$", parameterInfo.ArgumentInfo.Description);

            return newTemplate;
        }
    }
}