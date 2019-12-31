namespace DotNetTool.Builder.Builder.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using global::Argument.Check;
    using Models;

    internal class CommandHandlerNoArgsAndNoOptionBuilder : ICommandHandlerStringBuilder
    {
        private const string Template = "CommandHandler.Create(() => _$command-argument-name$Service.HandleAsync(new $command-name$Parameters($argument-names$)))";

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

            return parameterInfo.ArgumentInfo.IsNull() && parameterInfo.Options.IsNullOrEmpty();
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
    }
}