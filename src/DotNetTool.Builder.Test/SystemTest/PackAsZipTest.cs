using System;
using System.Collections.Generic;
using System.IO;
using Extensions.Pack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Builder.Test.SystemTest
{
    [TestClass]
    public class PackAsZipTest : SystemTestBase
    { 
        private FileInfo _targetZipFile;

        protected override void BeforeExecution()
        {
            base.BeforeExecution();

            _targetZipFile = new FileInfo(Path.Combine(Environment.CurrentDirectory, "dotnet.tool.install.zip"));
        }

        [TestMethod]
        public void Should_Print_Out_Correct_Message()
        {
            Assert.IsTrue(_targetZipFile.Exists, Output);
        }

        protected override void AfterExecution()
        {
            _targetZipFile.Exists.IfTrueThen(() => _targetZipFile.Delete());
            base.AfterExecution();
        }

        protected override IEnumerable<string> CollectConsoleParameters()
        {
            yield return "--from-file";
            yield return Path.Combine(Environment.CurrentDirectory, "GeneratedTools", "dotnet.tool.install.json");
            yield return "--as-zip";
            yield return _targetZipFile.FullName;
        }
    }
}