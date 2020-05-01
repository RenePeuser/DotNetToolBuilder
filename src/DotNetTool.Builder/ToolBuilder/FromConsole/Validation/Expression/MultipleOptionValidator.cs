using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression
{
    internal class MultipleOptionValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(expressionInfo).Flatten(Environment.NewLine);
            return new ValidationResult(errors);
        }

        private IEnumerable<string> CollectErrors(ExpressionInfo expressionInfo)
        {
            var optionGroupedByName = expressionInfo.Tokens.OfType<OptionToken>().GroupBy(option => option.Value);
            var duplicatedOptions = optionGroupedByName.Where(g => g.Count() > 1);
            foreach (var duplicatedOption in duplicatedOptions)
            {
                yield return $"The option: '{duplicatedOption.Key}' is multiple used. Name of each the options must be unique";
            }
        }
    }
}
