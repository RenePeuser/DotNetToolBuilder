using System;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;

namespace DotNetTool.Builder.ErrorHandling
{
    internal sealed class ErrorHandler(IConsoleService consoleService) : IErrorHandler
    {
        public async Task HandleErrors(InvocationContext context, Func<InvocationContext, Task> next)
        {
            try
            {
                await next(context).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var ex = FindMostSuitableException(e);

                if (ex is DotNetToolBuilderException)
                {
                    consoleService.WriteError(ex.Message);
                }
                else
                {
                    consoleService.WriteError("An unhandled Error occurred:");
                    consoleService.WriteLine();
                    consoleService.WriteError(ex.ToString());
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
