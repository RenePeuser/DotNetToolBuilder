using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.InfoCollectors
{
    public class CollectProjectName : CollectInfoStep, ICollectProjectName
    {
        public CollectProjectName(IConsoleService consoleService) : base(consoleService,
            "Please enter the name of your project: (Sample: Trumpf.Hmi.New.Submarine)".AsInput())
        {
        }
    }
}