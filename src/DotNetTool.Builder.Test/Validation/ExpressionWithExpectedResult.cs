using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Test.Validation
{
    public class ExpressionWithExpectedResult
    {
        internal ExpressionWithExpectedResult(DotNetToolName dotNetToolName, string expression)
        {
            DotNetToolName = dotNetToolName;
            Expression = expression;
        }

        internal DotNetToolName DotNetToolName { get; }

        public string Expression { get; }
    }
}
