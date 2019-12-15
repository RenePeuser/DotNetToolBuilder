using System;
using System.Collections.Generic;
using System.Linq;
using trumpf.hmi.dotnettool.builder.Extensions;
using trumpf.hmi.dotnettool.builder.Models;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder.Builder.Parameter
{
    internal class ParameterClassBuilder
    {
        private const string template =
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

        private const string ctorArgument = @"$type$ $argName$";

        internal string Build(string projectName, CliParameterInfo cliParameterInfo, string nameSpace)
        {
            var ctorArguments = BuildCtorArguments(cliParameterInfo);
            var properties = BuildProperties(ctorArguments);
            var propertyString = BuildPropertyString(properties);

            var argumentString = BuildArguments(ctorArguments);
            var propertyInitializer = BuildPropertyInitializerString(ctorArguments, properties);

            var newTemplate = template.Replace("$ctor-arguments$", argumentString)
                                      .Replace("$agrument-to-properties$", propertyInitializer)
                                      .Replace("$properties$", propertyString)
                                      .Replace("$projectName$", projectName)
                                      .Replace("$namespace$", nameSpace)
                                      .Replace("$command-name$", cliParameterInfo.Name.FirstCharToUpper());

            return newTemplate;
        }

        private string BuildPropertyString(IEnumerable<Property> properties)
        {
            var result = properties.Select(p => $"public {p.Type} {p.Name} {"{"} get; {"}"}").Flatten(Environment.NewLine);
            return result;
        }

        private string BuildPropertyInitializerString(IEnumerable<CtorArgument> arguments, IEnumerable<Property> properties)
        {
            var result = BuildPropertyInitializer(arguments, properties);
            return result.Flatten(Environment.NewLine);
        }

        private IEnumerable<string> BuildPropertyInitializer(IEnumerable<CtorArgument> arguments, IEnumerable<Property> properties)
        {
            foreach (var property in properties)
            {
                var argument = arguments.Single(a => a.Name.ToLower().EqualsTo(property.Name.ToLower()));
                yield return $"{property.Name} = {argument.Name};";
            }
        }

        private string BuildArguments(IEnumerable<CtorArgument> ctorArguments)
        {
            var result = ctorArguments.Select(arg => ctorArgument.Replace("$type$", arg.Type).Replace("$argName$", arg.Name)).Flatten(", ");
            return result;
        }


        internal IEnumerable<CtorArgument> BuildCtorArguments(CliParameterInfo cliParameterInfo)
        {
            var argumentInfo = cliParameterInfo.ArgumentInfo;
            if (argumentInfo.IsNotNull())
            {
                yield return new CtorArgument("object", argumentInfo.Name);
            }

            foreach (var optionInfo in cliParameterInfo.Options)
            {
                yield return new CtorArgument("bool", optionInfo.ArgumentName);
            }
        }

        internal IEnumerable<Property> BuildProperties(IEnumerable<CtorArgument> ctorArguments)
        {
            foreach (var argument in ctorArguments)
            {
                yield return new Property(argument.Type, argument.Name.FirstCharToUpper());
            }
        }
    }
}