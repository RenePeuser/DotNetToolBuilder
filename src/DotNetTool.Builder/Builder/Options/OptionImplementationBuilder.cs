using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Builder.Options
{
    public class OptionImplementationBuilder : IOptionImplementationBuilder
    {
        private const string template =
@"namespace $namespace$
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

        public string Build(string projectName, ParameterInfo parameterInfo, string nameSpace)
        {
            var optionsMethods = BuildOptionsMethodFrom(parameterInfo.Options).ToList();
            var optionsMethodAsString = optionsMethods.Select(m => m.MethodSyntax).Flatten(Environment.NewLine);
            var yieldStatements = optionsMethods.Select(m => $"yield return {m.MethodName}();").Flatten(Environment.NewLine);

            var newTemplate = template.Replace("$project-name$", projectName)
                .Replace("$command-name$", parameterInfo.Name.FirstCharToUpper())
                .Replace("$yield-option$", yieldStatements)
                .Replace("$namespace$", nameSpace)
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
                    .Replace("$option-name$", option.NormalizedValue);

                var methodName = $"Build{option.NormalizedValue}Option";
                yield return new MethodInfo(methodName, newMethod);
            }
        }

        private const string optionTemplate =
@"Option(new[] { ""$option-name$"", ""$option-alias$"" }, ""$option-description$"".AsDescription())
{
    Required = $required-value$
}";

        private const string optionArgumentTemplate =
@"Option(new[] { ""$option-name$"", ""$option-alias$"" }, ""$option-description$"".AsDescription())
{
    Required = $required-value$,
    Argument = new Argument(""$option-argument-name$"")
}";

        private string BuildNewOptionString(OptionInfo optionInfo)
        {
            return optionInfo.Argument.IsNotNull()
                ? BuildNewOptionWithArgumentString(optionInfo)
                : BuildNewOptionWithoutArgumentString(optionInfo);
        }

        private string BuildNewOptionWithoutArgumentString(OptionInfo optionInfo)
        {
            var newTemplate = optionTemplate.Replace("$option-name$", optionInfo.Name)
                .Replace("$option-alias$", optionInfo.Alias)
                .Replace("$option-description$", optionInfo.Description)
                .Replace("$required-value$", optionInfo.Required.ToString().ToLower());

            return newTemplate;
        }

        private string BuildNewOptionWithArgumentString(OptionInfo optionInfo)
        {
            var newTemplate = optionArgumentTemplate.Replace("$option-name$", optionInfo.Name)
                .Replace("$option-alias$", optionInfo.Alias)
                .Replace("$option-argument-name$", optionInfo.Argument.Name)
                .Replace("$option-description$", optionInfo.Description)
                .Replace("$required-value$", optionInfo.Required.ToString().ToLower());

            return newTemplate;
        }
    }
}