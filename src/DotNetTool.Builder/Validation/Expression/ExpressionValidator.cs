using System.Collections.Generic;
using System.Linq;
using System.Text;
using Argument.Check;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Validation.Expression
{
    internal class ExpressionValidator : IExpressionValidator
    {
        private readonly IEnumerable<IExpressionContentValidator> _expressionContentValidators;

        public ExpressionValidator(IEnumerable<IExpressionContentValidator> expressionContentValidators)
        {
            Throw.IfNull(() => expressionContentValidators);

            _expressionContentValidators = expressionContentValidators;
        }

        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            if (expressionInfo.OptimizedExpressions.IsNullOrWhiteSpace())
            {
                return new ValidationResult(false, "Input must not be 'null', 'empty' or 'whitespace");
            }

            var results = _expressionContentValidators.Select(validator => validator.IsValid(dotNetDotNetToolName, expressionInfo));
            if (results.All(r => r.IsValid))
            {
                return new ValidationResult(true, null);
            }

            var stringBuilder = new StringBuilder();
            results.Where(result => result.IsValid.IsFalse()).ForEach(result => stringBuilder.AppendLine(result.Errors));

            return new ValidationResult(false, stringBuilder.ToString());
        }
    }
}
