using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Parser;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer;
using DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal class ParameterExpressionCollector : IParameterExpressionCollector
    {
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;
        private readonly IConsoleService _consoleService;
        private readonly IExpressionTokenizer _expressionTokenizer;
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

        public CommandInfo CollectFor(DotNetToolName dotNetDotNetToolName, string projectName)
        {
            CommandInfo parameter = null;
            while (true)
            {
                ExpressionInfo expressionInfo = null;
                ValidationResult validationResult = null;
                while (validationResult.IsNull() || validationResult.IsValid.IsFalse())
                {
                    _consoleService.WriteInput("Please enter your parameter expression");
                    _consoleService.WriteSample($"Sample: '{dotNetDotNetToolName.Name} command <argument> --option");

                    var parameterExpression = _consoleService.ReadLine();
                    expressionInfo = _expressionTokenizer.Tokenize(parameterExpression);
                    validationResult = _expressionValidator.IsValid(dotNetDotNetToolName, expressionInfo, projectName);
                    if (validationResult.IsValid.IsFalse())
                    {
                        _consoleService.WriteError(validationResult.Errors);
                    }
                }

                parameter = _parameterExpressionParser.Parse(expressionInfo, parameter);

                var required = _collectTillInputCorrect.CollectTillInputIsValid("Do you want to add another parameter expression ? yes(y) or no (n)", input => input.EqualsAnyOf("yes", "y", "no", "n"), input => $"Input: '{input}' does not match any of yes(y) or no (n)");
                if (required.EqualsAnyOf("n", "no"))
                {
                    return parameter;
                }
            }
        }
    }
}
