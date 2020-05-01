using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.SystemTest
{
    [TestClass]
    public class TestInvalidFiles : SystemTestBase
    {
        [TestMethod]
        public void Should_Return_Not_Ok_Code()
        {
            Assert.AreNotEqual(ExitCode, 0);
        }

        [TestMethod]
        public void Should_Print_Out_Correct_Message()
        {
            Assert.IsTrue(Output.Contains("Could not find strategy for your given parameters:"));
        }

        protected override IEnumerable<string> CollectConsoleParameters()
        {
            yield return "--from-file";
            yield return "notexistingfile.xml";
        }
    }
}