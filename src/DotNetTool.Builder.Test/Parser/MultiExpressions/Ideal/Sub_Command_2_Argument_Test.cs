using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Parser.MultiExpressions.Ideal
{
    [TestClass]
    public class Sub_Command_2_Argument_Test : ParameterExpressionBaseClass
    {
        private ArgumentInfo _argumentInfo;

        protected override IEnumerable<string> GetExpressionsToParse()
        {
            yield return "tool command1 <command1-arg>[System.IO.FileInfo] --version <version>";
            yield return "tool command2 <command2-arg>[string] --version <version>";
        }

        protected override void OnInit()
        {
            _argumentInfo = ParseResult.SubCommands.Last().Argument;
        }

        [TestMethod]
        public void Assert_Argument_Is_Not_Null()
        {
            Assert.IsNotNull(_argumentInfo);
        }

        [TestMethod]
        public void Assert_Argument_Value()
        {
            Assert.AreEqual("<command2-arg>[string]", _argumentInfo.Value);
        }

        [TestMethod]
        public void Assert_Argument_Description()
        {
            Assert.AreEqual("", _argumentInfo.Description);
        }

        [TestMethod]
        public void Assert_Argument_Name()
        {
            Assert.AreEqual("command2-arg", _argumentInfo.Name);
        }

        [TestMethod]
        public void Assert_Argument_NormalizedName()
        {
            Assert.AreEqual("Command2-arg", _argumentInfo.NormalizedName);
        }

        [TestMethod]
        public void Assert_Argument_Type()
        {
            Assert.AreEqual("string", _argumentInfo.Type);
        }
    }
}
