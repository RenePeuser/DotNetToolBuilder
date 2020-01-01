using System;
using System.Threading.Tasks;
using Argument.Check;

using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    internal class ProcessService : IProcessService
    {
        private readonly IProcessBuilder _processBuilder;
        private readonly IConsoleService _consoleService;

        public ProcessService(IProcessBuilder processBuilder, IConsoleService consoleService)
        {
            Throw.IfNull(() => processBuilder);
            Throw.IfNull(() => consoleService);

            _processBuilder = processBuilder;
            _consoleService = consoleService;
        }

        public Task<CliRunResult> RunCliCommandAsync(string command, string arguments)
        {
            _consoleService.WriteInfo($"{command} {arguments}");

            var process = _processBuilder.BuildFrom(command, arguments);
            var tcs = new TaskCompletionSource<CliRunResult>();
            process.EnableRaisingEvents = true;
            process.Exited += (_, __) =>
            {
                var readToEnd = process.StandardOutput.ReadToEnd();
                process.ExitCode.IsEqualTo(0)
                                .IfTrueThen(() => _consoleService.WriteSuccess(readToEnd))
                                .IfFalseThen(() => _consoleService.WriteError(readToEnd));

                tcs.TrySetResult(new CliRunResult(process.ExitCode, readToEnd));
            };

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start().IfFalseThen(() => tcs.SetException(new Exception($"Failed to start cli command: {command} {arguments}")));
            return tcs.Task;
        }

        public Task<CliRunResult> StartCliCommandAsync(string command, string arguments)
        {
            _consoleService.WriteInfo($"{command} {arguments}");

            var process = _processBuilder.BuildFrom(command, arguments);
            var tcs = new TaskCompletionSource<CliRunResult>();
            process.EnableRaisingEvents = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            var start = process.Start();
            if (start)
            {
                var cliRunResult = new CliRunResult(0, $"Program: '{command}' successfully started");
                _consoleService.WriteSuccess(cliRunResult.Output);
                tcs.SetResult(cliRunResult);
            }
            else
            {
                tcs.SetException(new Exception($"Failed to start cli command: {command} {arguments}"));
            }
            return tcs.Task;
        }
    }
}
