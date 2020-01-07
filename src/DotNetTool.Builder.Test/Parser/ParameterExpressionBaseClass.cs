using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Parser.Argument;
using DotNetTool.Builder.Parser.Commands;
using DotNetTool.Builder.Parser.Options;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

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
            var consoleService = Substitute.For<IConsoleService>();
            var collectTillOk = Substitute.For<ICollectTillInputCorrect>();
            var argumentTypeOptimizer = new ArgumentTypeOptimizer(GetTypeNameOptimizers().ToList());
            var paser = new ParameterExpressionParser(new CommandParser(consoleService), new ArgumentParser(consoleService, argumentTypeOptimizer), new OptionParser(consoleService, collectTillOk), new ParameterService());
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

        private IEnumerable<ITypeNameOptimizer> GetTypeNameOptimizers()
        {
            yield return new FileInfoOptimizer();
            yield return new DirectoryInfoOptimizer();
            yield return new FileSystemInfoOptimizer();
        }

        protected abstract void OnInit();
    }
}
