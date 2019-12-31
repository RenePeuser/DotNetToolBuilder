namespace DotNetTool.Builder.Test.Parser.SingleExpression
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class Root_Command_Test : ParameterExpressionBaseClass
    {
        private CommandInfo _rootCommand;

        protected override IEnumerable<string> GetExpressionsToParse()
        {
            yield return "root sub1 <sub1Arg>[System.IO.FileInfo] --sub1-opt <sub-opt-value> --sub-opt2";
        }

        protected override void OnInit()
        {
            _rootCommand = ParseResult;
        }

        [TestMethod]
        public void Assert_Root_Command_Name()
        {
            Assert.AreEqual("root", _rootCommand.Name);
        }

        [TestMethod]
        public void Assert_Root_Command_Options()
        {
            Assert.AreEqual(0, _rootCommand.Options.Count());
        }

        [TestMethod]
        public void Assert_Root_Command_Argument()
        {
            Assert.IsNull(_rootCommand.Argument);
        }

        [TestMethod]
        public void Assert_Root_Command_Description()
        {
            Assert.AreEqual(string.Empty, _rootCommand.Description);
        }

        [TestMethod]
        public void Assert_Root_SubCommands_Count()
        {
            Assert.AreEqual(1, _rootCommand.SubCommands.Count());
        }
    }
}
