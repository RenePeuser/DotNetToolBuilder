using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Tokenizer;
using DotNetTool.Builder.Validation;
using DotNetTool.Builder.Validation.Expression;

namespace DotNetTool.Builder.Services.Collectors
{
    internal class ParameterExpressionCollector : IParameterExpressionCollector
    {
        private readonly IConsoleService _consoleService;
        private readonly IExpressionTokenizer _expressionTokenizer;
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;
        private readonly IExpressionValidator _expressionValidator;
        private readonly IParameterExpressionParser _parameterExpressionParser;

        public ParameterExpressionCollector(
            IConsoleService consoleService, 
            IParameterExpressionParser parameterExpressionParser, 
            IExpressionValidator expressionValidator, 
            IExpressionTokenizer expressionTokenizer,
            ICollectTillInputCorrect collectTillInputCorrect)
        {
            _consoleService = consoleService;
            _parameterExpressionParser = parameterExpressionParser;
            _expressionValidator = expressionValidator;
            _expressionTokenizer = expressionTokenizer;
            _collectTillInputCorrect = collectTillInputCorrect;
        }

        public CommandInfo CollectFor(string dotNetToolName)
        {
            CommandInfo parameter = null;
            while (true)
            {
                ExpressionInfo expressionInfo = null;
                ValidationResult validationResult = null;
                while (validationResult.IsNull() || validationResult.IsValid.IsFalse())
                {
                    _consoleService.WriteInput("Please enter your parameter expression");
                    _consoleService.WriteSample($"Sample: '{dotNetToolName} command <argument> --option");

                    var parameterExpression = _consoleService.ReadLine();
                    expressionInfo = _expressionTokenizer.Tokenize(parameterExpression);
                    validationResult = _expressionValidator.IsValid(dotNetToolName, expressionInfo);
                    if (validationResult.IsValid.IsFalse())
                    {
                        _consoleService.WriteError(validationResult.Errors);
                    }
                }

                parameter = _parameterExpressionParser.Parse(expressionInfo, parameter);

                var required = _collectTillInputCorrect.CollectTillUserInputOk("Do you want to add another parameter expression ? yes(y) or no (n)", "yes", "y", "no", "n");
                if (required.EqualsAnyOf("n", "no"))
                {
                    return parameter;
                }
            }
        }
    }
}
