using System.Threading.Tasks;

namespace trumpf.hmi.dotnettool.builder.Services
{
    public interface IDotNetToolSynchronizer
    {
        Task SynchronizeTool(DotNetToolInfo dotNetToolInfo);
    }
}