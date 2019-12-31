namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Models;
    using Tokenizer.Tokens;

    public class OnlyOneArgumentValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo)
        {
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
                    yield return $"Argument: {token.Value}' was defined after another argument: {lastArgumentToken.Value}.{Environment.NewLine}You can define an argument only after a command 'myCommand <arg>' or an option '--option <opt-arg>' ";
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