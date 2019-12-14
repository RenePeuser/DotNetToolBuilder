using System;
using System.Collections.Generic;
using System.Linq;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder
{
    public class OptionImplementationBuilder
    {
        private const string template = 
@"namespace $projectName$
{
    using System.Collections.Generic;
    using System.CommandLine;
    using $project-name$.Rendering;

    public class $command-name$OptionsBuilder : I$command-name$OptionsBuilder
    {
        public IEnumerable<Option> Build()
        {   
            $yield-option$
        }

        $build-option-method$                                       
    }
}";

        public string Build(string projectName, CliParameterInfo parameterInfo)
        {
            var optionsMethods = BuildOptionsMethodFrom(parameterInfo.Options).ToList();
            var optionsMethodAsString = optionsMethods.Select(m => m.MethodSyntax).Flatten(Environment.NewLine);
            var yieldStatements = optionsMethods.Select(m => $"yield return {m.MethodName}();").Flatten(Environment.NewLine);

            var newTemplate = template.Replace("$projectName$", projectName)
                .Replace("$command-name$", parameterInfo.Name.FirstCharToUpper())
                .Replace("$yield-option$", yieldStatements)
                .Replace("$build-option-method$", optionsMethodAsString);

            return newTemplate;
        }

        private const string optionMethodTemplate = 
@"private Option Build$option-name$Option()
{
    return new $option$;
}";

        private IEnumerable<MethodInfo> BuildOptionsMethodFrom(IEnumerable<OptionInfo> options)
        {
            foreach (var option in options)
            {
                var neewOptionStatement = BuildNewOptionString(option);
                var newMethod = optionMethodTemplate.Replace("$option$", neewOptionStatement)
                    .Replace("$option-name$", option.Name.FirstCharToUpper());

                var methodName = $"Build{option.Name.FirstCharToUpper()}Option";
                yield return new MethodInfo(methodName, newMethod);
            }
        }

        private const string optionTemplate = 
@"Option(new[] { ""$option-name$"", ""$option-alias$"" }, ""$option-description$"".AsDescription())
{
    Required = $required-value$
}";

        private string BuildNewOptionString(OptionInfo optionInfo)
        {
            var newTemplate = optionTemplate.Replace("$option-name$", optionInfo.Name)
                .Replace("$option-alias$", optionInfo.Alias)
                .Replace("$option-description$", optionInfo.Description)
                .Replace("$required-value$", optionInfo.Required.ToString().ToLower());

            return newTemplate;
        }
    }
}