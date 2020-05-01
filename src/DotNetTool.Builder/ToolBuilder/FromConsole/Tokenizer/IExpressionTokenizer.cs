using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer
{
    internal interface IExpressionTokenizer
    {
        ExpressionInfo Tokenize(string expression);
    }
}
