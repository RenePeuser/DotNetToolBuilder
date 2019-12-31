namespace DotNetTool.Builder.Tokenizer
{
    using Models;

    public interface IExpressionTokenizer
    {
        ExpressionInfo Tokenize(string expression);
    }
}