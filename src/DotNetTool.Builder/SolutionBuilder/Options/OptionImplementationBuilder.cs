using System;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;
using Extensions.Pack;

namespace DotNetTool.Builder.SolutionBuilder.Options
{
    internal class OptionImplementationBuilder : IOptionImplementationBuilder
    {
        private const string Template =
            @"namespace $namespace$
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.CommandLine; 

    internal class $command-name$OptionsBuilder : I$command-name$OptionsBuilder
    {
        public IEnumerable<Option> Build()
        {   
$yield-option$
        }

$build-option-method$                                       
    }
}";

        private readonly IOptionMethodsBuilder _optionMethodsBuilder;

        public OptionImplementationBuilder(IOptionMethodsBuilder optionMethodsBuilder)
        {
            Throw.IfNull(() => optionMethodsBuilder);

            _optionMethodsBuilder = optionMethodsBuilder;
        }

        public string Build(string projectName, CommandInfo parameterInfo, string nameSpace)
        {
            Throw.IfNullOrWhiteSpace(() => projectName);
            Throw.IfNull(() => parameterInfo);
            Throw.IfNullOrWhiteSpace(() => nameSpace);

            var currentNamespace = $"{nameSpace}.Options";
            var optionsMethods = _optionMethodsBuilder.Build(parameterInfo.Options).ToList();
            var optionsMethodAsString = optionsMethods.Select(m => m.MethodSyntax).Flatten(Environment.NewLine);
            var yieldStatements = optionsMethods.Select(m => $"            yield return {m.MethodName}();").Flatten(Environment.NewLine);

            var newTemplate = Template.Replace("$project-name$", projectName)
                .Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$yield-option$", yieldStatements)
                .Replace("$namespace$", currentNamespace)
                .Replace("$build-option-method$", optionsMethodAsString);

            return newTemplate;
        }
    }
}
