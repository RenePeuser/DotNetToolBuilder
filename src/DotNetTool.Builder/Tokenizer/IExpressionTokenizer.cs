using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Tokenizer
{
    internal interface IExpressionTokenizer
    {
        ExpressionInfo Tokenize(string expression);
    }
}
