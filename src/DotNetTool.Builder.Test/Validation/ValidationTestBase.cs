namespace DotNetTool.Builder.Test.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using DotNetTool.Builder.Validation;
    using DotNetTool.Builder.Validation.Expression;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ToolNameValidator = DotNetTool.Builder.Validation.Expression.ToolNameValidator;

    public abstract class ValidationTestBase
    {
        internal ExpressionValidator ExpressionValidator { get; private set; }

        [TestInitialize]
        public void Init()
        {
            ExpressionValidator = new ExpressionValidator(GetValidators().ToList());
        }

        private IEnumerable<IExpressionContentValidator> GetValidators()
        {
            var primitiveTypeNameValidator = new PrimitiveTypeNameValidator();
            yield return new ArgumentValidator(primitiveTypeNameValidator);
            yield return new TypeCastValidator();
            yield return new CharValidator();
            yield return new CommandMustBeforeOptionOrArgumentValidator();
            yield return new MinimumCommandValidator();
            yield return new MultipleWhitespacesValidator();
            yield return new OnlyOneArgumentValidator();
            yield return new OptionValidator(primitiveTypeNameValidator);
            yield return new ToolNameValidator();
            yield return new CommandNameValidation();
        }
    }
}