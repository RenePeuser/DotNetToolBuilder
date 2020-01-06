namespace DotNetTool.Builder.Test.Validation
{
    public class ExpressionWithExpectedResult
    {
        public ExpressionWithExpectedResult(string toolName, string expression)
        {
            ToolName = toolName;
            Expression = expression;
        }

        public string ToolName { get; }

        public string Expression { get; }
    }
}
