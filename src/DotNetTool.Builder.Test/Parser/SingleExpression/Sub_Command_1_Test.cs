namespace DotNetTool.Builder.Test.Parser.SingleExpression
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class Sub_Command_1_Test : ParameterExpressionBaseClass
    {
        private CommandInfo _subCommand1;

        protected override IEnumerable<string> GetExpressionsToParse()
        {
            yield return "root sub1 <sub1Arg>[System.IO.FileInfo] --sub1-opt <sub-opt-value> --sub-opt2";
        }


        protected override void OnInit()
        {
            _subCommand1 = ParseResult.SubCommands.First();
        }

        [TestMethod]
        public void Assert_Name()
        {
            Assert.AreEqual("sub1", _subCommand1.Name);
        }

        [TestMethod]
        public void Assert_Description()
        {
            Assert.AreEqual(string.Empty, _subCommand1.Description);
        }

        [TestMethod]
        public void Assert_Options_Count()
        {
            Assert.AreEqual(2, _subCommand1.Options.Count());
        }

        [TestMethod]
        public void Assert_Argument_Is_Not_Null()
        {
            Assert.IsNotNull(_subCommand1.Argument);
        }

        [TestMethod]
        public void Assert_SubcCommands_Count()
        {
            Assert.IsFalse(_subCommand1.SubCommands.Any());
        }
    }
}
