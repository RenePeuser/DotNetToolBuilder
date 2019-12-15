using System.Threading.Tasks;

namespace trumpf.hmi.dotnettool.builder.Services
{
    public interface IProcessService
    {
        Task<CliRunResult> RunCliCommandAsync(string command, string arguments);
    }
}