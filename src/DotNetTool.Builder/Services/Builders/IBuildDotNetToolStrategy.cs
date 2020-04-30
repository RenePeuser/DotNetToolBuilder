using DotNetTool.Builder.Dotnet.Newtool;

namespace DotNetTool.Builder.Services.Builders
{
    internal interface IBuildDotNetToolStrategy
    {
        Models.DotNetTool BuildFrom(NewToolParameters newToolParameters);

        bool IsThisBuilderFor(NewToolParameters newToolParameters);
    }
}