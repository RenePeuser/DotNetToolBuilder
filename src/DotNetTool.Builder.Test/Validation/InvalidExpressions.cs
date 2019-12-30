using System.Collections.Generic;

namespace DotNetTool.Builder.Test.Validation
{
    using System;
    using System.Linq;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;


    [TestClass]
    public class InvalidExpressions : ValidationTestBase
    {
        [TestMethod]
        public void All_Expressions_Must_Be_Not_Valid()
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

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool §");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool %");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool &");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool /");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool (");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool )");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool [");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool ]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool ?");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool $");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool ´");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool []");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool < >");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool - -");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool [ ]");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <%>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool -&-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool [$]");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool arg>");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[string");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[ ]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[--]");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg> invalidCommand");

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

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option invalidCommand");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option <value> invalidCommand");

            yield return new ExpressionWithExpectedResult("!§$%&/()=?`´", "!§$%&/()=?`´");
            yield return new ExpressionWithExpectedResult("dotnet", "!§$%&/()=?`´");
        }
    }

    [TestClass]
    public class ValidExpressions : ValidationTestBase
    {
        [TestMethod]
        public void All_Expressions_Must_Be_Valid()
        {
            var validExpressions = GetAll().ToList();

            var invalidExpressions = from expression in validExpressions
                                     let isInvalid = ExpressionValidator.IsValid(expression.ToolName, expression.Expression).IsValid.IsFalse()
                                     where isInvalid
                                     select new { IsValid = isInvalid, Expression = expression.Expression };


            Assert.IsTrue(invalidExpressions.IsEmpty(), invalidExpressions.ToString($"Following expressions was invalid, which should be valid:{Environment.NewLine}", result => result.Expression));
        }

        public IEnumerable<ExpressionWithExpectedResult> GetAll()
        {
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet build");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet build <solution>");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet build <solution> --no-restore");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet build <solution> --configuration <build-config>");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet build <solution>[System.IO.FileInfo]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet build [System.IO.FileInfo]<solution>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet build <solution>[System.IO.FileInfo] --no-restore");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet build [System.IO.FileInfo]<solution> --no-restore");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet build <solution> --configuration <build-config>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet build <solution> --configuration <build-config>[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet build <solution> --configuration [string]<build-config>");

            yield return new ExpressionWithExpectedResult("songoku", "songoku collect dragonballs --all");
            yield return new ExpressionWithExpectedResult("songoku", "songoku do transform to <saiyajin-level>");
            yield return new ExpressionWithExpectedResult("songoku", "songoku do transform to <saiyajin-level> --use-sensobean");
        }
    }
}
