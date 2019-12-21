using System.Threading.Tasks;

namespace DotNetTool.Builder.Services
{
    public interface IDotNetToolSynchronizer
    {
        Task SynchronizeTool(DotNetToolInfo dotNetToolInfo);
    }
}