using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Tokenizer.Tokens;

    public class OptionValidator : IExpressionContentValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public OptionValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        public ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo)
        {
            var errors = CollectErrors(expressionInfo).Flatten(Environment.NewLine);
            return new ValidationResult(errors.IsNullOrWhiteSpace(), errors);
        }

        private IEnumerable<string> CollectErrors(ExpressionInfo expressionInfo)
        {
            var optionTokens = expressionInfo.Tokens.OfType<OptionToken>().ToList();
            foreach (var optionToken in optionTokens)
            {
                var option = optionToken.Value;

                var test = option.Split("--");
                if (test[1].StartsWith("-"))
                {
                    yield return $"Option: '{option}' must start with: '--'. Sample: '--option' or --my-option";
                    continue;
                }

                var optionName = option.TrimStart('-');
                if (optionName.IsNullOrWhiteSpace())
                {
                    yield return $"Option: '{option}' is missing name";
                    continue;
                }

                if (optionName.Contains("<") || optionName.Contains(">"))
                {
                    yield return $"Option '{option}' contains argument syntax, please separate the argument with a whitespace";
                }

                if (optionName.Contains("[") || optionName.Contains("]"))
                {
                    yield return $"Option '{option}' contains type cast syntax, type cast is only valid at argument";
                }

                if (optionName.Contains("--"))
                {
                    yield return $"Option '{option}' contains '--' is only allowed at the beginning, to separate verbs use '-'";
                }

                if (char.IsLetterOrDigit(option.Last()).IsFalse())
                {
                    yield return $"Option: '{option}' must ends only with a letter or digit";
                }

                if (char.IsLetter(optionName.First()).IsFalse())
                {
                    yield return $"Option: {optionName} must begin with a letter";
                }

                var validationResult = _primitiveTypeNameValidator.IsValid(optionName);
                if (validationResult.IsValid.IsFalse())
                {
                    yield return $"The name of an argument does not match a name of a type: {optionName}";
                }
            }
        }
    }
}