using System;
using System.CommandLine.Invocation;
using System.Text;
using System.Threading.Tasks;
using Argument.Check;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    internal class ProcessService : IProcessService
    {
        private readonly IConsoleService _consoleService;

        public ProcessService(IConsoleService consoleService)
        {
            Throw.IfNull(() => consoleService);

            _consoleService = consoleService;
        }

        public async Task<CliRunResult> RunAsync(string command, string arguments)
        {
            _consoleService.WriteInfo($"{command} {arguments}");

            var stringBuilder = new StringBuilder();
            var result = await Process.ExecuteAsync(command, arguments, null, s => stringBuilder.AppendLine(s));
            var output = stringBuilder.ToString();
            if (result.NotEqualsTo(0))
            {
                _consoleService.WriteError(output);
            }
            else
            {
                _consoleService.WriteSuccess(output);
            }

            return new CliRunResult(result, output);
        }

        public Task<CliRunResult> StartAsync(string command, string arguments)
        {
            _consoleService.WriteInfo($"{command} {arguments}");

            var tcs = new TaskCompletionSource<CliRunResult>();
            var stringBuilder = new StringBuilder();
            var process = Process.StartProcess(command, arguments, null, s => stringBuilder.AppendLine(s));
            var start = process.Start();
            if (start)
            {
                var cliRunResult = new CliRunResult(0, $"Program: '{command}' successfully started");
                _consoleService.WriteSuccess(cliRunResult.Output);
                tcs.SetResult(cliRunResult);
            }
            else
            {
                tcs.SetException(new Exception($"Failed to start cli command: '{command} {arguments}'"));
            }

            return tcs.Task;
        }
    }
}
