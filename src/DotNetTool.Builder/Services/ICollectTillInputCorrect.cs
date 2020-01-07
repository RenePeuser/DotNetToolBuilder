using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace DotNetTool.Builder.Services
{
    internal interface ICollectTillInputCorrect
    {
        string CollectTillUserInputOk(string messageForUser, params string[] expectedInput);
    }
}