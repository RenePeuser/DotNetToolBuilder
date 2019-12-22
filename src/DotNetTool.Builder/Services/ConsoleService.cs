using System;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Services
{
    public class ConsoleService : IConsoleService
    {
        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string value)
        {
            Console.WriteLine();
            Console.WriteLine(value);
        }

        public void WriteInfo(string value)
        {
            WriteLine(value);
        }

        public void WriteInput(string value)
        {
            WriteLine(value.AsInput());
        }

        public void WriteSuccess(string value)
        {
            WriteLine(value.AsSuccessfull());
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteSample(string value)
        {
            WriteLine(value.AsSample());
        }

        public void WriteError(string value)
        {
            WriteLine(value.AsError());
        }
    }
}
