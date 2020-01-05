namespace DotNetTool.Builder.Test.Parser.MultiExpressions
{
    using System.Collections.Generic;
    using System.Linq;
    using AssertHelper;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class IncrasingCommandsWithArg : ParameterExpressionBaseClass
    {
        private CommandInfo _rootCommand;

        protected override IEnumerable<string> GetExpressionsToParse()
        {
            yield return "root cmd1";
            yield return "root cmd1 <cmd1-arg>";
            yield return "root cmd1 cmd2";
            yield return "root cmd1 cmd2 <cmd2-arg>";
            yield return "root cmd1 cmd2 cmd3";
            yield return "root cmd1 cmd2 cmd3 <cmd3-arg>";
            yield return "root cmd1 cmd2 cmd3 cmd4";
            yield return "root cmd1 cmd2 cmd3 cmd4 <cmd4-arg>";
            yield return "root cmd1 cmd2 cmd3 cmd4 cmd5";
            yield return "root cmd1 cmd2 cmd3 cmd4 cmd5 <cmd5-arg>";
        }

        protected override void OnInit()
        {
            _rootCommand = ParseResult;
        }

        [TestMethod]
        public void Root_Command_Does_Not_Have_Argument()
        {
            Assert.IsNull(_rootCommand.Argument);
        }

        [TestMethod]
        public void Root_Command_Does_Not_Have_AnyOptions()
        {
            Assert.IsTrue(_rootCommand.Options == null || !_rootCommand.Options.Any());
        }

        [TestMethod]
        public void All_Sub_Commands_Count_Must_Be_One()
        {
            var errors = _rootCommand.AssertAllSubCommandRecursive(cmd => Enumerable.Count<CommandInfo>(cmd.SubCommands) > 1, cmd => $"Command: '{cmd.Name}' have more than one sub command").ToList();

            Assert.IsFalse(errors.Any(), errors.ToErrorMessage("Following errors was collected:"));
        }

        [TestMethod]
        public void All_Sub_Commands_Must_Have_One_Argument()
        {
            var errors = _rootCommand.SubCommands.First().AssertAllSubCommandRecursive(cmd => cmd.Argument?.Name != $"{cmd.Name}-arg", cmd => $"Command: '{cmd.Name}' have not expected argument name: '{cmd.Name}-arg'").ToList();

            Assert.IsFalse(errors.Any(), errors.ToErrorMessage("Following errors was collected:"));
        }
    }
}