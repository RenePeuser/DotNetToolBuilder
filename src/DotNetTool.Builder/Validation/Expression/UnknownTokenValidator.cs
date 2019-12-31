namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Argument.Check;
    using Extensions;
    using Models;
    using Tokenizer.Tokens;

    public class UnknownTokenValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNullOrWhiteSpace(() => dotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(dotNetToolName, expressionInfo).Flatten(Environment.NewLine);
            return new ValidationResult(errors.IsNullOrWhiteSpace(), errors);
        }

        private IEnumerable<string> CollectErrors(string dotNetToolName, ExpressionInfo expressionInfo)
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
            yield return $"{dotNetToolName} command <cmd-arg> --option <opt-arg>";
        }
    }
}