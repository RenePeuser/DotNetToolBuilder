using System.Collections.Generic;

namespace DotNetTool.Builder.Test.Validation
{
    using System;
    using System.Linq;
    using DotNetTool.Builder.Validation;
    using DotNetTool.Builder.Validation.Expression;
    using Extensions;
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
            yield return new ExpressionMultipleWhitespacesValidator();
        }
    }

    [TestClass]
    public class InvalidExpressions : ValidationTestBase
    {
        // Hint implement all thees tests because later we want evaluate the output of each error
        // but of time issue we want stabilize the beta version.
        [TestMethod]
        public void Missing_Tool_Name_Is_Not_Allowed()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "").IsValid);
        }

        [TestMethod]
        public void Whitespace_Only_Is_Not_Allowed()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", " ").IsValid);
        }

        [TestMethod]
        public void Type_Cast_Only_Is_Not_Allowed()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "[string]").IsValid);
        }

        [TestMethod]
        public void Empty_Type_Cast_Only_Is_Not_Allowed()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "[]").IsValid);
        }

        [TestMethod]
        public void Option_Only_Is_Not_Allowed()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "--version").IsValid);
        }

        [TestMethod]
        public void Only_Tool_Name_Is_Not_Allowed()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "dotnet").IsValid);
        }

        [TestMethod]
        public void Only_Tool_Name_And_Post_Whitespace_Is_Not_Allowed()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "dotnet").IsValid);
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
        public void Only_Tool_Name_And_Casted_Argument_Is_Not_Valid()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "dotnet [string]<version>").IsValid);
        }

        [TestMethod]
        public void One_Command_And_Multiple_Arguments_Are_Not_Allowed()
        {
            Assert.IsFalse(ExpressionValidator.IsValid("dotnet", "dotnet tool <arg1> <arg2>").IsValid);
        }


        [TestMethod]
        public void All_Invalid_Expressions_Must_Be_Not_Valid()
        {
            var invalidExpressions = GetAll().ToList();

            var validExpressions = from expression in invalidExpressions
                                   let isValid = ExpressionValidator.IsValid(expression.ToolName, expression.Expression).IsValid
                                   where isValid
                                   select new { IsValid = isValid, Expression = expression.Expression };


            Assert.IsTrue(validExpressions.IsEmpty(), validExpressions.ToString($"Following expressions was valid, which should NOT:{Environment.NewLine}", result => result.Expression));
        }

        public IEnumerable<ExpressionWithExpectedResult> GetAll()
        {
            yield return new ExpressionWithExpectedResult("dotnet", "");
            yield return new ExpressionWithExpectedResult("dotnet", " ");
            yield return new ExpressionWithExpectedResult("dotnet", "  ");
            yield return new ExpressionWithExpectedResult("dotnet", "[]");
            yield return new ExpressionWithExpectedResult("dotnet", "<>");
            yield return new ExpressionWithExpectedResult("dotnet", "--");

            yield return new ExpressionWithExpectedResult("dotnet", "[string");
            yield return new ExpressionWithExpectedResult("dotnet", "<version");
            yield return new ExpressionWithExpectedResult("dotnet", "-version");

            yield return new ExpressionWithExpectedResult("dotnet", "string]");
            yield return new ExpressionWithExpectedResult("dotnet", "version>");
            yield return new ExpressionWithExpectedResult("dotnet", "-version");

            yield return new ExpressionWithExpectedResult("dotnet", "[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "<version>");
            yield return new ExpressionWithExpectedResult("dotnet", "--version");

            yield return new ExpressionWithExpectedResult("dotnet", " dotnet");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet ");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet  ");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet []");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet <>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet --");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet [string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet <version>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet --version");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet [string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet <version>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet --version");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet  tool");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool  ");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool ");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool  ");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool arg>");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[string");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[string]--option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg><arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[string]<arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[string]--option");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool - option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool -option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool -- option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option ");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option <");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option <arg");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option<arg>[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option--option");

            yield return new ExpressionWithExpectedResult("!§$%&/()=?`´", "!§$%&/()=?`´");
            yield return new ExpressionWithExpectedResult("dotnet", "!§$%&/()=?`´");
        }
    }


    public class ExpressionWithExpectedResult
    {
        public ExpressionWithExpectedResult(string toolName, string expression) : this(toolName, expression, string.Empty)
        {
        }

        public ExpressionWithExpectedResult(string toolName, string expression, string expectedMessage)
        {
            ToolName = toolName;
            Expression = expression;
            ExpectedMessage = expectedMessage;
        }

        public string ToolName { get; set; }
        public string Expression { get; }
        public string ExpectedMessage { get; }
    }
}
