namespace DotNetTool.Builder.Test.Tokenizer
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.IsTrue(ExpressionInfo.Tokens.IsEmpty());
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