using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using DotNetTool.Builder.Services.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression
{
    internal sealed class OptionValidator : IExpressionContentValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public OptionValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            Throw.IfNull(() => primitiveTypeNameValidator);

            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo,
            string projectName)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(expressionInfo).Flatten(Environment.NewLine);
            return new ValidationResult(errors);
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
                    yield return $"The option: '{option}' must start with: '--'. Sample: '--option' or --my-option";
                    continue;
                }

                var optionName = option.TrimStart('-');
                if (optionName.IsNullOrWhiteSpace())
                {
                    yield return $"The option: '{option}' is missing name";
                    continue;
                }

                if (optionName.Contains("<") || optionName.Contains(">"))
                {
                    yield return $"The option: '{option}' contains argument syntax, please separate the argument with a whitespace";
                }

                if (optionName.Contains("[") || optionName.Contains("]"))
                {
                    yield return $"The option: '{option}' contains type cast syntax, type cast is only valid at argument";
                }

                if (optionName.Contains("--"))
                {
                    yield return $"The option: '{option}' contains '--' is only allowed at the beginning, to separate verbs use '-'";
                }

                if (char.IsLetterOrDigit(option.Last()).IsFalse())
                {
                    yield return $"The option: '{option}' must ends only with a letter or digit";
                }

                if (char.IsLetter(optionName.First()).IsFalse())
                {
                    yield return $"The option: '{optionName}' must begin with a letter";
                }

                if (optionName.Contains("."))
                {
                    yield return $"The option: '{optionName}' must not contains '.'";
                }

                var validationResult = _primitiveTypeNameValidator.IsPrimitiveTypeName(optionName);
                if (validationResult.IsValid)
                {
                    yield return $"The name of an option does not match a name of a type: '{optionName}'";
                }
            }
        }
    }
}
