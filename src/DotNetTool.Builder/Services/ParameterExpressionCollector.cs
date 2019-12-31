using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Validation;
using DotNetTool.Builder.Validation.Expression;

namespace DotNetTool.Builder.Services
{
    using Tokenizer;

    public class ParameterExpressionCollector : IParameterExpressionCollector
    {
        private readonly IConsoleService _consoleService;
        private readonly IParameterExpressionParser _parameterExpressionParser;
        private readonly IExpressionValidator _expressionValidator;
        private readonly IExpressionTokenizer _expressionTokenizer;

        public ParameterExpressionCollector(IConsoleService consoleService, IParameterExpressionParser parameterExpressionParser, IExpressionValidator expressionValidator, IExpressionTokenizer expressionTokenizer)
        {
            _consoleService = consoleService;
            _parameterExpressionParser = parameterExpressionParser;
            _expressionValidator = expressionValidator;
            _expressionTokenizer = expressionTokenizer;
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

                    var expressionInfo = _expressionTokenizer.Tokenize(parameterExpression);
                    validationResult = _expressionValidator.IsValid(dotNetToolName, expressionInfo);
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
