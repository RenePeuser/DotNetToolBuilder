using System.IO;
using Trumpf.Hmi.FileSystemAbstraction.FileSystem;

namespace DotNetTool.Builder.Services
{
    public interface IVisualStudioService
    {
        void Open(TiFileInfo solution);
    }
}