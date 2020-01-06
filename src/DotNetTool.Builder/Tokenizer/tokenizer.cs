using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Tokenizer
{
    internal class tokenizer : IExpressionTokenizer
    {
        private readonly IEnumerable<ITokenizer> _tokenizers;

        public tokenizer(IEnumerable<ITokenizer> tokenizers)
        {
            _tokenizers = tokenizers;
        }

        public ExpressionInfo Tokenize(string expression)
        {
            var splitted = expression.Split().FilterNullOrWhitespace();
            var optimizedExpression = splitted.Flatten(" ");
            var tokens = GetAllTokensFrom(splitted).ToList();
            return new ExpressionInfo(expression, optimizedExpression, tokens);
        }

        private IEnumerable<Token> GetAllTokensFrom(IEnumerable<string> tokens)
        {
            foreach (var token in tokens)
            {
                var tokenizers = _tokenizers.Where(tokenizer => tokenizer.IsThisTokenizerFor(token));
                if (tokenizers.Any())
                {
                    foreach (var tokenizer in tokenizers)
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
