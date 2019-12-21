using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Parser.Argument;
using DotNetTool.Builder.Parser.Options;
using DotNetTool.Builder.Parser.Parameters;
using DotNetTool.Builder.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trumpf.Hmi.Extensions;
using Trumpf.Hmi.ObjectCreator;

namespace DotNetTool.Builder.Test.Parser
{
    [TestClass]
    public abstract class ParameterExpressionBaseClass
    {
        protected CliParameterInfo ParseResult { get; private set; }

        protected abstract string GetExpressionToParse();

        [TestInitialize]
        public void Init()
        {
            var consoleService = TcObjectCreator.Create<IConsoleService>();
            var argumentOnlyParser = new ArgumentOnly(consoleService);
            var argumentWithPreTypeCast = new ArgumentWithPreTypeCast(argumentOnlyParser);
            var argumentWithPostTypeCast = new ArgumentWithPostTypeCast(argumentOnlyParser);
            var optionParser = new OptionParser(consoleService);
            var parameterParser = new ParameterParser(consoleService);

            var allparsers = new IParameterValueParser[] { argumentOnlyParser, argumentWithPreTypeCast, argumentWithPostTypeCast, optionParser, parameterParser };
            var paser = new ParameterExpressionParser(allparsers);

            ParseResult = paser.Parse("root sub1 <sub1Arg>[System.IO.FileInfo] --sub1-opt <sub-opt-value> --sub-opt2", null);
            OnInit();
        }

        protected virtual void OnInit()
        {
        }
    }

    [TestClass]
    public class Sub_Command_1_Argument_Test : ParameterExpressionBaseClass
    {
        private ArgumentInfo _argumentInfo;

        protected override string GetExpressionToParse()
        {
            return "root sub1 <sub1Arg>[System.IO.FileInfo] --sub1-opt <sub-opt-value> --sub-opt2";
        }

        protected override void OnInit()
        {
            base.OnInit();
            _argumentInfo = ParseResult.SubCommands.First().ArgumentInfo;
        }

        [TestMethod]
        public void Assert_Argument_Is_Not_Null()
        {
            Assert.IsNotNull(_argumentInfo);
        }

        [TestMethod]
        public void Assert_Argument_Value()
        {
            Assert.AreEqual("<sub1Arg>[System.IO.FileInfo]", _argumentInfo.Value);
        }

        [TestMethod]
        public void Assert_Argument_Description()
        {
            Assert.AreEqual("", _argumentInfo.Description);
        }

        [TestMethod]
        public void Assert_Argument_Name()
        {
            Assert.AreEqual("sub1Arg", _argumentInfo.Name);
        }

        [TestMethod]
        public void Assert_Argument_NormalizedName()
        {
            Assert.AreEqual("Sub1Arg", _argumentInfo.NormalizedName);
        }

        [TestMethod]
        public void Assert_Argument_Type()
        {
            Assert.AreEqual("System.IO.FileInfo", _argumentInfo.Type);
        }
    }


    [TestClass]
    public class Sub_Command_1_Test : ParameterExpressionBaseClass
    {
        private CliParameterInfo _subCommand1;

        protected override string GetExpressionToParse()
        {
            return "root sub1 <sub1Arg>[System.IO.FileInfo] --sub1-opt <sub-opt-value> --sub-opt2";
        }

        protected override void OnInit()
        {
            base.OnInit();
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


    [TestClass]
    public class Root_Command_Test : ParameterExpressionBaseClass
    {
        protected override string GetExpressionToParse()
        {
            return "root sub1 <sub1Arg>[System.IO.FileInfo] --sub1-opt <sub-opt-value> --sub-opt2";
        }

        [TestMethod]
        public void Assert_Root_Command_Name()
        {
            Assert.AreEqual("root", ParseResult.Name);
        }

        [TestMethod]
        public void Assert_Root_Command_Options()
        {
            Assert.AreEqual(0, ParseResult.Options.Count());
        }

        [TestMethod]
        public void Assert_Root_Command_Argument()
        {
            Assert.IsNull(ParseResult.ArgumentInfo);
        }

        [TestMethod]
        public void Assert_Root_Command_Description()
        {
            Assert.AreEqual(string.Empty, ParseResult.Description);
        }

        [TestMethod]
        public void Assert_Root_SubCommands_Count()
        {
            Assert.AreEqual(1, ParseResult.SubCommands.Count());
        }
    }
}
