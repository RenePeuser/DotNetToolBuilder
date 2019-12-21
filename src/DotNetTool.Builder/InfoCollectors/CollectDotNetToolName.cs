using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.InfoCollectors
{
    public class CollectDotNetToolName : CollectInfoStep, ICollectDotNetToolName
    {
        private static readonly string title =
            "Please enter the name of the DotNetTool: (Sample: 'dotnet')".AsInput();

        public CollectDotNetToolName(IConsoleService consoleService) : base(consoleService, title)
        {
        }
    }
}