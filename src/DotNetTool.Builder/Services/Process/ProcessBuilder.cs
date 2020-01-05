using Argument.Check;

namespace DotNetTool.Builder.Services.Process
{
    internal class ProcessBuilder : IProcessBuilder
    {
        public IProcess BuildFrom(string command, string arguments)
        {
            Throw.IfNullOrWhiteSpace(() => command);
            Throw.IfNullOrWhiteSpace(() => arguments);

            var process = new ProcessProxy(new System.Diagnostics.Process());

            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            return process;
        }
    }
}
