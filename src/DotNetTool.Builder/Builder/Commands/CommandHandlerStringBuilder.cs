using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class CommandHandlerStringBuilder : ICommandHandlerStringBuilder
    {
        private const string template =
            "CommandHandler.Create<$types$>(($argument-names$) => _$command-argument-name$Service.HandleAsync(new $command-name$Parameters($argument-names$)))";

        public string Build(ParameterInfo parameterInfo)
        {
            var arguments = BuildCtorArguments(parameterInfo);
            var types = arguments.Select(arg => arg.Type).Flatten(", ");
            var argNames = arguments.Select(arg => arg.Name).Flatten(", ");

            var newTemplate = template.Replace("$types$", types)
                .Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$command-argument-name$", parameterInfo.Name)
                .Replace("$argument-names$", argNames);
            return newTemplate;
        }

        internal IEnumerable<CtorArgument> BuildCtorArguments(ParameterInfo parameterInfo)
        {
            var argumentInfo = parameterInfo.ArgumentInfo;
            if (argumentInfo.IsNotNull()) yield return new CtorArgument(argumentInfo.Type, argumentInfo.Name);

            foreach (var optionInfo in parameterInfo.Options)
                yield return new CtorArgument("bool", optionInfo.ArgumentName);
        }
    }
}