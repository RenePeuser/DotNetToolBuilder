using System.Linq;
using DotNetTool.Builder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Test.Parser
{
    [TestClass]
    public class Sub_Command_1_Test : ParameterExpressionBaseClass
    {
        private ParameterInfo _subCommand1;

        protected override string GetExpressionToParse()
        {
            return "root sub1 <sub1Arg>[System.IO.FileInfo] --sub1-opt <sub-opt-value> --sub-opt2";
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
            Assert.IsNotNull(_subCommand1.ArgumentInfo);

        }

        [TestMethod]
        public void Assert_SubcCommands_Count()
        {
            Assert.IsTrue(_subCommand1.SubCommands.IsEmpty());

        }
    }
}