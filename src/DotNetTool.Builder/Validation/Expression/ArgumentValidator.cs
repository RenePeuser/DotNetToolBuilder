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

        // Valid argument declarations
        // <arg>
        // <arg>[string]
        // [string]<arg>
        private IEnumerable<string> CheckForErrors(ExpressionInfo expressionInfo)
        {
            var argumentTokens = expressionInfo.Tokens.OfType<ArgumentToken>().ToList();
            foreach (var argumentToken in argumentTokens)
            {
                if (argumentToken.Value.Count(c => c == '<' || c == '>').NotEqualsTo(2))
                {
                    yield return $"Argument: '{argumentToken}' contains another argument syntax. Multiple '<' or '>' are not valid";
                    continue;
                }

                if (argumentToken.Value.IndexOf('<') > argumentToken.Value.IndexOf('>'))
                {
                    yield return $"Argument: '{argumentToken}' must starts with '< and ends with '>'";
                    continue;
                }

                var start = argumentToken.Value.IndexOf("<") + 1;
                var end = argumentToken.Value.IndexOf(">");
                var argumentName = argumentToken.Value.Substring(start, end - start);
                if (argumentName.IsNullOrWhiteSpace())
                {
                    yield return $"Missing argument name: {argumentToken}";
                    yield break;
                }

                if (char.IsLetter(argumentName.First()).IsFalse())
                {
                    yield return $"Argument: {argumentName} must begin with a letter.";
                }

                var validationResult = _primitiveTypeNameValidator.IsValid(argumentName);
                if (validationResult.IsValid.IsFalse())
                {
                    yield return $"The name of an argument does not match a name of a type: {argumentName}";
                }

                if (argumentToken.Value.Contains("--"))
                {
                    yield return $"Argument '{argumentToken}' contains '--' is only allowed for options, to separate verbs use '-'";
                }

                var preCast = argumentToken.Value.Split('<').First();
                if (preCast.IsNotNullOrEmpty())
                {
                    if (preCast.StartsWith("[").IsFalse() || preCast.EndsWith("]").IsFalse())
                    {
                        yield return $"Argument: {argumentToken} is invalid, only a type cast can be attached to an argument. Sample: [string]<arg>";
                    }
                }

                var postCast = argumentToken.Value.Split('>').Last();
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