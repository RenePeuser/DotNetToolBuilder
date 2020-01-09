using System;
using System.Runtime.CompilerServices;
using DotNetTool.Builder.Validation;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace DotNetTool.Builder.Services
{
    internal interface ICollectTillInputCorrect
    {
        string CollectTillInputIsValid(string messageForUser, Predicate<string> inputValidation, Func<string, string> getErrorMessageForInput);

        string CollectTillInputIsValid(string messageForUser, IInputValidator inputValidator);
    }
}