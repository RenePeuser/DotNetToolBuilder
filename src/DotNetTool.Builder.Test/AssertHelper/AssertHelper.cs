namespace DotNetTool.Builder.Test.AssertHelper
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Extensions;
    using Models;
    using NSubstitute.Core;

    public static class AssertHelper
    {
        internal static IEnumerable<string> AssertAllSubCommandRecursive(this CommandInfo commandInfo, Predicate<CommandInfo> validateFunc, Func<CommandInfo, string> errorMessage)
        {
            if (commandInfo.IsNull())
            {
                yield break;
            }

            if (commandInfo.SubCommands.IsNullOrEmpty())
            {
                yield break;
            }

            if (validateFunc(commandInfo))
            {
                yield return errorMessage(commandInfo);
            }

            foreach (var subCommand in commandInfo.SubCommands)
            {
                var errors = AssertAllSubCommandRecursive(subCommand, validateFunc, errorMessage);
                foreach (var error in errors)
                {
                    yield return error;
                }
            }
        }

        internal static string ToErrorMessage(this IEnumerable<string> errors, string title)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(title);
            stringBuilder.AppendLine();

            errors.ForEach(error => stringBuilder.AppendLine($"- {error}"));
            return stringBuilder.ToString();
        }
    }
}
