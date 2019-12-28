namespace DotNetTool.Builder.Validation.Expression
{
    public interface IExpressionContentValidator
    {
        ValidationResult IsValid(string dotNetToolName, string expression);
    }
}