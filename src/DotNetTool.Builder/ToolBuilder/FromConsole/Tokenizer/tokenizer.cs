using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer
{
    internal sealed class Tokenizer(IEnumerable<ITokenizer> tokenizers) : IExpressionTokenizer
    {
        public ExpressionInfo Tokenize(string expression)
        {
            var splittedExpression = expression.Split().FilterNullOrWhitespace().ToList();
            var optimizedExpression = splittedExpression.Flatten(" ");
            var tokens = GetAllTokensFrom(splittedExpression).ToList();
            var firstCommandIsRootCommand = tokens.OfType<CommandToken>().FirstOrDefault();
            if (firstCommandIsRootCommand.IsNotNull())
            {
                tokens[tokens.IndexOf(firstCommandIsRootCommand)] = new RootCommandToken(firstCommandIsRootCommand.Value);
            }

            return new ExpressionInfo(expression, optimizedExpression, tokens);
        }

        private IEnumerable<Token> GetAllTokensFrom(IEnumerable<string> tokens)
        {
            foreach (var token in tokens)
            {
                var tokenizers1 = tokenizers.Where(tokenizer => tokenizer.IsThisTokenizerFor(token)).ToList();
                if (tokenizers1.Any())
                {
                    foreach (var tokenizer in tokenizers1)
                    {
                        yield return tokenizer.GetToken(token);
                    }

                    continue;
                }

                yield return new UnknownToken(token);
            }
        }
    }
}
