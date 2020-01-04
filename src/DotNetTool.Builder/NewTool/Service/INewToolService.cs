using System.Threading.Tasks;

namespace DotNetTool.Builder.NewTool.Service
{
    internal interface INewToolService
    {       
        Task<int> HandleAsync(NewToolParameters parameters);
    }
}