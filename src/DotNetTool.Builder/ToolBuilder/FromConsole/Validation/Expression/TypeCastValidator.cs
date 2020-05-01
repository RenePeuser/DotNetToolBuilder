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
    internal class TypeCastValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(expressionInfo).ToList();
            return new ValidationResult(errors.Flatten(Environment.NewLine));
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
                        yield return $"The typecast: '{value}' must begin with '[' and ends with ']'";
                        continue;
                    }

                    if (value.StartsWith("[") && value.EndsWith("]"))
                    {
                        yield return $"The typecast: '{value}' must be close to an argument. Sample: <myArg>[string] or [string]<myArg>";
                        continue;
                    }

                    var start = value.IndexOf("[", StringComparison.Ordinal) + 1;
                    var end = value.IndexOf("]", StringComparison.Ordinal);
                    var typeName = value[start..end];
                    if (typeName.IsNullOrWhiteSpace())
                    {
                        yield return $"The typecast: '{typeName}' must only contains letter, digits or '.'. Sample: '[string]' or '[System.IO.FileInfo]'";
                        yield break;
                    }

                    if (typeName.All(c => char.IsLetterOrDigit(c) || c == '.').IsFalse())
                    {
                        yield return $"The typecast: '{typeName}' must only contains letter, digits or '.'";
                        continue;
                    }

                    if (char.IsLetter(typeName.First()).IsFalse())
                    {
                        yield return $"The typecast: '{typeName}' must begin with a letter";
                    }
                }
            }
        }
    }
}
