using System;
using System.Threading.Tasks;
using DotNetTool.Builder.Models;
using Trumpf.Hmi.ArgumentChecking;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Services
{
    internal class ProcessService : IProcessService
    {
        private readonly IProcessBuilder _processBuilder;

        public ProcessService(IProcessBuilder processBuilder)
        {
            Throw.IfNull(() => processBuilder);

            _processBuilder = processBuilder;
        }

        public Task<CliRunResult> RunCliCommandAsync(string command, string arguments)
        {
            var process = _processBuilder.BuildFrom(command, arguments);
            var tcs = new TaskCompletionSource<CliRunResult>();
            process.EnableRaisingEvents = true;
            process.Exited += (_, __) =>
            {
                tcs.TrySetResult(new CliRunResult(process.ExitCode, process.StandardOutput.ReadToEnd()));
            };

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start().IfFalseThen(() =>
                tcs.SetException(new Exception($"Failed to start cli command: {command} {arguments}")));
            return tcs.Task;
        }
    }
}