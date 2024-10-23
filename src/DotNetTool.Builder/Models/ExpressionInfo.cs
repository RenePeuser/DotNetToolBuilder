using System.Collections.Generic;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;

namespace DotNetTool.Builder.Models
{
    internal sealed class ExpressionInfo(string expression,
                                         string optimizedExpressions,
                                         IEnumerable<Token> tokens)
    {
        public string Expression { get; } = expression;

        public string OptimizedExpressions { get; } = optimizedExpressions;

        public IEnumerable<Token> Tokens { get; } = tokens;
    }
}
