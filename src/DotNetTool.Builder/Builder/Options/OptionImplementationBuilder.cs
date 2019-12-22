using System;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    public class OptionImplementationBuilder : IOptionImplementationBuilder
    {
        private readonly IOptionMethodsBuilder _optionMethodsBuilder;

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

        public OptionImplementationBuilder(IOptionMethodsBuilder optionMethodsBuilder)
        {
            _optionMethodsBuilder = optionMethodsBuilder;
        }

        public string Build(string projectName, ParameterInfo parameterInfo, string nameSpace)
        {
            var optionsMethods = _optionMethodsBuilder.Build(parameterInfo.Options).ToList();
            var optionsMethodAsString = optionsMethods.Select(m => m.MethodSyntax).Flatten(Environment.NewLine);
            var yieldStatements = optionsMethods.Select(m => $"yield return {m.MethodName}();").Flatten(Environment.NewLine);

            var newTemplate = template.Replace("$project-name$", projectName)
                .Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$yield-option$", yieldStatements)
                .Replace("$namespace$", nameSpace)
                .Replace("$build-option-method$", optionsMethodAsString);

            return newTemplate;
        }
    }
}