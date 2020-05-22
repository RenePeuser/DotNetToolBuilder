using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression
{
    internal class UnknownTokenValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo,
            string projectName)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(dotNetDotNetToolName, expressionInfo).Flatten(Environment.NewLine);
            return new ValidationResult(errors);
        }

        private IEnumerable<string> CollectErrors(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            var unknownTokens = expressionInfo.Tokens.OfType<UnknownToken>().ToList();
            if (unknownTokens.IsEmpty())
            {
                yield break;
            }

            if (unknownTokens.Any())
            {
                yield return "Unknown tokens detected:";
            }

            foreach (var commandToken in unknownTokens)
            {
                yield return commandToken.Value;
            }

            yield return "Only commands, options or aguments are allowed. Sample:";
            yield return $"{dotNetDotNetToolName.Name} command <cmd-arg> --option <opt-arg>";
        }
    }
}
