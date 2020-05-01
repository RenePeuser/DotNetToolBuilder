using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.Services.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using DotNetTool.Builder.ToolBuilder.FromConsole.Parser;
using DotNetTool.Builder.ToolBuilder.FromConsole.Parser.Argument;
using DotNetTool.Builder.ToolBuilder.FromConsole.Parser.Commands;
using DotNetTool.Builder.ToolBuilder.FromConsole.Parser.Options;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer;
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
            var collectTillOk = Substitute.For<ICollectTillInputCorrect>();
            var descriptionCollector = Substitute.For<ICollectDescription>();
            var optionAliasCollector = Substitute.For<ICollectOptionAlias>();
            var dotNetToolNameNormalizer = new DotNetToolNameNormalizer();
            var argumentTypeOptimizer = new ArgumentTypeOptimizer(GetTypeNameOptimizers().ToList());
            var argumentParser = new ArgumentParser(argumentTypeOptimizer, descriptionCollector);
            var optionParser = new OptionParser(collectTillOk, optionAliasCollector, descriptionCollector);
            var parser = new ParameterExpressionParser(new CommandParser(descriptionCollector, dotNetToolNameNormalizer), argumentParser, optionParser, new ParameterService());
            var tokenizer = new ToolBuilder.FromConsole.Tokenizer.Tokenizer(GetTokenizer().ToList());
            var expressions = GetExpressionsToParse().ToList();

            CommandInfo lastCommandInfo = null;
            foreach (var expression in expressions)
            {
                var expressionInfo = tokenizer.Tokenize(expression);
                lastCommandInfo = parser.Parse(expressionInfo, lastCommandInfo);
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
            var builtInTypetable = new BuiltInTypeTableService();
            var primitiveTypeNameValidator = new PrimitiveTypeNameValidator(builtInTypetable);

            yield return new FileInfoOptimizer();
            yield return new DirectoryInfoOptimizer();
            yield return new FileSystemInfoOptimizer();
            yield return new SystemTypeNameOptimizer(primitiveTypeNameValidator, builtInTypetable);
        }

        protected abstract void OnInit();
    }
}
