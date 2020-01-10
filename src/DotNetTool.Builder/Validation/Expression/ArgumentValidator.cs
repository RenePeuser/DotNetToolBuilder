using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Validation.Expression
{
    internal class ArgumentValidator : IExpressionContentValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ArgumentValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CheckForErrors(expressionInfo).ToList();
            return new ValidationResult(errors.IsEmpty(), errors.Flatten(Environment.NewLine));
        }

        private IEnumerable<string> CheckForErrors(ExpressionInfo expressionInfo)
        {
            Throw.IfNull(() => expressionInfo);

            var argumentTokens = expressionInfo.Tokens.OfType<ArgumentToken>().ToList();
            foreach (var argumentToken in argumentTokens)
            {
                var argument = argumentToken.Value;

                if (argument.StartsWith("[") && argument.EndsWith("]"))
                {
                    yield return $"Argument: '{argument}' definition is missing, a type cast must close to an argument. Sample: '<arg>[int]' or [int]<arg>";
                    continue;
                }

                var argumentStartEndTokenCount = argument.Count(c => c == '<' || c == '>');
                if (argumentStartEndTokenCount < 1)
                {
                    yield return $"Argument: '{argument}' missing start token '<' and end token'>'.";
                    continue;
                }

                if (argumentStartEndTokenCount == 1)
                {
                    if (argument.Contains("<"))
                    {
                        yield return $"Argument: '{argument}' missing end '>' token.";
                        continue;
                    }

                    yield return $"Argument: '{argument}' missing start '<' token.";
                    continue;
                }

                if (argumentStartEndTokenCount.NotEqualsTo(2))
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
                    yield return $"Argument: {argument} must begin with a letter";
                }

                var validationResult = _primitiveTypeNameValidator.IsValid(argumentName);
                if (validationResult.IsValid.IsFalse())
                {
                    yield return $"The name of an argument does not match a name of a type: {argument}";
                }

                if (argument.Contains("--"))
                {
                    yield return $"Argument '{argument}' contains '--' is only allowed for options, to separate verbs use '-'";
                }

                var preCast = argument.Split('<').First();
                if (preCast.IsNotNullOrEmpty())
                {
                    if (preCast.StartsWith("[").IsFalse())
                    {
                        yield return $"Typecast: {argument} must starts with a '['. Sample: [string]<arg>";
                    }

                    if (preCast.EndsWith("]").IsFalse())
                    {
                        yield return $"Typecast: {argument} must ends with a ']'. Sample: [string]<arg>";
                    }
                }

                var postCast = argument.Split('>').Last();
                if (postCast.IsNotNullOrEmpty())
                {
                    if (postCast.StartsWith("[").IsFalse())
                    {
                        yield return $"Typecast: {argument} must starts with a '['. Sample: <arg>[string]";
                    }

                    if (postCast.EndsWith("]").IsFalse())
                    {
                        yield return $"Typecast: {argument} must ends with a ']'. Sample: <arg>[string]";
                    }
                }
            }
        }
    }
}
