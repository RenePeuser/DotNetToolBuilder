using System.Threading.Tasks;

namespace DotNetTool.Builder.DotNet.Newtool.Service
{
    internal interface INewToolService
    {
        Task<int> HandleAsync(NewToolParameters parameters);
    }
}
