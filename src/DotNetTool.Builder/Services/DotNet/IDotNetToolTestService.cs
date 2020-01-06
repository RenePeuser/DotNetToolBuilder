using System.Threading.Tasks;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.DotNet
{
    internal interface IDotNetToolTestService
    {
        Task RunAsync(IFileInfo solutionFile, Models.DotNetTool dotNetTool);
    }
}
