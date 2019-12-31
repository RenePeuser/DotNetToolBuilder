namespace DotNetTool.Builder.Test.Parser.MultiExpressions.Ideal
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class Sub_Command_2_Option_Test : ParameterExpressionBaseClass
    {
        private OptionInfo _option;
        private IEnumerable<OptionInfo> _options;

        protected override IEnumerable<string> GetExpressionsToParse()
        {
            yield return "tool command1 <command1-arg>[System.IO.FileInfo] --version <version>";
            yield return "tool command2 <command2-arg>[string] --version <version>";
        }

        protected override void OnInit()
        {
            _options = ParseResult.SubCommands.First().Options;
            _option = _options.First();
        }

        [TestMethod]
        public void Assert_Option_Count()
        {
            Assert.AreEqual(1, _options.Count());
        }

        [TestMethod]
        public void Assert_Option_Name()
        {
            Assert.AreEqual("version", _option.Name);
        }

        [TestMethod]
        public void Assert_Option_Normalized_Name()
        {
            Assert.AreEqual("Version", _option.NormalizedName);
        }
    }
}
