namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Argument.Check;
    using Extensions;
    using Models;
    using Tokenizer.Tokens;

    internal class MultipleOptionValidator : IExpressionContentValidator
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
            var optionGroupedByName = expressionInfo.Tokens.OfType<OptionToken>().GroupBy(option => option.Value);
            var duplicatedOptions = optionGroupedByName.Where(g => g.Count() > 1);
            foreach (var duplicatedOption in duplicatedOptions)
            {
                yield return $"Option: '{duplicatedOption.Key}' is multiple used. Name of each the options must be unique";
            }
        }
    }
}