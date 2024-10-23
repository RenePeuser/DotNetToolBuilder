using System.Diagnostics;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("ExitCode: '{" + nameof(ExitCode) + "}'")]
    internal sealed class CliRunResult(int exitCode,
                                       string output)
    {
        public int ExitCode { get; } = exitCode;

        public string Output { get; } = output;
    }
}
