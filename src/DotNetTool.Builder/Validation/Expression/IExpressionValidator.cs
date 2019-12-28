namespace DotNetTool.Builder.Validation.Expression
{
    public interface IExpressionValidator
    {
        ValidationResult IsValid(string dotNetToolName, string expression);
    }
}