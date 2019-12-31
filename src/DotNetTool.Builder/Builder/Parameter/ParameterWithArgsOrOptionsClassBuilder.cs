using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Builder.Parameter
{
    using Models;

    internal class ParameterWithArgsOrOptionsClassBuilder : IParameterSpecificClassBuilder
    {
        private const string Template =
            @"namespace $namespace$
{
    public class $command-name$Parameters
    {
        public $command-name$Parameters($ctor-arguments$)
        {
$agrument-to-properties$
        }

$properties$
    }
}";

        private const string CtorArgument = @"$type$ $argName$";

        public string Build(string projectName, ParameterInfo parameterInfo, string nameSpace)
        {
            var ctorArguments = BuildCtorArguments(parameterInfo).ToList();
            var properties = BuildProperties(ctorArguments).ToList();
            var propertyString = BuildPropertyString(properties);

            var argumentString = BuildArguments(ctorArguments);
            var propertyInitializer = BuildPropertyInitializerString(ctorArguments, properties);

            var newTemplate = Template.Replace("$ctor-arguments$", argumentString)
                .Replace("$agrument-to-properties$", propertyInitializer)
                .Replace("$properties$", propertyString)
                .Replace("$projectName$", projectName)
                .Replace("$namespace$", nameSpace)
                .Replace("$command-name$", parameterInfo.NormalizedName);

            return newTemplate;
        }

        public bool IsThisBuilderFor(ParameterInfo parameterInfo)
        {
            return parameterInfo.ArgumentInfo.IsNotNull() || parameterInfo.Options.Any();
        }

        private string BuildPropertyString(IEnumerable<Property> properties)
        {
            var result = properties.Select(p => $"        public {p.Type} {p.Name} {{ get; }}").Flatten(Environment.NewLine);
            return result;
        }

        private string BuildPropertyInitializerString(IEnumerable<CtorArgument> arguments,
            IEnumerable<Property> properties)
        {
            var result = BuildPropertyInitializer(arguments, properties);
            return result.Flatten(Environment.NewLine);
        }

        private IEnumerable<string> BuildPropertyInitializer(IEnumerable<CtorArgument> arguments, IEnumerable<Property> properties)
        {
            foreach (var property in properties)
            {
                var argument = arguments.Single(a => a.Name.ToLower().EqualsTo(property.Name.ToLower()));
                yield return $"            {property.Name} = {argument.Name};";
            }
        }

        private string BuildArguments(IEnumerable<CtorArgument> ctorArguments)
        {
            var result = ctorArguments.Select(arg => CtorArgument.Replace("$type$", arg.Type).Replace("$argName$", arg.Name)).Flatten(", ");
            return result;
        }


        private IEnumerable<CtorArgument> BuildCtorArguments(ParameterInfo parameterInfo)
        {
            var argumentInfo = parameterInfo.ArgumentInfo;
            if (argumentInfo.IsNotNull())
            {
                yield return new CtorArgument(argumentInfo.Type, argumentInfo.Name);
            }

            foreach (var optionInfo in parameterInfo.Options)
            {
                if (optionInfo.Argument.IsNotNull())
                {
                    yield return new CtorArgument(optionInfo.Argument.Type, optionInfo.ArgumentName);
                }
                else
                {
                    yield return new CtorArgument("bool", optionInfo.ArgumentName);
                }
            }
        }

        private IEnumerable<Property> BuildProperties(IEnumerable<CtorArgument> ctorArguments)
        {
            foreach (var argument in ctorArguments)
            {
                yield return new Property(argument.Type, argument.NormalizedName);
            }
        }
    }
}