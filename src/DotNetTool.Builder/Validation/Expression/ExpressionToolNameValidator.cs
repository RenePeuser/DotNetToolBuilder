using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ExpressionToolNameValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            var errros = Validate(dotNetToolName, expression).ToList();
            return new ValidationResult(errros.IsNullOrEmpty(), errros.Flatten(Environment.NewLine));
        }

        private IEnumerable<string> Validate(string dotNetToolName, string expression)
        {
            if (expression.IsNullOrWhiteSpace())
            {
                yield return "Expression must not be null or empty.";
                yield break;
            }

            var toolName = expression.Split()[0];
            if (toolName.NotEqualsTo(dotNetToolName))
            {
                yield return $"Expression must start with your defined dotnet tool name: '{dotNetToolName}'";
            }
        }
    }
};