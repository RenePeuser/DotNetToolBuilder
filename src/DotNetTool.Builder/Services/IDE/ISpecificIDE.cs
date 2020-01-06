using System.Threading.Tasks;
using DotNetTool.Builder.Dotnet.Newtool;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IDE
{
    internal interface ISpecificIDE
    {
        public Task OpenAsync(IFileInfo solutionFileInfo, NewToolParameters parameters);
    }
}
