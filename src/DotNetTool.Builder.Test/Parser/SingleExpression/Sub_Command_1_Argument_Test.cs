using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Parser.SingleExpression
{
    [TestClass]
    public class Sub_Command_1_Argument_Test : ParameterExpressionBaseClass
    {
        private ArgumentInfo _argumentInfo;

        protected override IEnumerable<string> GetExpressionsToParse()
        {
            yield return "root sub1 <sub1Arg>[System.IO.FileInfo] --sub1-opt <sub-opt-value> --sub-opt2";
        }


        protected override void OnInit()
        {
            _argumentInfo = ParseResult.SubCommands.First().Argument;
        }

        [TestMethod]
        public void Assert_Argument_Is_Not_Null()
        {
            Assert.IsNotNull(_argumentInfo);
        }

        [TestMethod]
        public void Assert_Argument_Value()
        {
            Assert.AreEqual("<sub1Arg>[System.IO.FileInfo]", _argumentInfo.Value);
        }

        [TestMethod]
        public void Assert_Argument_Description()
        {
            Assert.AreEqual("", _argumentInfo.Description);
        }

        [TestMethod]
        public void Assert_Argument_Name()
        {
            Assert.AreEqual("sub1Arg", _argumentInfo.Name);
        }

        [TestMethod]
        public void Assert_Argument_NormalizedName()
        {
            Assert.AreEqual("Sub1Arg", _argumentInfo.NormalizedName);
        }

        [TestMethod]
        public void Assert_Argument_Type()
        {
            Assert.AreEqual("System.IO.FileInfo", _argumentInfo.Type);
        }

        [TestMethod]
        public void Assert_Argument_Optimized_Type_Name()
        {
            Assert.AreEqual("System.IO.FileInfo", _argumentInfo.OptimizedType);
        }
    }
}
