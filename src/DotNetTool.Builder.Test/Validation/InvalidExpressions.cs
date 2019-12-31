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
            invalidExpressions = (List<ExpressionWithExpectedResult>) (new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg> invalidCommand")).ToIList();

            var validExpressions = from expression in invalidExpressions
                                   let expressionInfo = ExpressionTokenizer.Tokenize(expression.Expression)
                                   let isValid = ExpressionValidator.IsValid(expression.ToolName, expressionInfo).IsValid
                                   where isValid
                                   select new { IsValid = isValid, expression.Expression };


            Assert.IsTrue(validExpressions.IsEmpty(), validExpressions.ToString($"Following expressions was valid, which should NOT:{Environment.NewLine}", result => result.Expression));
        }

        public IEnumerable<ExpressionWithExpectedResult> GetAll()
        {
            yield return new ExpressionWithExpectedResult("§", "§ tool");
            yield return new ExpressionWithExpectedResult("%", "% tool");
            yield return new ExpressionWithExpectedResult("&", "& tool");
            yield return new ExpressionWithExpectedResult("/", "/ tool");
            yield return new ExpressionWithExpectedResult("(", "( tool");
            yield return new ExpressionWithExpectedResult(")", ") tool");
            yield return new ExpressionWithExpectedResult("[", "[ tool");
            yield return new ExpressionWithExpectedResult("]", "] tool");
            yield return new ExpressionWithExpectedResult("?", "? tool");
            yield return new ExpressionWithExpectedResult("$", "$ tool");
            yield return new ExpressionWithExpectedResult("´", "´ tool");

            yield return new ExpressionWithExpectedResult(" ", "  tool");
            yield return new ExpressionWithExpectedResult("-", "- tool");

            // This is already handled when user has to give in tool name, this situation can not happen.
            // yield return new ExpressionWithExpectedResult("a.b", "a.b tool");
            // yield return new ExpressionWithExpectedResult("a-b", "a-b tool");

            yield return new ExpressionWithExpectedResult("a/b", "a/b tool");
            yield return new ExpressionWithExpectedResult("a-b", "a-b  tool");

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

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet 2");
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

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <->");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool ---");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool [-]");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <.>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool -.-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool [.]");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <%>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool -&-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool [$]");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool<arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool--option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool[string]");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool<arg>-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool--option-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool[string]-");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool arg>");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <2arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[2string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[string");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[ ]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[--]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>--option <arg>--");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>--option <arg>[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>--option <opt>-[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg> --option <arg>--");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg> --option <opt>-[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg> --option [string]-<arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg> --option <arg>[string]-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg> --option<arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg> --option<arg>[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg> --option<opt>-[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg> --option<opt>-[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg> invalidCommand");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[string]--option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg><arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[string]<arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[string]--option");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --2option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool - option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool -option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool -- option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option <");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option <arg");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option[string]-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option-[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option<arg>[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option--option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option invalidCommand");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option <value> invalidCommand");

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option <value>[string] -");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option <value> -");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option -");

            yield return new ExpressionWithExpectedResult("!§$%&/()=?`´", "!§$%&/()=?`´");
            yield return new ExpressionWithExpectedResult("dotnet", "!§$%&/()=?`´");
        }
    }
}
