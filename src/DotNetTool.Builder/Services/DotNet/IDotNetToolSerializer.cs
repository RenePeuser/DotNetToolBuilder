using DotNetTool.Builder.Dotnet.Newtool;
using FileSystem.Abstraction;
using FileInfo = System.IO.FileInfo;

namespace DotNetTool.Builder.Services.DotNet
{
    internal interface IDotNetToolSerializer
    {
        Models.DotNetTool DeserializeFrom(FileInfo fileInfo);
        void Serialize(Models.DotNetTool dotNetTool, NewToolParameters target);
    }
}