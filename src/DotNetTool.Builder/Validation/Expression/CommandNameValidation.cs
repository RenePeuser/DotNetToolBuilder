namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Argument.Check;
    using Extensions;
    using Models;
    using Tokenizer.Tokens;

    internal class CommandNameValidation : IExpressionContentValidator
    {
        public ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNullOrWhiteSpace(() => dotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(expressionInfo).Flatten(Environment.NewLine);
            return new ValidationResult(errors.IsNullOrWhiteSpace(), errors);
        }

        private IEnumerable<string> CollectErrors(ExpressionInfo expressionInfo)
        {
            var commandTokens = expressionInfo.Tokens.OfType<CommandToken>().ToList();
            foreach (var commandToken in commandTokens)
            {
                var command = commandToken.Value;
                if (command.Contains("--") || command.Contains("<") || command.Contains("["))
                {
                    yield return $"Command: {command} most not contain argument '<arg>', typecast '[type]' or option-syntax '[--option]'";
                }

                if (char.IsLetter(command.First()).IsFalse())
                {
                    yield return $"Argument: {command} must begin with a letter";
                }
            }
        }
    }
}