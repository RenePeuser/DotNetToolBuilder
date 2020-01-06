using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Parser.Argument;
using DotNetTool.Builder.Parser.Commands;
using DotNetTool.Builder.Parser.Options;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Test.Mocks;
using DotNetTool.Builder.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Parser
{
    [TestClass]
    public abstract class ParameterExpressionBaseClass
    {
        internal CommandInfo ParseResult { get; private set; }

        protected abstract IEnumerable<string> GetExpressionsToParse();

        [TestInitialize]
        public void Init()
        {
            var consoleService = new ConsoleMock();
            var paser = new ParameterExpressionParser(new CommandParser(consoleService), new ArgumentParser(consoleService), new OptionParser(consoleService), new ParameterService());
            var tokenizer = new DotNetTool.Builder.Tokenizer.Tokenizer(GetTokenizer().ToList());
            var expressions = GetExpressionsToParse().ToList();

            CommandInfo lastCommandInfo = null;
            foreach (var expression in expressions)
            {
                var expressionInfo = tokenizer.Tokenize(expression);
                lastCommandInfo = paser.Parse(expressionInfo, lastCommandInfo);
            }

            ParseResult = lastCommandInfo;

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
