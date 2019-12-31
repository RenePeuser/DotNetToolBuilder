namespace DotNetTool.Builder.Test.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using DotNetTool.Builder.Tokenizer;
    using DotNetTool.Builder.Validation;
    using DotNetTool.Builder.Validation.Expression;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ToolNameValidator = DotNetTool.Builder.Validation.Expression.ToolNameValidator;

    public abstract class ValidationTestBase
    {
        internal IExpressionValidator ExpressionValidator { get; private set; }
        internal IExpressionTokenizer ExpressionTokenizer { get; private set; }

        [TestInitialize]
        public void Init()
        {
            ExpressionTokenizer = new ExpressionTokenizer(GetTokenizers().ToList());
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
            var primitiveTypeNameValidator = new PrimitiveTypeNameValidator();
            yield return new ArgumentValidator(primitiveTypeNameValidator);
            yield return new TypeCastValidator();
            yield return new CharValidator();
            yield return new CommandMustBeforeOptionOrArgumentValidator();
            yield return new MinimumCommandValidator();
            yield return new OnlyOneArgumentValidator();
            yield return new OptionValidator(primitiveTypeNameValidator);
            yield return new ToolNameValidator();
            yield return new CommandNameValidation();
            yield return new UnknownTokenValidator();
        }
    }
}