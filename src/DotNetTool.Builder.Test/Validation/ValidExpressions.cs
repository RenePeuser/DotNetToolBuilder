namespace DotNetTool.Builder.Test.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidExpressions : ValidationTestBase
    {
        [TestMethod]
        public void All_Expressions_Must_Be_Valid()
        {
            var validExpressions = GetAll().ToList();
            var invalidExpressions = from expression in validExpressions
                                     let expressionInfo = ExpressionTokenizer.Tokenize(expression.Expression)
                                     let isValid = ExpressionValidator.IsValid(expression.ToolName, expressionInfo).IsValid.IsFalse()
                                     where isValid
                                     select new { IsValid = isValid, expression.Expression };


            Assert.IsTrue(invalidExpressions.IsEmpty(), invalidExpressions.ToString($"Following expressions was invalid, which should be valid:{Environment.NewLine}", result => result.Expression));
        }

        public IEnumerable<ExpressionWithExpectedResult> GetAll()
        {
            // New feature multiple whitespaces will be optimized away
            yield return new ExpressionWithExpectedResult("dotnet", " dotnet build");
            yield return new ExpressionWithExpectedResult("dotnet", " dotnet    build");
            yield return new ExpressionWithExpectedResult("dotnet", " dotnet build");
            yield return new ExpressionWithExpectedResult("dotnet", " dotnet build   ");

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

            yield return new ExpressionWithExpectedResult("dotnet", "dotnet  build  <solution>  --configuration  [string]<build-config>");

            yield return new ExpressionWithExpectedResult("son-goku", "son-goku build");

            yield return new ExpressionWithExpectedResult("songoku", "songoku collect dragonballs --all");
            yield return new ExpressionWithExpectedResult("songoku", "songoku do transform to <saiyajin-level>");
            yield return new ExpressionWithExpectedResult("songoku", "songoku do transform to <saiyajin-level> --use-sensobean");
        }
    }
}