using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using Argument.Check;
    using Models;

    internal class ExpressionValidator : IExpressionValidator
    {
        private readonly IEnumerable<IExpressionContentValidator> _expressionContentValidators;

        public ExpressionValidator(IEnumerable<IExpressionContentValidator> expressionContentValidators)
        {
            Throw.IfNull(() => expressionContentValidators);

            _expressionContentValidators = expressionContentValidators;
        }

        public ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNullOrWhiteSpace(() => dotNetToolName);
            Throw.IfNull(() => expressionInfo);

            if (expressionInfo.OptimizedExpressions.IsNullOrWhiteSpace())
            {
                return new ValidationResult(false, "Input must not be 'null', 'empty' or 'whitespace");
            }

            var results = _expressionContentValidators.Select(validator => validator.IsValid(dotNetToolName, expressionInfo));
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