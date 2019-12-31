namespace DotNetTool.Builder.Test.Parser.MultiExpressions.Ideal
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class Sub_Command_2_Test : ParameterExpressionBaseClass
    {
        private CommandInfo _subCommand1;

        protected override IEnumerable<string> GetExpressionsToParse()
        {
            yield return "tool command1 <command1-arg>[System.IO.FileInfo] --version <version>";
            yield return "tool command2 <command2-arg>[string] --version <version>";
        }

        protected override void OnInit()
        {
            _subCommand1 = ParseResult.SubCommands.First();
        }

        [TestMethod]
        public void Assert_Name()
        {
            Assert.AreEqual("command1", _subCommand1.Name);
        }

        [TestMethod]
        public void Assert_Description()
        {
            Assert.AreEqual(string.Empty, _subCommand1.Description);
        }

        [TestMethod]
        public void Assert_Options_Count()
        {
            Assert.AreEqual(1, _subCommand1.Options.Count());
        }

        [TestMethod]
        public void Assert_Argument_Is_Not_Null()
        {
            Assert.IsNotNull(_subCommand1.Argument);
        }

        [TestMethod]
        public void Assert_SubcCommands_Count()
        {
            Assert.IsTrue(_subCommand1.SubCommands.IsEmpty());
        }
    }
}
