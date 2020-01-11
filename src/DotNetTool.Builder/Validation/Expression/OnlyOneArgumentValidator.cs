using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Validation.Expression
{
    internal class OnlyOneArgumentValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(expressionInfo).ToList();
            return new ValidationResult(errors.IsEmpty(), errors.Flatten(Environment.NewLine));
        }

        private IEnumerable<string> CollectErrors(ExpressionInfo expressionInfo)
        {
            var tokens = expressionInfo.Tokens;
            ArgumentToken lastArgumentToken = null;

            foreach (var token in tokens)
            {
                if (lastArgumentToken.IsNotNull() && token.Is<ArgumentToken>())
                {
                    yield return $"The argument: {token.Value}' was defined after another argument: {lastArgumentToken.Value}.{Environment.NewLine}You can define an argument only after a command 'myCommand <arg>' or an option '--option <opt-arg>' ";
                }

                if (token is ArgumentToken argumentToken)
                {
                    lastArgumentToken = argumentToken;
                }
                else
                {
                    lastArgumentToken = null;
                }
            }
        }
    }
}
