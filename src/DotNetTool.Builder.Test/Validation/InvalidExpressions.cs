using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Validation
{
    [TestClass]
    public class InvalidExpressions : ValidationTestBase
    {
        [TestMethod]
        public void All_Expressions_Must_Be_Not_Valid()
        {
            var invalidExpressions = GetAll().ToList();
            //invalidExpressions = (List<ExpressionWithExpectedResult>)(new ExpressionWithExpectedResult("a-b", "a-b command ---invalidoption")).ToIList();

            var validExpressions = (from expression in invalidExpressions
                                    let expressionInfo = ExpressionTokenizer.Tokenize(expression.Expression)
                                    let isValid = ExpressionValidator.IsValid(expression.DotNetToolName, expressionInfo).IsValid
                                    where isValid
                                    select expression.Expression).ToList();


            Assert.IsFalse(validExpressions.Any(), AssertHelper.AssertHelper.ToErrorMessage(validExpressions, "Following expressions was valid, which should NOT:"));
        }


        private IEnumerable<ExpressionWithExpectedResult> GetAll()
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
            yield return new ExpressionWithExpectedResult("-", "- tool");

            //This is already handled when user has to give in tool name, this situation can not happen here.
            yield return new ExpressionWithExpectedResult("a.b", "a.b tool");
            yield return new ExpressionWithExpectedResult("a/b", "a/b tool");
            yield return new ExpressionWithExpectedResult("!§$%&/()=?`´", "!§$%&/()=?`´");

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
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool []<>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool <arg>[]i");
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
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool ---option");
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
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option <arg> --option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool --option --option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool §");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool %");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool &");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool /");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool (");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool )");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool [");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool ]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool ?");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool $");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool ´");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool []");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool < >");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool - -");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool [ ]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <->");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool ---");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool [-]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <.>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool -.-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool [.]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <%>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool -&-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool [$]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool<arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool--option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool<arg>-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool--option-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool[string]-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <2arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg>string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg>[2string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg>[string");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg>[ ]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg>[--]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg>--option <arg>--");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg>--option <arg>[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg>--option <opt>-[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg> --option <arg>--");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg> --option <opt>-[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg> --option [string]-<arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg> --option <arg>[string]-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg> --option<arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg> --option<arg>[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg> --option<opt>-[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg> --option<opt>-[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg> invalidCommand");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg>[string]--option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg><arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg>[string]<arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool <arg>[string]--option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --2option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool - option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool -option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool -- option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option <");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool ---option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option <arg");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option arg>");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option[string]-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option-[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option<arg>[string]");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option--option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option-");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option invalidCommand");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option <value> invalidCommand");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option <value>[string] -");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option <value> -");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option -");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option <arg> --option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet not-valid tool --option --option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet to-ol --option");
            yield return new ExpressionWithExpectedResult("dotnet", "dotnet tool tool --option");
            yield return new ExpressionWithExpectedResult("dotnet", "!§$%&/()=?`´");
        }
    }
}
