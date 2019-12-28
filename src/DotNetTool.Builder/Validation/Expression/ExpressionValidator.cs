using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    public class ExpressionValidator : IExpressionValidator
    {
        private readonly IEnumerable<IExpressionContentValidator> _expressionContentValidators;

        public ExpressionValidator(IEnumerable<IExpressionContentValidator> expressionContentValidators)
        {
            _expressionContentValidators = expressionContentValidators;
        }

        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            if (expression.IsNullOrWhiteSpace())
            {
                return new ValidationResult(false, "Input must not be 'null', 'empty' or 'whitespace");
            }

            var results = _expressionContentValidators.Select(validator => validator.IsValid(dotNetToolName, expression));
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