using System.Linq;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Tokenizer
{
    [TestClass]
    public class OnlyOptionTest : TokenizerTestBase
    {
        [TestMethod]
        public void Should_Creatable()
        {
            Assert.IsNotNull(ExpressionInfo);
        }

        [TestMethod]
        public void Should_Return_Original_Expression()
        {
            Assert.AreEqual(GetExpression(), ExpressionInfo.Expression);
        }

        [TestMethod]
        public void Should_Return_One_Tokens()
        {
            Assert.AreEqual(1, ExpressionInfo.Tokens.Count());
        }

        [TestMethod]
        public void Should_Return_Command_Token()
        {
            var commandToken = ExpressionInfo.Tokens.OfType<OptionToken>();
            Assert.IsNotNull(commandToken);
        }

        [TestMethod]
        public void Should_Return_OptimizedExpressions_Equals_To_OriginalExpression()
        {
            Assert.AreEqual(GetExpression(), ExpressionInfo.OptimizedExpressions);
        }

        protected override string GetExpression()
        {
            return "--option";
        }
    }
}
