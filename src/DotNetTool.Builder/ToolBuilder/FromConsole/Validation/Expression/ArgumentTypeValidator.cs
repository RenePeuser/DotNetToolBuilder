using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.Services.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression
{
    internal class ArgumentTypeValidator : IExpressionContentValidator
    {
        private readonly IArgumentTypeOptimizer _argumentTypeOptimizer;
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ArgumentTypeValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator, IArgumentTypeOptimizer argumentTypeOptimizer)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
            _argumentTypeOptimizer = argumentTypeOptimizer;
        }

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
                var start = value.IndexOf("[", StringComparison.Ordinal) + 1;
                var end = value.IndexOf("]", StringComparison.Ordinal);
                if (start < 0 || end < 0)
                {
                    continue;
                }

                var typeName = value[start..end];
                if (typeName.IsNullOrWhiteSpace())
                {
                    continue;
                }

                var optimizedTypeName = _argumentTypeOptimizer.OptimizeType(typeName);
                var isValidTypeNameResult = _primitiveTypeNameValidator.IsTypeName(optimizedTypeName);
                if (isValidTypeNameResult.IsValid.IsFalse())
                {
                    yield return isValidTypeNameResult.Errors;
                }
            }
        }
    }
}
