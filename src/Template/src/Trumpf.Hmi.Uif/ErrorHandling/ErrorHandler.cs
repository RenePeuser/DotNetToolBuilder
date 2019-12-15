namespace Trumpf.Hmi.Uif.ErrorHandling
{
    using System;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;
    using Trumpf.Hmi.Extensions;
    using Trumpf.Hmi.Uif.Rendering;

    public class ErrorHandler : IErrorHandler
    {
        public async Task HandleErrors(InvocationContext context, Func<InvocationContext, Task> next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                var ex = FindMostSuitableException(e);

                if (ex.Is<UifException>())
                {
                    Console.Error.WriteLine(ex.Message.AsError());
                }
                else
                {
                    Console.Error.WriteLine("An unhandled Error occurred:".AsError());
                    Console.Error.WriteLine();
                    Console.Error.WriteLine(ex.ToString().AsError());
                }

                context.ResultCode = 1;
            }
        }

        private static Exception FindMostSuitableException(Exception exception)
        {
            if (exception.Is<UifException>())
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