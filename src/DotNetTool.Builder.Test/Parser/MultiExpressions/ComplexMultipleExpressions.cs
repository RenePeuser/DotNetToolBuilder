using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Parser.MultiExpressions
{
    [TestClass]
    public class ComplexMultipleExpressions : ParameterExpressionBaseClass
    {
        private CommandInfo _rootCommand;

        protected override IEnumerable<string> GetExpressionsToParse()
        {
            yield return "root cmd1";
            yield return "root cmd1 <arg1>";
            yield return "root cmd1 <arg1> --cmd1-opt1";
            yield return "root cmd1 <arg1> --cmd1-opt1 <cmd1-opt1-arg>";
            yield return "root cmd1 <arg1> --cmd1-opt1 <cmd1-opt1-arg> --cmd1-opt2 <cmd1-opt2-arg>";
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

        [TestMethod]
        public void Assert_Cmd1_Options_Count()
        {
            Assert.AreEqual(2, _rootCommand.SubCommands.First().Options.Count());
        }

        [TestMethod]
        public void Assert_Cmd1_Option_1_Name()
        {
            Assert.AreEqual("cmd1-opt1", _rootCommand.SubCommands.First().Options.First().Name);
        }

        [TestMethod]
        public void Assert_Cmd1_Option_1_Argument()
        {
            Assert.IsNotNull(_rootCommand.SubCommands.First().Options.First().Argument);
        }

        [TestMethod]
        public void Assert_Cmd1_Option_1_Argument_Name()
        {
            Assert.IsNotNull("cmd1-opt1-arg", _rootCommand.SubCommands.First().Options.First().Argument.Name);
        }

        [TestMethod]
        public void Assert_Cmd2_Option_2_Name()
        {
            Assert.AreEqual("cmd1-opt2", _rootCommand.SubCommands.First().Options.Last().Name);
        }

        [TestMethod]
        public void Assert_Cmd2_Option_2_Argument()
        {
            Assert.IsNotNull(_rootCommand.SubCommands.First().Options.Last().Argument);
        }

        [TestMethod]
        public void Assert_Cmd2_Option_2_Argument_Name()
        {
            Assert.AreEqual("cmd1-opt2-arg", _rootCommand.SubCommands.First().Options.Last().Argument.Name);
        }
    }
}
