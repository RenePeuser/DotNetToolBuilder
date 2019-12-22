using DotNetTool.Builder.FileSystemAbstraction;

namespace DotNetTool.Builder.Services
{
    public interface IVisualStudioService
    {
        void Open(IFileInfo solution);
    }
}
