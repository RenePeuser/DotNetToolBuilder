using System.Collections.Generic;

namespace DotNetTool.Builder.Test.Validation
{
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
            yield return new ExpressionMinimumCommandValidator();
            yield return new ExpressionOnlyOneArgumentValidator();
            yield return new ExpressionOptionValidator(primitiveTypeNameValidator);
            yield return new ExpressionToolNameValidator();
        }
    }

    [TestClass]
    public class InvalidExpressions : ValidationTestBase
    {
        [TestMethod]
        public void Only_Tool_Name_Is_Not_Allowed()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "dotnet").IsValid);
        }

        [TestMethod]
        public void Only_Tool_Name_And_Post_Whitespace_Is_Not_Allowed()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "dotnet" ).IsValid);
        }

        [TestMethod]
        public void Only_Tool_Name_And_Pre_Whitespace_Is_Not_Allowed()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", " dotnet").IsValid);
        }

        [TestMethod]
        public void Only_Tool_Name_And_One_Cast_Is_Not_Valid()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "dotnet [string]").IsValid);
        }

        [TestMethod]
        public void Only_Tool_Name_And_Option_Is_Not_Valid()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "dotnet --version").IsValid);
        }

        [TestMethod]
        public void Only_Tool_Name_And_Argument_Is_Not_Valid()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "dotnet <version>").IsValid);
        }

        [TestMethod]
        public void Only_Tool_Name_And__Casted_Argument_Is_Not_Valid()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "dotnet [string]<version>").IsValid);
        }
    }
}
