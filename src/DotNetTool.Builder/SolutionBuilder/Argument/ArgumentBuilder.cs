using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Argument
{
    internal class ArgumentBuilder : IArgumentBuilder
    {
        private const string Template =
            @"namespace $namespace$
{
    using System;
    using System.IO;
    using System.CommandLine;

    internal class $command-name$ArgumentBuilder : I$command-name$ArgumentBuilder
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

        public string Build(string projectName, CommandInfo parameterInfo, string nameSpace)
        {
            Throw.IfNullOrWhiteSpace(() => projectName);
            Throw.IfNull(() => parameterInfo);
            Throw.IfNullOrWhiteSpace(() => nameSpace);

            var currentNamespace = $"{nameSpace}.Arguments";
            var newTemplate = Template.Replace("$project-name$", projectName)
                .Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$argument-name$", parameterInfo.Argument.Name)
                .Replace("$namespace$", currentNamespace)
                .Replace("$type$", parameterInfo.Argument.OptimizedType)
                .Replace("$argument-description$", parameterInfo.Argument.Description);

            return newTemplate;
        }
    }
}
