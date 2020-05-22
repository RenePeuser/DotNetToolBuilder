using System.Collections.Generic;
using System.Linq;
using System.Text;
using Argument.Check;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression
{
    internal class ExpressionValidator : IExpressionValidator
    {
        private readonly IEnumerable<IExpressionContentValidator> _expressionContentValidators;

        public ExpressionValidator(IEnumerable<IExpressionContentValidator> expressionContentValidators)
        {
            Throw.IfNull(() => expressionContentValidators);

            _expressionContentValidators = expressionContentValidators;
        }

        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo, string projectName)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            if (expressionInfo.OptimizedExpressions.IsNullOrWhiteSpace())
            {
                return new ValidationResult("Input must not be 'null', 'empty' or 'whitespace");
            }

            var results = _expressionContentValidators.Select(validator => validator.IsValid(dotNetDotNetToolName, expressionInfo, projectName));
            if (results.All(r => r.IsValid))
            {
                return new ValidationResult(null);
            }

            var stringBuilder = new StringBuilder();
            results.Where(result => result.IsValid.IsFalse()).ForEach(result => stringBuilder.AppendLine(result.Errors));

            return new ValidationResult(stringBuilder.ToString());
        }
    }
}
