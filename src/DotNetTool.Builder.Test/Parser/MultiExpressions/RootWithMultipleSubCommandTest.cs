using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Parser.MultiExpressions
{
    [TestClass]
    public class RootWithMultipleSubCommandTest : ParameterExpressionBaseClass
    {
        private CommandInfo _rootCommand;

        protected override IEnumerable<string> GetExpressionsToParse()
        {
            yield return "sonic list";
            yield return "sonic update";
            yield return "sonic delete";
        }

        protected override void OnInit()
        {
            _rootCommand = ParseResult;
        }

        [TestMethod]
        public void Assert_Root_Command_Name()
        {
            Assert.AreEqual(3, _rootCommand.SubCommands.Count());
        }

        [TestMethod]
        public void Assert_List_SubCommand()
        {
            Assert.AreEqual("list", _rootCommand.SubCommands.ElementAt(0).Name);
        }

        [TestMethod]
        public void Assert_Update_SubCommand()
        {
            Assert.AreEqual("update", _rootCommand.SubCommands.ElementAt(1).Name);
        }

        [TestMethod]
        public void Assert_Delete_SubCommand()
        {
            Assert.AreEqual("delete", _rootCommand.SubCommands.ElementAt(2).Name);
        }
    }
}
