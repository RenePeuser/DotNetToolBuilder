using System.IO;
using DotNetTool.Builder.DotNet.Newtool;

namespace DotNetTool.Builder.Services.DotNet
{
    internal interface IDotNetToolSerializer
    {
        Models.DotNetTool DeserializeFrom(FileInfo fileInfo);
        void Serialize(Models.DotNetTool dotNetTool, NewToolParameters target);
    }
}
