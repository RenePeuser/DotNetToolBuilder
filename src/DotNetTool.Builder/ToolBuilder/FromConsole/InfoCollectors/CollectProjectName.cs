using DotNetTool.Builder.Services.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal sealed class CollectProjectName(IProjectNameValidator inputValidator,
                                             ICollectTillInputCorrect collectTillInputCorrect) : ICollectProjectName
    {
        private const string Title = "Please enter the name of your project: (Sample: 'My.New.Tool', this is the name of your solution !)";

        public string Collect()
        {
            var projectName = collectTillInputCorrect.CollectTillInputIsValid(Title, inputValidator);
            return projectName;
        }
    }
}
