using System;
using System.Collections.Generic;
using Argument.Check;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression
{
    internal class CommandMustBeforeOptionOrArgumentValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(expressionInfo.Tokens).Flatten(Environment.NewLine);
            return new ValidationResult(errors);
        }

        private IEnumerable<string> CollectErrors(IEnumerable<Token> expressionTokens)
        {
            Token argumentOrOptionToken = null;
            foreach (var token in expressionTokens)
            {
                if (token.IsNot<CommandToken>())
                {
                    argumentOrOptionToken = token;
                    continue;
                }

                if (token.IsNot<CommandToken>() || argumentOrOptionToken.IsNull())
                {
                    continue;
                }

                switch (argumentOrOptionToken)
                {
                    case ArgumentToken argumentToken:
                        yield return $"The command: '{token.Value}' was defined after an argument: '{argumentToken.Value}'";
                        break;
                    case OptionToken optionToken:
                        yield return $"The command: '{token.Value}' was defined after an option: '{optionToken.Value}'";
                        break;
                }
            }
        }
    }
}
