using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.InfoCollectors
{
    public class CollectDotNetToolName : CollectInfoStep, ICollectDotNetToolName
    {
        public CollectDotNetToolName(IConsoleService consoleService) : base(consoleService, "Please enter the name of the DotNetTool: (Sample: 'dotnet')".AsInput())
        {
        }
    }
}
