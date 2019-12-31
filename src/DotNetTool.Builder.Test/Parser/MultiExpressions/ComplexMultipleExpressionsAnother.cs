namespace DotNetTool.Builder.Test.Parser.MultiExpressions
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DotNetTool.Builder.Models;
    using DotNetTool.Builder.Test.Parser;


    [TestClass]
    public class ComplexMultipleExpressionsAnother : ParameterExpressionBaseClass
    {
        private CommandInfo _rootCommand;

        protected override IEnumerable<string> GetExpressionsToParse()
        {
            yield return "root cmd1 <arg1>";
            yield return "root cmd1 --cmd1-opt1";
            yield return "root cmd1 <arg2>";
            yield return "root cmd1 <arg2> --cmd1-opt1";
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
        public void Assert_Root_SubCommands_Count()
        {
            Assert.AreEqual(1, _rootCommand.SubCommands.Count());
        }

        [TestMethod]
        public void Assert_Cmd1_Argument_Count()
        {
            Assert.IsNotNull(_rootCommand.SubCommands.First().Argument);
        }

        [TestMethod]
        public void Assert_Cmd1_Argument_Name()
        {
            Assert.IsNotNull("arg1", _rootCommand.SubCommands.First().Argument.Name);
        }
    }
}