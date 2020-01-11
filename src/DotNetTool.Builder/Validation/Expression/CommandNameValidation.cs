using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Validation.Expression
{
    internal class CommandNameValidation : IExpressionContentValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public CommandNameValidation(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(expressionInfo).Flatten(Environment.NewLine);
            return new ValidationResult(errors.IsNullOrWhiteSpace(), errors);
        }

        private IEnumerable<string> CollectErrors(ExpressionInfo expressionInfo)
        {
            var commandTokens = expressionInfo.Tokens.AllTypeEquals<CommandToken>().ToList();
            foreach (var commandToken in commandTokens)
            {
                var command = commandToken.Value;
                if (command.Contains("--") || command.Contains("<") || command.Contains("["))
                {
                    yield return $"The command: {command} most not contain argument '<arg>', typecast '[type]' or option-syntax '[--option]'";
                }

                if (char.IsLetter(command.First()).IsFalse())
                {
                    yield return $"The command: {command} must begin with a letter";
                }

                if (command.All(char.IsLetterOrDigit).IsFalse())
                {
                    yield return $"The command: {command} must only contains letters or digits";
                }

                var validationResult = _primitiveTypeNameValidator.IsTypeName(command);
                if (validationResult.IsValid)
                {
                    yield return $"The command: '{command}' must not be a name of a type";
                }
            }
        }
    }
}
