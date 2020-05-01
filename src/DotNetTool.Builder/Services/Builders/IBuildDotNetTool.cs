using DotNetTool.Builder.DotNet.Newtool;

namespace DotNetTool.Builder.Services.Builders
{
    internal interface IBuildDotNetTool
    {
        Models.DotNetTool BuildFrom(NewToolParameters newToolParameters);

        bool IsThisBuilderFor(NewToolParameters newToolParameters);
    }
}