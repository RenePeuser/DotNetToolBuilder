using System.Threading.Tasks;

namespace DotNetTool.Builder.Dotnet.Newtool.Service
{
    internal interface INewToolService
    {
        Task<int> HandleAsync(NewToolParameters parameters);
    }
}
