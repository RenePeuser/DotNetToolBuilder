namespace DotNetTool.Builder.Services
{
    using System.Threading.Tasks;
    using FileSystemAbstraction;
    using Models;

    public interface IDotNetToolTestService
    {
        Task RunAsync(IFileInfo solutionFile, DotNetTool dotNetTool);
    }
}