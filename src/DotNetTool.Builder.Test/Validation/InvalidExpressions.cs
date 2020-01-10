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
            var dotNetToolName = new DotNetToolName("dotnet");

            yield return new ExpressionWithExpectedResult(new DotNetToolName("§"), "§ tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName("%"), "% tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName("&"), "& tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName("/"), "/ tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName("("), "( tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName(")"), ") tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName("["), "[ tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName("]"), "] tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName("?"), "? tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName("$"), "$ tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName("´"), "´ tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName("-"), "- tool");

            //This is already handled when user has to give in tool name, this situation can not happen here.
            yield return new ExpressionWithExpectedResult(new DotNetToolName("a.b"), "a.b tool");
            yield return new ExpressionWithExpectedResult(new DotNetToolName("a/b"), "a/b tool"); ;
            yield return new ExpressionWithExpectedResult(new DotNetToolName("!§$%&/()=?`´"), "!§$%&/()=?`´");

            yield return new ExpressionWithExpectedResult(dotNetToolName, "");
            yield return new ExpressionWithExpectedResult(dotNetToolName, " ");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "  ");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "[]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "<>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "--");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "[string");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "<version");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "-version");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "version>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "-version");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "<version>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "--version");
            yield return new ExpressionWithExpectedResult(dotNetToolName, " dotnet");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet ");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet 2");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet  ");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet []");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet <>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet --");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet [string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet <version>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet --version");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet [string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet <version>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet --version");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool []<>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool §");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool %");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool &");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool /");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool (");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool )");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool [");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool ]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool ?");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool $");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool ´");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool []");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool < >");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool - -");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool [ ]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <->");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool ---");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool [-]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <.>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool -.-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool [.]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <%>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool -&-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool [$]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool<arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool--option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool<arg>-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool--option-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool[string]-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <2arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg>string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg>[2string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg>[string");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg>[ ]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg>[--]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg>--option <arg>--");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg>--option <arg>[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg>--option <opt>-[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg> --option <arg>--");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg> --option <opt>-[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg> --option [string]-<arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg> --option <arg>[string]-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg> --option<arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg> --option<arg>[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg> --option<opt>-[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg> --option<opt>-[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg> invalidCommand");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg>[string]--option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg><arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg>[string]<arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool <arg>[string]--option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --2option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool - option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool -option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool -- option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option <");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool ---option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option <arg");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option[string]-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option-[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option<arg>[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option--option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option invalidCommand");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option <value> invalidCommand");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option <value>[string] -");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option <value> -");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option -");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option <arg> --option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool --option --option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool §");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool %");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool &");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool /");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool (");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool )");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool [");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool ]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool ?");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool $");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool ´");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool []");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool < >");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool - -");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool [ ]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <->");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool ---");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool [-]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <.>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool -.-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool [.]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <%>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool -&-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool [$]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool<arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool--option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool<arg>-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool--option-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool[string]-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <2arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg>string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg>[2string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg>[string");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg>[ ]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg>[--]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg>--option <arg>--");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg>--option <arg>[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg>--option <opt>-[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg> --option <arg>--");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg> --option <opt>-[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg> --option [string]-<arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg> --option <arg>[string]-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg> --option<arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg> --option<arg>[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg> --option<opt>-[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg> --option<opt>-[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg> invalidCommand");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg>[string]--option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg><arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg>[string]<arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool <arg>[string]--option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --2option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool - option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool -option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool -- option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option <");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool ---option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option <arg");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option arg>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option[string]-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option-[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option<arg>[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option--option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option-");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option invalidCommand");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option <value> invalidCommand");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option <value>[string] -");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option <value> -");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option -");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option <arg> --option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet not-valid tool --option --option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet to-ol --option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet tool tool --option");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "!§$%&/()=?`´");
        }
    }
}
