namespace rps.template.ErrorHandling
{
    using System;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;

    public interface IErrorHandler
    {
        Task HandleErrors(InvocationContext context, Func<InvocationContext, Task> next);
    }
}