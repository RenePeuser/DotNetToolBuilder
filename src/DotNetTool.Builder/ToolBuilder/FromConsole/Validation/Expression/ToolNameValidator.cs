using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using DotNetTool.Builder.Services.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression
{
    internal class ToolNameValidator : IExpressionContentValidator
    {
        private readonly IToolNameValidator _toolNameValidator;

        public ToolNameValidator(IToolNameValidator toolNameValidator)
        {
            Throw.IfNull(() => toolNameValidator);

            _toolNameValidator = toolNameValidator;
        }

        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(dotNetDotNetToolName, expressionInfo).ToList();
            return new ValidationResult(errors.Flatten(Environment.NewLine));
        }

        private IEnumerable<string> CollectErrors(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            if (expressionInfo.OptimizedExpressions.IsNullOrWhiteSpace())
            {
                yield return $"The expression:'{expressionInfo.Expression}' must not be null or empty";
                yield break;
            }

            var firstCommand = expressionInfo.Tokens.FirstOrDefault();
            if (firstCommand.IsNull())
            {
                yield return $"The expression:'{expressionInfo.Expression}' must not be null or empty";
            }

            if (firstCommand.Value.NotEqualsTo(dotNetDotNetToolName.Value))
            {
                yield return $"The expression:'{expressionInfo.Expression}' must start with your root command: '{dotNetDotNetToolName.Value}'";
            }

            var result = _toolNameValidator.Validate(firstCommand.Value);
            if (result.IsValid.IsFalse())
            {
                yield return result.Errors;
            }
        }
    }
}
