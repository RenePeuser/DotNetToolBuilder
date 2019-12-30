using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Validation;
using DotNetTool.Builder.Validation.Expression;

namespace DotNetTool.Builder.Services
{
    public class ParameterExpressionCollector : IParameterExpressionCollector
    {
        private readonly IConsoleService _consoleService;
        private readonly IParameterExpressionParser _parameterExpressionParser;
        private readonly IExpressionValidator _expressionValidator;

        public ParameterExpressionCollector(IConsoleService consoleService, IParameterExpressionParser parameterExpressionParser, IExpressionValidator expressionValidator)
        {
            _consoleService = consoleService;
            _parameterExpressionParser = parameterExpressionParser;
            _expressionValidator = expressionValidator;
        }

        public ParameterInfo CollectFor(string dotNetToolName)
        {
            ParameterInfo parameter = null;

            while (true)
            {

                string parameterExpression = null;
                ValidationResult validationResult = null;
                while (validationResult.IsNull() || validationResult.IsValid.IsFalse())
                {
                    _consoleService.WriteInput("Please enter your parameter expression");
                    _consoleService.WriteSample("Sample: 'myTool install <package> --global");

                    parameterExpression = _consoleService.ReadLine();
                    validationResult = _expressionValidator.IsValid(dotNetToolName, parameterExpression);
                    if (validationResult.IsValid.IsFalse())
                    {
                        _consoleService.WriteError(validationResult.Errors);
                    }
                }

                var parseResult = _parameterExpressionParser.Parse(parameterExpression, parameter);

                if (parameter.IsNull())
                {
                    parameter = parseResult;
                }

                _consoleService.WriteInput("Do you want to add another parameter expression ? yes(y) or no (n)");

                var result = _consoleService.ReadLine();
                if (result.Contains("no") || result.Contains("n"))
                {
                    return parameter;
                }
            }
        }
    }
}
