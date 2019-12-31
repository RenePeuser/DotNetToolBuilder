using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class ToolNameValidator : IExpressionContentValidator
    {
        private readonly IToolNameValidator _toolNameValidator;

        public ToolNameValidator(IToolNameValidator toolNameValidator)
        {
            _toolNameValidator = toolNameValidator;
        }

        public ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo)
        {
            var errors = CollectErrors(dotNetToolName, expressionInfo).ToList();
            return new ValidationResult(errors.IsEmpty(), errors.Flatten(Environment.NewLine));
        }

        private IEnumerable<string> CollectErrors(string dotNetToolName, ExpressionInfo expressionInfo)
        {
            if (expressionInfo.OptimizedExpressions.IsNullOrWhiteSpace())
            {
                yield return "Expression must not be null or empty.";
                yield break;
            }

            var firstCommand = expressionInfo.Tokens.FirstOrDefault();
            if (firstCommand.IsNull())
            {
                yield return "Expression must not be null or empty.";
            }

            var result = _toolNameValidator.IsValid(firstCommand.Value);
            if (result.IsValid.IsFalse())
            {
                yield return result.Errors;
            }
        }
    }
};