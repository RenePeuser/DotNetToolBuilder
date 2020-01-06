using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Parser.MultiExpressions
{
    [TestClass]
    public class MultiEqualSubCommandsTest : ParameterExpressionBaseClass
    {
        private CommandInfo _rootCommand;

        protected override IEnumerable<string> GetExpressionsToParse()
        {
            yield return "dotnet tool install <package>";
            yield return "dotnet tool update <package>";
        }

        protected override void OnInit()
        {
            _rootCommand = ParseResult;
        }

        [TestMethod]
        public void Assert_Root_Command_Name()
        {
            Assert.AreEqual("dotnet", _rootCommand.Name);
        }

        [TestMethod]
        public void Assert_Root_SubCommands_Count()
        {
            Assert.AreEqual(1, _rootCommand.SubCommands.Count());
        }

        [TestMethod]
        public void Assert_Tool_Command()
        {
            Assert.AreEqual("tool", _rootCommand.SubCommands.First().Name);
        }

        [TestMethod]
        public void Assert_Install_Command()
        {
            Assert.AreEqual("install", _rootCommand.SubCommands.First().SubCommands.First().Name);
        }

        [TestMethod]
        public void Assert_Install_Argument_Command()
        {
            Assert.AreEqual("package", _rootCommand.SubCommands.First().SubCommands.First().Argument.Name);
        }

        [TestMethod]
        public void Assert_Install_Options_Count()
        {
            Assert.AreEqual(0, _rootCommand.SubCommands.First().SubCommands.First().Options.Count());
        }

        [TestMethod]
        public void Assert_Update_Command()
        {
            Assert.AreEqual("update", _rootCommand.SubCommands.First().SubCommands.Last().Name);
        }

        [TestMethod]
        public void Assert_Update_Argument_Command()
        {
            Assert.AreEqual("package", _rootCommand.SubCommands.First().SubCommands.Last().Argument.Name);
        }

        [TestMethod]
        public void Assert_Update_Options_Count()
        {
            Assert.AreEqual(0, _rootCommand.SubCommands.First().SubCommands.First().Options.Count());
        }
    }
}
