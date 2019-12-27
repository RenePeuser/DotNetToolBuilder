using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.InfoCollectors
{
    public class CollectDotNetToolName : CollectInfoStep, ICollectDotNetToolName
    {
        public CollectDotNetToolName(IConsoleService consoleService, IToolNameValidator inputValidator) : base(consoleService, inputValidator, "Please enter the name of the DotNetTool: (Sample: 'dotnet')")
        {
        }
    }
}
