using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    using global::Argument.Check;

    internal class CommandHandlerWithArgsOrOptionBuilder : ICommandHandlerStringBuilder
    {
        private const string Template = "CommandHandler.Create<$types$>(($argument-names$) => _$command-argument-name$Service.HandleAsync(new $command-name$Parameters($argument-names$)))";

        public string Build(ParameterInfo parameterInfo)
        {
            Throw.IfNull(() => parameterInfo);

            var arguments = BuildCtorArguments(parameterInfo);
            var types = arguments.Select(arg => arg.Type).Flatten(", ");
            var argNames = arguments.Select(arg => arg.Name).Flatten(", ");

            var newTemplate = Template.Replace("$types$", types)
                                      .Replace("$command-name$", parameterInfo.NormalizedName)
                                      .Replace("$command-argument-name$", parameterInfo.AsArgumentName)
                                      .Replace("$argument-names$", argNames);
            return newTemplate;
        }

        public bool IsThisBuilderFor(ParameterInfo parameterInfo)
        {
            Throw.IfNull(() => parameterInfo);

            return parameterInfo.ArgumentInfo.IsNotNull() || parameterInfo.Options.Any();
        }

        internal IEnumerable<CtorArgument> BuildCtorArguments(ParameterInfo parameterInfo)
        {
            Throw.IfNull(() => parameterInfo);

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
    }
}
