using System;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace DotNetTool.Builder.ErrorHandling
{
    internal interface IErrorHandler
    {
        Task HandleErrors(InvocationContext context, Func<InvocationContext, Task> next);
    }
}