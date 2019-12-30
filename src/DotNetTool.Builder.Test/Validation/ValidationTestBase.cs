namespace DotNetTool.Builder.Test.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using DotNetTool.Builder.Validation;
    using DotNetTool.Builder.Validation.Expression;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            yield return new ExpressionArgumentValidator(primitiveTypeNameValidator);
            yield return new ExpressionCastValidator();
            yield return new ExpressionCharValidator();
            yield return new ExpressionCommandMustBeforeOptionOrArgumentValidator();
            yield return new ExpressionMinimumCommandValidator();
            yield return new ExpressionOnlyOneArgumentValidator();
            yield return new ExpressionOptionValidator(primitiveTypeNameValidator);
            yield return new ExpressionToolNameValidator();
            yield return new ExpressionMultipleWhitespacesValidator();
        }
    }
}