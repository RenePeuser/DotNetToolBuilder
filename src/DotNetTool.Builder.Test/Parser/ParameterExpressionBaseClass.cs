using DotNetTool.Builder.Models;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Parser.Argument;
using DotNetTool.Builder.Parser.Options;
using DotNetTool.Builder.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace DotNetTool.Builder.Test.Parser
{
    using System.Collections.Generic;
    using System.Linq;
    using DotNetTool.Builder.Parser.Commands;
    using DotNetTool.Builder.Tokenizer;

    [TestClass]
    public abstract class ParameterExpressionBaseClass
    {
        protected CommandInfo ParseResult { get; private set; }

        protected abstract string GetExpressionToParse();

        [TestInitialize]
        public void Init()
        {
            var consoleService = Substitute.For<IConsoleService>();
            var paser = new ParameterExpressionParser(new CommandParser(consoleService), new ArgumentParser(consoleService), new OptionParser(consoleService), new ParameterService());
            var tokenizer = new ExpressionTokenizer(GetTokenizer().ToList());
            var expression = GetExpressionToParse();
            var expressionInfo = tokenizer.Tokenize(expression);
            ParseResult = paser.Parse(expressionInfo, null);
            OnInit();
        }

        private IEnumerable<ITokenizer> GetTokenizer()
        {
            yield return new ArgumentTokenizer();
            yield return new OptionTokenizer();
            yield return new CommandTokenizer();
        }

        protected abstract void OnInit();
    }
}
