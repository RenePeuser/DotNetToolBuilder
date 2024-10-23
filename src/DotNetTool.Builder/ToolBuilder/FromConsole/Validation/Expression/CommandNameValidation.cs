using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using DotNetTool.Builder.Services.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression
{
    internal sealed class CommandNameValidation(ValidateCommandNameFromString validateCommandNameFromString) : IExpressionContentValidator
    {
        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo,
                                        string projectName)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(expressionInfo).Flatten(Environment.NewLine);
            return new ValidationResult(errors);
        }

        private IEnumerable<string> CollectErrors(ExpressionInfo expressionInfo)
        {
            var commandTokens = expressionInfo.Tokens.AllTypesAreEqualsTo<CommandToken>().ToList();
            foreach (var commandToken in commandTokens)
            {
                var command = commandToken.Value;
                var errors = validateCommandNameFromString.CollectErrors(command).ToList();

                foreach (var error in errors)
                {
                    yield return error;
                }
            }
        }
    }
}
