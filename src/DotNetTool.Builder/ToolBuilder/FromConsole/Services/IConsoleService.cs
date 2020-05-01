using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Services
{
    internal interface IConsoleService
    {
        string ReadLine();

        void WriteInput(string value);
        void WriteSample(string value);
        void WriteError(string value);
        void WriteSuccess(string value);
        void WriteInfo(string value);
        void WriteLine();
    }
}
