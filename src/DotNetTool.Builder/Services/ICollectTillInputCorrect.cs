using System.Runtime.CompilerServices;
using DotNetTool.Builder.Validation;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace DotNetTool.Builder.Services
{
    internal interface ICollectTillInputCorrect
    {
        string CollectTillInoutIsValid(string messageForUser, params string[] expectedInput);
        string CollectTillInoutIsValid(string messageForUser, IInputValidator inputValidator);
    }
}