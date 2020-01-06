using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.Tokenizer
{
    [TestClass]
    public class ExprssionEmptyTest : TokenizerTestBase
    {
        [TestMethod]
        public void Should_Creatable()
        {
            Assert.IsNotNull(ExpressionInfo);
        }

        [TestMethod]
        public void Should_Return_Empty_Expression()
        {
            Assert.AreEqual(GetExpression(), ExpressionInfo.Expression);
        }

        [TestMethod]
        public void Should_Return_No_Tokens()
        {
            Assert.IsFalse(ExpressionInfo.Tokens.Any());
        }

        [TestMethod]
        public void Should_Return_Empty_Optiimized_Expression()
        {
            Assert.AreEqual(GetExpression(), ExpressionInfo.OptimizedExpressions);
        }

        protected override string GetExpression()
        {
            return "";
        }
    }
}
