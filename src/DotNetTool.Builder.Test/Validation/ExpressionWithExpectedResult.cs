namespace DotNetTool.Builder.Test.Validation
{
    public class ExpressionWithExpectedResult
    {
        public ExpressionWithExpectedResult(string toolName, string expression) : this(toolName, expression, string.Empty)
        {
        }

        public ExpressionWithExpectedResult(string toolName, string expression, string expectedMessage)
        {
            ToolName = toolName;
            Expression = expression;
            ExpectedMessage = expectedMessage;
        }

        public string ToolName { get; }
        public string Expression { get; }
        public string ExpectedMessage { get; }
    }
}