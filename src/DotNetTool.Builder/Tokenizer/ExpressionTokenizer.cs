using System.Collections.Generic;

namespace DotNetTool.Builder.Tokenizer
{
    using System.Linq;
    using Extensions;
    using Models;
    using Tokens;

    public class ExpressionTokenizer : IExpressionTokenizer
    {
        private readonly IEnumerable<ITokenizer> _tokenizers;

        public ExpressionTokenizer(IEnumerable<ITokenizer> tokenizers)
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