using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Tokenizer.Tokens;

    public class ArgumentValidator : IExpressionContentValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ArgumentValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        public ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo)
        {
            var errors = CheckForErrors(expressionInfo).ToList();
            return new ValidationResult(errors.IsEmpty(), errors.Flatten(Environment.NewLine));
        }

        private IEnumerable<string> CheckForErrors(ExpressionInfo expressionInfo)
        {
            var argumentTokens = expressionInfo.Tokens.OfType<ArgumentToken>().ToList();
            foreach (var argumentToken in argumentTokens)
            {
                var argument = argumentToken.Value;
                if (argument.Count(c => c == '<' || c == '>').NotEqualsTo(2))
                {
                    yield return $"Argument: '{argument}' contains another argument syntax. Multiple '<' or '>' are not valid";
                    continue;
                }

                if (argument.IndexOf('<') > argument.IndexOf('>'))
                {
                    yield return $"Argument: '{argument}' must starts with '< and ends with '>'";
                    continue;
                }

                var start = argument.IndexOf("<", StringComparison.Ordinal) + 1;
                var end = argument.IndexOf(">", StringComparison.Ordinal);
                var argumentName = argument[start..end];
                if (argumentName.IsNullOrWhiteSpace())
                {
                    yield return $"Missing argument name: {argument}";
                    yield break;
                }

                if (char.IsLetter(argumentName.First()).IsFalse())
                {
                    yield return $"Argument: {argument} must begin with a letter.";
                }

                var validationResult = _primitiveTypeNameValidator.IsValid(argumentName);
                if (validationResult.IsValid.IsFalse())
                {
                    yield return $"The name of an argument does not match a name of a type: {argument}";
                }

                if (argument.Contains("--"))
                {
                    yield return $"Argument '{argumentToken}' contains '--' is only allowed for options, to separate verbs use '-'";
                }

                var preCast = argument.Split('<').First();
                if (preCast.IsNotNullOrEmpty())
                {
                    if (preCast.StartsWith("[").IsFalse() || preCast.EndsWith("]").IsFalse())
                    {
                        yield return $"Argument: {argumentToken} is invalid, only a type cast can be attached to an argument. Sample: [string]<arg>";
                    }
                }

                var postCast = argument.Split('>').Last();
                if (postCast.IsNotNullOrEmpty())
                {
                    if (postCast.StartsWith("[").IsFalse() || postCast.EndsWith("]").IsFalse())
                    {
                        yield return $"Argument: {argumentToken} is invalid, only a type cast can be attached to an argument. Sample: <arg>[string]";
                    }
                }
            }
        }
    }
}