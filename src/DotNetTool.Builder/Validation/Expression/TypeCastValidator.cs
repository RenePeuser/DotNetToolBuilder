using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using Argument.Check;
    using Models;
    using Tokenizer.Tokens;

    internal class TypeCastValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNullOrWhiteSpace(() => dotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(expressionInfo).ToList();
            return new ValidationResult(errors.IsEmpty(), errors.Flatten(Environment.NewLine));
        }

        private IEnumerable<string> CollectErrors(ExpressionInfo expressionInfo)
        {
            var argumentTokens = expressionInfo.Tokens.OfType<ArgumentToken>();
            foreach (var argumentToken in argumentTokens)
            {
                var value = argumentToken.Value;
                if (value.Contains("[") || value.Contains("]"))
                {
                    if (value.Count(c => c == '[' || c == ']') != 2 || value.IndexOf('[') > value.IndexOf(']'))
                    {
                        yield return $"Typecast: '{value}' must begin with '[' and ends with ']'";
                        continue;
                    }

                    if (value.StartsWith("[") && value.EndsWith("]"))
                    {
                        yield return $"Typecast: '{value}' must be close to an argument. Sample: <myArg>[string] or [string]<myArg>";
                        continue;
                    }

                    var start = value.IndexOf("[", StringComparison.Ordinal) + 1;
                    var end = value.IndexOf("]", StringComparison.Ordinal);
                    var typeName = value[start..end];

                    if (typeName.IsNullOrWhiteSpace())
                    {
                        yield return $"Typecast: '{typeName}' must only contains letter, digits or '.'. Sample: '[string]' or '[System.IO.FileInfo]'";
                        yield break;
                    }

                    if (typeName.All(c => char.IsLetterOrDigit(c) || c == '.').IsFalse())
                    {
                        yield return $"Typecast: '{typeName}' must only contains letter, digits or '.'";
                        continue;
                    }
                    if (char.IsLetter(typeName.First()).IsFalse())
                    {
                        yield return $"Typecast: '{typeName}' must begin with a letter";
                    }
                }
            }
        }
    }
}