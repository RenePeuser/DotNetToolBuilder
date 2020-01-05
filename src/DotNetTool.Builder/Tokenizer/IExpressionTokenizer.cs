namespace DotNetTool.Builder.Tokenizer
{
    using Models;

    internal interface IExpressionTokenizer
    {
        ExpressionInfo Tokenize(string expression);
    }
}