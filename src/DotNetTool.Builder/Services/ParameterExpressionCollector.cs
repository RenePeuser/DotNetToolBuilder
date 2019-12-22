using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Parser;

namespace DotNetTool.Builder.Services
{
    public class ParameterExpressionCollector : IParameterExpressionCollector
    {
        private readonly IConsoleService _consoleService;
        private readonly IParameterExpressionParser _parameterExpressionParser;

        public ParameterExpressionCollector(IConsoleService consoleService,
            IParameterExpressionParser parameterExpressionParser)
        {
            _consoleService = consoleService;
            _parameterExpressionParser = parameterExpressionParser;
        }

        public ParameterInfo Collect()
        {
            ParameterInfo parameter = null;

            while (true)
            {
                _consoleService.WriteLine("Please enter your parameter expression".AsInput());
                _consoleService.WriteLine(
                    "Sample: 'dotnet tool install --global <package>  [--version not needed is a default command]')"
                        .AsSample());

                var parameterExpression = _consoleService.ReadLine();
                var parseResult = _parameterExpressionParser.Parse(parameterExpression, parameter);

                if (parameter.IsNull())
                {
                    parameter = parseResult;
                }

                _consoleService.WriteLine();

                _consoleService.WriteLine(
                    "Do you want to add another parameter expression ? yes(y) or no (n)".AsInput());

                var result = _consoleService.ReadLine();
                if (result.Contains("no") || result.Contains("n"))
                {
                    return parameter;
                }
            }
        }
    }
}
