using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Test.Validation
{
    public class ExpressionWithExpectedResult
    {
        internal ExpressionWithExpectedResult(string dotNetToolName, string expression)
        {
            DotNetToolName = new DotNetToolName(dotNetToolName, dotNetToolName);
            Expression = expression;
        }

        internal DotNetToolName DotNetToolName { get; }

        public string Expression { get; }
    }
}
