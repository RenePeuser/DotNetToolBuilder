using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Validation
{
    [TestClass]
    public class ValidExpressions : ValidationTestBase
    {
        [TestMethod]
        public void All_Expressions_Must_Be_Valid()
        {
            var validExpressions = GetAll().ToList();
            var invalidExpressions = (from expression in validExpressions
                                      let expressionInfo = ExpressionTokenizer.Tokenize(expression.Expression)
                                      let isValid = !ExpressionValidator.IsValid(expression.DotNetToolName, expressionInfo).IsValid
                                      where isValid
                                      select expression.Expression).ToList();

            Assert.IsFalse(invalidExpressions.Any(), AssertHelper.AssertHelper.ToErrorMessage(invalidExpressions, "Following expressions was invalid, which should be valid:"));
        }

        private IEnumerable<ExpressionWithExpectedResult> GetAll()
        {
            var dotNetToolName = new DotNetToolName("dotnet");

            // New feature multiple whitespaces will be optimized away
            yield return new ExpressionWithExpectedResult(dotNetToolName, " dotnet build");
            yield return new ExpressionWithExpectedResult(dotNetToolName, " dotnet    build");
            yield return new ExpressionWithExpectedResult(dotNetToolName, " dotnet build");
            yield return new ExpressionWithExpectedResult(dotNetToolName, " dotnet build   ");

            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet build");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet build <solution>");
            
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet build <solution> --no-restore");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet build <solution> --configuration <build-config>");
            
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet build <solution>[System.IO.FileInfo]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet build [System.IO.FileInfo]<solution>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet build <solution>[System.IO.FileInfo] --no-restore");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet build [System.IO.FileInfo]<solution> --no-restore");
            
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet build <solution> --configuration <build-config>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet build <solution> --configuration <build-config>[string]");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet build <solution> --configuration [string]<build-config>");
            
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet  build  <solution>  --configuration  [string]<build-config>");

            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet collect dragonballs --all");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet do transform to <saiyajin-level>");
            yield return new ExpressionWithExpectedResult(dotNetToolName, "dotnet do transform to <saiyajin-level> --use-sensobean");

            var dotnetToolNameWithMinus = new DotNetToolName("dotnet-tool", "tool");

            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, " dotnet-tool build");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, " dotnet-tool    build");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, " dotnet-tool build");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, " dotnet-tool build   ");
            
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool build");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool build <solution>");
            
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool build <solution> --no-restore");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool build <solution> --configuration <build-config>");
            
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool build <solution>[System.IO.FileInfo]");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool build [System.IO.FileInfo]<solution>");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool build <solution>[System.IO.FileInfo] --no-restore");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool build [System.IO.FileInfo]<solution> --no-restore");
            
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool build <solution> --configuration <build-config>");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool build <solution> --configuration <build-config>[string]");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool build <solution> --configuration [string]<build-config>");
            
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool  build  <solution>  --configuration  [string]<build-config>");
            
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool collect dragonballs --all");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool do transform to <saiyajin-level>");
            yield return new ExpressionWithExpectedResult(dotnetToolNameWithMinus, "dotnet-tool do transform to <saiyajin-level> --use-sensobean");
        }
    }
}
