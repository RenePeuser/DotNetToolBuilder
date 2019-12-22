using System;

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
            Console.WriteLine(value);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
