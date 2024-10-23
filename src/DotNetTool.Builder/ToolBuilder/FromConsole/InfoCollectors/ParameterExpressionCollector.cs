using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Parser;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer;
using DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal sealed class ParameterExpressionCollector(IConsoleService consoleService,
                                                       IParameterExpressionParser parameterExpressionParser,
                                                       IExpressionValidator expressionValidator,
                                                       IExpressionTokenizer expressionTokenizer,
                                                       ICollectTillInputCorrect collectTillInputCorrect)
        : IParameterExpressionCollector
    {
        public CommandInfo CollectFor(DotNetToolName dotNetDotNetToolName, string projectName)
        {
            CommandInfo parameter = null;
            while (true)
            {
                ExpressionInfo expressionInfo = null;
                ValidationResult validationResult = null;
                while (validationResult.IsNull() || validationResult.IsValid.IsFalse())
                {
                    consoleService.WriteInput("Please enter your parameter expression");
                    consoleService.WriteSample($"Sample: '{dotNetDotNetToolName.Name} command <argument> --option");

                    var parameterExpression = consoleService.ReadLine();
                    expressionInfo = expressionTokenizer.Tokenize(parameterExpression);
                    validationResult = expressionValidator.IsValid(dotNetDotNetToolName, expressionInfo, projectName);
                    if (validationResult.IsValid.IsFalse())
                    {
                        consoleService.WriteError(validationResult.Errors);
                    }
                }

                parameter = parameterExpressionParser.Parse(expressionInfo, parameter);

                var required = collectTillInputCorrect.CollectTillInputIsValid("Do you want to add another parameter expression ? yes(y) or no (n)", input => input.EqualsAnyOf("yes", "y", "no", "n"), input => $"Input: '{input}' does not match any of yes(y) or no (n)");
                if (required.EqualsAnyOf("n", "no"))
                {
                    return parameter;
                }
            }
        }
    }
}
