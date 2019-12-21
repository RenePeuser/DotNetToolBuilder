using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Parser.Argument;
using DotNetTool.Builder.Parser.Options;
using DotNetTool.Builder.Parser.Parameters;
using DotNetTool.Builder.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trumpf.Hmi.ObjectCreator;

namespace DotNetTool.Builder.Test.Parser
{
    [TestClass]
    public class ParameterExpressionParserTest
    {
        private CliParameterInfo _parseResult;

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

            _parseResult = paser.Parse("root sub1 <sub1Arg>[System.IO.FileInfo] --sub1-opt <sub-opt-value> --sub-opt2", null);
        }

        [TestMethod]
        public void Assert_Root_Command_Name()
        {
            Assert.AreEqual("root", _parseResult.Name);
        }

        [TestMethod]
        public void Assert_Root_Command_Options()
        {
            Assert.AreEqual(0, _parseResult.Options.Count());
        }

        [TestMethod]
        public void Assert_Root_Command_Argument()
        {
            Assert.IsNull(_parseResult.ArgumentInfo);
        }

        [TestMethod]
        public void Assert_Root_Command_Description()
        {
            Assert.IsNull(_parseResult.Decsription);
        }

    }
}
