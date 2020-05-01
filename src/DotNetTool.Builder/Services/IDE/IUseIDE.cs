using System.Threading.Tasks;
using DotNetTool.Builder.DotNet.Newtool;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IDE
{
    internal interface IUseIDE
    {
        public Task OpenAsync(IFileInfo solutionFileInfo, NewToolParameters parameters);
    }
}
