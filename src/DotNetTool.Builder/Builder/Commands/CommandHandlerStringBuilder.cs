using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class CommandHandlerStringBuilder : ICommandHandlerStringBuilder
    {
        private const string template = "CommandHandler.Create<$types$>(($argument-names$) => _$command-argument-name$Service.HandleAsync(new $command-name$Parameters($argument-names$)))";

        public string Build(CliParameterInfo cliParameterInfo)
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
                yield return new CtorArgument(argumentInfo.Type, argumentInfo.Name);
            }

            foreach (var optionInfo in cliParameterInfo.Options)
            {
                yield return new CtorArgument("bool", optionInfo.ArgumentName);
            }
        }
    }
}