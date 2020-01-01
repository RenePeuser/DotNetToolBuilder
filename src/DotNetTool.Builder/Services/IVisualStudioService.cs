using DotNetTool.Builder.FileSystemAbstraction;

namespace DotNetTool.Builder.Services
{
    using System.Threading.Tasks;

    public interface IVisualStudioService
    {
        Task OpenAsync(IFileInfo solution);
    }
}
