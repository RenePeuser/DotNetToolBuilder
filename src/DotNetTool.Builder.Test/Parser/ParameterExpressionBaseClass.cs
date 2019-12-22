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
    public abstract class ParameterExpressionBaseClass
    {
        protected ParameterInfo ParseResult { get; private set; }

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
            var parameterService = new ParameterService();

            var allparsers = new IParameterValueParser[] { argumentOnlyParser, argumentWithPreTypeCast, argumentWithPostTypeCast, optionParser, parameterParser };
            var paser = new ParameterExpressionParser(allparsers, parameterService);

            var expression = GetExpressionToParse();
            ParseResult = paser.Parse(expression, null);
            OnInit();
        }

        protected abstract void OnInit();
    }
}
