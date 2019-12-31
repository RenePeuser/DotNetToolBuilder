namespace DotNetTool.Builder.Test.Tokenizer
{
    using System.Collections.Generic;
    using System.Linq;
    using DotNetTool.Builder.Tokenizer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    public abstract class TokenizerTestBase
    {
        internal IExpressionTokenizer ExpressionTokenizer { get; private set; }
        internal ExpressionInfo ExpressionInfo { get; private set; }

        [TestInitialize]
        public void Init()
        {
            ExpressionTokenizer = new ExpressionTokenizer(GetTokenizer().ToList());
            ExpressionInfo = ExpressionTokenizer.Tokenize(GetExpression());
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
