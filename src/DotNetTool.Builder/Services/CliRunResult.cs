namespace DotNetTool.Builder.Services
{
    public class CliRunResult
    {
        public CliRunResult(int exitCode, string output)
        {
            ExitCode = exitCode;
            Output = output;
        }

        public int  ExitCode { get; }
        public string Output { get; }
    }
}