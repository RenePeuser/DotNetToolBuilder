using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.Services.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer;
using DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToolNameValidator = DotNetTool.Builder.Services.Validation.ToolNameValidator;

namespace DotNetTool.Builder.Test.Validation
{
    public abstract class ValidationTestBase
    {
        internal IExpressionValidator ExpressionValidator { get; private set; }
        internal IExpressionTokenizer ExpressionTokenizer { get; private set; }

        [TestInitialize]
        public void Init()
        {
            ExpressionTokenizer = new ToolBuilder.FromConsole.Tokenizer.Tokenizer(GetTokenizers().ToList());
            ExpressionValidator = new ExpressionValidator(GetValidators().ToList());
        }

        private IEnumerable<ITokenizer> GetTokenizers()
        {
            yield return new CommandTokenizer();
            yield return new ArgumentTokenizer();
            yield return new OptionTokenizer();
        }

        private IEnumerable<IExpressionContentValidator> GetValidators()
        {
            var builtInTypeTableService = new BuiltInTypeTableService();
            var primitiveTypeNameValidator = new PrimitiveTypeNameValidator(builtInTypeTableService);
            var toolNameValidator = new ToolNameValidator(primitiveTypeNameValidator);
            var argumentTypeOptimizer = new ArgumentTypeOptimizer(GetTypeNameOptimizers().ToList());
            var validateCommandNameFromString = new ValidateCommandNameFromString(primitiveTypeNameValidator);

            yield return new ArgumentValidator(primitiveTypeNameValidator);
            yield return new TypeCastValidator();
            yield return new CharValidator();
            yield return new CommandMustBeforeOptionOrArgumentValidator();
            yield return new MinimumCommandValidator();
            yield return new OnlyOneArgumentValidator();
            yield return new OptionValidator(primitiveTypeNameValidator);
            yield return new ToolBuilder.FromConsole.Validation.Expression.ToolNameValidator(toolNameValidator);
            yield return new CommandNameValidation(validateCommandNameFromString);
            yield return new UnknownTokenValidator();
            yield return new MultipleOptionValidator();
            yield return new DuplicatedCommandValidator();
            yield return new ArgumentTypeValidator(primitiveTypeNameValidator, argumentTypeOptimizer);
        }

        private IEnumerable<ITypeNameOptimizer> GetTypeNameOptimizers()
        {
            var builtInTypeTableService = new BuiltInTypeTableService();
            var primitiveTypeNameValidator = new PrimitiveTypeNameValidator(builtInTypeTableService);

            yield return new FileInfoOptimizer();
            yield return new DirectoryInfoOptimizer();
            yield return new FileSystemInfoOptimizer();
            yield return new SystemTypeNameOptimizer(primitiveTypeNameValidator, builtInTypeTableService);
        }
    }
}
