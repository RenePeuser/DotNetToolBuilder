using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Tokenizer
{
    public abstract class TokenizerTestBase
    {
        internal ExpressionInfo ExpressionInfo { get; private set; }

        [TestInitialize]
        public void Init()
        {
            var tokenizer = new ToolBuilder.FromConsole.Tokenizer.Tokenizer(GetTokenizer().ToList());
            ExpressionInfo = tokenizer.Tokenize(GetExpression());
        }

        protected abstract string GetExpression();

        private IEnumerable<ITokenizer> GetTokenizer()
        {
            yield return new ArgumentTokenizer();
            yield return new OptionTokenizer();
            yield return new CommandTokenizer();
        }
    }
}
