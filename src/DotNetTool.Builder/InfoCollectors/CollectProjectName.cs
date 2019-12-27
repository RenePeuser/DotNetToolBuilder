using DotNetTool.Builder.Services;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.InfoCollectors
{
    public class CollectProjectName : CollectInfoStep, ICollectProjectName
    {
        public CollectProjectName(IConsoleService consoleService, IProjectNameValidator inputValidator) : base(consoleService, inputValidator, "Please enter the name of your project: (Sample: 'My.New.Tool', this is the name of your solution !)")
        {
        }
    }
}
