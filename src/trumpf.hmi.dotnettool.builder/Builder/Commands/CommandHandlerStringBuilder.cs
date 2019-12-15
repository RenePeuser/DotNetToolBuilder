using System.Collections.Generic;
using System.Linq;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder.Builder.Commands
{
    internal class CommandHandlerStringBuilder
    {
        private const string template = "CommandHandler.Create<$types$>(($argument-names$) => _$command-argument-name$Service.HandleAsync(new $command-name$Parameters($argument-names$)))";

        internal string Build(CliParameterInfo cliParameterInfo)
        {
            var arguments = BuildCtorArguments(cliParameterInfo);
            var types = arguments.Select(arg => arg.Type).Flatten(", ");
            var argNames = arguments.Select(arg => arg.Name).Flatten(", ");

            var newTemplate = template.Replace("$types$", types)
                .Replace("$command-name$", cliParameterInfo.Name.FirstCharToUpper())
                .Replace("$command-argument-name$", cliParameterInfo.Name)
                .Replace("$argument-names$", argNames);
            return newTemplate;
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
                yield return new CtorArgument("bool", optionInfo.Name);
            }
        }
    }
}