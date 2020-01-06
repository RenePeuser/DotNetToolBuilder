using System.Collections.Generic;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Models
{
    internal class ExpressionInfo
    {
        public ExpressionInfo(string expression, string optimizedExpressions, IEnumerable<Token> tokens)
        {
            Expression = expression;
            OptimizedExpressions = optimizedExpressions;
            Tokens = tokens;
        }

        public string Expression { get; }

        public string OptimizedExpressions { get; }

        public IEnumerable<Token> Tokens { get; }
    }
}
