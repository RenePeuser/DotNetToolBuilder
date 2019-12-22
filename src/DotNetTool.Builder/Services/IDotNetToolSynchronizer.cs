using System.Threading.Tasks;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    public interface IDotNetToolSynchronizer
    {
        Task SynchronizeTool(DotNetToolInfo dotNetToolInfo);
    }
}