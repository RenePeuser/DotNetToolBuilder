using System;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.ErrorHandling
{
    public class ErrorHandler : IErrorHandler
    {
        private readonly IConsoleService _consoleService;

        public ErrorHandler(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public async Task HandleErrors(InvocationContext context, Func<InvocationContext, Task> next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                var ex = FindMostSuitableException(e);

                if (ex is DotNetToolBuilderException)
                {
                    _consoleService.WriteError(ex.Message);
                }
                else
                {
                    _consoleService.WriteError("An unhandled Error occurred:");
                    _consoleService.WriteLine();
                    _consoleService.WriteError(ex.ToString());
                }

                context.ResultCode = 1;
            }
        }

        private static Exception FindMostSuitableException(Exception exception)
        {
            if (exception is DotNetToolBuilderException)
            {
                return exception;
            }

            if (exception.InnerException != null)
            {
                return FindMostSuitableException(exception.InnerException);
            }

            return exception;
        }
    }

}